// Decompiled with JetBrains decompiler
// Type: FriendArenaRankingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class FriendArenaRankingInfo : FriendInfo
{
  private Network.EventData eventData;

  public override void Initialize()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    this.friendCharaInfo = eventData[0] as FriendCharaInfo;
    this.data = eventData[0] as CharaInfo;
    this.eventData = eventData[1] as Network.EventData;
    if (this.friendCharaInfo != null)
    {
      this.dataFollower = this.friendCharaInfo.follower;
      this.dataFollowing = this.friendCharaInfo.following;
    }
    this.nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    this.isFollowerList = Object.op_Implicit(Object.FindObjectOfType(typeof (FriendFollowerList)));
    this.InitializeBase();
  }

  protected override void OnOpen()
  {
  }

  private void OnQuery_SCORE()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.data.userId
    });
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    if (this.data.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      return;
    this.SetActive((Enum) FriendArenaRankingInfo.UI.BTN_FOLLOW, false);
    this.SetActive((Enum) FriendArenaRankingInfo.UI.BTN_UNFOLLOW, false);
    this.SetActive((Enum) FriendArenaRankingInfo.UI.OBJ_BLACKLIST_ROOT, false);
    this.SetActive((Enum) FriendArenaRankingInfo.UI.OBJ_BLACKLIST_ROOT, false);
    this.SetActive((Enum) FriendArenaRankingInfo.UI.BTN_BLACKLIST_IN, false);
    this.SetActive((Enum) FriendArenaRankingInfo.UI.BTN_BLACKLIST_OUT, false);
  }

  protected new enum UI
  {
    LBL_NAME,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    SPR_COMMENT,
    LBL_COMMENT,
    OBJ_LAST_LOGIN,
    LBL_LAST_LOGIN,
    LBL_LAST_LOGIN_TIME,
    LBL_LEVEL,
    OBJ_LEVEL_ROOT,
    LBL_USER_ID,
    OBJ_USER_ID_ROOT,
    TEX_MODEL,
    BTN_FOLLOW,
    BTN_UNFOLLOW,
    OBJ_BLACKLIST_ROOT,
    BTN_BLACKLIST_IN,
    BTN_BLACKLIST_OUT,
    OBJ_ICON_WEAPON_1,
    OBJ_ICON_WEAPON_2,
    OBJ_ICON_WEAPON_3,
    OBJ_ICON_ARMOR,
    OBJ_ICON_HELM,
    OBJ_ICON_ARM,
    OBJ_ICON_LEG,
    BTN_ICON_WEAPON_1,
    BTN_ICON_WEAPON_2,
    BTN_ICON_WEAPON_3,
    BTN_ICON_ARMOR,
    BTN_ICON_HELM,
    BTN_ICON_ARM,
    BTN_ICON_LEG,
    OBJ_EQUIP_ROOT,
    OBJ_EQUIP_SET_ROOT,
    OBJ_FRIEND_INFO_ROOT,
    OBJ_CHANGE_EQUIP_INFO_ROOT,
    LBL_MAX,
    LBL_NOW,
    OBJ_FOLLOW_ARROW_ROOT,
    SPR_FOLLOW_ARROW,
    SPR_FOLLOWER_ARROW,
    SPR_BLACKLIST_ICON,
    SPR_SAME_CLAN_ICON,
    LBL_LEVEL_WEAPON_1,
    LBL_LEVEL_WEAPON_2,
    LBL_LEVEL_WEAPON_3,
    LBL_LEVEL_ARMOR,
    LBL_LEVEL_HELM,
    LBL_LEVEL_ARM,
    LBL_LEVEL_LEG,
    LBL_CHANGE_MODE,
    BTN_MAGI,
    LBL_SET_NAME,
    OBJ_DEGREE_PLATE_ROOT,
    BTN_DELETEFOLLOWER,
    BTN_KICK,
    BTN_JOIN,
  }
}
