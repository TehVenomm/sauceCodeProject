public class ItemDetailSellQuest : ItemDetailSellBase
{
	public override void UpdateUI()
	{
		if (data is QuestSortData && MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(data.GetTableID()) != null)
		{
			base.UpdateUI();
			SetActive(UI.OBJ_MONEY_ROOT, is_visible: false);
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
