public class MissionCheckClearTime : MissionCheckBase
{
	public override bool IsMissionClear()
	{
		return (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestLimitTime() - MonoBehaviourSingleton<InGameProgress>.I.remaindTime) / 60f <= (float)missionParam;
	}
}
