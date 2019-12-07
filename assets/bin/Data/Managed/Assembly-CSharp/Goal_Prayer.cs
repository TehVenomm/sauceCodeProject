public class Goal_Prayer : Goal
{
	private Character target;

	private float revival_range;

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.PRAYER;
	}

	public Goal_Prayer SetPrayer(Character target, float revival_range)
	{
		this.target = target;
		this.revival_range = revival_range;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		brain.moveCtrl.StopOn();
	}

	protected override STATUS Process(Brain brain)
	{
		if (target == null)
		{
			SetStatus(STATUS.COMPLETED);
			return status;
		}
		if ((double)AIUtility.GetLengthWithBetweenObject(brain.owner, target) > (double)revival_range)
		{
			SetStatus(STATUS.COMPLETED);
		}
		if (!target.isDead && !target.IsStone())
		{
			SetStatus(STATUS.COMPLETED);
		}
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.StopOff();
		if (target != null)
		{
			target = null;
		}
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
	{
		base.HandleEvent(brain, ev, param);
		if (ev == BRAIN_EVENT.DESTROY_OBJECT)
		{
			StageObject y = (StageObject)param;
			if (target == y)
			{
				target = null;
			}
		}
	}

	public override string ToStringGoal()
	{
		return base.ToStringGoal() + " target=" + target;
	}
}
