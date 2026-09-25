// Decompiled with JetBrains decompiler
// Type: TradingPostSellItemDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class TradingPostSellItemDialog : GameSection
{
  protected TradingPostItemStartingAtPriceModel startingPrice;

  public override void Initialize()
  {
    this.StartCoroutine(this.DoInitialize());
    base.Initialize();
  }

  private IEnumerator DoInitialize()
  {
    int itemId = (int) MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemId;
    bool isRequestDone = false;
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemStartingAtPrice(itemId, (Action<bool, TradingPostItemStartingAtPriceModel>) ((isSuccess, ret) =>
    {
      if (!isSuccess)
        return;
      isRequestDone = true;
      this.startingPrice = ret;
    }));
    while (isRequestDone)
      yield return (object) null;
  }

  public override void UpdateUI()
  {
    string key = "TEXT_SELL";
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.STR_TITLE_U, this.sectionData.GetText(key));
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.STR_TITLE_D, this.sectionData.GetText(key));
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.LBL_NUM_GEM, (object) MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemQuantity);
    this.SetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM, 1, 1, Mathf.Min(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemQuantity, (int) Singleton<TradingPostTable>.I.GetItemData(MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.itemId).maxQuantity), new EventDelegate.Callback(this.OnChangeSliderNum));
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", (object) this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM)));
    this.SetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE, 1, MonoBehaviourSingleton<TradingPostManager>.I.tradingSellMinGem, MonoBehaviourSingleton<TradingPostManager>.I.tradingSellMaxGem, new EventDelegate.Callback(this.OnChangeSliderPrice));
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.STR_SALE_PRICE, this.GetPriceRate());
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.LBL_BASE_PRICE, (object) this.startingPrice.result.unitPrice);
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.STR_SOLD_COUNT, string.Format(this.sectionData.GetText("STR_NUM_PEOPLE_COUNT"), (object) this.startingPrice.result.saleNumber));
    this.SetSupportEncoding((Enum) TradingPostSellItemDialog.UI.STR_SOLD_COUNT, true);
  }

  private void OnQuery_SALE_NUM_MINUS()
  {
    this.SetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM, this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM) - 1);
  }

  private void OnQuery_SALE_NUM_PLUS()
  {
    this.SetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM, this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM) + 1);
  }

  private void OnQuery_SALE_PRICE_MINUS()
  {
    this.SetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE, this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE) - 1);
  }

  private void OnQuery_SALE_PRICE_PLUS()
  {
    this.SetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE, this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE) + 1);
  }

  private void OnQuery_SALE()
  {
    GameSection.StayEvent();
    int progressInt = this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM);
    double num = (double) this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE) / (double) progressInt;
    MonoBehaviourSingleton<TradingPostManager>.I.SendRequestSellItem((int) MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSellItemData.uniqID, this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM), this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE), (Action<bool>) (isSuccess =>
    {
      MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost = isSuccess;
      GameSection.ResumeEvent(isSuccess);
    }));
  }

  protected int GetSliderNum()
  {
    return this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM);
  }

  protected int GetSliderPrice()
  {
    return this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE);
  }

  private void OnChangeSliderNum()
  {
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", (object) this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM)));
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.STR_SALE_PRICE, this.GetPriceRate());
  }

  private void OnChangeSliderPrice()
  {
    this.SetLabelText((Enum) TradingPostSellItemDialog.UI.STR_SALE_PRICE, this.GetPriceRate());
  }

  protected string GetPriceRate()
  {
    int progressInt1 = this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_NUM);
    int progressInt2 = this.GetProgressInt((Enum) TradingPostSellItemDialog.UI.SLD_SALE_PRICE);
    float num = (float) progressInt2 / (float) progressInt1;
    return string.Format(this.sectionData.GetText("STR_SALE_PRICE"), (object) progressInt2, (double) num >= 10.0 ? (object) string.Format("{0:#,0.}", (object) num) : (object) string.Format("{0:#,0.00}", (object) num));
  }

  protected enum UI
  {
    OBJ_FRAME,
    SPR_FRAME,
    TITLE,
    STR_TITLE_U,
    STR_TITLE_D,
    OBJ_NUM_GEM,
    SPR_NUM_GEM_BG,
    LBL_NUM_GEM,
    STR_NUM_GEM,
    OBJ_SALE_NUM,
    STR_SALE_NUM,
    LBL_SALE_NUM,
    SLD_SALE_NUM,
    BTN_SALE_NUM_MINUS,
    BTN_SALE_NUM_PLUS,
    OBJ_SALE_PRICE,
    STR_SALE_PRICE_TEXT,
    STR_SALE_PRICE,
    SLD_SALE_PRICE,
    BTN_SALE_PRICE_MINUS,
    BTN_SALE_PRICE_PLUS,
    OBJ_BASE_PRICE,
    STR_BASE_PRICE,
    STR_SOLD_COUNT,
    LBL_BASE_PRICE,
    SPR_GEM_ICON,
    BTN_SELL,
    TEXT_BTN_SELL,
    LBL_CAPTION,
  }
}
