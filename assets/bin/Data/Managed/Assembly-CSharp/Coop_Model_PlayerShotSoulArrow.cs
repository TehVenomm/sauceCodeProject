// Decompiled with JetBrains decompiler
// Type: Coop_Model_PlayerShotSoulArrow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Coop_Model_PlayerShotSoulArrow : Coop_Model_ObjectSyncPositionBase
{
  public Vector3 shotPos = Vector3.zero;
  public Quaternion bowRot;
  public List<Vector3> targetPosList = new List<Vector3>();

  public Coop_Model_PlayerShotSoulArrow() => this.packetType = PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW;
}
