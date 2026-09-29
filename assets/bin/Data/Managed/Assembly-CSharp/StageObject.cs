// Decompiled with JetBrains decompiler
// Type: StageObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StageObject : ControlObject, IBulletObserver
{
  private int _id;
  protected List<StageObject.AttackedContinuationStatus> continuationList = new List<StageObject.AttackedContinuationStatus>();
  protected List<StageObject.HitIntervalStatus> hitIntervalList = new List<StageObject.HitIntervalStatus>();
  protected uint voiceChannel;
  public StageObject.HIT_OFF_FLAG hitOffFlag;
  protected List<StageObject.HitOffTimer> hitOffTimers = new List<StageObject.HitOffTimer>();
  private Collider[] ignoreColliders;
  public List<int> loopSeForceEndList = new List<int>();
  protected bool isWallStay;
  protected float wallStayTimer;
  protected StageObject.WaitingPacketParam[] waitingPacketParams = new StageObject.WaitingPacketParam[16 /*0x10*/];
  protected StageObject.NodeTable nodeCache = new StageObject.NodeTable();
  protected AttackedHitStatus nowAttackedHitStatus;
  private bool isRegisteredStageObjectManager;
  protected List<IBulletObservable> bulletObservableList = new List<IBulletObservable>();
  protected List<int> bulletObservableIdList = new List<int>();
  public int bulletIndex;

  public InGameSettingsManager.StageObjectParam objectParameter { get; private set; }

  public StageObject.OBJECT_TYPE objectType { get; protected set; }

  public virtual int id
  {
    get => this._id;
    set => this._id = value;
  }

  public ControllerBase controller { get; set; }

  public bool isInitialized { get; protected set; }

  public bool isLoading { get; private set; }

  public Rigidbody _rigidbody { get; protected set; }

  public Collider _collider { get; protected set; }

  public ObjectPacketReceiver packetReceiver { get; protected set; }

  public ObjectPacketSender packetSender { get; protected set; }

  public StageObject.COOP_MODE_TYPE coopMode { get; protected set; }

  public bool IsCoopNone() => this.coopMode == StageObject.COOP_MODE_TYPE.NONE;

  public bool IsOriginal() => this.coopMode == StageObject.COOP_MODE_TYPE.ORIGINAL;

  public bool IsMirror() => this.coopMode == StageObject.COOP_MODE_TYPE.MIRROR;

  public bool IsPuppet() => this.coopMode == StageObject.COOP_MODE_TYPE.PUPPET;

  public int coopClientId { get; protected set; }

  public bool isCoopInitialized { get; set; }

  public void SetHitOffFlag(bool enable, StageObject.HIT_OFF_FLAG flag)
  {
    if (enable)
      this.hitOffFlag |= flag;
    else
      this.hitOffFlag &= ~flag;
  }

  public List<Collider> ignoreHitAttackColliders { get; protected set; }

  public bool IsWallStay()
  {
    return this.objectParameter != null && (double) this.wallStayTimer >= (double) this.objectParameter.wallStayCheckTime;
  }

  public bool isDestroyWaitFlag { get; protected set; }

  public bool IsRegisteredStageObjectManager => this.isRegisteredStageObjectManager;

  public void AddController<T>() where T : ControllerBase
  {
    if (!Object.op_Equality((Object) this.controller, (Object) null))
      return;
    if (!CoopStageObjectUtility.CanControll(this))
      Log.Error(LOG.INGAME, "StageObject::AddController. field block obj({0},{1}) to {2}", (object) this, (object) this.coopMode, (object) typeof (T));
    else
      ((Component) this).gameObject.AddComponent<T>();
  }

  public void RemoveController()
  {
    if (!Object.op_Inequality((Object) this.controller, (Object) null))
      return;
    this.controller.SetEnableControll(false);
    Object.Destroy((Object) this.controller);
  }

  public Vector2 positionXZ
  {
    get
    {
      Vector3 position = this._position;
      return new Vector2(position.x, position.z);
    }
    set
    {
      Vector3 position = this._position;
      position.x = value.x;
      position.z = value.y;
      this._position = position;
    }
  }

  public Vector2 forwardXZ
  {
    get
    {
      Vector3 forward = this._forward;
      return new Vector2(forward.x, forward.z);
    }
  }

  public virtual void LookAt(Vector3 pos, bool isBlindEnable = false)
  {
    pos.y = this._position.y;
    this._LookAt(pos);
  }

  protected virtual void OnEnable()
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      this.SetNotifyMaster((DisableNotifyMonoBehaviour) MonoBehaviourSingleton<StageObjectManager>.I);
      this.isRegisteredStageObjectManager = true;
    }
    if (!MonoBehaviourSingleton<MiniMap>.IsValid())
      return;
    MonoBehaviourSingleton<MiniMap>.I.Attach((MonoBehaviour) this);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!MonoBehaviourSingleton<MiniMap>.IsValid())
      return;
    MonoBehaviourSingleton<MiniMap>.I.Detach((MonoBehaviour) this);
  }

  protected override void Awake()
  {
    base.Awake();
    this.id = 0;
    this.objectType = StageObject.OBJECT_TYPE.STAGE_OBJECT;
    this.id = 0;
    this.isInitialized = false;
    this.coopMode = StageObject.COOP_MODE_TYPE.NONE;
    this.coopClientId = 0;
    this.isCoopInitialized = false;
    this.hitOffFlag = StageObject.HIT_OFF_FLAG.NONE;
    this.ignoreHitAttackColliders = new List<Collider>();
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = ((Component) this).GetComponent<Collider>();
    this.objectParameter = !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() ? new InGameSettingsManager.StageObjectParam() : MonoBehaviourSingleton<InGameSettingsManager>.I.stageObject;
    if (Object.op_Equality((Object) this.packetReceiver, (Object) null))
      this.packetReceiver = ObjectPacketReceiver.SetupComponent(this);
    if (!Object.op_Equality((Object) this.packetSender, (Object) null))
      return;
    this.packetSender = ObjectPacketSender.SetupComponent(this);
  }

  protected virtual void Start()
  {
  }

  protected virtual void Clear()
  {
  }

  public virtual void OnLoadStart()
  {
    this.isLoading = true;
    this.Clear();
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.LOAD;
  }

  public virtual void OnLoadComplete()
  {
    this.nodeCache.Clear();
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.LOAD;
    this.isLoading = false;
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = ((Component) this).GetComponent<Collider>();
    if (!this.isInitialized)
      this.Initialize();
    if (!MonoBehaviourSingleton<MiniMap>.IsValid())
      return;
    MonoBehaviourSingleton<MiniMap>.I.Attach((MonoBehaviour) this);
  }

  protected virtual void Initialize()
  {
    this.voiceChannel = this.GetVoiceChannel();
    this.isInitialized = true;
  }

  protected virtual uint GetVoiceChannel() => 0;

  protected virtual bool EnablePlaySound() => true;

  public virtual bool DestroyObject()
  {
    this.isDestroyWaitFlag = false;
    if (Object.op_Inequality((Object) this.packetSender, (Object) null))
      this.packetSender.OnDestroyObject();
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(this);
    Object.Destroy((Object) ((Component) this).gameObject);
    return true;
  }

  protected virtual void Update()
  {
    int index1 = 0;
    while (index1 < this.hitOffTimers.Count)
    {
      if ((double) this.hitOffTimers[index1].endTime <= (double) Time.time)
      {
        this.hitOffFlag &= ~this.hitOffTimers[index1].hitOffFlag;
        this.hitOffTimers.RemoveAt(index1);
      }
      else
        ++index1;
    }
    if (Object.op_Inequality((Object) this.packetReceiver, (Object) null))
      this.packetReceiver.OnUpdate();
    if (Object.op_Inequality((Object) this.packetSender, (Object) null))
      this.packetSender.OnUpdate();
    this.UpdateWaitingPacket();
    int index2 = 0;
    for (int count = this.continuationList.Count; index2 < count; ++index2)
      this.OnAttackedContinuationUpdate(this.continuationList[index2]);
    if (this.hitIntervalList.IsNullOrEmpty<StageObject.HitIntervalStatus>())
      return;
    this.hitIntervalList.RemoveAll((Predicate<StageObject.HitIntervalStatus>) (item => !item.enable));
  }

  protected virtual void LateUpdate()
  {
  }

  protected virtual void FixedUpdate()
  {
    if (this.isWallStay)
    {
      this.wallStayTimer += Time.deltaTime;
    }
    else
    {
      this.wallStayTimer -= Time.deltaTime * 0.5f;
      if ((double) this.wallStayTimer < 0.0)
        this.wallStayTimer = 0.0f;
    }
    this.isWallStay = false;
    for (int index = 0; index < this.continuationList.Count; ++index)
    {
      if (this.continuationList[index] != null)
        this.OnAttackedContinuationFixedUpdate(this.continuationList[index]);
    }
    if (this.hitIntervalList.IsNullOrEmpty<StageObject.HitIntervalStatus>())
      return;
    int index1 = 0;
    for (int count = this.hitIntervalList.Count; index1 < count; ++index1)
      this.hitIntervalList[index1].hitIntervalTimer -= Time.deltaTime;
  }

  protected virtual void OnCollisionEnter(Collision collision)
  {
  }

  protected virtual void OnCollisionStay(Collision collision)
  {
    if (collision.gameObject.layer != 9 && collision.gameObject.layer != 17 && collision.gameObject.layer != 18)
      return;
    this.isWallStay = true;
  }

  protected virtual void OnCollisionExit(Collision collision)
  {
  }

  public virtual void OnAnimatorMove()
  {
  }

  public virtual void OnDetachedObject(StageObject stage_object)
  {
    if (!(stage_object is Enemy) || (stage_object as Enemy).colliders != this.ignoreColliders)
      return;
    this.ResetIgnoreColliders();
  }

  public virtual bool CheckHitAttack(
    AttackHitInfo info,
    Collider to_collider,
    StageObject to_object)
  {
    return true;
  }

  public virtual void OnAvoidHit(StageObject fromObject, AttackHitInfo attackHitInfo)
  {
  }

  public virtual void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
  {
    hit_param.toObject.OnAttackedHit(info, hit_param);
  }

  public virtual AttackHitColliderProcessor.HitParam SelectHitCollider(
    AttackHitColliderProcessor processor,
    List<AttackHitColliderProcessor.HitParam> hit_params)
  {
    return hit_params[0];
  }

  public virtual void OnAttackedHit(
    AttackHitInfo info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    AttackedHitStatus status1 = new AttackedHitStatus();
    status1.hitParam = hit_param;
    status1.attackInfo = info;
    status1.fromObjectID = hit_param.fromObject.id;
    status1.fromObject = hit_param.fromObject;
    status1.fromType = hit_param.fromObject.objectType;
    status1.fromPos = hit_param.fromObject._position;
    status1.hitPos = hit_param.point;
    status1.distanceXZ = hit_param.distanceXZ;
    status1.hitTime = hit_param.time;
    status1.isSpAttackHit = hit_param.isSpAttackHit;
    status1.attackMode = hit_param.attackMode;
    status1.damageDistanceData = hit_param.damageDistanceData;
    status1.exHitPos = hit_param.exHitPos;
    this.nowAttackedHitStatus = status1;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      status1.fromClientID = MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId;
    if (status1.fromType == StageObject.OBJECT_TYPE.SELF)
      status1.fromType = StageObject.OBJECT_TYPE.PLAYER;
    this.OnAttackedHitDirection(new AttackedHitStatusDirection(status1));
    if (!this.IsValidAttackedHit(hit_param.fromObject) || this.IsPuppet() || hit_param.fromObject.IsPuppet())
      return;
    this.OnAttackedHitLocal(new AttackedHitStatusLocal(status1));
    if (this.IsMirror() || this.IsPuppet())
    {
      if (!Object.op_Inequality((Object) this.packetSender, (Object) null))
        return;
      this.packetSender.OnAttackedHitOwner(new AttackedHitStatusOwner(status1));
    }
    else
    {
      if (!this.IsEnableAttackedHitOwner())
        return;
      this.OnAttackedHitOwner(new AttackedHitStatusOwner(status1));
      AttackedHitStatusFix status2 = new AttackedHitStatusFix(status1);
      this.OnAttackedHitFix(status2);
      if (!Object.op_Inequality((Object) this.packetSender, (Object) null))
        return;
      this.packetSender.OnAttackedHitFix(status2);
    }
  }

  protected virtual bool IsValidAttackedHit(StageObject from_object) => true;

  protected virtual void OnAttackedHitDirection(AttackedHitStatusDirection status)
  {
    if (!this.CheckStatusForHitEffect(status))
    {
      this.OnIgnoreHitAttack();
    }
    else
    {
      status.fromObject.OnAttackFromHitDirection(status, this);
      this.OnPlayAttackedHitEffect(status);
    }
  }

  protected virtual void OnIgnoreHitAttack()
  {
  }

  protected virtual bool CheckStatusForHitEffect(AttackedHitStatusDirection status) => true;

  protected virtual void OnAttackFromHitDirection(
    AttackedHitStatusDirection status,
    StageObject to_object)
  {
  }

  protected virtual void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
  {
  }

  protected virtual void OnAttackedHitLocal(AttackedHitStatusLocal status)
  {
  }

  public virtual void AbsorptionProc(Character targetChar, AttackedHitStatusLocal status)
  {
  }

  public virtual void AbsorptionProcByBuff(AttackedHitStatusLocal status)
  {
  }

  public virtual bool CutAndAbsorbDamageByBuff(
    Character targetCharacter,
    AttackedHitStatusLocal status)
  {
    return false;
  }

  public virtual bool ChargeSkillWhenDamagedByBuff() => false;

  public virtual bool InvincibleDamageByBuff(
    Character targetCharacter,
    AttackedHitStatusLocal status)
  {
    return false;
  }

  public virtual void GetAtk(
    AttackHitInfo info,
    ref AtkAttribute atk,
    SkillInfo.SkillParam skillParamInfo = null)
  {
    if (info == null)
      return;
    atk.Add(info.atk);
  }

  public virtual void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
  }

  public virtual bool IsEnableAttackedHitOwner() => true;

  public virtual void OnAttackedHitFix(AttackedHitStatusFix status)
  {
  }

  public virtual bool OnContinuationEnter(
    AttackContinuationInfo info,
    StageObject from_object,
    Collider from_collider,
    float time)
  {
    int index = 0;
    for (int count = this.continuationList.Count; index < count; ++index)
    {
      if (this.continuationList[index].attackInfo == info && Object.op_Equality((Object) this.continuationList[index].fromCollider, (Object) from_collider))
        return false;
    }
    StageObject.AttackedContinuationStatus status = new StageObject.AttackedContinuationStatus();
    status.attackInfo = info;
    status.fromObject = from_object;
    status.fromCollider = from_collider;
    status.hitTime = time;
    status.hitStartTime = Time.time;
    this.continuationList.Add(status);
    this.OnAttackedContinuationStart(status);
    return true;
  }

  public virtual void OnContinuationExit(AttackContinuationInfo info, Collider from_collider)
  {
    int index = 0;
    for (int count = this.continuationList.Count; index < count; ++index)
    {
      if (this.continuationList[index].attackInfo == info && Object.op_Equality((Object) this.continuationList[index].fromCollider, (Object) from_collider))
      {
        this.OnAttackedContinuationEnd(this.continuationList[index]);
        this.continuationList.RemoveAt(index);
        break;
      }
    }
  }

  protected virtual void OnAttackedContinuationStart(StageObject.AttackedContinuationStatus status)
  {
  }

  protected virtual void OnAttackedContinuationUpdate(StageObject.AttackedContinuationStatus status)
  {
  }

  protected virtual void OnAttackedContinuationFixedUpdate(
    StageObject.AttackedContinuationStatus status)
  {
  }

  protected virtual void OnAttackedContinuationEnd(StageObject.AttackedContinuationStatus status)
  {
  }

  protected float GetContinuationTimeChangeRate(StageObject.AttackedContinuationStatus status)
  {
    if (status.attackInfo == null)
      return 1f;
    float num1 = status.hitTime + Time.time - status.hitStartTime;
    float continuationTimeChangeRate = 1f;
    AttackInfo.TimeChange timeChange = status.attackInfo.timeChange;
    if ((double) timeChange.intervalTime > 0.0)
    {
      float num2 = (num1 - timeChange.startTime) / timeChange.intervalTime;
      if ((double) num2 < 0.0)
        num2 = 0.0f;
      if ((double) num2 > 1.0)
        num2 = 1f;
      continuationTimeChangeRate = timeChange.startRate + (timeChange.endRate - timeChange.startRate) * num2;
    }
    return continuationTimeChangeRate;
  }

  public virtual Vector3 GetCameraTargetPos()
  {
    return Vector3.op_Addition(this._position, new Vector3(0.0f, 1f, 0.0f));
  }

  protected void IgnoreColliders(Collider[] colliders)
  {
    if (Object.op_Equality((Object) this._collider, (Object) null) || colliders == null)
      return;
    if (this.ignoreColliders != null)
      Utility.IgnoreCollision(this._collider, this.ignoreColliders, false);
    Utility.IgnoreCollision(this._collider, colliders, true);
    this.ignoreColliders = colliders;
  }

  protected void ResetIgnoreColliders()
  {
    if (Object.op_Equality((Object) this._collider, (Object) null) || this.ignoreColliders == null)
      return;
    Utility.IgnoreCollision(this._collider, this.ignoreColliders, false);
    this.ignoreColliders = (Collider[]) null;
  }

  public void SetCoopMode(StageObject.COOP_MODE_TYPE coop_mode, int client_id)
  {
    if ((coop_mode == StageObject.COOP_MODE_TYPE.NONE || coop_mode == StageObject.COOP_MODE_TYPE.ORIGINAL) && client_id != 0)
      Log.Error(LOG.INGAME, "StageObject::SetCoopMode() Err ( client_id is invalid. )");
    if (coop_mode == StageObject.COOP_MODE_TYPE.ORIGINAL && !CoopStageObjectUtility.CanControll(this))
    {
      Log.Error(LOG.INGAME, "StageObject::SetCoopMode. field block obj({0}) to {1}", (object) this, (object) coop_mode);
    }
    else
    {
      if (this.coopMode != StageObject.COOP_MODE_TYPE.NONE)
      {
        bool flag = false;
        if (CoopManager.IsValidInCoop())
          flag = true;
        if (!flag)
        {
          Log.Error(LOG.INGAME, "StageObject::SetCoopMode() Err ( not coop )");
          return;
        }
      }
      this.coopMode = coop_mode;
      this.coopClientId = client_id;
    }
  }

  public virtual Transform FindNode(string name)
  {
    if (string.IsNullOrEmpty(name))
      return this._transform;
    StringKeyTableBase.Item nodeItem = this.nodeCache.GetNodeItem(name);
    if (nodeItem != null)
    {
      if (nodeItem.value != null)
        return nodeItem.value as Transform;
      this.nodeCache.Remove(name);
    }
    Transform node = Utility.Find(this._transform, name);
    if (Object.op_Inequality((Object) node, (Object) null))
      this.nodeCache.Add(name, node);
    return node;
  }

  public virtual void OnAnimEvent(AnimEventData.EventData data)
  {
    switch (data.id)
    {
      case AnimEventFormat.ID.INVICIBLE_ON:
        this.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
        return;
      case AnimEventFormat.ID.INVICIBLE_OFF:
        this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INVICIBLE;
        return;
      case AnimEventFormat.ID.SHAKE_CAMERA:
        float floatArg = data.floatArgs[0];
        float cycle_time = data.floatArgs.Length > 1 ? data.floatArgs[1] : 0.0f;
        if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
          return;
        MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(this._position, floatArg, cycle_time);
        return;
      case AnimEventFormat.ID.SE_ONESHOT:
        int intArg1 = data.intArgs[0];
        string stringArg1 = data.stringArgs[0];
        if (intArg1 != 0)
        {
          if (!this.EnablePlaySound())
            return;
          SoundManager.PlayOneShotSE(intArg1, (DisableNotifyMonoBehaviour) this, this.FindNode(stringArg1));
          return;
        }
        break;
      case AnimEventFormat.ID.SE_LOOP_PLAY:
        int intArg2 = data.intArgs[0];
        if (data.intArgs.Length > 1 && data.intArgs[1] != 0)
          this.loopSeForceEndList.Add(intArg2);
        string stringArg2 = data.stringArgs[0];
        if (!this.EnablePlaySound())
          return;
        SoundManager.PlayLoopSE(intArg2, (DisableNotifyMonoBehaviour) this, this.FindNode(stringArg2));
        return;
      case AnimEventFormat.ID.SE_LOOP_STOP:
        SoundManager.StopLoopSE(data.intArgs[0], (DisableNotifyMonoBehaviour) this);
        return;
    }
    Log.Error(LOG.INGAME, "AnimEvent Error! Event={0} Object={1}", (object) data.name, (object) ((Object) this).name);
  }

  public virtual AttackInfo[] GetAttackInfos() => (AttackInfo[]) null;

  public virtual float GetAttackInfoRate() => 0.0f;

  public virtual AttackInfo FindAttackInfo(string name, bool fix_rate = true, bool isDuplicate = false)
  {
    return this._FindAttackInfo(this.GetAttackInfos(), name, fix_rate, this.GetAttackInfoRate(), isDuplicate);
  }

  public virtual AttackInfo FindAttackInfoExternal(string name, bool fix_rate, float rate)
  {
    return this._FindAttackInfo(this.GetAttackInfos(), name, fix_rate, rate);
  }

  protected virtual AttackInfo _FindAttackInfo(
    AttackInfo[] attack_infos,
    string name,
    bool fix_rate,
    float rate,
    bool isDuplicate = false)
  {
    if (string.IsNullOrEmpty(name))
      return (AttackInfo) null;
    if (attack_infos == null)
      return (AttackInfo) null;
    AttackInfo attackInfo1 = (AttackInfo) null;
    int index = 0;
    for (int length = attack_infos.Length; index < length; ++index)
    {
      AttackInfo attackInfo2 = attack_infos[index];
      if (attackInfo2.name == name)
      {
        if (fix_rate && !string.IsNullOrEmpty(attackInfo2.rateInfoName) && (double) rate != 0.0)
        {
          AttackInfo attackInfo3 = this._FindAttackInfo(attack_infos, attackInfo2.rateInfoName, false, 0.0f);
          attackInfo1 = attackInfo2.GetRateAttackInfo(attackInfo3, rate);
          break;
        }
        attackInfo1 = attackInfo2;
        break;
      }
    }
    if (attackInfo1 == null)
    {
      Log.Error(LOG.INGAME, "FindAttackInfo not found. name : " + name);
      attackInfo1 = attack_infos[0];
    }
    return isDuplicate ? attackInfo1.Duplicate() : attackInfo1;
  }

  public virtual SkillInfo.SkillParam GetSkillParam(int index) => (SkillInfo.SkillParam) null;

  public virtual void SetHitOffTimer(StageObject.HIT_OFF_FLAG flag, float time)
  {
    if ((double) time <= 0.0 || flag == StageObject.HIT_OFF_FLAG.NONE)
      return;
    this.hitOffFlag |= flag;
    float num = Time.time + time;
    int index = 0;
    for (int count = this.hitOffTimers.Count; index < count; ++index)
    {
      if (this.hitOffTimers[index].hitOffFlag == flag)
      {
        if ((double) this.hitOffTimers[index].endTime >= (double) num)
          return;
        this.hitOffTimers[index].endTime = num;
        return;
      }
    }
    this.hitOffTimers.Add(new StageObject.HitOffTimer()
    {
      endTime = num,
      hitOffFlag = flag
    });
  }

  public virtual void StartWaitingPacket(
    StageObject.WAITING_PACKET type,
    bool keep_sync,
    float add_margin_time = 0.0f)
  {
    if (this.IsCoopNone())
      return;
    this.waitingPacketParams[(int) type] = new StageObject.WaitingPacketParam()
    {
      type = type,
      startTime = Time.time,
      keepSync = keep_sync,
      addMarginTime = add_margin_time
    };
  }

  public virtual bool IsValidWaitingPacket(StageObject.WAITING_PACKET type)
  {
    return !this.IsCoopNone() && this.waitingPacketParams[(int) type] != null;
  }

  public virtual void UpdateWaitingPacket()
  {
    if (this.IsCoopNone())
      return;
    int index1 = 0;
    for (int index2 = 16 /*0x10*/; index1 < index2; ++index1)
    {
      StageObject.WaitingPacketParam waitingPacketParam = this.waitingPacketParams[index1];
      if (waitingPacketParam != null)
      {
        if ((double) waitingPacketParam.startTime <= 0.0)
        {
          Log.Error("StageObject::UpdateWaitingPacket() Err ( waitingPacketStartTime <= 0.0f )");
          break;
        }
        if (this.IsOriginal())
        {
          if (waitingPacketParam.keepSync && (double) Time.time >= (double) waitingPacketParam.startTime + (double) this.objectParameter.waitingPacketIntervalTime)
            this.KeepWaitingPacket(waitingPacketParam.type);
        }
        else if (this.IsPuppet() || this.IsMirror())
        {
          float num = this.objectParameter.waitingPacketMarginTime + waitingPacketParam.addMarginTime;
          if (waitingPacketParam.keepSync)
            num += this.objectParameter.waitingPacketIntervalTime;
          if ((double) Time.time > (double) waitingPacketParam.startTime + (double) num)
            this.OnFailedWaitingPacket(waitingPacketParam.type);
        }
      }
    }
  }

  public void KeepWaitingPacket(StageObject.WAITING_PACKET type)
  {
    StageObject.WaitingPacketParam waitingPacketParam = this.waitingPacketParams[(int) type];
    if (waitingPacketParam == null)
      return;
    waitingPacketParam.startTime = Time.time;
    if (!Object.op_Inequality((Object) this.packetSender, (Object) null))
      return;
    this.packetSender.OnKeepWaitingPacket(waitingPacketParam.type);
  }

  public virtual void OnFailedWaitingPacket(StageObject.WAITING_PACKET type)
  {
    this.EndWaitingPacket(type);
  }

  public virtual void EndWaitingPacket(StageObject.WAITING_PACKET type)
  {
    this.waitingPacketParams[(int) type] = (StageObject.WaitingPacketParam) null;
  }

  public static Vector3 GetAppearToTargetPos(
    Vector3 from_pos,
    Vector3 target_pos,
    Vector3 col_offset,
    float col_radius,
    float appear_distance,
    float appear_margin)
  {
    return StageObject._GetAppearToTargetPos(from_pos, target_pos, col_offset, col_radius, appear_distance, appear_margin, true, true, out bool _);
  }

  private static Vector3 _GetAppearToTargetPos(
    Vector3 from_pos,
    Vector3 target_pos,
    Vector3 col_offset,
    float col_radius,
    float appear_distance,
    float appear_margin,
    bool from_inside,
    bool target_inside,
    out bool just_appear)
  {
    just_appear = false;
    Vector3 vector3 = Vector3.op_Subtraction(target_pos, from_pos);
    float magnitude = ((Vector3) ref vector3).magnitude;
    if ((double) magnitude <= 0.0)
    {
      just_appear = true;
      return from_pos;
    }
    if ((double) appear_distance >= (double) magnitude)
      return from_pos;
    if ((double) col_offset.y <= 0.0)
      col_offset.y = 0.1f;
    List<StageObject.CastHitInfo> castHitInfoList = new List<StageObject.CastHitInfo>();
    RaycastHit[] raycastHitArray1 = Physics.RaycastAll(Vector3.op_Addition(target_pos, col_offset), Vector3.op_UnaryNegation(vector3), magnitude, 393728 /*0x060200*/);
    int index1 = 0;
    for (int length = raycastHitArray1.Length; index1 < length; ++index1)
      castHitInfoList.Add(new StageObject.CastHitInfo()
      {
        distance = ((RaycastHit) ref raycastHitArray1[index1]).distance - col_radius,
        faceToTarget = true,
        collider = ((RaycastHit) ref raycastHitArray1[index1]).collider
      });
    castHitInfoList.Add(new StageObject.CastHitInfo()
    {
      distance = 0.0f,
      faceToTarget = !target_inside,
      collider = (Collider) null
    });
    RaycastHit[] raycastHitArray2 = Physics.RaycastAll(Vector3.op_Addition(from_pos, col_offset), vector3, magnitude, 393728 /*0x060200*/);
    int index2 = 0;
    for (int length = raycastHitArray2.Length; index2 < length; ++index2)
      castHitInfoList.Add(new StageObject.CastHitInfo()
      {
        distance = magnitude - ((RaycastHit) ref raycastHitArray2[index2]).distance + col_radius,
        faceToTarget = false,
        collider = ((RaycastHit) ref raycastHitArray2[index2]).collider
      });
    castHitInfoList.Add(new StageObject.CastHitInfo()
    {
      distance = magnitude,
      faceToTarget = from_inside,
      collider = (Collider) null
    });
    castHitInfoList.Sort((Comparison<StageObject.CastHitInfo>) ((a, b) =>
    {
      float num = a.distance - b.distance;
      if ((double) num == 0.0)
      {
        if (a.faceToTarget == b.faceToTarget)
          return 0;
        return !a.faceToTarget ? -1 : 1;
      }
      return (double) num <= 0.0 ? -1 : 1;
    }));
    int index3 = 0;
    for (int count = castHitInfoList.Count; index3 < count; ++index3)
    {
      StageObject.CastHitInfo castHitInfo1 = castHitInfoList[index3];
      if (!castHitInfo1.checkCollider && !Object.op_Equality((Object) castHitInfo1.collider, (Object) null))
      {
        int index4 = index3;
        while (0 <= index4 && index4 < count)
        {
          StageObject.CastHitInfo castHitInfo2 = castHitInfoList[index4];
          if (index4 != index3)
          {
            if (Object.op_Equality((Object) castHitInfo2.collider, (Object) castHitInfo1.collider))
            {
              castHitInfo2.checkCollider = true;
              break;
            }
            castHitInfo2.enable = false;
          }
          if (castHitInfo1.faceToTarget)
            ++index4;
          else
            --index4;
        }
      }
    }
    float num1 = magnitude;
    for (int index5 = castHitInfoList.Count - 2; index5 >= 0; --index5)
    {
      StageObject.CastHitInfo castHitInfo3 = castHitInfoList[index5];
      StageObject.CastHitInfo castHitInfo4 = castHitInfoList[index5 + 1];
      if (castHitInfo3.enable && castHitInfo4.enable && (double) castHitInfo3.distance != (double) castHitInfo4.distance && !castHitInfo3.faceToTarget && castHitInfo4.faceToTarget)
      {
        if ((double) castHitInfo3.distance <= (double) appear_distance && (double) castHitInfo4.distance >= (double) appear_distance)
        {
          num1 = appear_distance;
          just_appear = true;
          break;
        }
        if ((double) castHitInfo3.distance >= (double) appear_distance)
          num1 = castHitInfo3.distance;
        else if ((double) castHitInfo4.distance >= (double) appear_distance - (double) appear_margin)
        {
          num1 = castHitInfo4.distance;
          break;
        }
      }
    }
    return (double) num1 == (double) magnitude ? from_pos : Vector3.op_Subtraction(target_pos, Vector3.op_Multiply(((Vector3) ref vector3).normalized, num1));
  }

  public virtual Vector3 GetPredictivePosition()
  {
    Vector3 pos;
    return (this.IsPuppet() || this.IsMirror()) && Object.op_Inequality((Object) this.packetReceiver, (Object) null) && this.packetReceiver.GetPredictivePosition(out pos) ? pos : this._position;
  }

  public virtual Vector3 GetTargetPosition(StageObject target)
  {
    return Object.op_Equality((Object) target, (Object) null) ? Vector3.zero : target._position;
  }

  public virtual void ApplySyncPosition(Vector3 pos, float dir, bool force_sync = false)
  {
    this._rotation = Quaternion.AngleAxis(dir, Vector3.up);
    this._position = pos;
  }

  public virtual bool isProgressStop()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return false;
    bool flag = false;
    if (MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
      flag = true;
    else if (this.IsCoopNone() || this.IsOriginal())
    {
      if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
        flag = true;
    }
    else if (MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      CoopClient byClientId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(this.coopClientId);
      if (Object.op_Inequality((Object) byClientId, (Object) null) && !byClientId.IsBattleStart())
        flag = true;
    }
    return flag;
  }

  public virtual AttackHitChecker ReferenceAttackHitChecker() => (AttackHitChecker) null;

  public List<IBulletObservable> GetBulletObservableList() => this.bulletObservableList;

  public virtual int GetObservedID() => -1;

  public virtual void RegisterObservable(IBulletObservable observable)
  {
    if (this.bulletObservableList.Contains(observable))
      return;
    this.bulletObservableList.Add(observable);
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    this.RegisterObservableID(observable.GetObservedID());
  }

  public void RegisterObservableID(int observedID)
  {
    if (this.bulletObservableIdList.Contains(observedID))
      return;
    this.bulletObservableIdList.Add(observedID);
    if (!Object.op_Inequality((Object) this.packetSender, (Object) null))
      return;
    this.packetSender.OnBulletObservableSet(observedID);
  }

  public virtual void OnBreak(int brokenBulletID, bool isSendOnlyOriginal)
  {
  }

  public virtual void OnBulletDestroy(int observedID)
  {
  }

  public virtual void OnSetSearchTarget(int observedID, int targetID)
  {
  }

  public virtual void OnSetTurretBitTarget(int observedID, int targetID, int regionID)
  {
  }

  public bool IsIgnoreByHitInterval(Collider fromCollider)
  {
    if (this.hitIntervalList.IsNullOrEmpty<StageObject.HitIntervalStatus>())
      return false;
    int index = 0;
    for (int count = this.hitIntervalList.Count; index < count; ++index)
    {
      if (Object.op_Equality((Object) this.hitIntervalList[index].fromCollider, (Object) fromCollider))
      {
        if ((double) this.hitIntervalList[index].hitIntervalTimer > 0.0)
          return true;
        this.hitIntervalList[index].enable = false;
        return false;
      }
    }
    return false;
  }

  public void SetHitIntervalStatus(Collider fromCollider, float hitInterval)
  {
    int index = 0;
    for (int count = this.hitIntervalList.Count; index < count; ++index)
    {
      if (Object.op_Equality((Object) this.hitIntervalList[index].fromCollider, (Object) fromCollider))
        return;
    }
    this.hitIntervalList.Add(new StageObject.HitIntervalStatus(fromCollider, hitInterval));
  }

  public virtual void OnRecvSetCoopMode(Coop_Model_ObjectCoopInfo model, CoopPacket packet)
  {
  }

  public enum OBJECT_TYPE
  {
    STAGE_OBJECT,
    CHARACTER,
    PLAYER,
    ENEMY,
    SELF,
    DECOY,
    WAVE_TARGET,
  }

  public class AttackedContinuationStatus
  {
    public AttackContinuationInfo attackInfo;
    public Collider fromCollider;
    public StageObject fromObject;
    public float hitTime;
    public float hitStartTime;
  }

  public class HitIntervalStatus
  {
    public Collider fromCollider;
    public float hitInterval;
    public float hitIntervalTimer;
    public bool enable;

    public HitIntervalStatus(Collider fromCollider, float hitInterval)
    {
      this.fromCollider = fromCollider;
      this.hitInterval = hitInterval;
      this.hitIntervalTimer = hitInterval;
      this.enable = true;
    }
  }

  [Serializable]
  public class StampInfo
  {
    [Tooltip("カメラ揺れ大きさ")]
    public float shakeCameraPercent;
    [Tooltip("カメラ揺れ周期（0で共通設定")]
    public float shakeCycleTime;
    [Tooltip("足踏みエフェクト名")]
    public string effectName;
    [Tooltip("足踏みエフェクトスケール")]
    public float effectScale = 1f;
    [Tooltip("足踏みSEID")]
    public int seID;
  }

  public enum COOP_MODE_TYPE
  {
    NONE,
    ORIGINAL,
    MIRROR,
    PUPPET,
  }

  [Flags]
  public enum HIT_OFF_FLAG
  {
    NONE = 0,
    FORCE = 1,
    OPEN_MENU = 2,
    INVICIBLE = 4,
    DEAD = 8,
    LOAD = 16, // 0x00000010
    INITIALIZE = 32, // 0x00000020
    BATTLE_START = 64, // 0x00000040
    DEAD_STANDUP = 128, // 0x00000080
    PLAY_MOTION = 256, // 0x00000100
    TUTORIAL = 512, // 0x00000200
    UNLOCK_EVENT = 1024, // 0x00000400
    TEST = 2048, // 0x00000800
    GRAB = 4096, // 0x00001000
    DEAD_REVIVE = 8192, // 0x00002000
  }

  protected class HitOffTimer
  {
    public StageObject.HIT_OFF_FLAG hitOffFlag;
    public float endTime;
  }

  public enum WAITING_PACKET
  {
    CHARACTER_MOVE_VELOCITY,
    CHARACTER_UPDATE_ACTION_POSITION,
    CHARACTER_UPDATE_DIRECTION,
    PLAYER_CHARGE_RELEASE,
    PLAYER_PRAYER_END,
    PLAYER_APPLY_CHANGE_WEAPON,
    ENEMY_WARP,
    ENEMY_UPDATE_BLEED_DAMAGE,
    ENEMY_UPDATE_SHADOWSEALING,
    PLAYER_JUMP_END,
    PLAYER_SOUL_BOOST,
    EVOLVE,
    PLAYER_PAIR_SWORDS_LASER_END,
    PLAYER_ONE_HAND_SWORD_MOVE_END,
    PLAYER_GATHER_GIMMICK,
    ENEMY_UPDATE_BOMBARROW,
    NUM,
  }

  protected class WaitingPacketParam
  {
    public StageObject.WAITING_PACKET type;
    public float startTime;
    public bool keepSync;
    public float addMarginTime;
  }

  protected class NodeTable : StringKeyTable<Transform>
  {
    public StringKeyTableBase.Item GetNodeItem(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (StringKeyTableBase.Item) null;
      List<StringKeyTableBase.Item> list = this.GetList(key);
      return list == null ? (StringKeyTableBase.Item) null : this.GetItem(list, key);
    }
  }

  private class CastHitInfo
  {
    public float distance;
    public bool faceToTarget;
    public Collider collider;
    public bool enable = true;
    public bool checkCollider;
  }
}
