// Decompiled with JetBrains decompiler
// Type: BulletControllerHealingHoming
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BulletControllerHealingHoming : BulletControllerHoming
{
  protected const float PERCENT = 0.01f;
  private static readonly int ANIM_STATE_PICKED = Animator.StringToHash("PICKED");
  protected bool m_isIgnoreColliderExceptTarget = true;
  protected bool m_isAlreadyDoneHitProcess;
  protected Animator m_effectAnimator;
  protected List<int> m_buffIdList;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    BulletData.BulletHealingHoming healingHomingBullet = bullet.dataHealingHomingBullet;
    if (healingHomingBullet == null)
      return;
    this.InitParam((BulletData.BulletHoming) healingHomingBullet);
    this.m_isIgnoreColliderExceptTarget = healingHomingBullet.isIgnoreColliderExceptTarget;
    this.m_isAlreadyDoneHitProcess = false;
    this.m_buffIdList = healingHomingBullet.buffIds;
    Utility.SetLayerWithChildren(((Component) this).transform, healingHomingBullet.defaultGenerateLayer);
    this.m_effectAnimator = ((Component) this).GetComponentInChildren<Animator>();
  }

  public override bool IsHit(Collider collider)
  {
    StageObject targetObject = this.targetObject;
    return !this.m_isIgnoreColliderExceptTarget || !Object.op_Inequality((Object) targetObject, (Object) null) || !(((Object) targetObject).name != ((Object) collider).name);
  }

  public override void OnHit(Collider collider)
  {
    if (this.m_isAlreadyDoneHitProcess)
      return;
    this.m_isAlreadyDoneHitProcess = true;
    this.PlayOnHitAnimation();
    this.HealAction();
    this.AddBuffAction(((Component) collider).gameObject.GetComponent<Player>());
    base.OnHit(collider);
  }

  protected void PlayOnHitAnimation()
  {
    if (Object.op_Inequality((Object) this.bulletObject, (Object) null))
      this.bulletObject.SetDisablePlayEndAnim();
    if (Object.op_Equality((Object) this.m_effectAnimator, (Object) null))
      return;
    this.m_effectAnimator.Play(BulletControllerHealingHoming.ANIM_STATE_PICKED, 0, 0.0f);
  }

  protected void HealAction()
  {
    if (this.bulletSkillInfoParam == null)
      return;
    StageObject targetObject = this.targetObject;
    if (Object.op_Equality((Object) targetObject, (Object) null) || !targetObject.IsCoopNone() && !targetObject.IsOriginal() || !(targetObject is Player))
      return;
    Character.HealData healData = new Character.HealData(this.bulletSkillInfoParam.healHp, this.bulletSkillInfoParam.tableData.healType, HEAL_EFFECT_TYPE.BASIS, new List<int>()
    {
      10
    });
    (targetObject as Player).OnHealReceive(healData);
  }

  protected void AddBuffAction(Player _player)
  {
    if (this.m_buffIdList == null || this.m_buffIdList.Count < 1 || this.bulletSkillInfoParam == null || Object.op_Equality((Object) _player, (Object) null) || _player.isDead)
      return;
    int index = 0;
    for (int count = this.m_buffIdList.Count; index < count; ++index)
      _player.StartBuffByBuffTableId(this.m_buffIdList[index], this.bulletSkillInfoParam);
  }
}
