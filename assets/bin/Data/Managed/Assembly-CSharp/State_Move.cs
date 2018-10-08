public class State_Move : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.SeekOn();
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		brain.moveCtrl.SeekOff();
		brain.moveCtrl.ResetStopRange();
	}

	public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
		if (ev == BRAIN_EVENT.END_ENEMY_ACTION)
		{
			fsm.ChangeState(STATE_TYPE.ATTACK);
		}
	}
}
