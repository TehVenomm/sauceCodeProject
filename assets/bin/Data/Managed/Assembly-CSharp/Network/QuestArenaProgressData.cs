// Decompiled with JetBrains decompiler
// Type: Network.QuestArenaProgressData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace Network;

public class QuestArenaProgressData
{
  public XorInt remainMilliSec = (XorInt) 0;
  public List<QuestArenaProgressData.ArenaTimeBonus> plusSec = new List<QuestArenaProgressData.ArenaTimeBonus>();
  public QuestCompleteRewardList reward = new QuestCompleteRewardList();
  public List<PointShopResultData> pointShop = new List<PointShopResultData>();

  public class ArenaTimeBonus
  {
    public string bonusName = string.Empty;
    public int plusSec;
  }
}
