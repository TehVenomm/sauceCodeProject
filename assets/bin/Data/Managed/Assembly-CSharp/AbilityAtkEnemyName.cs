// Decompiled with JetBrains decompiler
// Type: AbilityAtkEnemyName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AbilityAtkEnemyName : AbilityAtkBase
{
  private string name;

  public override void init(Player _player, string target, int val)
  {
    base.init(_player, target, val);
    this.name = target;
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    Enemy enemy = chara as Enemy;
    if (Object.op_Equality((Object) enemy, (Object) null))
      return (AtkAttribute) null;
    return this.name != enemy.enemyTableData.name ? (AtkAttribute) null : this.attr;
  }
}
