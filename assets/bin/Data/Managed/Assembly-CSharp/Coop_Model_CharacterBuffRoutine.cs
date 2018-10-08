public class Coop_Model_CharacterBuffRoutine : Coop_Model_ObjectBase
{
	public int type;

	public int value;

	public int valueType;

	public int fromObjectID;

	public int fromEquipIndex;

	public int fromSkillIndex;

	public Coop_Model_CharacterBuffRoutine()
	{
		base.packetType = PACKET_TYPE.CHARACTER_BUFFROUTINE;
	}

	public BuffParam.BuffData Deserialize()
	{
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.type = (BuffParam.BUFFTYPE)type;
		buffData.value = value;
		buffData.valueType = (BuffParam.VALUE_TYPE)valueType;
		buffData.fromObjectID = fromObjectID;
		buffData.fromEquipIndex = fromEquipIndex;
		buffData.fromSkillIndex = fromSkillIndex;
		return buffData;
	}
}
