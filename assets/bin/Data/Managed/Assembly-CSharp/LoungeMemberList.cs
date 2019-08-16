using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeMemberList : GameSection
{
	private enum UI
	{
		GRD_LIST,
		TEX_MODEL,
		LBL_LEVEL,
		LBL_NAME,
		GRD_FOLLOW_ARROW,
		OBJ_FOLLOW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
		OBJ_FIELD,
		OBJ_QUEST,
		OBJ_LOUNGE,
		OBJ_ARENA,
		LBL_PLAYING_FIELD,
		LBL_AREA_NAME,
		LBL_PLAYING_QUEST,
		LBL_PLAYING_READY,
		LBL_QUEST_NAME,
		LBL_IN_LOUNGE,
		LBL_PLAYING_ARENA,
		LBL_NON_LIST,
		SPR_ICON_HOST,
		SPR_ICON_FIRST_MET,
		LBL_MEMBER_NUMBER_NOW,
		LBL_MEMBER_NUMBER_MAX,
		STR_LIST_NUM,
		OBJ_DEGREE_FRAME_ROOT
	}

	private List<PartyModel.SlotInfo> members;

	public override void Initialize()
	{
		members = new List<PartyModel.SlotInfo>(8);
		SetLabelText((Enum)UI.STR_LIST_NUM, base.sectionData.GetText("MEMBER_NUMBER"));
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool isWait = true;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.GetRallyList(delegate
		{
			isWait = false;
		});
		while (isWait)
		{
			yield return null;
		}
		base.Initialize();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
	}

	public override void UpdateUI()
	{
		SetMembers();
		UpdateListUI();
		SetActive((Enum)UI.LBL_NON_LIST, members.Count <= 0);
		SetLabelText((Enum)UI.LBL_MEMBER_NUMBER_NOW, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount().ToString());
		SetLabelText((Enum)UI.LBL_MEMBER_NUMBER_MAX, (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.num + 1).ToString());
	}

	private void SetMembers()
	{
		List<PartyModel.SlotInfo> slotInfos = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
		members.Clear();
		for (int i = 0; i < slotInfos.Count; i++)
		{
			if (slotInfos[i].userInfo != null && slotInfos[i].userInfo.userId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
			{
				members.Add(slotInfos[i]);
			}
		}
	}

	private void UpdateListUI()
	{
		SetDynamicList((Enum)UI.GRD_LIST, "LoungeMemberListItem", members.Count, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			SetupListItem(members[i], i, t, is_recycle);
		});
	}

	private void SetupListItem(PartyModel.SlotInfo data, int i, Transform t, bool is_recycle)
	{
		SetEvent(t, "DETAIL", i);
		SetMemberInfo(data, i, t);
	}

	private void SetMemberInfo(PartyModel.SlotInfo data, int i, Transform t)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		CharaInfo userInfo = data.userInfo;
		FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
		otherEquipSetCalculator.SetEquipSet(data.userInfo.equipSet);
		SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(userInfo, need_weapon: false, need_helm: true, need_leg: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
		SetLabelText(t, UI.LBL_NAME, userInfo.name);
		SetLabelText(t, UI.LBL_LEVEL, userInfo.level.ToString());
		string clanId = (userInfo.userClanData == null) ? "0" : userInfo.userClanData.cId;
		SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower, clanId);
		SetActive(t, UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
		SetPlayingStatus(t, userInfo.userId);
		SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(userInfo.selectedDegrees, isButton: false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsRallyUser(userInfo.userId))
		{
			SetBadge(t, -1, 1, 10, 0, is_scale_normalize: true);
		}
	}

	private void SetFollowStatus(Transform t, int user_id, bool following, bool follower, string clanId)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		if (flag)
		{
			SetActive(t, UI.SPR_FOLLOW, is_visible: false);
			SetActive(t, UI.SPR_FOLLOWER, is_visible: false);
			return;
		}
		bool is_visible = false;
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan != null && MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsRegistered())
		{
			is_visible = (clanId == MonoBehaviourSingleton<UserInfoManager>.I.userClan.cId);
		}
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		SetActive(t, UI.OBJ_FOLLOW, following || follower);
		SetActive(t, UI.SPR_FOLLOW, following);
		SetActive(t, UI.SPR_FOLLOWER, follower);
		SetActive(t, UI.SPR_SAME_CLAN_ICON, is_visible);
		UIGrid component = base.GetComponent<UIGrid>(t, (Enum)UI.GRD_FOLLOW_ARROW);
		if (component != null)
		{
			component.Reposition();
		}
	}

	private void SetPlayingStatus(Transform root, int userId)
	{
		SetActive(root, UI.OBJ_LOUNGE, is_visible: false);
		SetActive(root, UI.OBJ_FIELD, is_visible: false);
		SetActive(root, UI.OBJ_QUEST, is_visible: false);
		SetActive(root, UI.OBJ_ARENA, is_visible: false);
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
		{
			SetActive(root, UI.OBJ_LOUNGE, is_visible: true);
			return;
		}
		LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
		if (loungeMemberStatus == null)
		{
			SetActive(root, UI.OBJ_LOUNGE, is_visible: true);
			return;
		}
		switch (loungeMemberStatus.GetStatus())
		{
		case LoungeMemberStatus.MEMBER_STATUS.LOUNGE:
			SetActive(root, UI.OBJ_LOUNGE, is_visible: true);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.QUEST:
			SetQuestInfo(root, loungeMemberStatus.questId);
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
				SetLabelText(root, UI.LBL_AREA_NAME, string.Empty);
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
		case LoungeMemberStatus.MEMBER_STATUS.ARENA:
			SetActive(root, UI.OBJ_ARENA, is_visible: true);
			SetActive(root, UI.LBL_PLAYING_ARENA, is_visible: true);
			break;
		}
	}

	private void SetQuestInfo(Transform root, int questId)
	{
		SetActive(root, UI.OBJ_QUEST, is_visible: true);
		string questText = Singleton<QuestTable>.I.GetQuestData((uint)questId).questText;
		SetLabelText(root, UI.LBL_QUEST_NAME, questText);
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		CharaInfo userInfo = members[num].userInfo;
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
		GameSection.SetEventData(userInfo);
	}
}
