using System;
using System.IO;
using System.Collections;
using System.Text;

namespace Wxv.MorseLearner
{
	public class Vocabulary
	{
		public const int MinRandomPool = 10;

		private SortedList words = new SortedList();

		public Vocabulary(Stream stream)
		{
			LoadFromStream (stream);
		}

		public Vocabulary(string filename)
		{
			LoadFromFile (filename);
		}
		
		public Vocabulary()
		{
		}

		private bool IsValidWord (string word)
		{
			foreach (char c in word.Trim().ToCharArray())
				if ((c != ' ') && (MorseConstant.DefaultLearnOrder.IndexOf (c.ToString()) == -1))
					return false;

			return word.Trim().Length > 0;
		}

		private string CleanWord (string word)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in word.Trim().ToCharArray())
				if ((c == ' ') || (MorseConstant.DefaultLearnOrder.IndexOf (c.ToString()) != -1))
					sb.Append (c);
			return sb.ToString();
		}

		private void AddWord (string word)
		{
            if (!IsValidWord (word))
				return; 

			word = CleanWord (word);
			if ((word.Length > 0) && !words.ContainsKey (word))
				words.Add (word, null);
		}

		public void LoadFromStream (Stream stream)
		{
			words.Clear();
			string word;
			try
			{
				using (StreamReader reader = new StreamReader (stream))
					while ((word = reader.ReadLine()) != null)
						AddWord (word);
			}
			catch (Exception) {}
		}

		public void LoadFromFile (string filename)
		{
			using (FileStream stream = new FileStream (filename, FileMode.Open))
				LoadFromStream (stream);
		}

		public void SaveToStream (Stream stream)
		{
			using (StreamWriter writer = new StreamWriter (stream))
				foreach (string word in words.Keys)
					writer.WriteLine (word);
		}

		public void SaveToFile (string filename)
		{
			using (FileStream stream = new FileStream (filename, FileMode.Create))
				SaveToStream (stream);
		}

		private bool IsSubsetOf (string word, IDictionary characters)
		{
			foreach (char c in word.ToCharArray())
				if (!characters.Contains (c.ToString()))
					return false;
			return true;
		}

		public string PickRandomWord (IDictionary characters, int maxSize)
		{
			ArrayList chanceWords = new ArrayList();

			IDictionary characterCount = new Hashtable ();
			foreach (string character in characters.Keys)
				characterCount.Add (character, 0);
			int totalCharacterCount = 0;

			foreach (string word in words.Keys)
				if ((word.Length <= maxSize) && IsSubsetOf (word, characters))
				{
					chanceWords.Add (word);
					foreach (string character in characters.Keys)
						if (word.IndexOf (character) != -1)
						{
							characterCount [character] = (int) characterCount [character] + 1;
							totalCharacterCount += 1;
						}
				}
			if (chanceWords.Count < MinRandomPool)
				return null;

			ArrayList chances = new ArrayList();
			double totalChance = 0.0;
			foreach (string word in chanceWords)
			{
				double chance = 0;
				foreach (string character in characters.Keys)
					if (word.IndexOf (character) != -1)
						chance += (double) characters [character] / ((int) characterCount [character] / (double) totalCharacterCount);

				chances.Add (chance);
				totalChance += chance;
			}

			
			double r = new Random().NextDouble() * totalChance;
			for (int i = 0; i < chanceWords.Count; i++)
			{
				r -= (double) chances[i];
				if (r < 0)
					return (string) chanceWords[i];
			}
			return null;
		}

	}
}
