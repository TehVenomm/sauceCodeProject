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
		return new BuffParam.BuffData
		{
			type = (BuffParam.BUFFTYPE)type,
			value = value,
			time = time
		};
	}
}
