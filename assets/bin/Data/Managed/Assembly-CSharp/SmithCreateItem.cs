public class SmithCreateItem : EquipGenerateBase
{
	public override void Initialize()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		smithType = SmithType.GENERATE;
		GameSection.SetEventData(smithData.generateTableData);
		base.Initialize();
		EquipItemTable.EquipItemData equipTableData = GetEquipTableData();
		if (equipTableData != null)
		{
			string caption = (!equipTableData.IsWeapon()) ? base.sectionData.GetText("CAPTION_DEFENCE") : base.sectionData.GetText("CAPTION_WEAPON");
			MonoBehaviourSingleton<UIManager>.I.common.AttachCaption(this, base.sectionData.backButtonIndex, caption);
		}
	}

	protected override void InitNeedMaterialData()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		needMaterial = MaterialSort(smithData.createEquipItemTable.needMaterial);
		needMoney = smithData.createEquipItemTable.needMoney;
		CheckNeedMaterialNumFromInventory();
	}

	protected override void OnQuery_START()
	{
		if (MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(GetEquipTableData()))
		{
			GameSection.ChangeEvent("START_REGISTED", null);
		}
		base.OnQuery_START();
	}

	protected void OnQuery_LOTTERY_LIST()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		GameSection.SetEventData(new object[2]
		{
			smithData,
			SmithType.GENERATE
		});
	}

	private void OnQuery_SmithConfirmCreate_YES()
	{
		OnQueryConfirmYES();
	}

	private void OnQuery_SmithConfirmCreateRegisted_YES()
	{
		OnQueryConfirmYES();
	}

	protected override uint GetCreateEquiptableID()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		return smithData.createEquipItemTable.id;
	}
}
