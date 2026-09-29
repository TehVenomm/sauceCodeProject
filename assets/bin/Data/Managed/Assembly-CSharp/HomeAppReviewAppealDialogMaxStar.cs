// Decompiled with JetBrains decompiler
// Type: HomeAppReviewAppealDialogMaxStar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class HomeAppReviewAppealDialogMaxStar : HomeAppReviewAppealDialogBase
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
    this.SendInfo(3, (System.Action) (() => Native.launchMyselfMarket()));
  }

  protected override void OnQuery_NO() => this.SendInfo(4);

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
