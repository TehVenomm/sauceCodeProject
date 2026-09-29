// Decompiled with JetBrains decompiler
// Type: BulletControllerObstacleCylinder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class BulletControllerObstacleCylinder : BulletControllerBase
{
  private Collider m_collider;
  private Rigidbody m_rigidbody;
  private BoxCollider[] m_colliderList;
  private BulletData.BulletObstacleCylinder m_bulletObstacleCylinder;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    this.m_collider = ((Component) this).GetComponent<Collider>();
    if (Object.op_Inequality((Object) this.m_collider, (Object) null))
      this.m_collider.enabled = false;
    this.m_bulletObstacleCylinder = bullet.dataObstacleCylinder;
    if (this.m_bulletObstacleCylinder == null)
      return;
    this.m_rigidbody = ((Component) this).GetComponent<Rigidbody>();
    if (Object.op_Inequality((Object) this.m_rigidbody, (Object) null))
    {
      this.m_rigidbody.useGravity = false;
      this.m_rigidbody.isKinematic = true;
    }
    float num1 = (float) (360 / this.m_bulletObstacleCylinder.colliderNum);
    float num2 = num1 * ((float) Math.PI / 180f);
    float radius = this.m_bulletObstacleCylinder.radius;
    for (int index = 0; index < this.m_bulletObstacleCylinder.colliderNum; ++index)
    {
      BoxCollider boxCollider = new GameObject("Collider")
      {
        transform = {
          parent = ((Component) this).transform,
          localPosition = new Vector3(radius * Mathf.Sin(num2 * (float) (index + 1)), 0.0f, radius * Mathf.Cos(num2 * (float) (index + 1))),
          localRotation = Quaternion.Euler(0.0f, num1 * (float) (index + 1), 0.0f)
        }
      }.AddComponent<BoxCollider>();
      boxCollider.size = this.m_bulletObstacleCylinder.size;
      boxCollider.center = this.m_bulletObstacleCylinder.center;
    }
    Utility.SetLayerWithChildren(((Component) this).transform, 18);
  }

  public override void Update()
  {
    base.Update();
    BulletData.BulletObstacleCylinder obstacleCylinder = this.m_bulletObstacleCylinder;
  }

  public override void OnShot() => Utility.SetLayerWithChildren(this._transform, 18);
}
