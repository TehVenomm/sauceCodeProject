using UnityEngine;

public class Goal_GoToTarget : GoalComposite
{
	private const float FAULT_LENGTH = 3f;

	private Vector3 targetPos = Vector3.get_zero();

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.GO_TO_TARGET;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			targetPos = brain.targetCtrl.GetAttackPosition();
			float num = 3f;
			if (!brain.moveCtrl.CanSeekToOpponent(targetPos, num))
			{
				PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
				RaycastHit seekHit = brain.moveCtrl.seekHit;
				Vector3 position = seekHit.get_transform().get_position();
				AddSubGoal<Goal_MoveToAround>().SetParam(place, position, num);
			}
			else if (!brain.targetCtrl.IsArrivalAttackPosition())
			{
				AddSubGoal<Goal_MoveToPosition>().SetParam(targetPos, num);
			}
			else
			{
				SetStatus(STATUS.COMPLETED);
			}
		}
	}

	protected override STATUS Process(Brain brain)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		if (brain.targetCtrl.GetLengthWithAttackPos(targetPos) > 3f)
		{
			SetStatus(STATUS.FAILED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
	}

	public override string ToStringGoal()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		string str = $" target={targetPos}";
		return base.ToStringGoal() + str;
	}
}
