// Decompiled with JetBrains decompiler
// Type: MineAttackObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MineAttackObject : AttackColliderObject
{
  public int ignoreLayerMask;

  public bool isHit { get; private set; }

  public int hitLayer { get; private set; }

  public Player hitPlayer { get; private set; }

  public void SetIgnoreLayerMask(int mask) => this.ignoreLayerMask = mask;

  public void ResetHit()
  {
    this.isHit = false;
    this.ActivateOwnCollider();
  }

  protected override void OnTriggerEnter(Collider collider)
  {
    this.hitLayer = ((Component) collider).gameObject.layer;
    if ((1 << this.hitLayer & this.ignoreLayerMask) > 0)
      return;
    if (this.hitLayer == 8)
    {
      this.hitPlayer = ((Component) collider).gameObject.GetComponent<Player>();
      if (Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null))
        return;
    }
    if (Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<HealAttackObject>(), (Object) null))
      return;
    this.isHit = true;
    this.DeactivateOwnCollider();
    base.OnTriggerEnter(collider);
  }
}
