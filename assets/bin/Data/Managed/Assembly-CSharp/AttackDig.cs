// Decompiled with JetBrains decompiler
// Type: AttackDig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AttackDig : MonoBehaviour
{
  private const float AIM_RAD_MIN = 0.00174532924f;
  public const string ANIM_STATE_BREAK = "BREAK";
  private const string ANIM_STATE_END = "END";
  private BulletData.BulletDig m_digData;
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
  private AttackDig.Function m_func;
  private AttackDig.State m_state;
  private int m_effectDeleteAnimHash;
  private Animator m_effectAnimator;
  private float m_attackTimer;
  private bool m_isCreatedBullet;

  public string AttackInfoName => this.m_atkInfo.name;

  public bool IsDeleted => this.m_isDeleted;

  public void Initialize(
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
    BulletData.BulletDig dataDig = bulletData.dataDig;
    this.m_aimAngleSpeed = dataDig.lookAtAngle * ((float) Math.PI / 180f);
    this.m_digData = dataDig;
    this.m_isDeleted = false;
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
      this.m_targetObject = targetObj;
    }
    this.RequestMain();
  }

  private void Update()
  {
    switch (this.m_func)
    {
      case AttackDig.Function.MAIN:
        this.FuncMain();
        break;
      case AttackDig.Function.DELETE:
        this.FuncDelete();
        break;
    }
  }

  private void RequestMain() => this.RequestFunction(AttackDig.Function.MAIN);

  private void FuncMain()
  {
    if (this.m_isDeleted)
      return;
    this.m_aliveTimer -= Time.deltaTime;
    if ((double) this.m_aliveTimer <= 0.0)
      this.RequestDestroy(false);
    else if (Object.op_Equality((Object) this.m_targetObject, (Object) null))
      this.RequestDestroy(false);
    else if (this.m_isCreatedBullet)
    {
      this.RequestDestroy(false);
    }
    else
    {
      Vector3 position = ((Component) this.m_targetObject).transform.position;
      position.y = 0.0f;
      switch (this.m_state)
      {
        case AttackDig.State.TRACKING:
          this.LookAtTarget(position);
          Vector3 vector3 = Vector3.op_Addition(this.m_cachedTransform.position, Vector3.op_Multiply(Vector3.op_Multiply(this.m_cachedTransform.forward, this.m_moveSpeed), Time.deltaTime));
          vector3.y = this.m_digData.floatingHeight;
          this.m_cachedTransform.position = vector3;
          position.y = 0.0f;
          vector3.y = 0.0f;
          if ((double) Vector3.Distance(position, vector3) > (double) this.m_digData.attackRange)
            break;
          this.m_attackTimer = this.m_digData.attackDelay;
          this.ForwardState();
          break;
        case AttackDig.State.ATTACK:
          this.m_attackTimer -= Time.deltaTime;
          if ((double) this.m_attackTimer > 0.0)
            break;
          Player targetObject = this.m_targetObject as Player;
          if (Object.op_Equality((Object) targetObject, (Object) null) || targetObject.isDead)
          {
            this.RequestDestroy(false);
            break;
          }
          this.CreateBullet();
          break;
      }
    }
  }

  private AnimEventShot CreateBullet()
  {
    BulletData bulletData = this.m_atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return (AnimEventShot) null;
    BulletData.BulletDig dataDig = bulletData.dataDig;
    if (dataDig == null)
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    Quaternion rotation = this.m_cachedTransform.rotation;
    Vector3 position = this.m_cachedTransform.position;
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(dataDig.flyOutBullet, this.m_attacker, this.m_atkInfo, position, rotation);
    if (Object.op_Equality((Object) externalBulletData, (Object) null))
    {
      Log.Error("Failed to create AnimEventShot for Dig!!");
      return (AnimEventShot) null;
    }
    this.m_isCreatedBullet = true;
    return externalBulletData;
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
    if (num2 >= 1.0 || (double) num1 >= (double) num3 || (double) num3 < 0.001745329238474369)
      return;
    float num4 = num1 / num3;
    this.m_cachedTransform.rotation = (double) num4 < 1.0 ? Quaternion.Slerp(Quaternion.LookRotation(forward), Quaternion.LookRotation(vector3), num4) : Quaternion.LookRotation(vector3);
  }

  public void RequestDestroy(bool isPlayBreakEffect = true)
  {
    if (this.m_func == AttackDig.Function.DELETE || this.m_isDeleted)
      return;
    this.RequestFunction(AttackDig.Function.DELETE);
    if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
    {
      this.Destroy();
    }
    else
    {
      this.m_effectDeleteAnimHash = !isPlayBreakEffect ? Animator.StringToHash("END") : Animator.StringToHash("BREAK");
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
      case AttackDig.State.TRACKING:
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
      case AttackDig.State.ATTACK:
        this.Destroy();
        this.ForwardState();
        break;
    }
  }

  private void Destroy()
  {
    if (this.m_isDeleted)
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
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.m_effectObj, (Object) null))
      return;
    EffectManager.ReleaseEffect(this.m_effectObj);
    this.m_effectObj = (GameObject) null;
  }

  private void RequestFunction(AttackDig.Function func)
  {
    this.m_func = func;
    this.SetState(AttackDig.State.TRACKING);
  }

  private void SetState(AttackDig.State state) => this.m_state = state;

  private void ForwardState() => ++this.m_state;

  private void BackState()
  {
    if (this.m_state <= AttackDig.State.NONE)
      return;
    --this.m_state;
  }

  private enum Function
  {
    NONE,
    MAIN,
    DELETE,
  }

  private enum State
  {
    NONE,
    TRACKING,
    ATTACK,
  }
}
