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
		return new BuffParam.BuffData
		{
			type = (BuffParam.BUFFTYPE)type,
			value = value,
			valueType = (BuffParam.VALUE_TYPE)valueType,
			fromObjectID = fromObjectID,
			fromEquipIndex = fromEquipIndex,
			fromSkillIndex = fromSkillIndex
		};
	}
}
