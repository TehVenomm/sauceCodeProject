public class State_BattleStart : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
		if (!brain.owner.isLoading && brain.owner.actionID != (Character.ACTION_ID)22)
		{
			fsm.ChangeState(STATE_TYPE.KILL_TARGET);
		}
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
	}
}
