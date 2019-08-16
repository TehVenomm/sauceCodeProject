public static class ScreenSafeArea
{
	private static bool successGetSafeArea;

	private static EdgeInsets edgeInsets;

	private static string _getSafeArea()
	{
		return string.Empty;
	}

	public static bool HasSafeArea()
	{
		syncSafeAreaData();
		if (successGetSafeArea && edgeInsets != null && !edgeInsets.IsZero())
		{
			return true;
		}
		return false;
	}

	public static EdgeInsets GetSafeArea()
	{
		syncSafeAreaData();
		return edgeInsets;
	}

	private static void syncSafeAreaData()
	{
		string text = _getSafeArea();
		if (!string.IsNullOrEmpty(text) && ConvertEdgeInsetsFromString(text))
		{
			successGetSafeArea = true;
		}
		else
		{
			successGetSafeArea = false;
		}
	}

	private static bool ConvertEdgeInsetsFromString(string str)
	{
		str = str.Replace("{", string.Empty).Replace("}", string.Empty);
		string[] array = str.Split(',');
		if (array.Length == 4 && float.TryParse(array[0], out float result) && float.TryParse(array[1], out float result2) && float.TryParse(array[2], out float result3) && float.TryParse(array[3], out float result4))
		{
			if (edgeInsets == null)
			{
				edgeInsets = new EdgeInsets(result, result2, result3, result4);
			}
			else
			{
				edgeInsets.Set(result, result2, result3, result4);
			}
			return true;
		}
		edgeInsets = null;
		return false;
	}
}
