public class MissionCheckSkill : MissionCheckBase
{
	private int skillCount;

	public override bool IsMissionClear()
	{
		if (missionParam > 0)
		{
			return skillCount >= missionParam;
		}
		return skillCount <= 0;
	}

	public override void OnSkillUse(SkillInfo.SkillParam param)
	{
		switch (missionRequire)
		{
		case MISSION_REQUIRE.ALL:
			skillCount++;
			break;
		case MISSION_REQUIRE.SKILL_ATTACK:
			if (param.tableData.type == SKILL_SLOT_TYPE.ATTACK)
			{
				skillCount++;
			}
			break;
		case MISSION_REQUIRE.SKILL_SUPPORT:
			if (param.tableData.type == SKILL_SLOT_TYPE.SUPPORT)
			{
				skillCount++;
			}
			break;
		case MISSION_REQUIRE.SKILL_HEAL:
			if (param.tableData.type == SKILL_SLOT_TYPE.HEAL)
			{
				skillCount++;
			}
			break;
		}
	}
}
