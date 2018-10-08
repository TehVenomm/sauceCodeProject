using Network;

public class Coop_Model_ClientChangeEquip : Coop_Model_ObjectBase
{
	public CharaInfo userInfo = new CharaInfo();

	public Coop_Model_ClientChangeEquip()
	{
		base.packetType = PACKET_TYPE.CLIENT_CHANGE_EQUIP;
	}
}
