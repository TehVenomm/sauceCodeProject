// Decompiled with JetBrains decompiler
// Type: Coop_Model_RoomExploreBossDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class Coop_Model_RoomExploreBossDead : Coop_Model_Base
{
  public int downCount;
  public float concussionTotal;
  public float concussionMax;
  public float concussionExtend;
  public List<int> breakIds;
  public List<Coop_Model_RoomExploreBossDead.TotalDamage> dmgs = new List<Coop_Model_RoomExploreBossDead.TotalDamage>();

  public Coop_Model_RoomExploreBossDead() => this.packetType = PACKET_TYPE.ROOM_EXPLORE_BOSS_DEAD;

  public void AddTotalDamageFromExplorePlayerStatus(ExplorePlayerStatus status)
  {
    this.dmgs.Add(new Coop_Model_RoomExploreBossDead.TotalDamage()
    {
      uid = status.userId,
      dmg = status.givenTotalDamage
    });
  }

  public class TotalDamage
  {
    public int uid;
    public int dmg;
  }
}
