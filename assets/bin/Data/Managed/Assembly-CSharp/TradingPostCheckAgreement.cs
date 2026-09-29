// Decompiled with JetBrains decompiler
// Type: TradingPostCheckAgreement
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class TradingPostCheckAgreement : GameSection
{
  public override void Initialize()
  {
    MonoBehaviourSingleton<TradingPostManager>.I.startSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory();
    base.Initialize();
  }

  public override void StartSection()
  {
    if (!TradingPostManager.IsAcceptUserAgreement())
      this.DispatchEvent("UA");
    else if (!TradingPostManager.IsFulfillRequirement() && (!MonoBehaviourSingleton<GoGameSettingsManager>.I.tradingpostCurrentScene.Contains(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName()) || !MonoBehaviourSingleton<GoGameSettingsManager>.I.tradingpostStartSection.Contains(MonoBehaviourSingleton<TradingPostManager>.I.startSectionName)))
    {
      this.DispatchEvent("LA");
    }
    else
    {
      this.DispatchEvent("PASSED");
      base.StartSection();
    }
  }
}
