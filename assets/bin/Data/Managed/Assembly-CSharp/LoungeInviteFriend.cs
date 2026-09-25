// Decompiled with JetBrains decompiler
// Type: LoungeInviteFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class LoungeInviteFriend : QuestAcceptRoomInviteFriend
{
  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInviteList((Action<bool, LoungeInviteCharaInfo[]>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.nowPage = 0;
        this.pageNumMax = recv_data != null ? Mathf.CeilToInt((float) recv_data.Length / 10f) : 1;
        this.inviteUsers = (PartyInviteCharaInfo[]) recv_data;
        this.SortArray();
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
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInvite(this.selectedUserIdList.ToArray(), (Action<bool, int[]>) ((is_success, invited_users) => GameSection.ResumeEvent(is_success)));
  }

  protected override void SetupListItem(
    PartyInviteCharaInfo info,
    int i,
    Transform t,
    bool is_recycle)
  {
    base.SetupListItem(info, i, t, is_recycle);
    bool is_visible1 = (int) info.level < 15;
    this.SetActive(t, (Enum) LoungeInviteFriend.UI.OBJ_CANT_LOUNGE, is_visible1);
    bool is_visible2 = this.IsUserInSameLounge(info);
    this.SetActive(t, (Enum) LoungeInviteFriend.UI.OBJ_IN_LOUNGE, is_visible2);
    if (is_visible1 | is_visible2)
    {
      this.SetActive(t, (Enum) LoungeInviteFriend.UI.OBJ_INVITED, false);
      this.SetActive(t, (Enum) LoungeInviteFriend.UI.OBJ_NOT_PROGRESSED, false);
      this.SetActive(t, (Enum) LoungeInviteFriend.UI.OBJ_ROOMCONDITION, false);
    }
    this.SetButtonEnabled(t, !info.invite && info.canEntry && !is_visible2 && !is_visible1);
    if (!LoungeMatchingManager.IsValidInLounge())
      return;
    this.SetActive(t, (Enum) LoungeInviteFriend.UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(info.userId));
  }

  protected override bool IsEnableInvite(PartyInviteCharaInfo _info)
  {
    return base.IsEnableInvite(_info) && (int) _info.level >= 15 && !this.IsUserInSameLounge(_info);
  }

  private bool IsUserInSameLounge(PartyInviteCharaInfo _info)
  {
    if (_info == null)
      return false;
    LoungeModel.Lounge loungeData = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData;
    if (loungeData == null)
      return false;
    for (int index = 0; index < loungeData.slotInfos.Count; ++index)
    {
      if (loungeData.slotInfos[index].userInfo != null && loungeData.slotInfos[index].userInfo.userId == _info.userId)
        return true;
    }
    return false;
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
    GRD_FOLLOW_ARROW,
    OBJ_FOLLOW,
    SPR_FOLLOW,
    SPR_FOLLOWER,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
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
    LBL_SELECT_ALL,
    TGL_OK,
    BTN_OK,
    BTN_ALL,
    OBJ_SELECTED,
    OBJ_INVITED,
    OBJ_ROOMCONDITION,
    OBJ_NOT_PROGRESSED,
    LBL_SORT,
    SPR_ICON_FIRST_MET,
    OBJ_CANT_LOUNGE,
    OBJ_IN_LOUNGE,
  }
}
