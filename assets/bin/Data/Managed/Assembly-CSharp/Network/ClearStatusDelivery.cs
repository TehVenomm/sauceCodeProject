using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class ClearStatusDelivery
	{
		public int deliveryId;

		public int deliveryStatus;

		public List<int> needCount = new List<int>();

		public int GetNeedCount(uint idx = 0u)
		{
			if (idx >= needCount.Count)
			{
				return 0;
			}
			return needCount[(int)idx];
		}

		public int GetAllNeedCount()
		{
			int num = 0;
			int i = 0;
			for (int count = needCount.Count; i < count; i++)
			{
				num += needCount[i];
			}
			return num;
		}
	}
}
