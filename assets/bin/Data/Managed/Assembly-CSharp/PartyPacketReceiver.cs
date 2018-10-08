using rhyme;
using System.Collections.Generic;

public class PartyPacketReceiver : PacketReceiver
{
	protected override void PacketUpdate()
	{
		if (!base.stopPacketUpdate)
		{
			List<CoopPacket> list = rymTPool<List<CoopPacket>>.Get();
			if (list.Capacity < base.packets.Count)
			{
				list.Capacity = base.packets.Count;
			}
			int i = 0;
			for (int count = base.packets.Count; i < count; i++)
			{
				list.Add(base.packets[i]);
			}
			int j = 0;
			for (int count2 = list.Count; j < count2; j++)
			{
				if (base.stopPacketUpdate)
				{
					break;
				}
				CoopPacket packet = list[j];
				if (HandleCoopEvent(packet))
				{
					AddDeleteQueue(packet);
				}
			}
			list.Clear();
			rymTPool<List<CoopPacket>>.Release(ref list);
			EraseUsedPacket();
		}
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.PARTY_ROOM_JOINED:
		{
			Party_Model_RoomJoined model4 = packet.GetModel<Party_Model_RoomJoined>();
			result = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvRoomJoined(model4);
			break;
		}
		case PACKET_TYPE.PARTY_ROOM_LEAVED:
		{
			Party_Model_RoomLeaved model3 = packet.GetModel<Party_Model_RoomLeaved>();
			result = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvRoomLeaved(model3);
			break;
		}
		case PACKET_TYPE.CHAT_MESSAGE:
		{
			Coop_Model_StageChatMessage model2 = packet.GetModel<Coop_Model_StageChatMessage>();
			result = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvChatMessage(model2);
			break;
		}
		case PACKET_TYPE.STAGE_CHAT_STAMP:
		{
			Coop_Model_StageChatStamp model = packet.GetModel<Coop_Model_StageChatStamp>();
			result = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvChatStamp(model);
			break;
		}
		}
		return result;
	}
}
