using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviourSingleton<AchievementManager>
{
	private List<AchievementCounter> achievementCounterList = new List<AchievementCounter>();

	private List<TaskInfo> taskInfos = new List<TaskInfo>();

	private Queue<TaskInfo> achievedTask = new Queue<TaskInfo>();

	private const uint STR_CRYSTAL = 100u;

	private const uint STR_GOLD = 101u;

	private const uint STR_EXP = 102u;

	private const uint STR_PIECE = 3000u;

	private bool firstSetAchievement = true;

	public List<AchievementCounter> monsterCollectionList => achievementCounterList.FindAll((AchievementCounter x) => x.Type == ACHIEVEMENT_TYPE.ENEMY_COLLECTION);

	public List<EquipItemCollection> equipItemCollectionList
	{
		get;
		private set;
	}

	public List<AchievementCounter> GetAchievementCounterList()
	{
		return achievementCounterList;
	}

	private AchievementCounter _GetAchievementCounter(ACHIEVEMENT_TYPE type, int subType = 0)
	{
		return achievementCounterList.Find((AchievementCounter a) => a.Type == type && a.subType == subType);
	}

	public List<TaskInfo> GetTaskInfos()
	{
		return taskInfos;
	}

	public int GetEquipItemCollectionNum()
	{
		AchievementCounter achievementCounter = _GetAchievementCounter(ACHIEVEMENT_TYPE.EQUIP_ITEM_COLLECTION);
		if (achievementCounter == null)
		{
			return 0;
		}
		return (int)achievementCounter.Count;
	}

	public int GetEnemyCollectionNum()
	{
		AchievementCounter achievementCounter = _GetAchievementCounter(ACHIEVEMENT_TYPE.ENEMY_COLLECTION);
		if (achievementCounter == null)
		{
			return 0;
		}
		return (int)achievementCounter.Count;
	}

	private EquipItemCollection _GetEquipItemCollection(string category)
	{
		return equipItemCollectionList.Find((EquipItemCollection c) => c.category == category);
	}

	private void _InitEquipItemCollection(EquipItemCollectionList list)
	{
		equipItemCollectionList.Clear();
		int i = 0;
		for (int count = list.categories.Count; i < count; i++)
		{
			EquipItemCollection equipItemCollection = new EquipItemCollection();
			equipItemCollection.category = list.categories[i];
			equipItemCollection.bit = list.bits[i];
			equipItemCollectionList.Add(equipItemCollection);
		}
	}

	public bool CheckEquipItemCollection(EquipItemTable.EquipItemData equipItem)
	{
		if (equipItem == null || equipItem.obtained == null || equipItem.obtained.category.Length == 0 || equipItem.obtained.flag < 0 || equipItem.obtained.flag >= 64)
		{
			return false;
		}
		return _GetEquipItemCollection(equipItem.obtained.category)?.CheckBit(equipItem.obtained.flag) ?? false;
	}

	public AchievementManager()
	{
		equipItemCollectionList = new List<EquipItemCollection>();
	}

	public string GetRewardName(REWARD_TYPE rewardType, uint itemId, uint num, uint param0)
	{
		string text = string.Empty;
		switch (rewardType)
		{
		case REWARD_TYPE.CRYSTAL:
			text = num.ToString() + " " + StringTable.Get(STRING_CATEGORY.COMMON, 100u) + " " + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
			break;
		case REWARD_TYPE.MONEY:
			text = num.ToString() + " " + StringTable.Get(STRING_CATEGORY.COMMON, 101u);
			break;
		case REWARD_TYPE.EXP:
			text = StringTable.Get(STRING_CATEGORY.COMMON, 102u);
			break;
		case REWARD_TYPE.ITEM:
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemId);
			if (itemData != null)
			{
				text = itemData.name;
				if (num > 1)
				{
					text = num.ToString() + " " + text + " " + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
				}
			}
			break;
		}
		case REWARD_TYPE.EQUIP_ITEM:
		{
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(itemId);
			if (equipItemData != null)
			{
				text = equipItemData.name;
				if (num > 1)
				{
					text = num.ToString() + " " + text + " " + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
				}
			}
			break;
		}
		case REWARD_TYPE.SKILL_ITEM:
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(itemId);
			if (skillItemData != null)
			{
				text = skillItemData.name;
				if (num > 1)
				{
					text = num.ToString() + " " + text + " " + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
				}
			}
			break;
		}
		case REWARD_TYPE.ACCESSORY:
		{
			AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(itemId);
			if (data != null)
			{
				text = data.name;
				if (num > 1)
				{
					text = text + num.ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 3000u);
				}
			}
			break;
		}
		}
		return text;
	}

	public void SetAchievement()
	{
		if (firstSetAchievement)
		{
			firstSetAchievement = false;
			OnceAchievementModel.Param achievement = MonoBehaviourSingleton<OnceManager>.I.result.achievement;
			if (achievement.achievement != null)
			{
				achievementCounterList = achievement.achievement;
			}
			if (achievement.equipCollection != null)
			{
				_InitEquipItemCollection(achievement.equipCollection);
			}
			List<TaskInfo> list = taskInfos = MonoBehaviourSingleton<OnceManager>.I.result.task;
		}
	}

	public void OnDiff(BaseModelDiff.DiffAchievement diff)
	{
		if (Utility.IsExist(diff.add))
		{
			achievementCounterList.AddRange(diff.add);
		}
		if (Utility.IsExist(diff.update))
		{
			long num = 0L;
			if (_GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL) != null)
			{
				num = _GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL).Count;
			}
			diff.update.ForEach(delegate(AchievementCounter achieve)
			{
				AchievementCounter achievementCounter = _GetAchievementCounter(achieve.Type, achieve.subType);
				if (achievementCounter != null)
				{
					achievementCounter.count = achieve.count;
				}
			});
			long num2 = 0L;
			if (_GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL) != null)
			{
				num2 = _GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL).Count;
			}
			if ((num2 >= 20 && num < 20) || (num2 >= 50 && num < 50) || (num2 >= 100 && num < 100) || (num2 >= 200 && num < 200) || (num2 >= 300 && num < 300) || (num2 >= 400 && num < 400))
			{
				GameSaveData.instance.happyTimeForRating = true;
			}
		}
	}

	public void OnDiff(BaseModelDiff.DiffEquipCollection diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			equipItemCollectionList.AddRange(diff.add);
			flag = true;
		}
		if (Utility.IsExist(diff.update))
		{
			diff.update.ForEach(delegate(EquipItemCollection collection)
			{
				EquipItemCollection equipItemCollection = _GetEquipItemCollection(collection.category);
				if (equipItemCollection != null)
				{
					equipItemCollection.bit = collection.bit;
				}
			});
			flag = true;
		}
		if (flag && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene" && MonoBehaviourSingleton<SmithManager>.IsValid())
		{
			int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
			MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(is_force: true);
			if (MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum() != badgeTotalNum)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE);
			}
		}
	}

	public void OnDiff(BaseModelDiff.DiffTaskList diff)
	{
		bool flag = false;
		if (diff.add != null)
		{
			for (int j = 0; j < diff.add.Count; j++)
			{
				if (!taskInfos.Contains(diff.add[j]))
				{
					taskInfos.Add(diff.add[j]);
					flag = true;
					if (diff.add[j].progress > 0)
					{
						MonoBehaviourSingleton<NativeGameService>.I.SetAchievementStep(diff.add[j].taskId, diff.add[j].progress, 0);
					}
				}
			}
		}
		if (diff.update != null)
		{
			int i = 0;
			while (i < diff.update.Count)
			{
				TaskInfo taskInfo = taskInfos.Find((TaskInfo info) => info.taskId == diff.update[i].taskId);
				MonoBehaviourSingleton<NativeGameService>.I.SetAchievementStep(diff.update[i].taskId, diff.update[i].progress, taskInfo.progress);
				taskInfo.newFlg = diff.update[i].newFlg;
				taskInfo.progress = diff.update[i].progress;
				taskInfo.status = diff.update[i].status;
				taskInfo.taskId = diff.update[i].taskId;
				_ = taskInfo.status;
				_ = 2;
				flag = true;
				int num = ++i;
			}
		}
		if (flag)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_TASK_LIST);
		}
	}

	private IEnumerator DispTaskAnnounce()
	{
		TaskClearAnnounce taskClearAnnounce = MonoBehaviourSingleton<UIManager>.I.taskClearAnnouce;
		if (taskClearAnnounce == null)
		{
			yield break;
		}
		while (achievedTask.Count != 0)
		{
			TaskInfo taskInfo = achievedTask.Dequeue();
			TaskTable.TaskData taskData = Singleton<TaskTable>.I.Get((uint)taskInfo.taskId);
			if (taskData != null)
			{
				bool wait = true;
				taskClearAnnounce.Play(taskData.title, taskData.GetRewardString(), delegate
				{
					wait = false;
				});
				while (wait)
				{
					yield return null;
				}
				yield return new WaitForSeconds(0.3f);
			}
		}
		yield return null;
	}
}
