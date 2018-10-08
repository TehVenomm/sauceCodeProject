public class Goal_AvoidRightAndLeft : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.AVOID_RIGHT_AND_LEFT;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!(brain.owner is Player))
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			PLACE pLACE = (!Utility.Coin()) ? PLACE.RIGHT : PLACE.LEFT;
			if (!brain.moveCtrl.CanPlaceAvoid(pLACE))
			{
				pLACE = ((pLACE == PLACE.LEFT) ? PLACE.RIGHT : PLACE.LEFT);
			}
			AddSubGoal<Goal_Avoid>().SetPlace(pLACE);
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
	}
}
