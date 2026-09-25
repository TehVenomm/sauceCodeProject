// Decompiled with JetBrains decompiler
// Type: FriendTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FriendTop : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) FriendTop.UI.LBL_FOLLOW_NUM, $"{MonoBehaviourSingleton<FriendManager>.I.followNum.ToString()}/{MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow.ToString()}");
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    this.SetLabelText((Enum) FriendTop.UI.LBL_FOLLOWER_NUM, $"{MonoBehaviourSingleton<FriendManager>.I.followerNum.ToString()}/{constDefine.FRIEND_MAX_FOLLOWER.ToString()}");
    this.SetLabelText((Enum) FriendTop.UI.LBL_BLACK_LIST_NUM, $"{MonoBehaviourSingleton<BlackListManager>.I.GetBlackListUserNum().ToString()}/{constDefine.BLACKLIST_MAX.ToString()}");
    this.SetBadge((Enum) FriendTop.UI.BTN_MUTUAL_FOLLOW_LIST, MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum, (SpriteAlignment) 3, -4, -4, true);
  }

  private void OnQuery_MUTUAL_FOLLOW()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private enum UI
  {
    LBL_FOLLOW_NUM,
    LBL_FOLLOWER_NUM,
    LBL_BLACK_LIST_NUM,
    BTN_MUTUAL_FOLLOW_LIST,
    BTN_MESSAGE,
  }
}
