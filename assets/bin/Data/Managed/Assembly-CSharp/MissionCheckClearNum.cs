public class MissionCheckClearNum : MissionCheckBase
{
	public override bool IsMissionClear()
	{
		return MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count - MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.Count <= missionParam;
	}
}
