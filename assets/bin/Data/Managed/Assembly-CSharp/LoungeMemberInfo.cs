// Decompiled with JetBrains decompiler
// Type: LoungeMemberInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class LoungeMemberInfo : FriendInfo
{
  private LoungeMemberStatus status;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as CharaInfo;
    if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
    {
      this.InitializeBase();
    }
    else
    {
      FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(this.data.userId);
      if (followLoungeMember == null)
      {
        this.InitializeBase();
      }
      else
      {
        this.dataFollower = followLoungeMember.follower;
        this.dataFollowing = followLoungeMember.following;
        this.nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
        this.isFollowerList = Object.op_Implicit(Object.FindObjectOfType(typeof (FriendFollowerList)));
        this.InitializeBase();
      }
    }
  }

  protected override void OnOpen()
  {
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.UpdateLoungeUI();
  }

  private void UpdateLoungeUI()
  {
    if (!this.CheckExistTarget())
    {
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
    {
      this.SetActive((Enum) LoungeMemberInfo.UI.BTN_KICK, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetOwnerUserId() == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
      if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
        return;
      this.SetActive((Enum) LoungeMemberInfo.UI.BTN_JOIN, false);
      this.status = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[this.data.userId];
      switch (this.status.GetStatus())
      {
        case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
        case LoungeMemberStatus.MEMBER_STATUS.FIELD:
          this.SetActive((Enum) LoungeMemberInfo.UI.BTN_JOIN, true);
          break;
        case LoungeMemberStatus.MEMBER_STATUS.QUEST:
          this.SetActive((Enum) LoungeMemberInfo.UI.BTN_JOIN, !this.CheckRush(this.status.questId));
          break;
        case LoungeMemberStatus.MEMBER_STATUS.ARENA:
          this.SetActive((Enum) LoungeMemberInfo.UI.BTN_JOIN, false);
          break;
      }
      if (this.data != null && this.data.userClanData != null)
        this.UpdateClanInfo(this.data);
      else
        this.DisableClanInfo();
    }
  }

  private bool CheckRush(int questId)
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint) questId);
    return questData != null && questData.rushId != 0U;
  }

  private void JoinField(int fieldMapId)
  {
    if ((long) fieldMapId == (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
    {
      GameSection.StopEvent();
    }
    else
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) fieldMapId);
      if (fieldMapData == null || fieldMapData.jumpPortalID == 0U)
        Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
      else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, false))
        GameSection.StopEvent();
      else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
      {
        this.DispatchEvent("CANT_JUMP");
      }
      else
      {
        GameSection.StayEvent();
        CoopApp.EnterField(fieldMapData.jumpPortalID, 0U, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
        {
          if (!is_connect)
          {
            GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
            GameSection.ResumeEvent(true);
            MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.DispatchEvent("CLOSE"));
          }
          else
          {
            GameSection.ResumeEvent(is_regist);
            if (!is_regist)
              return;
            MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
          }
        }));
      }
    }
  }

  private void JoinParty(string partyId)
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
    {
      new EventData("MAIN_MENU_LOUNGE", (object) null),
      new EventData("GACHA_QUEST_COUNTER", (object) null),
      new EventData("JOIN_ROOM", (object) partyId)
    });
  }

  private bool CheckExistTarget()
  {
    return MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(this.data.userId) != null;
  }

  private void SetFollowLoungeCharaInfo(int userId, bool follow)
  {
    MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userId).following = follow;
  }

  private void OnQuery_JOIN()
  {
    switch (this.status.GetStatus())
    {
      case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
      case LoungeMemberStatus.MEMBER_STATUS.QUEST:
        this.JoinParty(this.status.partyId);
        break;
      case LoungeMemberStatus.MEMBER_STATUS.FIELD:
        this.JoinField(this.status.fieldMapId);
        break;
    }
  }

  protected override void OnQuery_FOLLOW()
  {
    if (!this.CheckExistTarget())
    {
      GameSection.StopEvent();
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
    {
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.data.name
      });
      this.SendFollow(new List<int>() { this.data.userId }, (Action<bool>) (is_success =>
      {
        if (!is_success)
          return;
        this.dataFollowing = !this.dataFollowing;
        this.SetFollowLoungeCharaInfo(this.data.userId, true);
      }));
    }
  }

  protected override void OnQuery_UNFOLLOW()
  {
    if (!this.CheckExistTarget())
    {
      GameSection.StopEvent();
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.data.name
      });
  }

  protected override void OnQuery_FriendUnFollowMessage_YES()
  {
    if (!this.CheckExistTarget())
    {
      GameSection.StopEvent();
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
    {
      GameSection.SetEventData((object) new object[1]
      {
        (object) this.data.name
      });
      this.SendUnFollow(this.data.userId, (Action<bool>) (is_success =>
      {
        if (!is_success)
          return;
        this.dataFollowing = !this.dataFollowing;
        this.SetFollowLoungeCharaInfo(this.data.userId, false);
      }));
    }
  }

  protected override void OnQuery_DELETEFOLLOWER()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
  }

  protected override void OnQuery_FriendDeleteFollowerMessage_YES()
  {
    if (!this.CheckExistTarget())
    {
      GameSection.StopEvent();
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
      base.OnQuery_FriendDeleteFollowerMessage_YES();
  }

  protected override void OnQuery_BLACK_LIST_IN()
  {
    if (!this.CheckExistTarget())
    {
      GameSection.StopEvent();
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
      base.OnQuery_BLACK_LIST_IN();
  }

  protected override void OnQuery_BLACK_LIST_OUT()
  {
    if (!this.CheckExistTarget())
    {
      GameSection.StopEvent();
      this.DispatchEvent("NON_TARGET_PLAYER");
    }
    else
      base.OnQuery_BLACK_LIST_OUT();
  }

  private void OnQuery_LoungeKickConfirm_YES()
  {
    GameSection.SetEventData((object) new object[1]
    {
      (object) this.data.name
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRoomPartyKick((Action<bool>) (isSuccess =>
    {
      if (isSuccess)
        GameSceneGlobalSettings.GetCurrentIHomeManager().IHomePeople.CastToLoungePeople()?.DestroyLoungePlayer(this.data.userId);
      GameSection.ResumeEvent(isSuccess);
    }), this.data.userId);
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
