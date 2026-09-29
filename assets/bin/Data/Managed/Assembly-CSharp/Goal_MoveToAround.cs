// Decompiled with JetBrains decompiler
// Type: Goal_MoveToAround
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_MoveToAround : GoalComposite
{
  private PLACE place = PLACE.LEFT;
  private float moveLen;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.MOVE_TO_AROUND;

  private Vector3 targetPos { get; set; }

  public Goal_MoveToAround SetParam(PLACE place, Vector3 pos, float len)
  {
    this.place = place;
    this.targetPos = pos;
    this.moveLen = len;
    return this;
  }

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    this.AddSubGoal<Goal_Move>().SetStick(this.place.GetVector2(), this.targetPos).SetGiveupTime(this.moveLen * 0.7f);
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }

  public override string ToStringGoal()
  {
    string str = $"targetPos={this.targetPos}, moveLen={this.moveLen}";
    return base.ToStringGoal() + str;
  }
}
