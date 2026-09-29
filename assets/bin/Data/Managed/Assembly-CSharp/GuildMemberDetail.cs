// Decompiled with JetBrains decompiler
// Type: GuildMemberDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GuildMemberDetail : HomeFriendDetail
{
  private FriendCharaInfo charaInfo;

  public override void Initialize()
  {
    this.charaInfo = GameSection.GetEventData() as FriendCharaInfo;
    base.Initialize();
  }

  public override void SetupFollowButton()
  {
    int num1 = MonoBehaviourSingleton<FriendManager>.I.followNum == MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow ? 1 : 0;
    bool is_visible1 = !this.charaInfo.following;
    bool follower = this.charaInfo.follower;
    this.SetEvent(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, "FOLLOW", 0);
    int num2 = is_visible1 ? 1 : 0;
    if ((num1 & num2) != 0)
    {
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, true);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, false);
      this.SetEvent(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, "INVALID_FOLLOW", 0);
    }
    else
    {
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_FOLLOW, is_visible1);
      this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_UNFOLLOW, !is_visible1);
    }
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.OBJ_BLACKLIST_ROOT, true);
    bool is_visible2 = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(this.charaInfo.userId);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_BLACKLIST_IN, !is_visible2);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.BTN_BLACKLIST_OUT, is_visible2);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_FOLLOW_ARROW, !is_visible2 && !is_visible1);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_FOLLOWER_ARROW, !is_visible2 & follower);
    this.SetActive(this.transRoot, (Enum) QuestFriendDetailBase.UI.SPR_BLACKLIST_ICON, is_visible2);
  }
}
