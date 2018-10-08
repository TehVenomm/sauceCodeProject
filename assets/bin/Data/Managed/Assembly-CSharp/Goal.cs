using System.Diagnostics;
using UnityEngine;

public abstract class Goal : Poolable
{
	public enum STATUS
	{
		INACTIVE,
		ACTIVE,
		COMPLETED,
		FAILED
	}

	protected STATUS status;

	public float giveupTime;

	public GOAL_TYPE goalType
	{
		get;
		private set;
	}

	public bool isInactive => status == STATUS.INACTIVE;

	public bool isActive => status == STATUS.ACTIVE;

	public bool isComplete => status == STATUS.COMPLETED;

	public bool isFailed => status == STATUS.FAILED;

	public override void OnAwake()
	{
		goalType = GetGoalType();
	}

	public override void OnInit()
	{
		status = STATUS.INACTIVE;
		giveupTime = 0f;
	}

	public override void OnFinal()
	{
	}

	public static T Alloc<T>() where T : Goal, new()
	{
		if (Singleton<GoalPool>.IsValid())
		{
			return Singleton<GoalPool>.I.Alloc<T>();
		}
		T result = new T();
		result.OnAwake();
		result.OnInit();
		return result;
	}

	public static void Free(Goal goal)
	{
		if (Singleton<GoalPool>.IsValid())
		{
			Singleton<GoalPool>.I.Free(goal);
		}
		else
		{
			goal.OnFinal();
			goal = null;
		}
	}

	protected abstract GOAL_TYPE GetGoalType();

	protected void SetStatus(STATUS status)
	{
		this.status = status;
	}

	public void SetGiveupTime(float time)
	{
		giveupTime = Time.time + time;
	}

	public bool IsGiveupTime()
	{
		if (giveupTime <= 0f)
		{
			return false;
		}
		return Time.time > giveupTime;
	}

	protected abstract void Activate(Brain brain);

	protected abstract STATUS Process(Brain brain);

	protected abstract void Terminate(Brain brain);

	public virtual STATUS Update(Brain brain)
	{
		if (isInactive)
		{
			Activate(brain);
		}
		if (IsGiveupTime())
		{
			SetStatus(STATUS.FAILED);
			return status;
		}
		return Process(brain);
	}

	public virtual void Destroy(Brain brain)
	{
		Terminate(brain);
		Free(this);
	}

	public virtual void HandleEvent(Brain brain, BRAIN_EVENT ev, object param = null)
	{
	}

	public override string ToString()
	{
		return ToStringGoal();
	}

	public virtual string ToStringGoal()
	{
		string arg = (!(giveupTime > 0f)) ? string.Empty : (" " + (giveupTime - Time.time).ToString("F2"));
		return $"Goal[ {goalType} ]: status={status}{arg}.";
	}

	[Conditional("GOAL_LOG")]
	protected void logd(string log)
	{
	}
}
