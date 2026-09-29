// Decompiled with JetBrains decompiler
// Type: Goal_Stop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_Stop : Goal
{
  private double giveupLen;

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.STOP;

  private Vector3 stopPos { get; set; }

  public Goal_Stop SetGiveupLen(float len)
  {
    this.giveupLen = (double) len;
    return this;
  }

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    brain.moveCtrl.StopOn();
    this.stopPos = brain.owner._transform.position;
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    if ((double) AIUtility.GetLengthWithBetweenPosition(brain.owner._transform.position, this.stopPos) > this.giveupLen)
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain) => brain.moveCtrl.StopOff();
}
