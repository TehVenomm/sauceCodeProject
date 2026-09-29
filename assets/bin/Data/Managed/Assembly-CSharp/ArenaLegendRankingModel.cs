// Decompiled with JetBrains decompiler
// Type: ArenaLegendRankingModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class ArenaLegendRankingModel : BaseModel
{
  public static string URL = "ajax/arena/legend-ranking";
  public List<ArenaLegendRankingModel.Param> result = new List<ArenaLegendRankingModel.Param>();

  [Serializable]
  public class Param
  {
    public Network.EventData eventData;
    public ArenaRankingData rankingData = new ArenaRankingData();
  }
}
