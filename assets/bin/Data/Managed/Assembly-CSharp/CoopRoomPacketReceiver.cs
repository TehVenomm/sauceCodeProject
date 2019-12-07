public class CoopRoomPacketReceiver : PacketReceiver
{
	private CoopRoom coopRoom
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		coopRoom = base.gameObject.GetComponent<CoopRoom>();
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.ROOM_SYNC_ALL_PORTAL_POINT:
		{
			Coop_Model_RoomSyncAllPortalPoint model14 = packet.GetModel<Coop_Model_RoomSyncAllPortalPoint>();
			coopRoom.OnRecvSyncAllPortalPoint(model14);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_UPDATE_PORTAL_POINT:
		{
			Coop_Model_RoomUpdatePortalPoint model13 = packet.GetModel<Coop_Model_RoomUpdatePortalPoint>();
			coopRoom.OnRecvRoomUpdatePortalPoint(model13);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS:
		{
			Coop_Model_RoomSyncExploreBoss model12 = packet.GetModel<Coop_Model_RoomSyncExploreBoss>();
			coopRoom.OnRecvSyncExploreBoss(model12);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS_MAP:
		{
			Coop_Model_RoomSyncExploreBossMap model11 = packet.GetModel<Coop_Model_RoomSyncExploreBossMap>();
			coopRoom.OnRecvSyncExploreBossMap(model11);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_EXPLORE_BOSS_DAMAGE:
		{
			Coop_Model_RoomExploreBossDamage model10 = packet.GetModel<Coop_Model_RoomExploreBossDamage>();
			coopRoom.OnRecvExploreBossDamage(packet.fromClientId, model10);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_EXPLORE_ALIVE:
			packet.GetModel<Coop_Model_RoomExploreAlive>();
			coopRoom.OnRecvExploreAlive();
			result = true;
			break;
		case PACKET_TYPE.ROOM_EXPLORE_ALIVE_REQUEST:
			packet.GetModel<Coop_Model_RoomExploreAliveRequest>();
			coopRoom.OnRecvExploreAliveRequest();
			result = true;
			break;
		case PACKET_TYPE.ROOM_EXPLORE_BOSS_DEAD:
		{
			Coop_Model_RoomExploreBossDead model9 = packet.GetModel<Coop_Model_RoomExploreBossDead>();
			result = coopRoom.OnRecvExploreBossDead(model9);
			break;
		}
		case PACKET_TYPE.ROOM_NOTIFY_ENCOUNTER_BOSS:
		{
			Coop_Model_RoomNotifyEncounterBoss model8 = packet.GetModel<Coop_Model_RoomNotifyEncounterBoss>();
			coopRoom.OnRecvNotifyEncounterBoss(packet.fromClientId, model8);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_SYNC_PLAYER_STATUS:
		{
			Coop_Model_RoomSyncPlayerStatus model7 = packet.GetModel<Coop_Model_RoomSyncPlayerStatus>();
			coopRoom.OnRecvSyncPlayerStatus(packet.fromClientId, model7);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_CHAT_STAMP:
		{
			Coop_Model_RoomChatStamp model6 = packet.GetModel<Coop_Model_RoomChatStamp>();
			coopRoom.OnRecvChatStamp(packet.fromClientId, model6);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_MOVE_FIELD:
		{
			Coop_Model_RoomMoveField model5 = packet.GetModel<Coop_Model_RoomMoveField>();
			coopRoom.OnRecvMoveField(model5);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_RUSH_REQUEST:
		{
			Coop_Model_RushRequest model4 = packet.GetModel<Coop_Model_RushRequest>();
			coopRoom.OnRecvRushRequest(packet.fromClientId, model4);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_RUSH_REQUESTED:
		{
			Coop_Model_RushRequested model3 = packet.GetModel<Coop_Model_RushRequested>();
			coopRoom.OnRecvRushRequested(model3);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_SYNC_DEFENSE_BATTLE:
		{
			Coop_Model_RoomSyncDefenseBattle model2 = packet.GetModel<Coop_Model_RoomSyncDefenseBattle>();
			coopRoom.OnRecvSyncDefenseBattle(model2);
			result = true;
			break;
		}
		case PACKET_TYPE.ROOM_NOTIFY_TRACE_BOSS:
		{
			Coop_Model_RoomNotifyTraceBoss model = packet.GetModel<Coop_Model_RoomNotifyTraceBoss>();
			coopRoom.OnRecvNotifyTraceBoss(packet.fromClientId, model);
			result = true;
			break;
		}
		}
		return result;
	}
}
