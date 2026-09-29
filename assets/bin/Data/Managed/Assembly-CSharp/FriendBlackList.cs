// Decompiled with JetBrains decompiler
// Type: FriendBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class FriendBlackList : FollowListBase
{
  public override void Initialize()
  {
    this.SetActive((Enum) FollowListBase.UI.BTN_SORT, false);
    this.titleType = FollowListBase.TITLE_TYPE.BLACKLIST;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) FollowListBase.UI.OBJ_FOLLOW_NUMBER_ROOT, true);
    this.SetLabelText((Enum) FollowListBase.UI.LBL_FOLLOW_NUMBER_NOW, MonoBehaviourSingleton<BlackListManager>.I.GetBlackListUserNum().ToString());
    this.SetLabelText((Enum) FollowListBase.UI.LBL_FOLLOW_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.BLACKLIST_MAX.ToString());
    this.ListUI();
  }

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<BlackListManager>.I.SendList(page, (Action<bool, BlackListListModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.recvList = recv_data.black;
        this.nowPage = page;
        this.pageNumMax = recv_data.pageNumMax;
      }
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  protected override void PostSendGetListByReopen(int page)
  {
    this.SetDirtyTable();
    base.PostSendGetListByReopen(page);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (GameSection.NOTIFY_FLAG) 0)
      this.isInitializeSendReopen = true;
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirtyTable();
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST;
  }
}
