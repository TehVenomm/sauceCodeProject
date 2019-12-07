public class Goal_RaiseAlly : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.RAISE_ALLY;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		RemoveAllSubGoals(brain);
		if (!brain.targetCtrl.CanRescueOfTargetAlly())
		{
			SetStatus(STATUS.COMPLETED);
			return;
		}
		float num = 2f;
		if (brain.owner is Player)
		{
			num = (brain.owner as Player).playerParameter.revivalRange;
		}
		num *= 0.7f;
		brain.moveCtrl.ChangeStopRange(num);
		Character target = brain.targetCtrl.GetAllyTarget() as Character;
		if (AIUtility.GetLengthWithBetweenObject(brain.owner, target) < num)
		{
			AddSubGoal<Goal_Prayer>().SetPrayer(target, num);
		}
		else
		{
			AddSubGoal<Goal_GoToAlly>();
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS sTATUS = UpdateSubGoals(brain);
		SetStatus(sTATUS);
		if (sTATUS == STATUS.COMPLETED && brain.targetCtrl.CanRescueOfTargetAlly())
		{
			SetStatus(STATUS.INACTIVE);
		}
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.ResetStopRange();
		brain.targetCtrl.SetAllyTarget(null);
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param = null)
	{
		base.HandleEvent(brain, ev, param);
		if (ev == BRAIN_EVENT.BULLET_CATCH && !IsNowProcess(GOAL_TYPE.ENSURE_SAFETY) && brain.dangerRader != null)
		{
			brain.dangerRader.AskWillBulletHit();
		}
	}
}
