// Decompiled with JetBrains decompiler
// Type: CoopPacketSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class CoopPacketSerializer
{
  public virtual PacketStream Serialize(CoopPacket packet) => (PacketStream) null;

  public PacketStream SerializeBinary(CoopPacket packet)
  {
    PacketMemoryStream stream = new PacketMemoryStream();
    this.OnSerializeBinaryPrefix(stream);
    this.OnSerializeBinaryHeader(stream, packet.header);
    this.OnSerializeBinaryModel(stream, packet.model);
    byte[] array = stream.ToArray();
    stream.Close();
    return new PacketStream((object) array);
  }

  protected virtual void OnSerializeBinaryPrefix(PacketMemoryStream stream)
  {
  }

  protected virtual void OnSerializeBinaryHeader(PacketMemoryStream stream, CoopPacketHeader header)
  {
  }

  protected virtual void OnSerializeBinaryModel(PacketMemoryStream stream, Coop_Model_Base model)
  {
  }

  public virtual PacketStream SerializeString(CoopPacket packet)
  {
    PacketStringStream stream = new PacketStringStream();
    this.OnSerializeStringPrefix(stream);
    this.OnSerializeStringHeader(stream, packet.header);
    this.OnSerializeStringModel(stream, packet.model);
    return new PacketStream((object) stream.ToString());
  }

  protected virtual void OnSerializeStringPrefix(PacketStringStream stream)
  {
  }

  protected virtual void OnSerializeStringHeader(PacketStringStream stream, CoopPacketHeader header)
  {
  }

  protected virtual void OnSerializeStringModel(PacketStringStream stream, Coop_Model_Base model)
  {
  }

  public CoopPacket Deserialize<T>(PacketStream stream) where T : Coop_Model_Base
  {
    if (stream.IsBuffer())
      return this.DeserializeBinary<T>(stream.ToBuffer());
    return stream.IsString() ? this.DeserializeString<T>(stream.ToString()) : (CoopPacket) null;
  }

  public CoopPacket DeserializeBinary<T>(byte[] data) where T : Coop_Model_Base
  {
    return this.DeserializeBinary(data, typeof (T));
  }

  private CoopPacket DeserializeBinary(byte[] data, System.Type type)
  {
    PacketMemoryStream stream = new PacketMemoryStream(data);
    CoopPacket coopPacket = new CoopPacket();
    this.OnDeserializeBinaryPrefix(stream);
    coopPacket.header = this.OnDeserializeBinaryHeader(stream);
    try
    {
      coopPacket.model = this.OnDeserializeBinaryModel(stream, type, coopPacket.header);
    }
    catch (Exception ex)
    {
    }
    stream.Close();
    return coopPacket;
  }

  protected virtual void OnDeserializeBinaryPrefix(PacketMemoryStream stream)
  {
  }

  protected virtual CoopPacketHeader OnDeserializeBinaryHeader(PacketMemoryStream stream)
  {
    return (CoopPacketHeader) null;
  }

  protected virtual Coop_Model_Base OnDeserializeBinaryModel(
    PacketMemoryStream stream,
    System.Type type,
    CoopPacketHeader header)
  {
    return (Coop_Model_Base) null;
  }

  public CoopPacket DeserializeString<T>(string data) where T : Coop_Model_Base
  {
    return this.DeserializeString(data, typeof (T));
  }

  private CoopPacket DeserializeString(string data, System.Type type)
  {
    PacketStringStream stream = new PacketStringStream(data);
    CoopPacket coopPacket = new CoopPacket();
    this.OnDeserializeStringPrefix(stream);
    coopPacket.header = this.OnDeserializeStringHeader(stream);
    coopPacket.model = this.OnDeserializeStringModel(stream, type, coopPacket.header);
    stream.Close();
    return coopPacket;
  }

  protected virtual void OnDeserializeStringPrefix(PacketStringStream stream)
  {
  }

  protected virtual CoopPacketHeader OnDeserializeStringHeader(PacketStringStream stream)
  {
    return (CoopPacketHeader) null;
  }

  protected virtual Coop_Model_Base OnDeserializeStringModel(
    PacketStringStream stream,
    System.Type type,
    CoopPacketHeader header)
  {
    return (Coop_Model_Base) null;
  }
}
