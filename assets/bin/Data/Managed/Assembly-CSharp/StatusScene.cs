using UnityEngine;

public class StatusScene : GameSection
{
	public override void Initialize()
	{
		RenderTargetCacher component = MonoBehaviourSingleton<AppMain>.I.mainCamera.GetComponent<RenderTargetCacher>();
		if ((Object)component != (Object)null)
		{
			component.enabled = true;
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
