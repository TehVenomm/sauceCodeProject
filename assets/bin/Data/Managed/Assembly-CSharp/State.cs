public abstract class State
{
	public abstract void Enter(StateMachine fsm, Brain brain);

	public abstract void Process(StateMachine fsm, Brain brain);

	public abstract void Exit(StateMachine fsm, Brain brain);

	public virtual void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
	}
}
