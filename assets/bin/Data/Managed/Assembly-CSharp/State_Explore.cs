using UnityEngine;

public class State_Explore : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.SetRootPosition(brain.owner.appearPos);
		Explore(fsm, brain);
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
		if (fsm.subFsm.currentType == STATE_TYPE.NONE)
		{
			Explore(fsm, brain);
		}
		if (fsm.subFsm.currentType == STATE_TYPE.STOP && brain.moveCtrl.isStopTimeOver)
		{
			Explore(fsm, brain);
		}
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		fsm.subFsm.ChangeState(STATE_TYPE.NONE);
	}

	public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
		if (ev == BRAIN_EVENT.END_ENEMY_ACTION)
		{
			fsm.subFsm.ChangeState(STATE_TYPE.NONE);
		}
	}

	private void Explore(StateMachine fsm, Brain brain)
	{
		float value = brain.param.moveParam.moveMaxLength * 0.5f;
		int num = Utility.Random(100);
		if (num < 15)
		{
			Vector3 rootPosition = brain.moveCtrl.rootPosition;
			rootPosition.x += Utility.SymmetryRandom(value);
			rootPosition.z += Utility.SymmetryRandom(value);
			brain.moveCtrl.SetTargetPos(rootPosition);
			fsm.subFsm.ChangeState(STATE_TYPE.ROTATE);
		}
		else if (num >= 15 && num < 45)
		{
			Vector3 rootPosition2 = brain.moveCtrl.rootPosition;
			rootPosition2.x += Utility.SymmetryRandom(value);
			rootPosition2.z += Utility.SymmetryRandom(value);
			brain.moveCtrl.SetTargetPos(rootPosition2);
			brain.moveCtrl.ChangeStopRange(1f);
			fsm.subFsm.ChangeState(STATE_TYPE.MOVE);
		}
		else
		{
			brain.moveCtrl.SetStopTime(Utility.Random(3f));
			fsm.subFsm.ChangeState(STATE_TYPE.STOP);
		}
	}
}
