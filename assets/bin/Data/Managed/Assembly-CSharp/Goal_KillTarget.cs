using UnityEngine;

public class Goal_KillTarget : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.KILL_TARGET;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			NPCAvtive(brain);
		}
	}

	private void NPCAvtive(Brain brain)
	{
		brain.moveCtrl.ChangeStopRange(brain.weaponCtrl.GetAttackReach());
		if (brain.targetCtrl.CanAttackTarget())
		{
			AddSubGoal<Goal_AttackTarget>();
		}
		else
		{
			AddSubGoal<Goal_GoToTarget>();
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.ResetStopRange();
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param = null)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		base.HandleEvent(brain, ev, param);
		switch (ev)
		{
		case BRAIN_EVENT.BULLET_CATCH:
			if (!IsNowProcess(GOAL_TYPE.ENSURE_SAFETY) && brain.dangerRader != null && brain.dangerRader.AskWillHit())
			{
				RemoveAllSubGoals(brain);
				AddSubGoal<Goal_EnsureSafety>();
			}
			break;
		case BRAIN_EVENT.COLLIDER_CATCH:
		{
			Vector3 attackPosition = brain.targetCtrl.GetAttackPosition();
			if (!IsNowProcess(GOAL_TYPE.ENSURE_SAFETY) && brain.dangerRader != null && brain.dangerRader.AskDangerPosition(attackPosition))
			{
				RemoveAllSubGoals(brain);
				AddSubGoal<Goal_EnsureSafety>();
			}
			break;
		}
		}
	}
}
