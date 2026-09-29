// Decompiled with JetBrains decompiler
// Type: GuildSearchFollowerRoomModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;

#nullable disable
public class GuildSearchFollowerRoomModel : BaseModel
{
  public static string URL = "clan/ClanFollowingList.go";
  public GuildSearchFollowerRoomModel.Param result = new GuildSearchFollowerRoomModel.Param();

  public class Param
  {
    public List<GuildSearchFollowerRoomModel.GuildFollowerModel> list = new List<GuildSearchFollowerRoomModel.GuildFollowerModel>();
  }

  public class GuildFollowerModel
  {
    public FriendCharaInfo charInfo;
    public GuildModel.Guild clanData;
  }
}
