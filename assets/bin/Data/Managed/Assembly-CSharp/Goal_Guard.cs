public class Goal_Guard : Goal
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.GUARD;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		brain.weaponCtrl.GuardOn();
	}

	protected override STATUS Process(Brain brain)
	{
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.weaponCtrl.GuardOff();
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
	{
		base.HandleEvent(brain, ev, param);
		if (ev == BRAIN_EVENT.END_ACTION)
		{
			int num = (int)param;
			if (num == 19)
			{
				SetStatus(STATUS.COMPLETED);
			}
		}
	}
}
