// Decompiled with JetBrains decompiler
// Type: MissionCheckBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class MissionCheckBase
{
  protected MISSION_REQUIRE missionRequire;
  protected int missionParam;

  public static MissionCheckBase CreateMissionCheck(QuestTable.MissionTableData data)
  {
    MissionCheckBase missionCheck;
    switch (data.missionType)
    {
      case MISSION_TYPE.DEAD:
        missionCheck = (MissionCheckBase) new MissionCheckDead();
        break;
      case MISSION_TYPE.CLEAR_NUM:
        missionCheck = (MissionCheckBase) new MissionCheckClearNum();
        break;
      case MISSION_TYPE.CLEAR_TIME:
        missionCheck = (MissionCheckBase) new MissionCheckClearTime();
        break;
      case MISSION_TYPE.RECEIVE_DAMAGE:
        missionCheck = (MissionCheckBase) new MissionCheckReceiveDamage();
        break;
      case MISSION_TYPE.ONCE_DAMAGE:
        missionCheck = (MissionCheckBase) new MissionCheckOneDamage();
        break;
      case MISSION_TYPE.BAD_STATUS_COUNT:
        missionCheck = (MissionCheckBase) new MissionCheckBadStatus();
        break;
      case MISSION_TYPE.EQUIP:
        missionCheck = (MissionCheckBase) new MissionCheckEqpip();
        break;
      case MISSION_TYPE.SKILL:
        missionCheck = (MissionCheckBase) new MissionCheckSkill();
        break;
      case MISSION_TYPE.BREAK_NUM:
        missionCheck = (MissionCheckBase) new MissionCheckBreakNum();
        break;
      case MISSION_TYPE.DOWN_COUNT:
        missionCheck = (MissionCheckBase) new MissionCheckDownCount();
        break;
      case MISSION_TYPE.USE_WEAPON:
        missionCheck = (MissionCheckBase) new MissionCheckUseWeapon();
        break;
      default:
        missionCheck = new MissionCheckBase();
        break;
    }
    if (missionCheck == null)
      return (MissionCheckBase) null;
    missionCheck.Initialize(data.missionRequire, data.missionParam);
    return missionCheck;
  }

  protected MissionCheckBase()
  {
  }

  protected virtual void Initialize(MISSION_REQUIRE require, int param)
  {
    this.missionRequire = require;
    this.missionParam = param;
  }

  public virtual bool IsMissionClear() => false;

  public virtual void OnDamage(AttackedHitStatusFix status, Character to_obj)
  {
  }

  public virtual void OnSkillUse(SkillInfo.SkillParam param)
  {
  }
}
