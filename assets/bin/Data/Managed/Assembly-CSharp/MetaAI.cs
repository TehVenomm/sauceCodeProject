public class MetaAI
{
	private const int kPlayerNum = 8;

	private Player[] needRescuePlayer = new Player[8];

	public void Update()
	{
		WatchingPlayer();
	}

	private void WatchingPlayer()
	{
		for (int i = 0; i < 8; i++)
		{
			needRescuePlayer[i] = null;
		}
		int num = 0;
		int num2 = 7;
		bool flag = false;
		int j = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; j < count; j++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[j] as Player;
			if (object.ReferenceEquals(player, null))
			{
				continue;
			}
			bool flag2 = false;
			if (!player.IsPrayed())
			{
				if (player.isDead && !player.isWaitingResurrectionHoming && player.rescueTime > 0f)
				{
					flag2 = true;
				}
				if (player.IsStone() && player.stoneRescueTime > 0f)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				if (player.isNpc)
				{
					needRescuePlayer[num2--] = player;
				}
				else
				{
					needRescuePlayer[num++] = player;
				}
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		for (int k = 0; k < 8; k++)
		{
			if (!object.ReferenceEquals(needRescuePlayer[k], null))
			{
				OnRescuePlayer(needRescuePlayer[k]);
			}
		}
	}

	private void OnRescuePlayer(Player dead_player)
	{
		NonPlayer nearestAliveNpc = AIUtility.GetNearestAliveNpc(dead_player);
		if (nearestAliveNpc == null || nearestAliveNpc.controller == null)
		{
			return;
		}
		Brain brain = nearestAliveNpc.controller.brain;
		if (!(brain == null) && brain.think != null)
		{
			brain.targetCtrl.SetAllyTarget(dead_player);
			if (brain.fsm != null)
			{
				brain.fsm.ChangeState(STATE_TYPE.RAISE_ALLY);
			}
		}
	}
}
