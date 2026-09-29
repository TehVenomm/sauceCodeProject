// Decompiled with JetBrains decompiler
// Type: Goal_Prayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_Prayer : Goal
{
  private Character target;
  private float revival_range;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.PRAYER;

  public Goal_Prayer SetPrayer(Character target, float revival_range)
  {
    this.target = target;
    this.revival_range = revival_range;
    return this;
  }

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    brain.moveCtrl.StopOn();
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    if (Object.op_Equality((Object) this.target, (Object) null))
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
      return this.status;
    }
    if ((double) AIUtility.GetLengthWithBetweenObject((StageObject) brain.owner, (StageObject) this.target) > (double) this.revival_range)
      this.SetStatus(Goal.STATUS.COMPLETED);
    if (!this.target.isDead && !this.target.IsStone())
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
    brain.moveCtrl.StopOff();
    if (!Object.op_Inequality((Object) this.target, (Object) null))
      return;
    this.target = (Character) null;
  }

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    if (ev != BRAIN_EVENT.DESTROY_OBJECT || !Object.op_Equality((Object) this.target, (Object) param))
      return;
    this.target = (Character) null;
  }

  public override string ToStringGoal() => $"{base.ToStringGoal()} target={(object) this.target}";
}
