using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Wxv.MorseLearner
{
	public class MorseLearnerConfig
	{
		public enum DifficultyEnum {Learner, Intermediate, Advanced};

		public static Keys[] InputKeys = new Keys[] 
		{
			Keys.LControlKey, 
			Keys.Alt, 
			Keys.RControlKey, 
			Keys.Down, 
			Keys.Insert, 
			Keys.Delete
		};
		public static string[] InputKeyNames = new string[] 
		{
			"Left Control", 
			"Alt", 
			"Right Control", 
			"Down", 
			"Insert", 
			"Delete"
		};

		public static MouseButtons[] InputMouseButtons = new MouseButtons[] 
		{
			MouseButtons.Left, 
			MouseButtons.Right
		};
		public static string[] InputMouseNames = new string[] 
		{
			"Left Mouse Button", 
			"Right Mouse Button"
		};

		public const string ConfigFilename = "MorseLearner_Config.xml";
		public const string DataFilename = "Data.pak";
		public const string FontFilename = @"slkscr.ttf";
		public const string WordStartSoundFilename = @"sounds\wordStart.wav";
		public const string WordFinishSoundFilename = @"sounds\wordFinish.wav";
		public const string VocabularyFilename = @"Vocabulary.txt";

		public const int MinFrequency = 250;
		public const int MaxFrequency = 2500;
		public const int StepFrequency = 25;
		public const int DefaultFrequency = 875;

		public const int MinWpm = 5;
		public const int MaxWpm = 30;
		public const int StepWpm = 1;
		public const int DefaultWpm = 16;
		public const int DefaultOverallWpm = 5;

		public const int ControlCInterval = 250;

		public const int WaitInputDelayLearner = 5000;
		public const int WaitInputDelayIntermediate = 3000;
		public const int WaitInputDelayAdvanced = 2000;

        public const bool DefaultShowMorse = true;

		public const int DefaultStartTransmitDelay = 500;
		public const int DefaultShowCorrectResultDelay = 150;
		public const int DefaultShowCorrectWordResultDelay = 750;
		public const int DefaultShowIncorrectResultDelay = 2000;
		public const double DefaultWordChance = 0.33;
		public const int DefaultSessionLength = 100;
		public const int DefaultSessionStartDelay = 1000;

		public const bool DefaultUseComDevice = false;
		public const bool DefaultUseKeyboard = true;
		public const bool DefaultUseMouse = false;
		public const Keys DefaultKey = Keys.RControlKey;
		public const MouseButtons DefaultMouseButton = MouseButtons.Left;
		public const ComDeviceEnum DefaultComDevice = ComDeviceEnum.COM1;
		public const ComDevicePinEnum DefaultComDevicePin = ComDevicePinEnum.DSR;
		public const bool DefaultSendKeys = false;
		public const int DefaultAutoClearDelay = 10000;
		public const int DefaultMessageEndDelay = 2000;
		public const DifficultyEnum DefaultDifficulty = DifficultyEnum.Learner;

		public MorseLearnerConfig() 
		{
		}

        private int wpm = DefaultWpm;
		[XmlAttribute()]
		public int Wpm 
		{
			get { return wpm; }
			set { wpm = value; }
		}
		
		private int overallWpm = DefaultOverallWpm;
		[XmlAttribute()]
		public int OverallWpm
		{
			get { return overallWpm; }
			set { overallWpm = value; }
		}

		private int frequency = DefaultFrequency;
		[XmlAttribute()]
		public int Frequency
		{
			get { return frequency; }
			set { frequency = value; }
		}

		private DifficultyEnum difficulty = DefaultDifficulty;
		[XmlAttribute()]
		public DifficultyEnum Difficulty
		{
			get { return difficulty; }
			set { difficulty = value; }
		}

        private bool showMorse = DefaultShowMorse;
        [XmlAttribute()]
        public bool ShowMorse
        {
            get { return showMorse; }
            set { showMorse = value; }
        }

		private bool useKeyboard = DefaultUseKeyboard;
		[XmlAttribute()]
		public bool UseKeyboard
		{
			get { return useKeyboard; }
			set { useKeyboard = value; }
		}

		private Keys key = DefaultKey;
		[XmlAttribute()]
		public Keys Key
		{
			get { return key; }
			set { key = value; }
		}

		private bool useMouse = DefaultUseMouse;
		[XmlAttribute()]
		public bool UseMouse
		{
			get { return useMouse; }
			set { useMouse = value; }
		}

		private MouseButtons mouseButton = DefaultMouseButton;
		[XmlAttribute()]
		public MouseButtons MouseButton
		{
			get { return mouseButton; }
			set { mouseButton = value; }
		}

		private bool useComDevice = DefaultUseComDevice;
		[XmlAttribute()]
		public bool UseComDevice
		{
			get { return useComDevice; }
			set { useComDevice = value; }
		}

		private ComDeviceEnum comDevice = DefaultComDevice;
		[XmlAttribute()]
		public ComDeviceEnum ComDevice
		{
			get { return comDevice; }
			set { comDevice = value; }
		}

		private ComDevicePinEnum comDevicePin = DefaultComDevicePin;
		[XmlAttribute()]
		public ComDevicePinEnum ComDevicePin
		{
			get { return comDevicePin; }
			set { comDevicePin = value; }
		}

		private bool sendKeys = DefaultSendKeys;
		[XmlAttribute()]
		public bool SendKeys
		{
			get { return sendKeys; }
			set { sendKeys = value; }
		}

		public int SessionStartDelay 
		{
			get { return DefaultSessionStartDelay; }
		}

		public int StartTransmitDelay 
		{
			get { return DefaultStartTransmitDelay; }
		}

		public int WaitInputDelay 
		{
			get 
			{ 
				if (difficulty == DifficultyEnum.Learner)
					return WaitInputDelayLearner;
				else if (difficulty == DifficultyEnum.Intermediate)
					return WaitInputDelayIntermediate;
				else 
					return WaitInputDelayAdvanced;
			}
		}

		public int ShowCorrectResultDelay 
		{
			get { return DefaultShowCorrectResultDelay; }
		}

		public int ShowCorrectWordResultDelay 
		{
			get { return DefaultShowCorrectWordResultDelay; }
		}

		public int ShowIncorrectResultDelay 
		{
			get { return DefaultShowIncorrectResultDelay; }
		}

		public double WordChance 
		{
			get { return DefaultWordChance; }
		}

		public int SessionLength 
		{
			get { return DefaultSessionLength; }
		}

		public int AutoClearDelay
		{
			get { return DefaultAutoClearDelay; }
		}

		public int MessageEndDelay
		{
			get { return DefaultMessageEndDelay; }
		}

        public static string FileName
        {
            get
            {
                return
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    + "\\" + ConfigFilename;
            }
        }

        private static MorseLearnerConfig instance;
		public static MorseLearnerConfig GetInstance()
		{
			if (instance == null)
			{
                if (File.Exists(FileName))
				{
					XmlSerializer ser = new XmlSerializer (typeof (MorseLearnerConfig));
                    using (StreamReader reader = new StreamReader(FileName))
						instance = (MorseLearnerConfig) ser.Deserialize (reader);
				}
				else
					 instance = new MorseLearnerConfig();
			}
			return instance;
		}


		public void SaveToFile()
		{
			XmlSerializer ser = new XmlSerializer (typeof (MorseLearnerConfig));
            using (StreamWriter writer = new StreamWriter(FileName))
				ser.Serialize (writer, this);
		}

	}
}
