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
		base.OnDestroy();
		if (MonoBehaviourSingleton<StoryDirector>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<StoryDirector>.I.gameObject);
		}
	}

	public override void UpdateUI()
	{
	}
}
