using System;
using System.Collections.Generic;
using System.Linq;

[Obsolete]
public class EnemyFieldDropItemTable : Singleton<EnemyFieldDropItemTable>
{
	public class EnemyFieldDropItemData
	{
		public uint enemyId;

		public uint itemId;

		public uint fieldId;

		public List<int> partIds = new List<int>(5);

		public EnemyFieldDropItemData(uint eId, uint iId, uint fId, List<int> pIds)
		{
			enemyId = eId;
			itemId = iId;
			fieldId = fId;
			partIds = pIds;
		}

		public override string ToString()
		{
			return $"enemyId:{enemyId} itemId:{itemId} fieldId:{fieldId} partsCount:{partIds.Count}";
		}
	}

	public const int PART_REWARD_NUM_MUX = 5;

	private Dictionary<uint, List<EnemyFieldDropItemData>> enemyToItemTable = new Dictionary<uint, List<EnemyFieldDropItemData>>();

	private object tableLock = new object();

	public unsafe void Add(uint enemyId, uint itemId, uint fieldId, List<int> partIds)
	{
		lock (tableLock)
		{
			if (!enemyToItemTable.TryGetValue(enemyId, out List<EnemyFieldDropItemData> value))
			{
				value = new List<EnemyFieldDropItemData>();
				enemyToItemTable[enemyId] = value;
			}
			_003CAdd_003Ec__AnonStorey763 _003CAdd_003Ec__AnonStorey;
			if (!value.Any(new Func<EnemyFieldDropItemData, bool>((object)_003CAdd_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)))
			{
				EnemyFieldDropItemData item = new EnemyFieldDropItemData(enemyId, itemId, fieldId, partIds);
				value.Add(item);
			}
		}
	}

	public List<EnemyFieldDropItemData> GetEnemyData(uint enemyId)
	{
		List<EnemyFieldDropItemData> tempList = new List<EnemyFieldDropItemData>();
		if (enemyToItemTable.ContainsKey(enemyId))
		{
			enemyToItemTable[enemyId].ForEach(delegate(EnemyFieldDropItemData x)
			{
				if (x.enemyId == enemyId)
				{
					tempList.Add(x);
				}
			});
		}
		return tempList;
	}
}
