using System;

namespace Wxv.MorseLearner
{
	/// <summary>
	/// Holds all the morse encodings and a link to a wave file representing it
	/// </summary>
	public class MorseConstant
	{
		public const string DefaultLearnOrder = "q7zg098o1jpwlram6bxdyckn23fu45vhsite" + Punctuation;
		public const string Alphabetic = "abcdefghijklmnopqrstuvwxyz";
		public const string Numeric = "0123456789";
		public const string Punctuation = @".?,':;-/(""$+";


		public const string DitStr = ".";
		public const string DahStr = "-";
		public const char DitChar = '.';
		public const char DahChar = '-';

		public const string WordSeperatorStr = " ";
		public const string CharSeperatorStr = "";

		public static int LengthToWpm (int length)
		{
			return (60 * 1000) / (50 * length);
		}
		
		public static int DitLength (int wpm)
		{
			return (60 * 1000) / (50 * wpm);
		}

		public static int DahLength (int wpm)
		{
			return DitLength (wpm) * 3;
		}

		public static int DahLengthMin (int wpm)
		{
			return DitLength (wpm) * 2;
		}

		public static int IntraCharLength (int wpm) 
		{
			return DitLength (wpm);
		}

		public static int InterCharLength (int wpm) 
		{
			return DitLength (wpm) * 3;
		}

		public static int InterCharLength (int wpm, int overallWpm)
		{
			if (overallWpm > 0)
				return (60 * 1000 - 31 * DitLength (wpm) * overallWpm) / (19 * overallWpm) * 3;
			else
				return InterCharLength (wpm);
		}

		public static int InterCharLengthMin (int wpm) 
		{
			return DitLength (wpm) * 2;
		}

		public static int InterWordLength (int wpm)
		{
			return DitLength (wpm) * 7;
		}

		public static int InterWordLength (int wpm, int overallWpm)
		{
			if (overallWpm > 0)
				return (60 * 1000 - 31 * DitLength (wpm) * overallWpm) / (19 * overallWpm) * 7;
			else
				return InterWordLength (wpm);
		}

		public static int InterWordLengthMin (int wpm)
		{
			return DitLength (wpm) * 6;
		}

		public static int InterMessageLength (int wpm)
		{
			return DitLength (wpm) * 28;
		}

		
		public static MorseConstant[] Values = new MorseConstant[] 
		{
			new MorseConstant ("a" , ".-" , @"sounds\alphaNumeric\a.wav"),
			new MorseConstant ("b" , "-..." , @"sounds\alphaNumeric\b.wav"),
			new MorseConstant ("c" , "-.-." , @"sounds\alphaNumeric\c.wav"),
			new MorseConstant ("d" , "-.." , @"sounds\alphaNumeric\d.wav"),
			new MorseConstant ("e" , "." , @"sounds\alphaNumeric\e.wav"),
			new MorseConstant ("f" , "..-." , @"sounds\alphaNumeric\f.wav"),
			new MorseConstant ("g" , "--." , @"sounds\alphaNumeric\g.wav"),
			new MorseConstant ("h" , "...." , @"sounds\alphaNumeric\h.wav"),
			new MorseConstant ("i" , ".." , @"sounds\alphaNumeric\i.wav"),
			new MorseConstant ("j" , ".---" , @"sounds\alphaNumeric\j.wav"),
			new MorseConstant ("k" , "-.-" , @"sounds\alphaNumeric\k.wav"),
			new MorseConstant ("l" , ".-.." , @"sounds\alphaNumeric\l.wav"),
			new MorseConstant ("m" , "--" , @"sounds\alphaNumeric\m.wav"),
			new MorseConstant ("n" , "-." , @"sounds\alphaNumeric\n.wav"),
			new MorseConstant ("o" , "---" , @"sounds\alphaNumeric\o.wav"),
			new MorseConstant ("p" , ".--." , @"sounds\alphaNumeric\p.wav"),
			new MorseConstant ("q" , "--.-" , @"sounds\alphaNumeric\q.wav"),
			new MorseConstant ("r" , ".-." , @"sounds\alphaNumeric\r.wav"),
			new MorseConstant ("s" , "..." , @"sounds\alphaNumeric\s.wav"),
			new MorseConstant ("t" , "-" , @"sounds\alphaNumeric\t.wav"),
			new MorseConstant ("u" , "..-" , @"sounds\alphaNumeric\u.wav"),
			new MorseConstant ("v" , "...-" , @"sounds\alphaNumeric\v.wav"),
			new MorseConstant ("w" , ".--" , @"sounds\alphaNumeric\w.wav"),
			new MorseConstant ("x" , "-..-" , @"sounds\alphaNumeric\x.wav"),
			new MorseConstant ("y" , "-.--" , @"sounds\alphaNumeric\y.wav"),
			new MorseConstant ("z" , "--.." , @"sounds\alphaNumeric\z.wav"),
			new MorseConstant ("1" , ".----" , @"sounds\alphaNumeric\1.wav"),
			new MorseConstant ("2" , "..---" , @"sounds\alphaNumeric\2.wav"),
			new MorseConstant ("3" , "...--" , @"sounds\alphaNumeric\3.wav"),
			new MorseConstant ("4" , "....-" , @"sounds\alphaNumeric\4.wav"),
			new MorseConstant ("5" , "....." , @"sounds\alphaNumeric\5.wav"),
			new MorseConstant ("6" , "-...." , @"sounds\alphaNumeric\6.wav"),
			new MorseConstant ("7" , "--..." , @"sounds\alphaNumeric\7.wav"),
			new MorseConstant ("8" , "---.." , @"sounds\alphaNumeric\8.wav"),
			new MorseConstant ("9" , "----." , @"sounds\alphaNumeric\9.wav"),
			new MorseConstant ("0", "Ø" , "-----" , @"sounds\alphaNumeric\0.wav"),
			new MorseConstant ("." , ".-.-.-" , @"sounds\punctuation\Full Stop.wav"),
			new MorseConstant ("?" , "..--.." , @"sounds\punctuation\Question Mark.wav"),
			new MorseConstant ("," , "--..--" , @"sounds\punctuation\Comma.wav"),
			new MorseConstant ("'" , ".----." , @"sounds\punctuation\Apostrophe.wav"),
			new MorseConstant (":" , "---..." , @"sounds\punctuation\Colon.wav"),
			new MorseConstant (";" , "-.-.-." , @"sounds\punctuation\Semicolon.wav"),
			new MorseConstant ("-" , "-...-" , @"sounds\punctuation\Dash.wav"),
			new MorseConstant ("/" , "-..-." , @"sounds\punctuation\Forward Slash.wav"),
			new MorseConstant ("()[]" , "-.--.-" , @"sounds\punctuation\Parenthesis.wav"),
			new MorseConstant ("\"" , ".-..-." , @"sounds\punctuation\Quote.wav"),
			new MorseConstant ("$" , "...-..-" , @"sounds\punctuation\Dollar Sign.wav"),
			new MorseConstant ("+" , ".-.-." , null)
		};

		public readonly string AllCharacters;
		public readonly string DisplayCharacter;
		public readonly string Morse;
		public readonly string Filename;

		private MorseConstant(string allCharacters, string morse, string filename)
		{
			this.AllCharacters = allCharacters.ToLower();
			this.Morse = morse;
			this.Filename = filename;
			this.DisplayCharacter = Character;
		}

		private MorseConstant(string allCharacters, string displayCharacter, string morse, string filename)
		{
			this.AllCharacters = allCharacters.ToLower();
			this.DisplayCharacter = displayCharacter;
			this.Morse = morse;
			this.Filename = filename;
		}

		public string Character 
		{
			get { return AllCharacters.Substring (0, 1); }
		}

		public static MorseConstant GetByCharacter (string character)
		{
			character = character.ToLower();
			foreach (MorseConstant c in Values)
				if (c.AllCharacters.IndexOf (character) != -1)
					return c;
			return null;
		}

		public static MorseConstant GetByMorse (string morse)
		{
			foreach (MorseConstant c in Values)
				if (c.Morse == morse)
					return c;
			return null;
		}

		public static string ConvertToDisplay (string character)
		{
			MorseConstant mc = GetByCharacter (character);
			if (mc != null)
				return mc.DisplayCharacter;
			else
				return character;
		}
	}
}
