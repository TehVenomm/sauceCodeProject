public static class Coop_Model_EnemyInitializeStatic
{
	public static void ApplyRegionWorks(this EnemyRegionWork[] region_works, Coop_Model_EnemyInitialize initialize_model)
	{
		if (initialize_model.regions != null)
		{
			int i = 0;
			for (int count = initialize_model.regions.Count; i < count && i < region_works.Length; i++)
			{
				Enemy.RegionWorkSyncData regionWorkSyncData = initialize_model.regions[i];
				EnemyRegionWork enemyRegionWork = region_works[i];
				enemyRegionWork.hp = regionWorkSyncData.hp;
				enemyRegionWork.isBroke = regionWorkSyncData.isBroke;
				enemyRegionWork.bleedList = regionWorkSyncData.bleedList;
				enemyRegionWork.isShieldDamage = regionWorkSyncData.isShieldDamage;
				enemyRegionWork.isShieldCriticalDamage = regionWorkSyncData.isShieldCriticalDamage;
				enemyRegionWork.shadowSealingData = regionWorkSyncData.shadowSealingData;
			}
		}
	}
}
