using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class AbilityItem
	{
		public class Data
		{
			public string abilityType;

			public int value;

			public string target;

			public string spTarget;

			public string format;
		}

		public string uniqId;

		public int abilityItemId;

		public string equipItemUniqId;

		public List<Data> data = new List<Data>();

		public override string ToString()
		{
			string empty = string.Empty;
			empty = empty + uniqId + ",";
			empty = empty + abilityItemId + ",";
			empty = empty + equipItemUniqId + ",";
			int i = 0;
			for (int count = data.Count; i < count; i++)
			{
				empty += "d(";
				empty = empty + data[i].abilityType + ",";
				empty = empty + data[i].value + ",";
				empty = empty + data[i].target + ",";
				empty = empty + data[i].spTarget + ",";
				empty += "),";
			}
			return base.ToString() + empty;
		}
	}
}
