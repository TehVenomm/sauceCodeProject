public class State_KillTarget : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		if (brain.think != null)
		{
			brain.think.KillTargetOn();
		}
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		if (brain.think != null)
		{
			brain.think.KillTargetOff();
		}
	}
}
