using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGachaEquipListTop : GameSection
{
	private enum UI
	{
		SCR_LIST,
		TBL_LIST,
		PNL_MATERIAL_INFO,
		LBL_NAME,
		TEX_EQUIP_MODEL,
		ITEM_ICON_1,
		ITEM_ICON_2,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		BTN_EQUIP_MODEL
	}

	private List<Coroutine> coroutineList = new List<Coroutine>();

	private void StopLoadCoroutine()
	{
		coroutineList.ForEach(delegate(Coroutine c)
		{
			if (c != null)
			{
				StopCoroutine(c);
			}
		});
		coroutineList.Clear();
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		object[] array = (object[])GameSection.GetEventData();
		uint materialID = (uint)array[0];
		CreateEquipItemTable.CreateEquipItemData[] equipItems = Singleton<CreateEquipItemTable>.I.GetSortedCreateEquipItemsByPart(materialID);
		SetTable(UI.TBL_LIST, "GachaEquipItem", equipItems.Length, reset: false, delegate(int i, Transform t, bool b)
		{
			CreateEquipItemTable.CreateEquipItemData obj = equipItems[i];
			uint equipItemID = obj.equipItemID;
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemID);
			SetLabelText(t, UI.LBL_NAME, equipItemData.name);
			SetEquipmentTypeIcon(t, UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, equipItemData);
			NeedMaterial[] needMaterial = obj.needMaterial;
			NeedMaterial needMaterial2 = (needMaterial.Length >= 1) ? needMaterial[0] : null;
			if (needMaterial2 != null)
			{
				SetItemIcon(needMaterial2.itemID, t, UI.ITEM_ICON_1);
			}
			NeedMaterial needMaterial3 = (needMaterial.Length >= 2) ? needMaterial[1] : null;
			if (needMaterial3 != null)
			{
				SetItemIcon(needMaterial3.itemID, t, UI.ITEM_ICON_2);
			}
			Coroutine item = StartCoroutine(LoadEquipModel(t, UI.TEX_EQUIP_MODEL, equipItemData.id));
			coroutineList.Add(item);
			Transform t2 = FindCtrl(t, UI.BTN_EQUIP_MODEL);
			SetEvent(t2, "DETAIL_MAX_PARAM", new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.GACHA_EQUIP_PREVIEW,
				equipItemData,
				materialID
			});
		});
	}

	private void SetItemIcon(uint itemID, Transform trans, UI target)
	{
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemID);
		ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.ITEM, itemData.iconID, itemData.rarity, FindCtrl(trans, target), ELEMENT_TYPE.MAX, null, -1, null, 0, is_new: false, -1, is_select: false, null, is_equipping: false, itemData.enemyIconID, itemData.enemyIconID2);
		if (itemIcon != null)
		{
			SetMaterialInfo(itemIcon.transform, REWARD_TYPE.ITEM, itemData.id, GetCtrl(UI.PNL_MATERIAL_INFO));
		}
	}

	private IEnumerator LoadEquipModel(Transform t, Enum _enum, uint item_id)
	{
		yield return null;
		SetRenderEquipModel(t, _enum, item_id);
	}

	public void OnQuery_GACHA_DETAIL_MAX_PARAM_FROM_NEWS()
	{
		object[] obj = GameSection.GetEventData() as object[];
		uint num = (uint)obj[0];
		int num2 = (int)obj[1];
		CreateEquipItemTable.CreateEquipItemData[] sortedCreateEquipItemsByPart = Singleton<CreateEquipItemTable>.I.GetSortedCreateEquipItemsByPart(num);
		if (num2 >= sortedCreateEquipItemsByPart.Length || num2 <= -1)
		{
			GameSection.StopEvent();
			return;
		}
		uint equipItemID = sortedCreateEquipItemsByPart[num2].equipItemID;
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemID);
		GameSection.ChangeEvent("DETAIL_MAX_PARAM", new object[3]
		{
			ItemDetailEquip.CURRENT_SECTION.GACHA_EQUIP_PREVIEW,
			equipItemData,
			num
		});
	}
}
