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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		stickVec = stick;
		targetPos = pos;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return base.ToStringGoal() + " targetPos=" + targetPos;
	}
}
