using System;

namespace Network
{
	[Serializable]
	public class GatherItemRecord
	{
		public int listId;

		public string name;

		public int num;

		public int maxValue;

		public int maxCrownType;

		public GATHER_ITEM_CROWN_TYPE GetCrownType()
		{
			return (GATHER_ITEM_CROWN_TYPE)maxCrownType;
		}

		public string GetSizeString()
		{
			return ShapeSize(maxValue);
		}

		public static string ShapeSize(int size)
		{
			string empty = string.Empty;
			if (size < 10)
			{
				return "0.0" + size.ToString();
			}
			if (size < 100)
			{
				return "0." + size.ToString();
			}
			empty = size.ToString();
			return empty.Insert(empty.Length - 2, ".");
		}
	}
}
