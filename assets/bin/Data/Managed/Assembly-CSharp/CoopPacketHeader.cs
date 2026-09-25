// Decompiled with JetBrains decompiler
// Type: CoopPacketHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public class CoopPacketHeader
{
  public int packetType;
  public int from;
  public int to;
  public bool promise;
  public int sequenceNo;

  public CoopPacketHeader()
  {
  }

  public CoopPacketHeader(int packetType, int from, int to, bool promise, int sequence_no)
  {
    this.packetType = packetType;
    this.from = from;
    this.to = to;
    this.promise = promise;
    this.sequenceNo = sequence_no;
  }

  public override string ToString()
  {
    return $"type={this.packetType} f={this.from} t={this.to} p={this.promise} s={this.sequenceNo}";
  }
}
