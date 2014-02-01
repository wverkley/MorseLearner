using System;
using System.Collections;

namespace Wxv.MorseLearner
{
	/// <summary>
	/// A CSV String Formatting Utility
	/// 
	/// Example use :
	/// Console.WriteLine (CsvFormat.Format (myStr));
	/// or
	/// Console.WriteLine (CsvFormat.Format (myStrCollection));
	/// 
	/// </summary>
	public class CsvUtil
	{
		// The grunt work method to convert a string to csv
		public static string Format (string str) 
		{
			string result = str.Replace ("\"", "\"\"");
			if ((result.IndexOf (",") != -1) || (result.IndexOf ("\n") != -1))
				result = "\"" + result + "\"";
			return result;
		}

		// Convert a collection to a CSV string list
		public static string Format (ICollection collection) 
		{
			string result = "";
			bool first = true;
			foreach (object value in collection) 
			{
				if (first)
				{
					result = Format (value.ToString());
					first = false;
				}
				else
					result += "," + Format (value.ToString());
			}
			return result;
		}

		public static string[] Extract (string str) 
		{
			int pos = 0;
			string field = "";
			bool inField = false;
			bool inQuotedField = false;
			bool waitingForComma = false;

			ArrayList fields = new ArrayList ();

			while (pos < str.Length) 
			{
				if (str [pos] == '"') 
				{
					// found a double-quote
					if (!inField) 
					{
						// field starts here
						inField = true;
						inQuotedField = true;
					}
					else
					{
						// we're in a field
						// is this a double-quote delimiter?
						if ((pos < (str.Length - 1)) &&  (str [pos + 1] == '"')) 
						{
							// this is a double quote delimiter
							field = field + str [pos];
							// skip over the next double quote
							pos++;
						}
						else 
						{
							if (!inQuotedField) 
							{
								throw new Exception ("invalid CSV format");
							}
							else
							{
								// this is the end of field
								inField = false;
								inQuotedField = false;
								fields.Add (field);
								field = "";
								waitingForComma = true;
							}
						}
					}
				}
				else if (str [pos] == ',') 
				{
					if (inQuotedField) 
					{
						field = field + str[pos];
					}
					else 
					{
						// not in a quoted field, so this must be a field delimiter
						if (!waitingForComma) 
						{
							fields.Add (field);
							field = "";
							inField = false;
						}
						else 
						{
							waitingForComma = false;
						}
					}
				}
				else 
				{
					inField = true;
					field = field + str [pos];						
				}

				pos++;
			}

			if (inField || (str [str.Length - 1] == ',')) 
			{
				// fallen off the end, or the last field is blank
				fields.Add(field);
			}

			return (string[]) fields.ToArray (typeof (string));
		
		}

	}
}
