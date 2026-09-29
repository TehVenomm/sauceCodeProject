// Decompiled with JetBrains decompiler
// Type: Network.PointEventCurrentData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class PointEventCurrentData
{
  public int eventId;
  public PointEventCurrentData.PointResultData pointRankingData;
  public string rewardTitle;

  public class PointResultData
  {
    public List<PointEventCurrentData.PointRewardData> getReward = new List<PointEventCurrentData.PointRewardData>();
    public PointEventCurrentData.PointRewardData nextReward;
    public int userPoint;
    public int getPoint;
    public List<PointEventCurrentData.BonusPointData> bonusPoint = new List<PointEventCurrentData.BonusPointData>();
    public int beforeRank;
    public int afterRank;
    public bool isStartedBoost;
  }

  public class PointRewardData
  {
    public int point;
    public List<PointEventCurrentData.Reward> reward = new List<PointEventCurrentData.Reward>();
  }

  public class Reward
  {
    public int type;
    public int itemId;
    public int num;
    public string description;
  }

  public class BonusPointData
  {
    public string name;
    public int point;
    public float boostRate;
  }
}
