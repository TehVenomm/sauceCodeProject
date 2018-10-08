using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestAcceptInvitation : QuestSearchListSelect
{
	protected new enum UI
	{
		GRD_QUEST,
		LBL_HOST_NAME,
		LBL_HOST_LV,
		TGL_MEMBER_1,
		TGL_MEMBER_2,
		TGL_MEMBER_3,
		LBL_LV,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE,
		STR_NON_LIST,
		BTN_CONDITION,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		LBL_CONDITION_A,
		LBL_CONDITION_B,
		LBL_CONDITION_DIFFICULTY,
		SPR_CONDITION_DIFFICULTY,
		STR_NO_CONDITION,
		LBL_CONDITION_ENEMY,
		OBJ_NPC_MESSAGE,
		SPR_WINDOW_BASE,
		SPR_ICON_DOUBLE,
		OBJ_SEARCH_INFO_ROOT,
		OBJ_QUEST_INFO_ROOT,
		OBJ_ORDER_QUEST_INFO_ROOT,
		SPR_ORDER_RARITY_FRAME,
		SPR_CHALLENGE_NOT_CLEAR,
		LBL_CHALLENGE_NOT_CLEAR,
		SPR_ICON_DEFENSE_BATTLE,
		LBL_RECRUTING_MEMBERS,
		TBL_QUEST,
		SCR_QUEST,
		LBL_LOUNGE_NAME,
		LBL_LABEL,
		OBJ_SYMBOL,
		TEX_STAMP,
		TGL_L_MEMBER_1,
		TGL_L_MEMBER_2,
		TGL_L_MEMBER_3,
		TGL_L_MEMBER_4,
		TGL_L_MEMBER_5,
		TGL_L_MEMBER_6,
		TGL_L_MEMBER_7,
		TEX_MODEL,
		LBL_NAME,
		LBL_LEVEL,
		SPR_ICON_HOST,
		SPR_ICON_FIRST_MET,
		OBJ_DEGREE_FRAME_ROOT,
		GRD_LIST,
		SPR_BLACKLIST_ICON,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		OBJ_LOUNGE,
		OBJ_FIELD,
		OBJ_QUEST,
		OBJ_ARENA,
		LBL_PLAYING_QUEST,
		LBL_PLAYING_READY,
		LBL_PLAYING_ARENA,
		LBL_AREA_NAME,
		LBL_GUILD_NAME,
		SPR_EMBLEM_LAYER_1,
		SPR_EMBLEM_LAYER_2,
		SPR_EMBLEM_LAYER_3,
		LBL_USER_INVITE,
		LBL_MEM_NUM,
		LBL_USER_NAME,
		LBL_CHAT_MESSAGE,
		LBL_MATERIAL_NAME,
		SLD_PROGRESS,
		OBJ_MATERIAL_ICON,
		LBL_QUATITY,
		OBJ_FULL,
		OBJ_NORMAL,
		LBL_DONATE_NUM,
		LBL_DONATE_MAX,
		BTN_GIFT
	}

	private PartyModel.Party[] parties;

	private LoungeModel.Lounge[] lounges;

	private LoungeModel.SlotInfo[] rallyInvites;

	private GuildInvitedModel.GuildInvitedInfo[] guildInvites;

	private DonateInvitationInfo[] guildDonateInvites;

	private UI[] loungeMembers = new UI[7]
	{
		UI.TGL_L_MEMBER_1,
		UI.TGL_L_MEMBER_2,
		UI.TGL_L_MEMBER_3,
		UI.TGL_L_MEMBER_4,
		UI.TGL_L_MEMBER_5,
		UI.TGL_L_MEMBER_6,
		UI.TGL_L_MEMBER_7
	};

	protected override void SendSearchRequest(Action onFinish, Action<bool> cb)
	{
		StartCoroutine(GetInvitedList(onFinish, cb));
	}

	private IEnumerator GetInvitedList(Action onFinish, Action<bool> cb)
	{
		bool partySuccess_ = false;
		bool loungeSuccess_ = false;
		bool rallySuccess_ = false;
		bool guildInviteSuccess_ = false;
		bool guildDonateInviteSuccess_ = false;
		bool waitGetData5 = true;
		MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty(delegate(bool partySuccess)
		{
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_0060: stateMachine*/)._003CpartySuccess__003E__0 = partySuccess;
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_0060: stateMachine*/)._003CwaitGetData_003E__5 = false;
		}, false);
		while (waitGetData5)
		{
			yield return (object)null;
		}
		waitGetData5 = true;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInvitedLounge(delegate(bool loungeSuccess)
		{
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_00a1: stateMachine*/)._003CloungeSuccess__003E__1 = loungeSuccess;
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_00a1: stateMachine*/)._003CwaitGetData_003E__5 = false;
		});
		while (waitGetData5)
		{
			yield return (object)null;
		}
		waitGetData5 = true;
		MonoBehaviourSingleton<LoungeMatchingManager>.I.GetRallyList(delegate(bool rallySuccess)
		{
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_00e1: stateMachine*/)._003CrallySuccess__003E__2 = rallySuccess;
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_00e1: stateMachine*/)._003CwaitGetData_003E__5 = false;
		});
		while (waitGetData5)
		{
			yield return (object)null;
		}
		waitGetData5 = true;
		MonoBehaviourSingleton<GuildManager>.I.SendInvitedGuild(delegate(bool guildSuccess)
		{
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_0121: stateMachine*/)._003CguildInviteSuccess__003E__3 = guildSuccess;
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_0121: stateMachine*/)._003CwaitGetData_003E__5 = false;
		}, false);
		while (waitGetData5)
		{
			yield return (object)null;
		}
		waitGetData5 = true;
		MonoBehaviourSingleton<GuildManager>.I.SendDonateInvitationList(delegate(bool guildDonateInviteSuccess)
		{
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_0162: stateMachine*/)._003CguildDonateInviteSuccess__003E__4 = guildDonateInviteSuccess;
			((_003CGetInvitedList_003Ec__IteratorC8)/*Error near IL_0162: stateMachine*/)._003CwaitGetData_003E__5 = false;
		}, false);
		while (waitGetData5)
		{
			yield return (object)null;
		}
		onFinish();
		cb?.Invoke(partySuccess_ && loungeSuccess_ && rallySuccess_ && guildInviteSuccess_ && guildDonateInviteSuccess_);
	}

	public override void UpdateUI()
	{
		if (!PartyManager.IsValidNotEmptyList() && !GuildManager.IsValidNotEmptyInviteList() && !GuildManager.IsValidNotEmptyDonateInviteList())
		{
			MonoBehaviourSingleton<UserInfoManager>.I.ClearPartyInvite();
			MonoBehaviourSingleton<UIManager>.I.invitationButton.Close(UITransition.TYPE.CLOSE);
			MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close(UITransition.TYPE.CLOSE);
		}
		if (!PartyManager.IsValidNotEmptyList() && !LoungeMatchingManager.IsValidNotEmptyList() && !LoungeMatchingManager.IsValidNotEmptyRallyList() && !GuildManager.IsValidNotEmptyInviteList() && !GuildManager.IsValidNotEmptyDonateInviteList())
		{
			SetActive(UI.GRD_QUEST, false);
			SetActive(UI.TBL_QUEST, false);
			SetActive(UI.STR_NON_LIST, true);
		}
		else
		{
			parties = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
			lounges = MonoBehaviourSingleton<LoungeMatchingManager>.I.lounges.ToArray();
			rallyInvites = MonoBehaviourSingleton<LoungeMatchingManager>.I.rallyInvite.ToArray();
			guildInvites = MonoBehaviourSingleton<GuildManager>.I.guildInviteList.ToArray();
			guildDonateInvites = MonoBehaviourSingleton<GuildManager>.I.donateInviteList.ToArray();
			SetActive(UI.TBL_QUEST, true);
			SetActive(UI.GRD_QUEST, true);
			SetActive(UI.STR_NON_LIST, false);
			UpdateTable();
		}
	}

	protected void UpdateTable()
	{
		int num = parties.Length;
		int num2 = lounges.Length;
		int num3 = rallyInvites.Length;
		int num4 = guildInvites.Length;
		int num5 = guildDonateInvites.Length;
		int item_num = num + num2 + num3 + num4 + num5;
		int partyStartIndex = num2;
		int rallyStartIndex = num + num2;
		int guildInviteStartIndex = num + num2 + num3;
		int guildDonateInviteStartIndex = num + num2 + num3 + num4;
		Transform ctrl = GetCtrl(UI.TBL_QUEST);
		if ((UnityEngine.Object)ctrl != (UnityEngine.Object)null)
		{
			int j = 0;
			for (int childCount = ctrl.childCount; j < childCount; j++)
			{
				Transform child = ctrl.GetChild(0);
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
		SetTable(UI.TBL_QUEST, string.Empty, item_num, true, delegate(int i, Transform parent)
		{
			Transform transform = null;
			if (i < guildDonateInviteStartIndex)
			{
				if (i < guildInviteStartIndex)
				{
					if (i < rallyStartIndex)
					{
						if (i < partyStartIndex)
						{
							return Realizes("LoungeSearchListItem", parent, true);
						}
						return Realizes("QuestInvitationListItem", parent, true);
					}
					return Realizes("LoungeMemberListItem", parent, true);
				}
				return Realizes("GuildInvitedListItem", parent, true);
			}
			int index5 = i - guildDonateInviteStartIndex;
			string prefab_name = InitGuildDonateInviteObject(index5);
			return Realizes(prefab_name, parent, true);
		}, delegate(int i, Transform t, bool is_recycle)
		{
			SetActive(t, true);
			if (i >= guildDonateInviteStartIndex)
			{
				int index = i - guildDonateInviteStartIndex;
				InitGuildDonateInvite(index, t);
			}
			else if (i >= guildInviteStartIndex)
			{
				int index2 = i - guildInviteStartIndex;
				InitGuild(index2, t);
			}
			else if (i >= rallyStartIndex)
			{
				int index3 = i - rallyStartIndex;
				InitRally(index3, t);
			}
			else if (i >= partyStartIndex)
			{
				int index4 = i - partyStartIndex;
				InitParty(index4, t);
			}
			else
			{
				InitLounge(i, t);
			}
		});
		UIScrollView component = GetComponent<UIScrollView>(UI.SCR_QUEST);
		component.enabled = true;
		RepositionTable();
	}

	private void InitLounge(int index, Transform t)
	{
		SetEvent(t, "SELECT_LOUNGE", index);
		LoungeModel.Lounge lounge = lounges[index];
		CharaInfo charaInfo = null;
		for (int i = 0; i < lounge.slotInfos.Count; i++)
		{
			if (lounge.slotInfos[i].userInfo.userId == lounge.ownerUserId)
			{
				charaInfo = lounge.slotInfos[i].userInfo;
				break;
			}
		}
		SetLabelText(t, UI.LBL_HOST_NAME, charaInfo.name);
		SetLabelText(t, UI.LBL_HOST_LV, charaInfo.level.ToString());
		SetLabelText(t, UI.LBL_LOUNGE_NAME, lounge.name);
		string text = StringTable.Get(STRING_CATEGORY.LOUNGE_LABEL, (uint)lounge.label);
		SetLabelText(t, UI.LBL_LABEL, text);
		SetStamp(t, lounge.stampId);
		int num = lounge.num + 1;
		int num2 = lounge.slotInfos.Count((LoungeModel.SlotInfo slotInfo) => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != lounge.ownerUserId);
		for (int j = 0; j < 7; j++)
		{
			bool is_visible = j < num - 1;
			SetActive(t, loungeMembers[j], is_visible);
			SetToggle(t, loungeMembers[j], j < num2);
		}
	}

	private void InitParty(int index, Transform t)
	{
		QuestTable.QuestTableData questTableData = null;
		questTableData = ((parties[index].quest.explore == null) ? Singleton<QuestTable>.I.GetQuestData((uint)parties[index].quest.questId) : Singleton<QuestTable>.I.GetQuestData((uint)parties[index].quest.explore.mainQuestId));
		if (questTableData == null)
		{
			SetActive(t, false);
		}
		else
		{
			UI uI = UI.OBJ_QUEST_INFO_ROOT;
			if (questTableData.questType == QUEST_TYPE.ORDER)
			{
				uI = UI.OBJ_ORDER_QUEST_INFO_ROOT;
				SetToggle(t, UI.OBJ_ORDER_QUEST_INFO_ROOT, true);
			}
			else
			{
				SetToggle(t, UI.OBJ_ORDER_QUEST_INFO_ROOT, false);
			}
			SetEvent(t, "SELECT_ROOM", index);
			Transform transform = FindCtrl(t, uI);
			SetEnemyIconGradeFrame(transform, UI.SPR_ORDER_RARITY_FRAME, questTableData);
			SetQuestData(questTableData, transform);
			SetPartyData(parties[index], t);
			SetMemberIcon(t, questTableData);
		}
	}

	private void InitRally(int index, Transform t)
	{
		SetEvent(t, "JOIN_MAP", index);
		LoungeModel.SlotInfo slotInfo = rallyInvites[index];
		CharaInfo userInfo = slotInfo.userInfo;
		FollowLoungeMember followLoungeMember = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetFollowLoungeMember(userInfo.userId);
		EquipSetCalculator otherEquipSetCalculator = MonoBehaviourSingleton<StatusManager>.I.GetOtherEquipSetCalculator(index + 4);
		otherEquipSetCalculator.SetEquipSet(slotInfo.userInfo.equipSet, false);
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
			RepositionTable();
		});
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

	private string InitGuildDonateInviteObject(int index)
	{
		DonateInvitationInfo donateInvitationInfo = guildDonateInvites[index];
		string result = "GuildDonateInvitationListItem";
		double num = donateInvitationInfo.expired / 1000.0 - DateTimeToTimestampSeconds();
		if (donateInvitationInfo.itemNum >= donateInvitationInfo.quantity)
		{
			result = "GuildDonateInvitationListItemFull";
		}
		else if (num < 1.0)
		{
			result = "GuildDonateInvitationListItemExpired";
		}
		return result;
	}

	private void InitGuildDonateInvite(int index, Transform t)
	{
		DonateInvitationInfo info = guildDonateInvites[index];
		if (MonoBehaviourSingleton<GuildManager>.I.guildData.emblem != null && MonoBehaviourSingleton<GuildManager>.I.guildData.emblem.Length >= 3)
		{
			SetSprite(t, UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[0]));
			SetSprite(t, UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[1]));
			SetSprite(t, UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(MonoBehaviourSingleton<GuildManager>.I.guildData.emblem[2]));
		}
		else
		{
			SetSprite(t, UI.SPR_EMBLEM_LAYER_1, string.Empty);
			SetSprite(t, UI.SPR_EMBLEM_LAYER_2, string.Empty);
			SetSprite(t, UI.SPR_EMBLEM_LAYER_3, string.Empty);
		}
		double num = info.expired / 1000.0 - DateTimeToTimestampSeconds();
		if (!(num < 1.0) && info.itemNum < info.quantity)
		{
			int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1, false);
			bool flag = info.itemNum >= info.quantity;
			SetLabelText(t, UI.LBL_CHAT_MESSAGE, info.msg);
			SetLabelText(t, UI.LBL_USER_NAME, info.nickName);
			SetLabelText(t, UI.LBL_MATERIAL_NAME, info.itemName);
			SetLabelText(t, UI.LBL_QUATITY, itemNum);
			SetLabelText(t, UI.LBL_DONATE_NUM, info.itemNum);
			SetLabelText(t, UI.LBL_DONATE_MAX, info.quantity);
			SetSliderValue(t, UI.SLD_PROGRESS, (float)info.itemNum / (float)info.quantity);
			if (!flag && itemNum > 0 && info.itemNum < info.quantity)
			{
				SetButtonEvent(t, UI.BTN_GIFT, new EventDelegate(delegate
				{
					DispatchEvent("SEND_GUILD_DONATE", info.ParseDonateInfo());
				}));
			}
			else
			{
				SetButtonEnabled(t, UI.BTN_GIFT, false);
			}
			Transform transform = FindCtrl(t, UI.OBJ_MATERIAL_ICON);
			Item item = new Item();
			item.uniqId = "0";
			item.itemId = info.itemId;
			item.num = info.itemNum;
			ItemInfo item2 = ItemInfo.CreateItemInfo(item);
			ItemSortData itemSortData = new ItemSortData();
			itemSortData.SetItem(item2);
			SetItemIcon(transform, itemSortData, transform);
		}
	}

	private void InitGuild(int index, Transform t)
	{
		GuildInvitedModel.GuildInvitedInfo guildInvitedInfo = guildInvites[index];
		if (LoungeMatchingManager.IsValidInLounge())
		{
			SetEvent(t, "SELECT_GUILD_LOUNGE", guildInvitedInfo);
		}
		else
		{
			SetEvent(t, "SELECT_GUILD_HOME", guildInvitedInfo);
		}
		SetLabelText(t, UI.LBL_GUILD_NAME, guildInvitedInfo.name);
		SetLabelText(t, UI.LBL_HOST_LV, guildInvitedInfo.level);
		if (guildInvitedInfo.emblem != null && guildInvitedInfo.emblem.Length >= 3)
		{
			SetSprite(t, UI.SPR_EMBLEM_LAYER_1, GuildItemManager.I.GetItemSprite(guildInvitedInfo.emblem[0]));
			SetSprite(t, UI.SPR_EMBLEM_LAYER_2, GuildItemManager.I.GetItemSprite(guildInvitedInfo.emblem[1]));
			SetSprite(t, UI.SPR_EMBLEM_LAYER_3, GuildItemManager.I.GetItemSprite(guildInvitedInfo.emblem[2]));
		}
		else
		{
			SetSprite(t, UI.SPR_EMBLEM_LAYER_1, string.Empty);
			SetSprite(t, UI.SPR_EMBLEM_LAYER_2, string.Empty);
			SetSprite(t, UI.SPR_EMBLEM_LAYER_3, string.Empty);
		}
		SetLabelText(t, UI.LBL_HOST_NAME, guildInvitedInfo.admin);
		SetLabelText(t, UI.LBL_USER_INVITE, guildInvitedInfo.sender + "'s recruiting");
		SetLabelText(t, UI.LBL_MEM_NUM, guildInvitedInfo.currentMem + "/" + guildInvitedInfo.memCap);
	}

	private void SetItemIcon(Transform holder, ItemSortData data, Transform parent_scroll)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int icon_id = -1;
		int num = -1;
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
			num = data.GetNum();
			if (num == 1)
			{
				num = -1;
			}
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		}
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(data.GetTableID());
			enemy_icon_id = itemData.enemyIconID;
		}
		ItemIcon itemIcon = null;
		if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = data.GetIconType();
			itemIconCreateParam.icon_id = data.GetIconID();
			itemIconCreateParam.rarity = data.GetRarity();
			itemIconCreateParam.parent = holder;
			itemIconCreateParam.element = data.GetIconElement();
			itemIconCreateParam.magi_enable_equip_type = data.GetIconMagiEnableType();
			itemIconCreateParam.num = data.GetNum();
			itemIconCreateParam.enemy_icon_id = enemy_icon_id;
			itemIconCreateParam.questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST;
			itemIcon = ItemIcon.Create(itemIconCreateParam);
		}
		else
		{
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, -1, "DROP", 0, is_new, -1, false, null, false, enemy_icon_id, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		}
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), parent_scroll);
	}

	private void OnQuery_JOIN_MAP()
	{
		int num = (int)GameSection.GetEventData();
		int userId = rallyInvites[num].userInfo.userId;
		LoungeMemberStatus loungeMemberStatus = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeMemberStatus[userId];
		switch (loungeMemberStatus.GetStatus())
		{
		case LoungeMemberStatus.MEMBER_STATUS.QUEST_READY:
		case LoungeMemberStatus.MEMBER_STATUS.QUEST:
			JoinParty(loungeMemberStatus.partyId, loungeMemberStatus.questId);
			break;
		case LoungeMemberStatus.MEMBER_STATUS.FIELD:
			JoinField(loungeMemberStatus.fieldMapId);
			break;
		}
	}

	private void JoinField(int fieldMapId)
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
			else if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				if (QuestManager.IsValidInGame())
				{
					MonoBehaviourSingleton<WorldMapManager>.I.SetJumpPortalID(fieldMapData.jumpPortalID);
					MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = true;
					MonoBehaviourSingleton<InGameProgress>.I.QuestToField(fieldMapData.jumpPortalID);
				}
				else
				{
					MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
					MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
				}
			}
			else
			{
				GameSection.StayEvent();
				CoopApp.EnterField(fieldMapData.jumpPortalID, 0u, delegate(bool is_matching, bool is_connect, bool is_regist)
				{
					if (!is_connect)
					{
						GameSection.ChangeStayEvent("COOP_SERVER_INVALID", null);
						GameSection.ResumeEvent(true, null);
						AppMain i = MonoBehaviourSingleton<AppMain>.I;
						i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
						{
							DispatchEvent("CLOSE", null);
						});
					}
					else
					{
						GameSection.ResumeEvent(is_regist, null);
						if (is_regist)
						{
							MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
						}
					}
				});
			}
		}
	}

	private void JoinParty(string partyId, int questID)
	{
		if (!partyId.Equals(MonoBehaviourSingleton<PartyManager>.I.GetPartyId()))
		{
			EventData[] autoEvents = new EventData[3]
			{
				new EventData("MAIN_MENU_LOUNGE", null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("JOIN_ROOM", partyId)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	private void SetStamp(Transform root, int stampId)
	{
		StampTable.Data data = Singleton<StampTable>.I.GetData((uint)stampId);
		if (data != null)
		{
			StartCoroutine(LoadStamp(root, stampId));
		}
	}

	private IEnumerator LoadStamp(Transform root, int stampId)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId, false);
		while (load_queue.IsLoading())
		{
			yield return (object)null;
		}
		if (lo_stamp.loadedObject != (UnityEngine.Object)null)
		{
			Texture2D stamp = lo_stamp.loadedObject as Texture2D;
			SetActive(root, UI.OBJ_SYMBOL, true);
			SetTexture(root, UI.TEX_STAMP, stamp);
		}
	}

	private void RepositionTable()
	{
		UITable component = GetComponent<UITable>(UI.TBL_QUEST);
		if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
		{
			component.Reposition();
			List<Transform> childList = component.GetChildList();
			for (int i = 0; i < childList.Count; i++)
			{
				Vector3 localPosition = childList[i].localPosition;
				localPosition.x = 0f;
				childList[i].localPosition = localPosition;
			}
		}
	}

	protected virtual void OnQuery_SELECT_LOUNGE()
	{
		int num = (int)GameSection.GetEventData();
		if (LoungeMatchingManager.IsValidInLounge())
		{
			GameSection.StopEvent();
			if (!(lounges[num].id == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.id))
			{
				EventData[] autoEvents = new EventData[5]
				{
					new EventData("LOUNGE", null),
					new EventData("LOUNGE_SETTINGS", null),
					new EventData("EXIT", null),
					new EventData("LOUNGE", null),
					new EventData("FRIEND_INVITED_LOUNGE", lounges[num].id)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			}
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(lounges[num].id, delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess, null);
			});
		}
	}

	protected virtual void OnQuery_SELECT_GUILD()
	{
	}

	private void OnCloseDialog_GuildDonateSendDialog()
	{
		RefreshUI();
	}

	private void OnCloseDialog_GuildInvitedJoinDialog()
	{
		RefreshUI();
	}

	private double DateTimeToTimestampSeconds()
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow - d).TotalSeconds;
	}
}
