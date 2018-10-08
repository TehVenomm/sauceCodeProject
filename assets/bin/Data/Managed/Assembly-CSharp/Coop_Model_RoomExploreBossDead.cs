using System.Collections.Generic;

public class Coop_Model_RoomExploreBossDead : Coop_Model_Base
{
	public class TotalDamage
	{
		public int uid;

		public int dmg;
	}

	public int downCount;

	public List<int> breakIds;

	public List<TotalDamage> dmgs = new List<TotalDamage>();

	public Coop_Model_RoomExploreBossDead()
	{
		base.packetType = PACKET_TYPE.ROOM_EXPLORE_BOSS_DEAD;
	}

	public void AddTotalDamageFromExplorePlayerStatus(ExplorePlayerStatus status)
	{
		TotalDamage totalDamage = new TotalDamage();
		totalDamage.uid = status.userId;
		totalDamage.dmg = status.givenTotalDamage;
		dmgs.Add(totalDamage);
	}
}
