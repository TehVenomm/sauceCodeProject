public class Coop_Model_PlayerUpdateSkillInfo : Coop_Model_ObjectBase
{
	public SkillInfo.SkillSettingsInfo settings_info = new SkillInfo.SkillSettingsInfo();

	public Coop_Model_PlayerUpdateSkillInfo()
	{
		base.packetType = PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO;
	}
}
