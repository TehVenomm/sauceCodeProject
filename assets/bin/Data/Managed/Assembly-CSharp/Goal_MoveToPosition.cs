// Decompiled with JetBrains decompiler
// Type: Goal_MoveToPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_MoveToPosition : GoalComposite
{
  private float moveLen;
  private float margin;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.MOVE_TO_POSITION;

  private Vector3 targetPos { get; set; }

  public Goal_MoveToPosition SetParam(Vector3 pos, float move_len)
  {
    this.targetPos = pos;
    this.moveLen = move_len;
    return this;
  }

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    Vector3 vector3 = Vector3.op_Subtraction(this.targetPos, brain.owner._transform.position);
    vector3.y = 0.0f;
    this.margin = 0.0f;
    if ((double) this.moveLen > 0.0)
    {
      float num = brain.owner.moveStopRange + this.moveLen;
      float magnitude = ((Vector3) ref vector3).magnitude;
      if ((double) magnitude > (double) num)
      {
        vector3 = Vector3.op_Multiply(vector3, num / magnitude);
        this.margin = this.moveLen / 3f;
      }
    }
    this.targetPos = Vector3.op_Addition(brain.owner._transform.position, vector3);
    this.AddSubGoal<Goal_Move>().SetStick(Vector2.up, this.targetPos).SetGiveupTime(this.moveLen + 0.5f);
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (brain.owner.IsArrivalPosition(this.targetPos, this.margin))
      this.SetStatus(Goal.STATUS.COMPLETED);
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
