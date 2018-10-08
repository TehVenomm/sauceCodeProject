public class Goal_EnsureSafety : GoalComposite
{
	private const float GUARD_GIVEUP_TIME = 5f;

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.ENSURE_SAFETY;
	}

	protected override void Activate(Brain brain)
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		RemoveAllSubGoals(brain);
		if (brain.dangerRader != null && brain.dangerRader.AskDanger(0.2f))
		{
			if (brain.dangerRader.AskWillBulletHit(0.2f))
			{
				AddSubGoal<Goal_AvoidRightAndLeft>();
			}
			else if (brain.dangerRader.AskWillDashHit(0.2f))
			{
				if (brain.weaponCtrl.IsGuardAttack())
				{
					AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
				}
				else
				{
					PLACE pLACE = brain.dangerRader.GetSafetySide();
					if (!brain.moveCtrl.CanPlaceAvoid(pLACE))
					{
						pLACE = ((pLACE == PLACE.LEFT) ? PLACE.RIGHT : PLACE.LEFT);
					}
					AddSubGoal<Goal_Avoid>().SetPlace(pLACE);
					AddSubGoal<Goal_Avoid>().SetPlace(pLACE);
				}
			}
			else if (brain.dangerRader.AskDangerPosition(brain.owner._position, 0.2f))
			{
				if (brain.weaponCtrl.IsGuardAttack())
				{
					AddSubGoal<Goal_Guard>().SetGiveupTime(5f);
				}
				else
				{
					PLACE safetyPlace = brain.dangerRader.GetSafetyPlace();
					AddSubGoal<Goal_Avoid>().SetPlace(safetyPlace);
				}
			}
			else if (brain.dangerRader.AskDangerPosition(brain.moveCtrl.targetPos, 0.2f))
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
