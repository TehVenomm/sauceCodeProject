public class Goal_SpecialAttack : Goal
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.SPECIAL_ATTACK;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!(brain.owner is Player))
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			brain.weaponCtrl.SpecialOn();
		}
	}

	protected override STATUS Process(Brain brain)
	{
		return status;
	}

	protected override void Terminate(Brain brain)
	{
		brain.weaponCtrl.SpecialOff();
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
	{
		base.HandleEvent(brain, ev, param);
		Player player = brain.owner as Player;
		if (ev == BRAIN_EVENT.END_ACTION)
		{
			int num = (int)param;
			if (player != null && num == 6)
			{
				if (player.attackMode == Player.ATTACK_MODE.ARROW && brain.weaponCtrl.isFullCharge)
				{
					SetStatus(STATUS.COMPLETED);
				}
				else if (player.isActSpecialAction)
				{
					SetStatus(STATUS.COMPLETED);
				}
			}
		}
	}
}
