// Decompiled with JetBrains decompiler
// Type: EquipSetDetailAbilityTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EquipSetDetailAbilityTable : EquipSetDetailStatusAndAbilityTable
{
  private AbilityDetailPopUp abilityDetailPopUp;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "AbilityTable";
      yield return "AbilityDataTable";
      yield return "AbilityItemLotTable";
      foreach (string str in base.requireDataTable)
        yield return str;
    }
  }

  public override void UpdateUI()
  {
    this.UpdateAbilityTable();
    base.UpdateUI();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.NoEventReleaseTouchAndReleases(this.GetComponent<UIGrid>((Enum) EquipSetDetailAbilityTable.UI.GRD_ABILITY).GetChildList());
    this.OnQuery_RELEASE_ABILITY();
  }

  protected override void SetAbilityItemEvent(Transform t, int index)
  {
    this.SetTouchAndRelease(((Component) ((Component) t).GetComponentInChildren<UIButton>()).transform, "ABILITY_DATA", "RELEASE_ABILITY", (object) index);
  }

  protected override void SetAbilityItemItemEvent(Transform t, int index)
  {
    this.SetTouchAndRelease(((Component) ((Component) t).GetComponentInChildren<UIButton>()).transform, "ABILITY_ITEM_DATA", "RELEASE_ABILITY", (object) index);
  }

  protected override void OnQuery_ABILITY_DATA()
  {
    int eventData = (int) GameSection.GetEventData();
    EquipItemAbility ability = this.abilityCollection[eventData].ability;
    Transform child = this.GetComponent<UIGrid>((Enum) EquipSetDetailAbilityTable.UI.GRD_ABILITY).GetChild(eventData);
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipSetDetailAbilityTable.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(child);
    this.abilityDetailPopUp.SetAbilityDetailText(ability);
    GameSection.StopEvent();
  }

  protected override void OnQuery_ABILITY_ITEM_DATA()
  {
    int eventData = (int) GameSection.GetEventData();
    AbilityItemInfo abilityItem = this.abilityItems[eventData - this.abilityCollection.Length];
    Transform child = this.GetComponent<UIGrid>((Enum) EquipSetDetailAbilityTable.UI.GRD_ABILITY).GetChild(eventData);
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipSetDetailAbilityTable.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.ShowAbilityDetail(child);
    this.abilityDetailPopUp.SetAbilityDetailText(abilityItem.GetName(), "", abilityItem.GetDescription());
    GameSection.StopEvent();
  }

  protected void OnQuery_RELEASE_ABILITY()
  {
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      return;
    this.abilityDetailPopUp.Hide();
    GameSection.StopEvent();
  }

  protected override void PreCacheAbilityDetail(string name, string ap, string desc)
  {
    if (Object.op_Equality((Object) this.abilityDetailPopUp, (Object) null))
      this.abilityDetailPopUp = this.CreateAndGetAbilityDetail((Enum) EquipSetDetailAbilityTable.UI.OBJ_DETAIL_ROOT);
    this.abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
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
    OBJ_EMPTY,
    LBL_NO_ITEM,
  }
}
