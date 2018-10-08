public class GoalEvaluator_RaiseAlly : GoalEvaluator
{
	public GoalEvaluator_RaiseAlly(float bias)
		: base(bias)
	{
	}

	public override float CalcEvaluateValue(Brain brain)
	{
		if (!(brain.owner is Player))
		{
			return 0f;
		}
		if (!brain.targetCtrl.CanReviveOfTargetAlly())
		{
			return 0f;
		}
		float num = 1f;
		return num * base.bias;
	}

	public override void SetGoal(Brain brain, Goal_Think think)
	{
		think.AddGoal_RaiseAlly(brain);
	}
}
