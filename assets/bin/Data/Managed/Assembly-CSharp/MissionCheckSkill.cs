// Decompiled with JetBrains decompiler
// Type: MissionCheckSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class MissionCheckSkill : MissionCheckBase
{
  private int skillCount;

  public override bool IsMissionClear()
  {
    return this.missionParam > 0 ? this.skillCount >= this.missionParam : this.skillCount <= 0;
  }

  public override void OnSkillUse(SkillInfo.SkillParam param)
  {
    switch (this.missionRequire)
    {
      case MISSION_REQUIRE.ALL:
        ++this.skillCount;
        break;
      case MISSION_REQUIRE.SKILL_ATTACK:
        if (param.tableData.type != SKILL_SLOT_TYPE.ATTACK)
          break;
        ++this.skillCount;
        break;
      case MISSION_REQUIRE.SKILL_SUPPORT:
        if (param.tableData.type != SKILL_SLOT_TYPE.SUPPORT)
          break;
        ++this.skillCount;
        break;
      case MISSION_REQUIRE.SKILL_HEAL:
        if (param.tableData.type != SKILL_SLOT_TYPE.HEAL)
          break;
        ++this.skillCount;
        break;
    }
  }
}
