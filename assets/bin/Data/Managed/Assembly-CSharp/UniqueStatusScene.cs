public class UniqueStatusScene : GameSection
{
	public override void Initialize()
	{
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if (component != null)
		{
			component.set_enabled(true);
		}
		MonoBehaviourSingleton<StatusStageManager>.I.SetSmithCharacterActivate(active: false);
		MonoBehaviourSingleton<StatusStageManager>.I.SetUniqueSmithCharacterActivate(active: true);
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalUniqueEquipSetData();
		base.Initialize();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
		base.Exit();
	}
}
