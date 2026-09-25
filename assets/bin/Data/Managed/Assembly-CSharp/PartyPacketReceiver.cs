// Decompiled with JetBrains decompiler
// Type: PartyPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System.Collections.Generic;

#nullable disable
public class PartyPacketReceiver : PacketReceiver
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
        flag = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvChatMessage(packet.GetModel<Coop_Model_StageChatMessage>());
        break;
      case PACKET_TYPE.PARTY_ROOM_JOINED:
        flag = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvRoomJoined(packet.GetModel<Party_Model_RoomJoined>());
        break;
      case PACKET_TYPE.PARTY_ROOM_LEAVED:
        flag = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvRoomLeaved(packet.GetModel<Party_Model_RoomLeaved>());
        break;
      case PACKET_TYPE.STAGE_CHAT_STAMP:
        flag = MonoBehaviourSingleton<PartyNetworkManager>.I.OnRecvChatStamp(packet.GetModel<Coop_Model_StageChatStamp>());
        break;
    }
    return flag;
  }
}
