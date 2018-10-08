public class Coop_Model_CharacterDead : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_CharacterDead()
	{
		base.packetType = PACKET_TYPE.CHARACTER_DEAD;
	}
}
