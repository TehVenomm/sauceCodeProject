// Decompiled with JetBrains decompiler
// Type: RegionMapDescriptionEnemyItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class RegionMapDescriptionEnemyItem : QuestListFieldItem
{
  public void SetUpEnemyOnly(EnemyTable.EnemyData field_enemy_table, int level)
  {
    this.SetUpEnemy(field_enemy_table);
    Transform transform = this.GetTransform();
    this.SetActive(transform, (Enum) RegionMapDescriptionEnemyItem.UI.LBL_FIELD_NAME, true);
    this.SetLabelText(transform, (Enum) RegionMapDescriptionEnemyItem.UI.LBL_FIELD_NAME, "Lv." + level.ToString());
    this.SetActive(transform, (Enum) RegionMapDescriptionEnemyItem.UI.OBJ_FIELD_ICON, false);
    this.SetActive(transform, (Enum) RegionMapDescriptionEnemyItem.UI.TEX_FIELD_SUB, false);
    this.SetActive(transform, (Enum) RegionMapDescriptionEnemyItem.UI.TEX_FIELD, false);
  }

  private new enum UI
  {
    LBL_FIELD_ENEMY_NAME,
    OBJ_ENEMY,
    LBL_FIELD_NAME,
    TEX_FIELD,
    TEX_FIELD_SUB,
    OBJ_FIELD_ICON,
  }
}
