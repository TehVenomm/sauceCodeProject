using UnityEngine;

public class ItemDetailEquipSkillSelectDialog : ItemDetailEquipSkillSelect
{
	private object initData;

	public override void Initialize()
	{
		initData = GameSection.GetEventData();
		base.Initialize();
	}

	protected override bool CheckApplicationVersion()
	{
		if (selectSkillItem != null && !MonoBehaviourSingleton<GameSceneManager>.I.CheckSkillItemAndOpenUpdateAppDialog(selectSkillItem.tableData, OnCancelSelect))
		{
			return false;
		}
		return base.CheckApplicationVersion();
	}

	private void OnCancelSelect()
	{
		selectSkillItem = equipSkillItem;
		selectIndex = GetSelectItemIndex(selectSkillItem);
		SetDirty(UI.GRD_INVENTORY);
		RefreshUI();
	}

	protected override object[] CreateDetailEventData(int index)
	{
		return new object[2]
		{
			callSection,
			inventory.datas[index].GetItemData()
		};
	}

	private void OnQuery_DETAIL()
	{
		Debug.LogWarning("OnQuery_DETAIL");
		selectIndex = (int)GameSection.GetEventData();
		GameSection.SetEventData(new ItemDetailSkillSimpleDialog.InitParam(CreateDetailEventData(selectIndex), initData));
	}
}
