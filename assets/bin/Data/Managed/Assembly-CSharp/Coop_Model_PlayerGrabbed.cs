public class Coop_Model_PlayerGrabbed : Coop_Model_ObjectSyncPositionBase
{
	public int enemyId;

	public string nodeName;

	public float duration;

	public int drainAtkId;

	public Coop_Model_PlayerGrabbed()
	{
		base.packetType = PACKET_TYPE.PLAYER_GRABBED;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction((Character.ACTION_ID)29))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
