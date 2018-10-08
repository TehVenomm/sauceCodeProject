using System;

internal static class PACKET_TYPE_Extention
{
	public static Type GetModelType(this PACKET_TYPE type)
	{
		return PacketTypeUtility.GetModelType(type);
	}
}
