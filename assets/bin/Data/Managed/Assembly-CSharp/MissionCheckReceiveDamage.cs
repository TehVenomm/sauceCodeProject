using UnityEngine;

public class MissionCheckReceiveDamage : MissionCheckBase
{
	private bool isOver;

	public override bool IsMissionClear()
	{
		return !isOver;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (!((Object)(to_obj as Self) == (Object)null) && status.damage >= missionParam)
		{
			isOver = true;
		}
	}
}
