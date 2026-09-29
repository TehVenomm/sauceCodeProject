// Decompiled with JetBrains decompiler
// Type: TradingPostUserAgreement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class TradingPostUserAgreement : GameSection
{
  public override void Initialize()
  {
    this.SetSupportEncoding((Enum) TradingPostUserAgreement.UI.MESSAGE, true);
    this.SetLabelText((Enum) TradingPostUserAgreement.UI.MESSAGE, this.sectionData.GetText("STR_MESSAGE"));
    this.SetLabelText((Enum) TradingPostUserAgreement.UI.LBL_BTN_0, this.sectionData.GetText("TEXT_BTN_BACK"));
    this.SetLabelText((Enum) TradingPostUserAgreement.UI.LBL_BTN_0_R, this.sectionData.GetText("TEXT_BTN_BACK"));
    this.SetLabelText((Enum) TradingPostUserAgreement.UI.LBL_BTN_2, this.sectionData.GetText("TEXT_BTN_CONTINUE"));
    this.SetLabelText((Enum) TradingPostUserAgreement.UI.LBL_BTN_2_R, this.sectionData.GetText("TEXT_BTN_CONTINUE"));
    if (TradingPostManager.IsAcceptUserAgreement())
      this.SetActive((Enum) TradingPostUserAgreement.UI.OBJ_FRAME, false);
    base.Initialize();
  }

  public override void StartSection()
  {
    if (TradingPostManager.IsAcceptUserAgreement())
      this.DispatchEvent("CONTINUE");
    base.StartSection();
  }

  private void OnQuery_CONTINUE()
  {
    if (!TradingPostManager.IsAcceptUserAgreement())
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<TradingPostManager>.I.SendRequestUserAgreement((Action<bool>) (success =>
      {
        MonoBehaviourSingleton<TradingPostManager>.I.isCheckUserAgreementSuccess = success;
        if (!TradingPostManager.IsFulfillRequirement() && !MonoBehaviourSingleton<GoGameSettingsManager>.I.tradingpostStartSection.Contains(MonoBehaviourSingleton<TradingPostManager>.I.startSectionName))
          GameSection.ChangeStayEvent("LA");
        GameSection.ResumeEvent(success);
        MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
        this.Close();
      }));
    }
    else
    {
      MonoBehaviourSingleton<GameSceneManager>.I.RemoveHistory(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName());
      this.Close();
    }
  }

  private void OnQuery_HELP() => GameSection.SetEventData((object) WebViewManager.TradingPost);

  private enum UI
  {
    MESSAGE,
    OBJ_FRAME,
    LBL_BTN_0,
    LBL_BTN_0_R,
    LBL_BTN_2,
    LBL_BTN_2_R,
  }
}
