// Decompiled with JetBrains decompiler
// Type: CoopPacket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class CoopPacket
{
  public CoopPacketHeader header { get; set; }

  public Coop_Model_Base model { get; set; }

  public PACKET_TYPE packetType
  {
    get => this.model == null ? PACKET_TYPE.ERROR_CONNECT_FAILED : (PACKET_TYPE) this.model.c;
  }

  public int destObjectId => this.model == null ? -1 : this.model.id;

  public int fromClientId => this.header.from;

  public int toClientId => this.header.to;

  public bool promise => this.header.promise;

  public int sequenceNo => this.header.sequenceNo;

  public T GetModel<T>() where T : Coop_Model_Base => this.model as T;

  public override string ToString() => $"header: {this.header}, model: {this.model}";

  public static CoopPacket Create(
    Coop_Model_Base model,
    int from_id,
    int to_id,
    bool promise,
    int sequence_no)
  {
    return new CoopPacket()
    {
      header = new CoopPacketHeader(model.c, from_id, to_id, promise, sequence_no),
      model = model
    };
  }
}
