using UnityEngine;

public class UserLevelTable : Singleton<UserLevelTable>, IDataTable
{
	public class UserLevelData
	{
		public XorInt lv = 0;

		public XorInt needExp = 0;

		public const string NT = "lv,needExp";

		public static bool cb(CSVReader csv_reader, UserLevelData data, ref uint key)
		{
			data.lv = (int)key;
			csv_reader.Pop(ref data.needExp);
			return true;
		}
	}

	private UIntKeyTable<UserLevelData> userLevelTable;

	private int maxLevel;

	public void CreateTable(string csv_text)
	{
		userLevelTable = TableUtility.CreateUIntKeyTable<UserLevelData>(csv_text, UserLevelData.cb, "lv,needExp");
		userLevelTable.TrimExcess();
	}

	public UserLevelData GetLevelTable(int level)
	{
		if (userLevelTable == null)
		{
			return null;
		}
		UserLevelData userLevelData = userLevelTable.Get((uint)level);
		if (userLevelData == null)
		{
			if (level <= GetMaxLevel())
			{
				Log.Error("UserLevelData is NULL :: id(Lv) = " + level);
			}
			return null;
		}
		if (level > GetMaxLevel())
		{
			return null;
		}
		return userLevelData;
	}

	public int GetMaxLevel()
	{
		if (maxLevel != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.USER_LEVEL_MAX && userLevelTable != null && userLevelTable.Get((uint)MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.USER_LEVEL_MAX) != null)
		{
			maxLevel = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.USER_LEVEL_MAX;
		}
		if (maxLevel > 0)
		{
			return maxLevel;
		}
		if (userLevelTable == null)
		{
			return 0;
		}
		userLevelTable.ForEach(delegate(UserLevelData data)
		{
			maxLevel = Mathf.Max(maxLevel, data.lv);
		});
		return maxLevel;
	}
}
