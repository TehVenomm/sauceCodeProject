public class CoopPacket
{
	public CoopPacketHeader header
	{
		get;
		set;
	}

	public Coop_Model_Base model
	{
		get;
		set;
	}

	public PACKET_TYPE packetType
	{
		get
		{
			if (model == null)
			{
				return PACKET_TYPE.ERROR_CONNECT_FAILED;
			}
			return (PACKET_TYPE)model.c;
		}
	}

	public int destObjectId
	{
		get
		{
			if (model == null)
			{
				return -1;
			}
			return model.id;
		}
	}

	public int fromClientId => header.from;

	public int toClientId => header.to;

	public bool promise => header.promise;

	public int sequenceNo => header.sequenceNo;

	public T GetModel<T>() where T : Coop_Model_Base
	{
		return model as T;
	}

	public override string ToString()
	{
		return $"header: {header}, model: {model}";
	}

	public static CoopPacket Create(Coop_Model_Base model, int from_id, int to_id, bool promise, int sequence_no)
	{
		CoopPacket coopPacket = new CoopPacket();
		CoopPacketHeader coopPacketHeader2 = coopPacket.header = new CoopPacketHeader(model.c, from_id, to_id, promise, sequence_no);
		coopPacket.model = model;
		return coopPacket;
	}
}
