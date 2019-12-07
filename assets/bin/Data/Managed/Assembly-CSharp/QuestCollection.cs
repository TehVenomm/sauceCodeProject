using System.Collections.Generic;

public class QuestCollection
{
	public List<ENEMY_TYPE>[] enemyTypeListAry
	{
		get;
		private set;
	}

	public List<ENEMY_TYPE> GetEnemyTypeList(QUEST_TYPE type)
	{
		return _GetEnemyTypeList(type);
	}

	public List<ENEMY_TYPE> GetAllEnemyTypeList()
	{
		List<ENEMY_TYPE> ret = new List<ENEMY_TYPE>(enemyTypeListAry[0]);
		int i = 1;
		for (int num = enemyTypeListAry.Length; i < num; i++)
		{
			enemyTypeListAry[i].ForEach(delegate(ENEMY_TYPE add_data)
			{
				if (ret.FindIndex((ENEMY_TYPE _data) => _data == add_data) == -1)
				{
					ret.Add(add_data);
				}
			});
		}
		return ret;
	}

	public QuestCollection()
	{
		enemyTypeListAry = new List<ENEMY_TYPE>[3];
		int i = 0;
		for (int num = enemyTypeListAry.Length; i < num; i++)
		{
			enemyTypeListAry[i] = new List<ENEMY_TYPE>();
		}
	}

	public void Collect(ENEMY_TYPE type, QUEST_TYPE quest_type)
	{
		List<ENEMY_TYPE> list = _GetEnemyTypeList(quest_type);
		if (list != null && list.IndexOf(type) == -1)
		{
			list.Add(type);
		}
	}

	public void Sort()
	{
		int i = 0;
		for (int num = enemyTypeListAry.Length; i < num; i++)
		{
			enemyTypeListAry[i].Sort();
		}
	}

	private List<ENEMY_TYPE> _GetEnemyTypeList(QUEST_TYPE quest_type)
	{
		int num = 0;
		switch (quest_type)
		{
		default:
			return null;
		case QUEST_TYPE.NORMAL:
			num = 0;
			break;
		case QUEST_TYPE.EVENT:
			num = 1;
			break;
		case QUEST_TYPE.ORDER:
			num = 2;
			break;
		}
		return enemyTypeListAry[num];
	}
}
