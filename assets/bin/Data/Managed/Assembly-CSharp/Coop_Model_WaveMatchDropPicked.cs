public class Coop_Model_WaveMatchDropPicked : Coop_Model_Base
{
	public int managedId;

	public uint tableId;

	public Coop_Model_WaveMatchDropPicked()
	{
		base.packetType = PACKET_TYPE.WAVEMATCH_DROP_PICKED;
	}

	public override string ToString()
	{
		return base.ToString() + "/" + managedId.ToString() + "/" + tableId.ToString();
	}
}
