using System;
using System.IO;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;

namespace Wxv.MorseLearner
{
	/*
	 * Simple class to write to a WaveOutDevice.  
	 * 
	 * While playing, write from a single thread.  Wait for it to finish (WaitAll), before using 
	 * it on another thread.
	 */
	public class WaveOutWriter : IDisposable
	{
		internal static void WaveOutProc (IntPtr hdrvr, int uMsg, int dwUser, ref WaveHeader waveheader, int dwParam2)
		{
			if (uMsg == WaveOutLib.MM_WOM_DONE)
			{
				GCHandle waveWriterHandle = (GCHandle) waveheader.dwUser;
				WaveOutWriter writer  = (WaveOutWriter) waveWriterHandle.Target;
				writer.WaveCompleted();
			}
		}


		private class WaveOutBuffer : IDisposable
		{
			private GCHandle waveWriterHandle;
			private IntPtr waveOutHandle;
			private byte[] data;
			private WaveHeader header = new WaveHeader();
			private GCHandle headerHandle;
			private GCHandle dataHandle;
			private bool prepared = false;

			private AutoResetEvent completedEvent;
			public AutoResetEvent CompletedEvent
			{
				get { return completedEvent; }
				set { completedEvent = value; }
			}

			private object tag;
			public object Tag
			{
				get { return tag; }
				set { tag = value; }
			}

			public int DataLength 
			{
				get { return data.Length; } 
			}

			public WaveOutBuffer (GCHandle waveWriterHandle, IntPtr waveOutHandle, byte[] data)
			{
				this.waveWriterHandle = waveWriterHandle;
				this.waveOutHandle = waveOutHandle;
				this.data = data;
			}

			public void Prepare()
			{
				headerHandle = GCHandle.Alloc(header, GCHandleType.Pinned);
				dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			
				header.dwUser = (IntPtr) waveWriterHandle;
				header.lpData = dataHandle.AddrOfPinnedObject();
				header.dwBufferLength = data.Length;
				WaveOutLib.CheckResult (WaveOutLib.waveOutPrepareHeader (
					waveOutHandle, ref header, Marshal.SizeOf(header)));
				prepared = true;
			}

			public void Write()
			{
				WaveOutLib.CheckResult (WaveOutLib.waveOutWrite (
					waveOutHandle, ref header, Marshal.SizeOf(header)));
			}

			public void Dispose()
			{
				if (prepared)
					WaveOutLib.waveOutUnprepareHeader (waveOutHandle, ref header, Marshal.SizeOf(header));
				if (dataHandle.IsAllocated)
					dataHandle.Free();
				if (headerHandle.IsAllocated)
					headerHandle.Free();
			}
		}

		private WaveDelegate waveOutProc = new WaveDelegate (WaveOutProc);
			
		private GCHandle waveWriterHandle;
		private WaveFormat waveFormat;
		private IntPtr waveOutHandle = IntPtr.Zero;
		private ArrayList playList = new ArrayList();
		private ArrayList completedList = new ArrayList();
		private AutoResetEvent completedEvent = new AutoResetEvent (false);
		private AutoResetEvent emptyEvent = new AutoResetEvent (false);
		private bool closing = false;

		private WaitCallback onCompleted;
		public WaitCallback OnCompleted
		{
			get { return onCompleted; }
			set { onCompleted = value; }
		}


		public WaveOutWriter (WaveFormat waveFormat)
		{
			this.waveFormat = waveFormat;
			this.waveWriterHandle = GCHandle.Alloc(this);

			WaveOutLib.CheckResult (WaveOutLib.waveOutOpen (
				out waveOutHandle, 
				-1,
				waveFormat,
				waveOutProc,
				0, 
				WaveOutLib.CALLBACK_FUNCTION));
		}
		
		public void Dispose()
		{
			closing = true;

			Stop();
			WaitAll();
			DisposeCompleted();
			WaveOutLib.waveOutClose (waveOutHandle);

			if (waveWriterHandle.IsAllocated)
				waveWriterHandle.Free();

			completedEvent.Close();
			emptyEvent.Close();
		}

		private void DisposeCompleted()
		{
			lock (playList)
			{
				foreach (WaveOutBuffer waveOutBuffer in completedList)
					waveOutBuffer.Dispose();
				completedList.Clear();
			}
		}
		
		private void WaveCompleted()
		{
			WaveOutBuffer waveOutBuffer;
			lock (playList)
			{ 
				waveOutBuffer = (WaveOutBuffer) playList[0];
				playList.RemoveAt(0);
				completedList.Add (waveOutBuffer);
				if (waveOutBuffer.CompletedEvent != null)
					waveOutBuffer.CompletedEvent.Set();
				completedEvent.Set();
				if (playList.Count == 0)
					emptyEvent.Set();
			}

			if (onCompleted != null)
				onCompleted (waveOutBuffer.Tag);
		}

		public int PlayingCount
		{
			get
			{
				lock (playList)
				{
					return playList.Count;
				}
			}
		}

		public int PlayingDataLength
		{
			get
			{
				lock (playList)
				{
					int result = 0;
					for (int i = 0; i < playList.Count; i++)
					{
						WaveOutBuffer waveOutBuffer = (WaveOutBuffer) playList[i];
						result += waveOutBuffer.DataLength;
					}
					return result;
				}
			}
		}

		public int PlayingDuration
		{
			get
			{
				return (int) ((PlayingDataLength  * 1000.0) / waveFormat.AverageBytesPerSecond);
			}
		}

		public int AheadCount
		{
			get
			{
				lock (playList)
				{
					if (playList.Count > 0)
						return playList.Count - 1;
					else
						return 0;
				}
			}
		}

		public int AheadDataLength
		{
			get
			{
				lock (playList)
				{
					int result = 0;
					for (int i = 1; i < playList.Count; i++)
					{
						WaveOutBuffer waveOutBuffer = (WaveOutBuffer) playList[i];
						result += waveOutBuffer.DataLength;
					}
					return result;
				}
			}
		}

		public int AheadDuration
		{
			get
			{
				return (int) ((AheadDataLength  * 1000.0) / waveFormat.AverageBytesPerSecond);
			}
		}

		
		public void Stop()
		{
			WaveOutLib.waveOutReset (waveOutHandle);
		}


		public void WaitAll()
		{
			emptyEvent.Reset();
			while (PlayingCount > 0)
				emptyEvent.WaitOne();
		}

		public void WaitAheadDurationBelow (int duration, int timeout)
		{
			completedEvent.Reset();
			while (AheadDuration > duration)
				completedEvent.WaitOne (timeout, false);
		}

		public void Write (byte[] data, AutoResetEvent completedEvent, object tag)
		{
			if ((data == null) || (data.Length == 0))
				throw new Exception ("Invalid arguments");
			
			DisposeCompleted();

			WaveOutBuffer waveOutBuffer = new WaveOutBuffer (waveWriterHandle, waveOutHandle, data);
			waveOutBuffer.CompletedEvent = completedEvent;
			waveOutBuffer.Tag = tag;
			waveOutBuffer.Prepare();
			lock (playList)
			{
				playList.Add (waveOutBuffer);
			}
			waveOutBuffer.Write();
		}

		public void Write (byte[] data)
		{
			Write (data, null, null);
		}

		public void WriteWait (byte[] data)
		{
			using (AutoResetEvent completedEvent = new AutoResetEvent (false))
			{
				Write (data, completedEvent, null);
				completedEvent.WaitOne();
			}
		}
	}

}
