// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyRegionNodeActivate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_EnemyRegionNodeActivate : Coop_Model_ObjectBase
{
  public int[] regionIDs;
  public bool isRandom;
  public int randomSelectedID = -1;

  public Coop_Model_EnemyRegionNodeActivate()
  {
    this.packetType = PACKET_TYPE.ENEMY_REGION_NODE_ACTIVATE;
  }
}
