using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wxv.MorseLearner
{
	public class MorseDocumentParser
	{
		public const int DefaultWpm = 5;
		public const int DefaultOverallWpm = 5;

		public enum ParseResultType {Character, InterChar, InterWord}

		public class ParseResult
		{
			public ParseResultType Type;
			public string Character;
			public string Morse;
			public bool Complete;
			public int[] Intervals;
			public int TotalInterval;
			public int Wpm;
			
			public bool IsWhitespace
			{
				get { return (Type == ParseResultType.InterChar) || (Type == ParseResultType.InterWord); }
			}
		}


		private MorseDocument doc;
		private int parseIndex;
		private int lastEstimatedWpm;

		
		public MorseDocumentParser(MorseDocument doc)
		{
			this.doc = doc;
			Clear();
		}


		public void Clear()
		{
			parseIndex = 0;
		}


		public string GetAsParseReport ()
		{
			return GetAsParseReport (0); 
		}

		
		public string GetAsParseReport (int wpm)
		{
			return GetAsParseReport (wpm); 
		}


		public string AsParseReport 
		{
			get { return GetAsParseReport(); }
		}


		public void ParseReset()
		{
			parseIndex = 0;
			lastEstimatedWpm = 0;
		}


		public ParseResult Parse (int wpm, bool live)
		{
			// nothing to parse
			if (parseIndex >= doc.Count)
				return null;

			if (wpm == 0)
				wpm = CalcEstimatedWpm();

			ParseResult result = new ParseResult();
			result.Complete = false;
			result.Morse = string.Empty;
			result.Character = null;

			if (wpm == 0)
				return result;

			int index = parseIndex;

			// check for word or char seperators
			while ((index < doc.Count) && (doc.GetInterval (index) < 0))
			{
				int interval = doc.GetInterval (index);
				if (Math.Abs (interval) > MorseConstant.InterWordLengthMin (wpm))
				{
					result.Complete = true;
					result.Character = MorseConstant.WordSeperatorStr;
					result.Type = ParseResultType.InterWord;
					result.TotalInterval = doc.GetIntervals (parseIndex, index, out result.Intervals);
					result.Wpm = wpm;

					parseIndex = index + 1;
					return result;
				}
				else if (Math.Abs (interval) > MorseConstant.InterCharLengthMin (wpm))
				{
					result.Complete = true;
					result.Character = MorseConstant.CharSeperatorStr;
					result.Type = ParseResultType.InterChar;
					result.TotalInterval = doc.GetIntervals (parseIndex, index, out result.Intervals);
					result.Wpm = wpm;

					parseIndex = index + 1;
					return result;
				}
				else
				{
					// ignore shorter inter character pauses, but we shouldnt 
					// get any if parsing is working properly
				}

				index++;
			}
			
			// loop through and check for characters
			if (index < doc.Count)
			{
				while (index < doc.Count)
				{
					int interval = doc.GetInterval (index);
				
					if (interval > 0)
					{
						if (interval < MorseConstant.DahLengthMin (wpm))
							result.Morse += MorseConstant.DitStr;
						else
							result.Morse += MorseConstant.DahStr;
					}
					else
					{
						if (Math.Abs (interval) > MorseConstant.InterCharLengthMin(wpm))
						{
							MorseConstant mc = MorseConstant.GetByMorse (result.Morse);
						
							result.Complete = true;
							if (mc != null)
								result.Character = mc.Character;
							result.Type = ParseResultType.Character;
							result.TotalInterval = doc.GetIntervals (parseIndex, index - 1, out result.Intervals);
							result.Wpm = wpm;

							parseIndex = index;
							return result;
						}
						else
						{
							// inter character pauses, continue parsing
						}
					}

					index++;
				}

				// if not live (e.g. if live the remaining stuff might be uncompleted), 
				// parse any remaining morse code
				if (!live)
				{
					result.Complete = true;

					MorseConstant mc = MorseConstant.GetByMorse (result.Morse);
					if (mc != null)
						result.Character = mc.Character;
					result.Type = ParseResultType.Character;
					result.TotalInterval = doc.GetIntervals (parseIndex, index - 1, out result.Intervals);
					result.Wpm = wpm;

					parseIndex = index;
					return result;
				}
			}

			return result;
		}


		public string Parse(int wpm)
		{
			StringBuilder sb = new StringBuilder();
			
			ParseResult mpr = Parse (wpm, false);
			while ((mpr != null) && mpr.Complete)
			{
				if (mpr.Character != null)
					sb.Append (mpr.Character);
				else
					sb.Append ("?");

				mpr = Parse (wpm, false);
			}
			
			return sb.ToString();
		}


		private int CalcEstimatedWpm ()
		{
			if ((parseIndex < doc.Count) && (lastEstimatedWpm != 0))
			{
				int interval = doc.GetInterval (parseIndex);
				if (-interval > MorseConstant.InterMessageLength (lastEstimatedWpm))
					lastEstimatedWpm = 0;
			}

			int minOn = -1;
			int maxOn = -1;
			int minOff = -1;
			int maxOff = -1;
			int min = -1;
			int max = -1;
			
			int index = parseIndex;
			while (index < doc.Count)
			{
				int interval = doc.GetInterval (index);
				bool state = interval > 0;
				interval = Math.Abs (interval);

				if ((interval < min) || (min == -1))
					min = interval;
				if ((interval > max) || (max == -1))
					max = interval;
				if (state)
				{
					if ((interval < minOn) || (minOn == -1))
						minOn = interval;
					if ((interval > maxOn) || (maxOn == -1))
						maxOn = interval;
				}
				else
				{
					if ((interval < minOff) || (minOff == -1))
						minOff = interval;
					if ((interval > maxOff) || (maxOff == -1))
						maxOff = interval;
				}

				if ((minOn != -1) 
					&& (minOff != -1) 
					&& (maxOff != -1) 
					&& (maxOff != -1) 
					&& (maxOff > (minOn * 2)) 
					&& (maxOn > (minOff * 2))
					&& (maxOn > (minOn * 2)))
				{
					int total = 0;
					int count = 0;

					total = minOn * 9;
					count += 9;
					
					total += minOff * 3;
					count += 3;

					if (lastEstimatedWpm != 0)
					{
						total += MorseConstant.DitLength (lastEstimatedWpm) * 12;
						count += 12;
					}
					
					lastEstimatedWpm = MorseConstant.LengthToWpm (total / count);
					return lastEstimatedWpm;
				}

				index++;
			}

			if ((lastEstimatedWpm != 0) 
				&& (min != -1) 
				&& (max != -1) 
				&& (max > (min * 2)))
			{
				int total = 0;
				int count = 0;

				total = min * 3;
				count += 3;

				total += MorseConstant.DitLength (lastEstimatedWpm) * 12;
				count += 12;
					
				lastEstimatedWpm = MorseConstant.LengthToWpm (total / count);
				return lastEstimatedWpm;
			}

			return 0;
		}


		public string Parse()
		{
			return Parse (0);
		}


		public string ParseReport (int wpm)
		{
			ParseReset();
			
			StringBuilder sb = new StringBuilder();
			
			ParseResult mpr = Parse (wpm, false);
			while ((mpr != null) && mpr.Complete)
			{
				if (mpr.Character != null)
					sb.Append ("'" + mpr.Character + "'");
				else
					//					sb.Append ("(" + mpr.Morse + ")");
					sb.Append (" ? ");

				sb.Append ("\t");
				sb.Append (mpr.Wpm);

				sb.Append ("\t");
				sb.Append (MorseConstant.DitLength (mpr.Wpm));

				sb.Append ("\t");
				sb.Append (mpr.Morse);

				sb.Append ("\t");
				sb.Append (CsvUtil.Format (mpr.Intervals));
				
				sb.Append ("\r\n");

				mpr = Parse (wpm, false);
			}
			
			return sb.ToString();
		}

	}

}
