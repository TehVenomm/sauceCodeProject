using UnityEngine;

public class StoryScene : GameSection
{
	public override void Initialize()
	{
		Utility.CreateGameObjectAndComponent("StoryDirector", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		base.OnDestroy();
		if (MonoBehaviourSingleton<StoryDirector>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<StoryDirector>.I.get_gameObject());
		}
	}

	public override void UpdateUI()
	{
	}
}
