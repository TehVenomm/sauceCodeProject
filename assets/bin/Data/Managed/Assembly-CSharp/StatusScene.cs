public class StatusScene : GameSection
{
	public override void Initialize()
	{
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if (component != null)
		{
			component.set_enabled(true);
		}
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
		MonoBehaviourSingleton<StatusManager>.I.CreateLocalVisualEquipData();
		MonoBehaviourSingleton<StatusStageManager>.I.SetSmithCharacterActivate(active: true);
		MonoBehaviourSingleton<StatusStageManager>.I.SetUniqueSmithCharacterActivate(active: false);
		base.Initialize();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
		base.Exit();
	}
}
