// Decompiled with JetBrains decompiler
// Type: ItemStorageSell
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemStorageSell : ItemStorageTop
{
  public override void Initialize()
  {
    this.confirmTo = ItemStorageSellConfirm.GO_BACK.SELL;
    this.InitializeCaption();
    base.Initialize();
  }

  protected override void ToDetail()
  {
    this.SaveCurrentScrollPosition();
    int eventData = (int) GameSection.GetEventData();
    this.sellItemData.Clear();
    SortCompareData data = this.inventories[(int) this.tab].datas[eventData];
    if (!data.CanSale())
    {
      if (data.IsFavorite())
        GameSection.ChangeEvent("NOT_SELL_FAVORITE");
      else if (data.IsHomeEquipping())
        GameSection.ChangeEvent("NOT_SELL_EQUIPPING");
      else if (data.IsUniqueEquipping())
        GameSection.ChangeEvent("NOT_SELL_UNIQUE_EQUIPPING");
      else
        GameSection.ChangeEvent("CAN_NOT_SELL");
    }
    else if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.SKILL_EQUIP) && data.GetTableID() == 10000000U)
      GameSection.ChangeEvent("NOT_SELL_DEFAULT_WEAPON");
    else if (this.tab == ItemStorageTop.TAB_MODE.MATERIAL)
    {
      GameSection.ChangeEvent("SELECT", (object) data);
    }
    else
    {
      this.sellItemData.Add(this.inventories[(int) this.tab].datas[eventData]);
      GameSection.ChangeEvent("EQUIP_SELECT");
      this.OnQuery_SELL();
    }
  }

  private void OnCloseDialog_ItemStorageSellConfirm()
  {
    if (this.isSellMode)
      return;
    this.sellItemData.Clear();
  }

  protected override void InitializeCaption()
  {
    Transform ctrl = this.GetCtrl((Enum) ItemStorageSell.UI.OBJ_CAPTION_3);
    string text = this.sectionData.GetText("CAPTION");
    this.SetLabelText(ctrl, (Enum) ItemStorageSell.UI.LBL_CAPTION, text);
    UITweenCtrl component = ((Component) ctrl).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    if (eventData < 0 || !this.IsEnableShowDetailByLongTap())
      return;
    this.SaveCurrentScrollPosition();
    if (this.tab == ItemStorageTop.TAB_MODE.EQUIP)
    {
      GameSection.ChangeEvent("DETAIL_EQUIP", (object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SELL,
        (object) this.inventories[(int) this.tab].datas[eventData]
      });
    }
    else
    {
      if (this.tab != ItemStorageTop.TAB_MODE.SKILL)
        return;
      GameSection.ChangeEvent("DETAIL_SKILL", (object) new ItemDetailSkillSimpleDialog.InitParam((object) new object[2]
      {
        (object) ItemDetailEquip.CURRENT_SECTION.SMITH_SELL,
        (object) this.inventories[(int) this.tab].datas[eventData]
      }, (object) null));
    }
  }

  private bool IsEnableShowDetailByLongTap()
  {
    return this.tab == ItemStorageTop.TAB_MODE.EQUIP || this.tab == ItemStorageTop.TAB_MODE.SKILL;
  }

  protected override bool IsRequiredIconGrayOut(SortCompareData _data)
  {
    return _data.GetNum() == 0 || _data.IsFavorite() || _data.IsEquipping() && this.tab == ItemStorageTop.TAB_MODE.EQUIP;
  }

  private new enum UI
  {
    SCR_INVENTORY,
    GRD_INVENTORY,
    GRD_INVENTORY_SMALL,
    SPR_SCR_BAR,
    SCR_INVENTORY_EQUIP,
    GRD_INVENTORY_EQUIP,
    GRD_INVENTORY_EQUIP_SMALL,
    SPR_EQUIP_SCR_BAR,
    TGL_CHANGE_INVENTORY,
    TGL_ICON_ASC,
    LBL_SORT,
    BTN_SORT,
    SPR_INVALID_SORT,
    LBL_INVALID_SORT,
    BTN_CHANGE,
    SPR_INVALID_CHANGE,
    TGL_TAB0,
    TGL_TAB1,
    TGL_TAB2,
    TGL_TAB3,
    TGL_TAB4,
    TGL_TAB5,
    OBJ_BTN_SELL_MODE,
    OBJ_SELL_MODE_ROOT,
    LBL_MAX_SELECT_NUM,
    LBL_SELECT_NUM,
    LBL_TOTAL,
    LBL_MAX_HAVE_NUM,
    LBL_NOW_HAVE_NUM,
    OBJ_CAPTION_3,
    LBL_CAPTION,
  }
}
