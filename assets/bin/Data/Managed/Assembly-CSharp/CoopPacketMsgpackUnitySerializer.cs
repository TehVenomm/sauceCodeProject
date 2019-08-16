using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CoopPacketMsgpackUnitySerializer : CoopPacketSerializer
{
	public int version = int.Parse("10");

	private SerializationContext context = SerializationContext.get_Default();

	private ObjectPacker packer = new ObjectPacker();

	public CoopPacketMsgpackUnitySerializer()
	{
		RegisterOverrideCommon(context);
	}

	public static void RegisterOverrideCommon(SerializationContext context)
	{
		context.get_Serializers().RegisterOverride<Vector3>(new Vector3Serializer(context));
		context.get_Serializers().RegisterOverride<Quaternion>(new QuaternionSerializer(context));
		context.get_Serializers().RegisterOverride<List<int>>(new ListSerializer<int>(context));
		context.get_Serializers().RegisterOverride<List<float>>(new ListSerializer<float>(context));
		context.get_Serializers().RegisterOverride<List<bool>>(new ListSerializer<bool>(context));
		context.get_Serializers().RegisterOverride<List<string>>(new ListSerializer<string>(context));
		context.get_Serializers().RegisterOverride<List<Vector3>>(new ListSerializer<Vector3>(context));
		context.get_Serializers().RegisterOverride<List<SkillInfo.SkillBaseInfo>>(new ListSerializer<SkillInfo.SkillBaseInfo>(context));
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
		byte[] buffer = stream.ReadBytes(len);
		MemoryStream memoryStream = new MemoryStream(buffer);
		CoopPacketHeader result = packer.Unpack<CoopPacketHeader>(memoryStream);
		memoryStream.Close();
		memoryStream = null;
		return result;
	}

	protected override Coop_Model_Base OnDeserializeBinaryModel(PacketMemoryStream stream, Type type, CoopPacketHeader header)
	{
		Type modelType = ((PACKET_TYPE)header.packetType).GetModelType();
		Coop_Model_Base coop_Model_Base = null;
		return (Coop_Model_Base)packer.Unpack(modelType, stream);
	}
}
