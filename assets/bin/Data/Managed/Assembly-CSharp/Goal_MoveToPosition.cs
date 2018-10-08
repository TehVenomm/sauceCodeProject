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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		targetPos = pos;
		moveLen = move_len;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		Vector3 val = targetPos - brain.owner._transform.get_position();
		val.y = 0f;
		margin = 0f;
		if (moveLen > 0f)
		{
			float num = brain.owner.moveStopRange + moveLen;
			float magnitude = val.get_magnitude();
			if (magnitude > num)
			{
				val *= num / magnitude;
				margin = moveLen / 3f;
			}
		}
		targetPos = brain.owner._transform.get_position() + val;
		AddSubGoal<Goal_Move>().SetStick(Vector2.get_up(), targetPos).SetGiveupTime(moveLen + 0.5f);
	}

	protected override STATUS Process(Brain brain)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		string str = $"targetPos={(object)targetPos}, moveLen={(object)moveLen}";
		return base.ToStringGoal() + str;
	}
}
