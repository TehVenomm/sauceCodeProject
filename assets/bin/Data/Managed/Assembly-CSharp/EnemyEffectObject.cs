public class EnemyEffectObject
{
	public enum DELETE_CONDITION
	{
		None,
		BarrierHp,
		RegionHp,
		ShieldHp
	}

	private DELETE_CONDITION m_deleteCondition;

	private EnemyRegionWork m_enemyRegionWork;

	private Enemy m_targetEnemy;

	private string m_uniqueName = string.Empty;

	private bool m_isDeleted;

	public string UniqueName => m_uniqueName;

	public EnemyEffectObject()
		: this()
	{
	}

	public void Initialize(Enemy enemy, EnemyRegionWork regionWork, int deleteCondition, string uniqueName)
	{
		m_targetEnemy = enemy;
		m_enemyRegionWork = regionWork;
		m_deleteCondition = (DELETE_CONDITION)deleteCondition;
		m_uniqueName = uniqueName;
	}

	private void SelfDestroy()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (m_targetEnemy != null)
		{
			m_targetEnemy.OnNotifyDeleteEnemyEffect(this);
			m_targetEnemy = null;
		}
		EffectManager.ReleaseEffect(this.get_gameObject(), true, false);
		m_isDeleted = true;
	}

	private void Update()
	{
		if (!m_isDeleted)
		{
			if (m_targetEnemy == null || m_targetEnemy.isDead)
			{
				SelfDestroy();
			}
			else
			{
				bool flag = false;
				switch (m_deleteCondition)
				{
				case DELETE_CONDITION.BarrierHp:
					flag = ((int)m_targetEnemy.BarrierHp <= 0);
					break;
				case DELETE_CONDITION.RegionHp:
					flag = ((int)m_enemyRegionWork.hp <= 0);
					break;
				case DELETE_CONDITION.ShieldHp:
					flag = ((int)m_targetEnemy.ShieldHp <= 0);
					break;
				}
				if (flag)
				{
					SelfDestroy();
				}
			}
		}
	}
}
