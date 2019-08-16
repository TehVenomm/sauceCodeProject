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
		if (!TradingPostManager.IsFulfillRequirement() && (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene") || !(MonoBehaviourSingleton<TradingPostManager>.I.startSectionName == "HomeTop")))
		{
			DispatchEvent("LA");
			return;
		}
		DispatchEvent("PASSED");
		base.StartSection();
	}
}
