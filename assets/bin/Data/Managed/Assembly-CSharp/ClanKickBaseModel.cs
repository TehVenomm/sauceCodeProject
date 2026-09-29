// Decompiled with JetBrains decompiler
// Type: ClanKickBaseModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ClanKickBaseModel : BaseModel
{
  public static string URL = "ajax/clan/kick-base";
  public ClanKickBaseModel.Param result = new ClanKickBaseModel.Param();

  public class Param
  {
    public PartyModel.Party clanParty;
  }

  public class RequestSendForm
  {
    public int uId;
  }
}
