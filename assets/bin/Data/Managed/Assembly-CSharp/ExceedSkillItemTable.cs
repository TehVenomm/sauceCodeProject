// Decompiled with JetBrains decompiler
// Type: ExceedSkillItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ExceedSkillItemTable : Singleton<ExceedSkillItemTable>, IDataTable
{
  public static readonly Color color = Color.green;
  private UIntKeyTable<ExceedSkillItemTable.ExceedSkillItemData> exceedSkillItemTable;
  private int maxExceedCnt = -1;
  private int[] basePoints;
  private float[] sameSkillPointRates;
  private float[] skillExceedRate;

  public void CreateTable(string csv_text)
  {
    this.exceedSkillItemTable = TableUtility.CreateUIntKeyTable<ExceedSkillItemTable.ExceedSkillItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<ExceedSkillItemTable.ExceedSkillItemData>(ExceedSkillItemTable.ExceedSkillItemData.cb), "id,exceedCnt,useGaugeRate,useGaugeRate2,startAt");
    this.exceedSkillItemTable.TrimExcess();
  }

  public ExceedSkillItemTable.ExceedSkillItemData GetExceedSkillItemData(int exceedCnt)
  {
    return exceedCnt == 0 ? (ExceedSkillItemTable.ExceedSkillItemData) null : this.exceedSkillItemTable.Find((Predicate<ExceedSkillItemTable.ExceedSkillItemData>) (x => x.startAt <= TimeManager.GetNow() && x.exceedCnt == exceedCnt));
  }

  public int GetMaxExceedCount()
  {
    if (this.maxExceedCnt < 0)
    {
      this.maxExceedCnt = 0;
      int max = 0;
      DateTime now = TimeManager.GetNow();
      this.exceedSkillItemTable.ForEach((Action<ExceedSkillItemTable.ExceedSkillItemData>) (x =>
      {
        if (!(x.startAt <= now))
          return;
        ++max;
      }));
      for (int index = 0; index < max + 1; ++index)
      {
        if (!this.IsExistExceed(index + 1))
        {
          this.maxExceedCnt = index;
          break;
        }
      }
    }
    return this.maxExceedCnt;
  }

  public int GetNeedExceedExp(RARITY_TYPE rarity, int exceedCnt)
  {
    float needExceedExp = 0.0f;
    for (int exceedCnt1 = 1; exceedCnt1 < exceedCnt + 1 && exceedCnt1 <= this.GetMaxExceedCount(); ++exceedCnt1)
    {
      int exceedRarityBasePoint = this.GetExceedRarityBasePoint(rarity);
      float exceedRate = this.GetExceedRate(exceedCnt1);
      needExceedExp += (float) exceedRarityBasePoint * exceedRate;
    }
    return (int) needExceedExp;
  }

  public int GetExceedExp(SkillItemInfo material)
  {
    return (int) ((double) this.GetExceedRarityBasePoint(material.tableData.rarity) * (double) this.GetExceedLevelRate(material.level, material.tableData.GetMaxLv(0)));
  }

  private float GetExceedLevelRate(int level, int levelMax)
  {
    if (levelMax <= 1)
      return 1f;
    float exceedMaxLevelRate = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SKILL_EXCEED_MAX_LEVEL_RATE;
    return (float) ((double) (level - 1) * ((double) exceedMaxLevelRate - 1.0) / (double) (levelMax - 1) + 1.0);
  }

  public bool IsExistExceed(int exceedCnt)
  {
    return this.exceedSkillItemTable.Find((Predicate<ExceedSkillItemTable.ExceedSkillItemData>) (x => x.exceedCnt == exceedCnt && x.startAt <= TimeManager.GetNow())) != null;
  }

  public void SetConst()
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    this.basePoints = new int[7]
    {
      0,
      constDefine.SKILL_EXCEED_POINT_RARITY_C,
      constDefine.SKILL_EXCEED_POINT_RARITY_B,
      constDefine.SKILL_EXCEED_POINT_RARITY_A,
      constDefine.SKILL_EXCEED_POINT_RARITY_S,
      constDefine.SKILL_EXCEED_POINT_RARITY_SS,
      constDefine.SKILL_EXCEED_POINT_RARITY_SSS
    };
    this.sameSkillPointRates = new float[7]
    {
      1f,
      constDefine.SKILL_EXCEED_SAME_RATE_C,
      constDefine.SKILL_EXCEED_SAME_RATE_B,
      constDefine.SKILL_EXCEED_SAME_RATE_A,
      constDefine.SKILL_EXCEED_SAME_RATE_S,
      constDefine.SKILL_EXCEED_SAME_RATE_SS,
      constDefine.SKILL_EXCEED_SAME_RATE_SSS
    };
    this.skillExceedRate = new float[11]
    {
      0.0f,
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
    if (this.basePoints == null)
      this.SetConst();
    if (type <= (RARITY_TYPE) (this.basePoints.Length - 1))
      return this.basePoints[(int) type];
    Debug.LogError((object) "not define \"basePoints\" in ExceedSkillItemTable");
    return 0;
  }

  public float GetExceedRaritySamePointRate(RARITY_TYPE type)
  {
    if (this.sameSkillPointRates == null)
      this.SetConst();
    int index = (int) type;
    if (index <= this.sameSkillPointRates.Length - 1)
      return this.sameSkillPointRates[index];
    Debug.LogError((object) "not define \"sameSkillPointRates\" in ExceedSkillItemTable");
    return 1f;
  }

  private float GetExceedRate(int exceedCnt)
  {
    if (this.skillExceedRate == null)
      this.SetConst();
    if (exceedCnt >= 0 && exceedCnt <= this.skillExceedRate.Length - 1)
      return this.skillExceedRate[exceedCnt];
    Debug.LogError((object) $"OutOfRange exceedCnt:{(object) exceedCnt} (not define skillExceedRate in ExceedSkillItemTable?)");
    return 0.0f;
  }

  public class ExceedSkillItemData
  {
    public int id;
    public int exceedCnt;
    public int useGaugeRate;
    public int useGaugeRate2;
    public DateTime startAt;
    public const string NT = "id,exceedCnt,useGaugeRate,useGaugeRate2,startAt";

    public static bool cb(
      CSVReader csv_reader,
      ExceedSkillItemTable.ExceedSkillItemData data,
      ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.exceedCnt);
      csv_reader.Pop(ref data.useGaugeRate);
      csv_reader.Pop(ref data.useGaugeRate2);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
        DateTime.TryParse(empty, out data.startAt);
      return true;
    }

    public int GetExceedUseGauge(int baseValue)
    {
      return this.useGaugeRate == 0 ? baseValue : (int) ((double) (baseValue * this.useGaugeRate) * 0.0099999997764825821);
    }

    public int GetExceedUseGauge2(int baseValue)
    {
      return this.useGaugeRate2 == 0 ? baseValue : (int) ((double) (baseValue * this.useGaugeRate2) * 0.0099999997764825821);
    }

    public int GetDecreaseUseGaugePercent() => 100 - this.useGaugeRate;
  }
}
