// Decompiled with JetBrains decompiler
// Type: GeneratedAttackObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GeneratedAttackObject : AttackColliderObject
{
  public override bool CheckHitAttack(
    AttackHitInfo info,
    Collider to_collider,
    StageObject to_object)
  {
    return (info.attackType != AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED || !(to_object is Player) || to_object is Self) && base.CheckHitAttack(info, to_collider, to_object);
  }
}
