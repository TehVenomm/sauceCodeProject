public class Coop_Model_ObjectBulletObservableSearchTarget : Coop_Model_ObjectBase
{
	public int observedID;

	public int targetId = -1;

	public Coop_Model_ObjectBulletObservableSearchTarget()
	{
		base.packetType = PACKET_TYPE.OBJECT_BULLET_OBSERVABLE_SEARCH_TARGET;
	}
}
