using System;
using UnityEngine;

public class ItemDetailEquipSkillSelectDialog : ItemDetailEquipSkillSelect
{
	private object initData;

	public override void Initialize()
	{
		initData = GameSection.GetEventData();
		base.Initialize();
	}

	protected unsafe override bool CheckApplicationVersion()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		if (selectSkillItem != null && !MonoBehaviourSingleton<GameSceneManager>.I.CheckSkillItemAndOpenUpdateAppDialog(selectSkillItem.tableData, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
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
		Debug.LogWarning((object)"OnQuery_DETAIL");
		selectIndex = (int)GameSection.GetEventData();
		ItemDetailSkillSimpleDialog.InitParam eventData = new ItemDetailSkillSimpleDialog.InitParam(CreateDetailEventData(selectIndex), initData);
		GameSection.SetEventData(eventData);
	}
}
