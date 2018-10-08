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
		TEX_RUSH_IMAGE
	}

	private QuestRushSearchRoomCondition.RushSearchRequestParam defaultParam;

	private List<int> questIdList = new List<int>();

	public override void Initialize()
	{
		questIdList = MonoBehaviourSingleton<PartyManager>.I.nowRushQuestIds;
		base.Initialize();
	}

	protected override void SendSearchRequest(Action onFinish, Action<bool> cb)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendRushSearch(delegate(bool is_success, Error err)
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
		}, false);
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

	public override void UpdateUI()
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
			SetGrid(UI.GRD_QUEST, "QuestRushSearchListSelectItem", partys.Length, false, delegate(int i, Transform t, bool is_recycle)
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
				}
			});
			base.UpdateUI();
		}
	}

	protected override void SetQuestData(QuestTable.QuestTableData questData, Transform t)
	{
		SetLabelText(t, UI.LBL_QUEST_NAME, questData.questText);
		UITexture component = FindCtrl(t, UI.TEX_RUSH_IMAGE).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.RUSH_QUEST_ICON, ResourceName.GetRushQuestIconName((int)questData.rushIconId));
	}

	private void OnCloseDialog_QuestRushSearchRoomCondition()
	{
		CloseSearchRoomCondition();
	}
}
