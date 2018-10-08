public class GoalEvaluator_SeeTarget : GoalEvaluator
{
	public GoalEvaluator_SeeTarget(float bias)
		: base(bias)
	{
	}

	public override float CalcEvaluateValue(Brain brain)
	{
		if (!brain.targetCtrl.IsAliveTarget())
		{
			return 0f;
		}
		float num = 0f;
		StageObject currentTarget = brain.targetCtrl.GetCurrentTarget();
		float num2 = EvaluateDistanceWithObject(brain, currentTarget);
		float num3 = EvaluateDangerWithTargetCondition(brain, currentTarget);
		float num4 = EvaluateDangerWithTargetPlace(brain, currentTarget);
		num += (1f - num2) * (num3 * num4);
		return num * base.bias;
	}

	public override void SetGoal(Brain brain, Goal_Think think)
	{
		think.AddGoal_SeeTarget(brain);
	}
}
