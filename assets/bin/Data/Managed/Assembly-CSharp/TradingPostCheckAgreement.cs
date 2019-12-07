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
		{
			DispatchEvent("UA");
			return;
		}
		if (!TradingPostManager.IsFulfillRequirement() && (!MonoBehaviourSingleton<GoGameSettingsManager>.I.tradingpostCurrentScene.Contains(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName()) || !MonoBehaviourSingleton<GoGameSettingsManager>.I.tradingpostStartSection.Contains(MonoBehaviourSingleton<TradingPostManager>.I.startSectionName)))
		{
			DispatchEvent("LA");
			return;
		}
		DispatchEvent("PASSED");
		base.StartSection();
	}
}
