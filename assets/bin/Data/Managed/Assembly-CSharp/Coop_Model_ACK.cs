// Decompiled with JetBrains decompiler
// Type: Coop_Model_ACK
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Coop_Model_ACK : Coop_Model_Base
{
  public int ack;
  public bool positive;

  public Coop_Model_ACK() => this.packetType = PACKET_TYPE.ACK;
}
