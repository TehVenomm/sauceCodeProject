// Decompiled with JetBrains decompiler
// Type: Goal_RaiseAlly
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_RaiseAlly : GoalComposite
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.RAISE_ALLY;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    this.RemoveAllSubGoals(brain);
    if (!brain.targetCtrl.CanRescueOfTargetAlly())
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      float num1 = 2f;
      if (brain.owner is Player)
        num1 = (brain.owner as Player).playerParameter.revivalRange;
      float num2 = num1 * 0.7f;
      brain.moveCtrl.ChangeStopRange(num2);
      Character allyTarget = brain.targetCtrl.GetAllyTarget() as Character;
      if ((double) AIUtility.GetLengthWithBetweenObject((StageObject) brain.owner, (StageObject) allyTarget) < (double) num2)
        this.AddSubGoal<Goal_Prayer>().SetPrayer(allyTarget, num2);
      else
        this.AddSubGoal<Goal_GoToAlly>();
    }
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    Goal.STATUS status = this.UpdateSubGoals(brain);
    this.SetStatus(status);
    if (status == Goal.STATUS.COMPLETED && brain.targetCtrl.CanRescueOfTargetAlly())
      this.SetStatus(Goal.STATUS.INACTIVE);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
    brain.moveCtrl.ResetStopRange();
    brain.targetCtrl.SetAllyTarget((StageObject) null);
  }

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param = null)
  {
    base.HandleEvent(brain, ev, param);
    if (ev != BRAIN_EVENT.BULLET_CATCH || this.IsNowProcess(GOAL_TYPE.ENSURE_SAFETY) || !Object.op_Inequality((Object) brain.dangerRader, (Object) null))
      return;
    brain.dangerRader.AskWillBulletHit();
  }
}
