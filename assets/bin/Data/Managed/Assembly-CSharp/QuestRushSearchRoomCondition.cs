using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestRushSearchRoomCondition : QuestSearchRoomConditionBase
{
	private enum UI
	{
		POP_TARGET_MIN_FLOOR,
		POP_TARGET_MAX_FLOOR,
		LBL_TARGET_MIN_FLOOR,
		LBL_TARGET_MAX_FLOOR
	}

	public class RushSearchRequestParam
	{
		public int minFloorQuestId;

		public int maxFloorQuestId;

		public RushSearchRequestParam(int minQuestId, int maxQuestId)
		{
			minFloorQuestId = minQuestId;
			maxFloorQuestId = maxQuestId;
		}

		public RushSearchRequestParam()
		{
		}
	}

	private RushSearchRequestParam searchParam = new RushSearchRequestParam();

	private List<int> questIdList = new List<int>();

	private string[] maxFloorList;

	private string[] minFloorList;

	private Transform minFloorPopup;

	private Transform maxFloorPopup;

	private static readonly string PopUpPrefabName = "ScrollablePopupList";

	public override void Initialize()
	{
		questIdList = MonoBehaviourSingleton<PartyManager>.I.nowRushQuestIds;
		LoadSearchRequestParam();
		CopySearchRequestParam();
		CreateFloorPopText();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateMinFloor();
		UpdateMaxFloor();
	}

	private void UpdateMinFloor()
	{
		int currentSelectMinFloorIndex = getCurrentSelectMinFloorIndex();
		SetLabelText((Enum)UI.LBL_TARGET_MIN_FLOOR, minFloorList[currentSelectMinFloorIndex]);
	}

	private void UpdateMaxFloor()
	{
		int currentSelectMaxFloorIndex = getCurrentSelectMaxFloorIndex();
		SetLabelText((Enum)UI.LBL_TARGET_MAX_FLOOR, maxFloorList[currentSelectMaxFloorIndex]);
	}

	protected override void LoadSearchRequestParam()
	{
		MonoBehaviourSingleton<PartyManager>.I.SetRushRequestFromPrefs();
		bool flag = true;
		if (questIdList != null)
		{
			flag &= questIdList.Contains(MonoBehaviourSingleton<PartyManager>.I.rushSearchRequest.minFloorQuestId);
			flag &= questIdList.Contains(MonoBehaviourSingleton<PartyManager>.I.rushSearchRequest.maxFloorQuestId);
		}
		if (!flag)
		{
			RushSearchRequestParam rushSearchRequest = new RushSearchRequestParam(questIdList[0], questIdList[questIdList.Count - 1]);
			MonoBehaviourSingleton<PartyManager>.I.SetRushSearchRequest(rushSearchRequest);
		}
	}

	protected override void CopySearchRequestParam()
	{
		RushSearchRequestParam rushSearchRequest = MonoBehaviourSingleton<PartyManager>.I.rushSearchRequest;
		searchParam.minFloorQuestId = rushSearchRequest.minFloorQuestId;
		searchParam.maxFloorQuestId = rushSearchRequest.maxFloorQuestId;
	}

	protected override void SetCondition()
	{
		MonoBehaviourSingleton<PartyManager>.I.SetRushSearchRequest(searchParam);
	}

	protected override void SendSearch()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendRushSearch(delegate(bool is_success, Error err)
		{
			if (!is_success && err == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST)
			{
				OnNotFoundQuest();
			}
			GameSection.ResumeEvent(true, null);
		}, true);
	}

	protected override void SendRandomMatching()
	{
		GameSection.SetEventData(new object[1]
		{
			false
		});
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendRushSearchRandomMatching(delegate(bool is_success, Error err)
		{
			if (!is_success)
			{
				OnNotFoundMatchingParty();
			}
			GameSection.ResumeEvent(true, null);
		});
	}

	private void CreateFloorPopText()
	{
		int count = questIdList.Count;
		int num = 0;
		maxFloorList = new string[count];
		minFloorList = new string[count];
		for (int i = 0; i < count; i++)
		{
			minFloorList[i] = (num + 1).ToString();
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)questIdList[i]);
			List<QuestTable.QuestTableData> sameRushQuestData = QuestTable.GetSameRushQuestData(questData.rushId);
			int num2 = sameRushQuestData.Count - 1;
			num += num2;
			maxFloorList[i] = num.ToString();
		}
	}

	public void OnQuery_TARGET_MIN_FLOOR()
	{
		ShowMinFloorPopup();
	}

	public void OnQuery_TARGET_MAX_FLOOR()
	{
		ShowMaxFloorPopup();
	}

	private void ShowMinFloorPopup()
	{
		if (minFloorPopup == null)
		{
			minFloorPopup = Realizes(PopUpPrefabName, GetCtrl(UI.POP_TARGET_MIN_FLOOR), false);
		}
		if (!(minFloorPopup == null))
		{
			int currentSelectMinFloorIndex = getCurrentSelectMinFloorIndex();
			bool[] array = new bool[minFloorList.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i <= getCurrentSelectMaxFloorIndex());
			}
			UIScrollablePopupList.CreatePopup(minFloorPopup, GetCtrl(UI.POP_TARGET_MIN_FLOOR), 7, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, minFloorList, array, currentSelectMinFloorIndex, delegate(int index)
			{
				searchParam.minFloorQuestId = questIdList[index];
				RefreshUI();
			});
		}
	}

	private void ShowMaxFloorPopup()
	{
		if (maxFloorPopup == null)
		{
			maxFloorPopup = Realizes(PopUpPrefabName, GetCtrl(UI.POP_TARGET_MAX_FLOOR), false);
		}
		if (!(maxFloorPopup == null))
		{
			int currentSelectMaxFloorIndex = getCurrentSelectMaxFloorIndex();
			bool[] array = new bool[maxFloorList.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (i >= getCurrentSelectMinFloorIndex());
			}
			UIScrollablePopupList.CreatePopup(maxFloorPopup, GetCtrl(UI.POP_TARGET_MAX_FLOOR), 7, UIScrollablePopupList.ATTACH_DIRECTION.BOTTOM, true, maxFloorList, array, currentSelectMaxFloorIndex, delegate(int index)
			{
				searchParam.maxFloorQuestId = questIdList[index];
				RefreshUI();
			});
		}
	}

	private int getCurrentSelectMinFloorIndex()
	{
		return getFloorIndex(searchParam.minFloorQuestId);
	}

	private int getCurrentSelectMaxFloorIndex()
	{
		return getFloorIndex(searchParam.maxFloorQuestId);
	}

	private int getFloorIndex(int questId)
	{
		int num = questIdList.IndexOf(questId);
		if (num <= -1)
		{
			num = 0;
		}
		return num;
	}
}
