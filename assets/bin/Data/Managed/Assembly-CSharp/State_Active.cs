public class State_Active : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		fsm.subFsm.ChangeState(STATE_TYPE.SEARCH);
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
	}
}
