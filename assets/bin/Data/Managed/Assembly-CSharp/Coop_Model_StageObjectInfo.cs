using System.Text;

public class Coop_Model_StageObjectInfo : Coop_Model_Base
{
	public int StageObjectID;

	public StageObject.COOP_MODE_TYPE CoopModeType;

	public Coop_Model_StageObjectInfo()
	{
		base.packetType = PACKET_TYPE.STAGE_OBJECT_INFO;
		StageObjectID = 0;
		CoopModeType = StageObject.COOP_MODE_TYPE.NONE;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat(",StageObjectID={0}", StageObjectID);
		stringBuilder.AppendFormat(",CoopModeType={0}", CoopModeType);
		return base.ToString() + stringBuilder.ToString();
	}
}
