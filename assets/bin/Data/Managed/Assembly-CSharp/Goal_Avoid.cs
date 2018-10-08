public class Goal_Avoid : Goal
{
	private PLACE place;

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.AVOID;
	}

	public void SetPlace(PLACE place)
	{
		this.place = place;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!(brain.owner is Player))
		{
			SetStatus(STATUS.COMPLETED);
		}
		else if (!brain.moveCtrl.CanPlaceAvoid(place))
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			brain.moveCtrl.AvoidOn();
			brain.moveCtrl.SetAvoid(place);
		}
	}

	protected override STATUS Process(Brain brain)
	{
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.AvoidOff();
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
	{
		base.HandleEvent(brain, ev, param);
		switch (ev)
		{
		case BRAIN_EVENT.PLAY_MOTION:
		{
			int num2 = (int)param;
			if (num2 == 115)
			{
				SetStatus(STATUS.COMPLETED);
			}
			break;
		}
		case BRAIN_EVENT.END_ACTION:
		{
			int num = (int)param;
			if (num == 12)
			{
				SetStatus(STATUS.COMPLETED);
			}
			break;
		}
		}
	}

	public override string ToStringGoal()
	{
		return base.ToStringGoal() + " place=" + place;
	}
}
