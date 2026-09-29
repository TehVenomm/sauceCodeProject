// Decompiled with JetBrains decompiler
// Type: TradingPostInventoryDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class TradingPostInventoryDialog : ItemStorageTop
{
  protected override void InitSellObjectMode()
  {
  }

  protected override void InitListItemEvent(ItemIcon icon, int i, SortCompareData item)
  {
    this.SetEvent(icon.transform, "DETAIL", (object) item);
  }

  protected override void InitializeCaption()
  {
    UITweenCtrl component = ((Component) this.GetCtrl((Enum) TradingPostInventoryDialog.UI.OBJ_CAPTION_3)).gameObject.GetComponent<UITweenCtrl>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Reset();
    int index = 0;
    for (int length = component.tweens.Length; index < length; ++index)
      component.tweens[index].ResetToBeginning();
    component.Play();
  }

  protected override void ToDetail()
  {
    SortCompareData eventData = GameSection.GetEventData() as SortCompareData;
    if (!TradingPostManager.IsItemValid(eventData.GetTableID()))
      GameSection.ChangeEvent("CANT_SELL");
    else
      MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostSellItemData(eventData.GetTableID(), eventData.GetUniqID(), eventData.GetNum());
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
