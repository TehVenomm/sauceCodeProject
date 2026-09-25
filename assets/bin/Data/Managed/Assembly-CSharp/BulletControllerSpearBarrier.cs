// Decompiled with JetBrains decompiler
// Type: BulletControllerSpearBarrier
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerSpearBarrier : BulletControllerBase
{
  private static readonly string OBJECT_NAME = "SpearBarrierBullet";
  private BulletData bulletData;
  private SphereCollider cachedCollider;
  private int ignoreLayerMask;
  private Player owner;
  private bool isRegistered;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
    this.bulletData = bullet;
    ((Object) ((Component) this).gameObject).name = BulletControllerSpearBarrier.OBJECT_NAME;
    ((Component) this).gameObject.layer = 31 /*0x1F*/;
    this.ignoreLayerMask |= 41984;
    this.ignoreLayerMask |= 20480 /*0x5000*/;
    if (bullet.data.isObjectHitDelete)
      return;
    this.ignoreLayerMask |= 2490880;
  }

  public override void RegisterFromObject(StageObject obj)
  {
    base.RegisterFromObject(obj);
    this.owner = obj as Player;
    this._transform.position = obj._position;
    this._transform.rotation = obj._rotation;
    this.isRegistered = true;
  }

  public override bool IsHit(Collider collider)
  {
    int layer = ((Component) collider).gameObject.layer;
    return (1 << layer & this.ignoreLayerMask) <= 0 && (layer != 8 || !Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null));
  }

  public override void Update()
  {
    this.timeCount += Time.deltaTime;
    if (Object.op_Inequality((Object) this.owner, (Object) null))
    {
      this._transform.position = this.owner._position;
      this._transform.rotation = this.owner._rotation;
      if (!this.owner.isActSpecialAction)
        this.bulletObject.ForceBreak();
      if (this.owner.spearCtrl.IsBarrierBulletDelete())
      {
        this.bulletObject.ForceBreak();
        this.owner.spearCtrl.DisableBarrierBulletDelete();
      }
    }
    if (!this.isRegistered || !Object.op_Equality((Object) this.owner, (Object) null))
      return;
    this.bulletObject.ForceBreak();
  }
}
