public class Coop_Model_ActionMine : Coop_Model_ObjectBase
{
	public enum ACTION_TYPE
	{
		DESTROY,
		EXPLODE,
		REFLECT,
		CREATE
	}

	public string atkInfoName;

	public string nodeName;

	public int type;

	public int objId;

	public int randSeed;

	public Coop_Model_ActionMine()
	{
		base.packetType = PACKET_TYPE.ACTION_MINE;
	}
}
