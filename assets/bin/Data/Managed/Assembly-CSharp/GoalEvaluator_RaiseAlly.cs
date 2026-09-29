// Decompiled with JetBrains decompiler
// Type: GoalEvaluator_RaiseAlly
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class GoalEvaluator_RaiseAlly(float bias) : GoalEvaluator(bias)
{
  public override float CalcEvaluateValue(Brain brain)
  {
    return !(brain.owner is Player) || !brain.targetCtrl.CanRescueOfTargetAlly() ? 0.0f : 1f * this.bias;
  }

  public override void SetGoal(Brain brain, Goal_Think think) => think.AddGoal_RaiseAlly(brain);
}
