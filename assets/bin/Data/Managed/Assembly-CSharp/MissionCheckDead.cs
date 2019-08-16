public class MissionCheckDead : MissionCheckBase
{
	private bool isDead;

	public override bool IsMissionClear()
	{
		return !isDead;
	}

	public override void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
		if (status.reactionType != 8)
		{
			return;
		}
		switch (missionRequire)
		{
		case MISSION_REQUIRE.SELF:
			if (!(to_obj as Self == null))
			{
				isDead = true;
			}
			break;
		case MISSION_REQUIRE.ALL:
			if (!(to_obj as Player == null) && !MonoBehaviourSingleton<StageObjectManager>.I.nonplayerList.Contains(to_obj))
			{
				isDead = true;
			}
			break;
		}
	}
}
