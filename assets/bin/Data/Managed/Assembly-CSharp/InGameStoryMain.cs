using UnityEngine;

public class InGameStoryMain : StoryMain
{
	public override void Initialize()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		Utility.CreateGameObjectAndComponent("StoryDirector", MonoBehaviourSingleton<AppMain>.I._transform, -1);
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
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
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
