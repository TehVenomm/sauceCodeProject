using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopGachaReward : GameSection
{
	private enum UI
	{
		GRD_ICON
	}

	private List<QuestItem.SellItem> sellItem;

	public override void Initialize()
	{
		sellItem = (GameSection.GetEventData() as List<QuestItem.SellItem>);
		sellItem.Sort((QuestItem.SellItem l, QuestItem.SellItem r) => l.pri - r.pri);
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		QuestItem.SellItem[] data_ary = sellItem.ToArray();
		int item_num = data_ary.Length;
		_003CUpdateUI_003Ec__AnonStorey463 _003CUpdateUI_003Ec__AnonStorey;
		SetGrid(UI.GRD_ICON, null, item_num, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
