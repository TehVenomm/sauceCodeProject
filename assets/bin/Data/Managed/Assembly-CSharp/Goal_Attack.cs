public class Goal_Attack : Goal
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.ATTACK;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		brain.weaponCtrl.AttackOn();
	}

	protected override STATUS Process(Brain brain)
	{
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.weaponCtrl.AttackOff();
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
	{
		base.HandleEvent(brain, ev, param);
		switch (ev)
		{
		case BRAIN_EVENT.PLAY_MOTION:
		{
			int num = (int)param;
			if (num >= 15 && num <= 114)
			{
				SetStatus(STATUS.COMPLETED);
			}
			break;
		}
		case BRAIN_EVENT.END_ACTION:
			if ((int)param == 6)
			{
				SetStatus(STATUS.COMPLETED);
			}
			break;
		}
	}
}
