// Decompiled with JetBrains decompiler
// Type: Goal_SpecialAttack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_SpecialAttack : Goal
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.SPECIAL_ATTACK;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!(brain.owner is Player))
      this.SetStatus(Goal.STATUS.COMPLETED);
    else
      brain.weaponCtrl.SpecialOn();
  }

  protected override Goal.STATUS Process(Brain brain) => this.status;

  protected override void Terminate(Brain brain) => brain.weaponCtrl.SpecialOff();

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    Player owner = brain.owner as Player;
    if (ev != BRAIN_EVENT.END_ACTION)
      return;
    int num = (int) param;
    if (!Object.op_Inequality((Object) owner, (Object) null) || num != 6)
      return;
    if (owner.attackMode == Player.ATTACK_MODE.ARROW && brain.weaponCtrl.isFullCharge)
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      if (!owner.isActSpecialAction)
        return;
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
  }
}
