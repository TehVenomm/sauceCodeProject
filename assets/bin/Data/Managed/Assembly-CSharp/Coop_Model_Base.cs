// Decompiled with JetBrains decompiler
// Type: Coop_Model_Base
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public class Coop_Model_Base
{
  public int c;
  public int id = -1;
  public bool r;
  public float lt;
  public int ct;
  public int u;

  protected PACKET_TYPE packetType
  {
    set => this.c = (int) value;
    get => (PACKET_TYPE) this.c;
  }

  public static Coop_Model_Base.CLIENT_TYPE GetClientType() => Coop_Model_Base.CLIENT_TYPE.ANDROID;

  public virtual bool IsPromiseOverAgainCheck() => false;

  public override string ToString()
  {
    return $"Packet({(Enum) (PACKET_TYPE) this.c}) to object `{this.id}'. r={this.r},lt={this.lt},ct={(Enum) (Coop_Model_Base.CLIENT_TYPE) this.ct},u={this.u}";
  }

  public enum CLIENT_TYPE
  {
    NONE,
    ANDROID,
    IOS,
  }
}
