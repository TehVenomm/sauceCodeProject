public class MissionCheckEqpip : MissionCheckBase
{
	private bool isClear;

	protected override void Initialize(MISSION_REQUIRE require, int param)
	{
		EquipItemInfo equipItemInfo = null;
		if (QuestManager.IsValidInGameSeriesArena())
		{
			for (int i = 1; i <= MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum(); i++)
			{
				EquipSetInfo orderUniqueEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetOrderUniqueEquipSet(i);
				if (orderUniqueEquipSet == null)
				{
					continue;
				}
				for (int j = 0; j < 3; j++)
				{
					EquipItemInfo equipItemInfo2 = orderUniqueEquipSet.item[j];
					if (equipItemInfo2 != null)
					{
						if (equipItemInfo == null)
						{
							equipItemInfo = equipItemInfo2;
						}
						if (Check(require, equipItemInfo2, equipItemInfo))
						{
							return;
						}
					}
				}
			}
		}
		else
		{
			for (int k = 0; k < 3; k++)
			{
				EquipItemInfo equipmentWeaponInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentWeaponInfo(k);
				if (equipmentWeaponInfo != null)
				{
					if (equipItemInfo == null)
					{
						equipItemInfo = equipmentWeaponInfo;
					}
					if (Check(require, equipmentWeaponInfo, equipItemInfo))
					{
						return;
					}
				}
			}
		}
		if (require != MISSION_REQUIRE.MULTI_EQUIP)
		{
			isClear = true;
		}
	}

	private bool Check(MISSION_REQUIRE require, EquipItemInfo checkWeapon, EquipItemInfo baseWeapon)
	{
		switch (require)
		{
		case MISSION_REQUIRE.ONLY_PAIR_SWORDS:
			return checkWeapon.tableData.type != EQUIPMENT_TYPE.PAIR_SWORDS;
		case MISSION_REQUIRE.ONLY_ONE_HAND_SWORD:
			return checkWeapon.tableData.type != EQUIPMENT_TYPE.ONE_HAND_SWORD;
		case MISSION_REQUIRE.ONLY_TWO_HAND_SWORD:
			return checkWeapon.tableData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD;
		case MISSION_REQUIRE.ONLY_SPEAR:
			return checkWeapon.tableData.type != EQUIPMENT_TYPE.SPEAR;
		case MISSION_REQUIRE.ONLY_ARROW:
			return checkWeapon.tableData.type != EQUIPMENT_TYPE.ARROW;
		case MISSION_REQUIRE.MULTI_EQUIP:
		{
			bool num = checkWeapon.tableData.type != baseWeapon.tableData.type;
			if (num)
			{
				isClear = true;
			}
			return num;
		}
		default:
			return false;
		}
	}

	public override bool IsMissionClear()
	{
		return isClear;
	}
}
