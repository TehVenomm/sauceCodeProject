// Decompiled with JetBrains decompiler
// Type: FieldGimmickCannonHeavy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FieldGimmickCannonHeavy : FieldGimmickCannonBase
{
  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForHeavy;
    this.m_baseTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
    this.m_cannonTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
  }

  public override void Shot()
  {
    if (!this.IsReadyForShot())
      return;
    if (Object.op_Inequality((Object) this._animator, (Object) null))
      this._animator.Play("Reaction", 0, 0.0f);
    AttackInfo attackHitInfo = this.GetAttackHitInfo();
    if (attackHitInfo == null)
      return;
    new GameObject("HeavyCannonball").AddComponent<AttackCannonball>().Initialize(new AttackCannonball.InitParamCannonball()
    {
      attacker = (StageObject) this.m_owner,
      atkInfo = attackHitInfo,
      launchTrans = this.m_cannonTrans,
      offsetPos = Vector3.zero,
      offsetRot = Quaternion.identity,
      shotRotation = this.m_cannonTrans.rotation
    });
    this.StartCoolTime();
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  protected override AttackInfo GetAttackHitInfo()
  {
    return Object.op_Equality((Object) this.m_owner, (Object) null) ? (AttackInfo) null : this.m_owner.GetAttackInfos().Find<AttackInfo>((Predicate<AttackInfo>) (info => info.name == "cannonball_heavy")) ?? (AttackInfo) null;
  }
}
