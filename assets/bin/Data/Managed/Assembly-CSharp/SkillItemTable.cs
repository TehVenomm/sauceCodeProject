using System;
using UnityEngine;

public class SkillItemTable : Singleton<SkillItemTable>, IDataTable
{
	public class SkillItemData
	{
		private class SkillMaxLevel
		{
			public int exceedCnt;

			public int maxLevel;

			public SkillMaxLevel(int _cnt, int _max_lv)
			{
				exceedCnt = _cnt;
				maxLevel = _max_lv;
			}
		}

		public const string NT = "skillItemId,appVer,type,name,text,rarity,modelId,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,reactionType,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes";

		public uint id;

		public string appVer;

		public SKILL_SLOT_TYPE type;

		public string name;

		public string text;

		public RARITY_TYPE rarity;

		public int modelID;

		public Vector3 modelColor;

		public int iconID;

		private int maxLv;

		public uint growID;

		public int baseNeedExp;

		public int baseGiveExp;

		public XorInt baseAtk;

		public XorInt baseDef;

		public XorInt baseHp;

		public int[] atkElement;

		public int[] defElement;

		public int baseSell;

		public ENABLE_EQUIP_TYPE enableEquipType;

		public float castTime;

		public XorInt useGauge;

		public string castStateName;

		public string actStateName;

		public string startEffectName;

		public int startSEID;

		public string actLocalEffectName;

		public string actOneshotEffectName;

		public int actSEID;

		public string enchantEffectName;

		public AttackHitInfo.ToEnemy.REACTION_TYPE reactionType;

		public string bulletName;

		public string[] attackInfoNames;

		public bool selfOnly;

		public XorInt skillAtk;

		public ELEMENT_TYPE skillAtkType;

		public XorInt skillAtkRate;

		public string hitEffectName;

		public int hitSEID;

		public XorFloat skillRange;

		public XorInt healHp;

		public HEAL_TYPE healType;

		public ENABLE_EQUIP_TYPE[] supportPassiveEqType;

		public BuffParam.BUFFTYPE[] supportType;

		public int[] supportValue;

		public float[] supportTime;

		public string[] supportEffectName;

		public SP_ATTACK_TYPE supportPassiveSpAttackType;

		public int[] buffTableIds;

		public int[] lockBuffTypes;

		private UIntKeyTable<SkillMaxLevel> maxLvData = new UIntKeyTable<SkillMaxLevel>();

		public int baseElemAtk
		{
			get
			{
				if (atkElement == null)
				{
					return 0;
				}
				return Mathf.Max(atkElement);
			}
		}

		public int baseElemDef
		{
			get
			{
				if (defElement == null)
				{
					return 0;
				}
				return Mathf.Max(defElement);
			}
		}

		public int GetMaxLv(int exceed_cnt)
		{
			SkillMaxLevel skillMaxLevel = maxLvData.Get((uint)exceed_cnt);
			if (skillMaxLevel == null)
			{
				int exceedMaxLevel = GetExceedMaxLevel(exceed_cnt);
				if (exceedMaxLevel > maxLv)
				{
					exceedMaxLevel = maxLv;
				}
				maxLvData.Add((uint)exceed_cnt, new SkillMaxLevel(exceed_cnt, exceedMaxLevel));
				return exceedMaxLevel;
			}
			return skillMaxLevel.maxLevel;
		}

		public int GetExceededMaxLevel()
		{
			return maxLv;
		}

		private int GetExceedMaxLevel(int exceed_cnt)
		{
			int result = 1;
			GrowSkillItemTable.GrowSkillItemData[] growSkillItemDataAry = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemDataAry(growID);
			GrowSkillItemTable.GrowSkillItemData under = null;
			GrowSkillItemTable.GrowSkillItemData over = null;
			Array.ForEach(growSkillItemDataAry, delegate(GrowSkillItemTable.GrowSkillItemData data)
			{
				if (data.exceedCnt > exceed_cnt && (over == null || data.lv < over.lv))
				{
					over = data;
				}
				if (data.exceedCnt <= exceed_cnt && (under == null || data.lv > under.lv))
				{
					under = data;
				}
			});
			if (over == null && under != null)
			{
				return under.lv;
			}
			if (under == null)
			{
				return result;
			}
			float num = (float)(exceed_cnt + 1 - under.exceedCnt) / (float)(over.exceedCnt - under.exceedCnt);
			return Mathf.FloorToInt(Mathf.Lerp((float)under.lv, (float)over.lv, num)) - 1;
		}

		public bool IsPassive()
		{
			return type != SKILL_SLOT_TYPE.ATTACK && type != SKILL_SLOT_TYPE.SUPPORT && type != SKILL_SLOT_TYPE.HEAL;
		}

		public bool IsEnableEquipType(EQUIPMENT_TYPE type)
		{
			return IsEnableEquipType(MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type));
		}

		public bool IsEnableEquipType(int type_index)
		{
			if (type_index < 0)
			{
				Log.Warning(LOG.OUTGAME, "IsEnableEquipType : Check index is out of bounds : index = " + type_index);
				return false;
			}
			if (enableEquipType == ENABLE_EQUIP_TYPE.ALL)
			{
				return true;
			}
			int num = type_index + 1;
			if (num > 6)
			{
				num = 6;
			}
			return enableEquipType == (ENABLE_EQUIP_TYPE)num;
		}

		public EQUIPMENT_TYPE? GetEnableEquipType()
		{
			switch (enableEquipType)
			{
			default:
				return null;
			case ENABLE_EQUIP_TYPE.ONE_HAND_SWORD:
				return EQUIPMENT_TYPE.ONE_HAND_SWORD;
			case ENABLE_EQUIP_TYPE.TWO_HAND_SWORD:
				return EQUIPMENT_TYPE.TWO_HAND_SWORD;
			case ENABLE_EQUIP_TYPE.SPEAR:
				return EQUIPMENT_TYPE.SPEAR;
			case ENABLE_EQUIP_TYPE.PAIR_SWORDS:
				return EQUIPMENT_TYPE.PAIR_SWORDS;
			case ENABLE_EQUIP_TYPE.ARROW:
				return EQUIPMENT_TYPE.ARROW;
			case ENABLE_EQUIP_TYPE.ARMORS:
				return EQUIPMENT_TYPE.ARMOR;
			}
		}

		public bool IsEnableSupportEquipType(EQUIPMENT_TYPE type, int support_index)
		{
			return IsEnableSupportEquipType(MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type), support_index);
		}

		public bool IsEnableSupportEquipType(int type_index, int support_index)
		{
			if (type_index < 0)
			{
				Log.Warning(LOG.OUTGAME, "IsEnableEquipType : Check index is out of bounds : index = " + type_index);
				return false;
			}
			if (supportPassiveEqType[support_index] == ENABLE_EQUIP_TYPE.ALL)
			{
				return true;
			}
			int num = type_index + 1;
			if (num > 6)
			{
				num = 6;
			}
			return supportPassiveEqType[support_index] == (ENABLE_EQUIP_TYPE)num;
		}

		public bool IsEnableSupportEquipType(ENABLE_EQUIP_TYPE type, int support_index)
		{
			if (supportPassiveEqType[support_index] == ENABLE_EQUIP_TYPE.ALL)
			{
				return true;
			}
			return supportPassiveEqType[support_index] == type;
		}

		public bool IsMatchSupportEquipType(EQUIPMENT_TYPE type)
		{
			if (supportPassiveEqType == null)
			{
				return false;
			}
			if (supportPassiveEqType.Length <= 0)
			{
				return false;
			}
			ENABLE_EQUIP_TYPE enableEquipTypeFromEquipmentType = GetEnableEquipTypeFromEquipmentType(type);
			for (int i = 0; i < supportPassiveEqType.Length; i++)
			{
				if (enableEquipTypeFromEquipmentType == supportPassiveEqType[i])
				{
					return true;
				}
			}
			return false;
		}

		private ENABLE_EQUIP_TYPE GetEnableEquipTypeFromEquipmentType(EQUIPMENT_TYPE type)
		{
			switch (type)
			{
			case EQUIPMENT_TYPE.ONE_HAND_SWORD:
				return ENABLE_EQUIP_TYPE.ONE_HAND_SWORD;
			case EQUIPMENT_TYPE.TWO_HAND_SWORD:
				return ENABLE_EQUIP_TYPE.TWO_HAND_SWORD;
			case EQUIPMENT_TYPE.SPEAR:
				return ENABLE_EQUIP_TYPE.SPEAR;
			case EQUIPMENT_TYPE.PAIR_SWORDS:
				return ENABLE_EQUIP_TYPE.PAIR_SWORDS;
			case EQUIPMENT_TYPE.ARROW:
				return ENABLE_EQUIP_TYPE.ARROW;
			case EQUIPMENT_TYPE.ARMOR:
			case EQUIPMENT_TYPE.HELM:
			case EQUIPMENT_TYPE.ARM:
			case EQUIPMENT_TYPE.LEG:
				return ENABLE_EQUIP_TYPE.ARMORS;
			default:
				return ENABLE_EQUIP_TYPE.ALL;
			}
		}

		public string GetExplanationText(int level = 1)
		{
			return SkillItemInfo.GetExplanationText(this, level);
		}

		public bool IsEnableNowApplicationVersion()
		{
			return AppMain.CheckApplicationVersion(appVer);
		}

		public static bool cb(CSVReader csv_reader, SkillItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.appVer);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.text);
			csv_reader.Pop(ref data.rarity);
			csv_reader.Pop(ref data.modelID);
			csv_reader.PopColor(ref data.modelColor);
			csv_reader.Pop(ref data.iconID);
			csv_reader.Pop(ref data.maxLv);
			csv_reader.Pop(ref data.growID);
			csv_reader.Pop(ref data.baseNeedExp);
			csv_reader.Pop(ref data.baseGiveExp);
			csv_reader.Pop(ref data.baseAtk);
			csv_reader.Pop(ref data.baseDef);
			csv_reader.Pop(ref data.baseHp);
			data.atkElement = new int[6];
			data.defElement = new int[6];
			for (int i = 0; i < 6; i++)
			{
				csv_reader.Pop(ref data.atkElement[i]);
			}
			for (int j = 0; j < 6; j++)
			{
				csv_reader.Pop(ref data.defElement[j]);
			}
			csv_reader.Pop(ref data.baseSell);
			csv_reader.Pop(ref data.enableEquipType);
			csv_reader.Pop(ref data.castTime);
			csv_reader.Pop(ref data.useGauge);
			csv_reader.Pop(ref data.castStateName);
			csv_reader.Pop(ref data.actStateName);
			csv_reader.Pop(ref data.startEffectName);
			csv_reader.Pop(ref data.startSEID);
			csv_reader.Pop(ref data.actLocalEffectName);
			csv_reader.Pop(ref data.actOneshotEffectName);
			csv_reader.Pop(ref data.actSEID);
			csv_reader.Pop(ref data.enchantEffectName);
			csv_reader.Pop(ref data.reactionType);
			csv_reader.Pop(ref data.bulletName);
			data.attackInfoNames = new string[5];
			for (int k = 0; k < 5; k++)
			{
				csv_reader.Pop(ref data.attackInfoNames[k]);
			}
			csv_reader.Pop(ref data.selfOnly);
			csv_reader.Pop(ref data.skillAtk);
			csv_reader.PopEnum(ref data.skillAtkType, ELEMENT_TYPE.MAX);
			csv_reader.Pop(ref data.skillAtkRate);
			csv_reader.Pop(ref data.hitEffectName);
			csv_reader.Pop(ref data.hitSEID);
			csv_reader.Pop(ref data.skillRange);
			csv_reader.Pop(ref data.healHp);
			csv_reader.Pop(ref data.healType);
			data.supportPassiveEqType = new ENABLE_EQUIP_TYPE[3];
			data.supportType = new BuffParam.BUFFTYPE[3];
			data.supportValue = new int[3];
			data.supportTime = new float[3];
			data.supportEffectName = new string[3];
			for (int l = 0; l < 3; l++)
			{
				csv_reader.Pop(ref data.supportPassiveEqType[l]);
				csv_reader.PopEnum(ref data.supportType[l], BuffParam.BUFFTYPE.NONE);
				csv_reader.Pop(ref data.supportValue[l]);
				csv_reader.Pop(ref data.supportTime[l]);
				csv_reader.Pop(ref data.supportEffectName[l]);
			}
			csv_reader.PopEnum(ref data.supportPassiveSpAttackType, SP_ATTACK_TYPE.NONE);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				string[] array = value.Split(':');
				data.buffTableIds = new int[array.Length];
				for (int m = 0; m < array.Length; m++)
				{
					data.buffTableIds[m] = array[m].ToInt32OrDefault(0);
				}
			}
			string value2 = string.Empty;
			csv_reader.Pop(ref value2);
			if (!string.IsNullOrEmpty(value2))
			{
				string[] array2 = value2.Split(':');
				data.lockBuffTypes = new int[array2.Length];
				for (int n = 0; n < array2.Length; n++)
				{
					data.lockBuffTypes[n] = array2[n].ToInt32OrDefault(0);
				}
			}
			return true;
		}
	}

	public class SkillSlotData
	{
		public SKILL_SLOT_TYPE slotType;

		public uint skill_id;

		public SkillSlotData()
		{
		}

		public SkillSlotData(uint id, SKILL_SLOT_TYPE type)
		{
			skill_id = id;
			slotType = type;
		}
	}

	public const int SUPPORT_MAX = 3;

	public const float SKILL_ATK_DISP_RATE = 0.02f;

	public const int SKILL_SLOT_TABLE_DATA_MAX = 9;

	private UIntKeyTable<SkillItemData> skillTable;

	public void CreateTable(string csv_text)
	{
		skillTable = TableUtility.CreateUIntKeyTable<SkillItemData>(csv_text, SkillItemData.cb, "skillItemId,appVer,type,name,text,rarity,modelId,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,reactionType,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes", null);
		skillTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(skillTable, csv_text, SkillItemData.cb, "skillItemId,appVer,type,name,text,rarity,modelId,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,reactionType,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes", null);
	}

	public SkillItemData GetSkillItemData(uint skill_id)
	{
		if (skillTable == null)
		{
			return null;
		}
		SkillItemData skillItemData = skillTable.Get(skill_id);
		if (skillItemData == null)
		{
			Log.TableError(this, skill_id);
			skillItemData = new SkillItemData();
			skillItemData.name = Log.NON_DATA_NAME;
		}
		return skillItemData;
	}
}
