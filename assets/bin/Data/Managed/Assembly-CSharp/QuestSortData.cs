public class QuestSortData : SortCompareData
{
	public int enemyModelID;

	public QuestItemInfo itemData;

	public override object GetItemData()
	{
		return itemData;
	}

	public override void SetItem(object item)
	{
		itemData = (QuestItemInfo)item;
		enemyModelID = Singleton<EnemyTable>.I.GetEnemyData((uint)GetMainEnemyID()).modelId;
	}

	public ELEMENT_TYPE GetEnemyElement()
	{
		return Singleton<EnemyTable>.I.GetEnemyData((uint)GetMainEnemyID())?.element ?? ELEMENT_TYPE.MAX;
	}

	public EnemyTable.EnemyData GetEnemyData()
	{
		return Singleton<EnemyTable>.I.GetEnemyData((uint)GetMainEnemyID());
	}

	private int GetMainEnemyID()
	{
		return itemData.infoData.questData.tableData.GetMainEnemyID();
	}

	public override void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
		switch (requirement)
		{
		default:
			sortingData = itemData.infoData.questData.tableData.questID;
			break;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)itemData.infoData.questData.tableData.rarity;
			break;
		case SortBase.SORT_REQUIREMENT.NUM:
			sortingData = itemData.infoData.questData.num;
			break;
		case SortBase.SORT_REQUIREMENT.DIFFICULTY:
			sortingData = (long)itemData.infoData.questData.tableData.difficulty;
			break;
		case SortBase.SORT_REQUIREMENT.ENEMY:
		{
			EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)itemData.infoData.questData.tableData.GetMainEnemyID());
			if (enemyData == null)
			{
				sortingData = 0L;
			}
			else
			{
				sortingData = (long)enemyData.type;
			}
			break;
		}
		}
	}

	public override bool IsAbsFirst()
	{
		return MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(GetTableID());
	}

	public override bool IsFavorite()
	{
		return false;
	}

	public override ulong GetUniqID()
	{
		return itemData.uniqueID;
	}

	public override int GetItemType()
	{
		return EnemyTypeToSortItemType();
	}

	public override int GetNum()
	{
		return itemData.infoData.questData.num;
	}

	public override uint GetTableID()
	{
		return itemData.infoData.questData.tableData.questID;
	}

	public override string GetName()
	{
		return itemData.infoData.questData.tableData.questText;
	}

	public override RARITY_TYPE GetRarity()
	{
		return itemData.infoData.questData.tableData.rarity;
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ItemIcon.GetItemIconType(itemData.infoData.questData.tableData.questType);
	}

	public override ELEMENT_TYPE GetIconElement()
	{
		return GetEnemyElement();
	}

	public override bool CanSale()
	{
		return !itemData.infoData.questData.tableData.cantSale;
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.QUEST_ITEM;
	}

	public override int GetIconID()
	{
		EnemyTable.EnemyData enemyData = GetEnemyData();
		if (enemyData != null)
		{
			return enemyData.iconId;
		}
		Log.Error("ENEMY_TABLE_DATA is Not Found : main_enemy_id = " + GetMainEnemyID());
		return 0;
	}

	private int EnemyTypeToSortItemType()
	{
		int num = 0;
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)GetMainEnemyID());
		if (enemyData == null)
		{
			return 0;
		}
		switch (enemyData.type)
		{
		default:
			return 1;
		case ENEMY_TYPE.GORILLA:
			return 1;
		case ENEMY_TYPE.RABBIT:
			return 2;
		case ENEMY_TYPE.CHIMERA:
			return 4;
		case ENEMY_TYPE.WOLF:
			return 8;
		case ENEMY_TYPE.DRAGON:
			return 16;
		case ENEMY_TYPE.DRAKE:
			return 32;
		case ENEMY_TYPE.WYVERN:
			return 64;
		case ENEMY_TYPE.UNDEAD_KNIGHT:
			return 128;
		case ENEMY_TYPE.WRAITH:
			return 256;
		case ENEMY_TYPE.GIANT:
			return 512;
		case ENEMY_TYPE.GOLEM:
			return 1024;
		case ENEMY_TYPE.ELEMENTAL:
			return 2048;
		case ENEMY_TYPE.CHICKEN:
			return 4096;
		case ENEMY_TYPE.MUSHROOM:
			return 8192;
		case ENEMY_TYPE.COW:
			return 16384;
		case ENEMY_TYPE.FROG:
			return 32768;
		case ENEMY_TYPE.BAT:
			return 65536;
		case ENEMY_TYPE.SLIME:
			return 131072;
		case ENEMY_TYPE.SAHUAGIN:
			return 262144;
		}
	}
}
