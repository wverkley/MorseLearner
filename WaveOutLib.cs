using System;
using System.Runtime.InteropServices;

namespace Wxv.MorseLearner
{
	public enum WaveFormats
	{
		Pcm = 1,
		Float = 3
	}

	[StructLayout(LayoutKind.Sequential)] 
	public class WaveFormat
	{
		public short FormatTag;
		public short Channels;
		public int SamplesPerSecond;
		public int AverageBytesPerSecond;
		public short BlockAlign;
		public short BitsPerSample;
		public short cbSize;

		public WaveFormat (int samplesPerSecond, short bitsPerSample, short channels)
		{
			this.FormatTag = (short) WaveFormats.Pcm;
			this.SamplesPerSecond = samplesPerSecond;
			this.BitsPerSample = bitsPerSample;
			this.Channels = channels;
			this.cbSize = 0;
               
			this.BlockAlign = (short) (channels * (bitsPerSample / 8));
			this.AverageBytesPerSecond = SamplesPerSecond * BlockAlign;
		}
	}

	[StructLayout(LayoutKind.Sequential)] 
	public struct WaveHeader
	{
		public IntPtr lpData; 
		public int dwBufferLength; 
		public int dwBytesRecorded;
		public IntPtr dwUser; 
		public int dwFlags; 
		public int dwLoops; 
		public IntPtr lpNext;
		public int reserved; 
	}


	public delegate void WaveDelegate(IntPtr hdrvr, int uMsg, int dwUser, ref WaveHeader wavhdr, int dwParam2);


	public class WaveOutLib
	{
		public const int MMSYSERR_NOERROR = 0; 
		
		public static void CheckResult (int result)
		{
			if (result != WaveOutLib.MMSYSERR_NOERROR)
				throw new Exception (result.ToString());
		}

		public const int MM_WOM_OPEN = 0x3BB;
		public const int MM_WOM_CLOSE = 0x3BC;
		public const int MM_WOM_DONE = 0x3BD;

		public const int CALLBACK_TYPEMASK   = 0x00070000;    // callback type mask 
		public const int CALLBACK_NULL       = 0x00000000;    // no callback 
		public const int CALLBACK_WINDOW     = 0x00010000;    // dwCallback is a HWND 
		public const int CALLBACK_TASK       = 0x00020000;    // dwCallback is a HTASK 
		public const int CALLBACK_FUNCTION   = 0x00030000;    // dwCallback is a FARPROC 
		public const int CALLBACK_THREAD     = CALLBACK_TASK; // thread ID replaces 16 bit task 
		public const int CALLBACK_EVENT      = 0x00050000;    // dwCallback is an EVENT Handle 
		
		public const int TIME_MS = 0x0001;  
		public const int TIME_SAMPLES = 0x0002;  
		public const int TIME_BYTES = 0x0004;  

		public const int WHDR_DONE       = 0x00000001;  // done bit 
		public const int WHDR_PREPARED   = 0x00000002;  // set if this header has been prepared 
		public const int WHDR_BEGINLOOP  = 0x00000004;  // loop start block 
		public const int WHDR_ENDLOOP    = 0x00000008;  // loop end block 
		public const int WHDR_INQUEUE    = 0x00000010;  // reserved for driver 

		private const string mmdll = "winmm.dll";

		[DllImport(mmdll)]
		public static extern int waveOutGetNumDevs();

		[DllImport(mmdll)]
		public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WaveHeader lpWaveOutHdr, int uSize);
		
		[DllImport(mmdll)]
		public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WaveHeader lpWaveOutHdr, int uSize);
		
		[DllImport(mmdll)]
		public static extern int waveOutWrite(IntPtr hWaveOut, ref WaveHeader lpWaveOutHdr, int uSize);
		
		[DllImport(mmdll)]
		public static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, WaveFormat lpFormat, IntPtr dwCallback, int dwInstance, int dwFlags);
		
		[DllImport(mmdll)]
		public static extern int waveOutOpen(out IntPtr hWaveOut, int uDeviceID, WaveFormat lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);

		[DllImport(mmdll)]
		public static extern int waveOutReset(IntPtr hWaveOut);
		
		[DllImport(mmdll)]
		public static extern int waveOutClose(IntPtr hWaveOut);
		
		[DllImport(mmdll)]
		public static extern int waveOutPause(IntPtr hWaveOut);
		
		[DllImport(mmdll)]
		public static extern int waveOutRestart(IntPtr hWaveOut);
		
		[DllImport(mmdll)]
		public static extern int waveOutGetPosition(IntPtr hWaveOut, out int lpInfo, int uSize);
		
		[DllImport(mmdll)]
		public static extern int waveOutSetVolume(IntPtr hWaveOut, int dwVolume);
		
		[DllImport(mmdll)]
		public static extern int waveOutGetVolume(IntPtr hWaveOut, out int dwVolume);
	}

}
