// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerSetPresentBullet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_PlayerSetPresentBullet : Coop_Model_ObjectBase
{
  public int presentBulletId;
  public int type;
  public Vector3 position;
  public string bulletName;

  public Coop_Model_PlayerSetPresentBullet()
  {
    this.packetType = PACKET_TYPE.PLAYER_SET_PRESENT_BULLET;
  }
}
