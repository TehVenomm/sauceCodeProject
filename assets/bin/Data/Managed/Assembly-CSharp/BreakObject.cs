// Decompiled with JetBrains decompiler
// Type: BreakObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BreakObject : GimmickObject
{
  [SerializeField]
  protected string breakEffectName = "";

  protected override bool IsValidAttackedHit(StageObject from_object)
  {
    return base.IsValidAttackedHit(from_object);
  }

  protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
  {
    base.OnAttackedHitLocal(status);
    status.damage = (int) status.attackInfo.atk.normal;
    status.damage += (int) status.attackInfo.atk.fire;
    status.damage += (int) status.attackInfo.atk.water;
    status.damage += (int) status.attackInfo.atk.thunder;
    status.damage += (int) status.attackInfo.atk.soil;
    status.damage += (int) status.attackInfo.atk.light;
    status.damage += (int) status.attackInfo.atk.dark;
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    base.OnAttackedHitFix(status);
    if (!this.IsBreakable(status))
      return;
    EffectManager.OneShot(this.breakEffectName, this._position, this._rotation);
    ((Component) this).gameObject.SetActive(false);
  }

  protected bool IsBreakable(AttackedHitStatusFix _fixedStatus)
  {
    if (_fixedStatus.attackInfo != null && _fixedStatus.attackInfo.isBreakObject || _fixedStatus.fromType == StageObject.OBJECT_TYPE.ENEMY && _fixedStatus.damage > 0)
      return true;
    return _fixedStatus.fromType == StageObject.OBJECT_TYPE.PLAYER && this.IsBreakableAttack(_fixedStatus.attackInfo.attackType);
  }

  protected bool IsBreakableAttack(AttackHitInfo.ATTACK_TYPE _type)
  {
    switch (_type)
    {
      case AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT:
      case AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST:
        return true;
      default:
        return false;
    }
  }
}
