// Decompiled with JetBrains decompiler
// Type: Coop_Model_RoomSyncExploreBoss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class Coop_Model_RoomSyncExploreBoss : Coop_Model_Base
{
  public int ceId;
  public int mId;
  public int hp;
  public int hpm;
  public int bhp;
  public int downCount;
  public float concussionTotal;
  public float concussionMax;
  public float concussionExtend;
  public Coop_Model_RoomSyncExploreBoss.Region[] rs;
  public int shp;
  public uint angid;
  public uint[] eangids;
  public bool isMM;
  public int deadReviveCount;
  public int recoveredHP;

  public Coop_Model_RoomSyncExploreBoss() => this.packetType = PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS;

  public void SetRegions(EnemyRegionWork[] regions)
  {
    this.rs = new Coop_Model_RoomSyncExploreBoss.Region[regions.Length];
    int index = 0;
    for (int length = regions.Length; index < length; ++index)
      this.rs[index] = new Coop_Model_RoomSyncExploreBoss.Region(regions[index]);
  }

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
      this.hp = (int) region.hp;
      this.bt = region.breakTime;
      this.b = region.isBroke;
      this.isd = region.isShieldDamage;
      this.iscd = region.isShieldCriticalDamage;
    }

    public void ApplyTo(EnemyRegionWork region)
    {
      region.hp = (XorInt) this.hp;
      region.breakTime = this.bt;
      region.isBroke = this.b;
      region.isShieldDamage = this.isd;
      region.isShieldCriticalDamage = this.iscd;
    }
  }
}
