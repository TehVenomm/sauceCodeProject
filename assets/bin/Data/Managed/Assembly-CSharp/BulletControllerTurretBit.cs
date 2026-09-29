// Decompiled with JetBrains decompiler
// Type: BulletControllerTurretBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class BulletControllerTurretBit : BulletControllerFollow
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
  private BulletData.BulletTurretBit dataTurretBit;
  private Player fromPlayer;
  private Enemy targetEnemy;
  private TargetPoint targetPoint;
  private AtkAttribute exNormalAtk;
  private AtkAttribute exFinalAtk;
  private Player.ATTACK_MODE exAttackMode;
  private SkillInfo.SkillParam exSkillParam;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
    this.followOffset = bullet.dataTurretBit.followOffset;
    this.attenuation = bullet.dataTurretBit.attenuation;
    this.dataTurretBit = bullet.dataTurretBit;
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
  }

  public override void PostInitialize()
  {
    this.normalAtkInfo = this.fromObject.FindAttackInfo(this.dataTurretBit.normalAtkInfoName);
    this.finalAttackInfo = this.fromObject.FindAttackInfo(this.dataTurretBit.finalAtkInfoName);
    if (this.normalAtkInfo != null)
    {
      this.exNormalAtk = new AtkAttribute();
      this.fromPlayer.GetAtk(this.normalAtkInfo as AttackHitInfo, ref this.exNormalAtk, (SkillInfo.SkillParam) null);
    }
    if (this.finalAttackInfo != null)
    {
      this.exFinalAtk = new AtkAttribute();
      this.fromPlayer.GetAtk(this.finalAttackInfo as AttackHitInfo, ref this.exFinalAtk, (SkillInfo.SkillParam) null);
    }
    this.exAttackMode = this.fromPlayer.attackMode;
    this.exSkillParam = this.fromPlayer.skillInfo.actSkillParam;
    if (Object.op_Equality((Object) this.fromPlayer, (Object) null) || this.dataTurretBit.uniqueId <= 0)
      return;
    this.fromPlayer.RegistBulletTurret((uint) this.dataTurretBit.uniqueId, this);
  }

  public override void RegisterFromObject(StageObject obj)
  {
    base.RegisterFromObject(obj);
    this.fromPlayer = obj as Player;
  }

  public override void DestroyBulletObject()
  {
    if (!this.isEndDelay || Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return;
    this.CreateBullet(this.finalAttackInfo, true);
  }

  public override void Update()
  {
    base.Update();
    if (this.UpdateTarget())
      this.SendTarget();
    this.UpdateShot();
  }

  private bool UpdateTarget()
  {
    if (Object.op_Equality((Object) this.fromPlayer, (Object) null) || Object.op_Equality((Object) this.fromPlayer.playerSender, (Object) null) || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
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

  private void SendTarget()
  {
    int targetId = -1;
    if (Object.op_Inequality((Object) this.targetEnemy, (Object) null))
      targetId = this.targetEnemy.id;
    int regionId = -1;
    if (Object.op_Inequality((Object) this.targetPoint, (Object) null))
      regionId = this.targetPoint.regionID;
    this.fromPlayer.playerSender.OnBulletObservableTurretBitTarget(this.bulletObject.GetObservedID(), targetId, regionId);
  }

  public void UpdateShot()
  {
    if (!this.isEndDelay)
    {
      this.firstShotDelayTimer -= Time.deltaTime;
      if ((double) this.firstShotDelayTimer > 0.0)
        return;
      this.isEndDelay = true;
    }
    if (Object.op_Equality((Object) this.targetEnemy, (Object) null))
      return;
    this._transform.rotation = Quaternion.Lerp(this._transform.rotation, Quaternion.LookRotation(Vector3.op_Subtraction(this.GetTargetPos(), this._transform.position)), this.lookAtInterpolate);
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

  private void CreateBullet(AttackInfo atkInfo, bool isFinal)
  {
    if (atkInfo == null)
      return;
    BulletData bulletData = atkInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null) || Object.op_Equality((Object) this.fromObject, (Object) null))
      return;
    AnimEventShot.CreateByExternalBulletData(bulletData, this.fromObject, atkInfo, this._transform.position, this._transform.rotation, isFinal ? this.exFinalAtk : this.exNormalAtk, this.exAttackMode, this.exSkillParam);
  }

  public void SetTargetId(int targetID, int regionID)
  {
    this.targetEnemy = (Enemy) null;
    this.targetPoint = (TargetPoint) null;
    if (targetID <= 0)
      return;
    StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Find((Predicate<StageObject>) (obj => obj.id == targetID));
    if (Object.op_Equality((Object) stageObject, (Object) null))
      return;
    this.targetEnemy = stageObject as Enemy;
    if (regionID < 0 || Object.op_Equality((Object) this.targetEnemy, (Object) null) || !this.targetEnemy.isBoss || regionID >= this.targetEnemy.targetPoints.Length)
      return;
    this.targetPoint = this.targetEnemy.targetPoints[regionID];
  }
}
