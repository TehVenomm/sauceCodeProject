// Decompiled with JetBrains decompiler
// Type: Coop_Model_RoomStageChanged
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_RoomStageChanged : Coop_Model_Base
{
  public int cid;
  public int sid;
  public int pstgid;
  public int pstghostid;
  public int stgid;
  public int stgidx;
  public int stghostid;

  public Coop_Model_RoomStageChanged() => this.packetType = PACKET_TYPE.ROOM_STAGE_CHANGED;

  public override string ToString()
  {
    string str = $"{$"{$"{$"{$"{$"{$",cid={(object) this.cid}"},sid={(object) this.sid}"},pstgid={(object) this.pstgid}"},pstghostid={(object) this.pstghostid}"},stgid={(object) this.stgid}"},stgidx={(object) this.stgidx}"},stghostid={(object) this.stghostid}";
    return base.ToString() + str;
  }
}
