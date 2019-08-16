using System;

public class ItemDetailSellQuest : ItemDetailSellBase
{
	public override void UpdateUI()
	{
		QuestSortData questSortData = data as QuestSortData;
		if (questSortData != null)
		{
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(data.GetTableID());
			if (questItem != null)
			{
				base.UpdateUI();
				SetActive((Enum)UI.OBJ_MONEY_ROOT, is_visible: false);
			}
		}
	}

	protected void OnQuery_SALE()
	{
		int sliderNum = GetSliderNum();
		GameSection.SetEventData(new object[2]
		{
			data,
			sliderNum
		});
	}
}
