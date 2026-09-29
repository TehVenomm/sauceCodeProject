// Decompiled with JetBrains decompiler
// Type: Goal_Think
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class Goal_Think : GoalComposite
{
  private SpanTimer choiceGoalSpanTimer;
  private Goal_Think.EVAL_FLAG evalFlag;
  private List<GoalEvaluator> evaluators = new List<GoalEvaluator>();

  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.THINK;

  public Goal_Think() => this.InitEvaluator();

  public Goal_Think SetChoiceGoalSpanTimer(float span)
  {
    this.choiceGoalSpanTimer = new SpanTimer(span);
    return this;
  }

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    this.ChoiceGoalFromEvaluator(brain);
    if (this.choiceGoalSpanTimer == null)
      return;
    this.choiceGoalSpanTimer.ResetNextTime();
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    switch (this.UpdateSubGoals(brain))
    {
      case Goal.STATUS.COMPLETED:
      case Goal.STATUS.FAILED:
        this.SetStatus(Goal.STATUS.INACTIVE);
        break;
    }
    if (this.choiceGoalSpanTimer != null && this.choiceGoalSpanTimer.IsReady())
      this.ChoiceGoalFromEvaluator(brain);
    return this.status;
  }

  protected override void Terminate(Brain brain)
  {
  }

  private void FlagOn(Goal_Think.EVAL_FLAG flag) => this.evalFlag |= flag;

  private void FlagOff(Goal_Think.EVAL_FLAG flag) => this.evalFlag &= ~flag;

  private bool FlagIsOn(Goal_Think.EVAL_FLAG flag) => (this.evalFlag & flag) == flag;

  public void KillTargetOn() => this.FlagOn(Goal_Think.EVAL_FLAG.KILL_TARGET);

  public void SeeTargetOn() => this.FlagOn(Goal_Think.EVAL_FLAG.SEE_TARGET);

  public void RaiseAllyOn() => this.FlagOn(Goal_Think.EVAL_FLAG.RAISE_ALLY);

  public void KillTargetOff() => this.FlagOff(Goal_Think.EVAL_FLAG.KILL_TARGET);

  public void SeeTargetOff() => this.FlagOff(Goal_Think.EVAL_FLAG.SEE_TARGET);

  public void RaiseAllyOff() => this.FlagOff(Goal_Think.EVAL_FLAG.RAISE_ALLY);

  private void InitEvaluator()
  {
    this.evaluators.Add((GoalEvaluator) new GoalEvaluator_KillTarget(1f));
    this.evaluators.Add((GoalEvaluator) new GoalEvaluator_SeeTarget(1f));
    this.evaluators.Add((GoalEvaluator) new GoalEvaluator_RaiseAlly(1f));
  }

  public void ChoiceGoalFromEvaluator(Brain brain)
  {
    GoalEvaluator goalEvaluator = (GoalEvaluator) null;
    float num1 = 0.0f;
    int index = 0;
    for (int count = this.evaluators.Count; index < count; ++index)
    {
      GoalEvaluator evaluator = this.evaluators[index];
      if (this.FlagIsOn((Goal_Think.EVAL_FLAG) (1 << index)))
      {
        float num2 = evaluator.CalcEvaluateValue(brain);
        if ((double) num2 > (double) num1)
        {
          num1 = num2;
          goalEvaluator = evaluator;
        }
      }
    }
    if (goalEvaluator != null)
    {
      goalEvaluator.SetGoal(brain, this);
    }
    else
    {
      if (this.GetCountSubGoal() > 0)
        return;
      this.AddGoal_Stop();
    }
  }

  public string ToStringEvaluator(Brain brain)
  {
    string str = this.evalFlag.ToString() + "\n";
    int index = 0;
    for (int count = this.evaluators.Count; index < count; ++index)
    {
      GoalEvaluator evaluator = this.evaluators[index];
      float num = evaluator.CalcEvaluateValue(brain);
      bool flag = this.FlagIsOn((Goal_Think.EVAL_FLAG) (1 << index));
      str = $"{str}{index.ToString() + (flag ? (object) "" : (object) "(Off)")}{(object) evaluator}: {(object) num}\n";
    }
    return str.TrimEnd((char[]) null);
  }

  public void AddGoal_SeeTarget(Brain brain)
  {
    if (this.IsNowProcess(GOAL_TYPE.SEE_TARGET))
      return;
    this.RemoveAllSubGoals(brain);
    this.AddSubGoal<Goal_SeeTarget>();
  }

  public void AddGoal_KillTarget(Brain brain)
  {
    if (this.IsNowProcess(GOAL_TYPE.KILL_TARGET))
      return;
    this.RemoveAllSubGoals(brain);
    this.AddSubGoal<Goal_KillTarget>();
  }

  public void AddGoal_RaiseAlly(Brain brain)
  {
    if (this.IsNowProcess(GOAL_TYPE.RAISE_ALLY))
      return;
    this.RemoveAllSubGoals(brain);
    this.AddSubGoal<Goal_RaiseAlly>();
  }

  public void AddGoal_Stop()
  {
    if (this.IsNowProcess(GOAL_TYPE.STOP))
      return;
    this.AddSubGoal<Goal_Stop>().SetGiveupLen(1f).SetGiveupTime(1f);
  }

  [Flags]
  public enum EVAL_FLAG
  {
    NONE = 0,
    KILL_TARGET = 1,
    SEE_TARGET = 2,
    RAISE_ALLY = 4,
  }
}
