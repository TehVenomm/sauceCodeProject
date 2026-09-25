// Decompiled with JetBrains decompiler
// Type: Coop_Model_EnemyInitializeStatic
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class Coop_Model_EnemyInitializeStatic
{
  public static void ApplyRegionWorks(
    this EnemyRegionWork[] region_works,
    Coop_Model_EnemyInitialize initialize_model)
  {
    if (initialize_model.regions == null)
      return;
    int index = 0;
    for (int count = initialize_model.regions.Count; index < count && index < region_works.Length; ++index)
    {
      Enemy.RegionWorkSyncData region = initialize_model.regions[index];
      EnemyRegionWork regionWork = region_works[index];
      regionWork.hp = (XorInt) region.hp;
      regionWork.isBroke = region.isBroke;
      regionWork.bleedList = region.bleedList;
      regionWork.isShieldDamage = region.isShieldDamage;
      regionWork.isShieldCriticalDamage = region.isShieldCriticalDamage;
      regionWork.shadowSealingData = region.shadowSealingData;
      regionWork.bombArrowDataHistory = region.bombArrowDataHistory;
    }
  }
}
