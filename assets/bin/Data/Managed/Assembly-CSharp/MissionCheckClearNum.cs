public class MissionCheckClearNum : MissionCheckBase
{
	public override bool IsMissionClear()
	{
		int num = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count - MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.Count;
		return num <= missionParam;
	}
}
