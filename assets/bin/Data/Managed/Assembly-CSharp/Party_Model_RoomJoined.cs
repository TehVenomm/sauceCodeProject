// Decompiled with JetBrains decompiler
// Type: Party_Model_RoomJoined
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Party_Model_RoomJoined : Coop_Model_Base
{
  public int cid;

  public Party_Model_RoomJoined() => this.packetType = PACKET_TYPE.PARTY_ROOM_JOINED;

  public override string ToString()
  {
    string str = $",cid={(object) this.cid}";
    return base.ToString() + str;
  }
}
