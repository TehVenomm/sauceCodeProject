// Decompiled with JetBrains decompiler
// Type: AttackFunnelBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackFunnelBit : MonoBehaviour, IBulletObservable
{
  public const float AIM_RAD_MIN = 0.00174532924f;
  public const string ANIM_STATE_BREAK = "BREAK";
  public const string ANIM_STATE_DISAPPEAR = "END";
  private BulletData.BulletFunnel m_funnelData;
  private StageObject m_attacker;
  private AttackInfo m_atkInfo;
  private Transform m_cachedTransform;
  private StageObject m_targetObject;
  private GameObject m_effectObj;
  private string m_landHitEffectName = string.Empty;
  private float m_aimAngleSpeed;
  private float m_moveSpeed;
  private float m_aliveTimer;
  private bool m_isDeleted;
  private AttackFunnelBit.Function m_func;
  private int m_state;
  private int m_effectDeleteAnimHash;
  private Animator m_effectAnimator;
  private float m_attackIntervalTimer;
  private AtkAttribute m_exAtk;
  private Player.ATTACK_MODE m_attackMode;
  private SkillInfo.SkillParam m_skillParam;
  private CapsuleCollider m_capsuleCollider;
  private float m_radius;
  private TargetPoint m_targetPoint;
  public string m_finalAtkInfoName;
  private int observedID;
  private List<IBulletObserver> bulletObserverList = new List<IBulletObserver>();

  public TargetPoint targetPoint => this.m_targetPoint;

  public virtual void Initialize(
    StageObject attacker,
    AttackInfo atkInfo,
    StageObject targetObj,
    Transform launchTrans,
    Vector3 offsetPos,
    Quaternion offsetRot)
  {
    this.m_attacker = attacker;
    this.m_atkInfo = atkInfo;
    if (atkInfo is AttackHitInfo attackHitInfo)
      attackHitInfo.enableIdentityCheck = false;
    BulletData bulletData = atkInfo.bulletData;
    this.m_landHitEffectName = bulletData.data.landHiteffectName;
    this.m_aliveTimer = bulletData.data.appearTime;
    this.m_moveSpeed = bulletData.data.speed;
    this.SetColliderByRadius(bulletData.data.radius);
    BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
    this.m_aimAngleSpeed = dataFunnel.lookAtAngle * ((float) Math.PI / 180f);
    this.m_funnelData = dataFunnel;
    this.m_isDeleted = false;
    this.m_finalAtkInfoName = dataFunnel.finalAtkInfoName;
    this.m_cachedTransform = ((Component) this).transform;
    this.m_cachedTransform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
    this.m_cachedTransform.position = Vector3.op_Addition(launchTrans.position, Quaternion.op_Multiply(launchTrans.rotation, offsetPos));
    this.m_cachedTransform.rotation = Quaternion.op_Multiply(launchTrans.rotation, offsetRot);
    this.m_cachedTransform.localScale = bulletData.data.timeStartScale;
    Transform effect = EffectManager.GetEffect(bulletData.data.effectName, ((Component) this).transform);
    if (Object.op_Inequality((Object) effect, (Object) null))
    {
      effect.localPosition = bulletData.data.dispOffset;
      effect.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
      effect.localScale = Vector3.one;
      this.m_effectObj = ((Component) effect).gameObject;
      this.m_effectAnimator = this.m_effectObj.GetComponent<Animator>();
    }
    this.RegisterObserver();
    if (Object.op_Inequality((Object) targetObj, (Object) null))
      this.RequestMain(targetObj);
    else
      this.RequestSearch();
  }

  private void SetColliderByRadius(float _radius)
  {
    if ((double) _radius <= 0.0)
      return;
    this.m_radius = _radius;
    this.m_capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    this.m_capsuleCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
    this.m_capsuleCollider.direction = 2;
    ((Collider) this.m_capsuleCollider).isTrigger = true;
    this.m_capsuleCollider.radius = this.m_radius;
    this.m_capsuleCollider.height = this.m_radius * 2f;
    Utility.SetLayerWithChildren(((Component) this).transform, 31 /*0x1F*/);
    this.SetTargetPoint();
  }

  private void SetTargetPoint()
  {
    this.m_targetPoint = ((Component) this).gameObject.AddComponent<TargetPoint>();
    this.m_targetPoint.isAimEnable = false;
    this.m_targetPoint.isTargetEnable = false;
  }

  private void Update()
  {
    switch (this.m_func)
    {
      case AttackFunnelBit.Function.Main:
        this.FuncMain();
        break;
      case AttackFunnelBit.Function.Search:
        this.FuncSearch();
        break;
      case AttackFunnelBit.Function.Delete:
        this.FuncDelete();
        break;
    }
  }

  private void RequestMain(StageObject targetObj)
  {
    this.m_targetObject = targetObj;
    this.RequestFunction(AttackFunnelBit.Function.Main);
  }

  private void FuncMain()
  {
    if (this.IsDeleted)
      return;
    this.m_aliveTimer -= Time.deltaTime;
    if ((double) this.m_aliveTimer <= 0.0)
      this.RequestDestroy(false);
    else if (Object.op_Equality((Object) this.m_targetObject, (Object) null))
    {
      this.RequestDestroy(false);
    }
    else
    {
      switch (this.m_state)
      {
        case 1:
          Vector3 position = ((Component) this.m_targetObject).transform.position;
          position.y = this.GetFloatingHeight();
          this.LookAtTarget(position);
          Vector3 vector3 = Vector3.op_Addition(this.m_cachedTransform.position, Vector3.op_Multiply(this.m_cachedTransform.forward, this.m_moveSpeed * Time.deltaTime));
          this.m_cachedTransform.position = vector3;
          if ((double) Vector3.Distance(position, vector3) > (double) this.GetAttackStartRange())
            break;
          this.m_attackIntervalTimer = this.m_funnelData.attackInterval;
          this.ForwardState();
          break;
        case 2:
          this.RotateAroundTarget();
          this.LookAtTarget(((Component) this.m_targetObject).transform.position);
          this.m_attackIntervalTimer -= Time.deltaTime;
          if ((double) this.m_attackIntervalTimer > 0.0)
            break;
          this.m_attackIntervalTimer = this.m_funnelData.attackInterval;
          if (this.CheckTargetDead())
          {
            this.RequestDestroy(false);
            break;
          }
          this.CreateBullet();
          break;
      }
    }
  }

  private void RotateAroundTarget()
  {
    BulletData.BulletFunnel funnelData = this.m_funnelData;
    float floatingHeight = this.GetFloatingHeight();
    Vector3 position1 = ((Component) this.m_targetObject).transform.position;
    position1.y = floatingHeight;
    Vector3 position2 = this.m_cachedTransform.position;
    position2.y = floatingHeight;
    Vector3 vector3_1 = Vector3.op_Subtraction(position2, position1);
    ((Vector3) ref vector3_1).Normalize();
    Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, this.GetAttackStartRange());
    Vector3 vector3_3 = Quaternion.op_Multiply(Quaternion.AngleAxis(funnelData.rotateAngle, Vector3.up), vector3_2);
    Vector3 vector3_4 = Vector3.op_Subtraction(Vector3.op_Addition(position1, vector3_3), this.m_cachedTransform.position);
    this.m_cachedTransform.position = Vector3.op_Addition(position2, Vector3.op_Multiply(vector3_4, Time.deltaTime));
  }

  private void LookAtTarget(Vector3 targetPos)
  {
    Vector3 forward = this.m_cachedTransform.forward;
    Vector3 position = this.m_cachedTransform.position;
    Vector3 vector3 = Vector3.op_Subtraction(targetPos, position);
    ((Vector3) ref vector3).Normalize();
    float num1 = this.m_aimAngleSpeed * Time.deltaTime;
    double num2 = (double) Vector3.Dot(forward, vector3);
    float num3 = Mathf.Acos((float) num2);
    if (num2 < 1.0 && (double) num1 < (double) num3 && (double) num3 >= 0.001745329238474369)
    {
      float num4 = num1 / num3;
      this.m_cachedTransform.rotation = (double) num4 < 1.0 ? Quaternion.Slerp(Quaternion.LookRotation(forward), Quaternion.LookRotation(vector3), num4) : Quaternion.LookRotation(vector3);
    }
    else
      this.m_cachedTransform.rotation = Quaternion.LookRotation(vector3);
  }

  private AnimEventShot CreateBullet()
  {
    BulletData bulletData = this.m_atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return (AnimEventShot) null;
    BulletData.BulletFunnel dataFunnel = bulletData.dataFunnel;
    return dataFunnel == null ? (AnimEventShot) null : this.CreateShot(dataFunnel.bitBullet, this.m_atkInfo);
  }

  private void RequestSearch() => this.RequestFunction(AttackFunnelBit.Function.Search);

  private void FuncSearch()
  {
    if (this.IsDeleted)
      return;
    this.m_aliveTimer -= Time.deltaTime;
    if ((double) this.m_aliveTimer <= 0.0)
    {
      this.RequestDestroy(false);
    }
    else
    {
      if (this.m_state != 1)
        return;
      Transform cachedTransform = this.m_cachedTransform;
      Vector3 vector3 = Vector3.op_Addition(cachedTransform.position, Vector3.op_Multiply(cachedTransform.forward, this.m_moveSpeed * Time.deltaTime));
      cachedTransform.position = vector3;
      float searchRange = this.m_funnelData.searchRange;
      if ((double) searchRange <= 0.0 || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
        return;
      Vector2 zero = Vector2.zero;
      zero.x = vector3.x;
      zero.y = vector3.z;
      StageObject targetObj = this.SearchNearestTarget(zero, searchRange);
      if (!Object.op_Inequality((Object) targetObj, (Object) null))
        return;
      this.RequestMain(targetObj);
    }
  }

  public void RequestDestroy(bool isPlayFallEffect = true)
  {
    if (this.m_func == AttackFunnelBit.Function.Delete || this.IsDeleted)
      return;
    this.RequestFunction(AttackFunnelBit.Function.Delete);
    this.CreateEndBullet();
    if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
    {
      this.Destroy();
    }
    else
    {
      this.m_effectDeleteAnimHash = Animator.StringToHash(isPlayFallEffect ? "BREAK" : "END");
      if (this.m_effectAnimator.HasState(0, this.m_effectDeleteAnimHash))
      {
        this.m_effectAnimator.Play(this.m_effectDeleteAnimHash, 0, 0.0f);
        this.m_effectAnimator.Update(0.0f);
      }
      else
      {
        Debug.LogWarning((object) "Not found delete animation!!");
        this.Destroy();
      }
    }
  }

  private void FuncDelete()
  {
    switch (this.m_state)
    {
      case 1:
        if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
        {
          this.ForwardState();
          break;
        }
        AnimatorStateInfo animatorStateInfo = this.m_effectAnimator.GetCurrentAnimatorStateInfo(0);
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
          break;
        this.ForwardState();
        break;
      case 2:
        this.Destroy();
        this.ForwardState();
        break;
    }
  }

  private void Destroy()
  {
    if (this.IsDeleted)
      return;
    this.m_isDeleted = true;
    if (!string.IsNullOrEmpty(this.m_landHitEffectName))
    {
      Transform effect = EffectManager.GetEffect(this.m_landHitEffectName);
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        effect.position = this.m_cachedTransform.position;
        effect.rotation = this.m_cachedTransform.rotation;
      }
    }
    if (Object.op_Inequality((Object) this.m_attacker, (Object) null))
    {
      Enemy attacker = this.m_attacker as Enemy;
      if (Object.op_Inequality((Object) attacker, (Object) null))
        attacker.OnDestroyFunnel(this);
      this.m_attacker = (StageObject) null;
    }
    this.NotifyDestroy();
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.m_effectObj, (Object) null))
      return;
    EffectManager.ReleaseEffect(this.m_effectObj);
    this.m_effectObj = (GameObject) null;
  }

  private AnimEventShot CreateEndBullet()
  {
    if (string.IsNullOrEmpty(this.m_finalAtkInfoName))
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    AttackInfo attackInfo = this.m_attacker.FindAttackInfo(this.m_finalAtkInfoName);
    if (attackInfo == null)
      return (AnimEventShot) null;
    BulletData bulletData = attackInfo.bulletData;
    return Object.op_Equality((Object) bulletData, (Object) null) ? (AnimEventShot) null : this.CreateShot(bulletData, attackInfo);
  }

  private AnimEventShot CreateShot(BulletData bltData, AttackInfo atkInfo)
  {
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
    {
      this.RequestDestroy();
      return (AnimEventShot) null;
    }
    Quaternion rotation = this.m_cachedTransform.rotation;
    Vector3 pos = Vector3.op_Addition(this.m_cachedTransform.position, Quaternion.op_Multiply(rotation, this.m_funnelData.offsetPosition));
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(bltData, this.m_attacker, atkInfo, pos, rotation, this.m_exAtk, this.m_attackMode, this.m_skillParam);
    if (!Object.op_Equality((Object) externalBulletData, (Object) null))
      return externalBulletData;
    Log.Error("Failed to create AnimEventShot for Funnel!!");
    return (AnimEventShot) null;
  }

  protected virtual bool CheckTargetDead()
  {
    Player targetObject = this.m_targetObject as Player;
    return Object.op_Equality((Object) targetObject, (Object) null) || targetObject.isDead;
  }

  protected virtual StageObject SearchNearestTarget(Vector2 bulletPos, float searchRadius)
  {
    float num1 = float.MaxValue;
    float radius = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.radius;
    StageObject stageObject = (StageObject) null;
    int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count;
    for (int index = 0; index < count; ++index)
    {
      Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] as Player;
      if (!player.isDead)
      {
        float num2 = Vector2.Distance(player.positionXZ, bulletPos);
        if ((double) num2 <= (double) radius + (double) searchRadius && (double) num2 <= (double) num1)
        {
          num1 = num2;
          stageObject = (StageObject) player;
        }
      }
    }
    return stageObject;
  }

  protected virtual float GetAttackStartRange() => this.m_funnelData.attackRange;

  protected virtual float GetFloatingHeight() => this.m_funnelData.floatingHeight;

  protected void SetAttackMode(Player.ATTACK_MODE attackMode) => this.m_attackMode = attackMode;

  protected void SetExAtk(AtkAttribute atk) => this.m_exAtk = atk;

  protected void SetSkillParam(SkillInfo.SkillParam param) => this.m_skillParam = param;

  protected StageObject TargetObject => this.m_targetObject;

  private void RequestFunction(AttackFunnelBit.Function func)
  {
    this.m_func = func;
    this.SetState(1);
  }

  private void SetState(int state) => this.m_state = state;

  private void ForwardState() => ++this.m_state;

  private void BackState()
  {
    if (this.m_state <= 0)
      return;
    --this.m_state;
  }

  public string AttackInfoName => this.m_atkInfo.name;

  public bool IsDeleted => this.m_isDeleted;

  private void OnTriggerEnter(Collider collider)
  {
    if (this.m_isDeleted || ((Component) collider).gameObject.layer != 14)
      return;
    this.NotifyBroken(true);
  }

  private void OnTriggerStay(Collider collider)
  {
    if (this.m_isDeleted || ((Component) collider).gameObject.layer != 14)
      return;
    this.NotifyBroken(true);
  }

  public int GetObservedID() => this.observedID;

  public void SetObservedID(int id) => this.observedID = id;

  public void RegisterObserver()
  {
    if (this.bulletObserverList.Contains((IBulletObserver) this.m_attacker))
      return;
    this.bulletObserverList.Add((IBulletObserver) this.m_attacker);
    this.SetObservedID(this.m_attacker.GetObservedID());
    this.m_attacker.RegisterObservable((IBulletObservable) this);
  }

  public void NotifyBroken(bool isSendOnlyOriginal = true)
  {
    for (int index = 0; index < this.bulletObserverList.Count; ++index)
      this.bulletObserverList[index].OnBreak(this.observedID, isSendOnlyOriginal);
  }

  public void NotifyDestroy()
  {
    for (int index = 0; index < this.bulletObserverList.Count; ++index)
      this.bulletObserverList[index].OnBulletDestroy(this.observedID);
  }

  public void ForceBreak() => this.RequestDestroy();

  public enum Function
  {
    None,
    Main,
    Search,
    Delete,
  }
}
