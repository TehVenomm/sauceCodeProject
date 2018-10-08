using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultEventRewardDialog : ItemSellConfirm
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
		LBL_REWARD_TITLE,
		GRD_EVENT_REWARD,
		OBJ_SCROLL_VIEW
	}

	private int totalGold;

	private int crystalNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalGold = (int)array[1];
		crystalNum = (int)array[2];
		SetLabelText((Enum)UI.LBL_REWARD_TITLE, (string)array[3]);
		base.isRareConfirm = true;
		base.isHideMainText = true;
		base.isButtonSingle = true;
		base.Initialize();
	}

	private void OnQuery_OK()
	{
		GameSection.BackSection();
	}

	protected unsafe override void DrawIcon()
	{
		SortCompareData[] itemData = sellData.ToArray();
		int reward_num = itemData.Length;
		if (crystalNum > 0)
		{
			reward_num++;
		}
		if (totalGold > 0)
		{
			reward_num++;
		}
		bool shouldAddGold = totalGold > 0;
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		_003CDrawIcon_003Ec__AnonStorey42B _003CDrawIcon_003Ec__AnonStorey42B;
		SetGrid(UI.GRD_EVENT_REWARD, null, sELL_SELECT_MAX, false, new Action<int, Transform, bool>((object)_003CDrawIcon_003Ec__AnonStorey42B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override int GetSellGold()
	{
		return totalGold;
	}
}
