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
			return;
		}
		if (!brain.moveCtrl.CanPlaceAvoid(place))
		{
			SetStatus(STATUS.COMPLETED);
			return;
		}
		brain.moveCtrl.AvoidOn();
		brain.moveCtrl.SetAvoid(place);
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
			if ((int)param == 115)
			{
				SetStatus(STATUS.COMPLETED);
			}
			break;
		case BRAIN_EVENT.END_ACTION:
			if ((int)param == 13)
			{
				SetStatus(STATUS.COMPLETED);
			}
			break;
		}
	}

	public override string ToStringGoal()
	{
		return base.ToStringGoal() + " place=" + place;
	}
}
