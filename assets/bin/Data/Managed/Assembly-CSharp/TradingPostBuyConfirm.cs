using Network;
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
		SetLabelText(UI.LBL_NAME, detail.from);
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
			if (data.GetUniqID() != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
		}
		ItemIcon itemIcon = null;
		itemIcon = ((data.GetIconType() != ITEM_ICON_TYPE.QUEST_ITEM) ? ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id) : ItemIcon.Create(new ItemIcon.ItemIconCreateParam
		{
			icon_type = data.GetIconType(),
			icon_id = data.GetIconID(),
			rarity = data.GetRarity(),
			parent = holder,
			element = data.GetIconElement(),
			magi_enable_equip_type = data.GetIconMagiEnableType(),
			num = data.GetNum(),
			enemy_icon_id = enemy_icon_id,
			questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
		}));
		itemIcon.SetRewardBG(is_visible: false);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}
}
