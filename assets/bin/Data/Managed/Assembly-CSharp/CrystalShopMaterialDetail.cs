using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalShopMaterialDetail : GameSection
{
	private enum UI
	{
		SPR_SALE,
		OBJ_BUY,
		SPR_BUY_NOW,
		LBL_PRICE,
		SCR_DETAIL,
		GRD_DETAIL,
		PNL_MATERIAL_INFO
	}

	private const string NORMAL_DROP_EFF_NAME = "ef_ui_dropitem_silver_01";

	private const string RARE_DROP_EFF_NAME = "ef_ui_dropitem_gold_01";

	private const string BREAK_DROP_EFF_NAME = "ef_ui_dropitem_red_01";

	private ProductData materialData;

	private string priceStr = string.Empty;

	private List<ItemSortData> datas;

	private int index;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		materialData = (array[0] as ProductData);
		priceStr = (array[1] as string);
		index = (int)array[2];
		InventoryList<ItemInfo, Item> inventoryList = ItemInfo.CreateList(materialData.items);
		datas = new List<ItemSortData>();
		for (LinkedListNode<ItemInfo> linkedListNode = inventoryList.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode != null && linkedListNode.Value != null && linkedListNode.Value.tableData != null)
			{
				ItemSortData itemSortData = new ItemSortData();
				itemSortData.SetItem(linkedListNode.Value);
				datas.Add(itemSortData);
			}
		}
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		string buttonName = "BTN_SHOP_NORMAL1";
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject lo_button = loadQueue.Load(RESOURCE_CATEGORY.GACHA_BUTTON, buttonName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		GameObject buttonObj = Object.Instantiate(lo_button.loadedObject) as GameObject;
		buttonObj.transform.parent = FindCtrl(base._transform, UI.OBJ_BUY);
		buttonObj.transform.localScale = new Vector3(1f, 1f, 1f);
		buttonObj.transform.localPosition = new Vector3(0f, 0f, 0f);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText(base._transform, UI.LBL_PRICE, priceStr);
		SetActive(UI.SPR_SALE, materialData.offerType == 3);
		SetGrid(UI.GRD_DETAIL, null, datas.Count, true, delegate(int i, Transform t, bool is_recycle)
		{
			ItemSortData data = datas[i];
			SetItemIcon(t, data, i);
		});
	}

	private bool IsRare(SortCompareData icon_base)
	{
		if (icon_base != null)
		{
			return GameDefine.IsRare(icon_base.GetRarity());
		}
		return false;
	}

	private bool IsBreakReward(SortCompareData icon_base)
	{
		if (icon_base != null)
		{
			return icon_base.GetCategory() == REWARD_CATEGORY.BREAK;
		}
		return false;
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
			num = data.GetNum();
			if (num == 1)
			{
				num = -1;
			}
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0L)
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
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, false, null, false, enemy_icon_id, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		}
		itemIcon.SetRewardBG(true);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}

	private void OnQuery_BUY()
	{
		RequestEvent("BUY", index);
	}
}
