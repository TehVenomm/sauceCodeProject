using UnityEngine;

public class HomeStageEventBase : MonoBehaviour
{
	public string eventName;

	public object eventData;

	protected virtual void Awake()
	{
		base.gameObject.layer = GetLayer();
	}

	protected virtual int GetLayer()
	{
		return 0;
	}

	public void DispatchEvent()
	{
		if (HomeBase.OnAfterGacha2Tutorial && !eventName.Contains("QUEST_COUNTER"))
		{
			return;
		}
		if (eventName.Contains("QUEST_COUNTER"))
		{
			if ((TutorialStep.HasFirstDeliveryCompleted() && !TutorialStep.HasAllTutorialCompleted()) || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor() != null)
			{
				return;
			}
		}
		else if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor() != null)
		{
			return;
		}
		if (eventName.Contains("EVENT_COUNTER") && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomeStageEventBase", base.gameObject, "QUEST_LOCK");
		}
		else if (!eventName.Contains("POINT_SHOP") && (!eventName.Contains("ARENA_LIST") || (MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)) && (!eventName.Contains("BINGO") || MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist()) && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomeStageEventBase", base.gameObject, eventName, eventData);
		}
	}
}
