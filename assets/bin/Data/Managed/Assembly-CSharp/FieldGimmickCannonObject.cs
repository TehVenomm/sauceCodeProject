// Decompiled with JetBrains decompiler
// Type: FieldGimmickCannonObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FieldGimmickCannonObject : FieldGimmickObject, IFieldGimmickCannon, IFieldGimmickObject
{
  private const string NAME_FIELD_GIMMICK_CANNON = "FieldGimmickCannon";
  private const string NAME_NODE_BASE = "CMN_cannon01_Origin/Move/Root/base/rot";
  private const string NAME_NODE_CANNON = "CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot";
  private const string NAME_ANIM_SHOT_REACTION = "Reaction";
  private const float ROTATE_RAD_PER_FRAME = 0.17453292f;
  private const float RADIUS_TARGET_CANNON = 2f;
  private const float SQR_RADIUS_TARGET_CANNON = 4f;
  private const float TIME_DEFAULT_COOL_TIME = 2f;
  public const int SE_ID_TURN_TO_TARGET = 10000079;
  private Transform m_baseTrans;
  private Transform m_cannonTrans;
  private Player m_owner;
  private Enemy m_boss;
  private Transform m_targetEffect;
  private FieldGimmickCannonObject.STATE m_state = FieldGimmickCannonObject.STATE.DISABLE;
  private float m_rotateTime;
  private float m_rotateFinishTime;
  private Quaternion m_rotStart = Quaternion.identity;
  private Quaternion m_rotEnd = Quaternion.identity;
  private BallisticLineRenderer ballisticLineRenderer;
  private float m_coolTime;
  private float m_coolFinishTime;

  private Animator _animator { get; set; }

  private Transform _transform { get; set; }

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_coolFinishTime = pointData.value1;
    this.m_baseTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
    this.m_cannonTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
    this.m_baseTrans.LookAt(Vector3.zero);
    this._animator = ((Component) this).gameObject.GetComponentInChildren<Animator>();
    if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
    if (!Object.op_Equality((Object) this.ballisticLineRenderer, (Object) null))
      return;
    this.ballisticLineRenderer = ((Component) this).gameObject.AddComponent<BallisticLineRenderer>();
  }

  public bool IsUsing() => Object.op_Inequality((Object) this.m_owner, (Object) null);

  public bool IsAbleToUse()
  {
    return Object.op_Equality((Object) this.m_owner, (Object) null) && this.m_state == FieldGimmickCannonObject.STATE.NONE;
  }

  public bool IsAbleToShot()
  {
    return this.IsUsing() && this.m_state == FieldGimmickCannonObject.STATE.READY;
  }

  public bool IsCooling() => this.m_state == FieldGimmickCannonObject.STATE.COOLTIME;

  public bool IsAimCamera() => true;

  public override float GetTargetRadius() => 2f;

  public override float GetTargetSqrRadius() => 4f;

  private void SetState(FieldGimmickCannonObject.STATE state) => this.m_state = state;

  public void OnLeave()
  {
    this.m_owner = (Player) null;
    this.ballisticLineRenderer.SetVisible(false);
    this.SetState(FieldGimmickCannonObject.STATE.NONE);
  }

  public void OnBoard(Player player)
  {
    this.m_owner = player;
    AttackInfo attackHitInfo = this.GetAttackHitInfo(player.GetCurrentWeaponElement());
    if (attackHitInfo != null)
      this.ballisticLineRenderer.SetBulletData(attackHitInfo.bulletData);
    this.SetState(FieldGimmickCannonObject.STATE.STANDBY);
  }

  public void SetStateReady()
  {
    if (this.m_owner is Self)
      this.ballisticLineRenderer.SetVisible(true);
    this.SetState(FieldGimmickCannonObject.STATE.READY);
  }

  public void SetStateRotate() => this.SetState(FieldGimmickCannonObject.STATE.ROTATE);

  public void SetStateCooltime()
  {
    this.m_coolTime = 2f;
    if ((double) this.m_coolFinishTime > 0.0)
      this.m_coolTime = this.m_coolFinishTime;
    this.SetState(FieldGimmickCannonObject.STATE.COOLTIME);
  }

  private bool IsReadyForShot() => this.m_state == FieldGimmickCannonObject.STATE.READY;

  private bool IsRemainCooltime() => (double) this.m_coolTime > 0.0;

  protected override void Awake()
  {
    this._transform = ((Component) this).transform;
    Utility.SetLayerWithChildren(((Component) this).transform, 19);
  }

  private void Update()
  {
    switch (this.m_state)
    {
      case FieldGimmickCannonObject.STATE.NONE:
        if (Object.op_Equality((Object) this.m_boss, (Object) null) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
          this.m_boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
        if (!this.m_boss.IsValidShield())
        {
          this.SetState(FieldGimmickCannonObject.STATE.DISABLE);
          break;
        }
        break;
      case FieldGimmickCannonObject.STATE.STANDBY:
        if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && !Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
        {
          Vector3 position = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
          position.y = 0.0f;
          Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
          Vector3 normalized = ((Vector3) ref vector3).normalized;
          float num = Vector3.Dot(this.m_baseTrans.forward, normalized);
          this.m_rotateTime = 0.0f;
          this.m_rotateFinishTime = (float) ((double) Mathf.Acos(num) / 0.17453292012214661 * (1.0 / (double) Application.targetFrameRate));
          this.m_rotStart = Quaternion.LookRotation(this.m_baseTrans.forward);
          this.m_rotEnd = Quaternion.LookRotation(normalized);
          if ((double) num >= 1.0)
          {
            this.SetStateReady();
            break;
          }
          this.SetStateRotate();
          SoundManager.PlayOneShotSE(10000079, this._transform.position);
          break;
        }
        break;
      case FieldGimmickCannonObject.STATE.ROTATE:
        this.m_rotateTime += Time.deltaTime;
        if ((double) this.m_rotateFinishTime <= 0.0)
        {
          this.SetStateReady();
          break;
        }
        float num1 = Mathf.Clamp(this.m_rotateTime / this.m_rotateFinishTime, 0.0f, 1f);
        if ((double) num1 >= 1.0)
        {
          this.SetStateReady();
          break;
        }
        this.m_baseTrans.localRotation = Quaternion.Lerp(this.m_rotStart, this.m_rotEnd, num1);
        break;
      case FieldGimmickCannonObject.STATE.READY:
        if (this.IsRemainCooltime())
        {
          this.SetState(FieldGimmickCannonObject.STATE.COOLTIME);
          break;
        }
        break;
      case FieldGimmickCannonObject.STATE.COOLTIME:
        if ((double) this.m_coolTime <= 0.0)
        {
          this.SetStateReady();
          break;
        }
        break;
      case FieldGimmickCannonObject.STATE.DISABLE:
        if (Object.op_Equality((Object) this.m_boss, (Object) null) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
          this.m_boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
        if (this.m_boss.IsValidShield())
        {
          this.OnLeave();
          break;
        }
        break;
    }
    if ((double) this.m_coolTime < 0.0)
      return;
    this.m_coolTime -= Time.deltaTime;
  }

  private void LateUpdate()
  {
    switch (this.m_state)
    {
      case FieldGimmickCannonObject.STATE.READY:
      case FieldGimmickCannonObject.STATE.COOLTIME:
        this.UpdateCannonRotation();
        this.UpdateCannonAngle();
        break;
    }
  }

  public override void UpdateTargetMarker(bool isNear)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (((!Object.op_Inequality((Object) boss, (Object) null) || !boss.IsValidShield() ? 0 : (!this.IsUsing() ? 1 : 0)) & (isNear ? 1 : 0)) != 0 && Object.op_Inequality((Object) self, (Object) null) && self.IsChangeableAction((Character.ACTION_ID) 31 /*0x1F*/))
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

  private void UpdateCannonAngle()
  {
    if (Object.op_Equality((Object) this.m_cannonTrans, (Object) null) || Object.op_Equality((Object) this.m_owner, (Object) null))
      return;
    Self owner = this.m_owner as Self;
    if (Object.op_Equality((Object) owner, (Object) null))
      return;
    this.m_cannonTrans.localRotation = Quaternion.Euler(owner.GetCannonShotEuler());
    this.ballisticLineRenderer.UpdateLine(this.m_cannonTrans.position, Quaternion.op_Multiply(this.m_cannonTrans.rotation, Vector3.forward));
  }

  private void UpdateCannonRotation()
  {
    if (Object.op_Equality((Object) this.m_baseTrans, (Object) null) || Object.op_Equality((Object) this.m_owner, (Object) null))
      return;
    this.m_baseTrans.localRotation = this.m_owner._rigidbody.rotation;
  }

  private AttackInfo GetAttackHitInfo(int weaponElement)
  {
    string attackInfoName = "cannonball_";
    switch (weaponElement)
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

  public void Shot()
  {
    if (!this.IsReadyForShot())
      return;
    if (Object.op_Inequality((Object) this._animator, (Object) null))
      this._animator.Play("Reaction", 0, 0.0f);
    AttackInfo attackHitInfo = this.GetAttackHitInfo(this.m_owner.GetCurrentWeaponElement());
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
    this.SetStateCooltime();
  }

  public Transform GetCannonTransform() => this.m_cannonTrans;

  public Transform GetBaseTransform() => this.m_baseTrans;

  public Vector3 GetBaseTransformForward()
  {
    return Object.op_Equality((Object) this.m_baseTrans, (Object) null) ? Vector3.forward : this.m_baseTrans.forward;
  }

  public Vector3 GetPosition() => this._transform.position;

  public void ApplyCannonVector(Vector3 cannonVec)
  {
    Vector3 vector3 = cannonVec;
    vector3.y = 0.0f;
    this.m_baseTrans.rotation = Quaternion.LookRotation(vector3);
    this.m_cannonTrans.rotation = Quaternion.LookRotation(cannonVec);
  }

  public override string GetObjectName() => "FieldGimmickCannon";

  private enum STATE
  {
    NONE,
    STANDBY,
    ROTATE,
    READY,
    COOLTIME,
    DISABLE,
  }
}
