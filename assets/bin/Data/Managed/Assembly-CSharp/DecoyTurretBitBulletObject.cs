// Decompiled with JetBrains decompiler
// Type: DecoyTurretBitBulletObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class DecoyTurretBitBulletObject : DecoyBulletObject
{
  private bool isEndDelay;
  private float firstShotDelayTimer;
  private float shotInterval_Sec;
  private float shotIntervalTimer;
  private float lookAtInterpolate = 0.2f;
  private float searchInterval_Sec;
  private float searchIntervalTimer;
  private float searchRangeSqr = 10f;
  private AttackInfo normalAtkInfo;
  private AttackInfo finalAttackInfo;
  private BulletData.BulletDecoyTurretBit dataTurretBit;
  private Enemy targetEnemy;
  private TargetPoint targetPoint;
  private AtkAttribute exNormalAtk;
  private AtkAttribute exFinalAtk;
  private Player.ATTACK_MODE exAttackMode;
  private SkillInfo.SkillParam exSkillParam;
  protected Transform rotateTransForm;
  protected Transform cachedChargeEffectTransform;

  protected override BulletData.BulletDecoy GetDecoyData(BulletData bullet)
  {
    return (BulletData.BulletDecoy) bullet.dataDecoyTurretBit;
  }

  protected override bool IsHitExplede() => false;

  protected override void Update()
  {
    base.Update();
    this.UpdateTarget();
    this.UpdateShot();
    if (!Object.op_Equality((Object) this.ownerPlayer, (Object) null))
      return;
    this.OnDisappear(true);
  }

  public override void Initialize(
    int playerId,
    int decoyId,
    BulletData bullet,
    Vector3 position,
    SkillInfo.SkillParam skill,
    bool isHit)
  {
    base.Initialize(playerId, decoyId, bullet, position, skill, isHit);
    this.dataTurretBit = bullet.dataDecoyTurretBit;
    this.firstShotDelayTimer = this.dataTurretBit.firstShotDelay_Sec;
    this.shotInterval_Sec = this.dataTurretBit.shotInterval_Sec;
    this.shotIntervalTimer = 0.0f;
    this.lookAtInterpolate = this.dataTurretBit.lookAtInterpolate;
    this.searchInterval_Sec = this.dataTurretBit.searchInterval_Sec;
    this.searchIntervalTimer = 0.0f;
    this.searchRangeSqr = this.dataTurretBit.searchRangeSqr;
    this.isEndDelay = false;
    this.targetEnemy = (Enemy) null;
    this.targetPoint = (TargetPoint) null;
    this.normalAtkInfo = this.ownerPlayer.FindAttackInfo(this.dataTurretBit.normalAtkInfoName);
    this.finalAttackInfo = this.ownerPlayer.FindAttackInfo(this.dataTurretBit.finalAtkInfoName);
    if (this.normalAtkInfo != null)
    {
      this.exNormalAtk = new AtkAttribute();
      this.ownerPlayer.GetAtk(this.normalAtkInfo as AttackHitInfo, ref this.exNormalAtk, skill);
    }
    if (this.finalAttackInfo != null)
    {
      this.exFinalAtk = new AtkAttribute();
      this.ownerPlayer.GetAtk(this.finalAttackInfo as AttackHitInfo, ref this.exFinalAtk, skill);
    }
    this.exAttackMode = this.ownerPlayer.attackMode;
    this.exSkillParam = this.ownerPlayer.skillInfo.actSkillParam;
    Transform transform = Utility.FindChild(this.cachedEffectTransform, this.dataTurretBit.chargeEffectParentNodename);
    if (Object.op_Equality((Object) transform, (Object) null))
      transform = this.cachedEffectTransform;
    if (!string.IsNullOrEmpty(this.dataTurretBit.chargeEffectName))
    {
      this.cachedChargeEffectTransform = EffectManager.GetEffect(this.dataTurretBit.chargeEffectName, MonoBehaviourSingleton<EffectManager>.I._transform);
      this.cachedChargeEffectTransform.SetParent(transform);
      this.cachedChargeEffectTransform.localPosition = this.dataTurretBit.chargeEffectDispOffset;
      this.cachedChargeEffectTransform.localRotation = Quaternion.Euler(this.dataTurretBit.chargeEffectDispRotation);
    }
    this.rotateTransForm = Utility.FindChild(this.cachedEffectTransform, this.dataTurretBit.rotateNodeName);
    if (!Object.op_Equality((Object) this.rotateTransForm, (Object) null))
      return;
    this.rotateTransForm = this.cachedEffectTransform;
  }

  private bool UpdateTarget()
  {
    if (Object.op_Equality((Object) this.ownerPlayer, (Object) null) || Object.op_Equality((Object) this.ownerPlayer.playerSender, (Object) null) || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    this.searchIntervalTimer -= Time.deltaTime;
    if ((double) this.searchIntervalTimer > 0.0)
      return false;
    this.searchIntervalTimer += this.searchInterval_Sec;
    this.targetEnemy = (Enemy) null;
    this.targetPoint = (TargetPoint) null;
    float num1 = float.MaxValue;
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; index1 < count; ++index1)
    {
      Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[index1] as Enemy;
      if (!Object.op_Equality((Object) enemy, (Object) null) && !enemy.isDead)
      {
        Vector3 vector3 = Vector3.op_Subtraction(enemy._position, this._transform.position);
        float sqrMagnitude = ((Vector3) ref vector3).sqrMagnitude;
        if ((double) num1 > (double) sqrMagnitude && (double) sqrMagnitude <= (double) this.searchRangeSqr)
        {
          this.targetEnemy = enemy;
          num1 = sqrMagnitude;
        }
      }
    }
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
      {
        this.targetEnemy = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      }
      else
      {
        this.shotIntervalTimer = 0.0f;
        return true;
      }
    }
    if (!this.targetEnemy.isBoss)
      return true;
    float num2 = float.MaxValue;
    int index2 = 0;
    for (int length = this.targetEnemy.targetPoints.Length; index2 < length; ++index2)
    {
      TargetPoint targetPoint = this.targetEnemy.targetPoints[index2];
      if (((Behaviour) targetPoint).enabled && ((Component) targetPoint).gameObject.activeInHierarchy && (targetPoint.regionID < 0 || targetPoint.regionID >= this.targetEnemy.regionWorks.Length || this.targetEnemy.regionWorks[targetPoint.regionID].enabled))
      {
        Vector3 vector3 = Vector3.op_Subtraction(targetPoint._transform.position, this._transform.position);
        float sqrMagnitude = ((Vector3) ref vector3).sqrMagnitude;
        if ((double) num2 > (double) sqrMagnitude)
        {
          this.targetPoint = targetPoint;
          num2 = sqrMagnitude;
        }
      }
    }
    return true;
  }

  public void UpdateShot()
  {
    if (Object.op_Equality((Object) this.rotateTransForm, (Object) null))
      return;
    this.rotateTransForm.rotation = Quaternion.Lerp(this.rotateTransForm.rotation, Quaternion.LookRotation(Vector3.op_Subtraction(this.GetTargetPos(), this.rotateTransForm.position)), this.lookAtInterpolate);
    if (!this.isEndDelay)
    {
      this.firstShotDelayTimer -= Time.deltaTime;
      if ((double) this.firstShotDelayTimer > 0.0)
        return;
      this.isEndDelay = true;
    }
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return;
    this.shotIntervalTimer -= Time.deltaTime;
    if ((double) this.shotIntervalTimer > 0.0)
      return;
    this.shotIntervalTimer += this.shotInterval_Sec;
    this.CreateBullet(this.normalAtkInfo, false);
  }

  private Vector3 GetTargetPos()
  {
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return Vector3.zero;
    return Object.op_Inequality((Object) this.targetPoint, (Object) null) ? this.targetPoint._transform.position : this.targetEnemy._position;
  }

  public override void OnDisappear(bool isExplode)
  {
    this.CreateBullet(this.finalAttackInfo, true);
    if (Object.op_Inequality((Object) this.cachedChargeEffectTransform, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.cachedChargeEffectTransform).gameObject);
    base.OnDisappear(isExplode);
  }

  private void CreateBullet(AttackInfo atkInfo, bool isFinal)
  {
    if (atkInfo == null)
      return;
    BulletData bulletData = atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null) || Object.op_Equality((Object) this.ownerPlayer, (Object) null) || Object.op_Equality((Object) this.cachedEffectTransform, (Object) null))
      return;
    Quaternion rot = Quaternion.LookRotation(Vector3.op_Subtraction(this.GetTargetPos(), this.cachedEffectTransform.position));
    AnimEventShot.CreateByExternalBulletData(bulletData, (StageObject) this.ownerPlayer, atkInfo, this.cachedEffectTransform.position, rot, isFinal ? this.exFinalAtk : this.exNormalAtk, this.exAttackMode, this.exSkillParam);
  }
}
