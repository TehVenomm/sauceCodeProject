public class MissionCheckBreakNum : MissionCheckBase
{
	public override bool IsMissionClear()
	{
		int num = 0;
		if (MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists != null)
		{
			int i = 0;
			for (int count = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists.Count; i < count; i++)
			{
				num += MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[i].Count - 1;
			}
		}
		return num >= missionParam;
	}
}
