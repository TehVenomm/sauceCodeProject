// Decompiled with JetBrains decompiler
// Type: FieldGimmickCannonBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FieldGimmickCannonBase : FieldGimmickObject, IFieldGimmickCannon, IFieldGimmickObject
{
  protected const string NAME_NODE_BASE = "CMN_cannon01_Origin/Move/Root/base/rot";
  protected const string NAME_NODE_CANNON = "CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot";
  protected const string NAME_ANIM_SHOT_REACTION = "Reaction";
  protected const float ROTATE_RAD_PER_FRAME = 0.17453292f;
  private const float RADIUS_TARGET_CANNON = 2f;
  private const float TIME_DEFAULT_COOL_TIME = 2f;
  public const int SE_ID_TURN_TO_TARGET = 10000079;
  protected Transform m_baseTrans;
  protected Transform m_cannonTrans;
  protected float m_coolTime;
  private float m_coolTimeCounter;
  protected float m_rotateTime;
  protected float m_rotateTimeCounter;
  private FieldGimmickCannonBase.STATE m_state;
  protected Player m_owner;
  private Enemy m_boss;
  protected Quaternion m_rotStart = Quaternion.identity;
  protected Quaternion m_rotEnd = Quaternion.identity;
  private Transform m_targetEffect;

  protected Animator _animator { get; set; }

  protected Transform _transform { get; set; }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_coolTime = pointData.value1;
    this._animator = ((Component) this).gameObject.GetComponentInChildren<Animator>();
  }

  public bool IsUsing() => Object.op_Inequality((Object) this.m_owner, (Object) null);

  public bool IsAbleToUse()
  {
    return Object.op_Equality((Object) this.m_owner, (Object) null) && this.m_state == FieldGimmickCannonBase.STATE.NONE;
  }

  public bool IsAbleToShot()
  {
    return this.IsUsing() && this.m_state == FieldGimmickCannonBase.STATE.READY;
  }

  public bool IsCooling() => this.m_state == FieldGimmickCannonBase.STATE.COOLTIME;

  protected bool IsRemainCoolTime() => (double) this.m_coolTimeCounter > 0.0;

  protected virtual bool IsReadyForShot() => this.m_state == FieldGimmickCannonBase.STATE.READY;

  public virtual bool IsAimCamera() => true;

  public virtual void OnBoard(Player player)
  {
    this.m_owner = player;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      this.m_boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    this.SetState(FieldGimmickCannonBase.STATE.STANDBY);
  }

  public virtual void OnLeave()
  {
    this.m_owner = (Player) null;
    this.SetState(FieldGimmickCannonBase.STATE.NONE);
  }

  protected override void Awake()
  {
    this._transform = ((Component) this).transform;
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
  }

  private void Update()
  {
    switch (this.m_state)
    {
      case FieldGimmickCannonBase.STATE.NONE:
        this.UpdateStateNone();
        break;
      case FieldGimmickCannonBase.STATE.STANDBY:
        this.UpdateStateStandBy();
        break;
      case FieldGimmickCannonBase.STATE.ROTATE:
        this.UpdateStateRotate();
        break;
      case FieldGimmickCannonBase.STATE.READY:
        this.UpdateStateReady();
        break;
      case FieldGimmickCannonBase.STATE.COOLTIME:
        this.UpdateStateCooltime();
        break;
      case FieldGimmickCannonBase.STATE.DISABLE:
        this.UpdateStateDisable();
        break;
    }
    this.DecreaseCoolTime();
  }

  private void LateUpdate()
  {
    switch (this.m_state)
    {
      case FieldGimmickCannonBase.STATE.READY:
      case FieldGimmickCannonBase.STATE.COOLTIME:
        this.UpdateCannonRotation();
        this.UpdateCannonAngle();
        break;
    }
  }

  protected virtual void UpdateStateNone()
  {
  }

  protected virtual void UpdateStateStandBy()
  {
    if (Object.op_Equality((Object) this.m_boss, (Object) null) || Object.op_Equality((Object) this.m_baseTrans, (Object) null))
      return;
    Vector3 position = this.m_boss._position;
    position.y = 0.0f;
    Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
    Vector3 normalized = ((Vector3) ref vector3).normalized;
    Vector3 forward = this.m_baseTrans.forward;
    float num = Vector3.Dot(forward, normalized);
    this.m_rotateTimeCounter = 0.0f;
    this.m_rotateTime = (float) ((double) Mathf.Acos(num) / 0.17453292012214661 * (1.0 / (double) Application.targetFrameRate));
    this.m_rotStart = this.m_baseTrans.localRotation;
    this.m_rotEnd = Quaternion.op_Multiply(this.m_rotStart, Quaternion.FromToRotation(forward, normalized));
    if ((double) num >= 1.0)
    {
      this.SetState(FieldGimmickCannonBase.STATE.READY);
    }
    else
    {
      this.SetState(FieldGimmickCannonBase.STATE.ROTATE);
      SoundManager.PlayOneShotSE(10000079, this._transform.position);
    }
  }

  protected virtual void UpdateStateRotate()
  {
    this.m_rotateTimeCounter += Time.deltaTime;
    if ((double) this.m_rotateTime <= 0.0)
    {
      this.SetState(FieldGimmickCannonBase.STATE.READY);
    }
    else
    {
      float num = Mathf.Clamp01(this.m_rotateTimeCounter / this.m_rotateTime);
      if ((double) num >= 1.0)
        this.SetState(FieldGimmickCannonBase.STATE.READY);
      else
        this.m_baseTrans.localRotation = Quaternion.Lerp(this.m_rotStart, this.m_rotEnd, num);
    }
  }

  protected virtual void UpdateStateReady()
  {
    if (!this.IsRemainCoolTime())
      return;
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  protected virtual void UpdateStateCooltime()
  {
    if ((double) this.m_coolTimeCounter > 0.0)
      return;
    this.SetState(FieldGimmickCannonBase.STATE.READY);
  }

  protected virtual void UpdateStateDisable()
  {
  }

  protected void StartCoolTime()
  {
    this.m_coolTimeCounter = 2f;
    if ((double) this.m_coolTime <= 0.0)
      return;
    this.m_coolTimeCounter = this.m_coolTime;
  }

  private void DecreaseCoolTime()
  {
    if ((double) this.m_coolTimeCounter < 0.0)
      return;
    this.m_coolTimeCounter -= Time.deltaTime;
  }

  private void UpdateCannonRotation()
  {
    if (Object.op_Equality((Object) this.m_baseTrans, (Object) null) || Object.op_Equality((Object) this.m_owner, (Object) null))
      return;
    this.m_baseTrans.localRotation = Quaternion.op_Multiply(this.m_owner._rigidbody.rotation, Quaternion.Inverse(this._transform.rotation));
  }

  private void UpdateCannonAngle()
  {
    if (Object.op_Equality((Object) this.m_baseTrans, (Object) null) || Object.op_Equality((Object) this.m_owner, (Object) null))
      return;
    Self owner = this.m_owner as Self;
    if (Object.op_Equality((Object) owner, (Object) null))
      return;
    this.m_cannonTrans.localRotation = Quaternion.Euler(owner.GetCannonShotEuler());
  }

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (((!Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null) ? 0 : (!this.IsUsing() ? 1 : 0)) & (isNear ? 1 : 0)) != 0 && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction((Character.ACTION_ID) 31 /*0x1F*/))
    {
      if (Object.op_Equality((Object) this.m_targetEffect, (Object) null) && !string.IsNullOrEmpty(ResourceName.GetFieldGimmickCannonTargetEffect()))
        this.m_targetEffect = EffectManager.GetEffect(ResourceName.GetFieldGimmickCannonTargetEffect(), this._transform);
      if (!Object.op_Inequality((Object) this.m_targetEffect, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
      this.m_targetEffect.Set(Vector3.op_Addition(Vector3.op_Addition(((Vector3) ref vector3).normalized, Vector3.up), this._transform.position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.m_targetEffect, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.m_targetEffect).gameObject);
    }
  }

  public virtual void Shot()
  {
    if (!this.IsReadyForShot())
      return;
    if (Object.op_Inequality((Object) this._animator, (Object) null))
      this._animator.Play("Reaction", 0, 0.0f);
    AttackInfo attackHitInfo = this.GetAttackHitInfo();
    if (attackHitInfo == null)
      return;
    new GameObject("AttackCannonball").AddComponent<AttackCannonball>().Initialize(new AttackCannonball.InitParamCannonball()
    {
      attacker = (StageObject) this.m_owner,
      atkInfo = attackHitInfo,
      launchTrans = this.m_cannonTrans,
      offsetPos = Vector3.zero,
      offsetRot = Quaternion.identity,
      shotRotation = this.m_cannonTrans.rotation
    });
    EffectManager.GetEffect("ef_btl_magibullet_shot_01", this.m_cannonTrans);
    this.StartCoolTime();
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  public void SetState(FieldGimmickCannonBase.STATE state) => this.m_state = state;

  public Vector3 GetPosition() => this._transform.position;

  public Transform GetCannonTransform() => this.m_cannonTrans;

  public Transform GetBaseTransform() => this.m_baseTrans;

  public Vector3 GetBaseTransformForward()
  {
    return Object.op_Equality((Object) this.m_baseTrans, (Object) null) ? Vector3.forward : this.m_baseTrans.forward;
  }

  protected virtual AttackInfo GetAttackHitInfo()
  {
    int num = 6;
    if (Object.op_Inequality((Object) this.m_owner, (Object) null))
      num = this.m_owner.GetCurrentWeaponElement();
    string attackInfoName = "cannonball_";
    switch (num)
    {
      case 0:
        attackInfoName += "fire";
        break;
      case 1:
        attackInfoName += "water";
        break;
      case 2:
        attackInfoName += "thunder";
        break;
      case 3:
        attackInfoName += "soil";
        break;
      case 4:
        attackInfoName += "light";
        break;
      case 5:
        attackInfoName += "dark";
        break;
      default:
        attackInfoName += "normal";
        break;
    }
    AttackInfo attackHitInfo = this.m_owner.GetAttackInfos().Find<AttackInfo>((Predicate<AttackInfo>) (info => info.name == attackInfoName));
    if (attackHitInfo != null)
      return attackHitInfo;
    Log.Error(LOG.INGAME, "Not found cannon attackInfo. name = " + attackInfoName);
    return (AttackInfo) null;
  }

  public virtual void ApplyCannonVector(Vector3 cannonVec)
  {
    Vector3 vector3 = cannonVec;
    vector3.y = 0.0f;
    this.m_baseTrans.rotation = Quaternion.LookRotation(vector3);
    this.m_cannonTrans.rotation = Quaternion.LookRotation(cannonVec);
  }

  public enum STATE
  {
    NONE,
    STANDBY,
    ROTATE,
    READY,
    COOLTIME,
    DISABLE,
  }
}
