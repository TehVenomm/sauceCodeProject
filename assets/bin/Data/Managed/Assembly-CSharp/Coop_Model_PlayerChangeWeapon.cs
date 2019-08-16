public class Coop_Model_PlayerChangeWeapon : Coop_Model_ObjectBase
{
	public Coop_Model_PlayerChangeWeapon()
	{
		base.packetType = PACKET_TYPE.PLAYER_CHANGE_WEAPON;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)27))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
