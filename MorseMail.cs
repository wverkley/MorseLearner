using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wxv.MorseLearner
{
	public abstract class MorseMail
	{
		public const string MorseMailBeginTag = "<MorseMail>";
		public const string MorseMailEndTag = "</MorseMail>";
		public const int MorseMailIntervalsPerLine = 10;

		
		public static bool IsMorseMail (string text)
		{
			string body = StringUtil.ExtractBetweenTag (text, MorseMailBeginTag, MorseMailEndTag);
			return (body != null);
		}


		public static string Export (MorseDocument doc)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append (MorseMailBeginTag);
			if (doc.StartTime != DateTime.MinValue)
			{
				sb.Append (doc.StartTime.ToString ("yyyy.MM.dd"));
				if (doc.StartTime.TimeOfDay.Ticks != 0)
					sb.Append (doc.StartTime.ToString (" HH:mm:ss"));
			}
			int count = 0;
			for (int i = 0; i < doc.Count; i++)
			{
				int interval = doc.GetInterval (i);
				if ((interval > 0) || ((i != 0) && (i != (doc.Count - 1)))) // ignore beginning and ending pauses
				{
					if ((count % MorseMailIntervalsPerLine) == 0)
						sb.Append ("\r\n");
					sb.Append ((interval >= 0) ? "+" : "-");
					sb.Append (Math.Abs (interval));
					count++;
				}
			}
			sb.Append ("\r\n" + MorseMailEndTag);

			return sb.ToString();
		}


		private static int Import_FindSeperator (string body, int startIndex)
		{
			int i0 = body.IndexOf ("+", startIndex);
			int i1 = body.IndexOf ("-", startIndex);
			if ((i0 == -1) && (i1 == -1))
				return -1;
			else if (i1 == -1) 
				return i0;
			else if (i0 == -1)
				return i1;
			else if (i0 < i1)
				return i0;
			else
				return i1;
		}


		private static void Import_Interval (string body, ref int index, out bool state, out int interval)
		{
			int nextIndex = Import_FindSeperator (body, index + 1); // look for the next seperator
			if (nextIndex == -1)
				nextIndex = body.Length;

			string intervalStr = body.Substring (index, nextIndex - index).Trim();
			index = nextIndex;

			if (intervalStr.Length < 1)
				throw new Exception ("Invalid format");

			if (intervalStr.Substring (0, 1) == "+")
				state = true;
			else if (intervalStr.Substring (0, 1) == "-")
				state = false;
			else
				throw new Exception ("Invalid format");

			try
			{
				interval = int.Parse (intervalStr.Substring (1, intervalStr.Length - 1));
				if (interval <= 0)
					throw new Exception("Interval cannot be less then zero");
			}
			catch (Exception ex)
			{
				throw new Exception ("Invalid format", ex);
			}
		}


		public static void Import (MorseDocument doc, string text)
		{
			doc.Clear();
			string body = StringUtil.ExtractBetweenTag (text, MorseMailBeginTag, MorseMailEndTag);
			if (body == null)
				throw new Exception ("Invalid Format");

			int index = Import_FindSeperator (body, 0);
			if (index == -1)
				throw new Exception ("Invalid Format");

			try
			{
				doc.StartTime = DateTime.Parse (body.Substring (0, index - 1));
			}
			catch (Exception)
			{
			}
			while (index < body.Length)
			{
				bool state;
				int interval;
				Import_Interval (body, ref index, out state, out interval);

				doc.AddInterval (interval, state);
			}
		}
	}

}
