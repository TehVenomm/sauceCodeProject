using System.Collections.Generic;

public class ExploreBossStatus
{
	public EnemyRegionWork[] regionWorks;

	public uint[] execAngryIds;

	public XorInt hpMax
	{
		get;
		private set;
	}

	public XorInt hp
	{
		get;
		private set;
	}

	public XorInt barrierHp
	{
		get;
		private set;
	}

	public XorInt downCount
	{
		get;
		private set;
	}

	public bool isDead
	{
		get;
		private set;
	}

	public XorInt shieldHp
	{
		get;
		private set;
	}

	public uint nowAngryId
	{
		get;
		private set;
	}

	public bool isMadMode
	{
		get;
		private set;
	}

	public void UpdateStatus(Enemy enemy)
	{
		hpMax = enemy.hpMax;
		hp = enemy.hp;
		barrierHp = enemy.BarrierHp;
		downCount = enemy.downCount;
		CopyRegionWorks(enemy.regionWorks);
		isDead = enemy.isDead;
		shieldHp = enemy.ShieldHp;
		nowAngryId = enemy.NowAngryID;
		if (enemy.ExecAngryIDList.Count > 0)
		{
			execAngryIds = enemy.ExecAngryIDList.ToArray();
		}
		isMadMode = enemy.IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE);
	}

	public void UpdateStatus(Coop_Model_RoomSyncExploreBoss boss)
	{
		hpMax = boss.hpm;
		hp = boss.hp;
		barrierHp = boss.bhp;
		downCount = boss.downCount;
		shieldHp = boss.shp;
		regionWorks = new EnemyRegionWork[boss.rs.Length];
		int i = 0;
		for (int num = boss.rs.Length; i < num; i++)
		{
			EnemyRegionWork enemyRegionWork = new EnemyRegionWork();
			boss.rs[i].ApplyTo(enemyRegionWork);
			regionWorks[i] = enemyRegionWork;
		}
		nowAngryId = boss.angid;
		execAngryIds = boss.eangids;
		isMadMode = boss.isMM;
	}

	public void UpdateStatus(Coop_Model_RoomExploreBossDead model)
	{
		hp = 0;
		isDead = true;
		downCount = model.downCount;
		if (regionWorks == null)
		{
			int num = 0;
			foreach (int breakId in model.breakIds)
			{
				if (num < breakId)
				{
					num = breakId;
				}
			}
			regionWorks = new EnemyRegionWork[num + 1];
			for (int i = 0; i <= num; i++)
			{
				EnemyRegionWork enemyRegionWork = new EnemyRegionWork();
				regionWorks[i] = enemyRegionWork;
			}
		}
		if (model.breakIds != null)
		{
			foreach (int breakId2 in model.breakIds)
			{
				if (regionWorks.Length > breakId2)
				{
					EnemyRegionWork enemyRegionWork2 = regionWorks[breakId2];
					if (enemyRegionWork2 != null)
					{
						enemyRegionWork2.hp = 0;
						enemyRegionWork2.isBroke = true;
					}
				}
			}
		}
		if ((int)hpMax <= 0)
		{
			foreach (Coop_Model_RoomExploreBossDead.TotalDamage dmg in model.dmgs)
			{
				hpMax = (int)hpMax + dmg.dmg;
			}
		}
	}

	private void CopyRegionWorks(EnemyRegionWork[] regionWorks)
	{
		this.regionWorks = new EnemyRegionWork[regionWorks.Length];
		for (int i = 0; i < regionWorks.Length; i++)
		{
			EnemyRegionWork enemyRegionWork = new EnemyRegionWork();
			enemyRegionWork.CopyFrom(regionWorks[i]);
			this.regionWorks[i] = enemyRegionWork;
		}
	}

	public List<int> GetBreakIds()
	{
		List<int> list = new List<int>();
		list.Add(0);
		if (regionWorks == null)
		{
			return list;
		}
		for (int i = 1; i < regionWorks.Length; i++)
		{
			if (regionWorks[i].isBroke)
			{
				list.Add(i);
			}
		}
		return list;
	}
}
