using System.Collections.Generic;
using UnityEngine;

public class TargetController
{
	private Brain brain;

	private StageObject allyTarget;

	public TargetController(Brain brain)
	{
		this.brain = brain;
	}

	public StageObject GetAllyTarget()
	{
		return allyTarget;
	}

	public void SetAllyTarget(StageObject ally)
	{
		allyTarget = ally;
	}

	public bool IsTargetingOfAlly()
	{
		return GetAllyTarget() != null;
	}

	public bool IsAliveTargetOfAlly()
	{
		return AIUtility.IsAlive(GetAllyTarget());
	}

	public bool CanRescueOfTargetAlly()
	{
		StageObject stageObject = GetAllyTarget();
		if (stageObject == null)
		{
			return false;
		}
		if (stageObject is Player)
		{
			Player player = stageObject as Player;
			return (player.isDead && player.rescueTime > 0f) || (player.IsStone() && player.stoneRescueTime > 0f);
		}
		if (stageObject is Character)
		{
			Character character = stageObject as Character;
			return character.isDead || character.IsStone();
		}
		return false;
	}

	public bool IsOtherPlayerReviveOfTarget()
	{
		Player player = GetAllyTarget() as Player;
		if (player != null)
		{
			for (int i = 0; i < player.prayerIds.Count; i++)
			{
				if (player.prayerIds[i] != brain.owner.id)
				{
					Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(player.prayerIds[i]) as Player;
					if (!player2.isNpc)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public StageObject GetCurrentTarget()
	{
		if (brain.owner.actionTarget == null)
		{
			Self self = brain.owner as Self;
			if (self != null && self.isAutoMode)
			{
				return GetTargetObjectOfNearest();
			}
		}
		return brain.owner.actionTarget;
	}

	public void MissCurrentTarget()
	{
		brain.owner.SetActionTarget(null, send: false);
	}

	public void SetCurrentTarget(StageObject target_obj)
	{
		StageObject currentTarget = GetCurrentTarget();
		brain.owner.SetActionTarget(target_obj);
		brain.opponentMem.OnTargetOpponent(target_obj, currentTarget);
	}

	public bool IsTargeting()
	{
		return GetCurrentTarget() != null;
	}

	public void UpdateTarget()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (brain.opponentMem.haveHateControl)
		{
			if (IsTargetInterestLoseOfHate())
			{
				currentTarget = GetTargetObjectOfHate();
			}
		}
		else if (!IsTargeting())
		{
			currentTarget = GetTargetObjectOfNearest();
		}
		SetCurrentTarget(currentTarget);
	}

	public StageObject GetTargetObjectOfNearest()
	{
		StageObject obj = null;
		double len = double.MaxValue;
		List<OpponentMemory.OpponentRecord> listOfSensedOpponent = brain.opponentMem.GetListOfSensedOpponent();
		listOfSensedOpponent.ForEach(delegate(OpponentMemory.OpponentRecord t)
		{
			if ((double)t.record.distance < len)
			{
				obj = t.obj;
				len = t.record.distance;
			}
		});
		return obj;
	}

	public StageObject GetTargetObjectOfScountingParam()
	{
		BrainParam.ScountingParam scoutParam = brain.param.scoutParam;
		if (scoutParam == null)
		{
			return null;
		}
		List<StageObject> targetObjectList = brain.GetTargetObjectList();
		for (int i = 0; i < targetObjectList.Count; i++)
		{
			if (scoutParam.IsScouted(brain.owner._transform, targetObjectList[i]._transform))
			{
				return targetObjectList[i];
			}
		}
		return null;
	}

	public StageObject GetTargetObjectOfHate()
	{
		if (brain.opponentMem.IsHateCycleLastTurn())
		{
			OpponentMemory.OpponentRecord opponentWithNotTargetInHateCycle = brain.opponentMem.GetOpponentWithNotTargetInHateCycle();
			if (opponentWithNotTargetInHateCycle != null)
			{
				return opponentWithNotTargetInHateCycle.obj;
			}
		}
		OpponentMemory.OpponentRecord opponentWithHigherHate = brain.opponentMem.GetOpponentWithHigherHate();
		if (opponentWithHigherHate != null)
		{
			return opponentWithHigherHate.obj;
		}
		List<OpponentMemory.OpponentRecord> listOfSensedOpponent = brain.opponentMem.GetListOfSensedOpponent();
		if (listOfSensedOpponent.Count <= 0)
		{
			return null;
		}
		int index = Utility.Random(listOfSensedOpponent.Count);
		OpponentMemory.OpponentRecord opponentRecord = listOfSensedOpponent[index];
		return opponentRecord.obj;
	}

	public OpponentMemory.OpponentRecord GetOpponent()
	{
		return brain.opponentMem.FindOrEmpty(GetCurrentTarget());
	}

	public bool IsAliveTarget()
	{
		return AIUtility.IsAlive(GetCurrentTarget());
	}

	public bool IsNearTarget()
	{
		return GetOpponent().record.isNearPlace;
	}

	public bool IsPlaceTarget(PLACE place)
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return false;
		}
		return brain.opponentMem.IsPlaceOpponent(currentTarget, place);
	}

	public bool CanAttackTarget()
	{
		if (brain.targetCtrl.IsSpecialAttackableTarget() || brain.targetCtrl.IsAttackableTarget() || brain.targetCtrl.IsArrivalAttackPosition() || brain.targetCtrl.IsAvoidAttackableTarget())
		{
			return true;
		}
		return false;
	}

	public bool IsAttackableTarget()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return false;
		}
		if (brain.owner is Player)
		{
			Player player = brain.owner as Player;
			if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && player.actionID == Character.ACTION_ID.ATTACK && player.enableInputCombo)
			{
				return true;
			}
		}
		return brain.opponentMem.IsAttackableOpponent(currentTarget);
	}

	public bool IsSpecialAttackableTarget()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return false;
		}
		return brain.opponentMem.IsSpecialAttackableOpponent(currentTarget);
	}

	public bool IsAvoidAttackableTarget()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return false;
		}
		if (brain.owner is Player)
		{
			Player player = brain.owner as Player;
			if (!player.CheckAttackMode(Player.ATTACK_MODE.TWO_HAND_SWORD))
			{
				return false;
			}
			if (player.CheckSpAttackType(SP_ATTACK_TYPE.SOUL))
			{
				return false;
			}
			if (brain.canAvoidAttack)
			{
				return true;
			}
			if (!brain.canCheckAvoidAttack)
			{
				return false;
			}
			if (player.playerParameter.twoHandSwordActionInfo.avoidAttackEnable && brain.opponentMem.IsAvoidAttackableOpponent(currentTarget))
			{
				brain.canCheckAvoidAttack = false;
				if (Utility.Dice100(80))
				{
					brain.canAvoidAttack = true;
					return true;
				}
				brain.canAvoidAttack = false;
				return false;
			}
		}
		return false;
	}

	public bool IsArrivalTarget()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return false;
		}
		return brain.opponentMem.IsArrivalPosition(currentTarget);
	}

	public bool IsArrivalAttackPosition()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return false;
		}
		return brain.opponentMem.IsArrivalAttackPosition(currentTarget);
	}

	public float GetDistance()
	{
		return GetOpponent().record.distance;
	}

	public Vector3 GetTargetPosition()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return GetOpponent().record.pos;
	}

	public Vector3 GetAttackPosition()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return GetOpponent().record.attackPos;
	}

	public float GetLengthWithAttackPos(Vector3 check_pos)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return 0f;
		}
		return brain.opponentMem.GetLengthWithAttackPos(currentTarget, check_pos);
	}

	public bool IsTargetInterestLoseOfHate()
	{
		StageObject currentTarget = GetCurrentTarget();
		if (currentTarget == null)
		{
			return true;
		}
		return brain.opponentMem.IsOpponentInterestLoseOfHate(currentTarget);
	}
}
