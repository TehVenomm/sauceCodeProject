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
		string str_dropIds = string.Empty;
		string str_dropItemIds = string.Empty;
		string str_dropNums = string.Empty;
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
		string empty = string.Empty;
		string text = empty;
		empty = text + ",sid=" + sid + ",eid=" + eid;
		text = empty;
		empty = text + ",exp=" + exp + ",money=" + money + ",portalPoint=" + ppt;
		text = empty;
		empty = text + ",keyid=" + defeatKeyId + ",sig=" + sig;
		empty = empty + ",rewardId=" + rewardId;
		empty = empty + ",rewardId2=" + rewardId2;
		empty = empty + ",dropIds=" + str_dropIds.Trim(',');
		empty = empty + ",dropItemIds=" + str_dropItemIds.Trim(',');
		empty = empty + ",dropNums=" + str_dropNums.Trim(',');
		empty = empty + ",deliverBitFlag=" + deliver;
		empty = empty + ",deliverBoostBitFlag=" + boostBit;
		empty = empty + ",deliverBoostNum=" + boostNum;
		empty = empty + ",boxType=" + boxType;
		return base.ToString() + empty;
	}
}
