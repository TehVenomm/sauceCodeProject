// Decompiled with JetBrains decompiler
// Type: MissionCheckUseWeapon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class MissionCheckUseWeapon : MissionCheckBase
{
  public override bool IsMissionClear()
  {
    DeliveryBattleInfo info = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
    EQUIPMENT_TYPE equipmentType = EQUIPMENT_TYPE.NONE;
    int index = 0;
    for (int count = info.damageByWeaponList.Count; index < count; ++index)
    {
      DeliveryBattleInfo.DamageByWeapon damageByWeapon = info.damageByWeaponList[index];
      if (damageByWeapon.damage > 0)
      {
        if (equipmentType == EQUIPMENT_TYPE.NONE)
          equipmentType = (EQUIPMENT_TYPE) damageByWeapon.equipmentType;
        switch (this.missionRequire)
        {
          case MISSION_REQUIRE.ONLY_ONE_HAND_SWORD:
            if (damageByWeapon.equipmentType != 0)
              return false;
            continue;
          case MISSION_REQUIRE.ONLY_TWO_HAND_SWORD:
            if (damageByWeapon.equipmentType != 1)
              return false;
            continue;
          case MISSION_REQUIRE.ONLY_SPEAR:
            if (damageByWeapon.equipmentType != 2)
              return false;
            continue;
          case MISSION_REQUIRE.ONLY_PAIR_SWORDS:
            if (damageByWeapon.equipmentType != 4)
              return false;
            continue;
          case MISSION_REQUIRE.ONLY_ARROW:
            if (damageByWeapon.equipmentType != 5)
              return false;
            continue;
          case MISSION_REQUIRE.MULTI_EQUIP:
            if ((EQUIPMENT_TYPE) damageByWeapon.equipmentType != equipmentType)
              return true;
            continue;
          default:
            continue;
        }
      }
    }
    return equipmentType != EQUIPMENT_TYPE.NONE && this.missionRequire != MISSION_REQUIRE.MULTI_EQUIP;
  }
}
