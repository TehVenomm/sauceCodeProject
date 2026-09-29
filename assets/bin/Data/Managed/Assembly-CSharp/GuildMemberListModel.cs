// Decompiled with JetBrains decompiler
// Type: GuildMemberListModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class GuildMemberListModel : BaseModel
{
  public static string URL = "clan/ClanMemberList.go";
  public GuildMemberListModel.Param result = new GuildMemberListModel.Param();

  [Serializable]
  public class Param
  {
    public List<FriendCharaInfo> list = new List<FriendCharaInfo>();
    public List<FriendCharaInfo> requesters = new List<FriendCharaInfo>();
    public int clanMasterId;
  }

  public class RequestSendForm
  {
    public int clanId;
  }
}
