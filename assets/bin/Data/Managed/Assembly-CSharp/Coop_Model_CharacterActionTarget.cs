public class Coop_Model_CharacterActionTarget : Coop_Model_ObjectBase
{
	public int target_id;

	public Coop_Model_CharacterActionTarget()
	{
		base.packetType = PACKET_TYPE.CHARACTER_ACTION_TARGET;
	}
}
