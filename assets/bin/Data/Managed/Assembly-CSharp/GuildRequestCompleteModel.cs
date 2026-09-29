// Decompiled with JetBrains decompiler
// Type: GuildRequestCompleteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class GuildRequestCompleteModel : BaseModel
{
  public static string URL = "ajax/guild-request/complete";
  public GuildRequestCompleteModel.Param result = new GuildRequestCompleteModel.Param();

  [Serializable]
  public class Param
  {
    public QuestCompleteRewardList reward = new QuestCompleteRewardList();
    public List<PointShopResultData> bonusPointShop = new List<PointShopResultData>();
    public List<PointEventCurrentData> pointEvent;
  }

  public class RequestSendForm
  {
    public int slotNo;
  }
}
