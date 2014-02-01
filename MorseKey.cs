using System;
using System.Threading;
using System.Windows.Forms;

namespace Wxv.MorseLearner
{
	public class MorseKey : IDisposable
	{
		private const int WaveDuration = 5000;
		public const int DefaultFrequency = 875;

		public const bool DefaultUseKeyboard = true;
		public const Keys DefaultKey = Keys.RControlKey;
		public const bool DefaultUseMouse = false;
		public const MouseButtons DefaultMouseButton = MouseButtons.Left;
		public const bool DefaultUseComDevice = true;
		public const ComDeviceEnum DefaultComDevice = ComDeviceEnum.COM1;
		public const ComDevicePinEnum DefaultComDevicePin = ComDevicePinEnum.DSR;
		
		private Control control;
		private bool active = false;
		private bool paused = false;
		private WaveDevice waveDevice;

		private KeyActivityHook keyHook = new KeyActivityHook ();
		private MouseActivityHook mouseHook = new MouseActivityHook ();
		private ComDeviceHook comDeviceHook = new ComDeviceHook ();

		bool keydown = false;
		DateTime start = DateTime.Now;

		private bool useKeyboard = DefaultUseKeyboard;
		public bool UseKeyboard
		{
			get { return useKeyboard; }
			set { useKeyboard = value; }
		}

		private Keys key = DefaultKey;
		public Keys Key
		{
			get { return key; }
			set { key = value; }
		}

		private bool useMouse = DefaultUseMouse;
		public bool UseMouse
		{
			get { return useMouse; }
			set { useMouse = value; }
		}

		private MouseButtons mouseButton = DefaultMouseButton;
		public MouseButtons MouseButton
		{
			get { return mouseButton; }
			set { mouseButton = value; }
		}

		private bool useComDevice = DefaultUseComDevice;
		public bool UseComDevice
		{
			get { return useComDevice; }
			set { useComDevice = value; }
		}

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

		private int frequency = DefaultFrequency;
		public int Frequency
		{
			get 
			{ 
				return frequency; 
			}
			set 
			{ 
				if (active) 
					throw new Exception ("Cannot set frequency while playing");
				if ((frequency != value) && (waveDevice != null))
				{
					waveDevice.Dispose(); 
					waveDevice = null;
				}
				frequency = value;
			}
		}

		private void BuildWaveDevice()
		{
			if (waveDevice != null)
				return;

			WaveGenerator generator = new WaveGenerator();
			generator.Frequency = frequency;
			generator.Duration = WaveDuration;

			WaveBuffer buffer = generator.Generate();
			waveDevice = new WaveDevice (buffer);
		}

		public MorseKey(Control control)
		{
			this.control = control;

			keyHook.OnKeyDown += new KeyEventHandler (MorseKey_KeyDown);
			keyHook.OnKeyUp += new KeyEventHandler (MorseKey_KeyUp);

			mouseHook.OnMouseDown += new MouseEventHandler (MorseKey_MouseDown);
			mouseHook.OnMouseUp += new MouseEventHandler (MorseKey_MouseUp);

			comDeviceHook.OnKeyDown = new WaitCallback (MorseKey_ComKeyDown);
			comDeviceHook.OnKeyUp = new WaitCallback (MorseKey_ComKeyUp);
		}

		public void Dispose()
		{
			Close();
		}

		public void Close()
		{
			if (!active)
				return;

			keyHook.Close(); 
			mouseHook.Close();
			comDeviceHook.Close();

			active = false;
		}

		public void Open()
		{
			active = true;
			keydown = false;
			paused = false;

			if (useComDevice)
			{
				comDeviceHook.ComDevice = ComDevice;
				comDeviceHook.ComDevicePin = ComDevicePin;
				try
				{
					comDeviceHook.Open();
				}
				catch (Exception) {}
			}

			if (useKeyboard)
				keyHook.Open();

			if (useMouse)
				mouseHook.Open();
		}

		public void Stop()
		{
			if (!active || paused)
				return;

			if (keydown)
				waveDevice.Stop();

            keydown = false;
			paused = true;
		}
	
		public void Start()
		{
			if (!active || !paused)
				return;

			BuildWaveDevice();

			paused = false;
			keydown = false;
		}

		private void MorseKeyDown(object state)
		{
			if (!paused && !keydown)
			{
				DateTime now = DateTime.Now;
				TimeSpan ts = now - start;

				start = DateTime.Now;
				keydown = true;

				waveDevice.Play();
				if (onKeyDown != null)
					onKeyDown (this);
			}
		}

		private void MorseKeyUp(object state)
		{
			if (!paused && keydown)
			{
				DateTime now = DateTime.Now;
				TimeSpan ts = now - start;

				start = DateTime.Now;
				keydown = false;
				waveDevice.Stop();
				if (onKeyUp != null)
					onKeyUp (this);
			}
		}

		private void MorseKey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == key) 
				MorseKeyDown(null);
		}

		private void MorseKey_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == key) 
				MorseKeyUp(null);
		}

		private void MorseKey_ComKeyDown(object sender)
		{
			MorseKeyDown (null);
		}

		private void MorseKey_ComKeyUp(object sender)
		{
			MorseKeyUp(null);
		}
	
		private void MorseKey_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == mouseButton)
				MorseKeyDown(null);
		}

		private void MorseKey_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == mouseButton)
				MorseKeyUp(null);
		}

	}
}
