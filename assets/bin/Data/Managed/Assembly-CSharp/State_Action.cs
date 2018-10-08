public class State_Action : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
		EnemyBrain enemyBrain = brain as EnemyBrain;
		EnemyActionController.ActionInfo nowAction = enemyBrain.actionCtrl.nowAction;
		if (nowAction.data.isRotate)
		{
			fsm.subFsm.ChangeState(STATE_TYPE.ROTATE);
		}
		else if (nowAction.data.isMove)
		{
			fsm.subFsm.ChangeState(STATE_TYPE.MOVE);
		}
		else
		{
			fsm.subFsm.ChangeState(STATE_TYPE.ATTACK);
		}
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
		fsm.subFsm.ChangeState(STATE_TYPE.NONE);
	}

	public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
	{
		if (ev == BRAIN_EVENT.END_ENEMY_ACTION && fsm.subFsm.currentType == STATE_TYPE.ATTACK)
		{
			fsm.ChangeState(STATE_TYPE.SEARCH);
		}
	}
}
