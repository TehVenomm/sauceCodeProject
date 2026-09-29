// Decompiled with JetBrains decompiler
// Type: LaserAttackObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class LaserAttackObject : AttackColliderObject
{
  private GameObject m_effectLaser;
  public Animator m_effectAnimator;
  public CapsuleCollider m_capCollider;

  public void CreateEffect(BulletData.BulletBase bulletBase)
  {
    Transform effect = EffectManager.GetEffect(bulletBase.effectName);
    if (Object.op_Equality((Object) effect, (Object) null))
    {
      Log.Error("Failed to create effect for LaserAttackObject!!");
    }
    else
    {
      effect.parent = ((Component) this).transform;
      effect.localPosition = bulletBase.dispOffset;
      effect.localRotation = Quaternion.Euler(bulletBase.dispRotation);
      this.m_effectLaser = ((Component) effect).gameObject;
      if (Object.op_Implicit((Object) this.m_effectLaser.GetComponent<Animator>()))
        this.m_effectAnimator = this.m_effectLaser.GetComponent<Animator>();
      if (!Object.op_Implicit((Object) ((Component) this).GetComponent<CapsuleCollider>()))
        return;
      this.m_capCollider = ((Component) this).GetComponent<CapsuleCollider>();
    }
  }

  public override void Destroy()
  {
    if (Object.op_Inequality((Object) this.m_effectLaser, (Object) null))
    {
      EffectManager.ReleaseEffect(this.m_effectLaser);
      this.m_effectLaser = (GameObject) null;
    }
    base.Destroy();
  }
}
