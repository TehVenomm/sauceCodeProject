using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class TaskCompleteReward
	{
		public class Item
		{
			public int itemId;

			public int num;
		}

		public class EquipItem
		{
			public int equipItemId;

			public int lv;

			public int num;
		}

		public class SkillItem
		{
			public int skillItemId;

			public int lv;

			public int num;
		}

		public class SellItem
		{
			public int itemId;

			public int num;

			public int price;
		}

		public int exp;

		public int money;

		public int crystal;

		public List<Item> item = new List<Item>();

		public List<EquipItem> equipItem = new List<EquipItem>();

		public List<SkillItem> skillItem = new List<SkillItem>();
	}
}
