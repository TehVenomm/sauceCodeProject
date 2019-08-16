public class EnemyWaveStrategyController
{
	private const float RADIUS_WAVE_TARGET = 4f;

	private bool isActive;

	private Enemy owner;

	private EnemyActionController.ActionInfo moveActionInfo;

	public EnemyWaveStrategyController(Enemy enemy)
	{
		if (!QuestManager.IsValidInGameWaveStrategy())
		{
			isActive = false;
			return;
		}
		owner = enemy;
		moveActionInfo = new EnemyActionController.ActionInfo
		{
			data = new EnemyActionTable.EnemyActionData()
		};
		moveActionInfo.data.atkRange = 1;
		moveActionInfo.data.name = "防衛対象オブジェクトへ移動";
		moveActionInfo.data.combiActionTypeInfos = EnemyActionTable.GetActionTypeInfos("rotate", "move");
		isActive = true;
	}

	public bool IsActive()
	{
		return isActive;
	}

	public bool IsArrivedTarget()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (owner == null || owner.actionTarget == null)
		{
			return true;
		}
		return owner.IsArrivalPosition(owner.actionTarget._position, 4f);
	}

	public EnemyActionController.ActionInfo GetAlteredAction()
	{
		return moveActionInfo;
	}
}
