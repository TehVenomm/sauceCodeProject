// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerShotShieldReflect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_PlayerShotShieldReflect : Coop_Model_ObjectBase
{
  public string atkInfoName;
  public int damage;
  public int targetId;
  public Vector3 offsetPos;
  public Vector3 offsetRot;

  public Coop_Model_PlayerShotShieldReflect()
  {
    this.packetType = PACKET_TYPE.PLAYER_SHOT_SHIELD_REFLECT;
  }
}
