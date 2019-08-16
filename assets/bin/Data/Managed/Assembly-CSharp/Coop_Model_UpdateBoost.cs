public class Coop_Model_UpdateBoost : Coop_Model_Base
{
	public int expUpEnd;

	public int moneyUpEnd;

	public int dropUpEnd;

	public int happenQuestUpEnd;

	public Coop_Model_UpdateBoost()
	{
		base.packetType = PACKET_TYPE.UPDATE_BOOST;
	}

	public override string ToString()
	{
		return base.ToString() + ",expUpEnd=" + expUpEnd + ",moneyUpEnd=" + moneyUpEnd + ",dropUpEnd=" + dropUpEnd + ",happenQuestUpEnd=" + happenQuestUpEnd;
	}
}
