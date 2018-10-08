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
		this.place = place;
		targetPos = pos;
		moveLen = len;
		return this;
	}

	protected override void Activate(Brain brain)
	{
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
		string str = $"targetPos={targetPos}, moveLen={moveLen}";
		return base.ToStringGoal() + str;
	}
}
