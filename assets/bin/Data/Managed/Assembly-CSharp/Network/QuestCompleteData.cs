// Decompiled with JetBrains decompiler
// Type: Network.QuestCompleteData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class QuestCompleteData
{
  public QuestCompleteRewardList reward = new QuestCompleteRewardList();
  public List<FollowPartyMember> friend;
  public int followNum;
  public List<PointEventCurrentData> pointEvent;
  public List<PointShopResultData> pointShop = new List<PointShopResultData>();
  public int guildPoint;
  public PointEventCurrentData pointExplore;
  public PointEventCurrentData waveMatchPoint;
  public QuestCompleteData.SeriesArenaData seriesArena = new QuestCompleteData.SeriesArenaData();
  public PartyModel.Param repeatParty;

  [Serializable]
  public class SeriesArenaData
  {
    public int beforeRank;
    public int afterRank;
    public int prevClearTime;
  }
}
