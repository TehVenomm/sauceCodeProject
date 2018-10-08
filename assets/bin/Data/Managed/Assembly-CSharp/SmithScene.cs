public class SmithScene : GameSection
{
	public override void Initialize()
	{
		base.Initialize();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<SmithManager>.I.InitSmithData();
		base.Exit();
	}
}
