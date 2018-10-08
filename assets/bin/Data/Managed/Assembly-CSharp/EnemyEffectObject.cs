using UnityEngine;

public class EnemyEffectObject : MonoBehaviour
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

	public void Initialize(Enemy enemy, EnemyRegionWork regionWork, int deleteCondition, string uniqueName)
	{
		m_targetEnemy = enemy;
		m_enemyRegionWork = regionWork;
		m_deleteCondition = (DELETE_CONDITION)deleteCondition;
		m_uniqueName = uniqueName;
	}

	private void SelfDestroy()
	{
		if ((Object)m_targetEnemy != (Object)null)
		{
			m_targetEnemy.OnNotifyDeleteEnemyEffect(this);
			m_targetEnemy = null;
		}
		EffectManager.ReleaseEffect(base.gameObject, true, false);
		m_isDeleted = true;
	}

	private void Update()
	{
		if (!m_isDeleted)
		{
			if ((Object)m_targetEnemy == (Object)null || m_targetEnemy.isDead)
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
