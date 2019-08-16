public class SmithShadowEvolveResult : EquipResultBase
{
	private bool direction;

	public override void Initialize()
	{
		smithType = SmithType.EVOLVE;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (!direction)
		{
			OnFinishedAddAbilityDirection();
			direction = true;
		}
		base.UpdateUI();
	}

	private void OnQuery_TO_SELECT()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("SmithGrowItemSelect"))
		{
			GameSection.StopEvent();
			TO_UNIQUE_OR_MAIN_STATUS();
		}
	}
}
