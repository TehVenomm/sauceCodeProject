// Decompiled with JetBrains decompiler
// Type: Network.QuestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class QuestItem
{
  public string uniqId;
  public int questId;
  public int num;
  public List<QuestItem.SellItem> sellItems = new List<QuestItem.SellItem>();
  public QuestData.QuestRewardList reward = new QuestData.QuestRewardList();
  public List<float> remainTimes = new List<float>();

  public class SellItem
  {
    public int type;
    public int itemId;
    public int param0;
    public int num;
    public int pri;
  }
}
