public class Coop_Model_PlayerRestraint : Coop_Model_ObjectSyncPositionBase
{
	public float duration;

	public float damageInterval;

	public int damageRate;

	public float reduceTimeByFlick;

	public string effectName = string.Empty;

	public bool isStopMotion;

	public bool isDisableRemoveByPlayerAttack = true;

	public Coop_Model_PlayerRestraint()
	{
		base.packetType = PACKET_TYPE.PLAYER_RESTRAINT;
	}
}
