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
		StageObject currentTarget = brain.targetCtrl.GetCurrentTarget();
		float num = EvaluateDistanceWithObject(brain, currentTarget);
		float num2 = EvaluateDangerWithTargetCondition(brain, currentTarget);
		float num3 = EvaluateDangerWithTargetPlace(brain, currentTarget);
		return (0f + (1f - num) * (num2 * num3)) * base.bias;
	}

	public override void SetGoal(Brain brain, Goal_Think think)
	{
		think.AddGoal_SeeTarget(brain);
	}
}
