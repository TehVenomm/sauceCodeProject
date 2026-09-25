// Decompiled with JetBrains decompiler
// Type: Lounge_Model_RoomMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Lounge_Model_RoomMove : Coop_Model_Base
{
  public int cid;
  public Vector3 pos;

  public Lounge_Model_RoomMove() => this.packetType = PACKET_TYPE.LOUNGE_ROOM_MOVE;

  public override string ToString()
  {
    string str = $"{$",cid={(object) this.cid}"},pos={(object) this.pos}";
    return base.ToString() + str;
  }
}
