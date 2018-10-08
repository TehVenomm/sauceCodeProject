using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultMissionClearRewardDialog : ItemSellConfirm
{
	public new enum UI
	{
		STR_INCLUDE_RARE,
		STR_MAIN_TEXT,
		STR_TITLE_R,
		GRD_ICON,
		LBL_TOTAL,
		OBJ_GOLD,
		BTN_0,
		BTN_1,
		BTN_CENTER,
		SCR_ICON,
		GRD_REWARD_ICON,
		OBJ_COMPLETE_ROOT,
		OBJ_NORMAL_ROOT
	}

	private int totalGold;

	private int crystalNum;

	private bool isComplete;

	private PointShopResultData missionPointData;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalGold = (int)array[1];
		crystalNum = (int)array[2];
		isComplete = (bool)array[3];
		missionPointData = (PointShopResultData)array[4];
		base.isRareConfirm = true;
		base.isHideMainText = true;
		base.isButtonSingle = true;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive((Enum)UI.OBJ_NORMAL_ROOT, !isComplete);
		SetActive((Enum)UI.OBJ_COMPLETE_ROOT, isComplete);
	}

	private void OnQuery_OK()
	{
		GameSection.BackSection();
	}

	protected unsafe override void DrawIcon()
	{
		SortCompareData[] sell_data_ary = sellData.ToArray();
		int reward_num = sell_data_ary.Length;
		if (crystalNum > 0)
		{
			reward_num++;
		}
		if (totalGold > 0)
		{
			reward_num++;
		}
		if (missionPointData != null && missionPointData.missionPoint > 0)
		{
			reward_num++;
		}
		bool shouldAddGold = totalGold > 0;
		bool shouldAddMissionPoint = missionPointData != null && missionPointData.missionPoint > 0;
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		_003CDrawIcon_003Ec__AnonStorey43E _003CDrawIcon_003Ec__AnonStorey43E;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, false, new Action<int, Transform, bool>((object)_003CDrawIcon_003Ec__AnonStorey43E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override int GetSellGold()
	{
		return totalGold;
	}
}
