using UnityEngine;

public class Goal_GoToAlly : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.GO_TO_ALLY;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.CanReviveOfTargetAlly())
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			StageObject allyTarget = brain.targetCtrl.GetAllyTarget();
			Vector3 position = allyTarget._transform.position;
			float num = 3f;
			if (!brain.moveCtrl.CanSeekToAlly(position, num))
			{
				PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
				Vector3 position2 = brain.moveCtrl.seekHit.transform.position;
				AddSubGoal<Goal_MoveToAround>().SetParam(place, position2, num);
			}
			else if (!brain.owner.IsArrivalPosition(position, 0f))
			{
				AddSubGoal<Goal_MoveToPosition>().SetParam(position, num);
			}
			else
			{
				SetStatus(STATUS.COMPLETED);
			}
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		if (!brain.targetCtrl.CanReviveOfTargetAlly())
		{
			SetStatus(STATUS.COMPLETED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
	}
}
