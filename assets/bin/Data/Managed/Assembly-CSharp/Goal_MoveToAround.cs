using UnityEngine;

public class Goal_MoveToAround : GoalComposite
{
	private PLACE place = PLACE.LEFT;

	private float moveLen;

	private Vector3 targetPos
	{
		get;
		set;
	}

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.MOVE_TO_AROUND;
	}

	public Goal_MoveToAround SetParam(PLACE place, Vector3 pos, float len)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		this.place = place;
		targetPos = pos;
		moveLen = len;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		AddSubGoal<Goal_Move>().SetStick(place.GetVector2(), targetPos).SetGiveupTime(moveLen * 0.7f);
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
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
