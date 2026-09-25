// Decompiled with JetBrains decompiler
// Type: Goal_EnsureSafety
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_EnsureSafety : GoalComposite
{
  private const float GUARD_GIVEUP_TIME = 5f;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.ENSURE_SAFETY;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    this.RemoveAllSubGoals(brain);
    if (Object.op_Inequality((Object) brain.dangerRader, (Object) null) && brain.dangerRader.AskDanger())
    {
      if (brain.dangerRader.AskWillBulletHit())
        this.AddSubGoal<Goal_AvoidRightAndLeft>();
      else if (brain.dangerRader.AskWillDashHit())
      {
        if (brain.weaponCtrl.IsGuardAttack())
        {
          this.AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
        }
        else
        {
          PLACE place = brain.dangerRader.GetSafetySide();
          if (!brain.moveCtrl.CanPlaceAvoid(place))
            place = place == PLACE.LEFT ? PLACE.RIGHT : PLACE.LEFT;
          this.AddSubGoal<Goal_Avoid>().SetPlace(place);
          this.AddSubGoal<Goal_Avoid>().SetPlace(place);
        }
      }
      else if (brain.dangerRader.AskDangerPosition(brain.owner._position))
      {
        if (brain.weaponCtrl.IsGuardAttack())
        {
          this.AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
        }
        else
        {
          PLACE safetyPlace = brain.dangerRader.GetSafetyPlace();
          this.AddSubGoal<Goal_Avoid>().SetPlace(safetyPlace);
        }
      }
      else if (brain.dangerRader.AskDangerPosition(brain.moveCtrl.targetPos))
      {
        PLACE safetySide = brain.dangerRader.GetSafetySide();
        this.AddSubGoal<Goal_MoveToAround>().SetParam(safetySide, brain.moveCtrl.targetPos, 3f);
      }
      else if (brain.weaponCtrl.IsGuardAttack())
        this.AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
      else if (Utility.Coin())
      {
        PLACE safetySide = brain.dangerRader.GetSafetySide();
        this.AddSubGoal<Goal_Avoid>().SetPlace(safetySide);
      }
      else
        this.AddSubGoal<Goal_Stop>().SetGiveupTime(0.3f);
    }
    else
      this.SetStatus(Goal.STATUS.COMPLETED);
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (Object.op_Inequality((Object) brain.dangerRader, (Object) null) && !brain.dangerRader.AskDanger(1f))
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }
}
