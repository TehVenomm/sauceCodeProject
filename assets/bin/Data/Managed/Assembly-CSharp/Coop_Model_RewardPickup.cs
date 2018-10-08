public class Coop_Model_RewardPickup : Coop_Model_Base
{
	public int rewardId;

	public int rewardKeyId;

	public string sig;

	public Coop_Model_RewardPickup()
	{
		base.packetType = PACKET_TYPE.REWARD_PICKUP;
	}

	public override string ToString()
	{
		return base.ToString() + ",rewardId=" + rewardId + ",rewardKeyId=" + rewardKeyId + ",sig=" + sig;
	}
}
