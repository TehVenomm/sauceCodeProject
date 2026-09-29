// Decompiled with JetBrains decompiler
// Type: HomeFriendDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HomeFriendDetail : QuestRoomUserInfoDetail
{
  private FriendCharaInfo charaInfo;

  protected override bool IsRoomObserve() => false;

  public override void Initialize()
  {
    this.charaInfo = GameSection.GetEventData() as FriendCharaInfo;
    InGameRecorder.PlayerRecord playerRecord = new InGameRecorder.PlayerRecord();
    playerRecord.id = this.charaInfo.userId;
    playerRecord.isNPC = false;
    playerRecord.isSelf = false;
    playerRecord.playerLoadInfo = PlayerLoadInfo.FromCharaInfo((CharaInfo) this.charaInfo, true, true, true, true);
    playerRecord.animID = PLAYER_ANIM_TYPE.GetStatus(this.charaInfo.sex);
    playerRecord.charaInfo = (CharaInfo) this.charaInfo;
    GameSection.SetEventData((object) new object[3]
    {
      (object) playerRecord,
      (object) false,
      (object) false
    });
    base.Initialize();
  }

  public override int GetCharaSex() => this.charaInfo.sex;

  public override void SetupCommentText()
  {
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_COMMENT, true);
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_COMMENT, this.charaInfo.comment);
  }

  public override void SetupFollowButton()
  {
    FriendCharaInfo friendCharaInfo = MonoBehaviourSingleton<FriendManager>.I.homeCharas.chara.Find((Predicate<FriendCharaInfo>) (chara => chara.userId == this.charaInfo.userId));
    bool flag1 = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow;
    bool is_visible = !friendCharaInfo.following;
    bool following = friendCharaInfo.following;
    bool follower = friendCharaInfo.follower;
    this.SetEvent(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, "FOLLOW", 0);
    if (flag1 & is_visible)
    {
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, true);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, false);
      this.SetEvent(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
    }
    else
    {
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, is_visible);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, !is_visible);
    }
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_BLACKLIST_ROOT, true);
    bool flag2 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(this.charaInfo.userId);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_BLACKLIST_IN, !flag2);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_BLACKLIST_OUT, flag2);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_FOLLOW_ARROW, !flag2 && !is_visible);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_FOLLOWER_ARROW, !flag2 & follower);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_BLACKLIST_ICON, flag2);
    bool same_clan_user = false;
    if (this.record != null && this.record.charaInfo != null && this.record.charaInfo.userClanData != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
      same_clan_user = this.record.charaInfo.userClanData.cId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId;
    this.SetFollowStatus(following, follower, flag2, same_clan_user);
  }

  protected override void SetupLastLogin()
  {
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_LAST_LOGIN, true);
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_LAST_LOGIN_TIME, this.charaInfo.lastLogin);
  }

  protected override void UpdateUserIDLabel()
  {
    this.SetLabelText(this.transRoot, (Enum) QuestFriendDetailBase.UI.LBL_USER_ID, this.charaInfo.code);
  }

  protected override void OnQuery_FOLLOW()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.charaInfo.name
    });
    this.SendFollow(new List<int>()
    {
      this.charaInfo.userId
    }, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(this.charaInfo.userId, true);
    }));
  }

  protected override void OnQuery_UNFOLLOW()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.charaInfo.name
    });
  }

  protected virtual void OnQuery_HomeFriendUnFollowMessage_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.charaInfo.name
    });
    this.SendUnFollow(this.charaInfo.userId, (Action<bool>) (is_success =>
    {
      if (!is_success)
        return;
      MonoBehaviourSingleton<FriendManager>.I.SetFollowToHomeCharaInfo(this.charaInfo.userId, false);
    }));
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM;
  }

  private new void OnEnable()
  {
    InputManager.OnDragAlways += new InputManager.OnTouchDelegate(this.OnDrag);
  }

  private new void OnDisable()
  {
    InputManager.OnDragAlways -= new InputManager.OnTouchDelegate(this.OnDrag);
    this.nowSectionName = string.Empty;
  }

  private void OnDrag(InputManager.TouchInfo touch_info)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null) || MonoBehaviourSingleton<UIManager>.I.IsDisable() || !this.CanRotateSection())
      return;
    ((Component) this.loader).transform.Rotate(GameDefine.GetCharaRotateVector(touch_info));
  }

  private bool CanRotateSection()
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nameof (HomeFriendDetail);
  }
}
