public class CoopStagePacketReceiver : PacketReceiver
{
	private CoopStage coopStage
	{
		get;
		set;
	}

	protected virtual void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		coopStage = this.get_gameObject().GetComponent<CoopStage>();
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.STAGE_PLAYER_POP:
		{
			Coop_Model_StagePlayerPop model7 = packet.GetModel<Coop_Model_StagePlayerPop>();
			result = coopStage.OnRecvStagePlayerPop(model7, packet);
			break;
		}
		case PACKET_TYPE.STAGE_INFO:
		{
			Coop_Model_StageInfo model10 = packet.GetModel<Coop_Model_StageInfo>();
			result = coopStage.OnRecvStageInfo(model10, packet);
			break;
		}
		case PACKET_TYPE.STAGE_RESPONSE_END:
		{
			Coop_Model_StageResponseEnd model9 = packet.GetModel<Coop_Model_StageResponseEnd>();
			result = coopStage.OnRecvStageResponseEnd(model9, packet);
			break;
		}
		case PACKET_TYPE.STAGE_QUEST_CLOSE:
		{
			Coop_Model_StageQuestClose model8 = packet.GetModel<Coop_Model_StageQuestClose>();
			result = coopStage.OnRecvQuestClose(model8.is_succeed);
			break;
		}
		case PACKET_TYPE.STAGE_TIMEUP:
			result = coopStage.OnRecvStageTimeup();
			break;
		case PACKET_TYPE.STAGE_CHAT:
		{
			Coop_Model_StageChat model6 = packet.GetModel<Coop_Model_StageChat>();
			if (model6.r)
			{
				result = coopStage.OnRecvStageChat(model6);
			}
			break;
		}
		case PACKET_TYPE.CHAT_MESSAGE:
		{
			Coop_Model_StageChatMessage model5 = packet.GetModel<Coop_Model_StageChatMessage>();
			result = coopStage.OnRecvChatMessage(packet.fromClientId, model5);
			break;
		}
		case PACKET_TYPE.STAGE_CHAT_STAMP:
		{
			Coop_Model_StageChatStamp model4 = packet.GetModel<Coop_Model_StageChatStamp>();
			result = coopStage.OnRecvChatStamp(model4);
			break;
		}
		case PACKET_TYPE.STAGE_REQUEST_POP:
		{
			Coop_Model_StageRequestPop model3 = packet.GetModel<Coop_Model_StageRequestPop>();
			result = coopStage.OnRecvRequestPop(model3, packet);
			break;
		}
		case PACKET_TYPE.STAGE_SYNC_PLAYER_RECORD:
		{
			Coop_Model_StageSyncPlayerRecord model2 = packet.GetModel<Coop_Model_StageSyncPlayerRecord>();
			coopStage.OnRecvSyncPlayerRecord(model2);
			result = true;
			break;
		}
		case PACKET_TYPE.ENEMY_BOSS_ESCAPE:
		{
			Coop_Model_EnemyBossEscape model = packet.GetModel<Coop_Model_EnemyBossEscape>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossEscape(model);
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
		}
		return result;
	}
}
