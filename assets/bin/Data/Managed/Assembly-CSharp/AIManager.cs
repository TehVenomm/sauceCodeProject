public class AIManager : MonoBehaviourSingleton<AIManager>
{
	private MetaAI metaAI = new MetaAI();

	private SpanTimer metaAISpanTimer = new SpanTimer(1f);

	protected override void Awake()
	{
		base.Awake();
		Singleton<GoalPool>.Create();
		Singleton<BrainBlackboard>.Create();
	}

	private void Update()
	{
		if (metaAISpanTimer.IsReady())
		{
			metaAI.Update();
		}
	}

	private void OnDestroy()
	{
		if (Singleton<GoalPool>.IsValid())
		{
			Singleton<GoalPool>.I.Clear();
		}
		if (Singleton<BrainBlackboard>.IsValid())
		{
			Singleton<BrainBlackboard>.I.Clear();
		}
	}
}
