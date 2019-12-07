using System;
using System.Globalization;

public class CoopPacketJsonSerializer : CoopPacketSerializer
{
	public string version = "10";

	public string ConvertUserToken(int client_id, int user_token_len)
	{
		string text = "";
		switch (client_id)
		{
		case -1000:
			return "";
		case -2000:
			return " ";
		default:
			return client_id.ToString().PadLeft(user_token_len);
		}
	}

	public int ConvertClientId(string user_token)
	{
		int num = 0;
		if (user_token == null || user_token.Length != 0)
		{
			if (user_token == " ")
			{
				return -2000;
			}
			return int.Parse(user_token);
		}
		return -1000;
	}

	public override PacketStream Serialize(CoopPacket packet)
	{
		return SerializeString(packet);
	}

	protected override void OnSerializeStringPrefix(PacketStringStream stream)
	{
		version = "10";
		stream.Write(version);
	}

	protected override void OnSerializeStringHeader(PacketStringStream stream, CoopPacketHeader header)
	{
		int user_token_len = (!(version == "00")) ? 1 : 11;
		string str = "";
		str += ConvertUserToken(header.from, user_token_len);
		str += ConvertUserToken(header.to, user_token_len);
		str += (header.promise ? "1" : "0");
		str += header.sequenceNo.ToString().PadLeft(16);
		string str2 = str.Length.ToString("X4");
		stream.Write(str2);
		stream.Write(str);
	}

	protected override void OnSerializeStringModel(PacketStringStream stream, Coop_Model_Base model)
	{
		Type modelType = ((PACKET_TYPE)model.c).GetModelType();
		string str = JSONSerializer.Serialize(model, modelType);
		stream.Write(str);
	}

	protected override void OnDeserializeStringPrefix(PacketStringStream stream)
	{
		version = stream.Read("10".Length);
	}

	protected override CoopPacketHeader OnDeserializeStringHeader(PacketStringStream stream)
	{
		string text = stream.Read(4);
		int len = (!(version == "00")) ? 1 : 11;
		int position = stream.Position;
		string user_token = stream.Read(len);
		string user_token2 = stream.Read(len);
		string a = stream.Read(1);
		string s = stream.Read(16);
		int num = stream.Position - position;
		if (num.ToString("X4") != text)
		{
			Log.Error(LOG.WEBSOCK, "break header packet! {0} != {1}", num, int.Parse(text, NumberStyles.HexNumber));
		}
		int from = ConvertClientId(user_token);
		int to = ConvertClientId(user_token2);
		bool promise = a == "1";
		int sequence_no = int.Parse(s);
		return new CoopPacketHeader(0, from, to, promise, sequence_no);
	}

	protected override Coop_Model_Base OnDeserializeStringModel(PacketStringStream stream, Type type, CoopPacketHeader header)
	{
		string message = stream.Read();
		Type modelType = ((PACKET_TYPE)JSONSerializer.Deserialize<Coop_Model_Base>(message).c).GetModelType();
		return JSONSerializer.Deserialize<Coop_Model_Base>(message, modelType);
	}
}
