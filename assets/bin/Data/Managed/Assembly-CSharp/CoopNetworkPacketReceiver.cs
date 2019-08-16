using rhyme;
using System.Collections.Generic;

public class CoopNetworkPacketReceiver : PacketReceiver
{
	public void EraseLostReceiverPackets()
	{
		int i = 0;
		for (int count = base.packets.Count; i < count; i++)
		{
			CoopPacket coopPacket = base.packets[i];
			if (coopPacket.destObjectId != 1000 && !(MonoBehaviourSingleton<CoopManager>.I.GetPacketReceiver(coopPacket) != null))
			{
				AddDeleteQueue(coopPacket);
			}
		}
		EraseUsedPacket();
	}

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
			CoopPacket coopPacket = list[j];
			if (coopPacket.destObjectId == 1000)
			{
				if (HandleCoopEvent(coopPacket))
				{
					AddDeleteQueue(coopPacket);
				}
			}
			else if (MonoBehaviourSingleton<CoopManager>.I.PacketRelay(coopPacket))
			{
				AddDeleteQueue(coopPacket);
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
		case PACKET_TYPE.ROOM_JOINED:
		{
			Coop_Model_RoomJoined model6 = packet.GetModel<Coop_Model_RoomJoined>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomJoined(model6);
			break;
		}
		case PACKET_TYPE.ROOM_LEAVED:
		{
			Coop_Model_RoomLeaved model16 = packet.GetModel<Coop_Model_RoomLeaved>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomLeaved(model16);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_CHANGED:
		{
			Coop_Model_RoomStageChanged model15 = packet.GetModel<Coop_Model_RoomStageChanged>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageChanged(model15);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_REQUESTED:
		{
			Coop_Model_RoomStageRequested model14 = packet.GetModel<Coop_Model_RoomStageRequested>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageRequested(model14);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_HOST_CHANGED:
		{
			Coop_Model_RoomStageHostChanged model13 = packet.GetModel<Coop_Model_RoomStageHostChanged>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageHostChanged(model13);
			break;
		}
		case PACKET_TYPE.ENEMY_POP:
		{
			Coop_Model_EnemyPop model12 = packet.GetModel<Coop_Model_EnemyPop>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyPop(model12);
			break;
		}
		case PACKET_TYPE.ENEMY_DEFEAT:
		{
			Coop_Model_EnemyDefeat model11 = packet.GetModel<Coop_Model_EnemyDefeat>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyDefeat(model11);
			break;
		}
		case PACKET_TYPE.REWARD_PICKUP:
		{
			Coop_Model_RewardPickup model10 = packet.GetModel<Coop_Model_RewardPickup>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvRewardPickup(model10);
			break;
		}
		case PACKET_TYPE.ENEMY_EXTERMINATION:
		{
			Coop_Model_EnemyExtermination model9 = packet.GetModel<Coop_Model_EnemyExtermination>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyExtermination(model9);
			break;
		}
		case PACKET_TYPE.EVENT_HAPPEN_QUEST:
		{
			Coop_Model_EventHappenQuest model8 = packet.GetModel<Coop_Model_EventHappenQuest>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEventHappenQuest(model8);
			break;
		}
		case PACKET_TYPE.EVENT_HAPPEN_QUEST_STATUS:
		{
			Coop_Model_EventHappenQuestStatus model7 = packet.GetModel<Coop_Model_EventHappenQuestStatus>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEventHappenQuestStatus(model7);
			break;
		}
		case PACKET_TYPE.UPDATE_BOOST_COMPLETE:
		{
			Coop_Model_UpdateBoostComplete model5 = packet.GetModel<Coop_Model_UpdateBoostComplete>();
			result = true;
			if (!model5.success)
			{
				MonoBehaviourSingleton<KtbWebSocket>.I.Close(1000);
			}
			break;
		}
		case PACKET_TYPE.ROOM_TIME_UPDATE:
		{
			Coop_Model_RoomTimeUpdate model4 = packet.GetModel<Coop_Model_RoomTimeUpdate>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomTimeUpdate(model4);
			break;
		}
		case PACKET_TYPE.ENEMY_BOSS_POP:
		{
			Coop_Model_EnemyBossPop model3 = packet.GetModel<Coop_Model_EnemyBossPop>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossPop(model3);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_INFO:
		{
			Coop_Model_WaveMatchInfo model2 = packet.GetModel<Coop_Model_WaveMatchInfo>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvWaveMatchInfo(model2);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP:
		{
			Coop_Model_WaveMatchDrop model = packet.GetModel<Coop_Model_WaveMatchDrop>();
			result = MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDrop(model);
			break;
		}
		}
		return result;
	}
}
