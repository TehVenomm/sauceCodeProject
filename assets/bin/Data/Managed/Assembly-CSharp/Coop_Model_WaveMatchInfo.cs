public class Coop_Model_WaveMatchInfo : Coop_Model_Base
{
	public int no;

	public int popGuardSec;

	public int intervalSec;

	public int isFinal;

	public Coop_Model_WaveMatchInfo()
	{
		base.packetType = PACKET_TYPE.WAVEMATCH_INFO;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",no=" + no;
		empty = empty + ",popGuardSec=" + popGuardSec;
		empty = empty + ",intervalSec=" + intervalSec;
		empty = empty + ",isFinal=" + isFinal;
		return base.ToString() + empty;
	}
}
