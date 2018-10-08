public class Coop_Model_ObjectBulletObservableSet : Coop_Model_ObjectBase
{
	public int observedID;

	public Coop_Model_ObjectBulletObservableSet()
	{
		base.packetType = PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_SET;
	}
}
