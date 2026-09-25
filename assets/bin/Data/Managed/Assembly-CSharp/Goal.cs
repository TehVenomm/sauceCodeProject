// Decompiled with JetBrains decompiler
// Type: Goal
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Diagnostics;
using UnityEngine;

#nullable disable
public abstract class Goal : Poolable
{
  protected Goal.STATUS status;
  public float giveupTime;

  public override void OnAwake() => this.goalType = this.GetGoalType();

  public override void OnInit()
  {
    this.status = Goal.STATUS.INACTIVE;
    this.giveupTime = 0.0f;
  }

  public override void OnFinal()
  {
  }

  public static T Alloc<T>() where T : Goal, new()
  {
    if (Singleton<GoalPool>.IsValid())
      return Singleton<GoalPool>.I.Alloc<T>();
    T obj = new T();
    obj.OnAwake();
    obj.OnInit();
    return obj;
  }

  public static void Free(Goal goal)
  {
    if (Singleton<GoalPool>.IsValid())
    {
      Singleton<GoalPool>.I.Free(goal);
    }
    else
    {
      goal.OnFinal();
      goal = (Goal) null;
    }
  }

  public GOAL_TYPE goalType { get; private set; }

  protected abstract GOAL_TYPE GetGoalType();

  public bool isInactive => this.status == Goal.STATUS.INACTIVE;

  public bool isActive => this.status == Goal.STATUS.ACTIVE;

  public bool isComplete => this.status == Goal.STATUS.COMPLETED;

  public bool isFailed => this.status == Goal.STATUS.FAILED;

  protected void SetStatus(Goal.STATUS status) => this.status = status;

  public void SetGiveupTime(float time) => this.giveupTime = Time.time + time;

  public bool IsGiveupTime()
  {
    return (double) this.giveupTime > 0.0 && (double) Time.time > (double) this.giveupTime;
  }

  protected abstract void Activate(Brain brain);

  protected abstract Goal.STATUS Process(Brain brain);

  protected abstract void Terminate(Brain brain);

  public virtual Goal.STATUS Update(Brain brain)
  {
    if (this.isInactive)
      this.Activate(brain);
    if (!this.IsGiveupTime())
      return this.Process(brain);
    this.SetStatus(Goal.STATUS.FAILED);
    return this.status;
  }

  public virtual void Destroy(Brain brain)
  {
    this.Terminate(brain);
    Goal.Free(this);
  }

  public virtual void HandleEvent(Brain brain, BRAIN_EVENT ev, object param = null)
  {
  }

  public override string ToString() => this.ToStringGoal();

  public virtual string ToStringGoal()
  {
    return $"Goal[ {this.goalType} ]: status={this.status}{((double) this.giveupTime > 0.0 ? (object) (" " + (this.giveupTime - Time.time).ToString("F2")) : (object) "")}.";
  }

  [Conditional("GOAL_LOG")]
  protected void logd(string log)
  {
  }

  public enum STATUS
  {
    INACTIVE,
    ACTIVE,
    COMPLETED,
    FAILED,
  }
}
