using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LoungeMemberInfo : FriendInfo
{
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
		BTN_JOIN
	}

	private LoungeMemberStatus status;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as CharaInfo);
		if (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
		{
			InitializeBase();
			return;
		}
		FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(data.userId);
		if (followLoungeMember == null)
		{
			InitializeBase();
			return;
		}
		dataFollower = followLoungeMember.follower;
		dataFollowing = followLoungeMember.following;
		nowSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		isFollowerList = Object.op_Implicit(Object.FindObjectOfType(typeof(FriendFollowerList)));
		InitializeBase();
	}

	protected override void OnOpen()
	{
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		UpdateLoungeUI();
	}

	private void UpdateLoungeUI()
	{
		if (!CheckExistTarget())
		{
			DispatchEvent("NON_TARGET_PLAYER");
			return;
		}
		int ownerUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetOwnerUserId();
		bool is_visible = ownerUserId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
		SetActive((Enum)UI.BTN_KICK, is_visible);
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
		{
			SetActive((Enum)UI.BTN_JOIN, is_visible: false);
			status = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[data.userId];
			switch (status.GetStatus())
			{
			case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
			case LoungeMemberStatus.MEMBER_STATUS.FIELD:
				SetActive((Enum)UI.BTN_JOIN, is_visible: true);
				break;
			case LoungeMemberStatus.MEMBER_STATUS.QUEST:
				SetActive((Enum)UI.BTN_JOIN, !CheckRush(status.questId));
				break;
			case LoungeMemberStatus.MEMBER_STATUS.ARENA:
				SetActive((Enum)UI.BTN_JOIN, is_visible: false);
				break;
			}
			if (data != null && data.userClanData != null)
			{
				UpdateClanInfo(data);
			}
			else
			{
				DisableClanInfo();
			}
		}
	}

	private bool CheckRush(int questId)
	{
		QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)questId);
		if (questData == null)
		{
			return false;
		}
		if (questData.rushId == 0)
		{
			return false;
		}
		return true;
	}

	private void JoinField(int fieldMapId)
	{
		if (fieldMapId == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
		{
			GameSection.StopEvent();
			return;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)fieldMapId);
		if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
		{
			Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, check_dst_quest: false))
		{
			GameSection.StopEvent();
			return;
		}
		if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
		{
			DispatchEvent("CANT_JUMP");
			return;
		}
		GameSection.StayEvent();
		CoopApp.EnterField(fieldMapData.jumpPortalID, 0u, delegate(bool is_matching, bool is_connect, bool is_regist)
		{
			if (!is_connect)
			{
				GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
				GameSection.ResumeEvent(is_resume: true);
				AppMain i = MonoBehaviourSingleton<AppMain>.I;
				i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
				{
					DispatchEvent("CLOSE");
				});
			}
			else
			{
				GameSection.ResumeEvent(is_regist);
				if (is_regist)
				{
					MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
				}
			}
		});
	}

	private void JoinParty(string partyId)
	{
		EventData[] autoEvents = new EventData[3]
		{
			new EventData("MAIN_MENU_LOUNGE", null),
			new EventData("GACHA_QUEST_COUNTER", null),
			new EventData("JOIN_ROOM", partyId)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private bool CheckExistTarget()
	{
		PartyModel.SlotInfo slotInfoByUserId = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetSlotInfoByUserId(data.userId);
		return slotInfoByUserId != null;
	}

	private void SetFollowLoungeCharaInfo(int userId, bool follow)
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userId).following = follow;
	}

	private void OnQuery_JOIN()
	{
		switch (status.GetStatus())
		{
		case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
		case LoungeMemberStatus.MEMBER_STATUS.QUEST:
			JoinParty(status.partyId);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.FIELD:
			JoinField(status.fieldMapId);
			break;
		}
	}

	protected override void OnQuery_FOLLOW()
	{
		if (!CheckExistTarget())
		{
			GameSection.StopEvent();
			DispatchEvent("NON_TARGET_PLAYER");
			return;
		}
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		List<int> list = new List<int>();
		list.Add(data.userId);
		SendFollow(list, delegate(bool is_success)
		{
			if (is_success)
			{
				dataFollowing = !dataFollowing;
				SetFollowLoungeCharaInfo(data.userId, follow: true);
			}
		});
	}

	protected override void OnQuery_UNFOLLOW()
	{
		if (!CheckExistTarget())
		{
			GameSection.StopEvent();
			DispatchEvent("NON_TARGET_PLAYER");
		}
		else
		{
			GameSection.SetEventData(new object[1]
			{
				data.name
			});
		}
	}

	protected override void OnQuery_FriendUnFollowMessage_YES()
	{
		if (!CheckExistTarget())
		{
			GameSection.StopEvent();
			DispatchEvent("NON_TARGET_PLAYER");
		}
		else
		{
			GameSection.SetEventData(new object[1]
			{
				data.name
			});
			SendUnFollow(data.userId, delegate(bool is_success)
			{
				if (is_success)
				{
					dataFollowing = !dataFollowing;
					SetFollowLoungeCharaInfo(data.userId, follow: false);
				}
			});
		}
	}

	protected override void OnQuery_DELETEFOLLOWER()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
	}

	protected override void OnQuery_FriendDeleteFollowerMessage_YES()
	{
		if (!CheckExistTarget())
		{
			GameSection.StopEvent();
			DispatchEvent("NON_TARGET_PLAYER");
		}
		else
		{
			base.OnQuery_FriendDeleteFollowerMessage_YES();
		}
	}

	protected override void OnQuery_BLACK_LIST_IN()
	{
		if (!CheckExistTarget())
		{
			GameSection.StopEvent();
			DispatchEvent("NON_TARGET_PLAYER");
		}
		else
		{
			base.OnQuery_BLACK_LIST_IN();
		}
	}

	protected override void OnQuery_BLACK_LIST_OUT()
	{
		if (!CheckExistTarget())
		{
			GameSection.StopEvent();
			DispatchEvent("NON_TARGET_PLAYER");
		}
		else
		{
			base.OnQuery_BLACK_LIST_OUT();
		}
	}

	private void OnQuery_LoungeKickConfirm_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			data.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRoomPartyKick(delegate(bool isSuccess)
		{
			if (isSuccess)
			{
				IHomeManager currentIHomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
				currentIHomeManager.IHomePeople.CastToLoungePeople()?.DestroyLoungePlayer(data.userId);
			}
			GameSection.ResumeEvent(isSuccess);
		}, data.userId);
	}
}
