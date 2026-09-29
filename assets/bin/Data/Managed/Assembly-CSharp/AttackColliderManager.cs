// Decompiled with JetBrains decompiler
// Type: AttackColliderManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackColliderManager : MonoBehaviourSingleton<AttackColliderManager>
{
  private List<AttackColliderProcessor> processorList = new List<AttackColliderProcessor>();
  private List<AttackColliderProcessor> tempList = new List<AttackColliderProcessor>();

  public AttackColliderProcessor CreateProcessor(
    AttackInfo _attack_info,
    StageObject _object,
    Collider _collider,
    IAttackCollider _collider_interface,
    Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE,
    DamageDistanceTable.DamageDistanceData damageDistanceData = null)
  {
    AttackColliderProcessor processor = (AttackColliderProcessor) null;
    switch (_attack_info)
    {
      case AttackHitInfo _:
        processor = (AttackColliderProcessor) new AttackHitColliderProcessor();
        break;
      case AttackContinuationInfo _:
        processor = (AttackColliderProcessor) new AttackContinuationColliderProcessor();
        break;
    }
    processor.SetFromInfo(_attack_info, _object, _collider, _collider_interface);
    processor.SetAttackMode(attackMode);
    processor.SetDamageDistanceData(damageDistanceData);
    this.AddProcessor(processor);
    return processor;
  }

  public void AddProcessor(AttackColliderProcessor processor)
  {
    if (this.processorList.IndexOf(processor) >= 0)
      return;
    this.processorList.Add(processor);
  }

  public void RemoveProcessor(AttackColliderProcessor processor)
  {
    this.processorList.Remove(processor);
  }

  private void FixedUpdate()
  {
    if (this.processorList.Count <= 0)
      return;
    this.tempList.Clear();
    if (this.tempList.Capacity < this.processorList.Count)
      this.tempList.Capacity = this.processorList.Count;
    int index1 = 0;
    for (int count = this.processorList.Count; index1 < count; ++index1)
      this.tempList.Add(this.processorList[index1]);
    int index2 = 0;
    for (int count = this.tempList.Count; index2 < count; ++index2)
      this.tempList[index2].OnFixedUpdate();
  }
}
