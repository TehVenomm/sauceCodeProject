using System.Text;

public class EscapeString
{
	private static string[] conversions = new string[133]
	{
		"%20",
		"%22",
		"%27",
		"%3C",
		"%3E",
		"%23",
		"%25",
		"%7B",
		"%7D",
		"%7C",
		"%5E",
		"%7E",
		"%5B",
		"%5D",
		"%24",
		"%26",
		"%2B",
		"%2C",
		"%2F",
		"%3A",
		"%3B",
		"%3D",
		"%3F",
		"%40",
		"%5F",
		"%0A",
		"%2E",
		"%09",
		"%C2%A1",
		"%C2%A2",
		"%C2%A3",
		"%C2%A4",
		"%C2%A5",
		"%C2%A6",
		"%C2%A7",
		"%C2%A8",
		"%C2%A9",
		"%C2%AA",
		"%C2%AB",
		"%C2%AC",
		"%C2%AD",
		"%C2%AE",
		"%C2%AF",
		"%C2%B0",
		"%C2%B1",
		"%C2%B2",
		"%C2%B3",
		"%C2%B4",
		"%C2%B5",
		"%C2%B6",
		"%C2%B7",
		"%C2%B8",
		"%C2%B9",
		"%C2%BA",
		"%C2%BB",
		"%C2%BC",
		"%C2%BD",
		"%C2%BE",
		"%C2%BF",
		"%C3%80",
		"%C3%81",
		"%C3%82",
		"%C3%83",
		"%C3%84",
		"%C3%85",
		"%C3%86",
		"%C3%87",
		"%C3%88",
		"%C3%89",
		"%C3%8A",
		"%C3%8B",
		"%C3%8C",
		"%C3%8D",
		"%C3%8E",
		"%C3%8F",
		"%C3%90",
		"%C3%91",
		"%C3%92",
		"%C3%93",
		"%C3%94",
		"%C3%95",
		"%C3%96",
		"%C3%97",
		"%C3%98",
		"%C3%99",
		"%C3%9A",
		"%C3%9B",
		"%C3%9C",
		"%C3%9D",
		"%C3%9E",
		"%C3%9F",
		"%C3%A0",
		"%C3%A1",
		"%C3%A2",
		"%C3%A3",
		"%C3%A4",
		"%C3%A5",
		"%C3%A6",
		"%C3%A7",
		"%C3%A8",
		"%C3%A9",
		"%C3%AA",
		"%C3%AB",
		"%C3%AC",
		"%C3%AD",
		"%C3%AE",
		"%C3%AF",
		"%C3%B0",
		"%C3%B1",
		"%C3%B2",
		"%C3%B3",
		"%C3%B4",
		"%C3%B5",
		"%C3%B6",
		"%C3%B7",
		"%C3%B8",
		"%C3%B9",
		"%C3%BA",
		"%C3%BB",
		"%C3%BC",
		"%C3%BD",
		"%C3%BE",
		"%C3%BF",
		"%2D",
		"%2A",
		"%E2%82%AC",
		"%60",
		"%21",
		"%28",
		"%29",
		"%5C",
		"%E2%80%99",
		"%E2%80%A6"
	};

	private static char[] symbols = new char[133]
	{
		' ',
		'"',
		'\'',
		'<',
		'>',
		'#',
		'%',
		'{',
		'}',
		'|',
		'^',
		'~',
		'[',
		']',
		'$',
		'&',
		'+',
		',',
		'/',
		':',
		';',
		'=',
		'?',
		'@',
		'_',
		'\n',
		'.',
		' ',
		'¡',
		'¢',
		'£',
		'¤',
		'¥',
		'¦',
		'§',
		'\u00a8',
		'©',
		'ª',
		'«',
		'¬',
		'­',
		'®',
		'\u00af',
		'°',
		'±',
		'²',
		'³',
		'\u00b4',
		'µ',
		'¶',
		'·',
		'\u00b8',
		'¹',
		'º',
		'»',
		'¼',
		'½',
		'¾',
		'¿',
		'À',
		'Á',
		'Â',
		'Ã',
		'Ä',
		'Å',
		'Æ',
		'Ç',
		'È',
		'É',
		'Ê',
		'Ë',
		'Ì',
		'Í',
		'Î',
		'Ï',
		'Ð',
		'Ñ',
		'Ò',
		'Ó',
		'Ô',
		'Õ',
		'Ö',
		'×',
		'Ø',
		'Ù',
		'Ú',
		'Û',
		'Ü',
		'Ý',
		'Þ',
		'ß',
		'à',
		'á',
		'â',
		'ã',
		'ä',
		'å',
		'æ',
		'ç',
		'è',
		'é',
		'ê',
		'ë',
		'ì',
		'í',
		'î',
		'ï',
		'ð',
		'ñ',
		'ò',
		'ó',
		'ô',
		'õ',
		'ö',
		'÷',
		'ø',
		'ù',
		'ú',
		'û',
		'ü',
		'ý',
		'þ',
		'ÿ',
		'-',
		'*',
		'€',
		'`',
		'!',
		'(',
		')',
		'\\',
		'\'',
		'…'
	};

	public static string Escape(string input)
	{
		StringBuilder stringBuilder = new StringBuilder("");
		for (int i = 0; i < input.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < conversions.Length; j++)
			{
				if (input[i] == symbols[j])
				{
					stringBuilder.Append(conversions[j]);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				stringBuilder.Append(input[i]);
			}
		}
		return stringBuilder.ToString();
	}

	public static string Unescape(string input)
	{
		StringBuilder stringBuilder = new StringBuilder("");
		char[] array = input.ToCharArray();
		int num = 0;
		while (num < array.Length)
		{
			if (array[num] == '%')
			{
				StringBuilder stringBuilder2 = new StringBuilder("");
				stringBuilder2.Append(array[num++]);
				stringBuilder2.Append(array[num++]);
				stringBuilder2.Append(array[num++]);
				string text = stringBuilder2.ToString();
				switch (text)
				{
				case "%C2":
				case "%C3":
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					text = stringBuilder2.ToString();
					break;
				case "%E2":
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					stringBuilder2.Append(array[num++]);
					text = stringBuilder2.ToString();
					break;
				}
				bool flag = false;
				for (int i = 0; i < conversions.Length; i++)
				{
					if (conversions[i] == text)
					{
						stringBuilder.Append(symbols[i]);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					stringBuilder.Append(text);
				}
			}
			else
			{
				stringBuilder.Append(array[num++]);
			}
		}
		return stringBuilder.ToString();
	}
}
