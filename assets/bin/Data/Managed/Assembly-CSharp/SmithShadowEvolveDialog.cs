using Network;

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
			ItemIcon itemIcon = ItemIconMaterial.CreateMaterialIcon(ItemIcon.GetItemIconType(itemData.type), itemData, GetCtrl(UI.OBJ_ICON_ROOT), haveingItemNum, num, "MATERIAL");
			SetMaterialInfo(itemIcon._transform, REWARD_TYPE.ITEM, itemData.id);
			SetActive(UI.BTN_OK, flag);
			SetActive(UI.BTN_INACTIVE, !flag);
			base.UpdateUI();
		}
	}

	private SmithManager.ResultData CreateResultData()
	{
		return new SmithManager.ResultData
		{
			beforeRarity = (int)itemInfo.tableData.rarity,
			beforeLevel = itemInfo.level,
			beforeMaxLevel = itemInfo.tableData.maxLv,
			beforeExceedCnt = itemInfo.exceed,
			beforeAtk = itemInfo.atk,
			beforeDef = itemInfo.def,
			beforeHp = itemInfo.hp,
			beforeElemAtk = itemInfo.elemAtk,
			beforeElemDef = itemInfo.elemDef
		};
	}

	private void OnQuery_YES()
	{
		GameSection.SetEventData(new object[1]
		{
			itemInfo.tableData.name
		});
	}

	private void OnQuery_SmithShadowEvolveConfirm_YES()
	{
		SmithManager.ResultData result_data = CreateResultData();
		GameSection.SetEventData(result_data);
		GameSection.StayEvent();
		MonoBehaviourSingleton<SmithManager>.I.SendShadowEvolveEquipItem(itemInfo.uniqueID, createData.needMaterial[0].itemID, delegate(Error error, EquipItemInfo info)
		{
			if (error == Error.None)
			{
				result_data.itemData = info;
				MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData = info;
				MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
				MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
				GameSection.ResumeEvent(is_resume: true);
			}
			else
			{
				GameSection.ResumeEvent(is_resume: false);
			}
		});
	}
}
