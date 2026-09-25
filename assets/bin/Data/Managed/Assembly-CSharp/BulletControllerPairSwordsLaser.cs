// Decompiled with JetBrains decompiler
// Type: BulletControllerPairSwordsLaser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerPairSwordsLaser : BulletControllerBase
{
  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    if ((double) this.speed <= 0.0)
      this._transform.localRotation = rot;
    this._rigidbody.isKinematic = true;
  }
}
