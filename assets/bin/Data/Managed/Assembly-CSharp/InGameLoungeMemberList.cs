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
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
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

	public unsafe override void UpdateUI()
	{
		int count = memberInfo.Count;
		if (count <= 0)
		{
			SetActive((Enum)UI.GRD_LIST, false);
			SetActive((Enum)UI.STR_NON_LIST, true);
		}
		else
		{
			SetGrid(UI.GRD_LIST, "InGameLoungeMemberListItem", count, false, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			SetActive((Enum)UI.STR_NON_LIST, false);
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
		SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower);
		SetActive(t, UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
		SetPlayingStatus(t, userInfo.userId);
		SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
		bool is_visible = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId;
		SetActive(t, UI.BTN_KICK, is_visible);
		SetEvent(t, UI.BTN_KICK, "KICK", i);
		SetEvent(t, UI.BTN_JOIN, "JOIN", i);
	}

	private void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		if (!flag)
		{
			SetActive(t, UI.SPR_FOLLOW, following);
			SetActive(t, UI.SPR_FOLLOWER, follower);
		}
	}

	private unsafe void OnScreenRotate(bool is_portrait)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		UIPanel panel = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>();
		Vector4 baseClipRegion = panel.baseClipRegion;
		if (is_portrait)
		{
			Vector3 localPosition = GetCtrl(UI.PORTRAIT_FRAME).get_localPosition();
			int height = GetHeight(UI.PORTRAIT_FRAME);
			GetCtrl(UI.FRAME).set_localPosition(localPosition);
			SetHeight((Enum)UI.FRAME, height);
			GetCtrl(UI.SCR_LIST).set_parent(GetCtrl(UI.PORTRAIT_LIST));
			baseClipRegion.w = (float)GetHeight(UI.PORTRAIT_LIST);
		}
		else
		{
			Vector3 localPosition2 = GetCtrl(UI.LANDSCAPE_FRAME).get_localPosition();
			int height2 = GetHeight(UI.LANDSCAPE_FRAME);
			GetCtrl(UI.FRAME).set_localPosition(localPosition2);
			SetHeight((Enum)UI.FRAME, height2);
			GetCtrl(UI.SCR_LIST).set_parent(GetCtrl(UI.LANDSCAPE_LIST));
			baseClipRegion.w = (float)GetHeight(UI.LANDSCAPE_LIST);
		}
		panel.baseClipRegion = baseClipRegion;
		panel.clipOffset = Vector2.get_zero();
		GetCtrl(UI.SCR_LIST).set_localPosition(Vector3.get_zero());
		ScrollViewResetPosition((Enum)UI.SCR_LIST);
		UpdateAnchors();
		AppMain i = MonoBehaviourSingleton<AppMain>.I;
		_003COnScreenRotate_003Ec__AnonStorey396 _003COnScreenRotate_003Ec__AnonStorey;
		i.onDelayCall = Delegate.Combine((Delegate)i.onDelayCall, (Delegate)new Action((object)_003COnScreenRotate_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void UpdateMemberList()
	{
		memberInfo.Clear();
		List<LoungeModel.SlotInfo> slotInfos = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
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
		SetActive(root, UI.OBJ_LOUNGE, false);
		SetActive(root, UI.OBJ_FIELD, false);
		SetActive(root, UI.OBJ_QUEST, false);
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus != null)
		{
			LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
			if (loungeMemberStatus != null)
			{
				switch (loungeMemberStatus.GetStatus())
				{
				case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
					SetActive(root, UI.OBJ_LOUNGE, true);
					break;
				case LoungeMemberStatus.MEMBER_STATUS.QUEST:
					SetQuestInfo(root, loungeMemberStatus.questId);
					SetActive(root, UI.BTN_JOIN, !CheckRush(loungeMemberStatus.questId));
					SetActive(root, UI.LBL_PLAYING_QUEST, true);
					SetActive(root, UI.LBL_PLAYING_READY, false);
					break;
				case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
					SetQuestInfo(root, loungeMemberStatus.questId);
					SetActive(root, UI.LBL_PLAYING_QUEST, false);
					SetActive(root, UI.LBL_PLAYING_READY, true);
					break;
				case LoungeMemberStatus.MEMBER_STATUS.FIELD:
				{
					SetActive(root, UI.OBJ_FIELD, true);
					FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)loungeMemberStatus.fieldMapId);
					if (fieldMapData == null)
					{
						SetLabelText(root, UI.LBL_AREA_NAME, string.Empty);
					}
					else
					{
						RegionTable.Data data = Singleton<RegionTable>.I.GetData(fieldMapData.regionId);
						if (data == null)
						{
							SetLabelText(root, UI.LBL_AREA_NAME, fieldMapData.mapName);
						}
						else
						{
							SetLabelText(root, UI.LBL_AREA_NAME, data.regionName + " - " + fieldMapData.mapName);
						}
					}
					break;
				}
				}
			}
		}
	}

	private void SetQuestInfo(Transform root, int questId)
	{
		SetActive(root, UI.OBJ_QUEST, true);
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
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			if (fieldMapId == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
			{
				GameSection.StopEvent();
			}
			else
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)fieldMapId);
				if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
				{
					Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
				}
				else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, false, true))
				{
					GameSection.StopEvent();
				}
				else if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
				{
					DispatchEvent("CANT_JUMP", null);
				}
				else
				{
					MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
					MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
				}
			}
		}
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
			GameSection.ResumeEvent(isSuccess, null);
		}, charaInfo.userId);
	}
}
