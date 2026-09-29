// Decompiled with JetBrains decompiler
// Type: CoopNetworkPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopNetworkPacketReceiver : PacketReceiver
{
  public void EraseLostReceiverPackets()
  {
    int index = 0;
    for (int count = this.packets.Count; index < count; ++index)
    {
      CoopPacket packet = this.packets[index];
      if (packet.destObjectId != 1000 && !Object.op_Inequality((Object) MonoBehaviourSingleton<CoopManager>.I.GetPacketReceiver(packet), (Object) null))
        this.AddDeleteQueue(packet);
    }
    this.EraseUsedPacket();
  }

  protected override void PacketUpdate()
  {
    if (this.stopPacketUpdate)
      return;
    List<CoopPacket> coopPacketList = rymTPool<List<CoopPacket>>.Get();
    if (coopPacketList.Capacity < this.packets.Count)
      coopPacketList.Capacity = this.packets.Count;
    int index1 = 0;
    for (int count = this.packets.Count; index1 < count; ++index1)
      coopPacketList.Add(this.packets[index1]);
    int index2 = 0;
    for (int count = coopPacketList.Count; index2 < count && !this.stopPacketUpdate; ++index2)
    {
      CoopPacket packet = coopPacketList[index2];
      if (packet.destObjectId == 1000)
      {
        if (this.HandleCoopEvent(packet))
          this.AddDeleteQueue(packet);
      }
      else if (MonoBehaviourSingleton<CoopManager>.I.PacketRelay(packet))
        this.AddDeleteQueue(packet);
    }
    coopPacketList.Clear();
    rymTPool<List<CoopPacket>>.Release(ref coopPacketList);
    this.EraseUsedPacket();
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    bool flag = false;
    switch (packet.packetType)
    {
      case PACKET_TYPE.ROOM_JOINED:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomJoined(packet.GetModel<Coop_Model_RoomJoined>());
        break;
      case PACKET_TYPE.ROOM_LEAVED:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomLeaved(packet.GetModel<Coop_Model_RoomLeaved>());
        break;
      case PACKET_TYPE.ROOM_STAGE_CHANGED:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageChanged(packet.GetModel<Coop_Model_RoomStageChanged>());
        break;
      case PACKET_TYPE.ROOM_STAGE_REQUESTED:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageRequested(packet.GetModel<Coop_Model_RoomStageRequested>());
        break;
      case PACKET_TYPE.ROOM_STAGE_HOST_CHANGED:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomStageHostChanged(packet.GetModel<Coop_Model_RoomStageHostChanged>());
        break;
      case PACKET_TYPE.ENEMY_POP:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyPop(packet.GetModel<Coop_Model_EnemyPop>());
        break;
      case PACKET_TYPE.ENEMY_DEFEAT:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyDefeat(packet.GetModel<Coop_Model_EnemyDefeat>());
        break;
      case PACKET_TYPE.REWARD_PICKUP:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvRewardPickup(packet.GetModel<Coop_Model_RewardPickup>());
        break;
      case PACKET_TYPE.ENEMY_EXTERMINATION:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyExtermination(packet.GetModel<Coop_Model_EnemyExtermination>());
        break;
      case PACKET_TYPE.UPDATE_BOOST_COMPLETE:
        Coop_Model_UpdateBoostComplete model = packet.GetModel<Coop_Model_UpdateBoostComplete>();
        flag = true;
        if (!model.success)
        {
          MonoBehaviourSingleton<KtbWebSocket>.I.Close();
          break;
        }
        break;
      case PACKET_TYPE.ROOM_TIME_UPDATE:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopRoom.OnRecvRoomTimeUpdate(packet.GetModel<Coop_Model_RoomTimeUpdate>());
        break;
      case PACKET_TYPE.ENEMY_BOSS_POP:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossPop(packet.GetModel<Coop_Model_EnemyBossPop>());
        break;
      case PACKET_TYPE.WAVEMATCH_INFO:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvWaveMatchInfo(packet.GetModel<Coop_Model_WaveMatchInfo>());
        break;
      case PACKET_TYPE.WAVEMATCH_DROP:
        flag = MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDrop(packet.GetModel<Coop_Model_WaveMatchDrop>());
        break;
      case PACKET_TYPE.EVENT_HAPPEN_QUEST:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEventHappenQuest(packet.GetModel<Coop_Model_EventHappenQuest>());
        break;
      case PACKET_TYPE.EVENT_HAPPEN_QUEST_STATUS:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEventHappenQuestStatus(packet.GetModel<Coop_Model_EventHappenQuestStatus>());
        break;
    }
    return flag;
  }
}
