using Network;
using System;
using UnityEngine;

public class TradingPostBuyConfirm : GameSection
{
	private enum UI
	{
		LBL_COST,
		LBL_COST_D,
		LBL_GEM,
		LBL_QUATITY,
		LBL_NAME,
		OBJ_ICON,
		PNL_MATERIAL_INFO
	}

	private TradingPostDetail detail;

	private int i;

	public override void Initialize()
	{
		detail = (GameSection.GetEventData() as TradingPostDetail);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_COST, detail.price);
		SetLabelText(UI.LBL_COST_D, detail.price);
		SetLabelText(UI.LBL_GEM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal);
		SetLabelText(UI.LBL_QUATITY, detail.quantity);
		SetLabelText((Enum)UI.LBL_NAME, detail.from);
		ItemInfo item = ItemInfo.CreateItemInfo(detail.itemId);
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item);
		SetItemIcon(GetCtrl(UI.OBJ_ICON), itemSortData);
	}

	private void OnQuery_YES()
	{
		GameSection.ChangeEvent("[BACK]");
		RequestEvent("PURCHASE");
	}

	private void SetItemIcon(Transform holder, ItemSortData data, int event_data = 0)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int icon_id = -1;
		int num = -1;
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		}
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(data.GetTableID());
			enemy_icon_id = itemData.enemyIconID;
		}
		ItemIcon itemIcon = null;
		if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = data.GetIconType();
			itemIconCreateParam.icon_id = data.GetIconID();
			itemIconCreateParam.rarity = data.GetRarity();
			itemIconCreateParam.parent = holder;
			itemIconCreateParam.element = data.GetIconElement();
			itemIconCreateParam.magi_enable_equip_type = data.GetIconMagiEnableType();
			itemIconCreateParam.num = data.GetNum();
			itemIconCreateParam.enemy_icon_id = enemy_icon_id;
			itemIconCreateParam.questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST;
			itemIcon = ItemIcon.Create(itemIconCreateParam);
		}
		else
		{
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id);
		}
		itemIcon.SetRewardBG(is_visible: false);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}
}
