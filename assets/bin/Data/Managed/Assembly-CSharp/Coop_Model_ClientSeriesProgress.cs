public class Coop_Model_ClientSeriesProgress : Coop_Model_ObjectBase
{
	public int ep;

	public Coop_Model_ClientSeriesProgress()
	{
		base.packetType = PACKET_TYPE.CLIENT_SERIES_PROGRESS;
	}
}
