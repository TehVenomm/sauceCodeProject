// Decompiled with JetBrains decompiler
// Type: AchievementManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AchievementManager : MonoBehaviourSingleton<AchievementManager>
{
  private List<AchievementCounter> achievementCounterList = new List<AchievementCounter>();
  private List<TaskInfo> taskInfos = new List<TaskInfo>();
  private Queue<TaskInfo> achievedTask = new Queue<TaskInfo>();
  private const uint STR_CRYSTAL = 100;
  private const uint STR_GOLD = 101;
  private const uint STR_EXP = 102;
  private const uint STR_PIECE = 3000;
  private bool firstSetAchievement = true;

  public List<AchievementCounter> GetAchievementCounterList() => this.achievementCounterList;

  private AchievementCounter _GetAchievementCounter(ACHIEVEMENT_TYPE type, int subType = 0)
  {
    return this.achievementCounterList.Find((Predicate<AchievementCounter>) (a => a.Type == type && a.subType == subType));
  }

  public List<TaskInfo> GetTaskInfos() => this.taskInfos;

  public int GetEquipItemCollectionNum()
  {
    AchievementCounter achievementCounter = this._GetAchievementCounter(ACHIEVEMENT_TYPE.EQUIP_ITEM_COLLECTION);
    return achievementCounter == null ? 0 : (int) achievementCounter.Count;
  }

  public int GetEnemyCollectionNum()
  {
    AchievementCounter achievementCounter = this._GetAchievementCounter(ACHIEVEMENT_TYPE.ENEMY_COLLECTION);
    return achievementCounter == null ? 0 : (int) achievementCounter.Count;
  }

  public List<AchievementCounter> monsterCollectionList
  {
    get
    {
      return this.achievementCounterList.FindAll((Predicate<AchievementCounter>) (x => x.Type == ACHIEVEMENT_TYPE.ENEMY_COLLECTION));
    }
  }

  public List<EquipItemCollection> equipItemCollectionList { get; private set; }

  private EquipItemCollection _GetEquipItemCollection(string category)
  {
    return this.equipItemCollectionList.Find((Predicate<EquipItemCollection>) (c => c.category == category));
  }

  private void _InitEquipItemCollection(EquipItemCollectionList list)
  {
    this.equipItemCollectionList.Clear();
    int index = 0;
    for (int count = list.categories.Count; index < count; ++index)
      this.equipItemCollectionList.Add(new EquipItemCollection()
      {
        category = list.categories[index],
        bit = list.bits[index]
      });
  }

  public bool CheckEquipItemCollection(EquipItemTable.EquipItemData equipItem)
  {
    if (equipItem == null || equipItem.obtained == null || equipItem.obtained.category.Length == 0 || equipItem.obtained.flag < 0 || equipItem.obtained.flag >= 64 /*0x40*/)
      return false;
    EquipItemCollection equipItemCollection = this._GetEquipItemCollection(equipItem.obtained.category);
    return equipItemCollection != null && equipItemCollection.CheckBit(equipItem.obtained.flag);
  }

  public AchievementManager() => this.equipItemCollectionList = new List<EquipItemCollection>();

  public string GetRewardName(REWARD_TYPE rewardType, uint itemId, uint num, uint param0)
  {
    string rewardName = string.Empty;
    switch (rewardType)
    {
      case REWARD_TYPE.CRYSTAL:
        rewardName = $"{num.ToString()} {StringTable.Get(STRING_CATEGORY.COMMON, 100U)} {StringTable.Get(STRING_CATEGORY.COMMON, 3000U)}";
        break;
      case REWARD_TYPE.MONEY:
        rewardName = $"{num.ToString()} {StringTable.Get(STRING_CATEGORY.COMMON, 101U)}";
        break;
      case REWARD_TYPE.ITEM:
        ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemId);
        if (itemData != null)
        {
          rewardName = itemData.name;
          if (num > 1U)
          {
            rewardName = $"{num.ToString()} {rewardName} {StringTable.Get(STRING_CATEGORY.COMMON, 3000U)}";
            break;
          }
          break;
        }
        break;
      case REWARD_TYPE.EQUIP_ITEM:
        EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(itemId);
        if (equipItemData != null)
        {
          rewardName = equipItemData.name;
          if (num > 1U)
          {
            rewardName = $"{num.ToString()} {rewardName} {StringTable.Get(STRING_CATEGORY.COMMON, 3000U)}";
            break;
          }
          break;
        }
        break;
      case REWARD_TYPE.SKILL_ITEM:
        SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(itemId);
        if (skillItemData != null)
        {
          rewardName = skillItemData.name;
          if (num > 1U)
          {
            rewardName = $"{num.ToString()} {rewardName} {StringTable.Get(STRING_CATEGORY.COMMON, 3000U)}";
            break;
          }
          break;
        }
        break;
      case REWARD_TYPE.ACCESSORY:
        AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData(itemId);
        if (data != null)
        {
          rewardName = data.name;
          if (num > 1U)
          {
            rewardName = rewardName + num.ToString() + StringTable.Get(STRING_CATEGORY.COMMON, 3000U);
            break;
          }
          break;
        }
        break;
      case REWARD_TYPE.EXP:
        rewardName = StringTable.Get(STRING_CATEGORY.COMMON, 102U);
        break;
    }
    return rewardName;
  }

  public void SetAchievement()
  {
    if (!this.firstSetAchievement)
      return;
    this.firstSetAchievement = false;
    OnceAchievementModel.Param achievement = MonoBehaviourSingleton<OnceManager>.I.result.achievement;
    if (achievement.achievement != null)
      this.achievementCounterList = achievement.achievement;
    if (achievement.equipCollection != null)
      this._InitEquipItemCollection(achievement.equipCollection);
    this.taskInfos = MonoBehaviourSingleton<OnceManager>.I.result.task;
  }

  public void OnDiff(BaseModelDiff.DiffAchievement diff)
  {
    if (Utility.IsExist((ICollection) diff.add))
      this.achievementCounterList.AddRange((IEnumerable<AchievementCounter>) diff.add);
    if (!Utility.IsExist((ICollection) diff.update))
      return;
    long num1 = 0;
    if (this._GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL) != null)
      num1 = this._GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL).Count;
    diff.update.ForEach((Action<AchievementCounter>) (achieve =>
    {
      AchievementCounter achievementCounter = this._GetAchievementCounter(achieve.Type, achieve.subType);
      if (achievementCounter == null)
        return;
      achievementCounter.count = achieve.count;
    }));
    long num2 = 0;
    if (this._GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL) != null)
      num2 = this._GetAchievementCounter(ACHIEVEMENT_TYPE.BOSS_KILL).Count;
    if ((num2 < 20L || num1 >= 20L) && (num2 < 50L || num1 >= 50L) && (num2 < 100L || num1 >= 100L) && (num2 < 200L || num1 >= 200L) && (num2 < 300L || num1 >= 300L) && (num2 < 400L || num1 >= 400L))
      return;
    GameSaveData.instance.happyTimeForRating = true;
  }

  public void OnDiff(BaseModelDiff.DiffEquipCollection diff)
  {
    bool flag = false;
    if (Utility.IsExist((ICollection) diff.add))
    {
      this.equipItemCollectionList.AddRange((IEnumerable<EquipItemCollection>) diff.add);
      flag = true;
    }
    if (Utility.IsExist((ICollection) diff.update))
    {
      diff.update.ForEach((Action<EquipItemCollection>) (collection =>
      {
        EquipItemCollection equipItemCollection = this._GetEquipItemCollection(collection.category);
        if (equipItemCollection == null)
          return;
        equipItemCollection.bit = collection.bit;
      }));
      flag = true;
    }
    if (!flag || !(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene") || !MonoBehaviourSingleton<SmithManager>.IsValid())
      return;
    int badgeTotalNum = MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum();
    MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(true);
    if (MonoBehaviourSingleton<SmithManager>.I.GetBadgeTotalNum() == badgeTotalNum)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_SMITH_BADGE);
  }

  public void OnDiff(BaseModelDiff.DiffTaskList diff)
  {
    bool flag = false;
    if (diff.add != null)
    {
      for (int index = 0; index < diff.add.Count; ++index)
      {
        if (!this.taskInfos.Contains(diff.add[index]))
        {
          this.taskInfos.Add(diff.add[index]);
          flag = true;
          if (diff.add[index].progress > 0)
            MonoBehaviourSingleton<NativeGameService>.I.SetAchievementStep(diff.add[index].taskId, diff.add[index].progress, 0);
        }
      }
    }
    if (diff.update != null)
    {
      for (int i = 0; i < diff.update.Count; ++i)
      {
        TaskInfo taskInfo = this.taskInfos.Find((Predicate<TaskInfo>) (info => info.taskId == diff.update[i].taskId));
        MonoBehaviourSingleton<NativeGameService>.I.SetAchievementStep(diff.update[i].taskId, diff.update[i].progress, taskInfo.progress);
        taskInfo.newFlg = diff.update[i].newFlg;
        taskInfo.progress = diff.update[i].progress;
        taskInfo.status = diff.update[i].status;
        taskInfo.taskId = diff.update[i].taskId;
        int status = taskInfo.status;
        flag = true;
      }
    }
    if (!flag)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_TASK_LIST);
  }

  private IEnumerator DispTaskAnnounce()
  {
    TaskClearAnnounce taskClearAnnounce = MonoBehaviourSingleton<UIManager>.I.taskClearAnnouce;
    if (!Object.op_Equality((Object) taskClearAnnounce, (Object) null))
    {
      while (this.achievedTask.Count != 0)
      {
        TaskInfo taskInfo = this.achievedTask.Dequeue();
        TaskTable.TaskData taskData = Singleton<TaskTable>.I.Get((uint) taskInfo.taskId);
        if (taskData != null)
        {
          bool wait = true;
          taskClearAnnounce.Play(taskData.title, taskData.GetRewardString(), (System.Action) (() => wait = false));
          while (wait)
            yield return (object) null;
          yield return (object) new WaitForSeconds(0.3f);
        }
      }
      yield return (object) null;
    }
  }
}
