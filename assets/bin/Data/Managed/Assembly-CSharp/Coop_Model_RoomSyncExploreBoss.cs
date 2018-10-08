using System;

public class Coop_Model_RoomSyncExploreBoss : Coop_Model_Base
{
	[Serializable]
	public class Region
	{
		private int hp;

		private float bt;

		private bool b;

		private int bh;

		private bool isd;

		private bool iscd;

		public Region()
		{
		}

		public Region(EnemyRegionWork region)
		{
			hp = region.hp;
			bt = region.breakTime;
			b = region.isBroke;
			isd = region.isShieldDamage;
			iscd = region.isShieldCriticalDamage;
		}

		public void ApplyTo(EnemyRegionWork region)
		{
			region.hp = hp;
			region.breakTime = bt;
			region.isBroke = b;
			region.isShieldDamage = isd;
			region.isShieldCriticalDamage = iscd;
		}
	}

	public int mId;

	public int hp;

	public int hpm;

	public int bhp;

	public int downCount;

	public Region[] rs;

	public int shp;

	public uint angid;

	public uint[] eangids;

	public bool isMM;

	public Coop_Model_RoomSyncExploreBoss()
	{
		base.packetType = PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS;
	}

	public void SetRegions(EnemyRegionWork[] regions)
	{
		rs = new Region[regions.Length];
		int i = 0;
		for (int num = regions.Length; i < num; i++)
		{
			rs[i] = new Region(regions[i]);
		}
	}
}
