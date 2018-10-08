using UnityEngine;

public class RegionMapDescriptionEnemyItem : QuestListFieldItem
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

	public void SetUpEnemyOnly(EnemyTable.EnemyData field_enemy_table, int level)
	{
		SetUpEnemy(field_enemy_table);
		Transform transform = GetTransform();
		SetActive(transform, UI.LBL_FIELD_NAME, true);
		SetLabelText(transform, UI.LBL_FIELD_NAME, "Lv." + level.ToString());
		SetActive(transform, UI.OBJ_FIELD_ICON, false);
		SetActive(transform, UI.TEX_FIELD_SUB, false);
		SetActive(transform, UI.TEX_FIELD, false);
	}
}
