using System;
using System.Collections.Generic;

public class AccountPopupAdjuster : GameSection
{
	protected List<string> popAdjustBeforeList;

	public string PopupTextAdjust(UILabel lbl, string pop_text)
	{
		if (popAdjustBeforeList == null)
		{
			popAdjustBeforeList = new List<string>();
		}
		popAdjustBeforeList.Add(pop_text);
		char[] array = pop_text.ToCharArray();
		Array.Reverse(array);
		string text = new string(array);
		int num = lbl.CalculateOffsetToFit(text);
		string empty = string.Empty;
		if (num > 0)
		{
			empty = text.Substring(num - 1);
			empty = "…" + empty;
			char[] array2 = empty.ToCharArray();
			Array.Reverse(array2);
			return new string(array2);
		}
		return pop_text;
	}

	public string GetAdjustBeforeText(int index)
	{
		if (popAdjustBeforeList == null || popAdjustBeforeList.Count <= index || index < 0)
		{
			return string.Empty;
		}
		return popAdjustBeforeList[index];
	}
}
