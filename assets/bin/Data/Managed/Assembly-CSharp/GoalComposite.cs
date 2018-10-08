using System.Collections.Generic;

public abstract class GoalComposite : Goal
{
	private Stack<Goal> subGoals = new Stack<Goal>();

	public override void Destroy(Brain brain)
	{
		base.Destroy(brain);
		RemoveAllSubGoals(brain);
	}

	public override void HandleEvent(Brain brain, BRAIN_EVENT ev, object param)
	{
		base.HandleEvent(brain, ev, param);
		HandleEventAllSubGoals(brain, ev, param);
	}

	private void HandleEventAllSubGoals(Brain brain, BRAIN_EVENT ev, object param)
	{
		foreach (Goal subGoal in subGoals)
		{
			subGoal.HandleEvent(brain, ev, param);
		}
	}

	public int GetCountSubGoal()
	{
		return subGoals.Count;
	}

	public bool IsNowProcess(GOAL_TYPE type)
	{
		if (subGoals.Count <= 0)
		{
			return false;
		}
		return subGoals.Peek().goalType == type;
	}

	protected STATUS UpdateSubGoals(Brain brain)
	{
		while (subGoals.Count > 0 && (subGoals.Peek().isComplete || subGoals.Peek().isFailed))
		{
			subGoals.Pop().Destroy(brain);
		}
		if (subGoals.Count <= 0)
		{
			return STATUS.COMPLETED;
		}
		STATUS sTATUS = subGoals.Peek().Update(brain);
		if (sTATUS != STATUS.COMPLETED)
		{
			return sTATUS;
		}
		if (subGoals.Count <= 1)
		{
			return STATUS.COMPLETED;
		}
		return STATUS.ACTIVE;
	}

	protected T AddSubGoal<T>() where T : Goal, new()
	{
		T val = Goal.Alloc<T>();
		subGoals.Push((Goal)val);
		return val;
	}

	protected void RemoveAllSubGoals(Brain brain)
	{
		if (subGoals.Count > 0)
		{
			while (subGoals.Count > 0)
			{
				subGoals.Pop().Destroy(brain);
			}
			subGoals.Clear();
		}
	}

	public override string ToString()
	{
		return ToStringSubgoals(1, true);
	}

	public string ToStringSubgoals(int layer, bool first)
	{
		string text = string.Empty + ToStringGoal();
		foreach (Goal subGoal in subGoals)
		{
			text += "\n";
			for (int i = 0; i < layer; i++)
			{
				text += "  ";
			}
			text += ((!first) ? "-" : "+");
			text = ((!(subGoal is GoalComposite)) ? (text + subGoal.ToStringGoal()) : (text + (subGoal as GoalComposite).ToStringSubgoals(layer + 1, first)));
			first = false;
		}
		return text;
	}
}
