// Decompiled with JetBrains decompiler
// Type: CoopStagePacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CoopStagePacketReceiver : PacketReceiver
{
  private CoopStage coopStage { get; set; }

  protected virtual void Awake()
  {
    this.coopStage = ((Component) this).gameObject.GetComponent<CoopStage>();
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    bool flag = false;
    switch (packet.packetType)
    {
      case PACKET_TYPE.CHAT_MESSAGE:
        Coop_Model_StageChatMessage model1 = packet.GetModel<Coop_Model_StageChatMessage>();
        flag = this.coopStage.OnRecvChatMessage(packet.fromClientId, model1);
        break;
      case PACKET_TYPE.STAGE_PLAYER_POP:
        flag = this.coopStage.OnRecvStagePlayerPop(packet.GetModel<Coop_Model_StagePlayerPop>(), packet);
        break;
      case PACKET_TYPE.STAGE_INFO:
        flag = this.coopStage.OnRecvStageInfo(packet.GetModel<Coop_Model_StageInfo>(), packet);
        break;
      case PACKET_TYPE.STAGE_RESPONSE_END:
        flag = this.coopStage.OnRecvStageResponseEnd(packet.GetModel<Coop_Model_StageResponseEnd>(), packet);
        break;
      case PACKET_TYPE.STAGE_QUEST_CLOSE:
        flag = this.coopStage.OnRecvQuestClose(packet.GetModel<Coop_Model_StageQuestClose>().is_succeed);
        break;
      case PACKET_TYPE.STAGE_TIMEUP:
        flag = this.coopStage.OnRecvStageTimeup();
        break;
      case PACKET_TYPE.STAGE_CHAT:
        Coop_Model_StageChat model2 = packet.GetModel<Coop_Model_StageChat>();
        if (model2.r)
        {
          flag = this.coopStage.OnRecvStageChat(model2);
          break;
        }
        break;
      case PACKET_TYPE.STAGE_CHAT_STAMP:
        flag = this.coopStage.OnRecvChatStamp(packet.GetModel<Coop_Model_StageChatStamp>());
        break;
      case PACKET_TYPE.STAGE_SYNC_TIME_REQUEST:
        this.coopStage.OnRecvSyncTimeRequest(packet.GetModel<Coop_Model_StageSyncTimeRequest>(), packet.fromClientId);
        flag = true;
        break;
      case PACKET_TYPE.STAGE_SYNC_TIME:
        this.coopStage.OnRecvSyncTime(packet.GetModel<Coop_Model_StageSyncTime>());
        flag = true;
        break;
      case PACKET_TYPE.STAGE_REQUEST_POP:
        flag = this.coopStage.OnRecvRequestPop(packet.GetModel<Coop_Model_StageRequestPop>(), packet);
        break;
      case PACKET_TYPE.STAGE_SYNC_PLAYER_RECORD:
        this.coopStage.OnRecvSyncPlayerRecord(packet.GetModel<Coop_Model_StageSyncPlayerRecord>());
        flag = true;
        break;
      case PACKET_TYPE.ENEMY_BOSS_ESCAPE:
        flag = MonoBehaviourSingleton<CoopManager>.I.coopStage.OnRecvEnemyBossEscape(packet.GetModel<Coop_Model_EnemyBossEscape>());
        break;
      case PACKET_TYPE.ENEMY_BOSS_ALIVE_REQUEST:
        this.coopStage.OnRecvEnemyBossAliveRequest(packet);
        flag = true;
        break;
      case PACKET_TYPE.ENEMY_BOSS_ALIVE_REQUESTED:
        this.coopStage.OnRecvEnemyBossAliveRequested();
        flag = true;
        break;
      case PACKET_TYPE.STAGE_OBJECT_INFO:
        flag = this.coopStage.OnRecvStageObjectInfo(packet.GetModel<Coop_Model_StageObjectInfo>(), packet);
        break;
      case PACKET_TYPE.ACTIVE_SUPPLY:
        this.coopStage.ActiveSupply(packet.GetModel<Coop_Model_ActiveSupply>().pointId);
        flag = true;
        break;
    }
    return flag;
  }
}
