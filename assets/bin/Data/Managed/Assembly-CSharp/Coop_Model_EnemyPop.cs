public class Coop_Model_EnemyPop : Coop_Model_Base
{
	public int sid;

	public int ownerClientId;

	public int popIndex;

	public int seriesIdx;

	public bool setPos;

	public float x;

	public float z;

	public Coop_Model_EnemyPop()
	{
		base.packetType = PACKET_TYPE.ENEMY_POP;
	}

	public override string ToString()
	{
		return base.ToString() + $",sid={sid},ownerClientId={ownerClientId},popIndex={popIndex},seriesIdx={seriesIdx}";
	}
}
