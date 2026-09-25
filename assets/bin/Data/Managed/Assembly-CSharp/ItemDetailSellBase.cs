// Decompiled with JetBrains decompiler
// Type: ItemDetailSellBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemDetailSellBase : GameSection
{
  protected SortCompareData data;

  public override void Initialize()
  {
    this.data = GameSection.GetEventData() as SortCompareData;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    bool uiUpdateInstant = this.uiUpdateInstant;
    this.uiUpdateInstant = true;
    bool is_visible = TradingPostManager.IsItemValid(this.data.GetTableID());
    this.SetActive((Enum) ItemDetailSellBase.UI.BTN_SALE_TP, is_visible);
    this.uiUpdateInstant = uiUpdateInstant;
    string key1 = "TEXT_SELL";
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_CAPTION, this.sectionData.GetText(key1));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.STR_TITLE_U, this.sectionData.GetText(key1));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.STR_TITLE_D, this.sectionData.GetText(key1));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.STR_SALE_NUM, this.sectionData.GetText("TEXT_SELL_NUM"));
    this.SetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM, 1, 1, this.data.GetNum(), new EventDelegate.Callback(this.OnChagenSlider));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_ITEM_NUM, this.data.GetNum().ToString());
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_MONEY, string.Format("{0, 8:#,0}", (object) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money));
    string key2 = "STR_SELL_GOLD";
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_SELL_GOLD_U, this.sectionData.GetText(key2));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_SELL_GOLD_D, this.sectionData.GetText(key2));
    string key3 = "STR_SELL_TP";
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_SELL_TRADING_POST_U, this.sectionData.GetText(key3));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_SELL_TRADING_POST_D, this.sectionData.GetText(key3));
    if (!is_visible)
      return;
    UISprite component = ((Component) this.GetCtrl((Enum) ItemDetailSellBase.UI.SPR_SALE_FRAME)).gameObject.GetComponent<UISprite>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.height += ((Component) this.GetCtrl((Enum) ItemDetailSellBase.UI.BTN_SALE_TP)).gameObject.GetComponent<UISprite>().height;
    else
      Debug.LogError((object) "Sprite frame is null");
  }

  private void OnChagenSlider()
  {
    int progressInt = this.GetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM);
    int num = this.data.GetSalePrice() * progressInt;
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", (object) progressInt));
    this.SetLabelText((Enum) ItemDetailSellBase.UI.LBL_SALE_PRICE, string.Format("{0,8:#,0}", (object) num));
  }

  private void OnQuery_SALE_NUM_MINUS()
  {
    this.SetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM, this.GetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM) - 1);
  }

  private void OnQuery_SALE_NUM_PLUS()
  {
    this.SetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM, this.GetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM) + 1);
  }

  protected int GetSliderNum() => this.GetProgressInt((Enum) ItemDetailSellBase.UI.SLD_SALE_NUM);

  protected enum UI
  {
    LBL_ITEM_NUM,
    LBL_SALE_NUM,
    LBL_SALE_PRICE,
    LBL_MONEY,
    LBL_CANT_SALE,
    BTN_SALE_NUM_MINUS,
    BTN_SALE_NUM_PLUS,
    SLD_SALE_NUM,
    SPR_SALE_FRAME,
    OBJ_MONEY_ROOT,
    BTN_SALE_TP,
    STR_TITLE_U,
    STR_TITLE_D,
    STR_SALE_NUM,
    LBL_CAPTION,
    LBL_SELL_GOLD_U,
    LBL_SELL_GOLD_D,
    LBL_SELL_TRADING_POST_U,
    LBL_SELL_TRADING_POST_D,
  }
}
