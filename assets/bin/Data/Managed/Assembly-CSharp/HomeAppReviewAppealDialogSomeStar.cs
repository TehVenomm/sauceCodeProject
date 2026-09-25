// Decompiled with JetBrains decompiler
// Type: HomeAppReviewAppealDialogSomeStar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class HomeAppReviewAppealDialogSomeStar : HomeAppReviewAppealDialogBase
{
  public override void Initialize()
  {
    base.Initialize();
    this.starValue = (int) GameSection.GetEventData();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.DisableStarButton();
  }

  protected override void OnQuery_YES()
  {
    GameSection.ChangeEvent("OPEN_OPINION_BOX", (object) new HomeAppReviewAppealDialogBase.Info(this.starValue, 1));
  }

  protected override void OnQuery_NO() => this.SendInfo(2);

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
