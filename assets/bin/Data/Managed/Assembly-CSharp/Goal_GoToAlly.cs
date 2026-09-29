// Decompiled with JetBrains decompiler
// Type: Goal_GoToAlly
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_GoToAlly : GoalComposite
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.GO_TO_ALLY;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!brain.targetCtrl.CanRescueOfTargetAlly())
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      Vector3 position1 = brain.targetCtrl.GetAllyTarget()._transform.position;
      float num = 3f;
      if (!brain.moveCtrl.CanSeekToAlly(position1, num))
      {
        PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
        RaycastHit seekHit = brain.moveCtrl.seekHit;
        Vector3 position2 = ((RaycastHit) ref seekHit).transform.position;
        this.AddSubGoal<Goal_MoveToAround>().SetParam(place, position2, num);
      }
      else if (!brain.owner.IsArrivalPosition(position1))
        this.AddSubGoal<Goal_MoveToPosition>().SetParam(position1, num);
      else
        this.SetStatus(Goal.STATUS.COMPLETED);
    }
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (!brain.targetCtrl.CanRescueOfTargetAlly())
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }
}
