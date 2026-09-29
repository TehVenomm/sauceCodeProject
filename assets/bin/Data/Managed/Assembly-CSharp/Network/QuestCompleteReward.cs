// Decompiled with JetBrains decompiler
// Type: Network.QuestCompleteReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class QuestCompleteReward
{
  public int exp;
  public int money;
  public int crystal;
  public List<QuestCompleteReward.Item> item = new List<QuestCompleteReward.Item>();
  public List<QuestCompleteReward.EquipItem> equipItem = new List<QuestCompleteReward.EquipItem>();
  public List<QuestCompleteReward.SkillItem> skillItem = new List<QuestCompleteReward.SkillItem>();
  public List<QuestCompleteReward.QuestItem> questItem = new List<QuestCompleteReward.QuestItem>();
  public List<QuestCompleteReward.EventPrice> eventPrice = new List<QuestCompleteReward.EventPrice>();
  public List<QuestCompleteReward.GatherItem> gatherItem = new List<QuestCompleteReward.GatherItem>();
  public List<QuestCompleteReward.AccessoryItem> accessoryItem = new List<QuestCompleteReward.AccessoryItem>();

  public void Add(QuestCompleteReward reward)
  {
    this.exp += reward.exp;
    this.money += reward.money;
    this.crystal += reward.crystal;
    this.item.AddRange((IEnumerable<QuestCompleteReward.Item>) reward.item);
    this.equipItem.AddRange((IEnumerable<QuestCompleteReward.EquipItem>) reward.equipItem);
    this.skillItem.AddRange((IEnumerable<QuestCompleteReward.SkillItem>) reward.skillItem);
    this.questItem.AddRange((IEnumerable<QuestCompleteReward.QuestItem>) reward.questItem);
    this.eventPrice.AddRange((IEnumerable<QuestCompleteReward.EventPrice>) reward.eventPrice);
    this.accessoryItem.AddRange((IEnumerable<QuestCompleteReward.AccessoryItem>) reward.accessoryItem);
  }

  public class BaseReward
  {
    public string rewardTitle;
  }

  public class Item : QuestCompleteReward.BaseReward
  {
    public int itemId;
    public int num;
  }

  public class EquipItem : QuestCompleteReward.BaseReward
  {
    public int equipItemId;
    public int lv;
    public int num;
  }

  public class SkillItem : QuestCompleteReward.BaseReward
  {
    public int skillItemId;
    public int lv;
    public int num;
  }

  public class QuestItem : QuestCompleteReward.BaseReward
  {
    public int questId;
    public int num;
  }

  public class SellItem : QuestCompleteReward.BaseReward
  {
    public int itemId;
    public int num;
    public int price;
  }

  public class EventPrice : QuestCompleteReward.BaseReward
  {
    public int gold;
    public int crystal;
  }

  public class GatherItem : QuestCompleteReward.BaseReward
  {
    public int gatherItemId;
    public int score;
    public PopSignatureInfo psig;
    public int status;
    public int maxCrownType;
  }

  public class AccessoryItem : QuestCompleteReward.BaseReward
  {
    public int accessoryId;
    public int num;
  }
}
