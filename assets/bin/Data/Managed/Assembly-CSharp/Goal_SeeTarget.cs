// Decompiled with JetBrains decompiler
// Type: Goal_SeeTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_SeeTarget : GoalComposite
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.SEE_TARGET;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    else if (brain.targetCtrl.IsAttackableTarget() && brain.moveCtrl.CanBackAvoid())
    {
      this.AddSubGoal<Goal_Avoid>().SetPlace(PLACE.BACK);
    }
    else
    {
      float range = brain.weaponCtrl.GetAttackReach() * 2f;
      brain.moveCtrl.ChangeStopRange(range);
      if (!brain.targetCtrl.IsArrivalTarget())
      {
        this.AddSubGoal<Goal_GoToTarget>();
      }
      else
      {
        Vector3 targetPosition = brain.targetCtrl.GetTargetPosition();
        bool flag = false;
        if (Utility.Dice100(80 /*0x50*/))
        {
          if (Utility.Coin())
          {
            PLACE place = Utility.Coin() ? PLACE.LEFT : PLACE.RIGHT;
            this.AddSubGoal<Goal_MoveToAround>().SetParam(place, targetPosition, 3f);
          }
          else if (brain.moveCtrl.CanRightAvoid() || brain.moveCtrl.CanLeftAvoid())
            this.AddSubGoal<Goal_AvoidRightAndLeft>();
          else
            flag = true;
        }
        else
          flag = true;
        if (!flag)
          return;
        this.AddSubGoal<Goal_Stop>().SetGiveupTime(1f);
      }
    }
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain) => brain.moveCtrl.ResetStopRange();
}
