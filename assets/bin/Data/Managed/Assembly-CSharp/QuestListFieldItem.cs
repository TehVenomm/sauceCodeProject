// Decompiled with JetBrains decompiler
// Type: QuestListFieldItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestListFieldItem : UIBehaviour
{
  public void SetUpFieldEnemy(
    EnemyTable.EnemyData field_enemy_table,
    ItemToFieldTable.ItemDetailToFieldData _field_data)
  {
    if (field_enemy_table == null)
      return;
    this.SetUpEnemy(field_enemy_table);
    this.SetUpField(_field_data);
  }

  public void SetUpGather(
    string field_name,
    ItemToFieldTable.ItemDetailToFieldPointData point_data)
  {
    Transform transform = this.GetTransform();
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.OBJ_FIELD_ICON, true);
    this.SetLabelText(transform, (Enum) QuestListFieldItem.UI.LBL_FIELD_NAME, field_name);
    this.SetLabelText(transform, (Enum) QuestListFieldItem.UI.LBL_FIELD_ENEMY_NAME, point_data.pointViewTable.itemDetailText);
    ResourceLoad.LoadGatherPointIconTexture(((Component) this.FindCtrl(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD)).GetComponent<UITexture>(), point_data.pointViewTable.iconID);
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD_SUB, true);
    ResourceLoad.LoadGatherPointIconTexture(((Component) this.FindCtrl(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD_SUB)).GetComponent<UITexture>(), point_data.pointViewTable.iconID);
  }

  protected void SetUpEnemy(EnemyTable.EnemyData field_enemy_table)
  {
    Transform transform = this.GetTransform();
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD_SUB, false);
    string name = field_enemy_table.name;
    this.SetLabelText(transform, (Enum) QuestListFieldItem.UI.LBL_FIELD_ENEMY_NAME, name);
    if (field_enemy_table == null)
      return;
    ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, field_enemy_table.iconId, new RARITY_TYPE?(), this.FindCtrl(transform, (Enum) QuestListFieldItem.UI.OBJ_ENEMY), field_enemy_table.element).SetEnableCollider(false);
  }

  private void SetUpField(ItemToFieldTable.ItemDetailToFieldData _field_data)
  {
    Transform transform = this.GetTransform();
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.OBJ_FIELD_ICON, true);
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD_SUB, false);
    this.SetLabelText(transform, (Enum) QuestListFieldItem.UI.LBL_FIELD_NAME, _field_data.mapData.mapName);
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.LBL_FIELD_NAME, true);
    ResourceLoad.LoadFieldIconTexture(((Component) this.FindCtrl(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD)).GetComponent<UITexture>(), _field_data.mapData);
    this.SetActive(transform, (Enum) QuestListFieldItem.UI.TEX_FIELD, true);
  }

  protected Transform GetTransform()
  {
    Transform transform = this._transform;
    if (Object.op_Equality((Object) transform, (Object) null))
      transform = ((Component) this).transform;
    return transform;
  }

  private enum UI
  {
    LBL_FIELD_ENEMY_NAME,
    OBJ_ENEMY,
    LBL_FIELD_NAME,
    TEX_FIELD,
    TEX_FIELD_SUB,
    OBJ_FIELD_ICON,
  }
}
