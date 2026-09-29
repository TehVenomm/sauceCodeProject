// Decompiled with JetBrains decompiler
// Type: AttackUndead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AttackUndead : MonoBehaviour
{
  private const float AIM_RAD_MIN = 0.00174532924f;
  private const string ANIM_STATE_END = "END";
  private const float FLOATING_RATE_IN_ATTACK = 0.3f;
  private const float STOP_UNDEAD_DISTANCE = 0.05f;
  private BulletData.BulletUndead m_undeadData;
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
  private AttackUndead.Function m_func;
  private AttackUndead.State m_state;
  private int m_effectDeleteAnimHash;
  private Animator m_effectAnimator;
  private float m_attackIntervalTimer;

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
    BulletData.BulletUndead dataUndead = bulletData.dataUndead;
    this.m_aimAngleSpeed = dataUndead.lookAtAngle * ((float) Math.PI / 180f);
    this.m_undeadData = dataUndead;
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
    }
    this.m_targetObject = targetObj;
    this.RequestMain();
  }

  private void Update()
  {
    switch (this.m_func)
    {
      case AttackUndead.Function.MAIN:
        this.FuncMain();
        break;
      case AttackUndead.Function.DELETE:
        this.FuncDelete();
        break;
    }
  }

  private void RequestMain() => this.RequestFunction(AttackUndead.Function.MAIN);

  private void FuncMain()
  {
    if (this.m_isDeleted)
      return;
    this.m_aliveTimer -= Time.deltaTime;
    if ((double) this.m_aliveTimer <= 0.0)
      this.RequestDestroy();
    else if (Object.op_Equality((Object) this.m_targetObject, (Object) null))
    {
      this.RequestDestroy();
    }
    else
    {
      Vector3 position1 = ((Component) this.m_targetObject).transform.position;
      switch (this.m_state)
      {
        case AttackUndead.State.TRACKING:
          position1.y = this.m_undeadData.floatingHeight;
          Vector3 vector3_1 = Vector3.op_Subtraction(position1, this.m_cachedTransform.position);
          Vector3 vector3_2 = Vector3.op_Addition(this.m_cachedTransform.position, Vector3.op_Multiply(Vector3.op_Multiply(((Vector3) ref vector3_1).normalized, this.m_moveSpeed), Time.deltaTime));
          // ISSUE: explicit constructor call
          ((Vector3) ref vector3_2).\u002Ector(vector3_2.x, vector3_2.y + Mathf.Sin(Time.time) * this.m_undeadData.floatingCoef, vector3_2.z);
          this.m_cachedTransform.position = vector3_2;
          position1.y = 0.0f;
          vector3_2.y = 0.0f;
          if ((double) Vector3.Distance(position1, vector3_2) > (double) this.m_undeadData.attackRange)
            break;
          this.m_attackIntervalTimer = this.m_undeadData.attackInterval;
          this.ForwardState();
          break;
        case AttackUndead.State.ATTACK:
          Vector3 position2 = this.m_cachedTransform.position;
          position1.y = 0.0f;
          position2.y = 0.0f;
          Vector3 vector3_3;
          if ((double) Vector3.Distance(position1, position2) < 0.05000000074505806)
          {
            vector3_3 = this.m_cachedTransform.position;
            // ISSUE: explicit constructor call
            ((Vector3) ref vector3_3).\u002Ector(vector3_3.x, vector3_3.y + (float) ((double) Mathf.Sin(Time.time) * (double) this.m_undeadData.floatingCoef * 0.30000001192092896), vector3_3.z);
          }
          else
          {
            position1.y = this.m_undeadData.floatingHeight;
            Vector3 vector3_4 = Vector3.op_Subtraction(position1, this.m_cachedTransform.position);
            vector3_3 = Vector3.op_Addition(this.m_cachedTransform.position, Vector3.op_Multiply(Vector3.op_Multiply(((Vector3) ref vector3_4).normalized, this.m_moveSpeed), Time.deltaTime));
            // ISSUE: explicit constructor call
            ((Vector3) ref vector3_3).\u002Ector(vector3_3.x, vector3_3.y + Mathf.Sin(Time.time) * this.m_undeadData.floatingCoef, vector3_3.z);
          }
          this.m_cachedTransform.position = vector3_3;
          position1.y = 0.0f;
          vector3_3.y = 0.0f;
          if ((double) Vector3.Distance(position1, vector3_3) > (double) this.m_undeadData.attackRange)
          {
            this.BackState();
            break;
          }
          this.m_attackIntervalTimer -= Time.deltaTime;
          if ((double) this.m_attackIntervalTimer > 0.0)
            break;
          this.m_attackIntervalTimer = this.m_undeadData.attackInterval;
          Player targetObject = this.m_targetObject as Player;
          if (Object.op_Equality((Object) targetObject, (Object) null) || targetObject.isDead)
          {
            this.RequestDestroy();
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
    BulletData.BulletUndead dataUndead = bulletData.dataUndead;
    if (dataUndead == null)
      return (AnimEventShot) null;
    if (Object.op_Equality((Object) this.m_attacker, (Object) null))
      return (AnimEventShot) null;
    Quaternion rotation = this.m_cachedTransform.rotation;
    Vector3 position = this.m_cachedTransform.position;
    AnimEventShot externalBulletData = AnimEventShot.CreateByExternalBulletData(dataUndead.closeBullet, this.m_attacker, this.m_atkInfo, position, rotation);
    if (!Object.op_Equality((Object) externalBulletData, (Object) null))
      return externalBulletData;
    Log.Error("Failed to create AnimEventShot for Undead!!");
    return (AnimEventShot) null;
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

  public void RequestDestroy()
  {
    if (this.m_func == AttackUndead.Function.DELETE || this.m_isDeleted)
      return;
    this.RequestFunction(AttackUndead.Function.DELETE);
    if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
    {
      this.Destroy();
    }
    else
    {
      this.m_effectDeleteAnimHash = Animator.StringToHash("END");
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
      case AttackUndead.State.TRACKING:
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
      case AttackUndead.State.ATTACK:
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

  private void RequestFunction(AttackUndead.Function func)
  {
    this.m_func = func;
    this.SetState(AttackUndead.State.TRACKING);
  }

  private void SetState(AttackUndead.State state) => this.m_state = state;

  private void ForwardState() => ++this.m_state;

  private void BackState()
  {
    if (this.m_state <= AttackUndead.State.NONE)
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
