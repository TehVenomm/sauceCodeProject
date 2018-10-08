using System.Collections.Generic;

public class Coop_Model_RoomSyncAllPortalPoint : Coop_Model_Base
{
	public class PortalData
	{
		public int id;

		public int pt;

		public int u;

		public PortalData()
		{
		}

		public PortalData(int id, int pt, int used)
		{
			this.id = id;
			this.pt = pt;
			u = used;
		}
	}

	public List<PortalData> ps = new List<PortalData>();

	public Coop_Model_RoomSyncAllPortalPoint()
	{
		base.packetType = PACKET_TYPE.ROOM_SYNC_ALL_PORTAL_POINT;
	}

	public void SetFromExplorePortalList(List<ExplorePortalPoint> portals)
	{
		for (int i = 0; i < portals.Count; i++)
		{
			ExplorePortalPoint explorePortalPoint = portals[i];
			if (explorePortalPoint.point > 0 || 0 < explorePortalPoint.used)
			{
				PortalData item = new PortalData(explorePortalPoint.portaiId, explorePortalPoint.point, explorePortalPoint.used);
				ps.Add(item);
			}
		}
	}
}
