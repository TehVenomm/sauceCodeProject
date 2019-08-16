using UnityEngine;

public class DebugScene : GameSection
{
	public override void Initialize()
	{
		if (MonoBehaviourSingleton<HomeManager>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<HomeManager>.I.get_gameObject());
		}
		base.Initialize();
	}
}
