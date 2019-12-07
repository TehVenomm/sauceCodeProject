public class Coop_Model_WaveMatchInfo : Coop_Model_Base
{
	public int no;

	public int popGuardSec;

	public int intervalSec;

	public int isFinal;

	public int finalNo;

	public Coop_Model_WaveMatchInfo()
	{
		base.packetType = PACKET_TYPE.WAVEMATCH_INFO;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",no=" + no;
		arg = arg + ",popGuardSec=" + popGuardSec;
		arg = arg + ",intervalSec=" + intervalSec;
		arg = arg + ",isFinal=" + isFinal;
		arg = arg + ",isFinal=" + finalNo;
		return base.ToString() + arg;
	}
}
