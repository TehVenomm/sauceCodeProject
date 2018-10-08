public class State_Stop : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.StopOn();
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.StopOff();
	}
}
