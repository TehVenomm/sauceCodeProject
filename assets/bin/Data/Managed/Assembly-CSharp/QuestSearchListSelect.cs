using Network;
using System;
using System.Linq;
using UnityEngine;

public class QuestSearchListSelect : QuestSearchListSelectBase
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
		SPR_ICON_WAVE_MATCH,
		SPR_ICON_SERIES_OF_BATTLES
	}

	private const string LIST_ITEM_PREFAB_NAME = "QuestSearchListSelectItem";

	protected override void SendSearchRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendSearch(delegate(bool is_success, Error err)
		{
			onFinish();
			if (!is_success && base.isInitialized)
			{
				if (err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
				{
					GameSection.ChangeStayEvent("NOT_FOUND_QUEST");
					if (cb != null)
					{
						cb(obj: true);
					}
				}
			}
			else if (cb != null)
			{
				cb(is_success);
			}
		}, saveSettings: false);
	}

	protected override void ResetSearchRequest()
	{
		MonoBehaviourSingleton<PartyManager>.I.ResetSearchRequest();
	}

	public override void UpdateUI()
	{
		QuestSearchRoomCondition.SearchRequestParam searchRequest = MonoBehaviourSingleton<PartyManager>.I.searchRequest;
		bool flag = (searchRequest.questTypeBit & 4) != 0;
		bool flag2 = (searchRequest.questTypeBit & 2) != 0;
		bool flag3 = (searchRequest.questTypeBit & 1) != 0;
		bool flag4 = (searchRequest.questTypeBit & 0x10) != 0;
		bool flag5 = (flag3 && flag2 && flag && flag4) || (!flag3 && !flag2 && !flag && !flag4);
		string text = string.Empty;
		string text2 = string.Empty;
		if (searchRequest.order == 0)
		{
			text = base.sectionData.GetText("STR_SELECT_CONDITION_RECOMMEND");
			text2 = string.Empty;
		}
		else if (flag5)
		{
			text = base.sectionData.GetText("STR_SELECT_CONDITION_ALL");
			text2 = string.Empty;
		}
		else
		{
			if (flag && string.IsNullOrEmpty(text))
			{
				text = base.sectionData.GetText("STR_SELECT_CONDITION_GACHA");
			}
			if (flag2)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = base.sectionData.GetText("STR_SELECT_CONDITION_EVENT");
				}
				else if (string.IsNullOrEmpty(text2))
				{
					text2 = base.sectionData.GetText("STR_SELECT_CONDITION_EVENT");
				}
			}
			if (flag3)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = base.sectionData.GetText("STR_SELECT_CONDITION_NORMAL");
				}
				else if (string.IsNullOrEmpty(text2))
				{
					text2 = base.sectionData.GetText("STR_SELECT_CONDITION_NORMAL");
				}
			}
		}
		SetLabelText((Enum)UI.LBL_CONDITION_A, text);
		SetLabelText((Enum)UI.LBL_CONDITION_B, text2);
		SetActive((Enum)UI.SPR_CONDITION_DIFFICULTY, is_visible: false);
		SetActive((Enum)UI.STR_NO_CONDITION, is_visible: true);
		bool is_visible = MonoBehaviourSingleton<PartyManager>.I.challengeInfo.NotClaer();
		SetActive((Enum)UI.SPR_CHALLENGE_NOT_CLEAR, is_visible);
		SetFontStyle((Enum)UI.LBL_CHALLENGE_NOT_CLEAR, 1);
		SetNpcMessage();
		if (!PartyManager.IsValidNotEmptyList())
		{
			SetActive((Enum)UI.GRD_QUEST, is_visible: false);
			SetActive((Enum)UI.STR_NON_LIST, is_visible: true);
			return;
		}
		PartyModel.Party[] partys = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
		SetActive((Enum)UI.GRD_QUEST, is_visible: true);
		SetActive((Enum)UI.STR_NON_LIST, is_visible: false);
		SetGrid(UI.GRD_QUEST, "QuestSearchListSelectItem", partys.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)partys[i].quest.questId);
			if (questData == null)
			{
				SetActive(t, is_visible: false);
			}
			else
			{
				SetEvent(t, "SELECT_ROOM", i);
				SetQuestData(questData, t);
				if (IsPlateChangeQuestType(questData.questType))
				{
					SetGateData(partys[i], t, questData.questType);
				}
				else
				{
					SetPartyData(partys[i], t);
				}
				SetStatusIconInfo(partys[i], t);
			}
		});
		base.UpdateUI();
	}

	protected void SetGateData(PartyModel.Party party, Transform t, QUEST_TYPE type)
	{
		int num = party.slotInfos.Count((PartyModel.SlotInfo slotInfo) => slotInfo != null && slotInfo.userInfo != null && slotInfo.userInfo.userId != party.ownerUserId);
		for (int i = 0; i < 3; i++)
		{
			SetToggle(t, ui[i], i < num);
		}
		if (type == QUEST_TYPE.GATE)
		{
			SetLabelText(t, UI.LBL_HOST_NAME, base.sectionData.GetText("GATE_QUEST_MESSAGE"));
		}
		else
		{
			SetLabelText(t, UI.LBL_HOST_NAME, string.Empty);
		}
		SetLabelText(t, UI.LBL_HOST_LV, string.Empty);
		SetLabelText(t, UI.LBL_LV, string.Empty);
	}

	protected override void SetQuestData(QuestTable.QuestTableData questData, Transform t)
	{
		ResetTween(t, UI.TWN_DIFFICULT_STAR);
		PlayTween(t, UI.TWN_DIFFICULT_STAR, forward: true, null, is_input_block: false);
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetEnemyIdByIndex(0));
		if (enemyData != null)
		{
			SetActive(t, UI.OBJ_ENEMY, is_visible: true);
			int iconId = enemyData.iconId;
			RARITY_TYPE? rarity = (questData.questType != QUEST_TYPE.ORDER) ? null : new RARITY_TYPE?(questData.rarity);
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element);
			itemIcon.SetEnableCollider(is_enable: false);
			SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
			SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
		else
		{
			SetActive(t, UI.OBJ_ENEMY, is_visible: false);
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, 6);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, is_visible: true);
		}
		Transform val = FindCtrl(t, UI.SPR_ICON_DOUBLE);
		Transform val2 = FindCtrl(t, UI.SPR_ICON_DEFENSE_BATTLE);
		Transform val3 = FindCtrl(t, UI.SPR_ICON_SERIES_OF_BATTLES);
		Transform val4 = FindCtrl(t, UI.LBL_RECRUTING_MEMBERS);
		Transform val5 = FindCtrl(t, UI.SPR_ICON_WAVE_MATCH);
		Transform val6 = FindCtrl(t, UI.SPR_WINDOW_BASE);
		if (val6 != null)
		{
			UISprite component = val6.GetComponent<UISprite>();
			Transform val7 = FindCtrl(t, UI.OBJ_SEARCH_INFO_ROOT);
			UISprite component2 = val7.GetComponent<UISprite>();
			if (IsPlateChangeQuestType(questData.questType))
			{
				component.spriteName = "QuestListPlateO";
				component2.spriteName = "SearchAdWindowO";
				val.get_gameObject().SetActive(true);
				val2.get_gameObject().SetActive(questData.questType == QUEST_TYPE.DEFENSE);
				val5.get_gameObject().SetActive(questData.questType == QUEST_TYPE.WAVE || questData.questType == QUEST_TYPE.WAVE_STRATEGY);
				val3.get_gameObject().SetActive(questData.questType == QUEST_TYPE.SERIES);
				val4.get_gameObject().SetActive(IsReqrutingMembersQuestType(questData.questType));
				string format = StringTable.Get(STRING_CATEGORY.GATE_QUEST_NAME, 0u);
				string text = string.Empty;
				if (enemyData != null)
				{
					text = string.Format(format, questData.GetMainEnemyLv(), enemyData.name);
				}
				SetLabelText(t, UI.LBL_QUEST_NAME, text);
				SetLabelText(t, UI.LBL_QUEST_NUM, string.Empty);
			}
			else
			{
				component.spriteName = "QuestListPlateN";
				component2.spriteName = "SearchAdWindow";
				val.get_gameObject().SetActive(false);
				val2.get_gameObject().SetActive(false);
				val5.get_gameObject().SetActive(false);
				val3.get_gameObject().SetActive(false);
				val4.get_gameObject().SetActive(false);
				SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
				SetLabelText(t, UI.LBL_QUEST_NUM, string.Format(base.sectionData.GetText("QUEST_NUMBER"), questData.locationNumber, questData.questNumber));
			}
		}
		else
		{
			SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
			SetLabelText(t, UI.LBL_QUEST_NUM, string.Format(base.sectionData.GetText("QUEST_NUMBER"), questData.locationNumber, questData.questNumber));
		}
		SetMemberIcon(t, questData);
	}

	private bool IsPlateChangeQuestType(QUEST_TYPE questType)
	{
		if (questType == QUEST_TYPE.GATE || questType == QUEST_TYPE.DEFENSE || questType == QUEST_TYPE.WAVE || questType == QUEST_TYPE.WAVE_STRATEGY || questType == QUEST_TYPE.SERIES)
		{
			return true;
		}
		return false;
	}

	private bool IsReqrutingMembersQuestType(QUEST_TYPE questType)
	{
		if (questType == QUEST_TYPE.DEFENSE || questType == QUEST_TYPE.WAVE || questType == QUEST_TYPE.WAVE_STRATEGY || questType == QUEST_TYPE.SERIES)
		{
			return true;
		}
		return false;
	}

	private void OnCloseDialog_QuestSearchRoomCondition()
	{
		CloseSearchRoomCondition();
	}

	protected void ResetSearchRequestTemp()
	{
		if (MonoBehaviourSingleton<PartyManager>.IsValid())
		{
			MonoBehaviourSingleton<PartyManager>.I.ResetSearchRequestTemp();
		}
	}

	protected override void OnDestroy()
	{
		ResetSearchRequestTemp();
		base.OnDestroy();
	}
}
