using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
/*
	Simple class to poll the DSR/CTS line on a Serial COM port.

	Has a lot of trouble doing the polling on a worker thread and invoking the main thread again.
	Was getting terminal exceptions on outwards Win32 calls.
	Therefore using a windows timer instead in the main thread instead.  Horribly ineffecient, 
	but its safe, simple, and it works.
*/
namespace Wxv.MorseLearner
{
	public enum ComDeviceEnum {COM1, COM2, COM3, COM4};
	public enum ComDevicePinEnum {DSR, CTS};

	public class ComDeviceHook : IDisposable
	{
		public const ComDeviceEnum DefaultComDevice = ComDeviceEnum.COM1;
		public const ComDevicePinEnum DefaultComDevicePin = ComDevicePinEnum.DSR;

		private const int TimerInterval = 5;

		private IntPtr hCom;
		private System.Windows.Forms.Timer timer;
		
		private bool open;

		private ComDeviceEnum comDevice = DefaultComDevice;
		public ComDeviceEnum ComDevice
		{
			get { return comDevice; }
			set { comDevice = value; }
		}

		private ComDevicePinEnum comDevicePin = DefaultComDevicePin;
		public ComDevicePinEnum ComDevicePin
		{
			get { return comDevicePin; }
			set { comDevicePin = value; }
		}

		private WaitCallback onKeyDown;
		public WaitCallback OnKeyDown
		{
			get { return onKeyDown; }
			set { onKeyDown = value; }
		}

		private WaitCallback onKeyUp;
		public WaitCallback OnKeyUp
		{
			get { return onKeyUp; }
			set { onKeyUp = value; }
		}

		public ComDeviceHook ()
		{
		}

		public void Dispose()
		{
			Close();
			timer.Dispose();
		}
		
		private Int32 modemStatus = 0;
		private Int32 lastModemStatus = 0;
		private Boolean pinOn = false;

		private void Poll(object state)
		{
			if (!ComLib.GetCommModemStatus (hCom, ref modemStatus))
				return;

			if (lastModemStatus != modemStatus)
			{
				bool pin = false;
				if (comDevicePin == ComDevicePinEnum.DSR)
					pin = ((modemStatus & (int) ComLib.ModemStatusBits.DataSetReadyOn) != 0);
				else if (comDevicePin == ComDevicePinEnum.CTS)
					pin = ((modemStatus & (int) ComLib.ModemStatusBits.ClearToSendOn) != 0);

				if (!pinOn && pin)
				{
					lastModemStatus = modemStatus;
					pinOn = true;
					if (onKeyDown != null)
						onKeyDown (this);
				}
				else if (pinOn && !pin)
				{
					lastModemStatus = modemStatus;
					pinOn = false;

					if (onKeyUp != null)
						onKeyUp (this);
				}
			}
		}

		private void TimerThread (object sender, EventArgs e)
		{
			Poll (sender);
		}

		public void Open()
		{
			if (open)
				return;

			hCom = ComLib.CreateFile (comDevice.ToString(),
				ComLib.GENERIC_READ | ComLib.GENERIC_WRITE,
				0, // exclusive access 
				0, // default security attributes 
				ComLib.OPEN_EXISTING,
				ComLib.FILE_FLAG_OVERLAPPED,
				0);
			if (hCom == IntPtr.Zero) 
				throw new Exception ("Unable to open Com Device");

			modemStatus = 0;
			lastModemStatus = 0;
			pinOn = false;

			open = true;

			timer = new System.Windows.Forms.Timer ();
			timer.Interval = TimerInterval;
			timer.Tick += new System.EventHandler (TimerThread);
			timer.Start();
		}

		public void Close()
		{
			if (!open)
				return;

			timer.Enabled = false;
			ComLib.CloseHandle (hCom);
			open = false;
		}

	}

}
