public class Coop_Model_CharacterBuffSync : Coop_Model_ObjectBase
{
	public BuffParam.BuffSyncParam sync_param;

	public Coop_Model_CharacterBuffSync()
	{
		base.packetType = PACKET_TYPE.CHARACTER_BUFFSYNC;
	}
}
