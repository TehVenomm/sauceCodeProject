using MsgPack;
using MsgPack.Serialization;
using System;
using System.IO;
using UnityEngine;

public class CoopPacketMsgpackUnitySerializer : CoopPacketSerializer
{
	public int version = int.Parse("10");

	private SerializationContext context = SerializationContext.Default;

	private ObjectPacker packer = new ObjectPacker();

	public static void RegisterOverrideCommon(SerializationContext context)
	{
		context.Serializers.RegisterOverride(new Vector3Serializer(context));
		context.Serializers.RegisterOverride(new QuaternionSerializer(context));
		context.Serializers.RegisterOverride(new ListSerializer<int>(context));
		context.Serializers.RegisterOverride(new ListSerializer<float>(context));
		context.Serializers.RegisterOverride(new ListSerializer<bool>(context));
		context.Serializers.RegisterOverride(new ListSerializer<string>(context));
		context.Serializers.RegisterOverride(new ListSerializer<Vector3>(context));
		context.Serializers.RegisterOverride(new ListSerializer<SkillInfo.SkillBaseInfo>(context));
	}

	public CoopPacketMsgpackUnitySerializer()
	{
		RegisterOverrideCommon(context);
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
		packer.Pack(memoryStream, header);
		byte[] array = memoryStream.ToArray();
		memoryStream.Close();
		memoryStream = null;
		stream.WriteInt32(array.Length);
		stream.WriteBytes(array);
	}

	protected override void OnSerializeBinaryModel(PacketMemoryStream stream, Coop_Model_Base model)
	{
		packer.Pack(stream, model);
	}

	protected override void OnDeserializeBinaryPrefix(PacketMemoryStream stream)
	{
		version = stream.ReadInt32();
	}

	protected override CoopPacketHeader OnDeserializeBinaryHeader(PacketMemoryStream stream)
	{
		int len = stream.ReadInt32();
		MemoryStream memoryStream = new MemoryStream(stream.ReadBytes(len));
		CoopPacketHeader result = packer.Unpack<CoopPacketHeader>(memoryStream);
		memoryStream.Close();
		memoryStream = null;
		return result;
	}

	protected override Coop_Model_Base OnDeserializeBinaryModel(PacketMemoryStream stream, Type type, CoopPacketHeader header)
	{
		Type modelType = ((PACKET_TYPE)header.packetType).GetModelType();
		return (Coop_Model_Base)packer.Unpack(modelType, stream);
	}
}
