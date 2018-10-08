using System.Collections.Generic;
using UnityEngine;

public class AutoBrain : Brain
{
	public Self self;

	public float choiceGoalSpan = 3f;

	public SkillController skillCtr;

	protected override void Awake()
	{
		base.Awake();
		self = (base.owner as Self);
	}

	protected override void OnInitialize()
	{
		base.OnInitialize();
		base.fsm = new StateMachine(this);
		base.fsm.SetCurrentState(STATE_TYPE.BATTLE_START);
		base.think = Goal.Alloc<Goal_Think>();
		base.think.SetChoiceGoalSpanTimer(choiceGoalSpan);
		base.dangerRader = DangerRader.Create(this, 10f);
		skillCtr = new SkillController(this);
		base.targetUpdateSpanTimer = new SpanTimer(1f);
	}

	public int CanActiSkill()
	{
		base.moveCtrl.ChangeStopRange(5f);
		if (base.targetCtrl.IsArrivalAttackPosition())
		{
			if ((Object)self.targetingPoint != (Object)null)
			{
				if ((Object)self.targetingPoint.owner != (Object)null)
				{
					return 1;
				}
				return 2;
			}
			return 2;
		}
		return 0;
	}

	public override List<StageObject> GetTargetObjectList()
	{
		return (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
	}
}
