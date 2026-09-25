// Decompiled with JetBrains decompiler
// Type: FriendSearch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class FriendSearch : FollowListBase
{
  private FriendSearch.SEARCH_TYPE searchType;
  private FriendSearch.SEARCH_TYPE tmpSearchType;
  private string searchName = string.Empty;
  private string searchID = string.Empty;

  public override void Initialize()
  {
    this.isInitializeSend = false;
    this.tmpSearchType = this.searchType;
    base.Initialize();
  }

  public override void UpdateUI() => this.ListUI();

  private void OnQuery_SEARCH_NAME()
  {
    this.tmpSearchType = FriendSearch.SEARCH_TYPE.NAME;
    GameSection.SetEventData((object) 0);
  }

  private void OnQuery_ID_SEARCH() => this.tmpSearchType = FriendSearch.SEARCH_TYPE.ID;

  private void OnQuery_AUTO_SEARCH()
  {
    GameSection.StayEvent();
    this.searchType = FriendSearch.SEARCH_TYPE.AUTO;
    this.SendGetList(0, (Action<bool>) (b =>
    {
      this.SetDirty((Enum) FriendSearch.UI.GRD_LIST);
      this.RefreshUI();
      GameSection.ResumeEvent(b);
    }));
  }

  protected override void SendGetList(int page, Action<bool> callback)
  {
    switch (this.searchType)
    {
      case FriendSearch.SEARCH_TYPE.AUTO:
        MonoBehaviourSingleton<FriendManager>.I.SendSearchLevel(page, (Action<bool, FriendSearchResult>) ((is_success, recv_data) =>
        {
          if (is_success)
          {
            this.recvList = this.ChangeData(recv_data.search);
            this.pageNumMax = recv_data.pageNumMax;
            this.nowPage = page;
          }
          callback(is_success);
        }));
        break;
      case FriendSearch.SEARCH_TYPE.NAME:
        if (string.IsNullOrEmpty(this.searchName))
        {
          callback(false);
          break;
        }
        MonoBehaviourSingleton<FriendManager>.I.SendSearchName(this.searchName, page, (Action<bool, FriendSearchResult>) ((is_success, recv_data) =>
        {
          if (is_success)
          {
            this.recvList = this.ChangeData(recv_data.search);
            this.pageNumMax = recv_data.pageNumMax;
            this.nowPage = page;
          }
          callback(is_success);
        }));
        break;
      case FriendSearch.SEARCH_TYPE.ID:
        if (string.IsNullOrEmpty(this.searchID))
        {
          callback(false);
          break;
        }
        MonoBehaviourSingleton<FriendManager>.I.SendSearchID(this.searchID, (Action<bool, FriendSearchResult>) ((is_success, recv_data) =>
        {
          if (is_success)
          {
            this.recvList = this.ChangeData(recv_data.search);
            this.pageNumMax = recv_data.pageNumMax;
            this.nowPage = page;
          }
          callback(is_success);
        }));
        break;
    }
  }

  private void OnCloseDialog_FriendSearchName() => this.OnCloseSearchDialog();

  private void OnCloseDialog_FriendSearchID() => this.OnCloseSearchDialog();

  private void OnCloseSearchDialog()
  {
    if (!(GameSection.GetEventData() is object[] eventData))
      return;
    this.searchType = this.tmpSearchType;
    if (!(bool) eventData[0])
      return;
    int num = (int) eventData[1];
    FriendSearchResult friendSearchResult = eventData[2] as FriendSearchResult;
    this.recvList = this.ChangeData(friendSearchResult.search);
    this.pageNumMax = friendSearchResult.pageNumMax;
    this.nowPage = num;
    if (this.searchType == FriendSearch.SEARCH_TYPE.ID)
      this.searchID = eventData[3] as string;
    else if (this.searchType == FriendSearch.SEARCH_TYPE.NAME)
      this.searchName = eventData[3] as string;
    this.RefreshUI();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirty((Enum) FriendSearch.UI.GRD_LIST);
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST;
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
  }

  private enum SEARCH_TYPE
  {
    AUTO,
    NAME,
    ID,
  }
}
