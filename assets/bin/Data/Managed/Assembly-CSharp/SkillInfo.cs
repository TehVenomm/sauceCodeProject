using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
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
		[Serializable]
		public class Element
		{
			public SkillBaseInfo baseInfo = new SkillBaseInfo();

			public float useGaugeCounter;
		}

		public List<Element> elementList = new List<Element>();
	}

	public class SkillParam
	{
		public int skillIndex = -1;

		public bool isValid;

		public SkillBaseInfo baseInfo;

		public SkillItemTable.SkillItemData tableData;

		public float useGaugeCounter;

		public bool isUsingSecondGrade;

		public AtkAttribute atk = new AtkAttribute();

		public float[] atkRates;

		public int healHp;

		public int[] supportValue = new int[3];

		public float[] supportTime = new float[3];

		public BulletData bullet;

		public XorFloat castTimeRate = 0f;

		public XorInt useGauge;

		public XorInt useGauge2;

		public float atkRate
		{
			get
			{
				if (atkRates != null && atkRates.Length > 0)
				{
					return atkRates[0];
				}
				return 1f;
			}
		}

		public XorInt GetMaxGaugeValue()
		{
			return (int)useGauge + (int)useGauge2;
		}

		public bool IsActiveType()
		{
			if (tableData == null)
			{
				return false;
			}
			bool result = false;
			switch (tableData.type)
			{
			case SKILL_SLOT_TYPE.ATTACK:
			case SKILL_SLOT_TYPE.SUPPORT:
			case SKILL_SLOT_TYPE.HEAL:
				result = true;
				break;
			}
			return result;
		}

		public float GetAttackRateByIndex(int index)
		{
			if (atkRates != null && atkRates.Length > index)
			{
				return atkRates[index];
			}
			return 1f;
		}
	}

	public const int SKILL_INDEX_NONE = -1;

	public const int ACTIVE_SKILL_MAX = 3;

	protected List<SkillParam> skillParams = new List<SkillParam>();

	public int skillIndex = -1;

	private ARENA_CONDITION[] arenaConditionList;

	private bool isArenaForbidMagiAttack;

	private bool isArenaForbidMagiSupport;

	private bool isArenaForbidMagiHeal;

	public Player player
	{
		get;
		protected set;
	}

	public int weaponOffset
	{
		get
		{
			int num = player.weaponIndex;
			if (num < 0)
			{
				num = 0;
			}
			return num * 3;
		}
	}

	public SkillParam actSkillParam
	{
		get
		{
			SkillParam skillParam = GetSkillParam(skillIndex);
			if (skillParam == null || !skillParam.isValid)
			{
				return null;
			}
			return skillParam;
		}
	}

	public SkillInfo(Player owner)
	{
		player = owner;
		skillParams = new List<SkillParam>();
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			SkillParam item = new SkillParam
			{
				skillIndex = i
			};
			skillParams.Add(item);
		}
	}

	public SkillParam GetSkillParam(int skill_index)
	{
		if (skillParams == null)
		{
			return null;
		}
		if (skill_index < 0 || skill_index >= skillParams.Count)
		{
			return null;
		}
		SkillParam skillParam = skillParams[skill_index];
		if (skillParam == null || !skillParam.isValid)
		{
			return null;
		}
		return skillParam;
	}

	public bool IsActSkillAction(int skill_index)
	{
		SkillParam skillParam = GetSkillParam(skill_index);
		if (skillParam == null)
		{
			return false;
		}
		if (GetPercentUseGauge(skill_index) < 1f)
		{
			return false;
		}
		if (!skillParam.IsActiveType())
		{
			return false;
		}
		if (IsArenaForbidSlotType(skillParam.tableData.type))
		{
			return false;
		}
		return true;
	}

	public float GetPercentUseGauge(int skill_index)
	{
		SkillParam skillParam = GetSkillParam(skill_index);
		if (skillParam == null)
		{
			return 0f;
		}
		if ((float)(int)skillParam.useGauge <= 0f)
		{
			return 1f;
		}
		float value = skillParam.useGaugeCounter / (float)(int)skillParam.useGauge;
		return Mathf.Clamp01(value);
	}

	public float GetPercentUseGauge2nd(int skill_index)
	{
		if (GetPercentUseGauge(skill_index) < 1f)
		{
			return 0f;
		}
		SkillParam skillParam = GetSkillParam(skill_index);
		if ((float)(int)skillParam.useGauge2 <= 0f)
		{
			return 0f;
		}
		float value = (skillParam.useGaugeCounter - (float)(int)skillParam.useGauge) / (float)(int)skillParam.useGauge2;
		return Mathf.Clamp01(value);
	}

	public void OnUpdate()
	{
		if (!player.isDead && !player.isProgressStop())
		{
			float num = MonoBehaviourSingleton<InGameSettingsManager>.I.player.healSkillGaugePerSecond * Time.deltaTime;
			num *= player.buffParam.GetSkillHealSpeedUp();
			AddUseGauge(num, true, false);
		}
	}

	public void OnActSkillAction()
	{
		SkillParam actSkillParam = this.actSkillParam;
		if (actSkillParam != null)
		{
			actSkillParam.isUsingSecondGrade = false;
			if ((int)actSkillParam.useGauge2 <= 0)
			{
				actSkillParam.useGaugeCounter = 0f;
			}
			else if (actSkillParam.useGaugeCounter >= (float)(int)actSkillParam.GetMaxGaugeValue())
			{
				actSkillParam.useGaugeCounter = 0f;
				actSkillParam.isUsingSecondGrade = true;
			}
			else
			{
				float num = actSkillParam.useGaugeCounter - (float)(int)actSkillParam.useGauge;
				float num2 = num / (float)(int)actSkillParam.useGauge2;
				actSkillParam.useGaugeCounter = num2 * (float)(int)actSkillParam.useGauge;
			}
		}
	}

	public void OnHitAttackEnemy(AttackHitInfo attack_info)
	{
		if (player.weaponInfo != null)
		{
			float num = (!attack_info.toEnemy.isSpecialAttack) ? player.weaponInfo.healSkillGaugeHitRate : player.weaponInfo.healSkillGaugeHitRateSpecialAttack;
			float add_gauge = player.playerParameter.healSkillGaugeHit * num * attack_info.atkRate;
			AddUseGauge(add_gauge, true, true);
		}
	}

	public void OnHitCounterEnemy(AttackHitInfo attack_info)
	{
		if (player.weaponInfo != null)
		{
			float add_gauge = player.playerParameter.healSkillGaugeHit * player.playerParameter.ohsActionInfo.Common_CounterHealSkillRate;
			AddUseGauge(add_gauge, true, true);
		}
	}

	public void DebugAddUseGauge1st()
	{
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			if (skillParams[i].isValid && !((float)(int)skillParams[i].useGauge2 <= 0f))
			{
				AddUseGaugeByIndex(i, -3.40282347E+38f, false, false, false, true);
				AddUseGaugeByIndex(i, (float)(int)skillParams[i].useGauge, false, false, false, true);
			}
		}
	}

	public void AddUseGauge(float add_gauge, bool set_only = false, bool isNotBuff = false)
	{
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			AddUseGaugeByIndex(i, add_gauge, set_only, false, isNotBuff, true);
		}
	}

	public void AddUseGaugeByIndex(int index, float add_gauge, bool set_only = false, bool isForceSet = false, bool isNotBuff = false, bool isCorrectWaveMatch = true)
	{
		if ((!set_only || (index >= weaponOffset && index < weaponOffset + 3)) && skillParams[index].isValid && !IsArenaForbidSlotType(skillParams[index].tableData.type))
		{
			if (!isForceSet && !skillParams[index].tableData.lockBuffTypes.IsNullOrEmpty())
			{
				int[] lockBuffTypes = skillParams[index].tableData.lockBuffTypes;
				for (int i = 0; i < lockBuffTypes.Length; i++)
				{
					if (player.IsValidBuff((BuffParam.BUFFTYPE)lockBuffTypes[i]))
					{
						return;
					}
				}
			}
			if (!player.IsValidShield() || !player.buffParam.IsValidShieldBuff(skillParams[index].skillIndex) || isForceSet)
			{
				if (player.IsValidShield())
				{
					int shieldFromSkillIndex = player.buffParam.GetShieldFromSkillIndex();
					if (shieldFromSkillIndex < 0 || (IsSkillApplyShield(skillParams[index].tableData) && IsExistShieldInLockBuffTypes(skillParams[shieldFromSkillIndex].tableData.lockBuffTypes) && !isForceSet))
					{
						return;
					}
				}
				float useGaugeCounter = skillParams[index].useGaugeCounter;
				float num = add_gauge;
				if (isNotBuff)
				{
					if (skillParams[index].tableData.GetAttackElementNum() <= 1)
					{
						num *= player.buffParam.GetSkillAbsorbUp((skillParams[index].tableData.type != SKILL_SLOT_TYPE.ATTACK) ? ELEMENT_TYPE.MAX : skillParams[index].tableData.skillAtkType);
					}
					else if (skillParams[index].tableData.type == SKILL_SLOT_TYPE.ATTACK)
					{
						num *= player.buffParam.GetSkillAbsorbUpByElementList(skillParams[index].tableData.skillAtkTypes);
					}
					if (isCorrectWaveMatch && QuestManager.IsValidInGameWaveMatch(false) && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						InGameSettingsManager.WaveMatchParam waveMatchParam = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
						switch (waveMatchParam.skillGaugeType)
						{
						case InGameSettingsManager.WaveMatchParam.eGaugeType.Zero:
							return;
						case InGameSettingsManager.WaveMatchParam.eGaugeType.Rate:
							num *= waveMatchParam.skillGaugeValue;
							break;
						case InGameSettingsManager.WaveMatchParam.eGaugeType.Constant:
							num = waveMatchParam.skillGaugeValue;
							break;
						}
					}
				}
				if (num == 3.40282347E+38f)
				{
					useGaugeCounter = (float)(int)skillParams[index].GetMaxGaugeValue();
				}
				else if (num == -3.40282347E+38f)
				{
					useGaugeCounter = 0f;
				}
				else
				{
					useGaugeCounter += num;
					if (useGaugeCounter < 0f)
					{
						useGaugeCounter = 0f;
					}
					if (useGaugeCounter > (float)(int)skillParams[index].GetMaxGaugeValue())
					{
						useGaugeCounter = (float)(int)skillParams[index].GetMaxGaugeValue();
					}
				}
				skillParams[index].useGaugeCounter = useGaugeCounter;
			}
		}
	}

	public void SetUseGaugeFull()
	{
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			if (skillParams[i].isValid)
			{
				skillParams[i].useGaugeCounter = (float)(int)skillParams[i].GetMaxGaugeValue();
			}
		}
	}

	public void ResetUseGauge()
	{
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			if (skillParams[i].isValid)
			{
				skillParams[i].useGaugeCounter = 0f;
			}
		}
	}

	public void ResetSecondGradeFlags()
	{
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			if (skillParams[i].isValid)
			{
				skillParams[i].isUsingSecondGrade = false;
			}
		}
	}

	public void SetSettingsInfo(SkillSettingsInfo skill_settings, List<CharaInfo.EquipItem> weapon_list)
	{
		List<SkillParam> list = new List<SkillParam>();
		if (skillParams != null)
		{
			list = skillParams;
		}
		skillParams = new List<SkillParam>();
		InGameManager i = MonoBehaviourSingleton<InGameManager>.I;
		InGameSettingsManager i2 = MonoBehaviourSingleton<InGameSettingsManager>.I;
		int j = 0;
		for (int num = 9; j < num; j++)
		{
			SkillParam skillParam = new SkillParam();
			skillParam.skillIndex = j;
			skillParams.Add(skillParam);
			if (skill_settings != null && Singleton<SkillItemTable>.IsValid())
			{
				SkillSettingsInfo.Element element = skill_settings.elementList[j];
				if (element != null)
				{
					skillParam.baseInfo = element.baseInfo;
					skillParam.useGaugeCounter = element.useGaugeCounter;
					if (skillParam.baseInfo != null && skillParam.baseInfo.id != 0)
					{
						skillParam.tableData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skillParam.baseInfo.id);
						if (skillParam.tableData != null && skillParam.IsActiveType())
						{
							if (weapon_list[j / 3] != null)
							{
								EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weapon_list[j / 3].eId);
								if (equipItemData != null && !skillParam.tableData.IsEnableEquipType(equipItemData.type))
								{
									continue;
								}
							}
							switch (skillParam.tableData.skillAtkType)
							{
							case ELEMENT_TYPE.FIRE:
								skillParam.atk.fire = (float)(int)skillParam.tableData.skillAtk;
								break;
							case ELEMENT_TYPE.WATER:
								skillParam.atk.water = (float)(int)skillParam.tableData.skillAtk;
								break;
							case ELEMENT_TYPE.THUNDER:
								skillParam.atk.thunder = (float)(int)skillParam.tableData.skillAtk;
								break;
							case ELEMENT_TYPE.SOIL:
								skillParam.atk.soil = (float)(int)skillParam.tableData.skillAtk;
								break;
							case ELEMENT_TYPE.LIGHT:
								skillParam.atk.light = (float)(int)skillParam.tableData.skillAtk;
								break;
							case ELEMENT_TYPE.DARK:
								skillParam.atk.dark = (float)(int)skillParam.tableData.skillAtk;
								break;
							default:
								skillParam.atk.normal = (float)(int)skillParam.tableData.skillAtk;
								break;
							}
							int attackElementNum = skillParam.tableData.GetAttackElementNum();
							if (attackElementNum > 0)
							{
								skillParam.atkRates = new float[attackElementNum];
								for (int k = 0; k < attackElementNum; k++)
								{
									skillParam.atkRates[k] = (float)(int)skillParam.tableData.skillAtkRates[k] * 0.01f;
								}
							}
							skillParam.healHp = skillParam.tableData.healHp;
							for (int l = 0; l < 3; l++)
							{
								skillParam.supportValue[l] = skillParam.tableData.supportValue[l];
								skillParam.supportTime[l] = skillParam.tableData.supportTime[l];
							}
							skillParam.castTimeRate = 0f;
							skillParam.useGauge = skillParam.tableData.useGauge;
							skillParam.useGauge2 = skillParam.tableData.useGauge2;
							if (Singleton<GrowSkillItemTable>.IsValid())
							{
								GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillParam.tableData.growID, skillParam.baseInfo.level);
								if (growSkillItemData != null)
								{
									float num2 = (float)growSkillItemData.GetGrowParamSkillAtk(skillParam.tableData.skillAtk);
									switch (skillParam.tableData.skillAtkType)
									{
									case ELEMENT_TYPE.FIRE:
										skillParam.atk.fire = num2;
										break;
									case ELEMENT_TYPE.WATER:
										skillParam.atk.water = num2;
										break;
									case ELEMENT_TYPE.THUNDER:
										skillParam.atk.thunder = num2;
										break;
									case ELEMENT_TYPE.SOIL:
										skillParam.atk.soil = num2;
										break;
									case ELEMENT_TYPE.LIGHT:
										skillParam.atk.light = num2;
										break;
									case ELEMENT_TYPE.DARK:
										skillParam.atk.dark = num2;
										break;
									default:
										skillParam.atk.normal = num2;
										break;
									}
									if (attackElementNum > 0)
									{
										for (int m = 0; m < attackElementNum; m++)
										{
											skillParam.atkRates[m] = (float)growSkillItemData.GetGrowParamSkillAtkRate(skillParam.tableData.skillAtkRates[m]) * 0.01f;
										}
									}
									skillParam.healHp = growSkillItemData.GetGrowParamHealHp(skillParam.tableData.healHp);
									for (int n = 0; n < 3; n++)
									{
										skillParam.supportValue[n] = growSkillItemData.GetGrowParamSupprtValue(skillParam.tableData.supportValue, n);
										skillParam.supportTime[n] = growSkillItemData.GetGrowParamSupprtTime(skillParam.tableData.supportTime, n);
									}
									skillParam.castTimeRate = growSkillItemData.GetGrowParamCastTimeRate();
									skillParam.useGauge = growSkillItemData.GetGrowParamUseGauge(skillParam.tableData.useGauge);
									skillParam.useGauge2 = growSkillItemData.GetGrowParamUseGauge2(skillParam.tableData.useGauge2);
								}
							}
							if (Singleton<ExceedSkillItemTable>.IsValid())
							{
								ExceedSkillItemTable.ExceedSkillItemData exceedSkillItemData = Singleton<ExceedSkillItemTable>.I.GetExceedSkillItemData(skillParam.baseInfo.exceedCnt);
								if (exceedSkillItemData != null)
								{
									skillParam.useGauge = exceedSkillItemData.GetExceedUseGauge(skillParam.useGauge);
									skillParam.useGauge2 = exceedSkillItemData.GetExceedUseGauge2(skillParam.useGauge2);
								}
							}
							if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
							{
								InGameSettingsManager.ArenaParam arenaParam = i2.arenaParam;
								if ((float)skillParam.baseInfo.exceedCnt < arenaParam.magiSpeedDownRegistSkillExceedLv && i.ContainsArenaCondition(ARENA_CONDITION.RECOVER_MAGI_SPEED_DOWN))
								{
									float num3 = arenaParam.magiSpeedDownRateBase - arenaParam.magiSpeedDownRate * (float)skillParam.baseInfo.exceedCnt;
									if (num3 < 0f)
									{
										num3 = 0f;
									}
									SkillParam skillParam2 = skillParam;
									skillParam2.useGauge = (int)skillParam2.useGauge + Mathf.FloorToInt((float)(int)skillParam.useGauge * num3);
									SkillParam skillParam3 = skillParam;
									skillParam3.useGauge2 = (int)skillParam3.useGauge2 + Mathf.FloorToInt((float)(int)skillParam.useGauge2 * num3);
								}
								if (i.ContainsArenaCondition(ARENA_CONDITION.RECOVER_MAGI_SPEED_UP))
								{
									SkillParam skillParam4 = skillParam;
									skillParam4.useGauge = (int)skillParam4.useGauge - Mathf.FloorToInt((float)(int)skillParam.useGauge * arenaParam.magiSpeedUpBaseRate);
									SkillParam skillParam5 = skillParam;
									skillParam5.useGauge2 = (int)skillParam5.useGauge2 - Mathf.FloorToInt((float)(int)skillParam.useGauge2 * arenaParam.magiSpeedUpBaseRate);
								}
							}
							if (list != null && list.Count > 0)
							{
								for (int num4 = 0; num4 < list.Count; num4++)
								{
									if (list[num4] != null && list[num4].tableData != null && list[num4].tableData.name == skillParam.tableData.name && (UnityEngine.Object)list[num4].bullet != (UnityEngine.Object)null)
									{
										skillParam.bullet = list[j].bullet;
									}
								}
							}
							skillParam.isValid = true;
						}
					}
				}
			}
		}
		arenaConditionList = i.GetArenaConditions();
		if (!arenaConditionList.IsNullOrEmpty())
		{
			for (int num5 = 0; num5 < arenaConditionList.Length; num5++)
			{
				switch (arenaConditionList[num5])
				{
				case ARENA_CONDITION.FORBID_MAGI_ATTACK:
					isArenaForbidMagiAttack = true;
					break;
				case ARENA_CONDITION.FORBID_MAGI_SUPPORT:
					isArenaForbidMagiSupport = true;
					break;
				case ARENA_CONDITION.FORBID_MAGI_HEAL:
					isArenaForbidMagiHeal = true;
					break;
				}
			}
		}
	}

	private bool IsArenaForbidSlotType(SKILL_SLOT_TYPE slotType)
	{
		switch (slotType)
		{
		case SKILL_SLOT_TYPE.ATTACK:
			return isArenaForbidMagiAttack;
		case SKILL_SLOT_TYPE.SUPPORT:
			return isArenaForbidMagiSupport;
		case SKILL_SLOT_TYPE.HEAL:
			return isArenaForbidMagiHeal;
		default:
			return false;
		}
	}

	private bool IsSkillApplyShield(SkillItemTable.SkillItemData tableData)
	{
		if (tableData == null)
		{
			return false;
		}
		if (!tableData.supportType.IsNullOrEmpty() && Array.IndexOf(tableData.supportType, BuffParam.BUFFTYPE.SHIELD) >= 0)
		{
			return true;
		}
		if (!tableData.buffTableIds.IsNullOrEmpty())
		{
			if (!Singleton<BuffTable>.IsValid())
			{
				return false;
			}
			BuffTable.BuffData buffData = null;
			for (int i = 0; i < tableData.buffTableIds.Length; i++)
			{
				buffData = Singleton<BuffTable>.I.GetData((uint)tableData.buffTableIds[i]);
				if (buffData.type == BuffParam.BUFFTYPE.SHIELD)
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool IsExistShieldInLockBuffTypes(int[] lockBuffTypes)
	{
		if (lockBuffTypes.IsNullOrEmpty())
		{
			return false;
		}
		if (Array.IndexOf(lockBuffTypes, 60) >= 0)
		{
			return true;
		}
		return false;
	}
}
