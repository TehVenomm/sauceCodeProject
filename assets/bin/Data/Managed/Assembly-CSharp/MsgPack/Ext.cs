namespace MsgPack
{
	public struct Ext
	{
		public sbyte Type
		{
			get;
			private set;
		}

		public byte[] Data
		{
			get;
			private set;
		}

		public Ext(sbyte type, byte[] data)
		{
			Type = type;
			Data = data;
		}
	}
}
