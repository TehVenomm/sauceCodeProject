public class ItemDetailEquipSetExt : GameSection
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

	private static object[] equipSetExtEventData;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as SortCompareData);
		GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.USE_ITEM, data.GetUniqID());
		base.Initialize();
	}

	public override void UpdateUI()
	{
		ItemInfo itemInfo = data.GetItemData() as ItemInfo;
		SetRenderItemModel(UI.TEX_MODEL, itemInfo.tableID);
		SetActive(UI.STR_SELL, data.CanSale());
		SetActive(UI.BTN_DETAIL_SELL, data.CanSale() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
		SetLabelText(UI.LBL_NAME, data.GetName());
		SetLabelText(UI.LBL_HAVE_NUM, data.GetNum().ToString());
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
		equipSetExtEventData = null;
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		if (MonoBehaviourSingleton<StatusManager>.I.EquipSetNum() >= constDefine.EQUIP_SET_EXT_MAX)
		{
			GameSection.ChangeEvent("OVER");
			return;
		}
		int num = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum();
		int num2 = num + constDefine.INVENTORY_EXTEND_EQUIP_SET;
		equipSetExtEventData = new object[3]
		{
			data.GetName(),
			num.ToString(),
			num2.ToString()
		};
		GameSection.SetEventData(equipSetExtEventData);
		GameSection.ChangeEvent("EXTEND");
	}

	protected void OnQuery_ItemDetailEquipSetExtConfirm_YES()
	{
		if (equipSetExtEventData == null)
		{
			Log.Error(LOG.OUTGAME, "EQUIP_SET_EXT data is NULL");
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(equipSetExtEventData);
			GameSection.StayEvent();
			MonoBehaviourSingleton<InventoryManager>.I.SendInventoryEquipSetExt(data.GetUniqID().ToString(), delegate(bool is_success)
			{
				if (is_success)
				{
					if (MonoBehaviourSingleton<StatusManager>.IsValid())
					{
						MonoBehaviourSingleton<StatusManager>.I.ResetEquipSetInfo();
					}
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_EQUIP_SET_INFO);
				}
				GameSection.ResumeEvent(is_success);
			});
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
