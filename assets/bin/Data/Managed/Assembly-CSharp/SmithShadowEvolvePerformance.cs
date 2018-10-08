public class SmithShadowEvolvePerformance : SmithPerformanceBase
{
	protected override void OnOpen()
	{
		director.Reset();
		director.StartEvolve(OnEndDirection);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	protected override void OnClose()
	{
		base.OnClose();
	}
}
