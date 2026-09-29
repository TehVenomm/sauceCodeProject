// Decompiled with JetBrains decompiler
// Type: ItemIconDetailEquipAbilitySetupper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ItemIconDetailEquipAbilitySetupper : ItemIconDetailEquipSetupper
{
  public override void Set(object[] data)
  {
    base.Set((object[]) null);
    SkillSlotUIData[] slot_data = data[1] as SkillSlotUIData[];
    bool is_show_main_status = (bool) data[2];
    this.infoRootAry[0].SetActive(true);
    if (Object.op_Equality((Object) this.gridEquipMark, (Object) null))
      this.gridEquipMark = ((Component) this.spEquipIndex).gameObject.GetComponentInParent<UIGrid>();
    if (data[0] is EquipItemInfo)
      this.SetData(data[0] as EquipItemInfo, slot_data, is_show_main_status, -1);
    else
      this.Set(data[0] as EquipItemTable.EquipItemData, slot_data, is_show_main_status);
  }

  protected void SetData(
    EquipItemInfo item,
    SkillSlotUIData[] slot_data,
    bool is_show_main_status,
    int equipping_sp_index)
  {
    this.SetEquipIndexSprite(equipping_sp_index);
    this.SetFavorite(item.isFavorite);
    this.SetIconStatusSprite(ItemIconDetail.ICON_STATUS.NONE);
    bool is_weapon = item.tableData.IsWeapon();
    if (is_weapon)
    {
      this.SetElement(item.GetTargetElement(), item.elemAtk, true);
    }
    else
    {
      int elemDef = item.elemDef;
      if (item.tableData.isFormer)
        elemDef = Mathf.FloorToInt((float) elemDef * 0.1f);
      this.SetElement(item.GetTargetElement(), elemDef, false);
    }
    if (is_show_main_status)
    {
      this.infoRootAry[1].SetActive(true);
      this.infoRootAry[2].SetActive(false);
      this.SetVisibleBG(true);
      this.SetName(item.tableData.name);
      this.SetLevel(item.level, item.tableData.maxLv, item.tableData.IsVisual());
      this.SetEquipValue(is_weapon, is_weapon ? item.atk : item.def);
    }
    else
    {
      this.infoRootAry[1].SetActive(false);
      this.infoRootAry[2].SetActive(true);
      this.SetVisibleBG(true);
      this.SetName(item.tableData.name);
      bool flag = true;
      EquipItemAbility[] ability = item.ability;
      this.objAbilityRoot.GetComponentsInChildren<UILabel>(Temporary.uiLabelList);
      int index = 0;
      for (int count = Temporary.uiLabelList.Count; index < count; ++index)
      {
        UILabel uiLabel = Temporary.uiLabelList[index];
        ((Behaviour) uiLabel).enabled = index < ability.Length && ability[index].id != 0U && ability[index].ap > 0;
        if (((Behaviour) uiLabel).enabled)
        {
          uiLabel.text = ability[index].GetNameAndAP();
          flag = false;
        }
      }
      Temporary.uiLabelList.Clear();
      ((Behaviour) this.lblNonAbility).enabled = flag;
    }
  }
}
