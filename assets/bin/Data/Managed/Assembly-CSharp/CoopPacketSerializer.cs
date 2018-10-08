using System;

public class CoopPacketSerializer
{
	public virtual PacketStream Serialize(CoopPacket packet)
	{
		return null;
	}

	public PacketStream SerializeBinary(CoopPacket packet)
	{
		PacketMemoryStream packetMemoryStream = new PacketMemoryStream();
		OnSerializeBinaryPrefix(packetMemoryStream);
		OnSerializeBinaryHeader(packetMemoryStream, packet.header);
		OnSerializeBinaryModel(packetMemoryStream, packet.model);
		byte[] stream = packetMemoryStream.ToArray();
		packetMemoryStream.Close();
		packetMemoryStream = null;
		return new PacketStream(stream);
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
		PacketStringStream packetStringStream = new PacketStringStream();
		OnSerializeStringPrefix(packetStringStream);
		OnSerializeStringHeader(packetStringStream, packet.header);
		OnSerializeStringModel(packetStringStream, packet.model);
		return new PacketStream(packetStringStream.ToString());
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
		{
			return DeserializeBinary<T>(stream.ToBuffer());
		}
		if (stream.IsString())
		{
			return DeserializeString<T>(stream.ToString());
		}
		return null;
	}

	public CoopPacket DeserializeBinary<T>(byte[] data) where T : Coop_Model_Base
	{
		return DeserializeBinary(data, typeof(T));
	}

	private CoopPacket DeserializeBinary(byte[] data, Type type)
	{
		PacketMemoryStream packetMemoryStream = new PacketMemoryStream(data);
		CoopPacket coopPacket = new CoopPacket();
		OnDeserializeBinaryPrefix(packetMemoryStream);
		coopPacket.header = OnDeserializeBinaryHeader(packetMemoryStream);
		try
		{
			coopPacket.model = OnDeserializeBinaryModel(packetMemoryStream, type, coopPacket.header);
		}
		catch (Exception)
		{
		}
		packetMemoryStream.Close();
		packetMemoryStream = null;
		return coopPacket;
	}

	protected virtual void OnDeserializeBinaryPrefix(PacketMemoryStream stream)
	{
	}

	protected virtual CoopPacketHeader OnDeserializeBinaryHeader(PacketMemoryStream stream)
	{
		return null;
	}

	protected virtual Coop_Model_Base OnDeserializeBinaryModel(PacketMemoryStream stream, Type type, CoopPacketHeader header)
	{
		return null;
	}

	public CoopPacket DeserializeString<T>(string data) where T : Coop_Model_Base
	{
		return DeserializeString(data, typeof(T));
	}

	private CoopPacket DeserializeString(string data, Type type)
	{
		PacketStringStream packetStringStream = new PacketStringStream(data);
		CoopPacket coopPacket = new CoopPacket();
		OnDeserializeStringPrefix(packetStringStream);
		coopPacket.header = OnDeserializeStringHeader(packetStringStream);
		coopPacket.model = OnDeserializeStringModel(packetStringStream, type, coopPacket.header);
		packetStringStream.Close();
		packetStringStream = null;
		return coopPacket;
	}

	protected virtual void OnDeserializeStringPrefix(PacketStringStream stream)
	{
	}

	protected virtual CoopPacketHeader OnDeserializeStringHeader(PacketStringStream stream)
	{
		return null;
	}

	protected virtual Coop_Model_Base OnDeserializeStringModel(PacketStringStream stream, Type type, CoopPacketHeader header)
	{
		return null;
	}
}
