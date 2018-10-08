using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailOrderSellConfirm : GameSection
{
	private enum UI
	{
		GRD_ICON,
		LBL_SELL,
		LBL_GOLD,
		LBL_EXP,
		LBL_TITLE_U,
		LBL_TITLE_D
	}

	private QuestSortData itemData;

	private int sellNum;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "EquipItemExceedTable";
		}
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		itemData = (array[0] as QuestSortData);
		sellNum = (int)array[1];
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_TITLE_U, base.sectionData.GetText("TITLE"));
		SetLabelText((Enum)UI.LBL_TITLE_D, base.sectionData.GetText("TITLE"));
		int num = 0;
		int num2 = 0;
		QuestItemInfo item_info = itemData.GetItemData() as QuestItemInfo;
		_003CUpdateUI_003Ec__AnonStorey3D6 _003CUpdateUI_003Ec__AnonStorey3D;
		SetGrid(UI.GRD_ICON, string.Empty, 1, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey3D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetLabelText((Enum)UI.LBL_GOLD, num.ToString());
		SetLabelText((Enum)UI.LBL_EXP, num2.ToString());
		SetLabelText((Enum)UI.LBL_SELL, string.Format(base.sectionData.GetText("STR_SELL"), itemData.GetName(), sellNum));
	}

	private unsafe void OnQuery_OK()
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		list.Add(itemData.GetUniqID().ToString());
		list2.Add(sellNum);
		GameSection.StayEvent();
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendSellQuest(list, list2, new Action<bool, SellQuestItemReward, List<uint>>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
