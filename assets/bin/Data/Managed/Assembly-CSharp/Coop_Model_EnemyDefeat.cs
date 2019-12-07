using System.Collections.Generic;

public class Coop_Model_EnemyDefeat : Coop_Model_Base
{
	public int sid;

	public int eid;

	public int exp;

	public int money;

	public int ppt;

	public int defeatKeyId;

	public string sig;

	public int x;

	public int z;

	public int rewardId;

	public int rewardId2;

	public List<int> dropIds;

	public List<int> dropTypes;

	public List<int> dropItemIds;

	public List<int> dropNums;

	public List<int> dropParam_0s;

	public int deliver;

	public int boostBit;

	public int boostNum;

	public bool dropLoungeShare;

	public int boxType;

	public Coop_Model_EnemyDefeat()
	{
		base.packetType = PACKET_TYPE.ENEMY_DEFEAT;
	}

	public override string ToString()
	{
		string str_dropIds = "";
		string str_dropItemIds = "";
		string str_dropNums = "";
		if (dropIds != null)
		{
			dropIds.ForEach(delegate(int id)
			{
				str_dropIds = str_dropIds + id + ",";
			});
		}
		if (dropItemIds != null)
		{
			dropItemIds.ForEach(delegate(int id)
			{
				str_dropItemIds = str_dropItemIds + id + ",";
			});
		}
		if (dropParam_0s != null)
		{
			dropNums.ForEach(delegate(int id)
			{
				str_dropNums = str_dropNums + id + ",";
			});
		}
		string text = "";
		text = text + ",sid=" + sid + ",eid=" + eid;
		text = text + ",exp=" + exp + ",money=" + money + ",portalPoint=" + ppt;
		text = text + ",keyid=" + defeatKeyId + ",sig=" + sig;
		text = text + ",rewardId=" + rewardId;
		text = text + ",rewardId2=" + rewardId2;
		text = text + ",dropIds=" + str_dropIds.Trim(',');
		text = text + ",dropItemIds=" + str_dropItemIds.Trim(',');
		text = text + ",dropNums=" + str_dropNums.Trim(',');
		text = text + ",deliverBitFlag=" + deliver;
		text = text + ",deliverBoostBitFlag=" + boostBit;
		text = text + ",deliverBoostNum=" + boostNum;
		text = text + ",boxType=" + boxType;
		return base.ToString() + text;
	}
}
