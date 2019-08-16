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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
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
			if (brain2 == null)
			{
				continue;
			}
			Vector3 val = allyObjectList[i]._position - position;
			if (val.get_sqrMagnitude() < brain.param.scoutParam.scoutingAudibilitySqr)
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
