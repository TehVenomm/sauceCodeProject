// Decompiled with JetBrains decompiler
// Type: GuildInviteFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class GuildInviteFriend : QuestAcceptRoomInviteFriend
{
  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<GuildManager>.I.SendInviteList((Action<bool, GuildInviteCharaInfo[]>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.nowPage = page;
        this.pageNumMax = recv_data == null || recv_data.Length == 0 ? 0 : Mathf.CeilToInt((float) recv_data.Length / 10f);
        this.inviteUsers = (PartyInviteCharaInfo[]) recv_data;
        this.Sort<FriendCharaInfo>(this.GetCurrentUserList());
      }
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return base.GetUpdateUINotifyFlags() | GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
  }

  private void OnQuery_OK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<GuildManager>.I.SendInvite(this.selectedUserIdList.ToArray(), (Action<bool, int[]>) ((is_success, invited_users) => GameSection.ResumeEvent(is_success)));
  }

  private new enum UI
  {
    SPR_TITLE_FOLLOW_LIST,
    SPR_TITLE_FOLLOWER_LIST,
    SPR_TITLE_MESSAGE,
    SPR_TITLE_BLACKLIST,
    OBJ_FOLLOW_NUMBER_ROOT,
    LBL_FOLLOW_NUMBER_NOW,
    LBL_FOLLOW_NUMBER_MAX,
    OBJ_DISABLE_USER_MASK,
    LBL_NAME,
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
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_LEVEL,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    BTN_PAGE_PREV,
    BTN_PAGE_NEXT,
    STR_TITLE,
    STR_TITLE_REFLECT,
    TGL_OK,
    BTN_OK,
    BTN_ALL,
    OBJ_SELECTED,
    OBJ_INVITED,
    OBJ_ROOMCONDITION,
    OBJ_NOT_PROGRESSED,
    LBL_SORT,
  }
}
