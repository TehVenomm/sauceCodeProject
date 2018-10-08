public class Coop_Model_CharacterIdle : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_CharacterIdle()
	{
		base.packetType = PACKET_TYPE.CHARACTER_IDLE;
	}

	public override bool IsHandleable(StageObject owner)
	{
		return base.IsHandleable(owner);
	}
}
