using UnityEngine;

public class Goal_Move : Goal
{
	private Vector2 stickVec
	{
		get;
		set;
	}

	private Vector3 targetPos
	{
		get;
		set;
	}

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.MOVE;
	}

	public Goal_Move SetStick(Vector2 stick, Vector3 pos)
	{
		stickVec = stick;
		targetPos = pos;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		brain.moveCtrl.SeekOn();
		brain.moveCtrl.SetSeek(stickVec, targetPos);
	}

	protected override STATUS Process(Brain brain)
	{
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.SeekOff();
	}

	public override string ToStringGoal()
	{
		return base.ToStringGoal() + " targetPos=" + targetPos;
	}
}
