// Decompiled with JetBrains decompiler
// Type: BaseModelDiff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
[Serializable]
public class BaseModelDiff
{
  public List<BaseModelDiff.DiffUser> user;
  public List<BaseModelDiff.DiffStatus> status;
  public List<BaseModelDiff.DiffEquipSet> equipSet;
  public List<BaseModelDiff.DiffUniqueEquipSet> uniqueEquipSet;
  public List<BaseModelDiff.DiffUserClan> userClan;
  public List<BaseModelDiff.DiffItem> item;
  public List<BaseModelDiff.DiffExpiredItem> expiredItem;
  public List<BaseModelDiff.DiffEquipItem> equipItem;
  public List<BaseModelDiff.DiffSkillItem> skillItem;
  public List<BaseModelDiff.DiffEquipSetSlot> skillItemEquipSlot;
  public List<BaseModelDiff.DiffUniqueEquipSetSlot> skillItemUniqueEquipSlot;
  public List<BaseModelDiff.DiffAbilityItem> abilityItem;
  public List<BaseModelDiff.DiffAccessory> accessory;
  public List<BaseModelDiff.DiffAccessorySet> accessorySet;
  public List<BaseModelDiff.DiffUniqueAccessorySet> uniqueAccessorySet;
  public List<BaseModelDiff.DiffQuestItem> questItem;
  public List<BaseModelDiff.DiffClearStatusQuest> clearStatusQuest;
  public List<BaseModelDiff.DiffClearStatusDelivery> clearStatusDelivery;
  public List<BaseModelDiff.DiffClearStatusQuestEnemySpecies> clearStatusQuestEnemySpecies;
  public List<BaseModelDiff.DiffGatherPoint> gatherPoint;
  public List<BaseModelDiff.DiffBlackList> blacklist;
  public List<BaseModelDiff.DiffDelivery> delivery;
  public List<BaseModelDiff.DiffTraveled> traveled;
  public List<BaseModelDiff.DiffFieldPortal> portal;
  public List<BaseModelDiff.DiffFriend> friend;
  public List<BaseModelDiff.DiffMessage> message;
  public List<BaseModelDiff.DiffBoost> boost;
  public List<BaseModelDiff.DiffNotice> notice;
  public List<BaseModelDiff.DiffFieldGather> fieldGather;
  public List<BaseModelDiff.DiffFieldGatherGrowth> fieldGrowthGather;
  public List<BaseModelDiff.DiffAchievement> achievement;
  public List<BaseModelDiff.DiffEquipCollection> equipCollection;
  public List<BaseModelDiff.DiffServerConstDefine> constDefine;
  public List<BaseModelDiff.DiffTaskList> task;
  public List<BaseModelDiff.DiffVisual> visual;
  public List<BaseModelDiff.DiffStamp> unlockStamps;
  public List<BaseModelDiff.DiffUnlockDegree> unlockDegrees;
  public List<BaseModelDiff.DiffSelectedDegree> selectedDegree;
  public List<BaseModelDiff.DiffGuildRequest> userGuildRequest;

  [Serializable]
  public class DiffUser
  {
    public List<BaseModelDiff.DiffUser.Name> name;
    public List<string> comment;
    public List<BaseModelDiff.DiffUser.Birthday> birthday;
    public List<BaseModelDiff.DiffUser.Option> option;
    public List<bool> inputInviteFlag;
    public List<int> pushEnable;

    public class Name
    {
      public string name = "";
      public EndDate editNameAt;
    }

    public class Birthday
    {
      public string birthday = "";
      public bool communityFlag;
    }

    public class Option
    {
      public int isParentPassSet;
      public int isStopperSet;
    }
  }

  [Serializable]
  public class DiffStatus
  {
    public List<BaseModelDiff.DiffStatus.Views> views;
    public List<BaseModelDiff.DiffStatus.Grow> grow;
    public List<int> money;
    public List<int> crystal;
    public List<int> eSetNo;
    public List<int> ueSetNo;
    public List<int> titleId;
    public List<BaseModelDiff.DiffStatus.Capacity> capacity;
    public List<int> tutorialStep;
    public List<string> tutorialBit;
    public List<int> tutorialQuestId;
    public List<int> researchLv;
    public List<int> questGrade;
    public List<int> fieldGrade;
    public List<BaseModelDiff.DiffStatus.ShowEquip> showEquip;
    public List<int> present;
    public List<int> fairyNum;
    public List<int> gathering;
    public List<int> maxEquipItemTargetNum;

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
      public string armorUniqId = "";
      public string armUniqId = "";
      public string legUniqId = "";
      public string helmUniqId = "";
      public int showHelm;
    }
  }

  [Serializable]
  public class DiffEquipSet
  {
    public List<EquipSetSimple> add;
    public List<EquipSetSimple> update;
  }

  [Serializable]
  public class DiffUniqueEquipSet
  {
    public List<EquipSetSimple> update;
  }

  [Serializable]
  public class DiffUserClan
  {
    public List<UserClanData> add;
    public List<UserClanData> update;
  }

  [Serializable]
  public class DiffItem
  {
    public List<Network.Item> add;
    public List<Network.Item> update;
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
  public class DiffUniqueAccessorySet
  {
    public List<AccessorySet> add;
    public List<AccessorySet> update;
    public List<string> del;
  }

  [Serializable]
  public class DiffSkillItem
  {
    public List<Network.SkillItem> add;
    public List<Network.SkillItem> update;
    public List<string> del;
  }

  [Serializable]
  public class DiffEquipSetSlot
  {
    public List<Network.SkillItem.DiffEquipSetSlot> add;
    public List<Network.SkillItem.DiffEquipSetSlot> update;
  }

  [Serializable]
  public class DiffUniqueEquipSetSlot
  {
    public List<Network.SkillItem.DiffEquipSetSlot> add;
    public List<Network.SkillItem.DiffEquipSetSlot> update;
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
    public List<int> del;
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
    public List<GatherPointData> add;
    public List<GatherPointData> update;
    public List<BaseModelDiff.DiffGatherPoint.RestTime> rest;

    public class RestTime
    {
      public int gatherPointId;
      public int rest;
      public int attackTime;
    }
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
}
