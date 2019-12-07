using System.Text;

internal class TagAnalyzer
{
	private int line;

	private int column;

	public string findTag
	{
		get;
		private set;
	}

	public string findTagText
	{
		get;
		private set;
	}

	protected virtual bool IsValidTag(string tag, int line, int column)
	{
		if (!(tag == "M") && !(tag == "F"))
		{
			Log.Warning(LOG.OUTGAME, "Invalid tag : " + tag + " :: line " + (line + 1) + " : column " + column);
			return false;
		}
		return true;
	}

	public bool IsFindTag()
	{
		if (!string.IsNullOrEmpty(findTag))
		{
			return !string.IsNullOrEmpty(findTagText);
		}
		return false;
	}

	public int Analyze(string text, int _line, int _column)
	{
		findTag = string.Empty;
		findTagText = string.Empty;
		line = _line;
		column = _column;
		char num = text[column];
		int length = text.Length;
		int result = column;
		if (num == '<' && !IsFindTag())
		{
			char c = '\0';
			int num2 = column + 1;
			StringBuilder stringBuilder = new StringBuilder();
			while (c != '>' && num2 < length)
			{
				c = text[num2++];
				if (c != '>')
				{
					stringBuilder.Append(c);
				}
			}
			if (IsValidTag(stringBuilder.ToString(), line, column))
			{
				findTag = stringBuilder.ToString();
				StringBuilder stringBuilder2 = new StringBuilder();
				StringBuilder stringBuilder3 = new StringBuilder();
				bool flag = false;
				while (!flag && num2 < length)
				{
					c = text[num2++];
					if (c == '<')
					{
						int num3 = num2;
						if (num3 >= length || text[num3++] != '/')
						{
							continue;
						}
						char c2 = '\0';
						while (c2 != '>' && num3 < length)
						{
							c2 = text[num3++];
							if (c2 != '>')
							{
								stringBuilder3.Append(c2);
							}
						}
						if (stringBuilder3.ToString() == findTag)
						{
							flag = true;
							num2 = num3;
						}
						else
						{
							Log.Error(LOG.OUTGAME, "not match END_TAG : start = " + findTag + " : end = " + stringBuilder3.ToString() + " :: line " + (line + 1) + " : column " + column);
						}
					}
					else
					{
						stringBuilder2.Append(c);
					}
				}
				if (flag)
				{
					findTagText = stringBuilder2.ToString();
					result = num2;
				}
				else if (num2 >= length)
				{
					Log.Error(LOG.OUTGAME, "did not end Tag Analyze till the end of line : tag = " + findTag + " : text = " + findTagText + " :: line " + (line + 1));
					findTag = string.Empty;
					findTagText = string.Empty;
				}
			}
		}
		return result;
	}
}
