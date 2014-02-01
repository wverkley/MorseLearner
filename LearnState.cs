using System;
using System.Collections;
using System.Xml;
using System.IO;

namespace Wxv.MorseLearner
{
	public class LearnCharacter
	{
		private string character;
		public string Character { get { return character; } }

		public double error = 1.0;
		public double Error { get { return error; } set { error = value; }}

		public LearnCharacter (string character)
		{
			this.character = character;
		}

		public string Morse { get {	return MorseConstant.GetByCharacter (character).Morse; } }
	}

	public class LearnState
	{
		public const string LearnStateFilename = "MorseLearner_State.xml";
		public const double DefaultOverallError = 0.3;

		public const double BeginnerLevel = 1.0;
		public const double IntermediateLevel = 0.4;
		public const double AdvancedLevel = 0.1;

		public const double BeginnerWordLevel = 3;
		public const double IntermediateWordLevel = 10;
		public const double AdvancedWordLevel = 20;
		public const double DefaultWordLevel = 3;

		public const double MinWordLevel = 3;
		public const double MaxWordLevel = 20;
		public const double WordLevelIncrement = 0.05;
		public const double WordLevelDecrement = 0.10;

		private string learnOrder = MorseConstant.DefaultLearnOrder;
		public string LearnOrder { get { return learnOrder; } }
		
		private ArrayList learnCharacters = new ArrayList();
		private double overalError = DefaultOverallError;
		
		private double wordLevel = DefaultWordLevel;


		public LearnCharacter Get (int index)
		{
			return (LearnCharacter) learnCharacters [index];
		}
		
		public IDictionary Characters
		{
			get
			{
				Hashtable result = new Hashtable();
				foreach (LearnCharacter lc in learnCharacters)
					result.Add (lc.Character, lc.Error);
				return result;
			}
		}

		public IDictionary AllCharacters
		{
			get
			{
				Hashtable result = new Hashtable();
				foreach (char c in MorseConstant.DefaultLearnOrder.ToCharArray())
				{
					LearnCharacter lc = GetByCharacter (c.ToString());
					if (lc != null)
						result.Add (lc.Character, lc.Error);
					else
						result.Add (c.ToString(), 0.0);
				}
				return result;
			}
		}

		public LearnCharacter GetByCharacter (string character)
		{
			foreach (LearnCharacter lc in learnCharacters)
				if (character == lc.Character)
					return lc;
			return null;
		}

		public int Count 
		{
			get { return learnCharacters.Count; }
		}

		public LearnState(string learnOrder)
		{
			this.learnOrder = learnOrder;
		
			AddCharacter();
			AddCharacter();
			AddCharacter();
			AddCharacter();
		}

		public LearnState() : this (MorseConstant.DefaultLearnOrder)
		{
		}

		public LearnState(XmlElement element) 
		{
			if (element.HasAttribute ("LearnOrder"))
				learnOrder = element.GetAttribute ("LearnOrder");
			
			try
			{
				overalError = double.Parse (element.GetAttribute ("OveralError"));
			}
			catch (Exception)
			{
				overalError = DefaultOverallError;
			}
			
			try
			{
				wordLevel = double.Parse (element.GetAttribute ("WordLevel"));
			}
			catch (Exception)
			{
				wordLevel = DefaultWordLevel;
			}

			foreach (XmlElement e0 in element.SelectNodes ("LearnCharacter"))
			{
				LearnCharacter lc = new LearnCharacter (e0.GetAttribute ("Character"));
				lc.Error = double.Parse (e0.GetAttribute ("Error"));
				learnCharacters.Add (lc);
			}
		}

		public void WriteToXML (XmlElement element)
		{
			if (learnOrder != MorseConstant.DefaultLearnOrder)
				element.SetAttribute ("LearnOrder", learnOrder);

			element.SetAttribute ("OveralError", overalError.ToString());
			element.SetAttribute ("WordLevel", wordLevel.ToString());

			foreach (LearnCharacter lc in learnCharacters)
			{
				XmlElement e0 = element.OwnerDocument.CreateElement ("LearnCharacter");
				e0.SetAttribute ("Character", lc.Character);
				e0.SetAttribute ("Error", lc.Error.ToString());
				element.AppendChild (e0);
			}
		}

		public bool AddCharacter (string character)
		{
			if ((GetByCharacter (character) == null) && (MorseConstant.GetByCharacter (character) != null))
			{
				learnCharacters.Add (new LearnCharacter (character));
				if (overalError < DefaultOverallError)
					overalError = DefaultOverallError;
				return true;
			}
			else
				return false;
		}

		public void AddCharacter ()
		{
			foreach (char c in learnOrder.ToCharArray())
				if (AddCharacter (c.ToString()))
					return;
			return;
		}

		public void RemoveAll()
		{
			learnCharacters.Clear();
			overalError = DefaultOverallError;
			wordLevel = BeginnerWordLevel;
		}

		public void AddAll()
		{
			foreach (char c in learnOrder.ToCharArray())
				AddCharacter (c.ToString());
		}

		public void AddNumeric()
		{
			foreach (char c in MorseConstant.Numeric)
				AddCharacter (c.ToString());
		}

		public void RemoveNumeric()
		{
			foreach (char c in MorseConstant.Numeric)
				RemoveCharacter (c.ToString());;
		}

		public void AddPunctuation()
		{
			foreach (char c in MorseConstant.Punctuation)
				AddCharacter (c.ToString());
		}

		public void RemovePunctuation()
		{
			foreach (char c in MorseConstant.Punctuation)
				RemoveCharacter (c.ToString());;
		}

		public void AddAlphabetic()
		{
			foreach (char c in MorseConstant.Alphabetic)
				AddCharacter (c.ToString());
		}

		public void RemoveAlphabetic()
		{
			foreach (char c in MorseConstant.Alphabetic)
				RemoveCharacter (c.ToString());;
		}

		public void SetBeginner ()
		{
			foreach (LearnCharacter lc in learnCharacters)
				lc.Error = BeginnerLevel;
			overalError = DefaultOverallError;
			wordLevel = BeginnerWordLevel;
		}

		public void SetIntermediate ()
		{
			foreach (LearnCharacter lc in learnCharacters)
				lc.Error = IntermediateLevel;
			overalError = DefaultOverallError;
			wordLevel = IntermediateWordLevel;
		}

		public void SetAdvanced ()
		{
			foreach (LearnCharacter lc in learnCharacters)
				lc.Error = AdvancedLevel;
			wordLevel = AdvancedWordLevel;
		}

		public LearnCharacter SelectRandomCharacter(Random random)
		{
			double total = 0;
			foreach (LearnCharacter lc in learnCharacters)
				total += lc.Error;

			double r = random.NextDouble() * total;
			foreach (LearnCharacter lc in learnCharacters) 
			{
				r -= lc.Error;
				if (r < 0)
					return lc;
			}
			return null;
		}


		public void RemoveCharacter (LearnCharacter lc)
		{
			learnCharacters.Remove (lc);
			if (overalError < DefaultOverallError)
				overalError = DefaultOverallError;
		}
			

		public void RemoveCharacter (string character)
		{
			LearnCharacter lc = GetByCharacter (character);
			if (lc != null)
				RemoveCharacter (lc);
		}
			

		public LearnCharacter SelectRandomCharacter()
		{
			return SelectRandomCharacter (new Random ());
		}


		/* Adjust old value by a weighted average with a new value. */
		private double Weigh (double oldValue, double newValue)
		{
			const double weighPercent = 0.125;
			return (1.0 - weighPercent) * oldValue + (weighPercent * newValue);
		}

	    /* Adjust individual and overall error rate estimations.  If both are
        suficiently low, increase the learning characters  */
		public void Grade (LearnCharacter lc, bool correct, bool addChar)
		{
			double weighValue = correct ? 0.0 : 1.0;

			overalError = Weigh (overalError, weighValue);
			lc.Error = Weigh (lc.Error, weighValue);

			if (overalError > 0.30)
				return;

			if (!addChar)
				return;

			if (overalError < 0.10)
				lc.Error = Weigh (lc.Error, weighValue); // second time
	
			foreach (LearnCharacter lc0 in learnCharacters)
				if (lc0.Error > 0.40)
					return;

			AddCharacter();
		}


		public void GradeWord (bool correct)
		{
			if (!correct)
				wordLevel = Math.Max (MinWordLevel, wordLevel - WordLevelDecrement);
			else
				wordLevel = Math.Min (MaxWordLevel, wordLevel + WordLevelIncrement);
		}


		public int RandomWordSize ()
		{
			double deviation = 1 + wordLevel / 20;

			double result = wordLevel;
			Random random = new Random ();
			for (int i = 0; i < 10; i++)
				result += (random.NextDouble () * deviation * 2) - deviation;
			
			result = 
				Math.Max (result, MinWordLevel);
			result = 
				Math.Min (result, MaxWordLevel);
			
			return (int) result;
		}

        public static string FileName
        {
            get 
            {
                return
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                    + "\\" + LearnStateFilename;
            }
        }

        private static LearnState instance;
		public static LearnState GetInstance()
		{
			if (instance == null)
			{
				if (File.Exists (FileName))
				{
					XmlDocument doc = new XmlDocument();
                    doc.Load(FileName);
					instance = new LearnState (doc.DocumentElement);
				}
				else
					instance = new LearnState();
			}
			return instance;
		}


		public void SaveToFile()
		{
			XmlDocument doc = new XmlDocument();
			doc.AppendChild (doc.CreateElement ("LearnState"));
			WriteToXML (doc.DocumentElement);
            doc.Save(FileName);
		}
	
	}
}
