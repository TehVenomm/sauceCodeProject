// Decompiled with JetBrains decompiler
// Type: ObjectPacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ObjectPacketSender : MonoBehaviour
{
  protected List<Coop_Model_ObjectBase> actionHistoryList = new List<Coop_Model_ObjectBase>();
  protected ObjectPacketSender.ActionHistoryData actionHistoryData;

  public StageObject owner { get; protected set; }

  public bool enableSend { get; set; }

  public float needWaitSyncTime { get; protected set; }

  public static ObjectPacketSender SetupComponent(StageObject set_object)
  {
    switch (set_object)
    {
      case Enemy _:
        return (ObjectPacketSender) ((Component) set_object).gameObject.AddComponent<EnemyPacketSender>();
      case Player _:
        return (ObjectPacketSender) ((Component) set_object).gameObject.AddComponent<PlayerPacketSender>();
      case Character _:
        return (ObjectPacketSender) ((Component) set_object).gameObject.AddComponent<CharacterPacketSender>();
      default:
        return ((Component) set_object).gameObject.AddComponent<ObjectPacketSender>();
    }
  }

  public ObjectPacketSender()
  {
    this.needWaitSyncTime = 0.0f;
    this.enableSend = true;
  }

  protected virtual void Awake() => this.owner = ((Component) this).GetComponent<StageObject>();

  protected int SendTo<T>(
    int to_client_id,
    T model,
    bool promise = false,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    if (!this.enableSend)
      Log.Error(LOG.COOP, "ObjectPacketSender::SendTo() Err. ( enableSend == false ) type : " + (object) (PACKET_TYPE) model.c);
    return MonoBehaviourSingleton<CoopNetworkManager>.I.SendToInBattle<T>(to_client_id, model, promise, onReceiveAck, onPreResend);
  }

  protected int SendBroadcast<T>(
    T model,
    bool promise = false,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    if (!this.enableSend)
      Log.Error(LOG.COOP, "ObjectPacketSender::SendTo() Err. ( enableSend == false ) type : " + (object) (PACKET_TYPE) model.c);
    return MonoBehaviourSingleton<CoopNetworkManager>.I.SendBroadcastInBattle<T>(model, promise, onReceiveAck, onPreResend);
  }

  protected int SendToExtra<T>(
    int to_client_id,
    T model,
    bool promise = false,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
    where T : Coop_Model_Base
  {
    if (!this.enableSend)
      Log.Error(LOG.COOP, "ObjectPacketSender::SendTo() Err. ( enableSend == false ) type : " + (object) (PACKET_TYPE) model.c);
    return MonoBehaviourSingleton<CoopNetworkManager>.I.SendTo<T>(to_client_id, model, promise, onReceiveAck, onPreResend);
  }

  public virtual bool IsEnableWaitSync() => false;

  public virtual float GetWaitTime(float base_time)
  {
    if (!this.IsEnableWaitSync())
      return base_time;
    float waitTime = this.needWaitSyncTime;
    if ((double) waitTime > (double) this.owner.objectParameter.maxWaitSyncTime)
      waitTime = this.owner.objectParameter.maxWaitSyncTime;
    if ((double) waitTime < (double) base_time)
      waitTime = base_time;
    return waitTime;
  }

  protected virtual void ClearActionHistory()
  {
    this.actionHistoryList.Clear();
    this.actionHistoryData = (ObjectPacketSender.ActionHistoryData) null;
  }

  protected virtual void StackActionHistory(Coop_Model_ObjectBase stack_model, bool is_act_model)
  {
    if (is_act_model)
      this.ClearActionHistory();
    else if (this.actionHistoryList.Count <= 0)
      return;
    if (this.actionHistoryList.Count <= 0)
    {
      this.actionHistoryData = new ObjectPacketSender.ActionHistoryData();
      this.actionHistoryData.startTime = Time.time;
      this.actionHistoryData.startPos = this.owner._position;
      ObjectPacketSender.ActionHistoryData actionHistoryData = this.actionHistoryData;
      Quaternion rotation = this.owner._rotation;
      double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
      actionHistoryData.startDir = (float) y;
    }
    this.actionHistoryList.Add(stack_model);
  }

  protected virtual void SendActionHistory(int to_client_id = 0)
  {
    int index = 0;
    for (int count = this.actionHistoryList.Count; index < count; ++index)
    {
      if (to_client_id == 0)
        this.SendBroadcast<Coop_Model_ObjectBase>(this.actionHistoryList[index]);
      else
        this.SendToExtra<Coop_Model_ObjectBase>(to_client_id, this.actionHistoryList[index]);
    }
    this.SaveNeedWaitSyncTime();
  }

  public void SaveNeedWaitSyncTime()
  {
    float num = 0.0f;
    if (this.actionHistoryData != null && (double) this.actionHistoryData.startTime >= 0.0)
      num = Time.time - this.actionHistoryData.startTime;
    if ((double) num <= (double) this.needWaitSyncTime)
      return;
    this.needWaitSyncTime = num;
  }

  public void PassNeedWaitSyncTime(float time)
  {
    if ((double) time <= 0.0 || (double) this.needWaitSyncTime <= 0.0)
      return;
    this.needWaitSyncTime -= time;
    if ((double) this.needWaitSyncTime >= 0.0)
      return;
    this.needWaitSyncTime = 0.0f;
  }

  public virtual void OnUpdate()
  {
  }

  public virtual void OnDestroyObject()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectDestroy model = new Coop_Model_ObjectDestroy();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_ObjectDestroy>(model, true);
  }

  public virtual void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    if (!this.enableSend || this.owner.IsCoopNone())
      return;
    Coop_Model_ObjectAttackedHitOwner model = new Coop_Model_ObjectAttackedHitOwner();
    model.id = this.owner.id;
    model.SetAttackedHitStatus(status);
    this.SendTo<Coop_Model_ObjectAttackedHitOwner>(this.owner.coopClientId, model);
  }

  public virtual void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    bool flag = false;
    if (status.afterHP <= 0)
      flag = true;
    if (status.breakRegion)
      flag = true;
    if (status.reactionType == 22)
      flag = true;
    Coop_Model_ObjectAttackedHitFix model = new Coop_Model_ObjectAttackedHitFix();
    model.id = this.owner.id;
    model.SetAttackedHitStatus(status);
    if (flag)
      this.SendBroadcast<Coop_Model_ObjectAttackedHitFix>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
      {
        if (Object.op_Equality((Object) this.owner, (Object) null))
          return false;
        Coop_Model_ObjectAttackedHitFix objectAttackedHitFix = send_model as Coop_Model_ObjectAttackedHitFix;
        Character owner1 = this.owner as Character;
        if (Object.op_Inequality((Object) owner1, (Object) null))
        {
          objectAttackedHitFix.afterHP = owner1.hp;
          Player owner2 = this.owner as Player;
          if (Object.op_Inequality((Object) owner2, (Object) null))
            objectAttackedHitFix.afterHealHp = owner2.healHp;
          if (objectAttackedHitFix.afterHP > 0 && objectAttackedHitFix.reactionType == 8)
            objectAttackedHitFix.reactionType = 0;
        }
        BarrierBulletObject owner3 = this.owner as BarrierBulletObject;
        if (Object.op_Inequality((Object) owner3, (Object) null))
          objectAttackedHitFix.afterHP = owner3.GetHp();
        return true;
      }));
    else
      this.SendBroadcast<Coop_Model_ObjectAttackedHitFix>(model);
  }

  public virtual void OnKeepWaitingPacket(StageObject.WAITING_PACKET waiting_packet_type)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectKeepWaitingPacket model = new Coop_Model_ObjectKeepWaitingPacket();
    model.id = this.owner.id;
    model.type = (int) waiting_packet_type;
    this.SendBroadcast<Coop_Model_ObjectKeepWaitingPacket>(model);
  }

  public void OnBulletObservableSet(int observedID)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectBulletObservableSet model = new Coop_Model_ObjectBulletObservableSet();
    model.id = this.owner.id;
    model.observedID = observedID;
    this.SendBroadcast<Coop_Model_ObjectBulletObservableSet>(model);
  }

  public void OnBulletObservableBroken(int observedID, bool isSendOnlyOriginal)
  {
    if (!this.enableSend)
      return;
    if (isSendOnlyOriginal)
    {
      if (!this.owner.IsOriginal())
        return;
    }
    else if (this.owner.IsCoopNone())
      return;
    Coop_Model_ObjectBulletObservableBroken model = new Coop_Model_ObjectBulletObservableBroken();
    model.id = this.owner.id;
    model.observedID = observedID;
    this.SendBroadcast<Coop_Model_ObjectBulletObservableBroken>(model);
  }

  public void OnBulletObservableSearchTarget(int observedID, int targetId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectBulletObservableSearchTarget model = new Coop_Model_ObjectBulletObservableSearchTarget();
    model.id = this.owner.id;
    model.observedID = observedID;
    model.targetId = targetId;
    this.SendBroadcast<Coop_Model_ObjectBulletObservableSearchTarget>(model);
  }

  public void OnBulletObservableTurretBitTarget(int observedID, int targetId, int regionId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectBulletObservableTurretBitTarget model = new Coop_Model_ObjectBulletObservableTurretBitTarget();
    model.id = this.owner.id;
    model.observedID = observedID;
    model.targetId = targetId;
    this.SendBroadcast<Coop_Model_ObjectBulletObservableTurretBitTarget>(model);
  }

  public void OnShotGimmickGenerator(Vector3 pos)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectShotGimmickGenerator model = new Coop_Model_ObjectShotGimmickGenerator();
    model.id = this.owner.id;
    model.pos = pos;
    this.SendBroadcast<Coop_Model_ObjectShotGimmickGenerator>(model);
  }

  public void OnSetCoopMode(StageObject.COOP_MODE_TYPE coopModeType)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ObjectCoopInfo model = new Coop_Model_ObjectCoopInfo();
    model.id = this.owner.id;
    model.CoopModeType = coopModeType;
    this.SendBroadcast<Coop_Model_ObjectCoopInfo>(model);
  }

  public class ActionHistoryData
  {
    public float startTime = -1f;
    public Vector3 startPos = Vector3.zero;
    public float startDir;
  }
}
