// Decompiled with JetBrains decompiler
// Type: MutualFollowFBInviteDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class MutualFollowFBInviteDialog : GameSection
{
  private List<FBManager.FriendData> selectedList = new List<FBManager.FriendData>();

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    bool getFriendFinish = false;
    bool getInvitableFriendFinish = false;
    MonoBehaviourSingleton<FBManager>.I.GetInvitableFriends((Action<bool>) (success =>
    {
      getInvitableFriendFinish = true;
      MonoBehaviourSingleton<FBManager>.I.GetFriends((Action<bool>) (f_success => getFriendFinish = true));
    }));
    while (!getInvitableFriendFinish || !getFriendFinish)
      yield return (object) null;
    this.SetInput((Enum) MutualFollowFBInviteDialog.UI.IPT_NAME, string.Empty, 0, (EventDelegate.Callback) (() => this.SetWrapContentFilterText((Enum) MutualFollowFBInviteDialog.UI.WRP_LIST, this.GetInputValue((Enum) MutualFollowFBInviteDialog.UI.IPT_NAME))));
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this._UpdateListFriend(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo);
    this._UpdateNumFriend(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo, MonoBehaviourSingleton<FBManager>.I.friendInfo);
    this._UpdateSelected();
  }

  private void _UpdateListFriend(
    FBManager.InvitableFriendInfo invitable_friend_info)
  {
    List<FBManager.FriendData> friendList = invitable_friend_info.data;
    int j = 0;
    this.SetWrapContentFilter((Enum) MutualFollowFBInviteDialog.UI.WRP_LIST, "MutualFollowFBInviteListItem", friendList.Count, false, (Action<int, Transform, bool>) ((i, t, b) =>
    {
      FBManager.FriendData friendData = friendList[j++];
      this.SetLabelText(t, (Enum) MutualFollowFBInviteDialog.UI.LBL_NAME, friendData.name);
      this.SetDownloadTexture(t, (Enum) MutualFollowFBInviteDialog.UI.TEX_AVATAR, friendData.picture.data.url);
      this.SetEvent(t, "SELECT", i);
    }), (Func<int, string, bool>) ((i, s) =>
    {
      FBManager.FriendData friendData = friendList[i];
      return friendData.name.ContainIgnoreCase(s) || s.ContainIgnoreCase(friendData.name);
    }));
  }

  private void _UpdateSelected()
  {
    this.SetLabelText((Enum) MutualFollowFBInviteDialog.UI.LBL_SELECTED, string.Format(this.sectionData.GetText("FRIEND_SELECTED"), (object) this.selectedList.Count));
  }

  private void _UpdateNumFriend(
    FBManager.InvitableFriendInfo invitable_friend_info,
    FBManager.FriendInfo friend_info)
  {
    List<FBManager.FriendData> data = friend_info.data;
    this.SetLabelText((Enum) MutualFollowFBInviteDialog.UI.LBL_SELECTED_NUM, string.Format(this.sectionData.GetText("FRIEND_NUM"), (object) friend_info.data.Count, (object) invitable_friend_info.data.Count));
  }

  private void OnQuery_SELECT()
  {
    int eventData = (int) GameSection.GetEventData();
    Transform child = this.GetCtrl((Enum) MutualFollowFBInviteDialog.UI.WRP_LIST).GetChild(eventData);
    this.SetActive(child, (Enum) MutualFollowFBInviteDialog.UI.OBJ_SELECT, true);
    FBManager.FriendData friendData = MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data[eventData];
    if (this.selectedList.Contains(friendData))
    {
      this.selectedList.Remove(friendData);
      this.SetActive(child, (Enum) MutualFollowFBInviteDialog.UI.OBJ_SELECT, false);
    }
    else
    {
      this.selectedList.Add(friendData);
      this.SetActive(child, (Enum) MutualFollowFBInviteDialog.UI.OBJ_SELECT, true);
    }
    this._UpdateSelected();
  }

  private void OnQuery_SELECTALL()
  {
    int count1 = this.selectedList.Count;
    this.selectedList.Clear();
    bool is_visible = false;
    int count2 = MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data.Count;
    if (count1 < count2)
    {
      this.selectedList.AddRange((IEnumerable<FBManager.FriendData>) MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data);
      is_visible = true;
    }
    foreach (Transform root in this.GetCtrl((Enum) MutualFollowFBInviteDialog.UI.WRP_LIST))
      this.SetActive(root, (Enum) MutualFollowFBInviteDialog.UI.OBJ_SELECT, is_visible);
    this._UpdateSelected();
  }

  private void OnQuery_INVITE()
  {
    List<string> list = this.selectedList.Select<FBManager.FriendData, string>((Func<FBManager.FriendData, string>) (o => o.id)).ToList<string>();
    if (list.Count > 0)
    {
      MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("invite_friend", "Social", new Dictionary<string, object>()
      {
        {
          "amount",
          (object) list.Count
        }
      });
      GameSection.ChangeEvent("INVITE_SUCCESS");
      GameSection.StayEvent();
      MonoBehaviourSingleton<FBManager>.I.AppRequest(this.sectionData.GetText("INVITE_MESSAGE"), list, "", "", (Action<bool, FBManager.AppRequestResult>) ((req_success, ret) =>
      {
        if (req_success)
        {
          MonoBehaviourSingleton<AccountManager>.I.SendTrackInviteFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, ((IEnumerable<string>) ret.to.Split(',')).ToList<string>(), (Action<bool>) (success =>
          {
            if (!success)
              GameSection.ChangeStayEvent("TRACK_FAIL");
            GameSection.ResumeEvent(true);
          }));
        }
        else
        {
          GameSection.ChangeStayEvent("INVITE_FAIL");
          GameSection.ResumeEvent(true);
        }
      }));
    }
    else
      GameSection.ChangeEvent("INVITE_NONE");
  }

  private enum UI
  {
    WRP_LIST,
    BTN_SELECTALL,
    BTN_INVITE,
    IPT_NAME,
    LBL_SELECTED,
    LBL_SELECTED_NUM,
    LBL_NAME,
    TEX_AVATAR,
    OBJ_SELECT,
  }
}
