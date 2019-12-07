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
		SetStatus(STATUS.ACTIVE);
		brain.moveCtrl.StopOn();
		stopPos = brain.owner._transform.position;
	}

	protected override STATUS Process(Brain brain)
	{
		if ((double)AIUtility.GetLengthWithBetweenPosition(brain.owner._transform.position, stopPos) > giveupLen)
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
