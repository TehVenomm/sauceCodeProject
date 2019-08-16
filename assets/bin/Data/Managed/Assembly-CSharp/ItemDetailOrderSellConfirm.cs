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

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_TITLE_U, base.sectionData.GetText("TITLE"));
		SetLabelText((Enum)UI.LBL_TITLE_D, base.sectionData.GetText("TITLE"));
		int num = 0;
		int num2 = 0;
		QuestItemInfo item_info = itemData.GetItemData() as QuestItemInfo;
		SetGrid(UI.GRD_ICON, string.Empty, 1, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			uint num3 = 0u;
			EquipItemExceedTable.EquipItemExceedData equipItemExceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedData(item_info.infoData.questData.tableData.rarity, item_info.infoData.questData.tableData.getType, item_info.infoData.questData.tableData.eventId);
			if (equipItemExceedData != null)
			{
				num3 = equipItemExceedData.exchangeItemId;
			}
			REWARD_TYPE rEWARD_TYPE = REWARD_TYPE.ITEM;
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(rEWARD_TYPE, num3, t, sellNum);
			SetMaterialInfo(itemIcon.transform, rEWARD_TYPE, num3);
			itemIcon.SetRewardBG(is_visible: true);
		});
		SetLabelText((Enum)UI.LBL_GOLD, num.ToString());
		SetLabelText((Enum)UI.LBL_EXP, num2.ToString());
		SetLabelText((Enum)UI.LBL_SELL, string.Format(base.sectionData.GetText("STR_SELL"), itemData.GetName(), sellNum));
	}

	private void OnQuery_OK()
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		list.Add(itemData.GetUniqID().ToString());
		list2.Add(sellNum);
		GameSection.StayEvent();
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendSellQuest(list, list2, delegate(bool is_success, SellQuestItemReward reward, List<uint> empty_quest_item_list)
		{
			empty_quest_item_list.ForEach(delegate(uint empty_data)
			{
				if (itemData.GetTableID() == empty_data)
				{
					GameSection.ChangeStayEvent("CLOSE_DETAIL");
				}
			});
			GameSection.ResumeEvent(is_success);
		});
	}
}
