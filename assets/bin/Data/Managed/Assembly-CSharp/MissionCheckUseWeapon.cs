using Network;

public class MissionCheckUseWeapon : MissionCheckBase
{
	public override bool IsMissionClear()
	{
		DeliveryBattleInfo info = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
		EQUIPMENT_TYPE eQUIPMENT_TYPE = EQUIPMENT_TYPE.NONE;
		int i = 0;
		for (int count = info.damageByWeaponList.Count; i < count; i++)
		{
			DeliveryBattleInfo.DamageByWeapon damageByWeapon = info.damageByWeaponList[i];
			if (damageByWeapon.damage <= 0)
			{
				continue;
			}
			if (eQUIPMENT_TYPE == EQUIPMENT_TYPE.NONE)
			{
				eQUIPMENT_TYPE = (EQUIPMENT_TYPE)damageByWeapon.equipmentType;
			}
			switch (missionRequire)
			{
			case MISSION_REQUIRE.ONLY_ONE_HAND_SWORD:
				if (damageByWeapon.equipmentType != 0)
				{
					return false;
				}
				break;
			case MISSION_REQUIRE.ONLY_TWO_HAND_SWORD:
				if (damageByWeapon.equipmentType != 1)
				{
					return false;
				}
				break;
			case MISSION_REQUIRE.ONLY_SPEAR:
				if (damageByWeapon.equipmentType != 2)
				{
					return false;
				}
				break;
			case MISSION_REQUIRE.ONLY_PAIR_SWORDS:
				if (damageByWeapon.equipmentType != 4)
				{
					return false;
				}
				break;
			case MISSION_REQUIRE.ONLY_ARROW:
				if (damageByWeapon.equipmentType != 5)
				{
					return false;
				}
				break;
			case MISSION_REQUIRE.MULTI_EQUIP:
				if (damageByWeapon.equipmentType != (int)eQUIPMENT_TYPE)
				{
					return true;
				}
				break;
			}
		}
		if (eQUIPMENT_TYPE != EQUIPMENT_TYPE.NONE)
		{
			return missionRequire != MISSION_REQUIRE.MULTI_EQUIP;
		}
		return false;
	}
}
