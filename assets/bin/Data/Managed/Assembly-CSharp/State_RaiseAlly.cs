public class State_RaiseAlly : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		if (brain.think != null)
		{
			brain.think.RaiseAllyOn();
		}
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
		if (!brain.targetCtrl.CanRescueOfTargetAlly())
		{
			fsm.ChangeState(STATE_TYPE.KILL_TARGET);
		}
		else if (brain.targetCtrl.IsOtherPlayerReviveOfTarget())
		{
			fsm.ChangeState(STATE_TYPE.KILL_TARGET);
		}
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		if (brain.think != null)
		{
			brain.think.RaiseAllyOff();
		}
	}
}
