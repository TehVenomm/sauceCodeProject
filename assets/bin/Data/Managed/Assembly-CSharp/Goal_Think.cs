using System;
using System.Collections.Generic;

public class Goal_Think : GoalComposite
{
	[Flags]
	public enum EVAL_FLAG
	{
		NONE = 0x0,
		KILL_TARGET = 0x1,
		SEE_TARGET = 0x2,
		RAISE_ALLY = 0x4
	}

	private SpanTimer choiceGoalSpanTimer;

	private EVAL_FLAG evalFlag;

	private List<GoalEvaluator> evaluators = new List<GoalEvaluator>();

	public Goal_Think()
	{
		InitEvaluator();
	}

	protected override GOAL_TYPE GetGoalType()
	{
		return GOAL_TYPE.THINK;
	}

	public Goal_Think SetChoiceGoalSpanTimer(float span)
	{
		choiceGoalSpanTimer = new SpanTimer(span);
		return this;
	}

	protected override void Activate(Brain brain)
	{
		SetStatus(STATUS.ACTIVE);
		ChoiceGoalFromEvaluator(brain);
		if (choiceGoalSpanTimer != null)
		{
			choiceGoalSpanTimer.ResetNextTime();
		}
	}

	protected override STATUS Process(Brain brain)
	{
		STATUS sTATUS = UpdateSubGoals(brain);
		if (sTATUS == STATUS.COMPLETED || sTATUS == STATUS.FAILED)
		{
			SetStatus(STATUS.INACTIVE);
		}
		if (choiceGoalSpanTimer != null && choiceGoalSpanTimer.IsReady())
		{
			ChoiceGoalFromEvaluator(brain);
		}
		return status;
	}

	protected override void Terminate(Brain brain)
	{
	}

	private void FlagOn(EVAL_FLAG flag)
	{
		evalFlag |= flag;
	}

	private void FlagOff(EVAL_FLAG flag)
	{
		evalFlag &= ~flag;
	}

	private bool FlagIsOn(EVAL_FLAG flag)
	{
		return (evalFlag & flag) == flag;
	}

	public void KillTargetOn()
	{
		FlagOn(EVAL_FLAG.KILL_TARGET);
	}

	public void SeeTargetOn()
	{
		FlagOn(EVAL_FLAG.SEE_TARGET);
	}

	public void RaiseAllyOn()
	{
		FlagOn(EVAL_FLAG.RAISE_ALLY);
	}

	public void KillTargetOff()
	{
		FlagOff(EVAL_FLAG.KILL_TARGET);
	}

	public void SeeTargetOff()
	{
		FlagOff(EVAL_FLAG.SEE_TARGET);
	}

	public void RaiseAllyOff()
	{
		FlagOff(EVAL_FLAG.RAISE_ALLY);
	}

	private void InitEvaluator()
	{
		evaluators.Add(new GoalEvaluator_KillTarget(1f));
		evaluators.Add(new GoalEvaluator_SeeTarget(1f));
		evaluators.Add(new GoalEvaluator_RaiseAlly(1f));
	}

	public void ChoiceGoalFromEvaluator(Brain brain)
	{
		GoalEvaluator goalEvaluator = null;
		float num = 0f;
		int i = 0;
		for (int count = evaluators.Count; i < count; i++)
		{
			GoalEvaluator goalEvaluator2 = evaluators[i];
			if (FlagIsOn((EVAL_FLAG)(1 << i)))
			{
				float num2 = goalEvaluator2.CalcEvaluateValue(brain);
				if (num2 > num)
				{
					num = num2;
					goalEvaluator = goalEvaluator2;
				}
			}
		}
		if (goalEvaluator != null)
		{
			goalEvaluator.SetGoal(brain, this);
		}
		else if (GetCountSubGoal() <= 0)
		{
			AddGoal_Stop();
		}
	}

	public string ToStringEvaluator(Brain brain)
	{
		string text = string.Empty + evalFlag + "\n";
		int i = 0;
		for (int count = evaluators.Count; i < count; i++)
		{
			GoalEvaluator goalEvaluator = evaluators[i];
			float num = goalEvaluator.CalcEvaluateValue(brain);
			bool flag = FlagIsOn((EVAL_FLAG)(1 << i));
			string text2 = text;
			text = text2 + string.Empty + i + ((!flag) ? "(Off)" : string.Empty) + string.Empty + goalEvaluator + ": " + num + "\n";
		}
		return text.TrimEnd(null);
	}

	public void AddGoal_SeeTarget(Brain brain)
	{
		if (!IsNowProcess(GOAL_TYPE.SEE_TARGET))
		{
			RemoveAllSubGoals(brain);
			AddSubGoal<Goal_SeeTarget>();
		}
	}

	public void AddGoal_KillTarget(Brain brain)
	{
		if (!IsNowProcess(GOAL_TYPE.KILL_TARGET))
		{
			RemoveAllSubGoals(brain);
			AddSubGoal<Goal_KillTarget>();
		}
	}

	public void AddGoal_RaiseAlly(Brain brain)
	{
		if (!IsNowProcess(GOAL_TYPE.RAISE_ALLY))
		{
			RemoveAllSubGoals(brain);
			AddSubGoal<Goal_RaiseAlly>();
		}
	}

	public void AddGoal_Stop()
	{
		if (!IsNowProcess(GOAL_TYPE.STOP))
		{
			AddSubGoal<Goal_Stop>().SetGiveupLen(1f).SetGiveupTime(1f);
		}
	}
}
