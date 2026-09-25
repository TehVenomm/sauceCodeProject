// Decompiled with JetBrains decompiler
// Type: CoopRoomPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CoopRoomPacketReceiver : PacketReceiver
{
  private CoopRoom coopRoom { get; set; }

  protected virtual void Awake()
  {
    this.coopRoom = ((Component) this).gameObject.GetComponent<CoopRoom>();
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    bool flag = false;
    switch (packet.packetType)
    {
      case PACKET_TYPE.ROOM_UPDATE_PORTAL_POINT:
        this.coopRoom.OnRecvRoomUpdatePortalPoint(packet.GetModel<Coop_Model_RoomUpdatePortalPoint>());
        flag = true;
        break;
      case PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS:
        this.coopRoom.OnRecvSyncExploreBoss(packet.GetModel<Coop_Model_RoomSyncExploreBoss>());
        flag = true;
        break;
      case PACKET_TYPE.ROOM_SYNC_EXPLORE_BOSS_MAP:
        this.coopRoom.OnRecvSyncExploreBossMap(packet.GetModel<Coop_Model_RoomSyncExploreBossMap>());
        flag = true;
        break;
      case PACKET_TYPE.ROOM_EXPLORE_BOSS_DEAD:
        flag = this.coopRoom.OnRecvExploreBossDead(packet.GetModel<Coop_Model_RoomExploreBossDead>());
        break;
      case PACKET_TYPE.ROOM_NOTIFY_ENCOUNTER_BOSS:
        Coop_Model_RoomNotifyEncounterBoss model1 = packet.GetModel<Coop_Model_RoomNotifyEncounterBoss>();
        this.coopRoom.OnRecvNotifyEncounterBoss(packet.fromClientId, model1);
        flag = true;
        break;
      case PACKET_TYPE.ROOM_SYNC_PLAYER_STATUS:
        Coop_Model_RoomSyncPlayerStatus model2 = packet.GetModel<Coop_Model_RoomSyncPlayerStatus>();
        this.coopRoom.OnRecvSyncPlayerStatus(packet.fromClientId, model2);
        flag = true;
        break;
      case PACKET_TYPE.ROOM_CHAT_STAMP:
        Coop_Model_RoomChatStamp model3 = packet.GetModel<Coop_Model_RoomChatStamp>();
        this.coopRoom.OnRecvChatStamp(packet.fromClientId, model3);
        flag = true;
        break;
      case PACKET_TYPE.ROOM_EXPLORE_BOSS_DAMAGE:
        Coop_Model_RoomExploreBossDamage model4 = packet.GetModel<Coop_Model_RoomExploreBossDamage>();
        this.coopRoom.OnRecvExploreBossDamage(packet.fromClientId, model4);
        flag = true;
        break;
      case PACKET_TYPE.ROOM_EXPLORE_ALIVE:
        packet.GetModel<Coop_Model_RoomExploreAlive>();
        this.coopRoom.OnRecvExploreAlive();
        flag = true;
        break;
      case PACKET_TYPE.ROOM_EXPLORE_ALIVE_REQUEST:
        packet.GetModel<Coop_Model_RoomExploreAliveRequest>();
        this.coopRoom.OnRecvExploreAliveRequest();
        flag = true;
        break;
      case PACKET_TYPE.ROOM_SYNC_ALL_PORTAL_POINT:
        this.coopRoom.OnRecvSyncAllPortalPoint(packet.GetModel<Coop_Model_RoomSyncAllPortalPoint>());
        flag = true;
        break;
      case PACKET_TYPE.ROOM_MOVE_FIELD:
        this.coopRoom.OnRecvMoveField(packet.GetModel<Coop_Model_RoomMoveField>());
        flag = true;
        break;
      case PACKET_TYPE.ROOM_RUSH_REQUEST:
        Coop_Model_RushRequest model5 = packet.GetModel<Coop_Model_RushRequest>();
        this.coopRoom.OnRecvRushRequest(packet.fromClientId, model5);
        flag = true;
        break;
      case PACKET_TYPE.ROOM_RUSH_REQUESTED:
        this.coopRoom.OnRecvRushRequested(packet.GetModel<Coop_Model_RushRequested>());
        flag = true;
        break;
      case PACKET_TYPE.ROOM_NOTIFY_TRACE_BOSS:
        Coop_Model_RoomNotifyTraceBoss model6 = packet.GetModel<Coop_Model_RoomNotifyTraceBoss>();
        this.coopRoom.OnRecvNotifyTraceBoss(packet.fromClientId, model6);
        flag = true;
        break;
      case PACKET_TYPE.ROOM_SYNC_DEFENSE_BATTLE:
        this.coopRoom.OnRecvSyncDefenseBattle(packet.GetModel<Coop_Model_RoomSyncDefenseBattle>());
        flag = true;
        break;
    }
    return flag;
  }
}
