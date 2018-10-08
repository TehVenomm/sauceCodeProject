using System;
using UnityEngine;

public class ExceedSkillItemTable : Singleton<ExceedSkillItemTable>, IDataTable
{
	public class ExceedSkillItemData
	{
		public const string NT = "id,exceedCnt,useGaugeRate,useGaugeRate2,startAt";

		public int id;

		public int exceedCnt;

		public int useGaugeRate;

		public int useGaugeRate2;

		public DateTime startAt;

		public static bool cb(CSVReader csv_reader, ExceedSkillItemData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.exceedCnt);
			csv_reader.Pop(ref data.useGaugeRate);
			csv_reader.Pop(ref data.useGaugeRate2);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				DateTime.TryParse(value, out data.startAt);
			}
			return true;
		}

		public int GetExceedUseGauge(int baseValue)
		{
			if (useGaugeRate == 0)
			{
				return baseValue;
			}
			return (int)((float)(baseValue * useGaugeRate) * 0.01f);
		}

		public int GetExceedUseGauge2(int baseValue)
		{
			if (useGaugeRate2 == 0)
			{
				return baseValue;
			}
			return (int)((float)(baseValue * useGaugeRate2) * 0.01f);
		}

		public int GetDecreaseUseGaugePercent()
		{
			return 100 - useGaugeRate;
		}
	}

	public static readonly Color color = Color.get_green();

	private UIntKeyTable<ExceedSkillItemData> exceedSkillItemTable;

	private int maxExceedCnt = -1;

	private int[] basePoints;

	private float[] skillExceedRate;

	public void CreateTable(string csv_text)
	{
		exceedSkillItemTable = TableUtility.CreateUIntKeyTable<ExceedSkillItemData>(csv_text, ExceedSkillItemData.cb, "id,exceedCnt,useGaugeRate,useGaugeRate2,startAt", null);
		exceedSkillItemTable.TrimExcess();
	}

	public ExceedSkillItemData GetExceedSkillItemData(int exceedCnt)
	{
		if (exceedCnt == 0)
		{
			return null;
		}
		return exceedSkillItemTable.Find((ExceedSkillItemData x) => x.startAt <= TimeManager.GetNow() && x.exceedCnt == exceedCnt);
	}

	public int GetMaxExceedCount()
	{
		if (maxExceedCnt < 0)
		{
			maxExceedCnt = 0;
			int max = 0;
			DateTime now = TimeManager.GetNow();
			exceedSkillItemTable.ForEach(delegate(ExceedSkillItemData x)
			{
				if (x.startAt <= now)
				{
					max++;
				}
			});
			for (int i = 0; i < max + 1; i++)
			{
				if (!IsExistExceed(i + 1))
				{
					maxExceedCnt = i;
					break;
				}
			}
		}
		return maxExceedCnt;
	}

	public int GetNeedExceedExp(RARITY_TYPE rarity, int exceedCnt)
	{
		float num = 0f;
		for (int i = 1; i < exceedCnt + 1 && i <= GetMaxExceedCount(); i++)
		{
			int exceedRarityBasePoint = GetExceedRarityBasePoint(rarity);
			float exceedRate = GetExceedRate(i);
			num += (float)exceedRarityBasePoint * exceedRate;
		}
		return (int)num;
	}

	public int GetExceedExp(SkillItemInfo material)
	{
		int exceedRarityBasePoint = GetExceedRarityBasePoint(material.tableData.rarity);
		float exceedLevelRate = GetExceedLevelRate(material.level, material.tableData.GetMaxLv(0));
		return (int)((float)exceedRarityBasePoint * exceedLevelRate);
	}

	private float GetExceedLevelRate(int level, int levelMax)
	{
		if (levelMax > 1)
		{
			float sKILL_EXCEED_MAX_LEVEL_RATE = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SKILL_EXCEED_MAX_LEVEL_RATE;
			return (float)(level - 1) * (sKILL_EXCEED_MAX_LEVEL_RATE - 1f) / (float)(levelMax - 1) + 1f;
		}
		return 1f;
	}

	public bool IsExistExceed(int exceedCnt)
	{
		return exceedSkillItemTable.Find((ExceedSkillItemData x) => x.exceedCnt == exceedCnt && x.startAt <= TimeManager.GetNow()) != null;
	}

	public void SetConst()
	{
		ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
		basePoints = new int[6]
		{
			0,
			constDefine.SKILL_EXCEED_POINT_RARITY_C,
			constDefine.SKILL_EXCEED_POINT_RARITY_B,
			constDefine.SKILL_EXCEED_POINT_RARITY_A,
			constDefine.SKILL_EXCEED_POINT_RARITY_S,
			constDefine.SKILL_EXCEED_POINT_RARITY_SS
		};
		skillExceedRate = new float[11]
		{
			0f,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_1,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_2,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_3,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_4,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_5,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_6,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_7,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_8,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_9,
			constDefine.SKILL_EXCEED_NEED_RATE_PLUS_10
		};
	}

	private int GetExceedRarityBasePoint(RARITY_TYPE type)
	{
		if (basePoints == null)
		{
			SetConst();
		}
		if ((int)type > basePoints.Length - 1)
		{
			Debug.LogError((object)"not define \"basePoints\" in ExceedSkillItemTable");
			return 0;
		}
		return basePoints[(int)type];
	}

	private float GetExceedRate(int exceedCnt)
	{
		if (skillExceedRate == null)
		{
			SetConst();
		}
		if (exceedCnt < 0 || exceedCnt > skillExceedRate.Length - 1)
		{
			Debug.LogError((object)("OutOfRange exceedCnt:" + exceedCnt + " (not define skillExceedRate in ExceedSkillItemTable?)"));
			return 0f;
		}
		return skillExceedRate[exceedCnt];
	}
}
