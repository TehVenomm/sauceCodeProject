public class Coop_Model_RewardGet : Coop_Model_Base
{
	public int rewardId;

	public Coop_Model_RewardGet()
	{
		base.packetType = PACKET_TYPE.REWARD_GET;
	}

	public override string ToString()
	{
		return base.ToString() + ",rewardId=" + rewardId;
	}
}
