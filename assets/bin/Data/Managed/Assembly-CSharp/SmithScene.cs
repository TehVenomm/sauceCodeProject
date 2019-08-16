public class SmithScene : GameSection
{
	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
		{
			MonoBehaviourSingleton<StatusStageManager>.I.SetEnableSmithCharacterActivate(active: true);
			MonoBehaviourSingleton<StatusStageManager>.I.SetDisableSmithCharacterActivate(active: false);
		}
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<SmithManager>.I.InitSmithData();
		base.Exit();
	}
}
