using UnityEngine;

public class Goal_Stop : Goal
{
	private double giveupLen;

	private Vector3 stopPos
	{
		get;
		set;
	}

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.STOP;
	}

	public Goal_Stop SetGiveupLen(float len)
	{
		giveupLen = len;
		return this;
	}

	protected override void Activate(Brain brain)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		brain.moveCtrl.StopOn();
		stopPos = brain.owner._transform.get_position();
	}

	protected override STATUS Process(Brain brain)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		double num = AIUtility.GetLengthWithBetweenPosition(brain.owner._transform.get_position(), stopPos);
		if (num > giveupLen)
		{
			SetStatus(STATUS.COMPLETED);
		}
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.StopOff();
	}
}
