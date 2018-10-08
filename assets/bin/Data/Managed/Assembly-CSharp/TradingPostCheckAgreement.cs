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
			DispatchEvent("UA", null);
		}
		else if (!TradingPostManager.IsFulfillRequirement() && (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene") || !(MonoBehaviourSingleton<TradingPostManager>.I.startSectionName == "HomeTop")))
		{
			DispatchEvent("LA", null);
		}
		else
		{
			DispatchEvent("PASSED", null);
			base.StartSection();
		}
	}
}
