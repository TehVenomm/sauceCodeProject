// Decompiled with JetBrains decompiler
// Type: GuildSearchModelWithID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GuildSearchModelWithID : BaseModel
{
  public GuildSearchModelWithID.Param result = new GuildSearchModelWithID.Param();

  public class Param
  {
    public GuildSearchModel.GuildSearchInfo guildInfo;
  }

  public class RequestSearchWithID
  {
    public static string path = "clan/ClanInfo.go";
    public string token;
    public int clanId;
  }
}
