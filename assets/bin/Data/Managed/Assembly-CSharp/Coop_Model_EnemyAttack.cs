public class Coop_Model_EnemyAttack : Coop_Model_Base
{
	public int sid;

	public int dmg;

	public Coop_Model_EnemyAttack()
	{
		base.packetType = PACKET_TYPE.ENEMY_ATTACK;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		string text = empty;
		empty = text + ",sid=" + sid + ",dmg=" + dmg;
		return base.ToString() + empty;
	}
}
