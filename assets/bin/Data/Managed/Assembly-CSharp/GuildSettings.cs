// Decompiled with JetBrains decompiler
// Type: GuildSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class GuildSettings : GameSection
{
  public override void Initialize()
  {
    base.Initialize();
    if (MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
    {
      this.SetActive((Enum) GuildSettings.UI.BTN_SETTING, true);
      this.SetActive((Enum) GuildSettings.UI.BTN_DELETE, true);
      this.SetActive((Enum) GuildSettings.UI.BTN_LEAVE, false);
    }
    else
    {
      this.SetActive((Enum) GuildSettings.UI.BTN_SETTING, false);
      this.SetActive((Enum) GuildSettings.UI.BTN_DELETE, false);
      this.SetActive((Enum) GuildSettings.UI.BTN_LEAVE, true);
    }
    if (MonoBehaviourSingleton<GuildManager>.I.guildData.privacy == 2)
    {
      if (MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
        this.SetActive((Enum) GuildSettings.UI.BTN_INVITE, true);
      else
        this.SetActive((Enum) GuildSettings.UI.BTN_INVITE, false);
    }
    else
      this.SetActive((Enum) GuildSettings.UI.BTN_INVITE, true);
    this.SetActive((Enum) GuildSettings.UI.SPR_BADGE, false);
    this.UpdateBadge();
  }

  private void UpdateBadge()
  {
    if (MonoBehaviourSingleton<GuildManager>.I.guildData == null)
      return;
    MonoBehaviourSingleton<GuildManager>.I.SendMemberList(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, (Action<bool, GuildMemberListModel>) ((success, ret) =>
    {
      List<FriendCharaInfo> source = new List<FriendCharaInfo>((IEnumerable<FriendCharaInfo>) ret.result.requesters);
      source.Remove(source.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)));
      this.SetActive(this.FindCtrl(this._transform, (Enum) GuildSettings.UI.BTN_MEMBER), (Enum) GuildSettings.UI.SPR_BADGE, source.Count > 0);
    }));
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) GuildSettings.UI.LBL_GUILD_ID, $"{MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId:D5}");
  }

  private void OnQuery_MESSAGE() => MonoBehaviourSingleton<GuildManager>.I.EmptyTalkUser();

  private void OnQuery_GuildDeleteConfirm_YES()
  {
    MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDelete((Action<bool, Error>) ((is_success, err) =>
    {
      if (is_success)
        MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_GuildLeaveConfirm_YES()
  {
    MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendLeave((Action<bool, Error>) ((is_success, err) =>
    {
      if (is_success)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_EQUIP_CHANGE);
        MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
      }
      GameSection.ResumeEvent(is_success);
    }));
  }

  private void OnQuery_EXIT() => MonoBehaviourSingleton<GuildManager>.I.IsEnterGuild = false;

  private void OnQuery_LEAVE()
  {
    GameSection.SetEventData((object) new CommonDialog.Desc(CommonDialog.TYPE.DECLINE_COMFIRM, "message"));
  }

  private void OnQuery_DELETE()
  {
    GameSection.SetEventData((object) new CommonDialog.Desc(CommonDialog.TYPE.DECLINE_COMFIRM, "message"));
  }

  private enum UI
  {
    LBL_GUILD_ID,
    BTN_LEAVE,
    BTN_DELETE,
    BTN_INVITE,
    BTN_SETTING,
    BTN_MEMBER,
    SPR_BADGE,
  }
}
