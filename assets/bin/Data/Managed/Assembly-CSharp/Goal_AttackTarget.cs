using UnityEngine;

public class Goal_AttackTarget : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.ATTACK_TARGET;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		else if (!brain.targetCtrl.CanAttackTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		else
		{
			Player player = brain.owner as Player;
			if ((Object)player != (Object)null && player.isLongAttackMode)
			{
				Vector3 targetPosition = brain.targetCtrl.GetTargetPosition();
				float distance = brain.targetCtrl.GetDistance();
				if (!brain.moveCtrl.CanSeekToOpponent(targetPosition, distance))
				{
					float len = 3f;
					PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
					Vector3 position = brain.moveCtrl.seekHit.transform.position;
					AddSubGoal<Goal_MoveToAround>().SetParam(place, position, len);
					return;
				}
			}
			bool flag = false;
			if (brain.targetCtrl.IsAttackableTarget())
			{
				brain.canCheckAvoidAttack = true;
				brain.weaponCtrl.AvoidAttackOff();
				if (brain.weaponCtrl.IsCombo())
				{
					flag = false;
				}
				else if (brain.weaponCtrl.GetSpecialReach() > 0f && Utility.Dice100(30))
				{
					flag = ((!player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL)) ? true : false);
				}
				else if (player.CheckAttackMode(Player.ATTACK_MODE.TWO_HAND_SWORD) && !player.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && player.playerParameter.twoHandSwordActionInfo.avoidAttackEnable && Utility.Dice100(40))
				{
					brain.weaponCtrl.AvoidAttackOn();
				}
			}
			else if (brain.targetCtrl.IsSpecialAttackableTarget())
			{
				flag = true;
			}
			else if (brain.targetCtrl.IsAvoidAttackableTarget())
			{
				brain.canAvoidAttack = false;
				brain.weaponCtrl.AvoidAttackOn();
			}
			if ((Object)player != (Object)null)
			{
				if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
				{
					flag = (player.pairSwordsCtrl.IsAbleToAlterSpAction() ? true : false);
				}
				else if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
				{
					flag = (player.IsSpActionGaugeFullCharged() ? true : false);
				}
			}
			if (flag)
			{
				AddSubGoal<Goal_SpecialAttack>();
			}
			else
			{
				AddSubGoal<Goal_Attack>();
			}
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
		if (!brain.targetCtrl.CanAttackTarget())
		{
			SetStatus(STATUS.COMPLETED);
		}
		return base.status;
	}

	protected override void Terminate(Brain brain)
	{
	}
}
