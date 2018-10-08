using System.Collections.Generic;

public class Coop_Model_PlayerGetHeal : Coop_Model_ObjectBase
{
	public int heal_hp;

	public int heal_type;

	public int effect_type;

	public List<int> applyAbilityTypeList;

	public bool receive;

	public Coop_Model_PlayerGetHeal()
	{
		base.packetType = PACKET_TYPE.PLAYER_GET_HEAL;
	}

	public void Serialize(int ownerId, Character.HealData healData, bool isReceive)
	{
		id = ownerId;
		heal_hp = healData.healHp;
		heal_type = (int)healData.healType;
		effect_type = (int)healData.effectType;
		applyAbilityTypeList = healData.applyAbilityTypeList;
		receive = isReceive;
	}

	public Character.HealData Deserialize()
	{
		return new Character.HealData(heal_hp, (HEAL_TYPE)heal_type, (HEAL_EFFECT_TYPE)effect_type, applyAbilityTypeList);
	}
}
