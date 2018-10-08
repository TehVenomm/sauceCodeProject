public class MissionCheckDownCount : MissionCheckBase
{
	private int downCount;

	public override bool IsMissionClear()
	{
		return downCount >= missionParam;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (!(to_obj as Enemy == null) && status.reactionType == 7)
		{
			downCount++;
		}
	}

	public void SetCount(int count)
	{
		downCount = count;
	}
}
