// Decompiled with JetBrains decompiler
// Type: ClanAcceptInviteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ClanAcceptInviteModel : BaseModel
{
  public static string URL = "ajax/clan/accept-invite";
  public ClanAcceptInviteModel.Param result = new ClanAcceptInviteModel.Param();

  public class Param
  {
    public PartyModel.Party clanParty;
    public ClanServer clanServer;
  }

  public class RequestSendForm
  {
    public int cId;
  }
}
