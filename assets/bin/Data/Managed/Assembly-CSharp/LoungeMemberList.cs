using Network;
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
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
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

	private List<LoungeModel.SlotInfo> members;

	public override void Initialize()
	{
		members = new List<LoungeModel.SlotInfo>(8);
		SetLabelText(UI.STR_LIST_NUM, base.sectionData.GetText("MEMBER_NUMBER"));
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool isWait = true;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.GetRallyList(delegate
		{
			((_003CDoInitialize_003Ec__IteratorE0)/*Error near IL_002d: stateMachine*/)._003CisWait_003E__0 = false;
		});
		while (isWait)
		{
			yield return (object)null;
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
		SetActive(UI.LBL_NON_LIST, members.Count <= 0);
		SetLabelText(UI.LBL_MEMBER_NUMBER_NOW, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount().ToString());
		SetLabelText(UI.LBL_MEMBER_NUMBER_MAX, (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.num + 1).ToString());
	}

	private void SetMembers()
	{
		List<LoungeModel.SlotInfo> slotInfos = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.slotInfos;
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
		SetDynamicList(UI.GRD_LIST, "LoungeMemberListItem", members.Count, false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			SetupListItem(members[i], i, t, is_recycle);
		});
	}

	private void SetupListItem(LoungeModel.SlotInfo data, int i, Transform t, bool is_recycle)
	{
		SetEvent(t, "DETAIL", i);
		SetMemberInfo(data, i, t);
	}

	private void SetMemberInfo(LoungeModel.SlotInfo data, int i, Transform t)
	{
		CharaInfo userInfo = data.userInfo;
		FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(i + 4);
		otherEquipSetCalculator.SetEquipSet(data.userInfo.equipSet, false);
		SetRenderPlayerModel(t, UI.TEX_MODEL, PlayerLoadInfo.FromCharaInfo(userInfo, false, true, false, true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), true, null);
		SetLabelText(t, UI.LBL_NAME, userInfo.name);
		SetLabelText(t, UI.LBL_LEVEL, userInfo.level.ToString());
		SetFollowStatus(t, userInfo.userId, followLoungeMember.following, followLoungeMember.follower);
		SetActive(t, UI.SPR_ICON_HOST, userInfo.userId == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.ownerUserId);
		SetPlayingStatus(t, userInfo.userId);
		SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(userInfo.userId));
		DegreePlate component = FindCtrl(t, UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>();
		component.Initialize(userInfo.selectedDegrees, false, delegate
		{
			GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>().Reposition();
		});
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsRallyUser(userInfo.userId))
		{
			SetBadge(t, -1, SpriteAlignment.TopLeft, 10, 0, true);
		}
	}

	private void SetFollowStatus(Transform t, int user_id, bool following, bool follower)
	{
		bool flag = MonoBehaviourSingleton<BlackListManager>.I.CheckBlackList(user_id);
		SetActive(t, UI.SPR_BLACKLIST_ICON, flag);
		if (flag)
		{
			SetActive(t, UI.SPR_FOLLOW, false);
			SetActive(t, UI.SPR_FOLLOWER, false);
		}
		else
		{
			SetActive(t, UI.SPR_FOLLOW, following);
			SetActive(t, UI.SPR_FOLLOWER, follower);
		}
	}

	private void SetPlayingStatus(Transform root, int userId)
	{
		SetActive(root, UI.OBJ_LOUNGE, false);
		SetActive(root, UI.OBJ_FIELD, false);
		SetActive(root, UI.OBJ_QUEST, false);
		SetActive(root, UI.OBJ_ARENA, false);
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus == null)
		{
			SetActive(root, UI.OBJ_LOUNGE, true);
		}
		else
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
				case LoungeMemberStatus.MEMBER_STATUS.ARENA:
					SetActive(root, UI.OBJ_ARENA, true);
					SetActive(root, UI.LBL_PLAYING_ARENA, true);
					break;
				}
			}
			else
			{
				SetActive(root, UI.OBJ_LOUNGE, true);
			}
		}
	}

	private void SetQuestInfo(Transform root, int questId)
	{
		SetActive(root, UI.OBJ_QUEST, true);
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
