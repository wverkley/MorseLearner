using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace Wxv.MorseLearner
{

	public class WaveBuffer
	{
		private WaveFormat format;
		public WaveFormat Format
		{
			get { return format; }
		}


		private byte[] data;
		public byte[] Data
		{
			get { return data; }
		}


		private string ReadChunk(BinaryReader reader)
		{
			byte[] ch = new byte[4];
			reader.Read(ch, 0, ch.Length);
			return System.Text.Encoding.ASCII.GetString(ch);
		}


		private void LoadFromStream (Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);
			if (ReadChunk(reader) != "RIFF")
				throw new Exception("Invalid file format");

			reader.ReadInt32();

			if (ReadChunk(reader) != "WAVE")
				throw new Exception ("Invalid file format");

			if (ReadChunk(reader) != "fmt ")
				throw new Exception ("Invalid file format");

			int len = reader.ReadInt32();
			if (len < 16)
				throw new Exception ("Invalid file format");

			format = new WaveFormat(22050, 16, 2);
			format.FormatTag = reader.ReadInt16();
			format.Channels = reader.ReadInt16();
			format.SamplesPerSecond = reader.ReadInt32();
			format.AverageBytesPerSecond = reader.ReadInt32();
			format.BlockAlign = reader.ReadInt16();
			format.BitsPerSample = reader.ReadInt16(); 

			// advance in the stream to skip the wave format block 
			len -= 16; // minimum format size
			while (len > 0)
			{
				reader.ReadByte();
				len--;
			}

			// assume the data chunk is aligned
			while(stream.Position < stream.Length && ReadChunk(reader) != "data")
			{}

			if (stream.Position >= stream.Length)
				throw new Exception("Invalid file format");

			int length = reader.ReadInt32();
			data = new byte [length];
			reader.Read (data, 0, length);
		}


		public WaveBuffer (string filename)
		{
			using (Stream stream = new FileStream (filename, FileMode.Open))
				LoadFromStream (stream);
		}


		public WaveBuffer (Stream stream)
		{
			LoadFromStream (stream);
		}


		public WaveBuffer (WaveFormat format, byte[] data)
		{
			this.format = format;
			this.data = data;
		}


		public void Play()
		{
			IntPtr waveOutHandle;

			AutoResetEvent waitEvent = new AutoResetEvent (false);
			
			WaveOutLib.CheckResult (WaveOutLib.waveOutOpen (
				out waveOutHandle, 
				-1,
				format,
				waitEvent.Handle,
				0, 
				WaveOutLib.CALLBACK_EVENT));
			try
			{
				WaveHeader header = new WaveHeader();

				GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
				header.lpData = dataHandle.AddrOfPinnedObject();
				header.dwBufferLength = data.Length;
				WaveOutLib.CheckResult (WaveOutLib.waveOutPrepareHeader (
					waveOutHandle, ref header, Marshal.SizeOf(header)));
				try
				{
					waitEvent.Reset();
					WaveOutLib.CheckResult (WaveOutLib.waveOutWrite (
						waveOutHandle, ref header, Marshal.SizeOf(header)));
					waitEvent.WaitOne();
				}
				finally
				{
					WaveOutLib.waveOutUnprepareHeader (waveOutHandle, ref header, Marshal.SizeOf(header));
				}
			}
			finally
			{
				WaveOutLib.waveOutClose (waveOutHandle);
			}
		}

	}

}
