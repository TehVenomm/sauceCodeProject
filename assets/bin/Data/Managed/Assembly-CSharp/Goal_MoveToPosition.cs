using UnityEngine;

public class Goal_MoveToPosition : GoalComposite
{
	private float moveLen;

	private float margin;

	private Vector3 targetPos
	{
		get;
		set;
	}

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.MOVE_TO_POSITION;
	}

	public Goal_MoveToPosition SetParam(Vector3 pos, float move_len)
	{
		targetPos = pos;
		moveLen = move_len;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		Vector3 b = targetPos - brain.owner._transform.position;
		b.y = 0f;
		margin = 0f;
		if (moveLen > 0f)
		{
			float num = brain.owner.moveStopRange + moveLen;
			float magnitude = b.magnitude;
			if (magnitude > num)
			{
				b *= num / magnitude;
				margin = moveLen / 3f;
			}
		}
		targetPos = brain.owner._transform.position + b;
		AddSubGoal<Goal_Move>().SetStick(Vector2.up, targetPos).SetGiveupTime(moveLen + 0.5f);
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		if (brain.owner.IsArrivalPosition(targetPos, margin))
		{
			SetStatus(STATUS.COMPLETED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
	}

	public override string ToStringGoal()
	{
		string str = $"targetPos={targetPos}, moveLen={moveLen}";
		return base.ToStringGoal() + str;
	}
}
