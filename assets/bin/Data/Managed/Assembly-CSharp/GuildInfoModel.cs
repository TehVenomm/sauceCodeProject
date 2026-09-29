// Decompiled with JetBrains decompiler
// Type: GuildInfoModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class GuildInfoModel : BaseModel
{
  public static string URL = "clan/ClanHome.go";
  public GuildInfoModel.GuildInfo result = new GuildInfoModel.GuildInfo();

  public class GuildInfo
  {
    public ChatChannelInfo chat;
    public List<GuildInfoModel.MemberChatStatus> online = new List<GuildInfoModel.MemberChatStatus>();
    public bool receivable;
    public string askUpdate;
    public bool invitation;
    public int donateCap;
    public int donateMaxCap;
    public GuildModel.Guild guildInfo;

    public bool IsOnline(int user_id)
    {
      foreach (GuildInfoModel.MemberChatStatus memberChatStatus in this.online)
      {
        if (memberChatStatus.id == user_id)
          return true;
      }
      return false;
    }

    public void OnOnline(int user_id)
    {
      foreach (GuildInfoModel.MemberChatStatus memberChatStatus in this.online)
      {
        if (memberChatStatus.id == user_id)
          return;
      }
      this.online.Add(new GuildInfoModel.MemberChatStatus()
      {
        id = user_id
      });
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_GUILD_LIST);
    }

    public void OnOffline(int user_id)
    {
      foreach (GuildInfoModel.MemberChatStatus memberChatStatus in this.online)
      {
        if (memberChatStatus.id == user_id)
        {
          this.online.Remove(memberChatStatus);
          MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_GUILD_LIST);
          break;
        }
      }
    }
  }

  [Serializable]
  public class MemberChatStatus
  {
    public int id;
  }
}
