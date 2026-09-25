// Decompiled with JetBrains decompiler
// Type: TermsWebViewDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class TermsWebViewDialog : WebViewDialog
{
  private bool isTermsAgreement;

  public override void Initialize()
  {
    base.Initialize();
    string str = MonoBehaviourSingleton<AccountManager>.I.termsUpdateDay != null ? DateTime.Parse(MonoBehaviourSingleton<AccountManager>.I.termsUpdateDay).ToString("yyyymmdd") : " ";
    this.SetLabelText((Enum) TermsWebViewDialog.UI.STR_CONFIRM_MESSAGE, string.Format(this.sectionData.GetText("STR_CONFIRM"), (object) str));
    this.SetLabelText((Enum) TermsWebViewDialog.UI.STR_AGE_MESSAGE, this.sectionData.GetText("STR_AGE"));
  }

  private void OnQuery_DECIDE()
  {
    MonoBehaviourSingleton<AccountManager>.I.termsCheck = false;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData("PUSH_START")
    });
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) TermsWebViewDialog.UI.SPR_CHECK, this.isTermsAgreement);
    this.SetActive((Enum) TermsWebViewDialog.UI.SPR_CHECK_OFF, !this.isTermsAgreement);
    this.SetActive((Enum) TermsWebViewDialog.UI.BTN_DECIDE, this.isTermsAgreement);
    this.SetActive((Enum) TermsWebViewDialog.UI.BTN_DECIDE_OFF, !this.isTermsAgreement);
  }

  private void OnQuery_TERMS()
  {
    this.isTermsAgreement = !this.isTermsAgreement;
    this.RefreshUI();
  }

  private enum UI
  {
    SPR_CHECK,
    SPR_CHECK_OFF,
    BTN_DECIDE,
    BTN_DECIDE_OFF,
    STR_CONFIRM_MESSAGE,
    STR_AGE_MESSAGE,
  }
}
