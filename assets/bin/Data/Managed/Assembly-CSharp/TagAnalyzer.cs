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
		switch (tag)
		{
		default:
			Log.Warning(LOG.OUTGAME, "Invalid tag : " + tag + " :: line " + (line + 1) + " : column " + column);
			return false;
		case "M":
		case "F":
			return true;
		}
	}

	public bool IsFindTag()
	{
		return !string.IsNullOrEmpty(findTag) && !string.IsNullOrEmpty(findTagText);
	}

	public int Analyze(string text, int _line, int _column)
	{
		findTag = string.Empty;
		findTagText = string.Empty;
		line = _line;
		column = _column;
		char c = text[column];
		int length = text.Length;
		int result = column;
		if (c == '<' && !IsFindTag())
		{
			char c2 = '\0';
			int num = column + 1;
			StringBuilder stringBuilder = new StringBuilder();
			while (c2 != '>' && num < length)
			{
				c2 = text[num++];
				if (c2 != '>')
				{
					stringBuilder.Append(c2);
				}
			}
			if (IsValidTag(stringBuilder.ToString(), line, column))
			{
				findTag = stringBuilder.ToString();
				StringBuilder stringBuilder2 = new StringBuilder();
				StringBuilder stringBuilder3 = new StringBuilder();
				bool flag = false;
				while (!flag && num < length)
				{
					c2 = text[num++];
					if (c2 == '<')
					{
						int num4 = num;
						if (num4 < length && text[num4++] == '/')
						{
							char c3 = '\0';
							while (c3 != '>' && num4 < length)
							{
								c3 = text[num4++];
								if (c3 != '>')
								{
									stringBuilder3.Append(c3);
								}
							}
							if (stringBuilder3.ToString() == findTag)
							{
								flag = true;
								num = num4;
							}
							else
							{
								Log.Error(LOG.OUTGAME, "not match END_TAG : start = " + findTag + " : end = " + stringBuilder3.ToString() + " :: line " + (line + 1) + " : column " + column);
							}
						}
					}
					else
					{
						stringBuilder2.Append(c2);
					}
				}
				if (flag)
				{
					findTagText = stringBuilder2.ToString();
					result = num;
				}
				else if (num >= length)
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
