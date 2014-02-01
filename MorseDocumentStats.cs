using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wxv.MorseLearner
{
	/* Miscellanious routines to calculate stats for a Morse Document */
	public class MorseDocumentStats
	{
		public class IntervalFrequency
		{
			public int Length;
			public int Frequency;

			public string AsCsv
			{
				get { return CsvUtil.Format (new object[] {Length, Frequency}); }
				set 
				{ 
					string[] values = CsvUtil.Extract (value);
					if (values.Length != 2)
						throw new Exception ("Invalid Format");
					Length = int.Parse (values [0]);
					Frequency = int.Parse (values [1]);
				}
			}
		}

		public static IntervalFrequency[] ExportFrequencyStats (MorseDocument doc, int division)
		{
			SortedList sl = new SortedList();
			
			for (int i = 0; i < doc.Count; i++)
			{
				int interval = doc.GetInterval (i);

				int length = interval - interval % division;

				IntervalFrequency mif = (IntervalFrequency) sl [length];
				if (mif != null)
					mif.Frequency++;
				else
				{
					mif = new IntervalFrequency();
					mif.Length = length;
					mif.Frequency = 1;
					sl.Add (length, mif);
				}
			}

			IntervalFrequency[] result = new IntervalFrequency [sl.Count];
			sl.Values.CopyTo (result, 0);
			return result;
		}


		public static string ExportFrequencyStatsCsv (MorseDocument doc, int division)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append ("Length,Frequency\r\n");

			foreach (IntervalFrequency mif in ExportFrequencyStats (doc, division))
				sb.Append (mif.AsCsv + "\r\n");

			return sb.ToString();
		}


		public const int MinOverallWpm = 2;
		public const int MaxOverallWpm = 50;

		
		/*
		 * Calculates the combined weight of the "on" states around the dit length
		 * and dah length values for a specific wpm.  Returns false if
		 * no dits or dahs could be found for the wpm.  The parser must already contain 
		 * dits and dahs to work with (the more consistent values, the better)
		 */
		private static bool CalcWpmWeighting (MorseDocument doc, int wpm, out double weight)
		{
			weight = 0;

			double ditLength = MorseConstant.DitLength (wpm);
			double dahLength = ditLength * 3;
			double ditDahMiddle = ditLength * 2;

			int ditCount = 0;
			double ditTotal = 0;
			int dahCount = 0;
			double dahTotal = 0;

			for (int i = 0; i < doc.Count; i++)
			{
				int interval = doc.GetInterval (i);
				if (interval > 0)
				{
					if (interval < ditDahMiddle)
					{
						ditCount++;
						ditTotal += interval;
					}
					else
					{
						dahCount++;
						dahTotal += interval;
					}
				}
			}

			if ((ditCount == 0) || (dahCount == 0))
				return false;

			double ditAvg = ditTotal / ditCount;
			double dahAvg = dahTotal / dahCount;

			double ditWeight = (ditAvg - ditLength) / ditLength;
			double dahWeight = (dahAvg - dahLength) / dahLength;

			weight = Math.Abs (ditWeight + dahWeight);

			return true;
		}

		/*
		 * Finds the Wpm value with the best (lowest) weighting which is our
		 * best guess estimate of the wpm of this message.  Returns zero if it 
		 * could not be calculated.
		 */
		private int CalcOverallWpm (MorseDocument doc)
		{
			double bestWeight = 0;
			int bestWpm = 0;

			bool found = false;
			for (int wpm = MinOverallWpm; wpm <= MaxOverallWpm; wpm++)
			{
				double weight;
				if (CalcWpmWeighting (doc, wpm, out weight))
					if (!found || (weight < bestWeight))
					{
						bestWpm = wpm;
						bestWeight = weight;
						found = true;
					}
			}

			return bestWpm;
		}


	}

}
