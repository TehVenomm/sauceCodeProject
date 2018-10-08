public class MissionCheckBadStatus : MissionCheckBase
{
	private int badStatusCount;

	public override bool IsMissionClear()
	{
		return badStatusCount <= missionParam;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (!(to_obj as Self == null) && (status.badStatusTotal.paralyze > 0f || status.badStatusTotal.poison > 0f || status.badStatusTotal.burning > 0f || status.badStatusTotal.speedDown > 0f || status.badStatusTotal.attackSpeedDown > 0f))
		{
			badStatusCount++;
		}
	}
}
