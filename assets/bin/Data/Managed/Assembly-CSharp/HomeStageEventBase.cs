public class HomeStageEventBase
{
	public string eventName;

	public object eventData;

	public HomeStageEventBase()
		: this()
	{
	}

	protected virtual void Awake()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().set_layer(GetLayer());
	}

	protected virtual int GetLayer()
	{
		return 0;
	}

	public void DispatchEvent()
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		if (!HomeBase.OnAfterGacha2Tutorial || eventName.Contains("QUEST_COUNTER"))
		{
			if (eventName.Contains("QUEST_COUNTER"))
			{
				if ((TutorialStep.HasFirstDeliveryCompleted() && !TutorialStep.HasAllTutorialCompleted()) || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor(0) != null)
				{
					return;
				}
			}
			else if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor(0) != null)
			{
				return;
			}
			if (eventName.Contains("EVENT_COUNTER") && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 20)
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomeStageEventBase", this.get_gameObject(), "QUEST_LOCK", null, null, true);
			}
			else if (!eventName.Contains("POINT_SHOP") && (!eventName.Contains("ARENA_LIST") || (MonoBehaviourSingleton<UserInfoManager>.I.isArenaOpen && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50)) && (!eventName.Contains("BINGO") || MonoBehaviourSingleton<QuestManager>.I.IsBingoPlayableEventExist()) && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomeStageEventBase", this.get_gameObject(), eventName, eventData, null, true);
			}
		}
	}
}
