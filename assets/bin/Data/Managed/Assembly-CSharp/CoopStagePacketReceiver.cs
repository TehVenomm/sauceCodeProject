public class CoopStagePacketReceiver : PacketReceiver
{
	private CoopStage coopStage
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		coopStage = this.get_gameObject().GetComponent<CoopStage>();
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.STAGE_PLAYER_POP:
		{
			Coop_Model_StagePlayerPop model11 = packet.GetModel<Coop_Model_StagePlayerPop>();
			result = coopStage.OnRecvStagePlayerPop(model11, packet);
			break;
		}
		case PACKET_TYPE.STAGE_INFO:
		{
			Coop_Model_StageInfo model14 = packet.GetModel<Coop_Model_StageInfo>();
			result = coopStage.OnRecvStageInfo(model14, packet);
			break;
		}
		case PACKET_TYPE.STAGE_RESPONSE_END:
		{
			Coop_Model_StageResponseEnd model13 = packet.GetModel<Coop_Model_StageResponseEnd>();
			result = coopStage.OnRecvStageResponseEnd(model13, packet);
			break;
		}
		case PACKET_TYPE.STAGE_QUEST_CLOSE:
		{
			Coop_Model_StageQuestClose model12 = packet.GetModel<Coop_Model_StageQuestClose>();
			result = coopStage.OnRecvQuestClose(model12.is_succeed);
			break;
		}
		case PACKET_TYPE.STAGE_TIMEUP:
			result = coopStage.OnRecvStageTimeup();
			break;
		case PACKET_TYPE.STAGE_CHAT:
		{
			Coop_Model_StageChat model10 = packet.GetModel<Coop_Model_StageChat>();
			if (model10.r)
			{
				result = coopStage.OnRecvStageChat(model10);
			}
			break;
		}
		case PACKET_TYPE.CHAT_MESSAGE:
		{
			Coop_Model_StageChatMessage model9 = packet.GetModel<Coop_Model_StageChatMessage>();
			result = coopStage.OnRecvChatMessage(packet.fromClientId, model9);
			break;
		}
		case PACKET_TYPE.STAGE_CHAT_STAMP:
		{
			Coop_Model_StageChatStamp model8 = packet.GetModel<Coop_Model_StageChatStamp>();
			result = coopStage.OnRecvChatStamp(model8);
			break;
		}
		case PACKET_TYPE.STAGE_REQUEST_POP:
		{
			Coop_Model_StageRequestPop model7 = packet.GetModel<Coop_Model_StageRequestPop>();
			result = coopStage.OnRecvRequestPop(model7, packet);
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_PLAYER_RECORD:
		{
			Coop_Model_StageSyncPlayerRecord model6 = packet.GetModel<Coop_Model_StageSyncPlayerRecord>();
			coopStage.OnRecvSyncPlayerRecord(model6);
			result = true;
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_TIME_REQUEST:
		{
			Coop_Model_StageSyncTimeRequest model5 = packet.GetModel<Coop_Model_StageSyncTimeRequest>();
			coopStage.OnRecvSyncTimeRequest(model5, packet.fromClientId);
			result = true;
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_TIME:
		{
			Coop_Model_StageSyncTime model4 = packet.GetModel<Coop_Model_StageSyncTime>();
			coopStage.OnRecvSyncTime(model4);
			result = true;
			break;
		}
		case PACKET_TYPE.ENEMY_BOSS_ESCAPE:
		{
			Coop_Model_EnemyBossEscape model3 = packet.GetModel<Coop_Model_EnemyBossEscape>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossEscape(model3);
			break;
		}
		case PACKET_TYPE.ENEMY_BOSS_ALIVE_REQUEST:
			coopStage.OnRecvEnemyBossAliveRequest(packet);
			result = true;
			break;
		case PACKET_TYPE.ENEMY_BOSS_ALIVE_REQUESTED:
			coopStage.OnRecvEnemyBossAliveRequested();
			result = true;
			break;
		case PACKET_TYPE.STAGE_OBJECT_INFO:
		{
			Coop_Model_StageObjectInfo model2 = packet.GetModel<Coop_Model_StageObjectInfo>();
			result = coopStage.OnRecvStageObjectInfo(model2, packet);
			break;
		}
		case PACKET_TYPE.ACTIVE_SUPPLY:
		{
			Coop_Model_ActiveSupply model = packet.GetModel<Coop_Model_ActiveSupply>();
			coopStage.ActiveSupply(model.pointId);
			result = true;
			break;
		}
		}
		return result;
	}
}
