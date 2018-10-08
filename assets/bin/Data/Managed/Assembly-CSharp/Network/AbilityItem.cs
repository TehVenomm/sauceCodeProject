using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
	[Serializable]
	public class AbilityItem
	{
		public class Data
		{
			public int abilityItemLotId;

			public string abilityType;

			public int value;

			public string target;

			public string spTarget;

			public string spAttackType;

			public string format;
		}

		public string uniqId;

		public int abilityItemId;

		public string equipItemUniqId;

		public List<Data> data = new List<Data>();

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0},", uniqId);
			stringBuilder.AppendFormat("{0},", abilityItemId);
			stringBuilder.AppendFormat("{0},", equipItemUniqId);
			int i = 0;
			for (int count = data.Count; i < count; i++)
			{
				stringBuilder.Append("d(");
				stringBuilder.AppendFormat("{0},", data[i].abilityType);
				stringBuilder.AppendFormat("{0},", data[i].value);
				stringBuilder.AppendFormat("{0},", data[i].target);
				stringBuilder.AppendFormat("{0},", data[i].spTarget);
				stringBuilder.AppendFormat("{0},", data[i].spAttackType);
				stringBuilder.Append("),");
			}
			return base.ToString() + stringBuilder.ToString();
		}
	}
}
