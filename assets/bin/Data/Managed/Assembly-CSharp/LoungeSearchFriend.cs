// Decompiled with JetBrains decompiler
// Type: LoungeSearchFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class LoungeSearchFriend : FollowListBase
{
  private List<LoungeSearchFollowerRoomModel.LoungeFollowerModel> loungeFollowers;
  private LoungeSearchFriend.UI[] members = new LoungeSearchFriend.UI[7]
  {
    LoungeSearchFriend.UI.TGL_MEMBER_1,
    LoungeSearchFriend.UI.TGL_MEMBER_2,
    LoungeSearchFriend.UI.TGL_MEMBER_3,
    LoungeSearchFriend.UI.TGL_MEMBER_4,
    LoungeSearchFriend.UI.TGL_MEMBER_5,
    LoungeSearchFriend.UI.TGL_MEMBER_6,
    LoungeSearchFriend.UI.TGL_MEMBER_7
  };
  private List<int> firstMetUserIds;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI() => this.UpdateListUI();

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendSearchFollowerRoom((Action<bool, List<LoungeSearchFollowerRoomModel.LoungeFollowerModel>, List<int>>) ((isSuccess, lounges, firstMetUserIds) =>
    {
      if (isSuccess)
      {
        this.nowPage = 1;
        this.pageNumMax = 1;
        this.loungeFollowers = lounges;
        this.firstMetUserIds = firstMetUserIds;
      }
      if (callback == null)
        return;
      callback(isSuccess);
    }));
  }

  private void UpdateListUI()
  {
    if (this.loungeFollowers == null || this.loungeFollowers.Count == 0)
    {
      this.SetActive((Enum) LoungeSearchFriend.UI.LBL_NON_LIST, true);
      this.SetActive((Enum) LoungeSearchFriend.UI.GRD_LIST, false);
      this.SetButtonEnabled((Enum) LoungeSearchFriend.UI.BTN_PAGE_PREV, false);
      this.SetButtonEnabled((Enum) LoungeSearchFriend.UI.BTN_PAGE_NEXT, false);
      this.SetLabelText((Enum) LoungeSearchFriend.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) LoungeSearchFriend.UI.LBL_MAX, "0");
    }
    else
    {
      this.SetLabelText((Enum) LoungeSearchFriend.UI.LBL_NOW, (this.nowPage + 1).ToString());
      this.SetActive((Enum) LoungeSearchFriend.UI.LBL_NON_LIST, false);
      this.SetActive((Enum) LoungeSearchFriend.UI.GRD_LIST, true);
      this.SetButtonEnabled((Enum) LoungeSearchFriend.UI.BTN_PAGE_PREV, this.nowPage > 0);
      this.SetButtonEnabled((Enum) LoungeSearchFriend.UI.BTN_PAGE_NEXT, this.nowPage + 1 < this.pageNumMax);
      this.SetDynamicList((Enum) LoungeSearchFriend.UI.GRD_LIST, "LoungeSearchFriendItem", this.loungeFollowers.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetupListItem(this.loungeFollowers[i], i, t)));
    }
  }

  private void SetupListItem(
    LoungeSearchFollowerRoomModel.LoungeFollowerModel data,
    int i,
    Transform t)
  {
    this.SetEvent(t, "JOIN", i);
    this.SetFollowerInfo(data, t);
    this.SetLoungeInfo(data, t);
  }

  private void SetFollowerInfo(
    LoungeSearchFollowerRoomModel.LoungeFollowerModel data,
    Transform t)
  {
    CharaInfo chara_info = (CharaInfo) null;
    for (int index = 0; index < data.slotInfos.Count; ++index)
    {
      if (data.slotInfos[index].userInfo != null && data.slotInfos[index].userInfo.userId == data.followerUserId)
        chara_info = data.slotInfos[index].userInfo;
    }
    this.SetRenderPlayerModel(t, (Enum) LoungeSearchFriend.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(chara_info, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) LoungeSearchFriend.UI.LBL_NAME, chara_info.name);
    this.SetLabelText(t, (Enum) LoungeSearchFriend.UI.LBL_LEVEL, chara_info.level.ToString());
    ((Component) this.FindCtrl(t, (Enum) LoungeSearchFriend.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(chara_info.selectedDegrees, false, (Action<DegreePlate>) (x => ((Component) this.GetCtrl((Enum) LoungeSearchFriend.UI.GRD_LIST)).GetComponent<UIGrid>().Reposition()));
    this.SetActive(t, (Enum) LoungeSearchFriend.UI.SPR_ICON_FIRST_MET, this.CheckFirstMet(chara_info.userId));
    this.SetFollowStatus(t, chara_info.userId, true, true, chara_info.userClanData.cId);
  }

  private bool CheckFirstMet(int userId)
  {
    int index = 0;
    for (int count = this.firstMetUserIds.Count; index < count; ++index)
    {
      if (userId == this.firstMetUserIds[index])
        return true;
    }
    return false;
  }

  private void SetLoungeInfo(
    LoungeSearchFollowerRoomModel.LoungeFollowerModel data,
    Transform t)
  {
    this.SetLabelText(t, (Enum) LoungeSearchFriend.UI.LBL_LOUNGE_NAME, data.name);
    string text = StringTable.Get(STRING_CATEGORY.LOUNGE_LABEL, (uint) data.label);
    this.SetLabelText(t, (Enum) LoungeSearchFriend.UI.LBL_LABEL, text);
    int num1 = data.num + 1;
    int num2 = data.slotInfos.Count<PartyModel.SlotInfo>((Func<PartyModel.SlotInfo, bool>) (slotInfo => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != data.ownerUserId));
    for (int index = 0; index < 7; ++index)
    {
      bool is_visible = index < num1 - 1;
      this.SetActive(t, (Enum) this.members[index], is_visible);
      this.SetToggle(t, (Enum) this.members[index], index < num2);
    }
  }

  private void OnQuery_JOIN()
  {
    LoungeSearchFollowerRoomModel.LoungeFollowerModel loungeFollower = this.loungeFollowers[(int) GameSection.GetEventData()];
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(loungeFollower.id, (Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
  }

  protected new enum UI
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
    OBJ_DEGREE_FRAME_ROOT,
    LBL_LOUNGE_NAME,
    LBL_LABEL,
    TGL_MEMBER_1,
    TGL_MEMBER_2,
    TGL_MEMBER_3,
    TGL_MEMBER_4,
    TGL_MEMBER_5,
    TGL_MEMBER_6,
    TGL_MEMBER_7,
    LBL_NON_LIST,
    SPR_ICON_FIRST_MET,
  }
}
