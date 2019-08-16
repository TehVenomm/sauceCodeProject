using System;
using UnityEngine;

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
		SetActive((Enum)UI.STR_SELL, data.CanSale());
		SetActive((Enum)UI.BTN_DETAIL_SELL, data.CanSale() && MonoBehaviourSingleton<ItemExchangeManager>.I.IsExchangeScene());
		SetLabelText((Enum)UI.LBL_NAME, data.GetName());
		SetLabelText((Enum)UI.LBL_HAVE_NUM, data.GetNum().ToString());
		SetLabelText((Enum)UI.LBL_DESCRIPTION, itemInfo.tableData.text);
		SetLabelText((Enum)UI.LBL_SELL, data.GetSalePrice().ToString());
		int num = 0;
		int num2 = 0;
		num = itemInfo.tableData.enemyIconID;
		num2 = itemInfo.tableData.enemyIconID2;
		ITEM_ICON_TYPE iconType = data.GetIconType();
		int iconID = data.GetIconID();
		RARITY_TYPE? rarity = data.GetRarity();
		Transform ctrl = GetCtrl(UI.OBJ_ICON_ROOT);
		ELEMENT_TYPE iconElement = data.GetIconElement();
		EQUIPMENT_TYPE? iconMagiEnableType = data.GetIconMagiEnableType();
		int num3 = -1;
		string event_name = null;
		int event_data = 0;
		bool is_new = false;
		int toggle_group = -1;
		bool is_select = false;
		string icon_under_text = null;
		bool is_equipping = false;
		int enemy_icon_id = num;
		int enemy_icon_id2 = num2;
		GET_TYPE getType = data.GetGetType();
		ItemIcon.Create(iconType, iconID, rarity, ctrl, iconElement, iconMagiEnableType, num3, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, enemy_icon_id, enemy_icon_id2, disable_rarity_text: false, getType);
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
