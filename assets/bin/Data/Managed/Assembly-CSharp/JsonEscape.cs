using System.Text;

public static class JsonEscape
{
	public static string escape(string s)
	{
		if (s == null || s.Length == 0)
		{
			return string.Empty;
		}
		char c = '\0';
		int length = s.Length;
		StringBuilder stringBuilder = new StringBuilder(length + 4);
		for (int i = 0; i < length; i++)
		{
			c = s[i];
			switch (c)
			{
			case '"':
			case '\\':
				stringBuilder.Append('\\');
				stringBuilder.Append(c);
				break;
			case '/':
				stringBuilder.Append('\\');
				stringBuilder.Append(c);
				break;
			case '\b':
				stringBuilder.Append("\\b");
				break;
			case '\t':
				stringBuilder.Append("\\t");
				break;
			case '\n':
				stringBuilder.Append("\\n");
				break;
			case '\f':
				stringBuilder.Append("\\f");
				break;
			case '\r':
				stringBuilder.Append("\\r");
				break;
			default:
				if (c > '\u007f')
				{
					int num = c;
					string value = "\\u" + num.ToString("x4");
					stringBuilder.Append(value);
				}
				else
				{
					stringBuilder.Append(c);
				}
				break;
			}
		}
		return stringBuilder.ToString();
	}

	public static string removeSpecialChars(string s)
	{
		if (s == null || s.Length == 0)
		{
			return string.Empty;
		}
		char c = '\0';
		int length = s.Length;
		StringBuilder stringBuilder = new StringBuilder(length);
		for (int i = 0; i < length; i++)
		{
			c = s[i];
			switch (c)
			{
			case '\b':
			case '\t':
			case '\n':
			case '\f':
			case '\r':
				stringBuilder.Append(" ");
				break;
			default:
				stringBuilder.Append(c);
				break;
			case '"':
			case '\'':
			case '\\':
				break;
			}
		}
		return stringBuilder.ToString();
	}
}
