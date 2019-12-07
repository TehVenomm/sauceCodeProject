using System.Collections.Generic;
using UnityEngine;

public class State_NonActive : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		fsm.subFsm.ChangeState(STATE_TYPE.EXPLORE);
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
		StageObject targetObjectOfScountingParam = brain.targetCtrl.GetTargetObjectOfScountingParam();
		if (targetObjectOfScountingParam != null)
		{
			if (brain.opponentMem != null)
			{
				brain.opponentMem.AddHate(targetObjectOfScountingParam, 100, Hate.TYPE.Damage);
			}
			ChangeActiveState(fsm, brain);
		}
		else
		{
			fsm.processSpan.SetTempSpan(0.1f);
		}
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		fsm.subFsm.ChangeState(STATE_TYPE.NONE);
	}

	private void ChangeActiveState(StateMachine fsm, Brain brain)
	{
		if (brain.owner != null)
		{
			brain.owner.SafeActIdle();
		}
		fsm.ChangeState(STATE_TYPE.ACTIVE);
	}

	public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
		if (ev != BRAIN_EVENT.ATTACKED_HIT)
		{
			return;
		}
		ChangeActiveState(fsm, brain);
		Vector3 position = brain.owner._position;
		if (brain.param.scoutParam == null)
		{
			return;
		}
		List<StageObject> allyObjectList = brain.GetAllyObjectList();
		for (int i = 0; i < allyObjectList.Count; i++)
		{
			if (allyObjectList[i].controller == null)
			{
				continue;
			}
			Brain brain2 = allyObjectList[i].controller.brain;
			if (!(brain2 == null) && (allyObjectList[i]._position - position).sqrMagnitude < brain.param.scoutParam.scoutingAudibilitySqr)
			{
				if (brain2.owner != null)
				{
					brain2.owner.SafeActIdle();
				}
				if (brain2.fsm != null)
				{
					brain2.fsm.ChangeState(STATE_TYPE.ACTIVE);
				}
			}
		}
	}
}
