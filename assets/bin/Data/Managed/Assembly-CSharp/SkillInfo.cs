// Decompiled with JetBrains decompiler
// Type: SkillInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SkillInfo
{
  public const int SKILL_INDEX_NONE = -1;
  public const int ACTIVE_SKILL_MAX = 3;
  protected List<SkillInfo.SkillParam> skillParams = new List<SkillInfo.SkillParam>();
  public int skillIndex = -1;
  private ARENA_CONDITION[] arenaConditionList;
  private bool isArenaForbidMagiAttack;
  private bool isArenaForbidMagiSupport;
  private bool isArenaForbidMagiHeal;

  public Player player { get; protected set; }

  public int weaponOffset
  {
    get
    {
      int num = this.player.weaponIndex;
      if (num < 0)
        num = 0;
      return num * 3;
    }
  }

  public SkillInfo.SkillParam actSkillParam
  {
    get
    {
      SkillInfo.SkillParam skillParam = this.GetSkillParam(this.skillIndex);
      return skillParam == null || !skillParam.isValid ? (SkillInfo.SkillParam) null : skillParam;
    }
  }

  public SkillInfo(Player owner)
  {
    this.player = owner;
    this.skillParams = new List<SkillInfo.SkillParam>();
    int num = 0;
    for (int index = 9; num < index; ++num)
      this.skillParams.Add(new SkillInfo.SkillParam()
      {
        skillIndex = num
      });
  }

  public SkillInfo.SkillParam GetSkillParam(int skill_index)
  {
    if (this.skillParams == null)
      return (SkillInfo.SkillParam) null;
    if (skill_index < 0 || skill_index >= this.skillParams.Count)
      return (SkillInfo.SkillParam) null;
    SkillInfo.SkillParam skillParam = this.skillParams[skill_index];
    return skillParam == null || !skillParam.isValid ? (SkillInfo.SkillParam) null : skillParam;
  }

  public bool IsActSkillAction(int skill_index)
  {
    SkillInfo.SkillParam skillParam = this.GetSkillParam(skill_index);
    return skillParam != null && (double) this.GetPercentUseGauge(skill_index) >= 1.0 && skillParam.IsActiveType() && !this.IsArenaForbidSlotType(skillParam.tableData.type);
  }

  public float GetPercentUseGauge(int skill_index)
  {
    SkillInfo.SkillParam skillParam = this.GetSkillParam(skill_index);
    if (skillParam == null)
      return 0.0f;
    return (double) (int) skillParam.useGauge <= 0.0 ? 1f : Mathf.Clamp01(skillParam.useGaugeCounter / (float) (int) skillParam.useGauge);
  }

  public float GetPercentUseGauge2nd(int skill_index)
  {
    if ((double) this.GetPercentUseGauge(skill_index) < 1.0)
      return 0.0f;
    SkillInfo.SkillParam skillParam = this.GetSkillParam(skill_index);
    return (double) (int) skillParam.useGauge2 <= 0.0 ? 0.0f : Mathf.Clamp01((skillParam.useGaugeCounter - (float) (int) skillParam.useGauge) / (float) (int) skillParam.useGauge2);
  }

  public void OnUpdate()
  {
    if (this.player.isDead || this.player.isProgressStop())
      return;
    this.AddUseGauge(MonoBehaviourSingleton<InGameSettingsManager>.I.player.healSkillGaugePerSecond * Time.deltaTime * this.player.buffParam.GetSkillHealSpeedUp(), true);
  }

  public void OnActSkillAction()
  {
    SkillInfo.SkillParam actSkillParam = this.actSkillParam;
    if (actSkillParam == null)
      return;
    actSkillParam.isUsingSecondGrade = false;
    if ((int) actSkillParam.useGauge2 <= 0)
      actSkillParam.useGaugeCounter = 0.0f;
    else if ((double) actSkillParam.useGaugeCounter >= (double) (int) actSkillParam.GetMaxGaugeValue())
    {
      actSkillParam.useGaugeCounter = 0.0f;
      actSkillParam.isUsingSecondGrade = true;
    }
    else
    {
      float num = (actSkillParam.useGaugeCounter - (float) (int) actSkillParam.useGauge) / (float) (int) actSkillParam.useGauge2;
      actSkillParam.useGaugeCounter = num * (float) (int) actSkillParam.useGauge;
    }
  }

  public void OnHitAttackEnemy(AttackHitInfo attack_info)
  {
    if (this.player.weaponInfo == null)
      return;
    this.AddUseGauge(this.player.playerParameter.healSkillGaugeHit * (attack_info.toEnemy.isSpecialAttack ? this.player.weaponInfo.healSkillGaugeHitRateSpecialAttack : this.player.weaponInfo.healSkillGaugeHitRate) * attack_info.atkRate, true, true);
  }

  public void OnHitCounterEnemy(AttackHitInfo attack_info)
  {
    if (this.player.weaponInfo == null)
      return;
    this.AddUseGauge(this.player.playerParameter.healSkillGaugeHit * this.player.playerParameter.ohsActionInfo.Common_CounterHealSkillRate, true, true);
  }

  public void DebugAddUseGauge1st()
  {
    int index1 = 0;
    for (int index2 = 9; index1 < index2; ++index1)
    {
      if (this.skillParams[index1].isValid && (double) (int) this.skillParams[index1].useGauge2 > 0.0)
      {
        this.AddUseGaugeByIndex(index1, float.MinValue);
        this.AddUseGaugeByIndex(index1, (float) (int) this.skillParams[index1].useGauge);
      }
    }
  }

  public void AddUseGauge(float add_gauge, bool set_only = false, bool isNotBuff = false)
  {
    int index1 = 0;
    for (int index2 = 9; index1 < index2; ++index1)
      this.AddUseGaugeByIndex(index1, add_gauge, set_only, isNotBuff: isNotBuff);
  }

  public void AddUseGaugeByIndex(
    int index,
    float add_gauge,
    bool set_only = false,
    bool isForceSet = false,
    bool isNotBuff = false,
    bool isCorrectWaveMatch = true)
  {
    if (set_only && (index < this.weaponOffset || index >= this.weaponOffset + 3) || !this.skillParams[index].isValid || this.IsArenaForbidSlotType(this.skillParams[index].tableData.type))
      return;
    if (!isForceSet && !((IList<int>) this.skillParams[index].tableData.lockBuffTypes).IsNullOrEmpty<int>())
    {
      foreach (BuffParam.BUFFTYPE lockBuffType in this.skillParams[index].tableData.lockBuffTypes)
      {
        if (this.player.IsValidBuff(lockBuffType))
          return;
      }
    }
    if (this.player.IsValidShield() && this.player.buffParam.IsValidShieldBuff(this.skillParams[index].skillIndex) && !isForceSet)
      return;
    if (this.player.IsValidShield())
    {
      int shieldFromSkillIndex = this.player.buffParam.GetShieldFromSkillIndex();
      if (shieldFromSkillIndex < 0 || this.IsSkillApplyShield(this.skillParams[index].tableData) && this.IsExistShieldInLockBuffTypes(this.skillParams[shieldFromSkillIndex].tableData.lockBuffTypes) && !isForceSet)
        return;
    }
    float useGaugeCounter = this.skillParams[index].useGaugeCounter;
    float num1 = add_gauge;
    if (isNotBuff)
    {
      if (this.skillParams[index].tableData.GetAttackElementNum() <= 1)
        num1 *= this.player.buffParam.GetSkillAbsorbUp(this.skillParams[index].tableData.type == SKILL_SLOT_TYPE.ATTACK ? this.skillParams[index].tableData.skillAtkType : ELEMENT_TYPE.MAX);
      else if (this.skillParams[index].tableData.type == SKILL_SLOT_TYPE.ATTACK)
        num1 *= this.player.buffParam.GetSkillAbsorbUpByElementList(this.skillParams[index].tableData.skillAtkTypes);
      if (isCorrectWaveMatch && QuestManager.IsValidInGameWaveMatch() && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      {
        InGameSettingsManager.WaveMatchParam waveMatchParam = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
        switch (waveMatchParam.skillGaugeType)
        {
          case InGameSettingsManager.WaveMatchParam.eGaugeType.Zero:
            return;
          case InGameSettingsManager.WaveMatchParam.eGaugeType.Rate:
            num1 *= waveMatchParam.skillGaugeValue;
            break;
          case InGameSettingsManager.WaveMatchParam.eGaugeType.Constant:
            num1 = waveMatchParam.skillGaugeValue;
            break;
        }
      }
    }
    float num2;
    if ((double) num1 == 3.4028234663852886E+38)
      num2 = (float) (int) this.skillParams[index].GetMaxGaugeValue();
    else if ((double) num1 == -3.4028234663852886E+38)
    {
      num2 = 0.0f;
    }
    else
    {
      num2 = useGaugeCounter + num1;
      if ((double) num2 < 0.0)
        num2 = 0.0f;
      if ((double) num2 > (double) (int) this.skillParams[index].GetMaxGaugeValue())
        num2 = (float) (int) this.skillParams[index].GetMaxGaugeValue();
    }
    this.skillParams[index].useGaugeCounter = num2;
  }

  public void SetUseGaugeFull()
  {
    int index1 = 0;
    for (int index2 = 9; index1 < index2; ++index1)
    {
      if (this.skillParams[index1].isValid)
        this.skillParams[index1].useGaugeCounter = (float) (int) this.skillParams[index1].GetMaxGaugeValue();
    }
  }

  public void ResetUseGauge()
  {
    int index1 = 0;
    for (int index2 = 9; index1 < index2; ++index1)
    {
      if (this.skillParams[index1].isValid)
        this.skillParams[index1].useGaugeCounter = 0.0f;
    }
  }

  public void ResetSecondGradeFlags()
  {
    int index1 = 0;
    for (int index2 = 9; index1 < index2; ++index1)
    {
      if (this.skillParams[index1].isValid)
        this.skillParams[index1].isUsingSecondGrade = false;
    }
  }

  public void SetSettingsInfo(
    SkillInfo.SkillSettingsInfo skill_settings,
    List<CharaInfo.EquipItem> weapon_list)
  {
    List<SkillInfo.SkillParam> skillParamList = new List<SkillInfo.SkillParam>();
    if (this.skillParams != null)
      skillParamList = this.skillParams;
    this.skillParams = new List<SkillInfo.SkillParam>();
    InGameManager i1 = MonoBehaviourSingleton<InGameManager>.I;
    InGameSettingsManager i2 = MonoBehaviourSingleton<InGameSettingsManager>.I;
    int index1 = 0;
    for (int index2 = 9; index1 < index2; ++index1)
    {
      SkillInfo.SkillParam skillParam1 = new SkillInfo.SkillParam();
      skillParam1.skillIndex = index1;
      this.skillParams.Add(skillParam1);
      if (skill_settings != null && Singleton<SkillItemTable>.IsValid())
      {
        SkillInfo.SkillSettingsInfo.Element element = skill_settings.elementList[index1];
        if (element != null)
        {
          skillParam1.baseInfo = element.baseInfo;
          skillParam1.useGaugeCounter = element.useGaugeCounter;
          if (skillParam1.baseInfo != null && skillParam1.baseInfo.id != 0)
          {
            skillParam1.tableData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) skillParam1.baseInfo.id);
            if (skillParam1.tableData != null && skillParam1.IsActiveType())
            {
              if (weapon_list[index1 / 3] != null)
              {
                EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) weapon_list[index1 / 3].eId);
                if (equipItemData != null && !skillParam1.tableData.IsEnableEquipType(equipItemData.type))
                  continue;
              }
              switch (skillParam1.tableData.skillAtkType)
              {
                case ELEMENT_TYPE.FIRE:
                  skillParam1.atk.fire = (float) (int) skillParam1.tableData.skillAtk;
                  break;
                case ELEMENT_TYPE.WATER:
                  skillParam1.atk.water = (float) (int) skillParam1.tableData.skillAtk;
                  break;
                case ELEMENT_TYPE.THUNDER:
                  skillParam1.atk.thunder = (float) (int) skillParam1.tableData.skillAtk;
                  break;
                case ELEMENT_TYPE.SOIL:
                  skillParam1.atk.soil = (float) (int) skillParam1.tableData.skillAtk;
                  break;
                case ELEMENT_TYPE.LIGHT:
                  skillParam1.atk.light = (float) (int) skillParam1.tableData.skillAtk;
                  break;
                case ELEMENT_TYPE.DARK:
                  skillParam1.atk.dark = (float) (int) skillParam1.tableData.skillAtk;
                  break;
                default:
                  skillParam1.atk.normal = (float) (int) skillParam1.tableData.skillAtk;
                  break;
              }
              int attackElementNum = skillParam1.tableData.GetAttackElementNum();
              if (attackElementNum > 0)
              {
                skillParam1.atkRates = new float[attackElementNum];
                for (int index3 = 0; index3 < attackElementNum; ++index3)
                  skillParam1.atkRates[index3] = (float) (int) skillParam1.tableData.skillAtkRates[index3] * 0.01f;
              }
              skillParam1.healHp = (int) skillParam1.tableData.healHp;
              for (int index4 = 0; index4 < 3; ++index4)
              {
                skillParam1.supportValue[index4] = skillParam1.tableData.supportValue[index4];
                skillParam1.supportTime[index4] = skillParam1.tableData.supportTime[index4];
              }
              skillParam1.castTimeRate = (XorFloat) 0.0f;
              skillParam1.useGauge = skillParam1.tableData.useGauge;
              skillParam1.useGauge2 = skillParam1.tableData.useGauge2;
              if (Singleton<GrowSkillItemTable>.IsValid())
              {
                GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillParam1.tableData.growID, skillParam1.baseInfo.level, skillParam1.baseInfo.exceedCnt);
                if (growSkillItemData != null)
                {
                  float growParamSkillAtk = (float) growSkillItemData.GetGrowParamSkillAtk((int) skillParam1.tableData.skillAtk);
                  switch (skillParam1.tableData.skillAtkType)
                  {
                    case ELEMENT_TYPE.FIRE:
                      skillParam1.atk.fire = growParamSkillAtk;
                      break;
                    case ELEMENT_TYPE.WATER:
                      skillParam1.atk.water = growParamSkillAtk;
                      break;
                    case ELEMENT_TYPE.THUNDER:
                      skillParam1.atk.thunder = growParamSkillAtk;
                      break;
                    case ELEMENT_TYPE.SOIL:
                      skillParam1.atk.soil = growParamSkillAtk;
                      break;
                    case ELEMENT_TYPE.LIGHT:
                      skillParam1.atk.light = growParamSkillAtk;
                      break;
                    case ELEMENT_TYPE.DARK:
                      skillParam1.atk.dark = growParamSkillAtk;
                      break;
                    default:
                      skillParam1.atk.normal = growParamSkillAtk;
                      break;
                  }
                  if (attackElementNum > 0)
                  {
                    for (int index5 = 0; index5 < attackElementNum; ++index5)
                      skillParam1.atkRates[index5] = (float) growSkillItemData.GetGrowParamSkillAtkRate((int) skillParam1.tableData.skillAtkRates[index5]) * 0.01f;
                  }
                  skillParam1.healHp = growSkillItemData.GetGrowParamHealHp((int) skillParam1.tableData.healHp);
                  for (int index6 = 0; index6 < 3; ++index6)
                  {
                    skillParam1.supportValue[index6] = growSkillItemData.GetGrowParamSupprtValue(skillParam1.tableData.supportValue, index6);
                    skillParam1.supportTime[index6] = growSkillItemData.GetGrowParamSupprtTime(skillParam1.tableData.supportTime, index6);
                  }
                  skillParam1.castTimeRate = (XorFloat) growSkillItemData.GetGrowParamCastTimeRate();
                  skillParam1.useGauge = (XorInt) growSkillItemData.GetGrowParamUseGauge((int) skillParam1.tableData.useGauge);
                  skillParam1.useGauge2 = (XorInt) growSkillItemData.GetGrowParamUseGauge2((int) skillParam1.tableData.useGauge2);
                }
              }
              if (Singleton<ExceedSkillItemTable>.IsValid())
              {
                ExceedSkillItemTable.ExceedSkillItemData exceedSkillItemData = Singleton<ExceedSkillItemTable>.I.GetExceedSkillItemData(skillParam1.baseInfo.exceedCnt);
                if (exceedSkillItemData != null)
                {
                  skillParam1.useGauge = (XorInt) exceedSkillItemData.GetExceedUseGauge((int) skillParam1.useGauge);
                  skillParam1.useGauge2 = (XorInt) exceedSkillItemData.GetExceedUseGauge2((int) skillParam1.useGauge2);
                }
              }
              if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
              {
                InGameSettingsManager.ArenaParam arenaParam = i2.arenaParam;
                if ((double) skillParam1.baseInfo.exceedCnt < (double) arenaParam.magiSpeedDownRegistSkillExceedLv && i1.ContainsArenaCondition(ARENA_CONDITION.RECOVER_MAGI_SPEED_DOWN))
                {
                  float num = arenaParam.magiSpeedDownRateBase - arenaParam.magiSpeedDownRate * (float) skillParam1.baseInfo.exceedCnt;
                  if ((double) num < 0.0)
                    num = 0.0f;
                  SkillInfo.SkillParam skillParam2 = skillParam1;
                  skillParam2.useGauge = (XorInt) ((int) skillParam2.useGauge + Mathf.FloorToInt((float) (int) skillParam1.useGauge * num));
                  SkillInfo.SkillParam skillParam3 = skillParam1;
                  skillParam3.useGauge2 = (XorInt) ((int) skillParam3.useGauge2 + Mathf.FloorToInt((float) (int) skillParam1.useGauge2 * num));
                }
                if (i1.ContainsArenaCondition(ARENA_CONDITION.RECOVER_MAGI_SPEED_UP))
                {
                  SkillInfo.SkillParam skillParam4 = skillParam1;
                  skillParam4.useGauge = (XorInt) ((int) skillParam4.useGauge - Mathf.FloorToInt((float) (int) skillParam1.useGauge * arenaParam.magiSpeedUpBaseRate));
                  SkillInfo.SkillParam skillParam5 = skillParam1;
                  skillParam5.useGauge2 = (XorInt) ((int) skillParam5.useGauge2 - Mathf.FloorToInt((float) (int) skillParam1.useGauge2 * arenaParam.magiSpeedUpBaseRate));
                }
              }
              if (skillParamList != null && skillParamList.Count > 0)
              {
                for (int index7 = 0; index7 < skillParamList.Count; ++index7)
                {
                  if (skillParamList[index7] != null && skillParamList[index7].tableData != null && skillParamList[index7].tableData.name == skillParam1.tableData.name && Object.op_Inequality((Object) skillParamList[index7].bullet, (Object) null))
                    skillParam1.bullet = skillParamList[index1].bullet;
                }
              }
              skillParam1.isValid = true;
            }
          }
        }
      }
    }
    this.arenaConditionList = i1.GetArenaConditions();
    if (((IList<ARENA_CONDITION>) this.arenaConditionList).IsNullOrEmpty<ARENA_CONDITION>())
      return;
    for (int index8 = 0; index8 < this.arenaConditionList.Length; ++index8)
    {
      switch (this.arenaConditionList[index8])
      {
        case ARENA_CONDITION.FORBID_MAGI_ATTACK:
          this.isArenaForbidMagiAttack = true;
          break;
        case ARENA_CONDITION.FORBID_MAGI_SUPPORT:
          this.isArenaForbidMagiSupport = true;
          break;
        case ARENA_CONDITION.FORBID_MAGI_HEAL:
          this.isArenaForbidMagiHeal = true;
          break;
      }
    }
  }

  private bool IsArenaForbidSlotType(SKILL_SLOT_TYPE slotType)
  {
    switch (slotType)
    {
      case SKILL_SLOT_TYPE.ATTACK:
        return this.isArenaForbidMagiAttack;
      case SKILL_SLOT_TYPE.SUPPORT:
        return this.isArenaForbidMagiSupport;
      case SKILL_SLOT_TYPE.HEAL:
        return this.isArenaForbidMagiHeal;
      default:
        return false;
    }
  }

  private bool IsSkillApplyShield(SkillItemTable.SkillItemData tableData)
  {
    if (tableData == null)
      return false;
    if (!((IList<BuffParam.BUFFTYPE>) tableData.supportType).IsNullOrEmpty<BuffParam.BUFFTYPE>() && Array.IndexOf<BuffParam.BUFFTYPE>(tableData.supportType, BuffParam.BUFFTYPE.SHIELD) >= 0)
      return true;
    if (!((IList<int>) tableData.buffTableIds).IsNullOrEmpty<int>() && Singleton<BuffTable>.IsValid())
    {
      for (int index = 0; index < tableData.buffTableIds.Length; ++index)
      {
        if (Singleton<BuffTable>.I.GetData((uint) tableData.buffTableIds[index]).type == BuffParam.BUFFTYPE.SHIELD)
          return true;
      }
    }
    return false;
  }

  private bool IsExistShieldInLockBuffTypes(int[] lockBuffTypes)
  {
    return !((IList<int>) lockBuffTypes).IsNullOrEmpty<int>() && Array.IndexOf<int>(lockBuffTypes, 60) >= 0;
  }

  [Serializable]
  public class SkillBaseInfo
  {
    public int id;
    public int level;
    public int exceedCnt;
  }

  [Serializable]
  public class SkillSettingsInfo
  {
    public List<SkillInfo.SkillSettingsInfo.Element> elementList = new List<SkillInfo.SkillSettingsInfo.Element>();

    [Serializable]
    public class Element
    {
      public SkillInfo.SkillBaseInfo baseInfo = new SkillInfo.SkillBaseInfo();
      public float useGaugeCounter;
    }
  }

  public class SkillParam
  {
    public int skillIndex = -1;
    public bool isValid;
    public SkillInfo.SkillBaseInfo baseInfo;
    public SkillItemTable.SkillItemData tableData;
    public float useGaugeCounter;
    public bool isUsingSecondGrade;
    public AtkAttribute atk = new AtkAttribute();
    public float[] atkRates;
    public int healHp;
    public int[] supportValue = new int[3];
    public float[] supportTime = new float[3];
    public BulletData bullet;
    public XorFloat castTimeRate = (XorFloat) 0.0f;
    public XorInt useGauge;
    public XorInt useGauge2;

    public float atkRate
    {
      get => this.atkRates != null && this.atkRates.Length != 0 ? this.atkRates[0] : 1f;
    }

    public XorInt GetMaxGaugeValue() => (XorInt) ((int) this.useGauge + (int) this.useGauge2);

    public bool IsActiveType()
    {
      if (this.tableData == null)
        return false;
      bool flag = false;
      switch (this.tableData.type)
      {
        case SKILL_SLOT_TYPE.ATTACK:
        case SKILL_SLOT_TYPE.SUPPORT:
        case SKILL_SLOT_TYPE.HEAL:
          flag = true;
          break;
      }
      return flag;
    }

    public float GetAttackRateByIndex(int index)
    {
      return this.atkRates != null && this.atkRates.Length > index ? this.atkRates[index] : 1f;
    }

    public int GetSupportValueTotalByType(BuffParam.BUFFTYPE type)
    {
      if (this.tableData == null)
        return 0;
      int valueTotalByType = 0;
      for (int index = 0; index < this.tableData.supportType.Length; ++index)
      {
        if (this.tableData.supportType[index] == type)
          valueTotalByType += this.supportValue[index];
      }
      return valueTotalByType;
    }

    public float GetSupportValueTotalAsRateByType(BuffParam.BUFFTYPE type)
    {
      if (this.tableData == null)
        return 0.0f;
      float totalAsRateByType = 0.0f;
      for (int index = 0; index < this.tableData.supportType.Length; ++index)
      {
        if (this.tableData.supportType[index] == type)
          totalAsRateByType += (float) this.supportValue[index] * 0.01f;
      }
      return totalAsRateByType;
    }
  }
}
