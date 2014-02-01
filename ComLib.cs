using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace Wxv.MorseLearner
{
	public abstract class ComLib
	{
		public enum DataParity 
		{
			Parity_None = 0,
			Pariti_Odd,
			Parity_Even,
			Parity_Mark 
		}

		// StopBit Data
		public enum DataStopBit 
		{
			StopBit_1 = 1,
			StopBit_2
		}

		[FlagsAttribute]
		public enum PurgeBuffers
		{
			RXAbort = 0x2,
			RXClear = 0x8,
			TxAbort = 0x1,
			TxClear = 0x4
		}

		public enum Lines
		{
			SetRts = 3,
			ClearRts = 4,
			SetDtr = 5,
			ClearDtr = 6,
			ResetDev = 7,   // Reset device if possible
			SetBreak = 8,   // Set the device break line.
			ClearBreak = 9  // Clear the device break line.
		}

		// Modem Status
		[FlagsAttribute]
		public enum ModemStatusBits
		{
			ClearToSendOn = 0x10,
			DataSetReadyOn = 0x20,
			RingIndicatorOn = 0x40,
			CarrierDetect = 0x80
		}

		// Working mode
		public enum Mode
		{
			NonOverlapped,
			Overlapped
		}

		// Comm Masks
		[FlagsAttribute()]
		public enum EventMasks
		{
			RxChar = 0x1,
			RXFlag = 0x2,
			TxBufferEmpty = 0x4,
			ClearToSend = 0x8,
			DataSetReady = 0x10,
			CarrierDetect = 0x20,
			Break = 0x40,
			StatusError = 0x80,
			Ring = 0x100
		}


		[StructLayoutAttribute(LayoutKind.Sequential, Pack=1)]
		public struct DCB
		{
			public Int32 DCBlength;
			public Int32 BaudRate;
			public Int32 Bits1;
			public Int16 wReserved;
			public Int16 XonLim;
			public Int16 XoffLim;
			public byte ByteSize;
			public byte Parity;
			public byte StopBits;
			public char XonChar;
			public char XoffChar;
			public char ErrorChar;
			public char EofChar;
			public char EvtChar;
			public Int16 wReserved2;
		}

		[StructLayoutAttribute(LayoutKind.Sequential, Pack=1)]
		public struct COMMTIMEOUTS
		{
			public Int32 ReadIntervalTimeout;
			public Int32 ReadTotalTimeoutMultiplier;
			public Int32 ReadTotalTimeoutConstant;
			public Int32 WriteTotalTimeoutMultiplier;
			public Int32 WriteTotalTimeoutConstant;
		}

		[StructLayoutAttribute(LayoutKind.Sequential, Pack=8)]
		public struct COMMCONFIG
		{
			public Int32 dwSize;
			public Int16 wVersion;
			public Int16 wReserved;
			public DCB dcbx;
			public Int32 dwProviderSubType;
			public Int32 dwProviderOffset;
			public Int32 dwProviderSize;
			public Int16 wcProviderData;
		}
    
		[StructLayoutAttribute(LayoutKind.Sequential, Pack=1)]
		public struct OVERLAPPED
		{
			public Int32 Internal;
			public Int32 InternalHigh;
			public Int32 Offset;
			public Int32 OffsetHigh;
			public IntPtr hEvent;
		}
    
		[StructLayoutAttribute(LayoutKind.Sequential, Pack=1)]
		public struct COMSTAT
		{
			public IntPtr fBitFields;
			public IntPtr cbInQue;
			public IntPtr cbOutQue;
		}

		public const int PURGE_RXABORT = 0x2;
		public const int PURGE_RXCLEAR = 0x8;
		public const int PURGE_TXABORT = 0x1;
		public const int PURGE_TXCLEAR = 0x4;
		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x40000000;
		public const int OPEN_EXISTING = 3;
		public const int INVALID_HANDLE_VALUE = -1;
		public const int IO_BUFFER_SIZE = 1024;
		public const Int32 FILE_FLAG_OVERLAPPED = 0x40000000;
		public const Int32 ERROR_IO_PENDING = 997;
		public const Int32 WAIT_OBJECT_0 = 0;
		public const Int32 ERROR_IO_INCOMPLETE = 996;
		public const Int32 WAIT_TIMEOUT = 0x102;
		public const uint INFINITE = 0xFFFFFFFF;
		public const int MAX_WRITE_BYTE_DELAY = 500; // Maximum delay in mS permitted between writing bytes to the UART TxData port

		// Win32 API
		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 SetCommState(IntPtr hCommDev, ref DCB lpDCB);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 GetCommState(IntPtr hCommDev, ref DCB lpDCB);

		[DllImportAttribute("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		public static extern Int32 BuildCommDCB(string lpDef, ref DCB lpDCB);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 SetupComm(IntPtr hFile, Int32 dwInQueue, Int32 dwOutQueue);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 SetCommTimeouts(IntPtr hFile, ref COMMTIMEOUTS lpCommTimeouts);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 GetCommTimeouts(IntPtr hFile, ref COMMTIMEOUTS lpCommTimeouts);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 ClearCommError(IntPtr hFile, ref Int32 lpErrors, ref COMSTAT lpComStat);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 PurgeComm(IntPtr hFile, Int32 dwFlags);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern bool EscapeCommFunction(IntPtr hFile, long ifunc);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 WaitCommEvent(IntPtr hFile, ref EventMasks Mask, ref OVERLAPPED lpOverlap);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 WriteFile(IntPtr hFile, byte[] Buffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 ReadFile(IntPtr hFile, out byte[] Buffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

		[DllImportAttribute("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern bool CloseHandle(IntPtr hObject);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern bool GetCommModemStatus(IntPtr hFile, ref Int32 lpModemStatus);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 GetOverlappedResult(IntPtr hFile, ref OVERLAPPED lpOverlapped, ref Int32 lpNumberOfBytesTransferred, Int32 bWait);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern Int32 SetCommMask(IntPtr hFile, EventMasks lpEvtMask);

		[DllImportAttribute("kernel32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		public static extern bool GetDefaultCommConfig(string lpszName, ref COMMCONFIG lpCC, ref int lpdwSize);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern bool SetCommBreak(IntPtr hFile);

		[DllImportAttribute("kernel32.dll", SetLastError=true)]
		public static extern bool ClearCommBreak(IntPtr hFile);
	}

}
