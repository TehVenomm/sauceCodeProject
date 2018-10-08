using Network;
using System;
using System.Collections.Generic;

[Serializable]
public class BaseModelDiff
{
	[Serializable]
	public class DiffUser
	{
		public class Name
		{
			public string name = string.Empty;

			public EndDate editNameAt;
		}

		public class Birthday
		{
			public string birthday = string.Empty;

			public bool communityFlag;
		}

		public class Option
		{
			public int isParentPassSet;

			public int isStopperSet;
		}

		public List<Name> name;

		public List<string> comment;

		public List<Birthday> birthday;

		public List<Option> option;

		public List<bool> inputInviteFlag;

		public List<int> pushEnable;
	}

	[Serializable]
	public class DiffStatus
	{
		public class Views
		{
			public int sex;

			public int faceId;

			public int hairId;

			public int hairColorId;

			public int skinId;

			public int voiceId;
		}

		public class Grow
		{
			public int level;

			public int exp;

			public int expPrev;

			public int expNext;

			public int hp;

			public int atk;

			public int def;

			public int maxFollow;
		}

		public class Capacity
		{
			public int maxEquipItem;

			public int maxSkillItem;
		}

		public class ShowEquip
		{
			public string armorUniqId = string.Empty;

			public string armUniqId = string.Empty;

			public string legUniqId = string.Empty;

			public string helmUniqId = string.Empty;

			public int showHelm;
		}

		public List<Views> views;

		public List<Grow> grow;

		public List<int> money;

		public List<int> crystal;

		public List<int> eSetNo;

		public List<int> titleId;

		public List<Capacity> capacity;

		public List<int> tutorialStep;

		public List<string> tutorialBit;

		public List<int> tutorialQuestId;

		public List<int> researchLv;

		public List<int> questGrade;

		public List<int> fieldGrade;

		public List<ShowEquip> showEquip;

		public List<int> present;

		public List<int> fairyNum;

		public List<int> gathering;

		public List<int> maxEquipItemTargetNum;
	}

	[Serializable]
	public class DiffEquipSet
	{
		public List<EquipSetSimple> add;

		public List<EquipSetSimple> update;
	}

	[Serializable]
	public class DiffItem
	{
		public List<Item> add;

		public List<Item> update;
	}

	[Serializable]
	public class DiffExpiredItem
	{
		public List<ExpiredItem> add;

		public List<ExpiredItem> update;
	}

	[Serializable]
	public class DiffEquipItem
	{
		public List<EquipItem> add;

		public List<EquipItem> update;

		public List<string> del;
	}

	[Serializable]
	public class DiffAbilityItem
	{
		public List<AbilityItem> add;

		public List<AbilityItem> update;

		public List<string> del;
	}

	[Serializable]
	public class DiffAccessory
	{
		public List<Accessory> add;

		public List<Accessory> update;

		public List<string> del;
	}

	[Serializable]
	public class DiffAccessorySet
	{
		public List<AccessorySet> add;

		public List<AccessorySet> update;

		public List<string> del;
	}

	[Serializable]
	public class DiffSkillItem
	{
		public List<SkillItem> add;

		public List<SkillItem> update;

		public List<string> del;
	}

	[Serializable]
	public class DiffEquipSetSlot
	{
		public List<SkillItem.DiffEquipSetSlot> add;

		public List<SkillItem.DiffEquipSetSlot> update;
	}

	[Serializable]
	public class DiffQuestItem
	{
		public List<QuestItem> add;

		public List<QuestItem> update;
	}

	[Serializable]
	public class DiffClearStatusQuest
	{
		public List<ClearStatusQuest> add;

		public List<ClearStatusQuest> update;
	}

	[Serializable]
	public class DiffClearStatusDelivery
	{
		public List<ClearStatusDelivery> add;

		public List<ClearStatusDelivery> update;
	}

	[Serializable]
	public class DiffClearStatusQuestEnemySpecies
	{
		public List<ClearStatusQuestEnemySpecies> add;

		public List<ClearStatusQuestEnemySpecies> update;
	}

	[Serializable]
	public class DiffGatherPoint
	{
		public class RestTime
		{
			public int gatherPointId;

			public int rest;

			public int attackTime;
		}

		public List<GatherPointData> add;

		public List<GatherPointData> update;

		public List<RestTime> rest;
	}

	[Serializable]
	public class DiffBlackList
	{
		public List<int> add;

		public List<int> del;
	}

	[Serializable]
	public class DiffDelivery
	{
		public List<Delivery> add;

		public List<Delivery> update;
	}

	public class DiffTraveled
	{
		public List<int> add;

		public List<int> del;
	}

	public class DiffFieldPortal
	{
		public List<FieldPortal> add;

		public List<FieldPortal> update;
	}

	public class DiffFriend
	{
		public List<int> follow;

		public List<int> follower;
	}

	public class DiffMessage
	{
		public List<FriendMessageData> add;
	}

	public class DiffBoost
	{
		public List<BoostStatus> add;

		public List<BoostStatus> update;
	}

	public class DiffNotice
	{
		public List<LoginNotice> login;
	}

	public class DiffFieldGather
	{
		public List<int> add;

		public List<int> del;
	}

	public class DiffFieldGatherGrowth
	{
		public List<GatherGrowthInfo> add;

		public List<GatherGrowthInfo> del;
	}

	public class DiffAchievement
	{
		public List<AchievementCounter> add;

		public List<AchievementCounter> update;
	}

	public class DiffEquipCollection
	{
		public List<EquipItemCollection> add;

		public List<EquipItemCollection> update;
	}

	public class DiffServerConstDefine
	{
		public List<ServerConstDefine> update;
	}

	public class DiffTaskList
	{
		public List<TaskInfo> add;

		public List<TaskInfo> update;
	}

	[Serializable]
	public class DiffVisual
	{
		public List<GlobalSettingsManager.HasVisuals> update;
	}

	[Serializable]
	public class DiffStamp
	{
		public List<int> update;
	}

	[Serializable]
	public class DiffUnlockDegree
	{
		public List<int> update;
	}

	[Serializable]
	public class DiffSelectedDegree
	{
		public List<SelectDegree> add;

		public List<SelectDegree> update;
	}

	[Serializable]
	public class DiffGuildRequest
	{
		public List<GuildRequestItem> add;

		public List<GuildRequestItem> update;
	}

	public List<DiffUser> user;

	public List<DiffStatus> status;

	public List<DiffEquipSet> equipSet;

	public List<DiffItem> item;

	public List<DiffExpiredItem> expiredItem;

	public List<DiffEquipItem> equipItem;

	public List<DiffSkillItem> skillItem;

	public List<DiffEquipSetSlot> skillItemEquipSlot;

	public List<DiffAbilityItem> abilityItem;

	public List<DiffAccessory> accessory;

	public List<DiffAccessorySet> accessorySet;

	public List<DiffQuestItem> questItem;

	public List<DiffClearStatusQuest> clearStatusQuest;

	public List<DiffClearStatusDelivery> clearStatusDelivery;

	public List<DiffClearStatusQuestEnemySpecies> clearStatusQuestEnemySpecies;

	public List<DiffGatherPoint> gatherPoint;

	public List<DiffBlackList> blacklist;

	public List<DiffDelivery> delivery;

	public List<DiffTraveled> traveled;

	public List<DiffFieldPortal> portal;

	public List<DiffFriend> friend;

	public List<DiffMessage> message;

	public List<DiffBoost> boost;

	public List<DiffNotice> notice;

	public List<DiffFieldGather> fieldGather;

	public List<DiffFieldGatherGrowth> fieldGrowthGather;

	public List<DiffAchievement> achievement;

	public List<DiffEquipCollection> equipCollection;

	public List<DiffServerConstDefine> constDefine;

	public List<DiffTaskList> task;

	public List<DiffVisual> visual;

	public List<DiffStamp> unlockStamps;

	public List<DiffUnlockDegree> unlockDegrees;

	public List<DiffSelectedDegree> selectedDegree;

	public List<DiffGuildRequest> userGuildRequest;
}
