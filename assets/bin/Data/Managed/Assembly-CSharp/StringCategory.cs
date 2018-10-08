using System;

public class StringCategory
{
	public static readonly string[] categorice = Enum.GetNames(typeof(STRING_CATEGORY));

	public static STRING_CATEGORY FromString(string str)
	{
		int i = 0;
		for (int num = categorice.Length; i < num; i++)
		{
			if (categorice[i] == str)
			{
				return (STRING_CATEGORY)i;
			}
		}
		Log.Error("{0} is not found, on STRING_CATEGORY.", str);
		return STRING_CATEGORY.COMMON;
	}
}
