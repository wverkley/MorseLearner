using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Wxv.MorseLearner
{
	public abstract class MorseDocumentCsv
	{
		public static string Export (MorseDocument doc)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append ("State,Interval\r\n");

			for (int i = 0; i < doc.Count; i++)
			{
				int interval = doc.GetInterval (i);
				sb.Append (CsvUtil.Format (new object[] {interval > 0, Math.Abs (interval)}) + "\r\n");
			}

			return sb.ToString();
		}


		private void Import (MorseDocument doc, string text)
		{
			doc.Clear();
			
			StringReader reader = new StringReader (text);
			if (reader.ReadLine() == null) // ignore header
				return;
			
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				string[] lineArray = line.Split (',');
				if (lineArray.Length != 2)
					throw new Exception ("Invalid format");

				bool state = bool.Parse (lineArray[0]);
				int interval = int.Parse (lineArray[1]);

				doc.AddInterval (interval, state);
			}
		}

	}

}
