using System;

internal static class CHAT_PACKET_TYPE_Extention
{
	public static Type GetModelType(this CHAT_PACKET_TYPE type)
	{
		return typeof(Chat_Model_Base);
	}
}
