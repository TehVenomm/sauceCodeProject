// Decompiled with JetBrains decompiler
// Type: AttackNWayLaser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackNWayLaser : MonoBehaviour
{
  private List<LaserAttackObject> m_laserAttackList = new List<LaserAttackObject>();
  private BulletData.BulletLaser m_laserData;
  private StageObject m_attacker;
  private string m_atkInfoName = string.Empty;
  private Transform m_parentTrans;
  private float m_nowAngleSpeed;
  private float m_aliveTimer;
  private bool m_isDelete;
  private bool m_isRequestDelete;
  private bool m_isAnimEnd;
  private bool m_isAtkEnd;

  public void Initialize(
    StageObject attacker,
    Transform parentTrans,
    AttackInfo atkInfo,
    int numLaser)
  {
    BulletData bulletData = atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return;
    BulletData.BulletBase data = bulletData.data;
    BulletData.BulletLaser dataLaser = bulletData.dataLaser;
    if (dataLaser == null || data == null)
      return;
    this.m_attacker = attacker;
    this.m_aliveTimer = data.appearTime;
    this.m_nowAngleSpeed = dataLaser.initAngleSpeed;
    this.m_laserData = dataLaser;
    this.m_parentTrans = parentTrans;
    int attackLayer = attacker is Player ? 14 : 15;
    if (atkInfo is AttackHitInfo attackHitInfo)
      attackHitInfo.enableIdentityCheck = false;
    this.m_atkInfoName = atkInfo.name;
    Transform transform = ((Component) this).transform;
    if (this.m_laserData.isLinkPositionOnly)
    {
      transform.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
      transform.position = parentTrans.position;
    }
    else
    {
      transform.parent = parentTrans;
      transform.localPosition = Vector3.zero;
    }
    transform.localRotation = Quaternion.identity;
    float radius = data.radius;
    float capsuleHeight = dataLaser.capsuleHeight;
    Vector3 offsetPosition = dataLaser.offsetPosition;
    float num1 = 360f / (float) numLaser;
    float num2 = 0.0f;
    for (int index = 0; index < numLaser; ++index)
    {
      LaserAttackObject laserAttackObject = new GameObject("LaserAttackObject").AddComponent<LaserAttackObject>();
      laserAttackObject.Initialize(attacker, transform, atkInfo, offsetPosition, new Vector3(0.0f, num2, 0.0f), radius, capsuleHeight, attackLayer);
      laserAttackObject.CreateEffect(data);
      this.m_laserAttackList.Add(laserAttackObject);
      num2 += num1;
    }
  }

  public void Destroy()
  {
    if (this.IsDeleted)
      return;
    int count = this.m_laserAttackList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (Object.op_Inequality((Object) this.m_laserAttackList[index], (Object) null))
      {
        if (Object.op_Implicit((Object) ((Component) ((Component) this.m_laserAttackList[index]).transform.GetChild(0)).GetComponent<Animator>()))
        {
          ((Component) ((Component) this.m_laserAttackList[index]).transform.GetChild(0)).GetComponent<Animator>().Play("END");
        }
        else
        {
          this.m_laserAttackList[index].Destroy();
          this.m_laserAttackList[index] = (LaserAttackObject) null;
        }
      }
    }
    this.m_laserAttackList.Clear();
    if (Object.op_Inequality((Object) this.m_attacker, (Object) null))
    {
      Enemy attacker1 = this.m_attacker as Enemy;
      if (Object.op_Inequality((Object) attacker1, (Object) null))
        attacker1.OnDestroyLaser(this);
      Player attacker2 = this.m_attacker as Player;
      if (Object.op_Inequality((Object) attacker2, (Object) null))
        attacker2.OnDestroyLaser(this);
    }
    this.m_isDelete = true;
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  public void RequestDestroy() => this.m_isRequestDelete = true;

  public void AnimEnd()
  {
    if (this.IsDeleted || !this.m_isAtkEnd)
      return;
    bool flag = true;
    int count = this.m_laserAttackList.Count;
    if (this.IsAnimEnd)
    {
      for (int index = 0; index < count; ++index)
      {
        if (Object.op_Inequality((Object) this.m_laserAttackList[index], (Object) null) && Object.op_Inequality((Object) this.m_laserAttackList[index].m_effectAnimator, (Object) null))
        {
          AnimatorStateInfo animatorStateInfo = this.m_laserAttackList[index].m_effectAnimator.GetCurrentAnimatorStateInfo(0);
          if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
            flag = false;
        }
      }
    }
    else
    {
      for (int index = 0; index < count; ++index)
      {
        if (Object.op_Inequality((Object) this.m_laserAttackList[index], (Object) null))
        {
          if (Object.op_Inequality((Object) this.m_laserAttackList[index].m_capCollider, (Object) null))
          {
            Object.Destroy((Object) this.m_laserAttackList[index].m_capCollider);
            this.m_laserAttackList[index].m_capCollider = (CapsuleCollider) null;
          }
          if (Object.op_Inequality((Object) this.m_laserAttackList[index].m_effectAnimator, (Object) null))
          {
            this.m_laserAttackList[index].m_effectAnimator.Play("END");
            flag = false;
          }
        }
      }
      this.IsAnimEnd = true;
    }
    if (!flag)
      return;
    this.Destroy();
  }

  private void Update()
  {
    if (this.IsDeleted)
      return;
    if (this.m_isAtkEnd)
    {
      this.AnimEnd();
    }
    else
    {
      if (this.m_isRequestDelete)
        this.m_aliveTimer = 0.0f;
      this.m_aliveTimer -= Time.deltaTime;
      if ((double) this.m_aliveTimer > 0.0)
        return;
      this.m_isAtkEnd = true;
    }
  }

  private void LateUpdate()
  {
    if (this.IsDeleted)
      return;
    BulletData.BulletLaser laserData = this.m_laserData;
    if (laserData.isLinkPositionOnly && Object.op_Inequality((Object) this.m_parentTrans, (Object) null))
      ((Component) this).transform.position = this.m_parentTrans.position;
    this.m_nowAngleSpeed += laserData.addAngleSpeed * Time.deltaTime;
    this.m_nowAngleSpeed = (double) laserData.addAngleSpeed <= 0.0 ? Mathf.Max(this.m_nowAngleSpeed, -laserData.limitAngleSpeed) : Mathf.Min(this.m_nowAngleSpeed, laserData.limitAngleSpeed);
    ((Component) this).transform.localRotation = Quaternion.op_Multiply(((Component) this).transform.localRotation, Quaternion.AngleAxis(this.m_nowAngleSpeed * Time.deltaTime, Vector3.up));
  }

  public string AttackInfoName => this.m_atkInfoName;

  public bool IsDeleted => this.m_isDelete;

  public bool IsAnimEnd
  {
    get => this.m_isAnimEnd;
    set => this.m_isAnimEnd = value;
  }
}
