using System;
using System.Collections.Generic;
using UnityEngine;

public class CoopLocalServerEnemyPop
{
	private CoopLocalServerStage stage;

	private int count;

	private List<CoopLocalServerEnemy> enemys;

	private bool isStart;

	public int popIndex
	{
		get;
		private set;
	}

	public FieldMapTable.EnemyPopTableData data
	{
		get;
		private set;
	}

	public void Init(CoopLocalServerStage stage, int idx, FieldMapTable.EnemyPopTableData data, int count)
	{
		this.stage = stage;
		popIndex = idx;
		this.data = data;
		this.count = count;
		enemys = new List<CoopLocalServerEnemy>();
		isStart = false;
	}

	public bool IsTotalComplete()
	{
		if (data.popNumTotal > 0)
		{
			return count >= data.popNumTotal;
		}
		return false;
	}

	public bool IsExtermination()
	{
		if (IsTotalComplete())
		{
			return GetPopNum() == 0;
		}
		return false;
	}

	public int GetPopNum()
	{
		int num = 0;
		enemys.ForEach(delegate(CoopLocalServerEnemy e)
		{
			if (e.IsPop())
			{
				num++;
			}
		});
		return num;
	}

	public void Start()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || isStart)
		{
			return;
		}
		isStart = true;
		Action<StageObject> action = delegate(StageObject o)
		{
			Enemy enemy = o as Enemy;
			if (!(enemy == null) && enemy.enemyPopIndex == popIndex)
			{
				CoopLocalServerEnemy coopLocalServerEnemy = new CoopLocalServerEnemy(this, 0f);
				coopLocalServerEnemy.Pop(enemy.id);
				enemys.Add(coopLocalServerEnemy);
			}
		};
		MonoBehaviourSingleton<StageObjectManager>.I.enemyList.ForEach(action);
		MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(action);
		for (int i = enemys.Count; i < data.popNumMax; i++)
		{
			float pop_time = data.GeneratePopTime();
			if (i < data.popNumInit)
			{
				pop_time = 0f;
			}
			else if (i < data.popNumMin)
			{
				pop_time = 0f;
			}
			CoopLocalServerEnemy item = new CoopLocalServerEnemy(this, pop_time);
			enemys.Add(item);
		}
		Update();
	}

	public void Update()
	{
		if (!IsTotalComplete())
		{
			enemys.ForEach(delegate(CoopLocalServerEnemy enemy)
			{
				if (!enemy.IsPop() && !IsTotalComplete() && enemy.IsReady())
				{
					Pop(enemy);
				}
			});
		}
	}

	public void Pop(CoopLocalServerEnemy enemy)
	{
		int sid = stage.GenerateEnemyUniqId();
		count++;
		enemy.Pop(sid);
		stage.socket.SendEnemyPop(enemy);
	}

	public void Out(int sid)
	{
		CoopLocalServerEnemy coopLocalServerEnemy = enemys.Find((CoopLocalServerEnemy e) => e.sid == sid);
		if (coopLocalServerEnemy != null)
		{
			float num = data.GeneratePopTime();
			if (GetPopNum() < data.popNumMin)
			{
				num = 0f;
			}
			coopLocalServerEnemy.Out(Time.time + num);
		}
	}
}
