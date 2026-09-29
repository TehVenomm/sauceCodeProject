// Decompiled with JetBrains decompiler
// Type: Goal_Move
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_Move : Goal
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.MOVE;

  private Vector2 stickVec { get; set; }

  private Vector3 targetPos { get; set; }

  public Goal_Move SetStick(Vector2 stick, Vector3 pos)
  {
    this.stickVec = stick;
    this.targetPos = pos;
    return this;
  }

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    brain.moveCtrl.SeekOn();
    brain.moveCtrl.SetSeek(this.stickVec, this.targetPos);
  }

  protected override Goal.STATUS Process(Brain brain) => this.status;

  protected override void Terminate(Brain brain) => brain.moveCtrl.SeekOff();

  public override string ToStringGoal()
  {
    return $"{base.ToStringGoal()} targetPos={(object) this.targetPos}";
  }
}
