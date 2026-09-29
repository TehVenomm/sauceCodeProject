// Decompiled with JetBrains decompiler
// Type: GuildInvitedModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class GuildInvitedModel : BaseModel
{
  public GuildInvitedModel.Param result = new GuildInvitedModel.Param();

  public class Param
  {
    public List<GuildInvitedModel.GuildInvitedInfo> list;
  }

  public class RequestInvited
  {
    public static string path = "clan/AtvUserRequestList.go";
  }

  public class GuildInvitedInfo
  {
    public int requestId;
    public int level;
    public string name;
    public string admin;
    public int privacy;
    public int currentMem;
    public int memCap;
    public int[] emblem;
    public int clanId;
    public string sender;
  }
}
