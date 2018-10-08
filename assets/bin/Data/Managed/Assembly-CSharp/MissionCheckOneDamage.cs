public class MissionCheckOneDamage : MissionCheckBase
{
	private bool isOver;

	public override bool IsMissionClear()
	{
		return !isOver;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (!(status.fromObject as Self == null) && status.damage >= missionParam)
		{
			isOver = true;
		}
	}
}
