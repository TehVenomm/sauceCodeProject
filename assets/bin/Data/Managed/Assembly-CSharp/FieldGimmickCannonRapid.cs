// Decompiled with JetBrains decompiler
// Type: FieldGimmickCannonRapid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FieldGimmickCannonRapid : FieldGimmickCannonBase
{
  private readonly Vector3 OFFSET_LEFT = new Vector3(-0.4f, 0.0f, 0.0f);
  private readonly Vector3 OFFSET_RIGHT = new Vector3(0.4f, 0.0f, 0.0f);
  private readonly Vector3 OFFSET_ZERO = Vector3.zero;
  private Vector3[] offsetArray;
  private int shotSeId;

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForRapid;
    this.m_baseTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
    this.m_cannonTrans = this.modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
    this.offsetArray = new Vector3[3]
    {
      this.OFFSET_ZERO,
      this.OFFSET_RIGHT,
      this.OFFSET_LEFT
    };
    this.shotSeId = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForRapid;
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
    int index = Random.Range(0, 3);
    new GameObject("AttackCannonball").AddComponent<AttackCannonball>().Initialize(new AttackCannonball.InitParamCannonball()
    {
      attacker = (StageObject) this.m_owner,
      atkInfo = attackHitInfo,
      launchTrans = this.m_cannonTrans,
      offsetPos = this.offsetArray[index],
      offsetRot = Quaternion.identity,
      shotRotation = this.m_cannonTrans.rotation
    });
    if (this.shotSeId > 0)
      SoundManager.PlayOneShotSE(this.shotSeId, this.m_cannonTrans.position);
    this.StartCoolTime();
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  protected override AttackInfo GetAttackHitInfo()
  {
    return Object.op_Equality((Object) this.m_owner, (Object) null) ? (AttackInfo) null : this.m_owner.GetAttackInfos().Find<AttackInfo>((Predicate<AttackInfo>) (info => info.name == "cannonball_rapid")) ?? (AttackInfo) null;
  }
}
