// Decompiled with JetBrains decompiler
// Type: HomeAppReviewAppealDialogBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class HomeAppReviewAppealDialogBase : GameSection
{
  protected int starValue;
  private static readonly HomeAppReviewAppealDialogBase.UI[] StarButtons = new HomeAppReviewAppealDialogBase.UI[5]
  {
    HomeAppReviewAppealDialogBase.UI.BTN_STAR1,
    HomeAppReviewAppealDialogBase.UI.BTN_STAR2,
    HomeAppReviewAppealDialogBase.UI.BTN_STAR3,
    HomeAppReviewAppealDialogBase.UI.BTN_STAR4,
    HomeAppReviewAppealDialogBase.UI.BTN_STAR5
  };
  private string itemString;

  public override void UpdateUI()
  {
    this.UpdateStarUI();
    base.UpdateUI();
  }

  protected void UpdateStarUI()
  {
    int length = HomeAppReviewAppealDialogBase.StarButtons.Length;
    for (int index = 0; index < length; ++index)
    {
      if (index < this.starValue)
        this.SetStarVisualActive(index, true);
      else
        this.SetStarVisualActive(index, false);
    }
  }

  protected void DisableStarButton()
  {
    int length = HomeAppReviewAppealDialogBase.StarButtons.Length;
    for (int index = 0; index < length; ++index)
      ((Component) this.GetCtrl((Enum) HomeAppReviewAppealDialogBase.StarButtons[index])).GetComponent<UIButton>().isEnabled = false;
  }

  protected void SetStarVisualActive(int index, bool active)
  {
    this.SetActive(this.GetCtrl((Enum) HomeAppReviewAppealDialogBase.StarButtons[index]), (Enum) HomeAppReviewAppealDialogBase.UI.OBJ_ON, active);
    this.SetActive(this.GetCtrl((Enum) HomeAppReviewAppealDialogBase.StarButtons[index]), (Enum) HomeAppReviewAppealDialogBase.UI.OBJ_OFF, !active);
  }

  protected void SendInfo(int replyAction, System.Action callback = null)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendAppReviewInfo(this.starValue, replyAction, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      callback.SafeInvoke();
    }));
  }

  protected virtual void OnQuery_YES()
  {
  }

  protected virtual void OnQuery_NO()
  {
  }

  protected void SetStarsEvent()
  {
    int length = HomeAppReviewAppealDialogBase.StarButtons.Length;
    for (int event_data = 0; event_data < length; ++event_data)
      this.SetEvent((Enum) HomeAppReviewAppealDialogBase.StarButtons[event_data], "STAR", event_data);
  }

  protected enum UI
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

  public enum ReplyAction
  {
    NO_NOSTAR,
    YES_SUMSTAR,
    NO_SUMSTAR,
    YES_MAXSTAR,
    NO_MAXSTAR,
  }

  public struct Info(int starValue, int replyAction)
  {
    public int starValue = starValue;
    public int replyAction = replyAction;
  }
}
