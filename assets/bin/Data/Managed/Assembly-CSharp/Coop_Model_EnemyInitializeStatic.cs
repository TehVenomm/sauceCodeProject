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
				EnemyRegionWork obj = region_works[i];
				obj.hp = regionWorkSyncData.hp;
				obj.isBroke = regionWorkSyncData.isBroke;
				obj.bleedList = regionWorkSyncData.bleedList;
				obj.isShieldDamage = regionWorkSyncData.isShieldDamage;
				obj.isShieldCriticalDamage = regionWorkSyncData.isShieldCriticalDamage;
				obj.shadowSealingData = regionWorkSyncData.shadowSealingData;
				obj.bombArrowDataHistory = regionWorkSyncData.bombArrowDataHistory;
			}
		}
	}
}
