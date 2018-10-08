using System.Collections.Generic;

public class Coop_Model_EventHappenQuest : Coop_Model_Base
{
	public int qId;

	public List<List<int>> rewards = new List<List<int>>();

	public int rareBossType;

	public Coop_Model_EventHappenQuest()
	{
		base.packetType = PACKET_TYPE.EVENT_HAPPEN_QUEST;
	}

	public override string ToString()
	{
		string reward_str = string.Empty;
		rewards.ForEach(delegate(List<int> r)
		{
			string text = reward_str;
			reward_str = text + "(" + r[0] + "," + r[1] + "," + r[2] + "),";
		});
		return base.ToString() + ",qId=" + qId + ",rewards=" + rewards.Count + "/" + reward_str;
	}
}
