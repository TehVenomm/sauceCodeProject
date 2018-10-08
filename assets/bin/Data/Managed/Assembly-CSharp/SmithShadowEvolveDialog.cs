using Network;
using System;

public class SmithShadowEvolveDialog : GameSection
{
	private enum UI
	{
		OBJ_ICON_ROOT,
		BTN_OK,
		BTN_INACTIVE
	}

	private EquipItemInfo itemInfo;

	private EquipItemTable.EquipItemData shadowEvolveData;

	private CreateEquipItemTable.CreateEquipItemData createData;

	public override void Initialize()
	{
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		itemInfo = smithData.selectEquipData;
		shadowEvolveData = itemInfo.tableData.GetShadowEvolveEquipTable();
		EquipItemTable.EquipItemData rootEquipTable = shadowEvolveData.GetRootEquipTable();
		createData = Singleton<CreateEquipItemTable>.I.GetCreateItemDataByEquipItem(rootEquipTable.id);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(createData.needMaterial[0].itemID);
		if (itemData != null)
		{
			int haveingItemNum = MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemData.id);
			int num = createData.needMaterial[0].num;
			bool flag = haveingItemNum >= num;
			ItemIcon itemIcon = ItemIconMaterial.CreateMaterialIcon(ItemIcon.GetItemIconType(itemData.type), itemData, GetCtrl(UI.OBJ_ICON_ROOT), haveingItemNum, num, "MATERIAL", 0, false);
			SetMaterialInfo(itemIcon._transform, REWARD_TYPE.ITEM, itemData.id, null);
			SetActive((Enum)UI.BTN_OK, flag);
			SetActive((Enum)UI.BTN_INACTIVE, !flag);
			base.UpdateUI();
		}
	}

	private SmithManager.ResultData CreateResultData()
	{
		SmithManager.ResultData resultData = new SmithManager.ResultData();
		resultData.beforeRarity = (int)itemInfo.tableData.rarity;
		resultData.beforeLevel = itemInfo.level;
		resultData.beforeMaxLevel = itemInfo.tableData.maxLv;
		resultData.beforeExceedCnt = itemInfo.exceed;
		resultData.beforeAtk = itemInfo.atk;
		resultData.beforeDef = itemInfo.def;
		resultData.beforeHp = itemInfo.hp;
		resultData.beforeElemAtk = itemInfo.elemAtk;
		resultData.beforeElemDef = itemInfo.elemDef;
		return resultData;
	}

	private void OnQuery_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			itemInfo.tableData.name
		});
	}

	private unsafe void OnQuery_SmithShadowEvolveConfirm_YES()
	{
		SmithManager.ResultData result_data = CreateResultData();
		GameSection.SetEventData(result_data);
		GameSection.StayEvent();
		_003COnQuery_SmithShadowEvolveConfirm_YES_003Ec__AnonStorey477 _003COnQuery_SmithShadowEvolveConfirm_YES_003Ec__AnonStorey;
		MonoBehaviourSingleton<SmithManager>.I.SendShadowEvolveEquipItem(itemInfo.uniqueID, createData.needMaterial[0].itemID, new Action<Error, EquipItemInfo>((object)_003COnQuery_SmithShadowEvolveConfirm_YES_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}
}
