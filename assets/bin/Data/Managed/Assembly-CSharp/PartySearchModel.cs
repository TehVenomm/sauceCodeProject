// Decompiled with JetBrains decompiler
// Type: PartySearchModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class PartySearchModel : BaseModel
{
  public static string URL = "ajax/party/search";
  public PartySearchModel.Param result = new PartySearchModel.Param();

  public class Param
  {
    public List<PartyModel.Party> partys = new List<PartyModel.Party>();
  }

  public class RequestSendForm
  {
    public int order;
    public int rarityBit;
    public int elementBit;
    public int enemyLevelMin;
    public int enemyLevelMax;
    public int enemySpecies;
    public int questTypeBit;
    public int isFs;
    public int isCs;
  }
}
