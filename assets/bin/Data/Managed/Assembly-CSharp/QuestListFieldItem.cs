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
		SetActive(transform, UI.OBJ_FIELD_ICON, is_visible: true);
		SetLabelText(transform, UI.LBL_FIELD_NAME, field_name);
		SetLabelText(transform, UI.LBL_FIELD_ENEMY_NAME, point_data.pointViewTable.itemDetailText);
		ResourceLoad.LoadGatherPointIconTexture(FindCtrl(transform, UI.TEX_FIELD).GetComponent<UITexture>(), point_data.pointViewTable.iconID);
		SetActive(transform, UI.TEX_FIELD_SUB, is_visible: true);
		ResourceLoad.LoadGatherPointIconTexture(FindCtrl(transform, UI.TEX_FIELD_SUB).GetComponent<UITexture>(), point_data.pointViewTable.iconID);
	}

	protected void SetUpEnemy(EnemyTable.EnemyData field_enemy_table)
	{
		Transform transform = GetTransform();
		SetActive(transform, UI.TEX_FIELD_SUB, is_visible: false);
		string name = field_enemy_table.name;
		SetLabelText(transform, UI.LBL_FIELD_ENEMY_NAME, name);
		if (field_enemy_table != null)
		{
			int iconId = field_enemy_table.iconId;
			ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, null, FindCtrl(transform, UI.OBJ_ENEMY), field_enemy_table.element).SetEnableCollider(is_enable: false);
		}
	}

	private void SetUpField(ItemToFieldTable.ItemDetailToFieldData _field_data)
	{
		Transform transform = GetTransform();
		SetActive(transform, UI.OBJ_FIELD_ICON, is_visible: true);
		SetActive(transform, UI.TEX_FIELD_SUB, is_visible: false);
		SetLabelText(transform, UI.LBL_FIELD_NAME, _field_data.mapData.mapName);
		SetActive(transform, UI.LBL_FIELD_NAME, is_visible: true);
		ResourceLoad.LoadFieldIconTexture(FindCtrl(transform, UI.TEX_FIELD).GetComponent<UITexture>(), _field_data.mapData);
		SetActive(transform, UI.TEX_FIELD, is_visible: true);
	}

	protected Transform GetTransform()
	{
		Transform transform = base._transform;
		if (transform == null)
		{
			transform = base.transform;
		}
		return transform;
	}
}
