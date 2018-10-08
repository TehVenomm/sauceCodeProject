using System;

public class SmithShadowEvolvePerformance : SmithPerformanceBase
{
	protected unsafe override void OnOpen()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		director.Reset();
		director.StartEvolve(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
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
