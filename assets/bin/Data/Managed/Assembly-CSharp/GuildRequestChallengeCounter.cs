public class GuildRequestChallengeCounter : QuestChallengeSelect
{
	public override void Initialize()
	{
		base.Initialize();
	}

	public override void OnQuery_SELECT_ORDER()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= challengeData.Length)
		{
			GameSection.StopEvent();
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog((uint)challengeData[num].questId))
		{
			GameSection.StopEvent();
			return;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)challengeData[num].questId);
		GameSection.SetEventData(MonoBehaviourSingleton<QuestManager>.I.GetQuestChallengeInfoData((uint)challengeData[num].questId));
		isScrollViewReady = false;
	}

	private void OnCloseDialog_GuildRequestChallengeRoomCondition()
	{
		OnCloseDialog_QuestAcceptChallengeRoomCondition();
	}
}
