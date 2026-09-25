// Decompiled with JetBrains decompiler
// Type: Goal_AvoidRightAndLeft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Goal_AvoidRightAndLeft : GoalComposite
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.AVOID_RIGHT_AND_LEFT;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!(brain.owner is Player))
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      PLACE place = Utility.Coin() ? PLACE.LEFT : PLACE.RIGHT;
      if (!brain.moveCtrl.CanPlaceAvoid(place))
        place = place == PLACE.LEFT ? PLACE.RIGHT : PLACE.LEFT;
      this.AddSubGoal<Goal_Avoid>().SetPlace(place);
    }
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }
}
