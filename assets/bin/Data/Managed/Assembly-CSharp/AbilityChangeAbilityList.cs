// Decompiled with JetBrains decompiler
// Type: AbilityChangeAbilityList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AbilityChangeAbilityList : UIBehaviour
{
  private EquipItemInfo equipItemInfo;
  private AbilityItemInfo abilityItemInfo;

  public bool EnableAbilityChange { get; private set; }

  public void SetParameter(EquipItemInfo equipItemInfo)
  {
    this.equipItemInfo = equipItemInfo;
    this.abilityItemInfo = equipItemInfo.GetAbilityItem();
    this.EnableAbilityChange = true;
    if (this.uiFirstUpdate)
      return;
    this.RefreshUI();
  }

  public override void UpdateUI()
  {
    if (this.equipItemInfo == null)
      return;
    this.SetFontStyle((Enum) AbilityChangeAbilityList.UI.STR_ABILITY, (FontStyle) 2);
    this.SetFontStyle((Enum) AbilityChangeAbilityList.UI.LBL_NAME_1, (FontStyle) 3);
    this.SetFontStyle((Enum) AbilityChangeAbilityList.UI.LBL_NAME_2, (FontStyle) 3);
    this.SetFontStyle((Enum) AbilityChangeAbilityList.UI.LBL_NAME_3, (FontStyle) 3);
    this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_NAME, this.equipItemInfo.tableData.name);
    this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_LV_NOW, this.equipItemInfo.level.ToString());
    this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_LV_MAX, this.equipItemInfo.tableData.maxLv.ToString());
    this.SetEquipmentTypeIcon((Enum) AbilityChangeAbilityList.UI.SPR_TYPE_ICON, (Enum) AbilityChangeAbilityList.UI.SPR_TYPE_ICON_BG, (Enum) AbilityChangeAbilityList.UI.SPR_TYPE_ICON_RARITY, this.equipItemInfo.tableData);
    EquipItemAbility[] validAbility = this.equipItemInfo.GetValidAbility();
    bool flag = false;
    int num = validAbility.Length > 3 ? 3 : validAbility.Length;
    int index;
    for (index = 0; index < num; ++index)
    {
      if (this.equipItemInfo.IsFixedAbility(index))
      {
        this.SetAbilityActive(index, false);
        this.SetAbilityActive(index + 3, true);
        this.SetAbilityData(index + 3, validAbility[index]);
      }
      else
      {
        flag = true;
        this.SetAbilityActive(index, true);
        this.SetAbilityData(index, validAbility[index]);
      }
    }
    this.EnableAbilityChange = flag;
    for (; index < 3; ++index)
      this.SetAbilityActive(index, false);
    bool is_visible1 = this.abilityItemInfo != null;
    bool is_visible2 = !this.equipItemInfo.tableData.IsEquipableAbilityItem() || !is_visible1;
    this.SetActive((Enum) AbilityChangeAbilityList.UI.OBJ_ABILITY_ITEM_ITEM_ROOT, is_visible1);
    this.SetActive((Enum) AbilityChangeAbilityList.UI.LBL_NO_ABILITY_ITEM, is_visible2);
    if (is_visible2)
    {
      if (!this.equipItemInfo.tableData.IsEquipableAbilityItem())
        this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_NO_ABILITY_ITEM, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 23U));
      else
        this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_NO_ABILITY_ITEM, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 24U));
    }
    else
      this.SetAbilityItemData();
  }

  private void SetAbilityActive(int index, bool active)
  {
    AbilityChangeAbilityList.UI[] uiArray = new AbilityChangeAbilityList.UI[6]
    {
      AbilityChangeAbilityList.UI.SPR_ABILITY_1,
      AbilityChangeAbilityList.UI.SPR_ABILITY_2,
      AbilityChangeAbilityList.UI.SPR_ABILITY_3,
      AbilityChangeAbilityList.UI.SPR_ABILITY_4,
      AbilityChangeAbilityList.UI.SPR_ABILITY_5,
      AbilityChangeAbilityList.UI.SPR_ABILITY_6
    };
    if (0 > index || 5 < index)
      return;
    this.SetActive((Enum) uiArray[index], active);
  }

  private void SetAbilityData(int index, EquipItemAbility ability)
  {
    AbilityChangeAbilityList.UI label_enum1 = AbilityChangeAbilityList.UI.LBL_NAME_1;
    AbilityChangeAbilityList.UI label_enum2 = AbilityChangeAbilityList.UI.LBL_POINT_1;
    AbilityChangeAbilityList.UI label_enum3 = AbilityChangeAbilityList.UI.LBL_DESC_1;
    AbilityChangeAbilityList.UI[] uiArray1 = new AbilityChangeAbilityList.UI[6]
    {
      AbilityChangeAbilityList.UI.LBL_NAME_1,
      AbilityChangeAbilityList.UI.LBL_NAME_2,
      AbilityChangeAbilityList.UI.LBL_NAME_3,
      AbilityChangeAbilityList.UI.LBL_NAME_4,
      AbilityChangeAbilityList.UI.LBL_NAME_5,
      AbilityChangeAbilityList.UI.LBL_NAME_6
    };
    AbilityChangeAbilityList.UI[] uiArray2 = new AbilityChangeAbilityList.UI[6]
    {
      AbilityChangeAbilityList.UI.LBL_POINT_1,
      AbilityChangeAbilityList.UI.LBL_POINT_2,
      AbilityChangeAbilityList.UI.LBL_POINT_3,
      AbilityChangeAbilityList.UI.LBL_POINT_4,
      AbilityChangeAbilityList.UI.LBL_POINT_5,
      AbilityChangeAbilityList.UI.LBL_POINT_6
    };
    AbilityChangeAbilityList.UI[] uiArray3 = new AbilityChangeAbilityList.UI[6]
    {
      AbilityChangeAbilityList.UI.LBL_DESC_1,
      AbilityChangeAbilityList.UI.LBL_DESC_2,
      AbilityChangeAbilityList.UI.LBL_DESC_3,
      AbilityChangeAbilityList.UI.LBL_DESC_4,
      AbilityChangeAbilityList.UI.LBL_DESC_5,
      AbilityChangeAbilityList.UI.LBL_DESC_6
    };
    if (0 <= index && 5 >= index)
    {
      label_enum1 = uiArray1[index];
      label_enum2 = uiArray2[index];
      label_enum3 = uiArray3[index];
    }
    AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(ability.id, ability.ap) ?? Singleton<AbilityDataTable>.I.GetMinimumAbilityData(ability.id);
    this.SetLabelText((Enum) label_enum1, ability.GetName());
    this.SetLabelText((Enum) label_enum2, ability.GetAP());
    this.SetLabelText((Enum) label_enum3, abilityData.description);
  }

  private void SetAbilityItemData()
  {
    if (this.abilityItemInfo == null)
      return;
    this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_ABILITY_ITEM_NAME, this.abilityItemInfo.GetName());
    this.SetLabelText((Enum) AbilityChangeAbilityList.UI.LBL_ABILITY_ITEM_DESC, this.abilityItemInfo.GetDescription());
  }

  private enum UI
  {
    STR_ABILITY,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    SPR_ABILITY_1,
    SPR_ABILITY_2,
    SPR_ABILITY_3,
    SPR_ABILITY_4,
    SPR_ABILITY_5,
    SPR_ABILITY_6,
    LBL_NAME_1,
    LBL_NAME_2,
    LBL_NAME_3,
    LBL_NAME_4,
    LBL_NAME_5,
    LBL_NAME_6,
    LBL_POINT_1,
    LBL_POINT_2,
    LBL_POINT_3,
    LBL_POINT_4,
    LBL_POINT_5,
    LBL_POINT_6,
    LBL_DESC_1,
    LBL_DESC_2,
    LBL_DESC_3,
    LBL_DESC_4,
    LBL_DESC_5,
    LBL_DESC_6,
    OBJ_ABILITY_ITEM_ITEM_ROOT,
    LBL_NO_ABILITY_ITEM,
    LBL_ABILITY_ITEM_NAME,
    LBL_ABILITY_ITEM_DESC,
  }
}
