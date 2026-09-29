// Decompiled with JetBrains decompiler
// Type: SkillSelectBaseSecond
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public abstract class SkillSelectBaseSecond : SkillSelectBase
{
  public const int DETACH_ALL_SKILL_INDEX = -2;
  protected bool isVisibleEmptySkill;

  public override void Initialize()
  {
    this.InitializeCaption();
    base.Initialize();
  }

  protected virtual void Update() => this.ObserveItemList();

  public override void UpdateUI() => base.UpdateUI();

  protected void SetVisibleEmptySkillType(bool is_visible, int index = 0)
  {
    this.isVisibleEmptySkill = is_visible;
    this.SetActive((Enum) SkillSelectBaseSecond.UI.OBJ_EMPTY_SKILL_ROOT, is_visible);
    if (!is_visible)
      return;
    SKILL_SLOT_TYPE skillSlotType = SKILL_SLOT_TYPE.NONE;
    if (this.equipItem != null)
    {
      SkillItemTable.SkillSlotData[] skillSlot = this.equipItem.tableData.GetSkillSlot(this.equipItem.exceed);
      if (skillSlot == null || skillSlot.Length <= index)
      {
        this.SetActive((Enum) SkillSelectBaseSecond.UI.OBJ_EMPTY_SKILL_ROOT, false);
        return;
      }
      skillSlotType = skillSlot[index].slotType;
    }
    this.SetLabelText((Enum) SkillSelectBaseSecond.UI.LBL_EMPTY_SKILL_TYPE, MonoBehaviourSingleton<StatusManager>.I.GetSkillItemGroupString(skillSlotType));
    this.SetSprite((Enum) SkillSelectBaseSecond.UI.SPR_EMPTY_SKILL, UIBehaviour.GetSkillIconSpriteName(skillSlotType, true, true));
  }

  protected override void UpdateParam()
  {
  }

  protected override void OnQuery_SELECT()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData >= 0)
    {
      this.selectIndex = eventData;
      this.selectSkillItem = this.inventory.datas[this.selectIndex].GetItemData() as SkillItemInfo;
    }
    else if (eventData == -2)
    {
      this.selectIndex = -2;
      this.selectSkillItem = (SkillItemInfo) null;
    }
    else
    {
      this.selectIndex = -1;
      this.selectSkillItem = (SkillItemInfo) null;
    }
    if (!this.CheckApplicationVersion())
      GameSection.StopEvent();
    else
      this.OnDecision();
  }

  protected virtual bool CheckApplicationVersion() => true;

  private void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) SkillSelectBaseSecond.UI.OBJ_CAPTION_3);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) SkillSelectBaseSecond.UI.LBL_CAPTION, text);
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
    OBJ_DETAIL_ROOT,
    TEX_MODEL,
    TEX_INNER_MODEL,
    LBL_NAME,
    LBL_LV_NOW,
    LBL_LV_MAX,
    LBL_ATK,
    LBL_DEF,
    LBL_HP,
    LBL_SELL,
    LBL_DESCRIPTION,
    OBJ_FAVORITE_ROOT,
    TWN_FAVORITE,
    TWN_UNFAVORITE,
    OBJ_SUB_STATUS,
    SPR_SKILL_TYPE_ICON,
    SPR_SKILL_TYPE_ICON_BG,
    SPR_SKILL_TYPE_ICON_RARITY,
    STR_TITLE_ITEM_INFO,
    STR_TITLE_DESCRIPTION,
    STR_TITLE_STATUS,
    STR_TITLE_SELL,
    PRG_EXP_BAR,
    OBJ_NEXT_EXP_ROOT,
    BTN_DECISION,
    STR_DECISION_R,
    BTN_SKILL_DECISION,
    STR_SKILL_DECISION,
    STR_SKILL_DECISION_R,
    OBJ_SKILL_INFO_ROOT,
    LBL_EQUIP_ITEM_NAME,
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    LBL_SORT,
    BTN_BACK,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    BTN_CHANGE_INVENTORY,
    OBJ_EMPTY_SKILL_ROOT,
    TEX_EMPTY_SKILL,
    SPR_EMPTY_SKILL,
    LBL_EMPTY_SKILL_TYPE,
    OBJ_CAPTION_3,
    LBL_CAPTION,
  }
}
