using UnityEngine;

public class InGameStoryMain : StoryMain
{
	public override void Initialize()
	{
		Utility.CreateGameObjectAndComponent("StoryDirector", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if ((Object)MonoBehaviourSingleton<StageManager>.I.stageObject != (Object)null)
			{
				MonoBehaviourSingleton<StageManager>.I.stageObject.gameObject.SetActive(false);
			}
			if ((Object)MonoBehaviourSingleton<StageManager>.I.skyObject != (Object)null)
			{
				MonoBehaviourSingleton<StageManager>.I.skyObject.gameObject.SetActive(false);
			}
		}
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		if (MonoBehaviourSingleton<StageManager>.IsValid())
		{
			if ((Object)MonoBehaviourSingleton<StageManager>.I.stageObject != (Object)null)
			{
				MonoBehaviourSingleton<StageManager>.I.stageObject.gameObject.SetActive(true);
			}
			if ((Object)MonoBehaviourSingleton<StageManager>.I.skyObject != (Object)null)
			{
				MonoBehaviourSingleton<StageManager>.I.skyObject.gameObject.SetActive(true);
			}
		}
		if (MonoBehaviourSingleton<StoryDirector>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<StoryDirector>.I.gameObject);
		}
		base.OnDestroy();
	}
}
