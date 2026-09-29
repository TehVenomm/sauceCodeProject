// Decompiled with JetBrains decompiler
// Type: ClanPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;

#nullable disable
public class ClanPacketReceiver : PacketReceiver
{
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
      if (this.HandleCoopEvent(packet))
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
      case PACKET_TYPE.CHAT_MESSAGE:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvChatMessage(packet.GetModel<Coop_Model_StageChatMessage>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_JOINED:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomJoined(packet.GetModel<Lounge_Model_RoomJoined>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_LEAVED:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomLeaved(packet.GetModel<Lounge_Model_RoomLeaved>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_KICK:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomKick(packet.GetModel<Lounge_Model_RoomKick>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_MOVE:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomMove(packet.GetModel<Lounge_Model_RoomMove>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_POSITION:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomPoisition(packet.GetModel<Lounge_Model_RoomPosition>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_ACTION:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomAction(packet.GetModel<Lounge_Model_RoomAction>());
        break;
      case PACKET_TYPE.LOUNGE_ROOM_AFK_KICK:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvRoomAFKKick(packet.GetModel<Lounge_Model_AFK_Kick>());
        break;
      case PACKET_TYPE.LOUNGE_MEMBER_LOUNGE:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberLounge(packet.GetModel<Lounge_Model_MemberLounge>());
        break;
      case PACKET_TYPE.LOUNGE_MEMBER_FIELD:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberField(packet.GetModel<Lounge_Model_MemberField>());
        break;
      case PACKET_TYPE.LOUNGE_MEMBER_QUEST:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberQuest(packet.GetModel<Lounge_Model_MemberQuest>());
        break;
      case PACKET_TYPE.LOUNGE_MEMBER_ARENA:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvMemberArena(packet.GetModel<Lounge_Model_MemberArena>());
        break;
      case PACKET_TYPE.STAGE_CHAT_STAMP:
        flag = MonoBehaviourSingleton<ClanNetworkManager>.I.OnRecvChatStamp(packet.GetModel<Coop_Model_StageChatStamp>());
        break;
    }
    return flag;
  }
}
