// Decompiled with JetBrains decompiler
// Type: EquipSetDetailStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class EquipSetDetailStatus : EquipSetDetailStatusAndAbilityTable
{
  public override void Initialize()
  {
    this.InitializeCaption();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateUIStatus();
    base.UpdateUI();
    this.UpdateWeaponIcon();
  }

  protected override void OnQuery_INDEX_L()
  {
    base.OnQuery_INDEX_L();
    this.UpdateWeaponIcon();
  }

  protected override void OnQuery_INDEX_R()
  {
    base.OnQuery_INDEX_R();
    this.UpdateWeaponIcon();
  }

  private void UpdateWeaponIcon()
  {
    int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
    EquipItemInfo equipItemInfo = this.equipSet.item[this.selectEquipIndex];
    ItemIcon iconByEquipItemInfo = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, sex, this.GetCtrl((Enum) EquipSetDetailStatus.UI.ICON_WEAPON), event_name: "DETAIL");
    if (equipItemInfo == null)
    {
      this.SetActive((Enum) EquipSetDetailStatus.UI.OBJ_WEAPON_INFO, false);
      this.SetActive((Enum) EquipSetDetailStatus.UI.OBJ_EQUIP_SET_SELECT, false);
    }
    else
    {
      if (equipItemInfo != null && equipItemInfo.tableID != 0U)
        iconByEquipItemInfo.SetEquipExt(equipItemInfo, this.GetComponent<UILabel>((Enum) EquipSetDetailStatus.UI.LBL_LV_NOW));
      this.SetLabelText((Enum) EquipSetDetailStatus.UI.LBL_NAME, equipItemInfo.tableData.name);
      this.SetLabelText((Enum) EquipSetDetailStatus.UI.LBL_LV_NOW, equipItemInfo.level.ToString());
      this.SetLabelText((Enum) EquipSetDetailStatus.UI.LBL_LV_MAX, equipItemInfo.tableData.maxLv.ToString());
      Transform ctrl1 = this.GetCtrl((Enum) EquipSetDetailStatus.UI.SPR_TYPE_ICON_BG);
      Transform ctrl2 = this.FindCtrl(ctrl1, (Enum) EquipSetDetailStatus.UI.SPR_TYPE_ICON);
      Transform ctrl3 = this.FindCtrl(ctrl1, (Enum) EquipSetDetailStatus.UI.SPR_TYPE_ICON_RARITY);
      this.SetEquipmentTypeIcon(ctrl2, ctrl1, ctrl3, equipItemInfo.tableData);
      this.SetActive(ctrl3, false);
      this.SetEvent(this.GetCtrl((Enum) EquipSetDetailStatus.UI.ICON_WEAPON), "DETAIL", this.selectEquipIndex);
    }
  }

  private void OnQuery_DETAIL()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) ItemDetailEquip.CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS,
      (object) this.equipSet.item[this.selectEquipIndex]
    });
  }

  protected virtual void OnQuery_TO_ABILITY()
  {
    GameSection.SetEventData((object) this.currentEventData);
  }

  private void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) EquipSetDetailStatus.UI.OBJ_CAPTION_3);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) EquipSetDetailStatus.UI.LBL_CAPTION, text);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected new enum UI
  {
    GRD_ABILITY,
    SCR_ABILITY,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_ATK_ELEM_FIRE,
    LBL_ATK_ELEM_WATER,
    LBL_ATK_ELEM_THUNDER,
    LBL_ATK_ELEM_EARTH,
    LBL_ATK_ELEM_LIGHT,
    LBL_ATK_ELEM_DARK,
    LBL_ATK_ELEM_NONE,
    LBL_DEF_ELEM_FIRE,
    LBL_DEF_ELEM_WATER,
    LBL_DEF_ELEM_THUNDER,
    LBL_DEF_ELEM_EARTH,
    LBL_DEF_ELEM_LIGHT,
    LBL_DEF_ELEM_DARK,
    LBL_DEF_ELEM_NONE,
    TGL_STATUS_WINDOW_INDEX0,
    TGL_STATUS_WINDOW_INDEX1,
    TGL_STATUS_WINDOW_INDEX2,
    TGL_WINDOW_ICON_INDEX0,
    TGL_WINDOW_ICON_INDEX1,
    TGL_WINDOW_ICON_INDEX2,
    TGL_BUTTON_INDEX0,
    TGL_BUTTON_INDEX1,
    TGL_BUTTON_INDEX2,
    OBJ_EQUIP_BTN_ROOT_ACTIVE,
    OBJ_EQUIP_BTN_ROOT_INACTIVE,
    SPR_BG0,
    SPR_BG1,
    SPR_BG2,
    SPR_BG3,
    SPR_BG4,
    SPR_BG5,
    SPR_BG6,
    SPR_BG7,
    SPR_BG8,
    SPR_BG9,
    OBJ_DETAIL_ROOT,
    OBJ_ABILITY_ITEM_ROOT,
    OBJ_ABILITY_ITEM_ITEM_ROOT,
    BTN_ABILITY,
    LBL_ABILITY_NAME,
    LBL_AP_0,
    LBL_AP_1,
    LBL_AP_2,
    LBL_AP_3,
    LBL_AP_4,
    LBL_AP_5,
    LBL_AP_6,
    LBL_AP_TOTAL,
    SPR_NAME_TAG,
    SPR_NAME_TAG_OFF,
    TGL_BG,
    TGL_NAME_TAG,
    ICON_WEAPON,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    SPR_TYPE_ICON,
    SPR_TYPE_ICON_BG,
    SPR_TYPE_ICON_RARITY,
    OBJ_CAPTION_3,
    LBL_CAPTION,
    OBJ_WEAPON_INFO,
    OBJ_EQUIP_SET_SELECT,
  }
}
