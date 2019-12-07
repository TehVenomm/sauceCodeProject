using System.Collections.Generic;

public class ItemDetailLithographSellConfirm : ItemStorageSellConfirm
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
		STR_NON_REWARD
	}

	private SortCompareData item;

	private int num;

	private bool m_isRareConfirm;

	public override string overrideBackKeyEvent => "NO";

	protected override bool isShowIconBG()
	{
		return true;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		item = (array[0] as SortCompareData);
		num = (int)array[1];
		List<SortCompareData> list = new List<SortCompareData>();
		list.Add(item);
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetHierarchyList().Exists((GameSectionHierarchy.HierarchyData x) => x.section.name == "ItemStorageSell"))
		{
			GameSection.SetEventData(new object[3]
			{
				ItemStorageTop.TAB_MODE.MATERIAL,
				list,
				GO_BACK.SELL
			});
		}
		else
		{
			GameSection.SetEventData(new object[3]
			{
				ItemStorageTop.TAB_MODE.MATERIAL,
				list,
				GO_BACK.TOP
			});
		}
		base.Initialize();
		m_isRareConfirm = false;
		list.ForEach(delegate(SortCompareData sort_data)
		{
			if (!m_isRareConfirm && GameDefine.IsRareLithograph(sort_data.GetRarity()))
			{
				m_isRareConfirm = true;
			}
		});
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	protected override NeedMaterial[] CreateNeedMaterialAry()
	{
		List<NeedMaterial> list = new List<NeedMaterial>();
		ItemInfo itemInfo = item.GetItemData() as ItemInfo;
		if (itemInfo != null && itemInfo.tableData.type == ITEM_TYPE.LITHOGRAPH)
		{
			uint num = 0u;
			EquipItemExceedTable.EquipItemExceedData equipItemExceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedData(itemInfo.tableData.rarity, itemInfo.tableData.getType, itemInfo.tableData.eventId);
			if (equipItemExceedData != null)
			{
				num = equipItemExceedData.exchangeItemId;
			}
			if (num != 0 && Singleton<ItemTable>.I.GetItemData(num) != null)
			{
				list.Add(new NeedMaterial(num, this.num));
			}
		}
		return list.ToArray();
	}

	protected override int GetTargetIconNum(SortCompareData[] sell_data_ary, int i)
	{
		return num;
	}

	protected override int GetSellGold()
	{
		return item.GetSalePrice() * num;
	}

	public void OnQuery_YES()
	{
		if (m_isRareConfirm)
		{
			GameSection.SetEventData(new object[1]
			{
				item.GetRarity().ToString()
			});
			GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM");
		}
		else
		{
			OnQuery_ItemDetailLithographSellIncludeRareConfirm_YES();
		}
	}

	public void OnQuery_ItemDetailLithographSellIncludeRareConfirm_YES()
	{
		List<string> list = new List<string>();
		List<int> list2 = new List<int>();
		list.Add(item.GetUniqID().ToString());
		list2.Add(num);
		if (num >= item.GetNum())
		{
			GameSection.ChangeEvent("CLOSE_DETAIL");
		}
		GameSection.StayEvent();
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellItem(list, list2, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	public void OnQuery_ItemDetailLithographSellIncludeRareConfirm_NO()
	{
	}

	protected override void ChangeEventForGoBack()
	{
	}
}
