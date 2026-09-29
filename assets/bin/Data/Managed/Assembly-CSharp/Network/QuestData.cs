// Decompiled with JetBrains decompiler
// Type: Network.QuestData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class QuestData
{
  public int questId;
  public int crystalNum;
  public QuestData.QuestRewardList reward = new QuestData.QuestRewardList();
  public QuestData.OrderQuestInfo order;
  public List<float> remainTimes = new List<float>();

  public class QuestRewardList
  {
    public List<int> types = new List<int>();
    public List<int> itemIds = new List<int>();
    public List<int> pri = new List<int>();
  }

  public class OrderQuestInfo
  {
    public int num;
  }
}
