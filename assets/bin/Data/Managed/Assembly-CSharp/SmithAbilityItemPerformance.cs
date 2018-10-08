using System;

public class SmithAbilityItemPerformance : SmithPerformanceBase
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
		director.StartAbilityChange(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/));
		base.OnOpen();
	}
}
