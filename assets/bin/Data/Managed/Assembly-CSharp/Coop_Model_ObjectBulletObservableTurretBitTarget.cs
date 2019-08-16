public class Coop_Model_ObjectBulletObservableTurretBitTarget : Coop_Model_ObjectBase
{
	public int observedID;

	public int targetId = -1;

	public int regionId = -1;

	public Coop_Model_ObjectBulletObservableTurretBitTarget()
	{
		base.packetType = PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_TURRETBIT_TARGET;
	}
}
