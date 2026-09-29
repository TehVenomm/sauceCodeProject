// Decompiled with JetBrains decompiler
// Type: Network.TaskCompleteReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class TaskCompleteReward
{
  public int exp;
  public int money;
  public int crystal;
  public List<TaskCompleteReward.Item> item = new List<TaskCompleteReward.Item>();
  public List<TaskCompleteReward.EquipItem> equipItem = new List<TaskCompleteReward.EquipItem>();
  public List<TaskCompleteReward.SkillItem> skillItem = new List<TaskCompleteReward.SkillItem>();

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
}
