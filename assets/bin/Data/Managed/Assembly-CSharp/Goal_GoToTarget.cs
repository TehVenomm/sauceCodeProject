// Decompiled with JetBrains decompiler
// Type: Goal_GoToTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_GoToTarget : GoalComposite
{
  private const float FAULT_LENGTH = 3f;
  private Vector3 targetPos = Vector3.zero;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.GO_TO_TARGET;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!brain.targetCtrl.IsAliveTarget())
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      this.targetPos = brain.targetCtrl.GetAttackPosition();
      float num = 3f;
      if (!brain.moveCtrl.CanSeekToOpponent(this.targetPos, num))
      {
        PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
        RaycastHit seekHit = brain.moveCtrl.seekHit;
        Vector3 position = ((RaycastHit) ref seekHit).transform.position;
        this.AddSubGoal<Goal_MoveToAround>().SetParam(place, position, num);
      }
      else if (!brain.targetCtrl.IsArrivalAttackPosition())
        this.AddSubGoal<Goal_MoveToPosition>().SetParam(this.targetPos, num);
      else
        this.SetStatus(Goal.STATUS.COMPLETED);
    }
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    if ((double) brain.targetCtrl.GetLengthWithAttackPos(this.targetPos) > 3.0)
      this.SetStatus(Goal.STATUS.FAILED);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }

  public override string ToStringGoal()
  {
    string str = $" target={this.targetPos}";
    return base.ToStringGoal() + str;
  }
}
