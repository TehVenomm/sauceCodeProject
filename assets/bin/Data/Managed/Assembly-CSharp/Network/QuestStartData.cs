// Decompiled with JetBrains decompiler
// Type: Network.QuestStartData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class QuestStartData
{
  public string qt;
  public List<QuestStartData.EnemyReward> enemy = new List<QuestStartData.EnemyReward>();

  public class EnemyReward
  {
    public List<QuestStartData.RegionDropItem> reward = new List<QuestStartData.RegionDropItem>();
    public QuestStartData.DropHpRate drop = new QuestStartData.DropHpRate();
  }

  public class RegionDropItem
  {
    public int regionId;
    public List<QuestStartData.BreakItem> breakReward = new List<QuestStartData.BreakItem>();
  }

  public class BreakItem
  {
    public int rarity;
    public int type;
    public int num;
  }

  public class DropHpRate
  {
    public List<float> hpRate = new List<float>();
    public List<int> rarity = new List<int>();
  }
}
