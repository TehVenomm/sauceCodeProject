public class MissionCheckBase
{
	protected MISSION_REQUIRE missionRequire;

	protected int missionParam;

	protected MissionCheckBase()
	{
	}

	public static MissionCheckBase CreateMissionCheck(QuestTable.MissionTableData data)
	{
		MissionCheckBase missionCheckBase = null;
		switch (data.missionType)
		{
		case MISSION_TYPE.DEAD:
			missionCheckBase = new MissionCheckDead();
			break;
		case MISSION_TYPE.RECEIVE_DAMAGE:
			missionCheckBase = new MissionCheckReceiveDamage();
			break;
		case MISSION_TYPE.CLEAR_NUM:
			missionCheckBase = new MissionCheckClearNum();
			break;
		case MISSION_TYPE.BAD_STATUS_COUNT:
			missionCheckBase = new MissionCheckBadStatus();
			break;
		case MISSION_TYPE.EQUIP:
			missionCheckBase = new MissionCheckEqpip();
			break;
		case MISSION_TYPE.CLEAR_TIME:
			missionCheckBase = new MissionCheckClearTime();
			break;
		case MISSION_TYPE.SKILL:
			missionCheckBase = new MissionCheckSkill();
			break;
		case MISSION_TYPE.BREAK_NUM:
			missionCheckBase = new MissionCheckBreakNum();
			break;
		case MISSION_TYPE.DOWN_COUNT:
			missionCheckBase = new MissionCheckDownCount();
			break;
		case MISSION_TYPE.ONCE_DAMAGE:
			missionCheckBase = new MissionCheckOneDamage();
			break;
		case MISSION_TYPE.USE_WEAPON:
			missionCheckBase = new MissionCheckUseWeapon();
			break;
		default:
			missionCheckBase = new MissionCheckBase();
			break;
		}
		if (missionCheckBase == null)
		{
			return null;
		}
		missionCheckBase.Initialize(data.missionRequire, data.missionParam);
		return missionCheckBase;
	}

	protected virtual void Initialize(MISSION_REQUIRE require, int param)
	{
		missionRequire = require;
		missionParam = param;
	}

	public virtual bool IsMissionClear()
	{
		return false;
	}

	public virtual void OnDamage(AttackedHitStatusFix status, Character to_obj)
	{
	}

	public virtual void OnSkillUse(SkillInfo.SkillParam param)
	{
	}
}
