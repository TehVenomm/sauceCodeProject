using rhyme;
using System.Collections.Generic;

public class ClanPacketReceiver : PacketReceiver
{
	protected override void PacketUpdate()
	{
		if (base.stopPacketUpdate)
		{
			return;
		}
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

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.LOUNGE_ROOM_JOINED:
		{
			Lounge_Model_RoomJoined model13 = packet.GetModel<Lounge_Model_RoomJoined>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomJoined(model13);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_LEAVED:
		{
			Lounge_Model_RoomLeaved model12 = packet.GetModel<Lounge_Model_RoomLeaved>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomLeaved(model12);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_ACTION:
		{
			Lounge_Model_RoomAction model11 = packet.GetModel<Lounge_Model_RoomAction>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomAction(model11);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_MOVE:
		{
			Lounge_Model_RoomMove model10 = packet.GetModel<Lounge_Model_RoomMove>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomMove(model10);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_POSITION:
		{
			Lounge_Model_RoomPosition model9 = packet.GetModel<Lounge_Model_RoomPosition>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomPoisition(model9);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_KICK:
		{
			Lounge_Model_RoomKick model8 = packet.GetModel<Lounge_Model_RoomKick>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomKick(model8);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_AFK_KICK:
		{
			Lounge_Model_AFK_Kick model7 = packet.GetModel<Lounge_Model_AFK_Kick>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomAFKKick(model7);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_LOUNGE:
		{
			Lounge_Model_MemberLounge model6 = packet.GetModel<Lounge_Model_MemberLounge>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberLounge(model6);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_FIELD:
		{
			Lounge_Model_MemberField model5 = packet.GetModel<Lounge_Model_MemberField>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberField(model5);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_QUEST:
		{
			Lounge_Model_MemberQuest model4 = packet.GetModel<Lounge_Model_MemberQuest>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberQuest(model4);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_ARENA:
		{
			Lounge_Model_MemberArena model3 = packet.GetModel<Lounge_Model_MemberArena>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberArena(model3);
			break;
		}
		case PACKET_TYPE.CHAT_MESSAGE:
		{
			Coop_Model_StageChatMessage model2 = packet.GetModel<Coop_Model_StageChatMessage>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvChatMessage(model2);
			break;
		}
		case PACKET_TYPE.STAGE_CHAT_STAMP:
		{
			Coop_Model_StageChatStamp model = packet.GetModel<Coop_Model_StageChatStamp>();
			result = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvChatStamp(model);
			break;
		}
		}
		return result;
	}
}
