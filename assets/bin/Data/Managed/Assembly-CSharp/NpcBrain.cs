using System.Collections.Generic;

public class NpcBrain : Brain
{
	protected Player player;

	public float choiceGoalSpan = 3f;

	protected override void Awake()
	{
		base.Awake();
		player = (base.owner as Player);
	}

	protected override void OnInitialize()
	{
		base.OnInitialize();
		base.fsm = new StateMachine(this);
		base.fsm.SetCurrentState(STATE_TYPE.BATTLE_START);
		base.think = Goal.Alloc<Goal_Think>();
		base.think.SetChoiceGoalSpanTimer(choiceGoalSpan);
		base.dangerRader = DangerRader.Create(this, 10f);
	}

	public override List<StageObject> GetTargetObjectList()
	{
		return (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? new List<StageObject>() : MonoBehaviourSingleton<StageObjectManager>.I.enemyList;
	}
}
