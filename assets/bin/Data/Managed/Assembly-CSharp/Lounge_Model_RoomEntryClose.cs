// Decompiled with JetBrains decompiler
// Type: Lounge_Model_RoomEntryClose
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Lounge_Model_RoomEntryClose : Coop_Model_Base
{
  public int reason;

  public Lounge_Model_RoomEntryClose() => this.packetType = PACKET_TYPE.LOUNGE_ROOM_ENTRY_CLOSE;

  public override string ToString() => $"{base.ToString()},reason={(object) this.reason}";
}
