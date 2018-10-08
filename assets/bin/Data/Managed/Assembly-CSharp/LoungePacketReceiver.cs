using rhyme;
using System.Collections.Generic;

public class LoungePacketReceiver : PacketReceiver
{
	protected override void PacketUpdate()
	{
		if (!base.stopPacketUpdate)
		{
			List<CoopPacket> obj = rymTPool<List<CoopPacket>>.Get();
			if (obj.Capacity < base.packets.Count)
			{
				obj.Capacity = base.packets.Count;
			}
			int i = 0;
			for (int count = base.packets.Count; i < count; i++)
			{
				obj.Add(base.packets[i]);
			}
			int j = 0;
			for (int count2 = obj.Count; j < count2; j++)
			{
				if (base.stopPacketUpdate)
				{
					break;
				}
				CoopPacket packet = obj[j];
				if (HandleCoopEvent(packet))
				{
					AddDeleteQueue(packet);
				}
			}
			obj.Clear();
			rymTPool<List<CoopPacket>>.Release(ref obj);
			EraseUsedPacket();
		}
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.LOUNGE_ROOM_JOINED:
		{
			Lounge_Model_RoomJoined model14 = packet.GetModel<Lounge_Model_RoomJoined>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomJoined(model14);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_LEAVED:
		{
			Lounge_Model_RoomLeaved model13 = packet.GetModel<Lounge_Model_RoomLeaved>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomLeaved(model13);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_ACTION:
		{
			Lounge_Model_RoomAction model12 = packet.GetModel<Lounge_Model_RoomAction>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomAction(model12);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_MOVE:
		{
			Lounge_Model_RoomMove model11 = packet.GetModel<Lounge_Model_RoomMove>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomMove(model11);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_POSITION:
		{
			Lounge_Model_RoomPosition model10 = packet.GetModel<Lounge_Model_RoomPosition>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomPoisition(model10);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_KICK:
		{
			Lounge_Model_RoomKick model9 = packet.GetModel<Lounge_Model_RoomKick>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomKick(model9);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_AFK_KICK:
		{
			Lounge_Model_AFK_Kick model8 = packet.GetModel<Lounge_Model_AFK_Kick>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomAFKKick(model8);
			break;
		}
		case PACKET_TYPE.LOUNGE_ROOM_HOST_CHANGED:
		{
			Lounge_Model_RoomHostChanged model7 = packet.GetModel<Lounge_Model_RoomHostChanged>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvRoomHostChanged(model7);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_LOUNGE:
		{
			Lounge_Model_MemberLounge model6 = packet.GetModel<Lounge_Model_MemberLounge>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvMemberLounge(model6);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_FIELD:
		{
			Lounge_Model_MemberField model5 = packet.GetModel<Lounge_Model_MemberField>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvMemberField(model5);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_QUEST:
		{
			Lounge_Model_MemberQuest model4 = packet.GetModel<Lounge_Model_MemberQuest>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvMemberQuest(model4);
			break;
		}
		case PACKET_TYPE.LOUNGE_MEMBER_ARENA:
		{
			Lounge_Model_MemberArena model3 = packet.GetModel<Lounge_Model_MemberArena>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvMemberArena(model3);
			break;
		}
		case PACKET_TYPE.CHAT_MESSAGE:
		{
			Coop_Model_StageChatMessage model2 = packet.GetModel<Coop_Model_StageChatMessage>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvChatMessage(model2);
			break;
		}
		case PACKET_TYPE.STAGE_CHAT_STAMP:
		{
			Coop_Model_StageChatStamp model = packet.GetModel<Coop_Model_StageChatStamp>();
			result = MonoBehaviourSingleton<LoungeNetworkManager>.I.OnRecvChatStamp(model);
			break;
		}
		}
		return result;
	}
}
