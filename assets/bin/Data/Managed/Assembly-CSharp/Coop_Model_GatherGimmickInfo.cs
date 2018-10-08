using System.Text;

public class Coop_Model_GatherGimmickInfo : Coop_Model_Base
{
	public int managedId;

	public int ownerId;

	public bool isUsed;

	public Coop_Model_GatherGimmickInfo()
	{
		base.packetType = PACKET_TYPE.GATHER_GIMMICK_INFO;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat(",managedId={0}", managedId);
		stringBuilder.AppendFormat(",ownerId={0}", ownerId);
		stringBuilder.AppendFormat(",isUsed={0}", isUsed);
		return base.ToString() + stringBuilder.ToString();
	}
}
