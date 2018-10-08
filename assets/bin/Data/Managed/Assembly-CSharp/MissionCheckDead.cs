using UnityEngine;

public class MissionCheckDead : MissionCheckBase
{
	private bool isDead;

	public override bool IsMissionClear()
	{
		return !isDead;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (status.reactionType == 8)
		{
			switch (missionRequire)
			{
			case MISSION_REQUIRE.SELF:
				if (!((Object)(to_obj as Self) == (Object)null))
				{
					isDead = true;
				}
				break;
			case MISSION_REQUIRE.ALL:
				if (!((Object)(to_obj as Player) == (Object)null) && !MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.Contains(to_obj))
				{
					isDead = true;
				}
				break;
			}
		}
	}
}
