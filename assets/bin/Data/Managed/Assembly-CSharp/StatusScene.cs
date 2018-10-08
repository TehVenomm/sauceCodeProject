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
		base.Initialize();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
		base.Exit();
	}
}
