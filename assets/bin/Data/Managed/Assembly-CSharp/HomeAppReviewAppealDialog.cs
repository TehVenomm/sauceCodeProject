// Decompiled with JetBrains decompiler
// Type: HomeAppReviewAppealDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class HomeAppReviewAppealDialog : HomeAppReviewAppealDialogBase
{
  private UIButton yesButton;
  private string itemString;

  public override void Initialize()
  {
    base.Initialize();
    this.yesButton = ((Component) this.GetCtrl((Enum) HomeAppReviewAppealDialog.UI.SPR_BTN_YES)).GetComponent<UIButton>();
    this.SetStarsEvent();
    this.itemString = StringTable.Get(STRING_CATEGORY.APP_REVIEW, 3U);
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) HomeAppReviewAppealDialog.UI.LBL_ITEM_TEXT, this.itemString);
    if (this.starValue == 0)
    {
      MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce("Received: " + this.itemString, "");
      this.SetEnableYesButton(false);
      this.yesButton.UpdateColor(true);
    }
    base.UpdateUI();
  }

  protected override void OnQuery_YES()
  {
    if (this.starValue >= 5)
    {
      GameSection.ChangeEvent("OPEN_MAX", (object) this.starValue);
    }
    else
    {
      if (this.starValue < 1)
        return;
      GameSection.ChangeEvent("OPEN_SOME", (object) this.starValue);
    }
  }

  protected override void OnQuery_NO() => this.SendInfo(0);

  private void OnQuery_STAR()
  {
    this.starValue = (int) GameSection.GetEventData() + 1;
    this.SetEnableYesButton(true);
    this.UpdateStarUI();
    GameSection.StopEvent();
  }

  private void SetEnableYesButton(bool isEnable)
  {
    this.yesButton.isEnabled = isEnable;
    Color color = !isEnable ? Color.gray : Color.white;
    ((Component) this.GetCtrl((Enum) HomeAppReviewAppealDialog.UI.LBL_BTN_YES)).GetComponent<UILabel>().color = color;
    ((Component) this.yesButton).GetComponent<UISprite>().color = color;
  }

  protected new enum UI
  {
    LBL_ITEM_TEXT,
    SPR_BTN_YES,
    LBL_BTN_YES,
    BTN_STAR1,
    BTN_STAR2,
    BTN_STAR3,
    BTN_STAR4,
    BTN_STAR5,
    OBJ_ON,
    OBJ_OFF,
  }
}
