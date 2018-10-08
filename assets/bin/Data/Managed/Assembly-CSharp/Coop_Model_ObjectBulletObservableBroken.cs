public class Coop_Model_ObjectBulletObservableBroken : Coop_Model_ObjectBase
{
	public int observedID;

	public Coop_Model_ObjectBulletObservableBroken()
	{
		base.packetType = PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_BROKEN;
	}
}
