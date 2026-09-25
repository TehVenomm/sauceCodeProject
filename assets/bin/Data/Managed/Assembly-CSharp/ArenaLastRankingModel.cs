// Decompiled with JetBrains decompiler
// Type: ArenaLastRankingModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class ArenaLastRankingModel : BaseModel
{
  public static string URL = "ajax/arena/last-ranking";
  public ArenaLastRankingModel.Param result = new ArenaLastRankingModel.Param();

  [Serializable]
  public class Param
  {
    public Network.EventData eventData;
    public List<ArenaRankingData> rankingDataList = new List<ArenaRankingData>();
    public int myRank;
  }

  public class RequestSendForm
  {
    public int groupId;
    public int isContainSelf;
  }
}
