using System.Collections.Generic;

public class CoopLocalServerStage
{
	private int nowEnemyId;

	private List<CoopLocalServerEnemyPop> enemyPops;

	private SpanTimer popSpanTimer = new SpanTimer(1f);

	public CoopLocalServerSocket socket
	{
		get;
		private set;
	}

	public CoopLocalServerStage(CoopLocalServerSocket socket)
	{
		this.socket = socket;
	}

	public void Init(uint map_id, List<CoopOfflineManager.EnemyPopParam> enemy_pop_params, int now_enemy_id)
	{
		nowEnemyId = now_enemy_id;
		InitEnemyPop(enemy_pop_params);
	}

	public void Update()
	{
		if (enemyPops != null && popSpanTimer.IsReady())
		{
			enemyPops.ForEach(delegate(CoopLocalServerEnemyPop epop)
			{
				epop.Update();
			});
		}
	}

	public int GenerateEnemyUniqId()
	{
		nowEnemyId++;
		if (nowEnemyId > 999999)
		{
			nowEnemyId = 500000;
		}
		return nowEnemyId;
	}

	private void InitEnemyPop(List<CoopOfflineManager.EnemyPopParam> enemy_pop_params)
	{
		if (QuestManager.IsValidInGameWaveMatch())
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
		}
		else
		{
			enemyPops = new List<CoopLocalServerEnemyPop>();
			int i = 0;
			for (int count = enemy_pop_params.Count; i < count; i++)
			{
				FieldMapTable.EnemyPopTableData data = enemy_pop_params[i].data;
				if (data != null && enemy_pop_params[i].data.enemyPopType == ENEMY_POP_TYPE.NONE)
				{
					int count2 = enemy_pop_params[i].count;
					CoopLocalServerEnemyPop coopLocalServerEnemyPop = new CoopLocalServerEnemyPop();
					coopLocalServerEnemyPop.Init(this, i, data, count2);
					enemyPops.Add(coopLocalServerEnemyPop);
				}
			}
		}
	}

	public void StartEnemyPop()
	{
		if (enemyPops != null)
		{
			enemyPops.ForEach(delegate(CoopLocalServerEnemyPop epop)
			{
				epop.Start();
			});
		}
	}

	public void OutEnemy(int sid)
	{
		if (enemyPops != null)
		{
			bool is_exterm = true;
			enemyPops.ForEach(delegate(CoopLocalServerEnemyPop epop)
			{
				epop.Out(sid);
				if (!epop.IsExtermination())
				{
					is_exterm = false;
				}
			});
			if (is_exterm)
			{
				socket.SendEnemyExtermination();
			}
		}
	}
}
