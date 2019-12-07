using UnityEngine;

public class Goal_GoToTarget : GoalComposite
{
	private const float FAULT_LENGTH = 3f;

	private Vector3 targetPos = Vector3.zero;

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.GO_TO_TARGET;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
			return;
		}
		targetPos = brain.targetCtrl.GetAttackPosition();
		float num = 3f;
		if (!brain.moveCtrl.CanSeekToOpponent(targetPos, num))
		{
			PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
			Vector3 position = brain.moveCtrl.seekHit.transform.position;
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

	protected override STATUS Process(Brain brain)
	{
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
		string str = $" target={targetPos}";
		return base.ToStringGoal() + str;
	}
}
