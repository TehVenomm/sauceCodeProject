// Decompiled with JetBrains decompiler
// Type: CoopClientPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CoopClientPacketReceiver : PacketReceiver
{
  private CoopClient coopClient { get; set; }

  protected virtual void Awake()
  {
    this.coopClient = ((Component) this).gameObject.GetComponent<CoopClient>();
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    bool flag = false;
    switch (packet.packetType)
    {
      case PACKET_TYPE.CLIENT_STATUS:
        flag = this.coopClient.OnRecvClientStatus(packet.GetModel<Coop_Model_ClientStatus>(), packet);
        break;
      case PACKET_TYPE.CLIENT_LOADING_PROGRESS:
        flag = this.coopClient.OnRecvClientLoadingProgress(packet.GetModel<Coop_Model_ClientLoadingProgress>());
        break;
      case PACKET_TYPE.CLIENT_CHANGE_EQUIP:
        flag = this.coopClient.OnRecvClientChangeEquip(packet.GetModel<Coop_Model_ClientChangeEquip>());
        break;
      case PACKET_TYPE.CLIENT_BATTLE_RETIRE:
        flag = this.coopClient.OnRecvClientBattleRetire(packet.GetModel<Coop_Model_ClientBattleRetire>());
        break;
      case PACKET_TYPE.CLIENT_SERIES_PROGRESS:
        flag = this.coopClient.OnRecvClientSeriesProgress(packet.GetModel<Coop_Model_ClientSeriesProgress>());
        break;
    }
    return flag;
  }
}
