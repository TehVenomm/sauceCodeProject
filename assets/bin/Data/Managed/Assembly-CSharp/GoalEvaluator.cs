using UnityEngine;

public abstract class GoalEvaluator
{
	public float bias
	{
		get;
		private set;
	}

	public GoalEvaluator(float bias)
	{
		this.bias = bias;
	}

	public abstract float CalcEvaluateValue(Brain brain);

	public abstract void SetGoal(Brain brain, Goal_Think think);

	private float EvaluateValue(float val, float min, float max)
	{
		val = Mathf.Clamp(val, min, max);
		return val / max;
	}

	protected float EvaluateDistanceWithObject(Brain brain, StageObject obj)
	{
		float lengthWithBetweenObject = AIUtility.GetLengthWithBetweenObject(brain.owner, obj);
		return EvaluateDistance(lengthWithBetweenObject);
	}

	protected float EvaluateDistance(float length)
	{
		float min = 1f;
		float max = 100f;
		return EvaluateValue(length, min, max);
	}

	protected float EvaluateHealth(Brain brain)
	{
		if (brain.owner.hpMax == 0)
		{
			return 1f;
		}
		return EvaluateValue(brain.owner.hp, 1f, brain.owner.hpMax);
	}

	protected float EvaluateDangerWithTargetCondition(Brain brain, StageObject target)
	{
		float val = 1f;
		if (target is Character)
		{
			Character character = target as Character;
			Character.ACTION_ID actionID = character.actionID;
			val = ((actionID != Character.ACTION_ID.ATTACK) ? 50f : 80f);
		}
		return EvaluateValue(val, 1f, 100f);
	}

	protected float EvaluateDangerWithTargetPlace(Brain brain, StageObject target)
	{
		float val = 1f;
		OpponentMemory.OpponentRecord opponentRecord = brain.opponentMem.Find(target);
		if (opponentRecord != null)
		{
			switch (opponentRecord.record.placeOfOpponent)
			{
			case PLACE.BACK:
				val = 10f;
				break;
			case PLACE.FRONT:
				val = 90f;
				break;
			case PLACE.RIGHT:
			case PLACE.LEFT:
				val = 50f;
				break;
			}
		}
		return EvaluateValue(val, 1f, 100f);
	}
}
