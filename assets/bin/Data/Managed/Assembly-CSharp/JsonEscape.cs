using System.Text;

public static class JsonEscape
{
	public static string escape(string s)
	{
		if (s == null || s.Length == 0)
		{
			return "";
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
				continue;
			case '/':
				stringBuilder.Append('\\');
				stringBuilder.Append(c);
				continue;
			case '\b':
				stringBuilder.Append("\\b");
				continue;
			case '\t':
				stringBuilder.Append("\\t");
				continue;
			case '\n':
				stringBuilder.Append("\\n");
				continue;
			case '\f':
				stringBuilder.Append("\\f");
				continue;
			case '\r':
				stringBuilder.Append("\\r");
				continue;
			}
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
		}
		return stringBuilder.ToString();
	}

	public static string removeSpecialChars(string s)
	{
		if (s == null || s.Length == 0)
		{
			return "";
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
