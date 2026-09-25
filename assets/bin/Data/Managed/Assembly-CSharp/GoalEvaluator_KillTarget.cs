// Decompiled with JetBrains decompiler
// Type: GoalEvaluator_KillTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GoalEvaluator_KillTarget(float bias) : GoalEvaluator(bias)
{
  public override float CalcEvaluateValue(Brain brain)
  {
    if (!brain.targetCtrl.IsAliveTarget())
      return 0.0f;
    StageObject currentTarget = brain.targetCtrl.GetCurrentTarget();
    return (float) (0.0 + (1.0 - (double) this.EvaluateDistanceWithObject(brain, currentTarget)) * (1.0 - (double) this.EvaluateDangerWithTargetCondition(brain, currentTarget) * (double) this.EvaluateDangerWithTargetPlace(brain, currentTarget))) * this.bias;
  }

  public override void SetGoal(Brain brain, Goal_Think think) => think.AddGoal_KillTarget(brain);
}
