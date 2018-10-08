using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestResultFirstClearRewardDialog : ItemSellConfirm
{
	private int totalSell;

	private int crystalNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		sellData = (array[0] as List<SortCompareData>);
		totalSell = (int)array[1];
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
		SortCompareData[] sell_data_ary = sellData.ToArray();
		int reward_num = sell_data_ary.Length;
		if (crystalNum > 0)
		{
			reward_num++;
		}
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		_003CDrawIcon_003Ec__AnonStorey42C _003CDrawIcon_003Ec__AnonStorey42C;
		SetGrid(UI.GRD_ICON, null, sELL_SELECT_MAX, false, new Action<int, Transform, bool>((object)_003CDrawIcon_003Ec__AnonStorey42C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override int GetSellGold()
	{
		return totalSell;
	}
}
