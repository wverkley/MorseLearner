using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wxv.MorseLearner
{
	public class MorseDocument
	{
		public const int DefaultWpm = 5;
		public const int DefaultOverallWpm = 5;

		private ArrayList intervals = new ArrayList();
		private DateTime startTime;

		
		public DateTime StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}


		public DateTime EndTime
		{
			get
			{
				DateTime result = startTime;
				if (intervals.Count > 0)
					result += new TimeSpan (GetTotalInterval() * TimeSpan.TicksPerMillisecond);
				return result;
			}
		}

		
		public int Count
		{
			get { return intervals.Count;	}
		}


		public bool IsEmpty
		{
			get { return intervals.Count == 0; }
		}

		
		public int GetInterval (int index)
		{
			return (int) intervals [index];
		}


		public void SetInterval (int index, int value)
		{
			intervals [index] = value;
		}


		public int this [int index]
		{
			get { return GetInterval (index); }
		}
		

		public int GetLastInterval ()
		{
			if (intervals.Count == 0)
				return 0;
			else
				return GetInterval (intervals.Count - 1);
		}


		public void AddInterval (int value)
		{
			if (value == 0)
				return;

			if (intervals.Count == 0)
			{				
				intervals.Add (value);
			}
			else 
			{
				if (Math.Sign (GetLastInterval()) != Math.Sign (value))
					intervals.Add (value);
				else 
					intervals [intervals.Count - 1] = GetLastInterval() + value;
			}
		}


		public void AddInterval (int value, bool state)
		{
			AddInterval (value * (state ? 1 : -1));
		}


		public int GetTotalInterval (int from, int to)
		{
			int result = 0;
			for (int index = from; index <= to; index++)
				result += Math.Abs (GetInterval (index));
			return result;
		}


		public int GetTotalInterval ()
		{
			return GetTotalInterval (0, intervals.Count - 1);
		}


		public int TotalInterval
		{
			get { return GetTotalInterval(); }
		}

		
		public int GetIntervals (int from, int to, out int[] intervals)
		{
			int length = to - from + 1;
			if (length <= 0)
			{
				intervals = new int[]{};
				return 0;
			}

			intervals = new int[length];
			int result = 0;
			for (int index = from; index <= to; index++)
			{
				intervals [index - from] = Math.Abs (GetInterval (index));
				result += Math.Abs (GetInterval (index));
			}
			return result;
		}


		private int wpm = DefaultWpm;
		public int Wpm
		{
			get { return wpm; }
			set { wpm = value; }
		}


		private int overallWpm = DefaultOverallWpm;
		public int OverallWpm
		{
			get { return overallWpm; }
			set { overallWpm = value; }
		}


		public MorseDocument()
		{
			Clear();
		}


		public void Clear()
		{
			intervals.Clear();
			startTime = DateTime.MinValue;
		}


		public string AsMorseMail 
		{
			get { return MorseMail.Export (this); }
			set { MorseMail.Import (this, value); }
		}


		public string AsParseReport 
		{
			get { return GetAsParseReport(); }
		}


		public string GetAsParseReport ()
		{
			return GetAsParseReport (0); 
		}

		
		public string GetAsParseReport (int wpm)
		{
			MorseDocumentParser parser = new MorseDocumentParser (this);
			return parser.ParseReport (wpm); 
		}


		public string AsMorseCsv
		{
			get { return MorseDocumentCsv.Export (this); }
			set { MorseMail.Import (this, value); }
		}


		public MorseDocumentStats.IntervalFrequency[] ExportFrequencyStats (int division)
		{
			return MorseDocumentStats.ExportFrequencyStats (this, division);
		}


		public string ExportFrequencyStatsCsv (int division)
		{
			return MorseDocumentStats.ExportFrequencyStatsCsv (this, division);
		}


		public string Parse(int wpm)
		{
			MorseDocumentParser parser = new MorseDocumentParser (this);
			return parser.Parse (wpm);
		}


		public string Parse()
		{
			return Parse (0);
		}


		private void AddTextChar (string character)
		{
			MorseConstant c = MorseConstant.GetByCharacter (character.ToString());
				
			bool firstAction = true;
			foreach (char action in c.Morse)
			{
				if (!firstAction)
					AddInterval (MorseConstant.IntraCharLength (wpm), false);
				firstAction = false;

				if (action == '-')
					AddInterval (MorseConstant.DahLength (wpm), true);
				else if (action == '.')
					AddInterval (MorseConstant.DitLength (wpm), true);
			}
		}


		public void AddText (string message)
		{
			bool inWord = false;
			foreach (char character in message.ToCharArray())
			{
				MorseConstant morse = MorseConstant.GetByCharacter (character.ToString());
				if (morse != null)
				{
					if (inWord)
						AddInterval (MorseConstant.InterCharLength (wpm, overallWpm), false);
					inWord = true;

					AddTextChar (character.ToString());
				}
				else
				{
					if (inWord)
						AddInterval (MorseConstant.InterWordLength (wpm, overallWpm), false);
					inWord = false;
				}
			}
		}

		
		public void AddInterChar ()
		{
			AddInterval (MorseConstant.InterCharLength (wpm, overallWpm), false);
		}


		public void AddInterWord ()
		{
			AddInterval (MorseConstant.InterWordLength (wpm, overallWpm), false);
		}


		public string Text
		{
			get 
			{ 
				return Parse(); 
			}
			set 
			{ 
				Clear();
				AddText (value); 
			} 
		}
	}

}
