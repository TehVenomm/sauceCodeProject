public class Coop_Model_ObjectDestroy : Coop_Model_ObjectBase
{
	public Coop_Model_ObjectDestroy()
	{
		base.packetType = PACKET_TYPE.OBJECT_DESTROY;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
