public class ItemDetailUseItem : GameSection
{
	protected enum UI
	{
		OBJ_DETAIL_ROOT,
		BTN_DETAIL_SELL,
		TEX_MODEL,
		OBJ_ICON_ROOT,
		LBL_NAME,
		LBL_SELL,
		LBL_HAVE_NUM,
		LBL_DESCRIPTION,
		STR_SELL,
		OBJ_BACK,
		BTN_USE
	}

	private SortCompareData data;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as SortCompareData);
		GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.USE_ITEM, data.GetUniqID());
		base.Initialize();
	}

	public override void UpdateUI()
	{
		ItemInfo itemInfo = data.GetItemData() as ItemInfo;
		SetActive(UI.STR_SELL, data.CanSale());
		SetActive(UI.BTN_DETAIL_SELL, data.CanSale() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
		SetLabelText(UI.LBL_NAME, data.GetName());
		SetLabelText(UI.LBL_HAVE_NUM, "Qty:" + data.GetNum().ToString());
		SetLabelText(UI.LBL_DESCRIPTION, itemInfo.tableData.text);
		SetLabelText(UI.LBL_SELL, data.GetSalePrice().ToString());
		int num = 0;
		int num2 = 0;
		num = itemInfo.tableData.enemyIconID;
		num2 = itemInfo.tableData.enemyIconID2;
		ItemIcon.Create(data.GetIconType(), data.GetIconID(), data.GetRarity(), GetCtrl(UI.OBJ_ICON_ROOT), data.GetIconElement(), data.GetIconMagiEnableType(), -1, null, 0, is_new: false, -1, is_select: false, null, is_equipping: false, num, num2, disable_rarity_text: false, data.GetGetType());
	}

	public void OnQuery_USE()
	{
		ItemInfo item = data.GetItemData() as ItemInfo;
		if (MonoBehaviourSingleton<StatusManager>.I.IsEffectedItem(item))
		{
			GameSection.ChangeEvent("OVER_WRITE_BOOST", new object[1]
			{
				data.GetName()
			});
		}
		else
		{
			GameSection.SetEventData(new object[1]
			{
				data.GetName()
			});
		}
	}

	protected void OnQuery_ItemDetailUseConfirm_YES()
	{
		SendUseItem();
	}

	protected void OnQuery_ItemDetailUseOverWriteConfirm_YES()
	{
		SendUseItem();
	}

	protected void SendUseItem()
	{
		GameSection.StayEvent();
		ItemInfo itemInfo = data.GetItemData() as ItemInfo;
		if (itemInfo != null && itemInfo.tableData != null)
		{
			if (itemInfo.tableData.id == 7500101 || itemInfo.tableData.id == 7500102)
			{
				MonoBehaviourSingleton<InventoryManager>.I.SendInventoryAutoItem(data.GetUniqID().ToString(), delegate(bool is_success)
				{
					GameSection.ResumeEvent(is_success);
				});
			}
			else
			{
				MonoBehaviourSingleton<InventoryManager>.I.SendInventoryUseItem(data.GetUniqID().ToString(), delegate(bool is_success)
				{
					if (is_success && FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
					{
						MonoBehaviourSingleton<CoopNetworkManager>.I.UpdateBoost();
					}
					GameSection.ResumeEvent(is_success);
				});
			}
		}
	}

	private void OnQuery_SELL()
	{
		if (!CanSell())
		{
			GameSection.ChangeEvent("NOT_SELL");
		}
		GameSection.SetEventData(data);
	}

	private bool CanSell()
	{
		if (data == null || !data.CanSale())
		{
			return false;
		}
		return true;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			ItemInfo itemInfo = MonoBehaviourSingleton<InventoryManager>.I.itemInventory.Find(data.GetUniqID());
			if (itemInfo != null && itemInfo.num > 0)
			{
				data = new ItemSortData();
				data.SetItem(itemInfo);
			}
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_ITEM_INVENTORY;
	}
}
