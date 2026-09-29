// Decompiled with JetBrains decompiler
// Type: Goal_KillTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Goal_KillTarget : GoalComposite
{
  protected override GOAL_TYPE GetGoalType() => GOAL_TYPE.KILL_TARGET;

  protected override void Activate(Brain brain)
  {
    this.SetStatus(Goal.STATUS.ACTIVE);
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    else
      this.NPCAvtive(brain);
  }

  private void NPCAvtive(Brain brain)
  {
    brain.moveCtrl.ChangeStopRange(brain.weaponCtrl.GetAttackReach());
    if (brain.targetCtrl.CanAttackTarget())
      this.AddSubGoal<Goal_AttackTarget>();
    else
      this.AddSubGoal<Goal_GoToTarget>();
  }

  protected override Goal.STATUS Process(Brain brain)
  {
    this.SetStatus(this.UpdateSubGoals(brain));
    if (!brain.targetCtrl.IsAliveTarget())
      this.SetStatus(Goal.STATUS.COMPLETED);
    return this.status;
  }

  protected override void Terminate(Brain brain) => brain.moveCtrl.ResetStopRange();

  public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param = null)
  {
    base.HandleEvent(brain, ev, param);
    switch (ev)
    {
      case BRAIN_EVENT.BULLET_CATCH:
        if (this.IsNowProcess(GOAL_TYPE.ENSURE_SAFETY) || !Object.op_Inequality((Object) brain.dangerRader, (Object) null) || !brain.dangerRader.AskWillHit())
          break;
        this.RemoveAllSubGoals(brain);
        this.AddSubGoal<Goal_EnsureSafety>();
        break;
      case BRAIN_EVENT.COLLIDER_CATCH:
        Vector3 attackPosition = brain.targetCtrl.GetAttackPosition();
        if (this.IsNowProcess(GOAL_TYPE.ENSURE_SAFETY) || !Object.op_Inequality((Object) brain.dangerRader, (Object) null) || !brain.dangerRader.AskDangerPosition(attackPosition))
          break;
        this.RemoveAllSubGoals(brain);
        this.AddSubGoal<Goal_EnsureSafety>();
        break;
    }
  }
}
