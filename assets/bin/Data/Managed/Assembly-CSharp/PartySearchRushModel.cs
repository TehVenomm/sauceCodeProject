// Decompiled with JetBrains decompiler
// Type: PartySearchRushModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class PartySearchRushModel : BaseModel
{
  public static string URL = "ajax/party/towersearchparty";
  public PartySearchRushModel.Param result = new PartySearchRushModel.Param();

  public class Param
  {
    public List<PartyModel.Party> partys = new List<PartyModel.Party>();
  }

  public class RequestSendForm
  {
    public int floorMinQuestId;
    public int floorMaxQuestId;
  }
}
