public class Goal_EnsureSafety : GoalComposite
{
	private const float GUARD_GIVEUP_TIME = 5f;

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.ENSURE_SAFETY;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		RemoveAllSubGoals(brain);
		if (brain.dangerRader != null && brain.dangerRader.AskDanger())
		{
			if (brain.dangerRader.AskWillBulletHit())
			{
				AddSubGoal<Goal_AvoidRightAndLeft>();
			}
			else if (brain.dangerRader.AskWillDashHit())
			{
				if (brain.weaponCtrl.IsGuardAttack())
				{
					AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
					return;
				}
				PLACE pLACE = brain.dangerRader.GetSafetySide();
				if (!brain.moveCtrl.CanPlaceAvoid(pLACE))
				{
					pLACE = ((pLACE == PLACE.LEFT) ? PLACE.RIGHT : PLACE.LEFT);
				}
				AddSubGoal<Goal_Avoid>().SetPlace(pLACE);
				AddSubGoal<Goal_Avoid>().SetPlace(pLACE);
			}
			else if (brain.dangerRader.AskDangerPosition(brain.owner._position))
			{
				if (brain.weaponCtrl.IsGuardAttack())
				{
					AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
					return;
				}
				PLACE safetyPlace = brain.dangerRader.GetSafetyPlace();
				AddSubGoal<Goal_Avoid>().SetPlace(safetyPlace);
			}
			else if (brain.dangerRader.AskDangerPosition(brain.moveCtrl.targetPos))
			{
				PLACE safetySide = brain.dangerRader.GetSafetySide();
				AddSubGoal<Goal_MoveToAround>().SetParam(safetySide, brain.moveCtrl.targetPos, 3f);
			}
			else if (brain.weaponCtrl.IsGuardAttack())
			{
				AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
			}
			else if (Utility.Coin())
			{
				PLACE safetySide2 = brain.dangerRader.GetSafetySide();
				AddSubGoal<Goal_Avoid>().SetPlace(safetySide2);
			}
			else
			{
				AddSubGoal<Goal_Stop>().SetGiveupTime(0.3f);
			}
		}
		else
		{
			SetStatus(STATUS.COMPLETED);
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS status = UpdateSubGoals(brain);
		SetStatus(status);
		if (brain.dangerRader != null && !brain.dangerRader.AskDanger(1f))
		{
			SetStatus(STATUS.COMPLETED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
	}
}
