// Decompiled with JetBrains decompiler
// Type: BulletControllerResurrectionHoming
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerResurrectionHoming : BulletControllerHealingHoming
{
  protected bool m_isAddBuffActionOnResurrected = true;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    BulletData.BulletResurrectionHoming resurrectionHomingBullet = bullet.dataResurrectionHomingBullet;
    if (resurrectionHomingBullet == null)
      return;
    this.InitParam((BulletData.BulletHoming) resurrectionHomingBullet);
    this.m_isIgnoreColliderExceptTarget = resurrectionHomingBullet.isIgnoreColliderExceptTarget;
    this.m_isAlreadyDoneHitProcess = false;
    this.m_buffIdList = resurrectionHomingBullet.buffIds;
    this.m_isAddBuffActionOnResurrected = resurrectionHomingBullet.isAddBuffActionOnResurrected;
    Utility.SetLayerWithChildren(((Component) this).transform, resurrectionHomingBullet.defaultGenerateLayer);
    this.m_effectAnimator = ((Component) this).GetComponentInChildren<Animator>();
  }

  public override void OnHit(Collider collider)
  {
    if (this.m_isAlreadyDoneHitProcess)
      return;
    this.m_isAlreadyDoneHitProcess = true;
    this.PlayOnHitAnimation();
    Player component = ((Component) collider).gameObject.GetComponent<Player>();
    if (Object.op_Inequality((Object) component, (Object) null) && component.isDead)
    {
      component.OnResurrectionReceive();
      this.AddBuffActionOnActDeadStandUp(component);
    }
    else
    {
      this.HealAction();
      this.AddBuffAction(component);
    }
  }

  protected void AddBuffActionOnActDeadStandUp(Player _player)
  {
    if (this.m_buffIdList == null || this.m_buffIdList.Count < 1 || this.bulletSkillInfoParam == null)
      return;
    int index = 0;
    for (int count = this.m_buffIdList.Count; index < count; ++index)
      _player.StartBuffByBuffTableIdOnActDeadStandUp(this.m_buffIdList[index], this.bulletSkillInfoParam);
  }
}
