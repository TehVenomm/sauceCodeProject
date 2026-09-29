// Decompiled with JetBrains decompiler
// Type: IAttackCollider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public interface IAttackCollider
{
  float GetTime();

  bool IsEnable();

  void SortHitStackList(
    List<AttackHitColliderProcessor.HitResult> stack_list);

  Vector3 GetCrossCheckPoint(Collider from_collider);

  bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object);

  void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param);

  AttackInfo GetAttackInfo();

  StageObject GetFromObject();
}
