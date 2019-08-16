using System.Runtime.InteropServices;

namespace MsgPack
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
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
