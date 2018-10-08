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
				this.StopCoroutine(c);
			}
		});
		coroutineList.Clear();
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		base.UpdateUI();
		object[] array = (object[])GameSection.GetEventData();
		uint materialID = (uint)array[0];
		CreateEquipItemTable.CreateEquipItemData[] equipItems = Singleton<CreateEquipItemTable>.I.GetSortedCreateEquipItemsByPart(materialID);
		_003CUpdateUI_003Ec__AnonStorey443 _003CUpdateUI_003Ec__AnonStorey;
		SetTable(UI.TBL_LIST, "GachaEquipItem", equipItems.Length, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void SetItemIcon(uint itemID, Transform trans, UI target)
	{
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(itemID);
		ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.ITEM, itemData.iconID, itemData.rarity, FindCtrl(trans, target), ELEMENT_TYPE.MAX, null, -1, null, 0, false, -1, false, null, false, itemData.enemyIconID, itemData.enemyIconID2, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		if (itemIcon != null)
		{
			SetMaterialInfo(itemIcon.transform, REWARD_TYPE.ITEM, itemData.id, GetCtrl(UI.PNL_MATERIAL_INFO));
		}
	}

	private IEnumerator LoadEquipModel(Transform t, Enum _enum, uint item_id)
	{
		yield return (object)null;
		SetRenderEquipModel(t, _enum, item_id, -1, -1, 1f);
	}

	public void OnQuery_GACHA_DETAIL_MAX_PARAM_FROM_NEWS()
	{
		object[] array = GameSection.GetEventData() as object[];
		uint num = (uint)array[0];
		int num2 = (int)array[1];
		CreateEquipItemTable.CreateEquipItemData[] sortedCreateEquipItemsByPart = Singleton<CreateEquipItemTable>.I.GetSortedCreateEquipItemsByPart(num);
		if (num2 >= sortedCreateEquipItemsByPart.Length || num2 <= -1)
		{
			GameSection.StopEvent();
		}
		else
		{
			CreateEquipItemTable.CreateEquipItemData createEquipItemData = sortedCreateEquipItemsByPart[num2];
			uint equipItemID = createEquipItemData.equipItemID;
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(equipItemID);
			GameSection.ChangeEvent("DETAIL_MAX_PARAM", new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.GACHA_EQUIP_PREVIEW,
				equipItemData,
				num
			});
		}
	}
}
