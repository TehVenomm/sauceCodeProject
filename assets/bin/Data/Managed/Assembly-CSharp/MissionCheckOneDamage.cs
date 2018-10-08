using UnityEngine;

public class MissionCheckOneDamage : MissionCheckBase
{
	private bool isOver;

	public override bool IsMissionClear()
	{
		return !isOver;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (!((Object)(status.fromObject as Self) == (Object)null) && status.damage >= missionParam)
		{
			isOver = true;
		}
	}
}
