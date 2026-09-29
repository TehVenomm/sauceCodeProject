// Decompiled with JetBrains decompiler
// Type: CannonballAttackObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CannonballAttackObject : AttackColliderObject
{
  public int ignoreLayerMask;
  private AttackCannonball owner;
  private float fixedTime;

  public bool isHit { get; private set; }

  public int hitLayer { get; private set; }

  public Enemy hitEnemy { get; private set; }

  public void SetIgnoreLayerMask(int mask) => this.ignoreLayerMask = mask;

  public void SetOwner(AttackCannonball owner)
  {
    this.owner = owner;
    this.fixedTime = 0.0f;
  }

  public void ResetHit()
  {
    this.isHit = false;
    this.ActivateOwnCollider();
  }

  public override float GetTime() => this.fixedTime;

  private void FixedUpdate() => this.fixedTime += Time.fixedDeltaTime;

  protected override void OnTriggerEnter(Collider collider)
  {
    this.hitLayer = ((Component) collider).gameObject.layer;
    if ((1 << this.hitLayer & this.ignoreLayerMask) != 0)
      return;
    if (this.hitLayer == 11)
      this.hitEnemy = ((Component) collider).gameObject.GetComponent<Enemy>();
    else if (this.hitLayer == 31 /*0x1F*/ && (Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<EscapePointObject>(), (Object) null) || Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<BarrierBulletObject>(), (Object) null)))
      return;
    this.isHit = true;
    base.OnTriggerEnter(collider);
    this.DeactivateOwnCollider();
    this.Destroy();
    if (!Object.op_Inequality((Object) this.owner, (Object) null))
      return;
    this.owner.OnHit();
  }
}
