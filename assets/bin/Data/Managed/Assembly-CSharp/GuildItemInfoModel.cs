// Decompiled with JetBrains decompiler
// Type: GuildItemInfoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class GuildItemInfoModel : BaseModel
{
  public GuildItemInfoModel.Param result = new GuildItemInfoModel.Param();

  public class Param
  {
    public List<GuildItemInfoModel.EmblemInfo> emblem;
  }

  public class EmblemInfo
  {
    public int id;
    public string image;
    public int price;
    public int currency;
    public int type;
    public string unlock;
  }

  public class RequestAllItemInfo
  {
    public static string path = "clan/ClanAllInfo.go";
    public string token;
  }
}
