// Decompiled with JetBrains decompiler
// Type: BulletControllerSearch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerSearch : BulletControllerBase
{
  private bool isOwnerSelf;
  private Player ownerPlayer;
  private Character targetEnemy;
  private int targetId = -1;
  private float searchStartTime;
  private float angularVelocity;
  private bool isStart;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
    this.searchStartTime = bullet.dataSearch.searchStartTime;
    this.angularVelocity = bullet.dataSearch.angularVelocity;
    this._rigidbody.velocity = Vector3.zero;
    this.isStart = false;
  }

  public override void RegisterTargetObject(StageObject obj)
  {
    base.RegisterTargetObject(obj);
    if (!Object.op_Inequality((Object) obj, (Object) null))
      return;
    this.targetEnemy = obj as Character;
    this.targetId = this.targetEnemy.id;
  }

  public override void PostInitialize()
  {
    this.ownerPlayer = this.fromObject as Player;
    this.isOwnerSelf = this.fromObject is Self;
  }

  public override void Update()
  {
    base.Update();
    if ((double) this.searchStartTime > (double) this.timeCount)
      return;
    if (!this.isStart)
    {
      this.isStart = true;
      this._rigidbody.velocity = Vector3.op_Multiply(Vector3.op_Multiply(this._transform.forward, this.speed), Time.deltaTime);
    }
    Vector3 vector3 = Vector3.op_Subtraction(this.GetDestination(), this._transform.position);
    vector3.y = 0.0f;
    float num1 = Mathf.Abs(Vector3.Angle(this._transform.forward, vector3));
    if ((double) num1 <= 0.0)
      return;
    float num2 = this.angularVelocity * Time.deltaTime / num1;
    if ((double) num2 > 1.0)
      num2 = 1f;
    this._transform.rotation = Quaternion.Lerp(this._transform.rotation, Quaternion.LookRotation(vector3), num2);
    this._rigidbody.velocity = Vector3.op_Multiply(Vector3.op_Multiply(this._transform.forward, this.speed), Time.deltaTime);
  }

  private Vector3 GetDestination()
  {
    if (this.isOwnerSelf)
    {
      this.CheckTarget();
      return Object.op_Equality((Object) this.targetEnemy, (Object) null) ? this.fromObject._position : this.targetEnemy._position;
    }
    return this.targetId == -1 ? this.fromObject._position : this.targetEnemy._position;
  }

  private void CheckTarget()
  {
    if (Object.op_Inequality((Object) this.targetEnemy, (Object) null) && !this.targetEnemy.isDead)
      return;
    this.targetEnemy = (Character) null;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    float num = float.MaxValue;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; index < count; ++index)
    {
      Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[index] as Enemy;
      if (!Object.op_Equality((Object) enemy, (Object) null) && !enemy.isDead && !enemy.enableAssimilation)
      {
        Vector3 vector3 = Vector3.op_Subtraction(enemy._position, this._transform.position);
        float sqrMagnitude = ((Vector3) ref vector3).sqrMagnitude;
        if ((double) num > (double) sqrMagnitude && (double) sqrMagnitude <= (double) this.bulletObject.bulletData.dataSearch.searchRangeSqr)
        {
          this.targetEnemy = (Character) enemy;
          num = sqrMagnitude;
        }
      }
    }
    if (Object.op_Equality((Object) this.ownerPlayer, (Object) null) || Object.op_Equality((Object) this.ownerPlayer.playerSender, (Object) null))
      return;
    if (Object.op_Inequality((Object) this.targetEnemy, (Object) null))
    {
      if (this.targetId == this.targetEnemy.id)
        return;
      this.targetId = this.targetEnemy.id;
    }
    else
    {
      if (this.targetId == -1)
        return;
      this.targetId = -1;
    }
    this.ownerPlayer.playerSender.OnBulletObservableSearchTarget(this.bulletObject.GetObservedID(), this.targetId);
  }

  public void SetTargetId(int id)
  {
    this.targetId = id;
    if (this.targetId == -1)
    {
      this.targetEnemy = (Character) null;
    }
    else
    {
      int index = 0;
      for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; index < count; ++index)
      {
        Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[index] as Enemy;
        if (!Object.op_Equality((Object) enemy, (Object) null) && enemy.id == id)
        {
          this.targetEnemy = (Character) enemy;
          return;
        }
      }
      this.targetEnemy = (Character) null;
      this.targetId = -1;
    }
  }
}
