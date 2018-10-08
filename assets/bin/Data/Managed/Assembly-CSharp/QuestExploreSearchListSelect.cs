using Network;
using System;
using UnityEngine;

public class QuestExploreSearchListSelect : QuestSearchListSelectBase
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
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		SPR_TYPE_DIFFICULTY
	}

	private const string LIST_ITEM_PREFAB_NAME = "QuestExploreSearchListSelectItem";

	private int eventId;

	public override void Initialize()
	{
		eventId = (int)GameSection.GetEventData();
		base.Initialize();
	}

	protected unsafe override void SendSearchRequest(Action onFinish, Action<bool> cb)
	{
		_003CSendSearchRequest_003Ec__AnonStorey41B _003CSendSearchRequest_003Ec__AnonStorey41B;
		MonoBehaviourSingleton<PartyManager>.I.SendEventSearch(eventId, new Action<bool, Error>((object)_003CSendSearchRequest_003Ec__AnonStorey41B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override void ResetSearchRequest()
	{
	}

	public unsafe override void UpdateUI()
	{
		if (!PartyManager.IsValidNotEmptyList())
		{
			SetActive((Enum)UI.GRD_QUEST, false);
			SetActive((Enum)UI.STR_NON_LIST, true);
		}
		else
		{
			PartyModel.Party[] partys = MonoBehaviourSingleton<PartyManager>.I.partys.ToArray();
			SetActive((Enum)UI.GRD_QUEST, true);
			SetActive((Enum)UI.STR_NON_LIST, false);
			_003CUpdateUI_003Ec__AnonStorey41C _003CUpdateUI_003Ec__AnonStorey41C;
			SetGrid(UI.GRD_QUEST, "QuestExploreSearchListSelectItem", partys.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey41C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
			SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
			SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
		}
	}

	private void SetDeliveryData(uint questId, Transform t)
	{
		DeliveryTable.DeliveryData deliveryTableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questId);
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, (deliveryTableDataFromQuestId != null && deliveryTableDataFromQuestId.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}
}
