// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerShotHealingHoming
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_PlayerShotHealingHoming : Coop_Model_ObjectBase
{
  public string atkInfoName;
  public string launchNodeName;
  public Vector3 offsetPos;
  public Vector3 offsetRot;
  public int[] targetPlayerIDs;
  public int targetNum;

  public Coop_Model_PlayerShotHealingHoming()
  {
    this.packetType = PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING;
  }
}
