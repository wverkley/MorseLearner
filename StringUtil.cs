using System;

namespace Wxv.MorseLearner
{
	public abstract class StringUtil
	{
		public static bool HasTags (string content, string beginTag, string endTag, int startIndex)
		{
			int beginTagIndex = content.IndexOf (beginTag, startIndex);
			if (beginTagIndex == -1)
				return false;

			int endTagIndex = content.IndexOf (endTag, beginTagIndex + beginTag.Length);
			if (endTagIndex == -1)
				return false;

			return true;
		}

		public static bool HasTags (string content, string beginTag, string endTag)
		{
			return HasTags (content, beginTag, endTag, 0);
		}

		public static string ExtractBetweenTag (string content, string beginTag, string endTag, ref int startIndex)
		{
			int beginTagIndex = content.IndexOf (beginTag, startIndex);
			if (beginTagIndex == -1)
				return null;

			int endTagIndex = content.IndexOf (endTag, beginTagIndex + beginTag.Length);
			if (endTagIndex == -1)
				return null;

			startIndex = endTagIndex + endTag.Length;

			return content.Substring (beginTagIndex + beginTag.Length, endTagIndex - beginTagIndex - beginTag.Length);
		}

		public static string ExtractBetweenTag (string content, string beginTag, string endTag)
		{
			int startIndex = 0;
			return ExtractBetweenTag (content, beginTag, endTag, ref startIndex);
		}
		
	}
}
