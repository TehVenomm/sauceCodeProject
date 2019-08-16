using UnityEngine;

public class InGameScene : GameSection
{
	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<InGameRecorder>.I);
		}
		if (MonoBehaviourSingleton<UIManager>.I.mainChat != null)
		{
			MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName(null);
		}
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			Object.Destroy(MonoBehaviourSingleton<InGameRecorder>.I);
		}
		if (MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			MonoBehaviourSingleton<QuestManager>.I.ClearPlayData();
		}
		if (MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.OnEndInGameScene();
		}
		if (MonoBehaviourSingleton<ChatManager>.IsValid())
		{
			MonoBehaviourSingleton<ChatManager>.I.DestroyRoomChat();
		}
	}
}
