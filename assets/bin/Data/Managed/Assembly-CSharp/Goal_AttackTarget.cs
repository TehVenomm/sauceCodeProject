using UnityEngine;

public class Goal_AttackTarget : GoalComposite
{
	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.ATTACK_TARGET;
	}

	protected override void Activate(Brain brain)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		SetStatus(STATUS.ACTIVE);
		if (!brain.targetCtrl.IsAliveTarget())
		{
			SetStatus(STATUS.COMPLETED);
			return;
		}
		if (!brain.targetCtrl.CanAttackTarget())
		{
			SetStatus(STATUS.COMPLETED);
			return;
		}
		Player player = brain.owner as Player;
		if (player != null && player.isLongAttackMode)
		{
			Vector3 targetPosition = brain.targetCtrl.GetTargetPosition();
			float distance = brain.targetCtrl.GetDistance();
			if (!brain.moveCtrl.CanSeekToOpponent(targetPosition, distance))
			{
				float len = 3f;
				PLACE place = Utility.Coin() ? PLACE.RIGHT : PLACE.LEFT;
				RaycastHit seekHit = brain.moveCtrl.seekHit;
				Vector3 position = seekHit.get_transform().get_position();
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
		if (player != null)
		{
			if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
			{
				flag = (player.pairSwordsCtrl.IsAbleToAlterSpAction() ? true : false);
			}
			else if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
			{
				flag = (player.IsSpActionGaugeFullCharged() ? true : false);
			}
			else if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
			{
				flag = true;
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
