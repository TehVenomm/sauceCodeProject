using MsgPack;
using MsgPack.Serialization;
using System;
using System.IO;
using UnityEngine;

public class CoopPacketMsgpackSerializer : CoopPacketSerializer
{
	public int version = int.Parse("10");

	private SerializationContext context = SerializationContext.Default;

	public CoopPacketMsgpackSerializer()
	{
		RegisterOverrideCommon(context);
	}

	public static void RegisterOverrideCommon(SerializationContext context)
	{
		context.Serializers.RegisterOverride(new Vector3Serializer(context));
		context.Serializers.RegisterOverride(new QuaternionSerializer(context));
		context.Serializers.RegisterOverride(new ListSerializer<int>(context));
		context.Serializers.RegisterOverride(new ListSerializer<float>(context));
		context.Serializers.RegisterOverride(new ListSerializer<bool>(context));
		context.Serializers.RegisterOverride(new ListSerializer<string>(context));
		context.Serializers.RegisterOverride(new ListSerializer<Vector3>(context));
	}

	public void ___iOSJITCompileExceptionAvoidMethod()
	{
		context.GetSerializer<Quaternion>();
	}

	public override PacketStream Serialize(CoopPacket packet)
	{
		return SerializeBinary(packet);
	}

	protected override void OnSerializeBinaryPrefix(PacketMemoryStream stream)
	{
		version = int.Parse("10");
		stream.WriteInt32(version);
	}

	protected override void OnSerializeBinaryHeader(PacketMemoryStream stream, CoopPacketHeader header)
	{
		MemoryStream memoryStream = new MemoryStream();
		MessagePackSerializer<CoopPacketHeader> serializer = context.GetSerializer<CoopPacketHeader>();
		serializer.Pack(memoryStream, header);
		byte[] array = memoryStream.ToArray();
		memoryStream.Close();
		memoryStream = null;
		stream.WriteInt32(array.Length);
		stream.WriteBytes(array);
	}

	protected override void OnSerializeBinaryModel(PacketMemoryStream stream, Coop_Model_Base model)
	{
		Type modelType = ((PACKET_TYPE)model.c).GetModelType();
		IMessagePackSingleObjectSerializer serializer = context.GetSerializer(modelType);
		serializer.Pack(stream, model);
	}

	protected override void OnDeserializeBinaryPrefix(PacketMemoryStream stream)
	{
		version = stream.ReadInt32();
	}

	protected override CoopPacketHeader OnDeserializeBinaryHeader(PacketMemoryStream stream)
	{
		int len = stream.ReadInt32();
		byte[] buffer = stream.ReadBytes(len);
		MemoryStream memoryStream = new MemoryStream(buffer);
		MessagePackSerializer<CoopPacketHeader> serializer = context.GetSerializer<CoopPacketHeader>();
		CoopPacketHeader result = serializer.Unpack(memoryStream);
		memoryStream.Close();
		memoryStream = null;
		return result;
	}

	protected override Coop_Model_Base OnDeserializeBinaryModel(PacketMemoryStream stream, Type type, CoopPacketHeader header)
	{
		Type modelType = ((PACKET_TYPE)header.packetType).GetModelType();
		IMessagePackSingleObjectSerializer serializer = context.GetSerializer(modelType);
		return (Coop_Model_Base)serializer.Unpack(stream);
	}
}
