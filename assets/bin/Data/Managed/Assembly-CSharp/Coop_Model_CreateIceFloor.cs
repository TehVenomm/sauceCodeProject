// Decompiled with JetBrains decompiler
// Type: Coop_Model_CreateIceFloor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Coop_Model_CreateIceFloor : Coop_Model_ObjectBase
{
  public string atkName;
  public List<Vector3> posList;
  public List<Quaternion> rotList;

  public Coop_Model_CreateIceFloor() => this.packetType = PACKET_TYPE.CREATE_ICE_FLOOR;
}
