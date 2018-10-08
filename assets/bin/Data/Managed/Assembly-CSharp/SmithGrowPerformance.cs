using System;

public class SmithGrowPerformance : SmithPerformanceBase
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected unsafe override void OnOpen()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		director.Reset();
		director.StartGrow(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
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