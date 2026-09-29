// Decompiled with JetBrains decompiler
// Type: ClanApplyModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ClanApplyModel : BaseModel
{
  public static string URL = "ajax/clan/apply";
  public ClanApplyModel.Param result = new ClanApplyModel.Param();

  public class Param
  {
    public PartyModel.Party clanParty;
    public ClanServer clanServer;
  }

  public class RequestSendForm
  {
    public string cId;
  }
}
