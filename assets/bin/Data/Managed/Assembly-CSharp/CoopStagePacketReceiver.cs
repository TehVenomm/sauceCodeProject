public class CoopStagePacketReceiver : PacketReceiver
{
	private CoopStage coopStage
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		coopStage = base.gameObject.GetComponent<CoopStage>();
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.STAGE_PLAYER_POP:
		{
			Coop_Model_StagePlayerPop model10 = packet.GetModel<Coop_Model_StagePlayerPop>();
			result = coopStage.OnRecvStagePlayerPop(model10, packet);
			break;
		}
		case PACKET_TYPE.STAGE_INFO:
		{
			Coop_Model_StageInfo model13 = packet.GetModel<Coop_Model_StageInfo>();
			result = coopStage.OnRecvStageInfo(model13, packet);
			break;
		}
		case PACKET_TYPE.STAGE_RESPONSE_END:
		{
			Coop_Model_StageResponseEnd model12 = packet.GetModel<Coop_Model_StageResponseEnd>();
			result = coopStage.OnRecvStageResponseEnd(model12, packet);
			break;
		}
		case PACKET_TYPE.STAGE_QUEST_CLOSE:
		{
			Coop_Model_StageQuestClose model11 = packet.GetModel<Coop_Model_StageQuestClose>();
			result = coopStage.OnRecvQuestClose(model11.is_succeed);
			break;
		}
		case PACKET_TYPE.STAGE_TIMEUP:
			result = coopStage.OnRecvStageTimeup();
			break;
		case PACKET_TYPE.STAGE_CHAT:
		{
			Coop_Model_StageChat model9 = packet.GetModel<Coop_Model_StageChat>();
			if (model9.r)
			{
				result = coopStage.OnRecvStageChat(model9);
			}
			break;
		}
		case PACKET_TYPE.CHAT_MESSAGE:
		{
			Coop_Model_StageChatMessage model8 = packet.GetModel<Coop_Model_StageChatMessage>();
			result = coopStage.OnRecvChatMessage(packet.fromClientId, model8);
			break;
		}
		case PACKET_TYPE.STAGE_CHAT_STAMP:
		{
			Coop_Model_StageChatStamp model7 = packet.GetModel<Coop_Model_StageChatStamp>();
			result = coopStage.OnRecvChatStamp(model7);
			break;
		}
		case PACKET_TYPE.STAGE_REQUEST_POP:
		{
			Coop_Model_StageRequestPop model6 = packet.GetModel<Coop_Model_StageRequestPop>();
			result = coopStage.OnRecvRequestPop(model6, packet);
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_PLAYER_RECORD:
		{
			Coop_Model_StageSyncPlayerRecord model5 = packet.GetModel<Coop_Model_StageSyncPlayerRecord>();
			coopStage.OnRecvSyncPlayerRecord(model5);
			result = true;
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_TIME_REQUEST:
		{
			Coop_Model_StageSyncTimeRequest model4 = packet.GetModel<Coop_Model_StageSyncTimeRequest>();
			coopStage.OnRecvSyncTimeRequest(model4, packet.fromClientId);
			result = true;
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_TIME:
		{
			Coop_Model_StageSyncTime model3 = packet.GetModel<Coop_Model_StageSyncTime>();
			coopStage.OnRecvSyncTime(model3);
			result = true;
			break;
		}
		case PACKET_TYPE.ENEMY_BOSS_ESCAPE:
		{
			Coop_Model_EnemyBossEscape model2 = packet.GetModel<Coop_Model_EnemyBossEscape>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossEscape(model2);
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
			Coop_Model_StageObjectInfo model = packet.GetModel<Coop_Model_StageObjectInfo>();
			result = coopStage.OnRecvStageObjectInfo(model, packet);
			break;
		}
		}
		return result;
	}
}
