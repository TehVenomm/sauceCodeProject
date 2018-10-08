using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CoopPacketMsgpackSerializer : CoopPacketSerializer
{
	public int version = int.Parse("10");

	private SerializationContext context = SerializationContext.get_Default();

	public CoopPacketMsgpackSerializer()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		RegisterOverrideCommon(context);
	}

	public static void RegisterOverrideCommon(SerializationContext context)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		context.get_Serializers().RegisterOverride<Vector3>(new Vector3Serializer(context));
		context.get_Serializers().RegisterOverride<Quaternion>(new QuaternionSerializer(context));
		context.get_Serializers().RegisterOverride<List<int>>(new ListSerializer<int>(context));
		context.get_Serializers().RegisterOverride<List<float>>(new ListSerializer<float>(context));
		context.get_Serializers().RegisterOverride<List<bool>>(new ListSerializer<bool>(context));
		context.get_Serializers().RegisterOverride<List<string>>(new ListSerializer<string>(context));
		context.get_Serializers().RegisterOverride<List<Vector3>>(new ListSerializer<Vector3>(context));
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
		serializer.Pack((Stream)memoryStream, header);
		byte[] array = memoryStream.ToArray();
		memoryStream.Close();
		memoryStream = null;
		stream.WriteInt32(array.Length);
		stream.WriteBytes(array);
	}

	protected override void OnSerializeBinaryModel(PacketMemoryStream stream, Coop_Model_Base model)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		Type modelType = ((PACKET_TYPE)model.c).GetModelType();
		IMessagePackSingleObjectSerializer val = context.GetSerializer(modelType);
		MessagePackSerializerExtensions.Pack(val, (Stream)stream, (object)model);
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
		CoopPacketHeader result = serializer.Unpack((Stream)memoryStream);
		memoryStream.Close();
		memoryStream = null;
		return result;
	}

	protected override Coop_Model_Base OnDeserializeBinaryModel(PacketMemoryStream stream, Type type, CoopPacketHeader header)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		Type modelType = ((PACKET_TYPE)header.packetType).GetModelType();
		IMessagePackSingleObjectSerializer val = context.GetSerializer(modelType);
		return (Coop_Model_Base)MessagePackSerializerExtensions.Unpack(val, (Stream)stream);
	}
}
