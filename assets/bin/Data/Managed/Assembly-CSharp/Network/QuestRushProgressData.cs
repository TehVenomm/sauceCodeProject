// Decompiled with JetBrains decompiler
// Type: Network.QuestRushProgressData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace Network;

public class QuestRushProgressData
{
  public QuestCompleteRewardList reward = new QuestCompleteRewardList();
  public int remainSec;
  public List<QuestRushProgressData.RushTimeBonus> plusSec = new List<QuestRushProgressData.RushTimeBonus>();
  public List<PointEventCurrentData> pointEvent;
  public List<PointShopResultData> pointShop = new List<PointShopResultData>();

  public class RushTimeBonus
  {
    public string bonusName = "";
    public int plusSec;
  }
}
