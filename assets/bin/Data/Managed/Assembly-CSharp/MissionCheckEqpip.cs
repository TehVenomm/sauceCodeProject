public class MissionCheckEqpip : MissionCheckBase
{
	private bool isClear;

	protected override void Initialize(MISSION_REQUIRE require, int param)
	{
		EquipItemInfo equipmentWeaponInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentWeaponInfo(0, -1);
		EQUIPMENT_TYPE type = equipmentWeaponInfo.tableData.type;
		for (int i = 0; i < 3; i++)
		{
			EquipItemInfo equipmentWeaponInfo2 = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentWeaponInfo(i, -1);
			if (equipmentWeaponInfo2 != null)
			{
				switch (require)
				{
				case MISSION_REQUIRE.ONLY_PAIR_SWORDS:
					if (equipmentWeaponInfo2.tableData.type != EQUIPMENT_TYPE.PAIR_SWORDS)
					{
						return;
					}
					break;
				case MISSION_REQUIRE.ONLY_ONE_HAND_SWORD:
					if (equipmentWeaponInfo2.tableData.type != 0)
					{
						return;
					}
					break;
				case MISSION_REQUIRE.ONLY_TWO_HAND_SWORD:
					if (equipmentWeaponInfo2.tableData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD)
					{
						return;
					}
					break;
				case MISSION_REQUIRE.ONLY_SPEAR:
					if (equipmentWeaponInfo2.tableData.type != EQUIPMENT_TYPE.SPEAR)
					{
						return;
					}
					break;
				case MISSION_REQUIRE.ONLY_ARROW:
					if (equipmentWeaponInfo2.tableData.type != EQUIPMENT_TYPE.ARROW)
					{
						return;
					}
					break;
				case MISSION_REQUIRE.MULTI_EQUIP:
					if (equipmentWeaponInfo2.tableData.type != type)
					{
						isClear = true;
						return;
					}
					break;
				}
			}
		}
		if (require != MISSION_REQUIRE.MULTI_EQUIP)
		{
			isClear = true;
		}
	}

	public override bool IsMissionClear()
	{
		return isClear;
	}
}
