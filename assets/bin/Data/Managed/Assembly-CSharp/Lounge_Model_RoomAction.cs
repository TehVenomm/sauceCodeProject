// Decompiled with JetBrains decompiler
// Type: Lounge_Model_RoomAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Lounge_Model_RoomAction : Coop_Model_Base
{
  public int cid;
  public int aid;

  public Lounge_Model_RoomAction() => this.packetType = PACKET_TYPE.LOUNGE_ROOM_ACTION;

  public override string ToString()
  {
    string str = $"{$",cid={(object) this.cid}"},aid={(object) this.aid}";
    return base.ToString() + str;
  }
}
