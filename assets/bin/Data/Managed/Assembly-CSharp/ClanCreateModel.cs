// Decompiled with JetBrains decompiler
// Type: ClanCreateModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ClanCreateModel : BaseModel
{
  public static string URL = "ajax/clan/create";
  public ClanCreateModel.Param result = new ClanCreateModel.Param();

  public class Param
  {
    public PartyModel.Party clanParty;
    public ClanServer clanServer;
  }

  public class RequestSendForm
  {
    public string name;
    public int jt;
    public int lbl;
    public string cmt;
    public string tag;
    public int crystalCL;
  }
}
