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
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		bool result = false;
		switch (packet.packetType)
		{
		case PACKET_TYPE.ROOM_JOINED:
		{
			Coop_Model_RoomJoined model5 = packet.GetModel<Coop_Model_RoomJoined>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomJoined(model5);
			break;
		}
		case PACKET_TYPE.ROOM_LEAVED:
		{
			Coop_Model_RoomLeaved model14 = packet.GetModel<Coop_Model_RoomLeaved>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomLeaved(model14);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_CHANGED:
		{
			Coop_Model_RoomStageChanged model13 = packet.GetModel<Coop_Model_RoomStageChanged>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageChanged(model13);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_REQUESTED:
		{
			Coop_Model_RoomStageRequested model12 = packet.GetModel<Coop_Model_RoomStageRequested>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageRequested(model12);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_HOST_CHANGED:
		{
			Coop_Model_RoomStageHostChanged model11 = packet.GetModel<Coop_Model_RoomStageHostChanged>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageHostChanged(model11);
			break;
		}
		case PACKET_TYPE.ENEMY_POP:
		{
			Coop_Model_EnemyPop model10 = packet.GetModel<Coop_Model_EnemyPop>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyPop(model10);
			break;
		}
		case PACKET_TYPE.ENEMY_DEFEAT:
		{
			Coop_Model_EnemyDefeat model9 = packet.GetModel<Coop_Model_EnemyDefeat>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyDefeat(model9);
			break;
		}
		case PACKET_TYPE.REWARD_PICKUP:
		{
			Coop_Model_RewardPickup model8 = packet.GetModel<Coop_Model_RewardPickup>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvRewardPickup(model8);
			break;
		}
		case PACKET_TYPE.ENEMY_EXTERMINATION:
		{
			Coop_Model_EnemyExtermination model7 = packet.GetModel<Coop_Model_EnemyExtermination>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyExtermination(model7);
			break;
		}
		case PACKET_TYPE.EVENT_HAPPEN_QUEST:
		{
			Coop_Model_EventHappenQuest model6 = packet.GetModel<Coop_Model_EventHappenQuest>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEventHappenQuest(model6);
			break;
		}
		case PACKET_TYPE.UPDATE_BOOST_COMPLETE:
		{
			Coop_Model_UpdateBoostComplete model4 = packet.GetModel<Coop_Model_UpdateBoostComplete>();
			result = true;
			if (!model4.success)
			{
				MonoBehaviourSingleton<KtbWebSocket>.I.Close(1000, "Bye!");
			}
			break;
		}
		case PACKET_TYPE.ROOM_TIME_UPDATE:
		{
			Coop_Model_RoomTimeUpdate model3 = packet.GetModel<Coop_Model_RoomTimeUpdate>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomTimeUpdate(model3);
			break;
		}
		case PACKET_TYPE.ENEMY_BOSS_POP:
		{
			Coop_Model_EnemyBossPop model2 = packet.GetModel<Coop_Model_EnemyBossPop>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossPop(model2);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_INFO:
		{
			Coop_Model_WaveMatchInfo model = packet.GetModel<Coop_Model_WaveMatchInfo>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvWaveMatchInfo(model);
			break;
		}
		}
		return result;
	}
}
