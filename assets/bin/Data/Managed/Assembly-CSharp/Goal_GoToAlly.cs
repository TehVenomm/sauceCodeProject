using UnityEngine;

public class Goal_GoToAlly : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.GO_TO_ALLY;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.CanReviveOfTargetAlly())
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			StageObject allyTarget = brain.targetCtrl.GetAllyTarget();
			Vector3 position = allyTarget._transform.get_position();
			float num = 3f;
			if (!brain.moveCtrl.CanSeekToAlly(position, num))
			{
				PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
				RaycastHit seekHit = brain.moveCtrl.seekHit;
				Vector3 position2 = seekHit.get_transform().get_position();
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
