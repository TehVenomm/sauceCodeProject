public class SmithExceedPerformance : SmithPerformanceBase
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected override void OnOpen()
	{
		director.Reset();
		director.StartExceed(OnEndDirection);
		base.OnOpen();
	}
}
