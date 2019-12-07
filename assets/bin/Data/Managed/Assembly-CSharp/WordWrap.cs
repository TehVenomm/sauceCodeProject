public static class WordWrap
{
	private static readonly string SuppressionHead = "!%),.:;?]}¢°’”‰′″℃、。\u3005〉》」』】〕ぁぃぅぇぉっゃゅょゎ\u309b\u309c\u309d\u309eァィゥェォッャュョヮヵヶ・\u30fc\u30fd\u30fe！％），．：；？］｝｡｣､･ｧｨｩｪｫｬｭｮｯ\uff70\uff9e\uff9f￠！？";

	private static readonly string SuppressionTail = "$([\\{£¥‘“〈《「『【〔＄（［｛｢￡￥";

	public static string Convert(UILabel label, string orgText)
	{
		string final = "";
		if (!label.Wrap(orgText, out final))
		{
			final = label.text;
		}
		if (final.Equals(orgText))
		{
			return orgText;
		}
		string text = orgText.Replace("\r\n", "\n");
		bool flag = true;
		while (flag)
		{
			flag = false;
			string text2 = "";
			string[] array = text.Split('\n');
			for (int i = 0; i < array.Length; i++)
			{
				if (0 < i)
				{
					text2 += "\n";
				}
				string orgText2 = array[i];
				string str = ConvertWrap(label, orgText2);
				text2 += str;
			}
			if (!text2.Equals(text))
			{
				flag = true;
				text = text2;
			}
		}
		return text;
	}

	private static string ConvertWrap(UILabel label, string orgText)
	{
		string final = "";
		if (!label.Wrap(orgText, out final))
		{
			final = orgText;
		}
		if (orgText.Equals(final))
		{
			return final;
		}
		string text = "";
		final = final.Replace("\n", "\n ");
		string[] array = final.Split('\n');
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = array[i];
			if (text2.Length == 0)
			{
				continue;
			}
			string value = text2[0].ToString();
			if (text.Length > 0 && SuppressionHead.IndexOf(value) >= 0)
			{
				text = text.Insert(text.Length - 1, "\n");
				for (int j = i; j < array.Length; j++)
				{
					text += array[j];
				}
				break;
			}
			string value2 = text2[text2.Length - 1].ToString();
			if (SuppressionTail.IndexOf(value2) >= 0)
			{
				text2 = text2.Insert(text2.Length - 1, "\n");
				text += text2;
				for (int k = i + 1; k < array.Length; k++)
				{
					text += array[k];
				}
				break;
			}
			text += text2;
		}
		return text;
	}
}
