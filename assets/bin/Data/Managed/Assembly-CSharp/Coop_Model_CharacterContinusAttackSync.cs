public class Coop_Model_CharacterContinusAttackSync : Coop_Model_ObjectBase
{
	public ContinusAttackParam.SyncParam sync_param;

	public Coop_Model_CharacterContinusAttackSync()
	{
		base.packetType = PACKET_TYPE.CHARACTER_CONTINUS_ATTACK_SYNC;
	}
}
