public class State_Select : State
{
	public override void Enter(StateMachine fsm, Brain brain)
	{
	}

	public override void Process(StateMachine fsm, Brain brain)
	{
		EnemyBrain enemyBrain = brain as EnemyBrain;
		if (!(enemyBrain == null) && enemyBrain.opponentMem != null)
		{
			enemyBrain.opponentMem.Update();
			enemyBrain.actionCtrl.SelectAction();
			if (enemyBrain.actionCtrl.canUseCount <= 0)
			{
				fsm.processSpan.SetTempSpan(1f);
			}
			else if (enemyBrain.actionCtrl.totalWeight <= 0)
			{
				fsm.ChangeState(STATE_TYPE.SEARCH);
			}
			else
			{
				fsm.ChangeState(STATE_TYPE.ACTION);
			}
		}
	}

	public override void Exit(StateMachine fsm, Brain brain)
	{
	}
}
