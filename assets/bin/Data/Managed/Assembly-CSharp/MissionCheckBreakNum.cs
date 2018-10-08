public class MissionCheckBreakNum : MissionCheckBase
{
	public override bool IsMissionClear()
	{
		bool result = false;
		if (MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists != null)
		{
			int i = 0;
			for (int count = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists.Count; i < count; i++)
			{
				if (MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[i].Count > missionParam)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}
}
