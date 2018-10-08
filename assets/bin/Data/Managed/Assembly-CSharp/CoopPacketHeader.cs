using System;

[Serializable]
public class CoopPacketHeader
{
	public int packetType;

	public int from;

	public int to;

	public bool promise;

	public int sequenceNo;

	public CoopPacketHeader()
	{
	}

	public CoopPacketHeader(int packetType, int from, int to, bool promise, int sequence_no)
	{
		this.packetType = packetType;
		this.from = from;
		this.to = to;
		this.promise = promise;
		sequenceNo = sequence_no;
	}

	public override string ToString()
	{
		return $"type={packetType} f={from} t={to} p={promise} s={sequenceNo}";
	}
}
