// Decompiled with JetBrains decompiler
// Type: AttackContinuationColliderProcessor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackContinuationColliderProcessor : AttackColliderProcessor
{
  protected List<StageObject> enterList = new List<StageObject>();

  protected AttackContinuationInfo attackContinuationInfo
  {
    get => this.attackInfo as AttackContinuationInfo;
  }

  public override void OnDestroy()
  {
    base.OnDestroy();
    int index = 0;
    for (int count = this.enterList.Count; index < count; ++index)
      this.enterList[index].OnContinuationExit(this.attackContinuationInfo, this.fromCollider);
    this.enterList.Clear();
  }

  public override void OnFixedUpdate() => base.OnFixedUpdate();

  public override void OnTriggerEnter(Collider to_collider)
  {
    base.OnTriggerEnter(to_collider);
    if (Object.op_Equality((Object) this.fromCollider, (Object) null) || Object.op_Equality((Object) this.fromObject, (Object) null) || !this.fromCollider.enabled || Object.op_Equality((Object) ((Component) to_collider).gameObject, (Object) ((Component) this.fromCollider).gameObject))
      return;
    StageObject componentInParent = ((Component) to_collider).gameObject.GetComponentInParent<StageObject>();
    if (Object.op_Equality((Object) componentInParent, (Object) null))
      return;
    float time = 0.0f;
    if (this.colliderInterface != null)
      time = this.colliderInterface.GetTime();
    if (!componentInParent.OnContinuationEnter(this.attackContinuationInfo, this.fromObject, this.fromCollider, time))
      return;
    this.enterList.Add(componentInParent);
  }

  public override void OnTriggerExit(Collider to_collider)
  {
    base.OnTriggerExit(to_collider);
    StageObject componentInParent = ((Component) to_collider).gameObject.GetComponentInParent<StageObject>();
    if (Object.op_Equality((Object) componentInParent, (Object) null))
      return;
    int index = 0;
    for (int count = this.enterList.Count; index < count; ++index)
    {
      if (Object.op_Equality((Object) this.enterList[index], (Object) componentInParent))
      {
        componentInParent.OnContinuationExit(this.attackContinuationInfo, this.fromCollider);
        this.enterList.RemoveAt(index);
        break;
      }
    }
  }
}
