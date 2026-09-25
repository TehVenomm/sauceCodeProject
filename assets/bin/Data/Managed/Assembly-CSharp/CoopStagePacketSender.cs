// Decompiled with JetBrains decompiler
// Type: CoopStagePacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CoopStagePacketSender : MonoBehaviour
{
  private CoopStage coopStage { get; set; }

  protected virtual void Awake()
  {
    this.coopStage = ((Component) this).gameObject.GetComponent<CoopStage>();
  }

  protected virtual void Start()
  {
  }

  private int Send<T>(
    T model,
    bool promise = true,
    int to_client_id = 0,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    return to_client_id == 0 ? MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcastInStage<T>(model, promise, onReceiveAck, onPreResend) : MonoBehaviourSingleton<CoopNetworkManager>.I.SendToInStage<T>(to_client_id, model, promise, onReceiveAck, onPreResend);
  }

  public void SendStageRequest(int to_client_id = 0)
  {
    Coop_Model_StageRequest model = new Coop_Model_StageRequest();
    model.id = 1002;
    model.series_index = 0;
    if (QuestManager.IsValidInGame())
      model.series_index = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
    this.Send<Coop_Model_StageRequest>(model, to_client_id: to_client_id);
  }

  public void SendStagePlayerPop(Player player, int to_client_id = 0)
  {
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    if (player.IsCoopNone())
      player.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
    Coop_Model_StagePlayerPop model = new Coop_Model_StagePlayerPop();
    model.id = 1002;
    model.sid = player.id;
    model.isSelf = player is Self;
    NonPlayer nonPlayer = player as NonPlayer;
    if (player.createInfo != null)
    {
      if (Object.op_Equality((Object) nonPlayer, (Object) null))
        model.charaInfo = player.createInfo.charaInfo;
      model.extentionInfo = player.createInfo.extentionInfo;
    }
    model.transferInfo = player.CreateTransferInfo();
    this.Send<Coop_Model_StagePlayerPop>(model, to_client_id: to_client_id);
  }

  public void SendStageInfo(int to_client_id = 0)
  {
    Coop_Model_StageInfo model = new Coop_Model_StageInfo();
    model.id = 1002;
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      model.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
    if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle())
    {
      model.isInFieldEnemyBossBattle = true;
      model.isInFieldFishingEnemyBattle = MonoBehaviourSingleton<CoopManager>.I.coopStage.GetisInFieldFishingEnemyBattle();
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      MonoBehaviourSingleton<StageObjectManager>.I.gimmickList.ForEach((Action<StageObject>) (o =>
      {
        if (o.IsCoopNone())
          o.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
        model.gimmicks.Add(new Coop_Model_StageInfo.GimmickInfo()
        {
          id = o.id,
          enable = ((Component) o).gameObject.activeSelf
        });
      }));
      MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmicksObjs(InGameProgress.eFieldGimmick.CarriableGimmick).ForEach((Action<IFieldGimmickObject>) (gimmick =>
      {
        FieldCarriableGimmickObject carriableGimmickObject = gimmick as FieldCarriableGimmickObject;
        if (Object.op_Equality((Object) carriableGimmickObject, (Object) null))
          return;
        model.carriableGimmickInfos.Add(carriableGimmickObject.GetCarriableGimmickInfo());
      }));
      MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmicksObjs(InGameProgress.eFieldGimmick.SupplyGimmick).ForEach((Action<IFieldGimmickObject>) (gimmick =>
      {
        FieldSupplyGimmickObject supplyGimmickObject = gimmick as FieldSupplyGimmickObject;
        if (Object.op_Equality((Object) supplyGimmickObject, (Object) null))
          return;
        model.supplyGimmickInfos.Add(supplyGimmickObject.GetSupplyGimmickInfo());
      }));
      model.enemyPos = Vector3.zero;
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
        model.enemyPos = MonoBehaviourSingleton<StageObjectManager>.I.boss._transform.position;
    }
    if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<InGameProgress>.IsValid())
      model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
    if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.HasFieldEnemyBossLimitTime())
      model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
    if (QuestManager.IsValidInGameWaveMatch())
    {
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
        model.rushLimitTime = MonoBehaviourSingleton<InGameProgress>.I.limitTime;
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        int index = 0;
        for (int count = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; index < count; ++index)
        {
          FieldWaveTargetObject waveTarget = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[index] as FieldWaveTargetObject;
          if (!Object.op_Equality((Object) waveTarget, (Object) null))
          {
            model.waveTargets.Add(new Coop_Model_StageInfo.WaveTargetInfo()
            {
              id = waveTarget.id,
              hp = waveTarget.nowHp
            });
            waveTarget.SetOwner(true);
          }
        }
      }
    }
    if (QuestManager.IsValidInGameWaveStrategy())
      this.coopStage.SetFirstWaveMatchInfoForStageInfo(ref model);
    this.Send<Coop_Model_StageInfo>(model, to_client_id: to_client_id, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
    {
      Coop_Model_StageInfo coopModelStageInfo = send_model as Coop_Model_StageInfo;
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
        coopModelStageInfo.elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime();
      return true;
    }));
  }

  public void SendObjectInfo(
    StageObject _stgObj,
    StageObject.COOP_MODE_TYPE _type,
    int _to_client_id = 0)
  {
    if (Object.op_Equality((Object) _stgObj, (Object) null))
      return;
    this.Send<Coop_Model_StageObjectInfo>(new Coop_Model_StageObjectInfo()
    {
      StageObjectID = _stgObj.id,
      CoopModeType = _type
    }, to_client_id: _to_client_id);
  }

  public void SendStageResponseEnd(CoopStage.STAGE_REQUEST_ERROR error_id = CoopStage.STAGE_REQUEST_ERROR.NONE, int to_client_id = 0)
  {
    Coop_Model_StageResponseEnd model = new Coop_Model_StageResponseEnd();
    model.id = 1002;
    model.error_id = (int) error_id;
    this.Send<Coop_Model_StageResponseEnd>(model, to_client_id: to_client_id);
  }

  public void SendStageQuestClose(bool is_succeed)
  {
    Coop_Model_StageQuestClose model = new Coop_Model_StageQuestClose();
    model.id = 1002;
    model.is_succeed = is_succeed;
    this.Send<Coop_Model_StageQuestClose>(model);
  }

  public void SendStageTimeup()
  {
    Coop_Model_StageTimeup model = new Coop_Model_StageTimeup();
    model.id = 1002;
    this.Send<Coop_Model_StageTimeup>(model);
  }

  public void SendStageSyncTimeRequest()
  {
    Coop_Model_StageSyncTimeRequest model = new Coop_Model_StageSyncTimeRequest();
    model.id = 1002;
    this.Send<Coop_Model_StageSyncTimeRequest>(model, false);
  }

  public void SendStageSyncTime(float elapsedTime, int toClientId)
  {
    Coop_Model_StageSyncTime model = new Coop_Model_StageSyncTime();
    model.id = 1002;
    model.elapsedTime = elapsedTime;
    this.Send<Coop_Model_StageSyncTime>(model, false, toClientId);
  }

  public void SendStageChat(int chara_id, int chat_id)
  {
    Coop_Model_StageChat model = new Coop_Model_StageChat();
    model.id = 1002;
    model.chara_id = chara_id;
    model.chat_id = chat_id;
    this.Send<Coop_Model_StageChat>(model, false);
  }

  public void SendChatMessage(int chara_id, string message)
  {
    Coop_Model_StageChatMessage model = new Coop_Model_StageChatMessage();
    model.id = 1002;
    model.chara_id = chara_id;
    model.text = message;
    model.user_id = 0;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
      model.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.Send<Coop_Model_StageChatMessage>(model, false);
  }

  public void SendChatStamp(int chara_id, int stamp_id)
  {
    Coop_Model_StageChatStamp model = new Coop_Model_StageChatStamp();
    model.id = 1002;
    model.chara_id = chara_id;
    model.stamp_id = stamp_id;
    model.user_id = 0;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
      model.user_id = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.Send<Coop_Model_StageChatStamp>(model, false);
  }

  public void SendRequestPop(int to_client_id, bool is_player, bool is_self, bool promise = false)
  {
    Coop_Model_StageRequestPop model = new Coop_Model_StageRequestPop();
    model.id = 1002;
    model.isPlayer = is_player;
    model.isSelf = is_self;
    this.Send<Coop_Model_StageRequestPop>(model, promise, to_client_id);
  }

  public void SendSyncPlayerRecord(
    InGameRecorder.PlayerRecordSyncHost record,
    int to_client_id,
    bool promise,
    Func<Coop_Model_Base, bool> onPreResend = null)
  {
    Coop_Model_StageSyncPlayerRecord model = new Coop_Model_StageSyncPlayerRecord();
    model.id = 1002;
    model.rec = record;
    this.Send<Coop_Model_StageSyncPlayerRecord>(model, promise, to_client_id, onPreResend: onPreResend);
  }

  public void SendEnemyBossEscape(int sid, bool promise)
  {
    Coop_Model_EnemyBossEscape model = new Coop_Model_EnemyBossEscape();
    model.sid = sid;
    model.id = 1002;
    this.Send<Coop_Model_EnemyBossEscape>(model, promise);
  }

  public void SendEnemyBossAliveRequest()
  {
    Coop_Model_EnemyBossAliveRequest model = new Coop_Model_EnemyBossAliveRequest();
    model.id = 1002;
    this.Send<Coop_Model_EnemyBossAliveRequest>(model, false);
  }

  public void SendEnemyBossAliveRequested(int toClientId)
  {
    Coop_Model_EnemyBossAliveRequested model = new Coop_Model_EnemyBossAliveRequested();
    model.id = 1002;
    this.Send<Coop_Model_EnemyBossAliveRequested>(model, false, toClientId);
  }

  public void OnActiveSupply(int pointId)
  {
    Coop_Model_ActiveSupply model = new Coop_Model_ActiveSupply();
    model.id = 1002;
    model.pointId = pointId;
    this.Send<Coop_Model_ActiveSupply>(model);
  }
}
