// Decompiled with JetBrains decompiler
// Type: Goal_Avoid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Goal_Avoid : Goal
{
  private PLACE place;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.AVOID;

  public void SetPlace(PLACE place) => this.place = place;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!(brain.owner is Player))
      this.SetStatus(Goal.STATUS.COMPLETED);
    else if (!brain.moveCtrl.CanPlaceAvoid(this.place))
    {
      this.SetStatus(Goal.STATUS.COMPLETED);
    }
    else
    {
      brain.moveCtrl.AvoidOn();
      brain.moveCtrl.SetAvoid(this.place);
    }
  }

  protected override Goal.STATUS Process(Brain brain) => this.status;

  protected override void Terminate(Brain brain) => brain.moveCtrl.AvoidOff();

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    switch (ev)
    {
      case BRAIN_EVENT.PLAY_MOTION:
        if ((int) param != 115)
          break;
        this.SetStatus(Goal.STATUS.COMPLETED);
        break;
      case BRAIN_EVENT.END_ACTION:
        if ((int) param != 13)
          break;
        this.SetStatus(Goal.STATUS.COMPLETED);
        break;
    }
  }

  public override string ToStringGoal() => $"{base.ToStringGoal()} place={(object) this.place}";
}
