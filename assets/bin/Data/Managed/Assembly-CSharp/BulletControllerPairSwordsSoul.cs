// Decompiled with JetBrains decompiler
// Type: BulletControllerPairSwordsSoul
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BulletControllerPairSwordsSoul : BulletControllerBase, IObservable
{
  private List<IObserver> observerList = new List<IObserver>();

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
  }

  public override void OnHit(Collider collider)
  {
    if (((Component) collider).gameObject.layer != 11 && ((Component) collider).gameObject.layer != 10)
      return;
    this.NotifyObservers();
  }

  public void RegisterObserver(IObserver observer) => this.observerList.Add(observer);

  public void NotifyObservers()
  {
    for (int index = 0; index < this.observerList.Count; ++index)
      this.observerList[index].OnHit();
  }
}
