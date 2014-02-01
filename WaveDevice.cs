using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace Wxv.MorseLearner
{
	public class WaveDevice : IDisposable
	{
		private WaveBuffer buffer;
		private IntPtr waveOutHandle = IntPtr.Zero;
		private GCHandle dataHandle;

		private WaveHeader header = new WaveHeader();

		private bool playing = false;
		private AutoResetEvent finishedEvent = new AutoResetEvent (false);


		public WaveDevice (WaveBuffer buffer)
		{
			this.buffer = buffer;
			Open();
		}

		
		public void Dispose()
		{
			Stop();
			Close();
		}
		

		private void Open()
		{
			WaveOutLib.CheckResult (WaveOutLib.waveOutOpen (
				out waveOutHandle, 
				-1,
				buffer.Format,
				finishedEvent.Handle,
				0, 
				WaveOutLib.CALLBACK_EVENT));

			dataHandle = GCHandle.Alloc(buffer.Data, GCHandleType.Pinned);
			header.lpData = dataHandle.AddrOfPinnedObject();
			header.dwBufferLength = buffer.Data.Length;
			header.dwFlags = WaveOutLib.WHDR_BEGINLOOP + WaveOutLib.WHDR_ENDLOOP;
			header.dwLoops = 99999;

			WaveOutLib.CheckResult (WaveOutLib.waveOutPrepareHeader (
				waveOutHandle, ref header, Marshal.SizeOf(header)));
		}


		private void Close()
		{
			WaveOutLib.waveOutUnprepareHeader (waveOutHandle, ref header, Marshal.SizeOf(header));
			dataHandle.Free();
			WaveOutLib.waveOutClose (waveOutHandle);
		}


		public void Play()
		{
			Stop();

			finishedEvent.Reset();
			WaveOutLib.CheckResult (WaveOutLib.waveOutWrite (
				waveOutHandle, ref header, Marshal.SizeOf(header)));
			playing = true;
		}


		public void Stop()
		{
			if (playing)
			{
				WaveOutLib.CheckResult (WaveOutLib.waveOutReset (waveOutHandle));
				finishedEvent.WaitOne (100, true);
				playing = false;
			}
		}
	
	}

}
