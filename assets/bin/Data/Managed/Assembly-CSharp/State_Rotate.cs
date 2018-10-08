public class State_Rotate : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.RotateOn();
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.RotateOff();
	}

	public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
		if (ev == BRAIN_EVENT.END_ENEMY_ACTION)
		{
			fsm.ChangeState(STATE_TYPE.MOVE);
		}
	}
}
