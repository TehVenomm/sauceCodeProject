using System.Text;

public class Coop_Model_ObjectCoopInfo : Coop_Model_Base
{
	public StageObject.COOP_MODE_TYPE CoopModeType;

	public Coop_Model_ObjectCoopInfo()
	{
		base.packetType = PACKET_TYPE.OBJECT_COOP_INFO;
		CoopModeType = StageObject.COOP_MODE_TYPE.NONE;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat(",CoopModeType={0}", CoopModeType);
		return base.ToString() + stringBuilder.ToString();
	}
}
