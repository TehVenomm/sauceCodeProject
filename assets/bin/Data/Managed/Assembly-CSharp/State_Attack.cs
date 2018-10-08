public class State_Attack : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		brain.weaponCtrl.AttackOn();
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		brain.weaponCtrl.AttackOff();
	}

	public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
		if (ev == BRAIN_EVENT.END_ENEMY_ACTION)
		{
			fsm.ChangeState(STATE_TYPE.ROTATE);
		}
	}
}
