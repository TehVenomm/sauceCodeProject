using rhyme;
using System.Collections.Generic;
using UnityEngine;

public class CoopNetworkPacketReceiver : PacketReceiver
{
	public void EraseLostReceiverPackets()
	{
		int i = 0;
		for (int count = base.packets.Count; i < count; i++)
		{
			CoopPacket coopPacket = base.packets[i];
			if (coopPacket.destObjectId != 1000 && !((Object)MonoBehaviourSingleton<CoopManager>.I.GetPacketReceiver(coopPacket) != (Object)null))
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
				CoopPacket coopPacket = obj[j];
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
		case PACKET_TYPE.ROOM_JOINED:
		{
			Coop_Model_RoomJoined model6 = packet.GetModel<Coop_Model_RoomJoined>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomJoined(model6);
			break;
		}
		case PACKET_TYPE.ROOM_LEAVED:
		{
			Coop_Model_RoomLeaved model15 = packet.GetModel<Coop_Model_RoomLeaved>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomLeaved(model15);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_CHANGED:
		{
			Coop_Model_RoomStageChanged model14 = packet.GetModel<Coop_Model_RoomStageChanged>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageChanged(model14);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_REQUESTED:
		{
			Coop_Model_RoomStageRequested model13 = packet.GetModel<Coop_Model_RoomStageRequested>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageRequested(model13);
			break;
		}
		case PACKET_TYPE.ROOM_STAGE_HOST_CHANGED:
		{
			Coop_Model_RoomStageHostChanged model12 = packet.GetModel<Coop_Model_RoomStageHostChanged>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageHostChanged(model12);
			break;
		}
		case PACKET_TYPE.ENEMY_POP:
		{
			Coop_Model_EnemyPop model11 = packet.GetModel<Coop_Model_EnemyPop>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyPop(model11);
			break;
		}
		case PACKET_TYPE.ENEMY_DEFEAT:
		{
			Coop_Model_EnemyDefeat model10 = packet.GetModel<Coop_Model_EnemyDefeat>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyDefeat(model10);
			break;
		}
		case PACKET_TYPE.REWARD_PICKUP:
		{
			Coop_Model_RewardPickup model9 = packet.GetModel<Coop_Model_RewardPickup>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvRewardPickup(model9);
			break;
		}
		case PACKET_TYPE.ENEMY_EXTERMINATION:
		{
			Coop_Model_EnemyExtermination model8 = packet.GetModel<Coop_Model_EnemyExtermination>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyExtermination(model8);
			break;
		}
		case PACKET_TYPE.EVENT_HAPPEN_QUEST:
		{
			Coop_Model_EventHappenQuest model7 = packet.GetModel<Coop_Model_EventHappenQuest>();
			result = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEventHappenQuest(model7);
			break;
		}
		case PACKET_TYPE.UPDATE_BOOST_COMPLETE:
		{
			Coop_Model_UpdateBoostComplete model5 = packet.GetModel<Coop_Model_UpdateBoostComplete>();
			result = true;
			if (!model5.success)
			{
				MonoBehaviourSingleton<KtbWebSocket>.I.Close(1000, "Bye!");
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
