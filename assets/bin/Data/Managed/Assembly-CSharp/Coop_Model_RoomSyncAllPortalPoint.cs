// Decompiled with JetBrains decompiler
// Type: Coop_Model_RoomSyncAllPortalPoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_RoomSyncAllPortalPoint : Coop_Model_Base
{
  public List<Coop_Model_RoomSyncAllPortalPoint.PortalData> ps = new List<Coop_Model_RoomSyncAllPortalPoint.PortalData>();

  public Coop_Model_RoomSyncAllPortalPoint()
  {
    this.packetType = PACKET_TYPE.ROOM_SYNC_ALL_PORTAL_POINT;
  }

  public void SetFromExplorePortalList(List<ExplorePortalPoint> portals)
  {
    for (int index = 0; index < portals.Count; ++index)
    {
      ExplorePortalPoint portal = portals[index];
      if (portal.point > 0 || 0 < portal.used)
        this.ps.Add(new Coop_Model_RoomSyncAllPortalPoint.PortalData(portal.portaiId, portal.point, portal.used));
    }
  }

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
      this.u = used;
    }
  }
}
