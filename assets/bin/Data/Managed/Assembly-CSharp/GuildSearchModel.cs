// Decompiled with JetBrains decompiler
// Type: GuildSearchModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class GuildSearchModel : BaseModel
{
  public GuildSearchModel.Param result = new GuildSearchModel.Param();

  public class Param
  {
    public List<GuildSearchModel.GuildSearchInfo> clanList;
  }

  public class GuildSearchInfo
  {
    public int level;
    public string name;
    public string admin;
    public int privacy;
    public int currentMem;
    public int memCap;
    public int[] emblem;
    public int clanId;
    public string tag;
  }

  public class RequestSearch
  {
    public static string path = "clan/ClanList.go";
    public string token;
  }

  public class RequestSearchWithKeyword
  {
    public static string path = "clan/ClanSearch.go";
    public string token;
    public string keyword;
  }
}
