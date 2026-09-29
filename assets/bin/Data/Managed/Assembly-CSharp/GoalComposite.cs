// Decompiled with JetBrains decompiler
// Type: GoalComposite
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public abstract class GoalComposite : Goal
{
  private Stack<Goal> subGoals = new Stack<Goal>();

  public override void Destroy(Brain brain)
  {
    base.Destroy(brain);
    this.RemoveAllSubGoals(brain);
  }

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
  {
    base.HandleEvent(brain, ev, param);
    this.HandleEventAllSubGoals(brain, ev, param);
  }

  private void HandleEventAllSubGoals(Brain brain, BRAIN_EVENT ev, object param)
  {
    foreach (Goal subGoal in this.subGoals)
      subGoal.HandleEvent(brain, ev, param);
  }

  public int GetCountSubGoal() => this.subGoals.Count;

  public bool IsNowProcess(GOAL_TYPE type)
  {
    return this.subGoals.Count > 0 && this.subGoals.Peek().goalType == type;
  }

  protected Goal.STATUS UpdateSubGoals(Brain brain)
  {
    while (this.subGoals.Count > 0 && (this.subGoals.Peek().isComplete || this.subGoals.Peek().isFailed))
      this.subGoals.Pop().Destroy(brain);
    if (this.subGoals.Count <= 0)
      return Goal.STATUS.COMPLETED;
    Goal.STATUS status = this.subGoals.Peek().Update(brain);
    if (status != Goal.STATUS.COMPLETED)
      return status;
    return this.subGoals.Count <= 1 ? Goal.STATUS.COMPLETED : Goal.STATUS.ACTIVE;
  }

  protected T AddSubGoal<T>() where T : Goal, new()
  {
    T obj = Goal.Alloc<T>();
    this.subGoals.Push((Goal) obj);
    return obj;
  }

  protected void RemoveAllSubGoals(Brain brain)
  {
    if (this.subGoals.Count <= 0)
      return;
    while (this.subGoals.Count > 0)
      this.subGoals.Pop().Destroy(brain);
    this.subGoals.Clear();
  }

  public override string ToString() => this.ToStringSubgoals(1, true);

  public string ToStringSubgoals(int layer, bool first)
  {
    string stringSubgoals = this.ToStringGoal() ?? "";
    foreach (Goal subGoal in this.subGoals)
    {
      stringSubgoals += "\n";
      for (int index = 0; index < layer; ++index)
        stringSubgoals += "  ";
      stringSubgoals += first ? "+" : "-";
      stringSubgoals = !(subGoal is GoalComposite) ? stringSubgoals + subGoal.ToStringGoal() : stringSubgoals + (subGoal as GoalComposite).ToStringSubgoals(layer + 1, first);
      first = false;
    }
    return stringSubgoals;
  }
}
