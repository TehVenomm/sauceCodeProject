using UnityEngine;

public class Goal_SeeTarget : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.SEE_TARGET;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
			return;
		}
		if (brain.targetCtrl.IsAttackableTarget() && brain.moveCtrl.CanBackAvoid())
		{
			AddSubGoal<Goal_Avoid>().SetPlace(PLACE.BACK);
			return;
		}
		float range = brain.weaponCtrl.GetAttackReach() * 2f;
		brain.moveCtrl.ChangeStopRange(range);
		if (!brain.targetCtrl.IsArrivalTarget())
		{
			AddSubGoal<Goal_GoToTarget>();
			return;
		}
		Vector3 targetPosition = brain.targetCtrl.GetTargetPosition();
		bool flag = false;
		if (Utility.Dice100(80))
		{
			if (Utility.Coin())
			{
				PLACE place = (!Utility.Coin()) ? PLACE.RIGHT : PLACE.LEFT;
				AddSubGoal<Goal_MoveToAround>().SetParam(place, targetPosition, 3f);
			}
			else if (brain.moveCtrl.CanRightAvoid() || brain.moveCtrl.CanLeftAvoid())
			{
				AddSubGoal<Goal_AvoidRightAndLeft>();
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			AddSubGoal<Goal_Stop>().SetGiveupTime(1f);
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.moveCtrl.ResetStopRange();
	}
}
