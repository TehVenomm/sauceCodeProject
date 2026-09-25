// Decompiled with JetBrains decompiler
// Type: BuyJackpotTicketDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class BuyJackpotTicketDialog : GameSection
{
  private const int MAX_NUMBER_TICKET_PURCHASE = 500;
  private int maxNum;
  private int nowSelected;
  private bool canUpdateUI = true;
  private int jackpotTicketPrice;
  private int numberTicketCanBuy;
  private Transform sprGem;
  private UILabel lblBuy;

  public override void Initialize()
  {
    int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    this.jackpotTicketPrice = MonoBehaviourSingleton<FortuneWheelManager>.I.WheelData.vaultInfo.ticketPrice;
    this.numberTicketCanBuy = crystal / this.jackpotTicketPrice;
    this.maxNum = 500;
    this.nowSelected = 1;
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.LBL_CURRENT_CRYSTAL, (object) crystal);
    this.canUpdateUI = false;
    this.sprGem = this.GetCtrl((Enum) BuyJackpotTicketDialog.UI.SPR_GEM);
    this.lblBuy = ((Component) this.GetCtrl((Enum) BuyJackpotTicketDialog.UI.LBL_BUY_BTN)).GetComponent<UILabel>();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.STR_TITLE_U, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.STR_TITLE_D, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.STR_SELECT_NUM, this.sectionData.GetText("STR_QUANTITY"));
    this.SetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM, this.nowSelected, 1, this.maxNum, new EventDelegate.Callback(this.OnChagenSlider));
  }

  private void OnChagenSlider()
  {
    int progressInt = this.GetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM);
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.LBL_SELECT_NUM, string.Format("{0,8:#,0}", (object) progressInt));
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.LBL_BUY_BTN, string.Format(StringTable.Get(STRING_CATEGORY.DRAGON_VAULT, 4U), (object) (progressInt * this.jackpotTicketPrice)));
    this.sprGem.localPosition = Vector2.op_Implicit(new Vector2((float) ((double) this.lblBuy.printedSize.x / 2.0 + 15.0), 3f));
  }

  private void OnQuery_SELECT_NUM_MINUS()
  {
    this.SetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM, this.GetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM) - 1);
  }

  private void OnQuery_SELECT_NUM_PLUS()
  {
    this.SetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM, this.GetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM) + 1);
  }

  protected int GetSliderNum()
  {
    return this.GetProgressInt((Enum) BuyJackpotTicketDialog.UI.SLD_SELECT_NUM);
  }

  private void OnQuery_BUY_TICKET()
  {
    int sliderNum = this.GetSliderNum();
    if ((double) (sliderNum * this.jackpotTicketPrice) > (double) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal)
    {
      this.DispatchEvent("BUY_GEM", (object) this.sectionData.GetText("STR_COMFIRM_BUY_GEM"));
    }
    else
    {
      if (sliderNum > 500)
        return;
      GameSection.StayEvent();
      MonoBehaviourSingleton<FortuneWheelManager>.I.BuyTicket(sliderNum, (Action<bool>) (b =>
      {
        GameSection.ResumeEvent(true);
        this.DispatchEvent("JACKPOT_BUY_MESSAGE", (object) this.sectionData.GetText(b ? "STR_BUY_SUCCESS" : "STR_BUY_FAILED"));
        MonoBehaviourSingleton<FortuneWheelManager>.I.RequestUpdateUI();
      }));
      this.SetButtonEnabled((Enum) BuyJackpotTicketDialog.UI.BTN_BUY, false);
    }
  }

  private void OnQuery_CANCEL() => GameSection.BackSection();

  private void OnQuery_ComfirmBuyGemDialog_YES()
  {
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    int crystal = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal;
    if ((flags & GameSection.NOTIFY_FLAG.CHANGED_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.SetLabelText((Enum) BuyJackpotTicketDialog.UI.LBL_CURRENT_CRYSTAL, (object) crystal);
  }

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey() => this.DispatchEvent("[BACK]");

  protected enum UI
  {
    LBL_SELECT_NUM,
    LBL_SELECT_PRICE,
    BTN_SELECT_NUM_MINUS,
    BTN_SELECT_NUM_PLUS,
    SLD_SELECT_NUM,
    SPR_SELECT_FRAME,
    SPR_REACH_LIMIT,
    LBL_REQUEST_LIMIT,
    STR_TITLE_U,
    STR_TITLE_D,
    STR_SELECT_NUM,
    LBL_BUY_BTN,
    LBL_CURRENT_CRYSTAL,
    BTN_BUY,
    SPR_GEM,
  }
}
