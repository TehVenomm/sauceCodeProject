public class MetaAI
{
	private const int kPlayerNum = 4;

	private Player[] deadPlayer = new Player[4];

	public void Update()
	{
		WatchingPlayer();
	}

	private void WatchingPlayer()
	{
		for (int i = 0; i < 4; i++)
		{
			deadPlayer[i] = null;
		}
		int num = 0;
		int num2 = 3;
		bool flag = false;
		int j = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; j < count; j++)
		{
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[j] as Player;
			if (!object.ReferenceEquals(player, null) && player.isDead && !player.IsPrayed() && player.rescueTime > 0f)
			{
				if (player.isNpc)
				{
					deadPlayer[num2--] = player;
				}
				else
				{
					deadPlayer[num++] = player;
				}
				flag = true;
			}
		}
		if (flag)
		{
			for (int k = 0; k < 4; k++)
			{
				if (!object.ReferenceEquals(deadPlayer[k], null))
				{
					OnRaisePlayer(deadPlayer[k]);
				}
			}
		}
	}

	private void OnRaisePlayer(Player dead_player)
	{
		NonPlayer nearestAliveNpc = AIUtility.GetNearestAliveNpc(dead_player);
		if (!(nearestAliveNpc == null) && !(nearestAliveNpc.controller == null))
		{
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
}
