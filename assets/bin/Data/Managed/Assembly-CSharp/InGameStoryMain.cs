using UnityEngine;

public class InGameStoryMain : StoryMain
{
	public override void Initialize()
	{
		Utility.CreateGameObjectAndComponent("StoryDirector", MonoBehaviourSingleton<AppMain>.I._transform);
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
			{
				MonoBehaviourSingleton<StageManager>.I.stageObject.get_gameObject().SetActive(false);
			}
			if (MonoBehaviourSingleton<StageManager>.I.skyObject != null)
			{
				MonoBehaviourSingleton<StageManager>.I.skyObject.get_gameObject().SetActive(false);
			}
		}
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if (MonoBehaviourSingleton<StageManager>.I.stageObject != null)
			{
				MonoBehaviourSingleton<StageManager>.I.stageObject.get_gameObject().SetActive(true);
			}
			if (MonoBehaviourSingleton<StageManager>.I.skyObject != null)
			{
				MonoBehaviourSingleton<StageManager>.I.skyObject.get_gameObject().SetActive(true);
			}
		}
		if (MonoBehaviourSingleton<StoryDirector>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<StoryDirector>.I.get_gameObject());
		}
		base.OnDestroy();
	}
}
