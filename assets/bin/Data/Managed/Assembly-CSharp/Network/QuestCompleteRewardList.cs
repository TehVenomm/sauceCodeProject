// Decompiled with JetBrains decompiler
// Type: Network.QuestCompleteRewardList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class QuestCompleteRewardList
{
  public QuestCompleteReward breakReward = new QuestCompleteReward();
  public QuestCompleteReward breakPartsReward = new QuestCompleteReward();
  public QuestCompleteReward drop = new QuestCompleteReward();
  public QuestCompleteReward mission = new QuestCompleteReward();
  public QuestCompleteReward followReward = new QuestCompleteReward();
  public QuestCompleteReward missionComplete = new QuestCompleteReward();
  public QuestCompleteReward first = new QuestCompleteReward();
  public QuestCompleteReward order = new QuestCompleteReward();
  public QuestCompleteReward boost = new QuestCompleteReward();
  public List<QuestCompleteReward.SellItem> sell = new List<QuestCompleteReward.SellItem>();
}
