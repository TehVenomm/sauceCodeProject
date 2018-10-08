namespace MsgPack
{
	public enum TypePrefixes : byte
	{
		PositiveFixNum = 0,
		NegativeFixNum = 224,
		Nil = 192,
		False = 194,
		True = 195,
		Float = 202,
		Double = 203,
		UInt8 = 204,
		UInt16 = 205,
		UInt32 = 206,
		UInt64 = 207,
		Int8 = 208,
		Int16 = 209,
		Int32 = 210,
		Int64 = 211,
		Raw16 = 218,
		Raw32 = 219,
		Array16 = 220,
		Array32 = 221,
		Map16 = 222,
		Map32 = 223,
		FixRaw = 160,
		FixArray = 144,
		FixMap = 0x80,
		Raw8 = 217,
		Bin8 = 196,
		Bin16 = 197,
		Bin32 = 198,
		Ext8 = 199,
		Ext16 = 200,
		Ext32 = 201,
		FixExt = 215
	}
}
