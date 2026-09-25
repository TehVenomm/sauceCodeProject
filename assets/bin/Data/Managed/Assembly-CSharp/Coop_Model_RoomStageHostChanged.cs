// Decompiled with JetBrains decompiler
// Type: Coop_Model_RoomStageHostChanged
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_RoomStageHostChanged : Coop_Model_Base
{
  public int stgid;
  public int stghostid;

  public Coop_Model_RoomStageHostChanged() => this.packetType = PACKET_TYPE.ROOM_STAGE_HOST_CHANGED;

  public override string ToString()
  {
    return $"{base.ToString()} ,stgid={(object) this.stgid},stghostid={(object) this.stghostid}";
  }
}
