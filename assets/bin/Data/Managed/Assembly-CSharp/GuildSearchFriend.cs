// Decompiled with JetBrains decompiler
// Type: GuildSearchFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GuildSearchFriend : FollowListBase
{
  private List<GuildSearchFollowerRoomModel.GuildFollowerModel> clanFollowers;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI() => this.UpdateListUI();

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<GuildManager>.I.SendSearchFollowerRoom((Action<bool, List<GuildSearchFollowerRoomModel.GuildFollowerModel>>) ((isSuccess, clans) =>
    {
      if (isSuccess)
      {
        this.nowPage = 1;
        this.pageNumMax = 1;
        this.clanFollowers = clans;
      }
      if (callback == null)
        return;
      callback(isSuccess);
    }));
  }

  private void UpdateListUI()
  {
    if (this.clanFollowers == null || this.clanFollowers.Count == 0)
    {
      this.SetActive((Enum) GuildSearchFriend.UI.LBL_NON_LIST, true);
      this.SetActive((Enum) GuildSearchFriend.UI.GRD_LIST, false);
      this.SetButtonEnabled((Enum) GuildSearchFriend.UI.BTN_PAGE_PREV, false);
      this.SetButtonEnabled((Enum) GuildSearchFriend.UI.BTN_PAGE_NEXT, false);
      this.SetLabelText((Enum) GuildSearchFriend.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) GuildSearchFriend.UI.LBL_MAX, "0");
    }
    else
    {
      this.SetLabelText((Enum) GuildSearchFriend.UI.LBL_NOW, (this.nowPage + 1).ToString());
      this.SetActive((Enum) GuildSearchFriend.UI.LBL_NON_LIST, false);
      this.SetActive((Enum) GuildSearchFriend.UI.GRD_LIST, true);
      this.SetButtonEnabled((Enum) GuildSearchFriend.UI.BTN_PAGE_PREV, this.nowPage > 0);
      this.SetButtonEnabled((Enum) GuildSearchFriend.UI.BTN_PAGE_NEXT, this.nowPage + 1 < this.pageNumMax);
      this.SetDynamicList((Enum) GuildSearchFriend.UI.GRD_LIST, "GuildSearchFriendItem", this.clanFollowers.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetupListItem(this.clanFollowers[i], i, t)));
    }
  }

  private void SetupListItem(
    GuildSearchFollowerRoomModel.GuildFollowerModel data,
    int i,
    Transform t)
  {
    this.SetEvent(t, "GUILD_INFO", data.clanData.clanId);
    this.SetFollowerInfo(data, t);
    this.SetLoungeInfo(data, t);
  }

  private void SetFollowerInfo(
    GuildSearchFollowerRoomModel.GuildFollowerModel data,
    Transform t)
  {
    this.SetRenderPlayerModel(t, (Enum) GuildSearchFriend.UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo((CharaInfo) data.charInfo, false, true, false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
    this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_NAME, data.charInfo.name);
    this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_LEVEL, data.charInfo.level.ToString());
    ((Component) this.FindCtrl(t, (Enum) GuildSearchFriend.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(data.charInfo.selectedDegrees, false, (Action<DegreePlate>) (x => ((Component) this.GetCtrl((Enum) GuildSearchFriend.UI.GRD_LIST)).GetComponent<UIGrid>().Reposition()));
  }

  private void SetLoungeInfo(
    GuildSearchFollowerRoomModel.GuildFollowerModel data,
    Transform t)
  {
    this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_LOUNGE_NAME, data.clanData.name);
    if (data.clanData.privacy == 0)
      this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_LABEL, "PUBLIC");
    else if (data.clanData.privacy == 1)
      this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_LABEL, "PRIVATE");
    else
      this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_LABEL, "CLOSE");
    if (data.clanData.currentMem != data.clanData.memCap)
      this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_MEMBER_NUM, $"{data.clanData.currentMem.ToString()}/{data.clanData.memCap.ToString()}");
    else
      this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_MEMBER_NUM, "Full");
    this.SetLabelText(t, (Enum) GuildSearchFriend.UI.LBL_CLAN_LV, "Lv " + data.clanData.level.ToString());
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
    LBL_MEMBER_NUM,
    LBL_CLAN_LV,
  }
}
