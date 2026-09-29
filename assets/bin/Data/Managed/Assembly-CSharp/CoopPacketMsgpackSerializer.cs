// Decompiled with JetBrains decompiler
// Type: CoopPacketMsgpackSerializer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack;
using MsgPack.Serialization;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class CoopPacketMsgpackSerializer : CoopPacketSerializer
{
  public int version = int.Parse("10");
  private SerializationContext context = SerializationContext.Default;

  public static void RegisterOverrideCommon(SerializationContext context)
  {
    context.Serializers.RegisterOverride<Vector3>((MessagePackSerializer<Vector3>) new Vector3Serializer(context));
    context.Serializers.RegisterOverride<Quaternion>((MessagePackSerializer<Quaternion>) new QuaternionSerializer(context));
    context.Serializers.RegisterOverride<List<int>>((MessagePackSerializer<List<int>>) new ListSerializer<int>(context));
    context.Serializers.RegisterOverride<List<float>>((MessagePackSerializer<List<float>>) new ListSerializer<float>(context));
    context.Serializers.RegisterOverride<List<bool>>((MessagePackSerializer<List<bool>>) new ListSerializer<bool>(context));
    context.Serializers.RegisterOverride<List<string>>((MessagePackSerializer<List<string>>) new ListSerializer<string>(context));
    context.Serializers.RegisterOverride<List<Vector3>>((MessagePackSerializer<List<Vector3>>) new ListSerializer<Vector3>(context));
  }

  public CoopPacketMsgpackSerializer()
  {
    CoopPacketMsgpackSerializer.RegisterOverrideCommon(this.context);
  }

  public void ___iOSJITCompileExceptionAvoidMethod() => this.context.GetSerializer<Quaternion>();

  public override PacketStream Serialize(CoopPacket packet) => this.SerializeBinary(packet);

  protected override void OnSerializeBinaryPrefix(PacketMemoryStream stream)
  {
    this.version = int.Parse("10");
    stream.WriteInt32(this.version);
  }

  protected override void OnSerializeBinaryHeader(
    PacketMemoryStream stream,
    CoopPacketHeader header)
  {
    MemoryStream memoryStream = new MemoryStream();
    this.context.GetSerializer<CoopPacketHeader>().Pack((Stream) memoryStream, header);
    byte[] array = memoryStream.ToArray();
    memoryStream.Close();
    stream.WriteInt32(array.Length);
    stream.WriteBytes(array);
  }

  protected override void OnSerializeBinaryModel(PacketMemoryStream stream, Coop_Model_Base model)
  {
    MessagePackSerializerExtensions.Pack((IMessagePackSerializer) this.context.GetSerializer(((PACKET_TYPE) model.c).GetModelType()), (Stream) stream, (object) model);
  }

  protected override void OnDeserializeBinaryPrefix(PacketMemoryStream stream)
  {
    this.version = stream.ReadInt32();
  }

  protected override CoopPacketHeader OnDeserializeBinaryHeader(PacketMemoryStream stream)
  {
    int len = stream.ReadInt32();
    MemoryStream memoryStream = new MemoryStream(stream.ReadBytes(len));
    CoopPacketHeader coopPacketHeader = this.context.GetSerializer<CoopPacketHeader>().Unpack((Stream) memoryStream);
    memoryStream.Close();
    return coopPacketHeader;
  }

  protected override Coop_Model_Base OnDeserializeBinaryModel(
    PacketMemoryStream stream,
    System.Type type,
    CoopPacketHeader header)
  {
    return (Coop_Model_Base) MessagePackSerializerExtensions.Unpack((IMessagePackSerializer) this.context.GetSerializer(((PACKET_TYPE) header.packetType).GetModelType()), (Stream) stream);
  }
}
