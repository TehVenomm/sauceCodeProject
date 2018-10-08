using UnityEngine;

public class DebugScene : GameSection
{
	public override void Initialize()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<HomeManager>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<HomeManager>.I.get_gameObject());
		}
		base.Initialize();
	}
}
