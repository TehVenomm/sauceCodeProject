using UnityEngine;

public class ItemIconDetailEquipAbilitySetupper : ItemIconDetailEquipSetupper
{
	public override void Set(object[] data)
	{
		base.Set((object[])null);
		SkillSlotUIData[] slot_data = data[1] as SkillSlotUIData[];
		bool is_show_main_status = (bool)data[2];
		infoRootAry[0].SetActive(value: true);
		if (gridEquipMark == null)
		{
			gridEquipMark = spEquipIndex.gameObject.GetComponentInParent<UIGrid>();
		}
		if (data[0] is EquipItemInfo)
		{
			SetData(data[0] as EquipItemInfo, slot_data, is_show_main_status, -1);
		}
		else
		{
			Set(data[0] as EquipItemTable.EquipItemData, slot_data, is_show_main_status);
		}
	}

	protected void SetData(EquipItemInfo item, SkillSlotUIData[] slot_data, bool is_show_main_status, int equipping_sp_index)
	{
		SetEquipIndexSprite(equipping_sp_index);
		SetFavorite(item.isFavorite);
		SetIconStatusSprite(ItemIconDetail.ICON_STATUS.NONE);
		bool flag = item.tableData.IsWeapon();
		if (flag)
		{
			SetElement(item.GetTargetElement(), item.elemAtk, isWeapon: true);
		}
		else
		{
			int num = item.elemDef;
			if (item.tableData.isFormer)
			{
				num = Mathf.FloorToInt((float)num * 0.1f);
			}
			SetElement(item.GetTargetElement(), num, isWeapon: false);
		}
		if (is_show_main_status)
		{
			infoRootAry[1].SetActive(value: true);
			infoRootAry[2].SetActive(value: false);
			SetVisibleBG(is_visible: true);
			SetName(item.tableData.name);
			SetLevel(item.level, item.tableData.maxLv, item.tableData.IsVisual());
			SetEquipValue(flag, flag ? item.atk : item.def);
			return;
		}
		infoRootAry[1].SetActive(value: false);
		infoRootAry[2].SetActive(value: true);
		SetVisibleBG(is_visible: true);
		SetName(item.tableData.name);
		bool enabled = true;
		EquipItemAbility[] ability = item.ability;
		objAbilityRoot.GetComponentsInChildren(Temporary.uiLabelList);
		int i = 0;
		for (int count = Temporary.uiLabelList.Count; i < count; i++)
		{
			UILabel uILabel = Temporary.uiLabelList[i];
			uILabel.enabled = (i < ability.Length && ability[i].id != 0 && ability[i].ap > 0);
			if (uILabel.enabled)
			{
				uILabel.text = ability[i].GetNameAndAP();
				enabled = false;
			}
		}
		Temporary.uiLabelList.Clear();
		lblNonAbility.enabled = enabled;
	}
}
