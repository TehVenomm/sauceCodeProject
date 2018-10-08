public class Coop_Model_CharacterBuffReceive : Coop_Model_ObjectBase
{
	public int type;

	public int value;

	public float time;

	public Coop_Model_CharacterBuffReceive()
	{
		base.packetType = PACKET_TYPE.CHARACTER_BUFFRECEIVE;
	}

	public BuffParam.BuffData Deserialize()
	{
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.type = (BuffParam.BUFFTYPE)type;
		buffData.value = value;
		buffData.time = time;
		return buffData;
	}
}
