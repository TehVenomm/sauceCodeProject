public class Coop_Model_EnemyOut : Coop_Model_Base
{
	public int sid;

	public int x;

	public int z;

	public bool isEscape;

	public Coop_Model_EnemyOut()
	{
		base.packetType = PACKET_TYPE.ENEMY_OUT;
	}

	public override string ToString()
	{
		return base.ToString() + ",sid=" + sid;
	}
}
