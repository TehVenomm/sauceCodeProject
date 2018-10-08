using Network;
using System;
using UnityEngine;

public class QuestWaveSearchListSelect : QuestSearchListSelectBase
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
		LBL_QUEST_NAME,
		STR_NON_LIST,
		OBJ_ENEMY,
		SPR_TYPE_DIFFICULTY
	}

	private const string LIST_ITEM_PREFAB_NAME = "QuestWaveSearchListSelectItem";

	private int eventId;

	public override void Initialize()
	{
		eventId = (int)GameSection.GetEventData();
		base.Initialize();
	}

	protected override void SendSearchRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendEventSearch(eventId, delegate(bool is_success, Error err)
		{
			onFinish();
			if (!is_success && base.isInitialized)
			{
				if (err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
				{
					GameSection.ChangeStayEvent("NOT_FOUND_QUEST", null);
					if (cb != null)
					{
						cb(true);
					}
				}
			}
			else if (cb != null)
			{
				cb(is_success);
			}
		});
	}

	protected override void ResetSearchRequest()
	{
	}

	public override void UpdateUI()
	{
		if (!PartyManager.IsValidNotEmptyList())
		{
			SetActive(UI.GRD_QUEST, false);
			SetActive(UI.STR_NON_LIST, true);
		}
		else
		{
			PartyModel.Party[] partys = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
			SetActive(UI.GRD_QUEST, true);
			SetActive(UI.STR_NON_LIST, false);
			SetGrid(UI.GRD_QUEST, "QuestWaveSearchListSelectItem", partys.Length, false, delegate(int i, Transform t, bool is_recycle)
			{
				QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)partys[i].quest.questId);
				if (questData == null)
				{
					SetActive(t, false);
				}
				else
				{
					SetEvent(t, "SELECT_ROOM", i);
					SetQuestData(questData, t);
					SetPartyData(partys[i], t);
					SetDeliveryData(questData.questID, t);
					SetStatusIconInfo(partys[i], t);
					SetMemberIcon(t, questData);
				}
			});
			base.UpdateUI();
		}
	}

	protected override void SetQuestData(QuestTable.QuestTableData questData, Transform t)
	{
		SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.GetMainEnemyID());
		if (enemyData != null)
		{
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, questData.rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		}
	}

	private void SetDeliveryData(uint questId, Transform t)
	{
		DeliveryTable.DeliveryData deliveryTableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questId);
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, (deliveryTableDataFromQuestId != null && deliveryTableDataFromQuestId.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
