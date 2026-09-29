// Decompiled with JetBrains decompiler
// Type: QuestOrderSellSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestOrderSellSettings : GameSection
{
  private QuestInfoData quest;
  private int haveNum;
  private int sellNum;

  public override void Initialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.quest = eventData[0] as QuestInfoData;
    this.haveNum = (int) eventData[1];
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) QuestOrderSellSettings.UI.LBL_ITEM_NUM, string.Format("{0, 8:#,0}", (object) this.haveNum));
    this.SetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM, 1, 1, this.haveNum, new EventDelegate.Callback(this.OnChagenSlider));
  }

  private void OnChagenSlider()
  {
    this.SetLabelText((Enum) QuestOrderSellSettings.UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", (object) this.GetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM)));
  }

  private void OnQuery_SALE_NUM_MINUS()
  {
    this.SetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM, this.GetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM) - 1);
  }

  private void OnQuery_SALE_NUM_PLUS()
  {
    this.SetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM, this.GetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM) + 1);
  }

  private void OnQuery_SELL()
  {
    this.sellNum = this.GetProgressInt((Enum) QuestOrderSellSettings.UI.SLD_SALE_NUM);
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.quest.questData.tableData.questText,
      (object) this.sellNum.ToString()
    });
  }

  public void OnQuery_QuestSellOrderConfirm_YES()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.quest.questData.tableData.questID,
      (object) this.sellNum
    });
  }

  private enum UI
  {
    LBL_ITEM_NUM,
    LBL_SALE_NUM,
    BTN_SALE_NUM_MINUS,
    BTN_SALE_NUM_PLUS,
    SLD_SALE_NUM,
    SPR_SALE_FRAME,
  }
}
