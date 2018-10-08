using UnityEngine;

public class QuestListFieldItem : UIBehaviour
{
	private enum UI
	{
		LBL_FIELD_ENEMY_NAME,
		OBJ_ENEMY,
		LBL_FIELD_NAME,
		TEX_FIELD,
		TEX_FIELD_SUB,
		OBJ_FIELD_ICON
	}

	public void SetUpFieldEnemy(EnemyTable.EnemyData field_enemy_table, ItemToFieldTable.ItemDetailToFieldData _field_data)
	{
		if (field_enemy_table != null)
		{
			SetUpEnemy(field_enemy_table);
			SetUpField(_field_data);
		}
	}

	public void SetUpGather(string field_name, ItemToFieldTable.ItemDetailToFieldPointData point_data)
	{
		Transform transform = GetTransform();
		SetActive(transform, UI.OBJ_FIELD_ICON, true);
		SetLabelText(transform, UI.LBL_FIELD_NAME, field_name);
		SetLabelText(transform, UI.LBL_FIELD_ENEMY_NAME, point_data.pointViewTable.itemDetailText);
		UITexture component = FindCtrl(transform, UI.TEX_FIELD).GetComponent<UITexture>();
		ResourceLoad.LoadGatherPointIconTexture(component, point_data.pointViewTable.iconID);
		SetActive(transform, UI.TEX_FIELD_SUB, true);
		Transform val = FindCtrl(transform, UI.TEX_FIELD_SUB);
		UITexture component2 = val.GetComponent<UITexture>();
		ResourceLoad.LoadGatherPointIconTexture(component2, point_data.pointViewTable.iconID);
	}

	protected void SetUpEnemy(EnemyTable.EnemyData field_enemy_table)
	{
		Transform transform = GetTransform();
		SetActive(transform, UI.TEX_FIELD_SUB, false);
		string name = field_enemy_table.name;
		SetLabelText(transform, UI.LBL_FIELD_ENEMY_NAME, name);
		if (field_enemy_table != null)
		{
			int iconId = field_enemy_table.iconId;
			ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, null, FindCtrl(transform, UI.OBJ_ENEMY), field_enemy_table.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
			itemIcon.SetEnableCollider(false);
		}
	}

	private void SetUpField(ItemToFieldTable.ItemDetailToFieldData _field_data)
	{
		Transform transform = GetTransform();
		SetActive(transform, UI.OBJ_FIELD_ICON, true);
		SetActive(transform, UI.TEX_FIELD_SUB, false);
		SetLabelText(transform, UI.LBL_FIELD_NAME, _field_data.mapData.mapName);
		SetActive(transform, UI.LBL_FIELD_NAME, true);
		UITexture component = FindCtrl(transform, UI.TEX_FIELD).GetComponent<UITexture>();
		ResourceLoad.LoadFieldIconTexture(component, _field_data.mapData);
		SetActive(transform, UI.TEX_FIELD, true);
	}

	protected Transform GetTransform()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		Transform val = base._transform;
		if (val == null)
		{
			val = this.get_transform();
		}
		return val;
	}
}
