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
		double num = AIUtility.GetLengthWithBetweenObject(brain.owner, target);
		if (num > (double)revival_range)
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
			StageObject stageObject = (StageObject)param;
			if (target == stageObject)
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
