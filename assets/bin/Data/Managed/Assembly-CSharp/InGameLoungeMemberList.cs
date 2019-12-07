using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameLoungeMemberList : GameSection
{
	private enum UI
	{
		FRAME,
		GRD_LIST,
		STR_NON_LIST,
		SCR_LIST,
		LBL_NAME,
		LBL_LEVEL,
		LBL_HP,
		LBL_ATK,
		LBL_DEF,
		LBL_COMMENT,
		GRD_FOLLOW_ARROW,
		OBJ_FOLLOW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
		BTN_FOLLOW,
		BTN_BLACK_LIST,
		SPR_ICON_FIRST_MET,
		PORTRAIT_FRAME,
		PORTRAIT_LIST,
		LANDSCAPE_FRAME,
		LANDSCAPE_LIST,
		SPR_ICON_HOST,
		BTN_KICK,
		OBJ_LOUNGE,
		OBJ_QUEST,
		OBJ_FIELD,
		BTN_JOIN,
		LBL_AREA_NAME,
		LBL_QUEST_NAME,
		LBL_PLAYING_QUEST,
		LBL_PLAYING_READY
	}

	private List<CharaInfo> memberInfo;

	public override void Initialize()
	{
		memberInfo = new List<CharaInfo>(8);
		UpdateMemberList();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		int count = memberInfo.Count;
		if (count <= 0)
		{
			SetActive(UI.GRD_LIST, is_visible: false);
			SetActive(UI.STR_NON_LIST, is_visible: true);
		}
		else
		{
			SetGrid(UI.GRD_LIST, "InGameLoungeMemberListItem", count, reset: false, delegate(int i, Transform t, bool isRecycle)
			{
				SetupListItem(memberInfo[i], i, t);
			});
			SetActive(UI.STR_NON_LIST, is_visible: false);
		}
	}

	public override void Exit()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		base.Exit();
	}

	private void SetupListItem(CharaInfo userInfo, int i, Transform t)
	{
		FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
		SetLabelText(t, UI.LBL_NAME, userInfo.name);
		SetLabelText(t, UI.LBL_LEVEL, userInfo.level.ToString());
		string clanId = (userInfo.userClanData != null) ? userInfo.userClanData.cId : "0";
		SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower, clanId);
		SetActive(t, UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
		SetPlayingStatus(t, userInfo.userId);
		SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
		bool is_visible = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId;
		SetActive(t, UI.BTN_KICK, is_visible);
		SetEvent(t, UI.BTN_KICK, "KICK", i);
		SetEvent(t, UI.BTN_JOIN, "JOIN", i);
	}

	private void SetFollowStatus(Transform t, int user_id, bool following, bool follower, string clanId)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		if (!flag)
		{
			bool is_visible = false;
			if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
			{
				is_visible = (clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
			}
			SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
			SetActive(t, UI.OBJ_FOLLOW, following | follower);
			SetActive(t, UI.SPR_FOLLOW, following);
			SetActive(t, UI.SPR_FOLLOWER, follower);
			SetActive(t, UI.SPR_SAME_CLAN_ICON, is_visible);
			UIGrid component = GetComponent<UIGrid>(t, UI.GRD_FOLLOW_ARROW);
			if (component != null)
			{
				component.Reposition();
			}
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		UIPanel panel = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>();
		Vector4 baseClipRegion = panel.baseClipRegion;
		if (is_portrait)
		{
			Vector3 localPosition = GetCtrl(UI.PORTRAIT_FRAME).localPosition;
			int height = GetHeight(UI.PORTRAIT_FRAME);
			GetCtrl(UI.FRAME).localPosition = localPosition;
			SetHeight(UI.FRAME, height);
			GetCtrl(UI.SCR_LIST).parent = GetCtrl(UI.PORTRAIT_LIST);
			baseClipRegion.w = GetHeight(UI.PORTRAIT_LIST);
		}
		else
		{
			Vector3 localPosition2 = GetCtrl(UI.LANDSCAPE_FRAME).localPosition;
			int height2 = GetHeight(UI.LANDSCAPE_FRAME);
			GetCtrl(UI.FRAME).localPosition = localPosition2;
			SetHeight(UI.FRAME, height2);
			GetCtrl(UI.SCR_LIST).parent = GetCtrl(UI.LANDSCAPE_LIST);
			baseClipRegion.w = GetHeight(UI.LANDSCAPE_LIST);
		}
		panel.baseClipRegion = baseClipRegion;
		panel.clipOffset = Vector2.zero;
		GetCtrl(UI.SCR_LIST).localPosition = Vector3.zero;
		ScrollViewResetPosition(UI.SCR_LIST);
		UpdateAnchors();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
		{
			RefreshUI();
			panel.Refresh();
		});
	}

	private void UpdateMemberList()
	{
		memberInfo.Clear();
		List<PartyModel.SlotInfo> slotInfos = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
		for (int i = 0; i < slotInfos.Count; i++)
		{
			if (slotInfos[i].userInfo != null && slotInfos[i].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				memberInfo.Add(slotInfos[i].userInfo);
			}
		}
		RefreshUI();
	}

	private void SetPlayingStatus(Transform root, int userId)
	{
		SetActive(root, UI.OBJ_LOUNGE, is_visible: false);
		SetActive(root, UI.OBJ_FIELD, is_visible: false);
		SetActive(root, UI.OBJ_QUEST, is_visible: false);
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
		{
			return;
		}
		LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
		if (loungeMemberStatus == null)
		{
			return;
		}
		switch (loungeMemberStatus.GetStatus())
		{
		case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
			SetActive(root, UI.OBJ_LOUNGE, is_visible: true);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.QUEST:
			SetQuestInfo(root, loungeMemberStatus.questId);
			SetActive(root, UI.BTN_JOIN, !CheckRush(loungeMemberStatus.questId));
			SetActive(root, UI.LBL_PLAYING_QUEST, is_visible: true);
			SetActive(root, UI.LBL_PLAYING_READY, is_visible: false);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
			SetQuestInfo(root, loungeMemberStatus.questId);
			SetActive(root, UI.LBL_PLAYING_QUEST, is_visible: false);
			SetActive(root, UI.LBL_PLAYING_READY, is_visible: true);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.FIELD:
		{
			SetActive(root, UI.OBJ_FIELD, is_visible: true);
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)loungeMemberStatus.fieldMapId);
			if (fieldMapData == null)
			{
				SetLabelText(root, UI.LBL_AREA_NAME, "");
				break;
			}
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
			if (data == null)
			{
				SetLabelText(root, UI.LBL_AREA_NAME, fieldMapData.mapName);
			}
			else
			{
				SetLabelText(root, UI.LBL_AREA_NAME, data.regionName + " - " + fieldMapData.mapName);
			}
			break;
		}
		}
	}

	private void SetQuestInfo(Transform root, int questId)
	{
		SetActive(root, UI.OBJ_QUEST, is_visible: true);
		string questText = Singleton<QuestTable>.I.GetQuestData((uint)questId).questText;
		SetLabelText(root, UI.LBL_QUEST_NAME, questText);
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
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
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
		MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
		MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
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

	private void OnQuery_CHANGE_INFO()
	{
		UpdateMemberList();
	}

	private void OnQuery_JOIN()
	{
		int index = (int)GameSection.GetEventData();
		CharaInfo charaInfo = memberInfo[index];
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
		{
			LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[charaInfo.userId];
			switch (loungeMemberStatus.GetStatus())
			{
			case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
				OnQuery_MAIN_MENU_LOUNGE();
				break;
			case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
			case LoungeMemberStatus.MEMBER_STATUS.QUEST:
				JoinParty(loungeMemberStatus.partyId);
				break;
			case LoungeMemberStatus.MEMBER_STATUS.FIELD:
				JoinField(loungeMemberStatus.fieldMapId);
				break;
			}
		}
	}

	private void OnQuery_KICK()
	{
		int index = (int)GameSection.GetEventData();
		CharaInfo charaInfo = memberInfo[index];
		GameSection.SetEventData(new object[1]
		{
			charaInfo.name
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRoomPartyKick(delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess);
		}, charaInfo.userId);
	}
}
