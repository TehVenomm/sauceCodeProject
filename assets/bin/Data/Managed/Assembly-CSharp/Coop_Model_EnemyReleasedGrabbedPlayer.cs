public class Coop_Model_EnemyReleasedGrabbedPlayer : Coop_Model_ObjectSyncPositionBase
{
	public float angle;

	public float power;

	public Coop_Model_EnemyReleasedGrabbedPlayer()
	{
		base.packetType = PACKET_TYPE.ENEMY_RELEASE_GRABBED_PLAYER;
	}

	public override bool IsHandleable(StageObject owner)
	{
		return base.IsHandleable(owner);
	}
}
