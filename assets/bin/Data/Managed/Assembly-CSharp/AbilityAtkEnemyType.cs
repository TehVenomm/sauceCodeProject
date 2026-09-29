// Decompiled with JetBrains decompiler
// Type: AbilityAtkEnemyType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AbilityAtkEnemyType : AbilityAtkBase
{
  private ENEMY_TYPE type;

  public override void init(Player _player, string target, int val)
  {
    base.init(_player, target, val);
    this.type = ENEMY_TYPE.NONE;
    if (!Enum.IsDefined(typeof (ENEMY_TYPE), (object) target))
      return;
    this.type = (ENEMY_TYPE) Enum.Parse(typeof (ENEMY_TYPE), target);
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    Enemy enemy = chara as Enemy;
    if (Object.op_Equality((Object) enemy, (Object) null))
      return (AtkAttribute) null;
    return this.type != enemy.GetEnemyType() ? (AtkAttribute) null : this.attr;
  }
}
