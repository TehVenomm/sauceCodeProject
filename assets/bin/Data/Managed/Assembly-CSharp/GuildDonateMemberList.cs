// Decompiled with JetBrains decompiler
// Type: GuildDonateMemberList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class GuildDonateMemberList : GuildMemberList
{
  private DonateInfo _info;
  private List<int> _invitedList;

  public override string ListItemEvent => "SELECT";

  public override void Initialize()
  {
    this._info = GameSection.GetEventData() as DonateInfo;
    base.Initialize();
  }

  protected override void GetListItem(Action<bool, object> callback)
  {
    base.GetListItem((Action<bool, object>) ((success, obj) => MonoBehaviourSingleton<GuildManager>.I.SendDonateInviteList(this._info.id, (Action<bool, GuildDonate.GuildDonateInviteListModel>) ((donate_success, ret) =>
    {
      this.allMember = new List<FriendCharaInfo>((IEnumerable<FriendCharaInfo>) ret.result.list);
      this.allMember.Remove(this.members.FirstOrDefault<FriendCharaInfo>((Func<FriendCharaInfo, bool>) (o => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)));
      this._invitedList = ret.result.donate_list.Where<GuildDonate.GuildDonateInviteListModel.DonateListInfo>((Func<GuildDonate.GuildDonateInviteListModel.DonateListInfo, bool>) (user => user.isInvited)).Select<GuildDonate.GuildDonateInviteListModel.DonateListInfo, int>((Func<GuildDonate.GuildDonateInviteListModel.DonateListInfo, int>) (x => x.userId)).ToList<int>();
      callback(success, (object) null);
    }))));
  }

  protected override void SetListItem(
    int i,
    Transform t,
    string event_name,
    FriendCharaInfo member)
  {
    this.SetActive(t, (Enum) GuildDonateMemberList.UI.OBJ_OFFLINE_MASK, false);
    bool is_visible = this._invitedList.Contains(member.userId);
    this.SetActive(t, (Enum) GuildDonateMemberList.UI.OBJ_SELECTED, is_visible);
    this.SetButtonEnabled(t, !is_visible);
    if (is_visible)
      return;
    this.SetEvent(t, this.ListItemEvent, (object) new object[2]
    {
      (object) i,
      (object) member
    });
  }

  private void OnQuery_SELECT()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    int index = (int) eventData[0];
    FriendCharaInfo member = eventData[1] as FriendCharaInfo;
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendDonateInvite(this._info.id, member.userId, (Action<bool>) (success =>
    {
      if (success)
      {
        Transform child = this.GetCtrl((Enum) GuildDonateMemberList.UI.GRD_LIST).GetChild(index).GetChild(0);
        this.SetActive(child, (Enum) GuildDonateMemberList.UI.OBJ_SELECTED, true);
        this.SetButtonEnabled(child, false);
        this._invitedList.Remove(member.userId);
      }
      GameSection.ResumeEvent(false);
    }));
  }

  private void OnQuery_INVITE()
  {
  }

  protected new enum UI
  {
    OBJ_GUILD_NUMBER_ROOT,
    LBL_GUILD_NUMBER_NOW,
    LBL_GUILD_NUMBER_MAX,
    GRD_LIST,
    TEX_MODEL,
    STR_NON_LIST,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    OBJ_COMMENT,
    LBL_COMMENT,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    OBJ_DEGREE_FRAME_ROOT,
    SPR_ICON_FIRST_MET,
    OBJ_OFFLINE_MASK,
    OBJ_SELECTED,
    OBJ_REQUEST_PENDING,
  }
}
