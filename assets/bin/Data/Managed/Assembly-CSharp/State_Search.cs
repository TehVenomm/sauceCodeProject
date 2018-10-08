public class State_Search : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
		brain.opponentMem.Update();
		brain.targetCtrl.UpdateTarget();
		brain.opponentMem.UpdateHate();
		if (brain.targetCtrl.IsTargeting())
		{
			fsm.ChangeState(STATE_TYPE.SELECT);
		}
		else
		{
			fsm.processSpan.SetTempSpan(1f);
		}
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
	}
}
