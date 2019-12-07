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
				MonoBehaviourSingleton<StageManager>.I.stageObject.gameObject.SetActive(value: false);
			}
			if (MonoBehaviourSingleton<StageManager>.I.skyObject != null)
			{
				MonoBehaviourSingleton<StageManager>.I.skyObject.gameObject.SetActive(value: false);
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
				MonoBehaviourSingleton<StageManager>.I.stageObject.gameObject.SetActive(value: true);
			}
			if (MonoBehaviourSingleton<StageManager>.I.skyObject != null)
			{
				MonoBehaviourSingleton<StageManager>.I.skyObject.gameObject.SetActive(value: true);
			}
		}
		if (MonoBehaviourSingleton<StoryDirector>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<StoryDirector>.I.gameObject);
		}
		base.OnDestroy();
	}
}
