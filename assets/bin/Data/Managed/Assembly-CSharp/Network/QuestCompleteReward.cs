using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class QuestCompleteReward
	{
		public class BaseReward
		{
			public string rewardTitle;
		}

		public class Item : BaseReward
		{
			public int itemId;

			public int num;
		}

		public class EquipItem : BaseReward
		{
			public int equipItemId;

			public int lv;

			public int num;
		}

		public class SkillItem : BaseReward
		{
			public int skillItemId;

			public int lv;

			public int num;
		}

		public class QuestItem : BaseReward
		{
			public int questId;

			public int num;
		}

		public class SellItem : BaseReward
		{
			public int itemId;

			public int num;

			public int price;
		}

		public class EventPrice : BaseReward
		{
			public int gold;

			public int crystal;
		}

		public class GatherItem : BaseReward
		{
			public int gatherItemId;

			public int score;

			public PopSignatureInfo psig;

			public int status;

			public int maxCrownType;
		}

		public class AccessoryItem : BaseReward
		{
			public int accessoryId;

			public int num;
		}

		public int exp;

		public int money;

		public int crystal;

		public List<Item> item = new List<Item>();

		public List<EquipItem> equipItem = new List<EquipItem>();

		public List<SkillItem> skillItem = new List<SkillItem>();

		public List<QuestItem> questItem = new List<QuestItem>();

		public List<EventPrice> eventPrice = new List<EventPrice>();

		public List<GatherItem> gatherItem = new List<GatherItem>();

		public List<AccessoryItem> accessoryItem = new List<AccessoryItem>();

		public void Add(QuestCompleteReward reward)
		{
			exp += reward.exp;
			money += reward.money;
			crystal += reward.crystal;
			item.AddRange(reward.item);
			equipItem.AddRange(reward.equipItem);
			skillItem.AddRange(reward.skillItem);
			questItem.AddRange(reward.questItem);
			eventPrice.AddRange(reward.eventPrice);
			accessoryItem.AddRange(reward.accessoryItem);
		}
	}
}
