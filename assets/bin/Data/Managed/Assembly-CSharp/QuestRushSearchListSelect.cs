using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestRushSearchListSelect : QuestSearchListSelectBase
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
		TEX_RUSH_IMAGE,
		SPR_TYPE_DIFFICULTY
	}

	private const string LIST_ITEM_PREFAB_NAME = "QuestRushSearchListSelectItem";

	private QuestRushSearchRoomCondition.RushSearchRequestParam defaultParam;

	private List<int> questIdList = new List<int>();

	public override void Initialize()
	{
		questIdList = MonoBehaviourSingleton<PartyManager>.I.nowRushQuestIds;
		base.Initialize();
	}

	protected unsafe override void SendSearchRequest(Action onFinish, Action<bool> cb)
	{
		_003CSendSearchRequest_003Ec__AnonStorey405 _003CSendSearchRequest_003Ec__AnonStorey;
		MonoBehaviourSingleton<PartyManager>.I.SendRushSearch(new Action<bool, Error>((object)_003CSendSearchRequest_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), false);
	}

	protected override void ResetSearchRequest()
	{
		MonoBehaviourSingleton<PartyManager>.I.ResetRushSearchRequest();
		if (defaultParam == null)
		{
			if (questIdList != null)
			{
				defaultParam = new QuestRushSearchRoomCondition.RushSearchRequestParam(questIdList.First(), questIdList.Last());
			}
			else
			{
				defaultParam = new QuestRushSearchRoomCondition.RushSearchRequestParam(0, 0);
			}
		}
		MonoBehaviourSingleton<PartyManager>.I.SetRushSearchRequest(defaultParam);
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
			_003CUpdateUI_003Ec__AnonStorey406 _003CUpdateUI_003Ec__AnonStorey;
			SetGrid(UI.GRD_QUEST, "QuestRushSearchListSelectItem", partys.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			base.UpdateUI();
		}
	}

	protected override void SetQuestData(QuestTable.QuestTableData questData, Transform t)
	{
		SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
		if (questData.userNumLimit < 4)
		{
			SetActive(t, UI.TGL_MEMBER_3, false);
		}
		if (questData.userNumLimit < 3)
		{
			SetActive(t, UI.TGL_MEMBER_2, false);
		}
		if (questData.userNumLimit < 2)
		{
			SetActive(t, UI.TGL_MEMBER_1, false);
		}
		UITexture component = FindCtrl(t, UI.TEX_RUSH_IMAGE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int)questData.rushIconId));
	}

	private void SetDeliveryData(uint questId, Transform t)
	{
		DeliveryTable.DeliveryData deliveryTableDataFromQuestId = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(questId);
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, (deliveryTableDataFromQuestId != null && deliveryTableDataFromQuestId.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}

	private void OnCloseDialog_QuestRushSearchRoomCondition()
	{
		CloseSearchRoomCondition();
	}
}
