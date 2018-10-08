public class SmithAbilityItemPerformance : SmithPerformanceBase
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected override void OnOpen()
	{
		director.Reset();
		director.StartAbilityChange(OnEndDirection);
		base.OnOpen();
	}
}
