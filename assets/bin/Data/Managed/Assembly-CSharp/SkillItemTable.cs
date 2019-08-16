using System;
using System.Runtime.CompilerServices;
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

		public XorInt useGauge2;

		public string castStateName;

		public string actStateName;

		public string startEffectName;

		public int startSEID;

		public string actLocalEffectName;

		public string actOneshotEffectName;

		public int actSEID;

		public string enchantEffectName;

		public string bulletName;

		public string[] attackInfoNames;

		public bool selfOnly;

		public XorInt skillAtk;

		public ELEMENT_TYPE[] skillAtkTypes;

		public XorInt[] skillAtkRates;

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

		public bool isTeleportation;

		public string exceedExtraText;

		private UIntKeyTable<SkillMaxLevel> maxLvData = new UIntKeyTable<SkillMaxLevel>();

		public const string NT = "skillItemId,appVer,type,name,text,rarity,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,useGauge2,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,skillAtkType2,skillAtkRate2,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes,isTeleportation,exceedExtraText";

		public ELEMENT_TYPE skillAtkType
		{
			get
			{
				if (skillAtkTypes != null && skillAtkTypes.Length > 0)
				{
					return skillAtkTypes[0];
				}
				return ELEMENT_TYPE.MAX;
			}
		}

		public XorInt skillAtkRate
		{
			get
			{
				if (skillAtkRates != null && skillAtkRates.Length > 0)
				{
					return skillAtkRates[0];
				}
				return 0;
			}
		}

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
			if (over != null)
			{
				return over.lv;
			}
			if (under != null)
			{
				return under.lv;
			}
			return 1;
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

		public string GetExplanationText(int level = 1, int exceedCnt = 0)
		{
			return SkillItemInfo.GetExplanationText(this, level, exceedCnt);
		}

		public bool IsEnableNowApplicationVersion()
		{
			return AppMain.CheckApplicationVersion(appVer);
		}

		public ELEMENT_TYPE GetAttackElementByIndex(int index)
		{
			if (skillAtkTypes != null && skillAtkTypes.Length > index)
			{
				return skillAtkTypes[index];
			}
			return ELEMENT_TYPE.MAX;
		}

		public XorInt GetAttackElementRateByIndex(int index)
		{
			if (skillAtkRates != null && skillAtkRates.Length > index)
			{
				return skillAtkRates[index];
			}
			return 100;
		}

		public int GetAttackElementNum()
		{
			if (skillAtkTypes == null)
			{
				return 0;
			}
			return skillAtkTypes.Length;
		}

		public bool HasElement(ELEMENT_TYPE element)
		{
			if (skillAtkTypes != null && skillAtkTypes.Length > 0)
			{
				for (int i = 0; i < skillAtkTypes.Length; i++)
				{
					if (skillAtkTypes[i] == element)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool cb(CSVReader csv_reader, SkillItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.appVer);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.text);
			csv_reader.Pop(ref data.rarity);
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
			csv_reader.Pop(ref data.useGauge2);
			csv_reader.Pop(ref data.castStateName);
			csv_reader.Pop(ref data.actStateName);
			csv_reader.Pop(ref data.startEffectName);
			csv_reader.Pop(ref data.startSEID);
			csv_reader.Pop(ref data.actLocalEffectName);
			csv_reader.Pop(ref data.actOneshotEffectName);
			csv_reader.Pop(ref data.actSEID);
			csv_reader.Pop(ref data.enchantEffectName);
			csv_reader.Pop(ref data.bulletName);
			data.attackInfoNames = new string[5];
			for (int k = 0; k < 5; k++)
			{
				csv_reader.Pop(ref data.attackInfoNames[k]);
			}
			csv_reader.Pop(ref data.selfOnly);
			csv_reader.Pop(ref data.skillAtk);
			ELEMENT_TYPE value = ELEMENT_TYPE.MAX;
			ELEMENT_TYPE value2 = ELEMENT_TYPE.MAX;
			XorInt value3 = 0;
			XorInt value4 = 0;
			csv_reader.PopEnum(ref value, ELEMENT_TYPE.MAX);
			csv_reader.Pop(ref value3);
			csv_reader.PopEnum(ref value2, ELEMENT_TYPE.MAX);
			csv_reader.Pop(ref value4);
			if (value2 == ELEMENT_TYPE.MAX)
			{
				data.skillAtkTypes = new ELEMENT_TYPE[1];
				data.skillAtkRates = new XorInt[1];
				data.skillAtkTypes[0] = value;
				data.skillAtkRates[0] = value3;
			}
			else
			{
				data.skillAtkTypes = new ELEMENT_TYPE[2];
				data.skillAtkRates = new XorInt[2];
				data.skillAtkTypes[0] = value;
				data.skillAtkRates[0] = value3;
				data.skillAtkTypes[1] = value2;
				data.skillAtkRates[1] = value4;
			}
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
			string value5 = string.Empty;
			csv_reader.Pop(ref value5);
			if (!string.IsNullOrEmpty(value5))
			{
				string[] array = value5.Split(':');
				data.buffTableIds = new int[array.Length];
				for (int m = 0; m < array.Length; m++)
				{
					data.buffTableIds[m] = array[m].ToInt32OrDefault();
				}
			}
			string value6 = string.Empty;
			csv_reader.Pop(ref value6);
			if (!string.IsNullOrEmpty(value6))
			{
				string[] array2 = value6.Split(':');
				data.lockBuffTypes = new int[array2.Length];
				for (int n = 0; n < array2.Length; n++)
				{
					data.lockBuffTypes[n] = array2[n].ToInt32OrDefault();
				}
			}
			switch (data.type)
			{
			case SKILL_SLOT_TYPE.ATTACK:
				data.modelID = 1;
				break;
			case SKILL_SLOT_TYPE.HEAL:
				data.modelID = 2;
				break;
			case SKILL_SLOT_TYPE.SUPPORT:
				data.modelID = 3;
				break;
			case SKILL_SLOT_TYPE.PASSIVE:
				data.modelID = 4;
				break;
			case SKILL_SLOT_TYPE.GROW:
				data.modelID = 5;
				break;
			}
			csv_reader.Pop(ref data.isTeleportation);
			csv_reader.Pop(ref data.exceedExtraText);
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

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<SkillItemData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<SkillItemData> _003C_003Ef__mg_0024cache1;

	public void CreateTable(string csv_text)
	{
		skillTable = TableUtility.CreateUIntKeyTable<SkillItemData>(csv_text, SkillItemData.cb, "skillItemId,appVer,type,name,text,rarity,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,useGauge2,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,skillAtkType2,skillAtkRate2,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes,isTeleportation,exceedExtraText");
		skillTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(skillTable, csv_text, SkillItemData.cb, "skillItemId,appVer,type,name,text,rarity,R,G,B,iconId,maxLv,growId,needExp,giveExp,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,price,enableEquipType,castTime,useGauge,useGauge2,castStateName,actStateName,startEffectName,startSEID,actLocalEffectName,actOneshotEffectName,actSEID,enchantEffectName,bulletName,attackInfoNames0,attackInfoNames1,attackInfoNames2,attackInfoNames3,attackInfoNames4,selfOnly,skillAtk,skillAtkType,skillAtkRate,skillAtkType2,skillAtkRate2,hitEffectName,hitSEID,skillRange,healHp,healType,supportPassiveEqType1,supportType1,supportValue1,supportTime1,supportEffectName1,supportPassiveEqType2,supportType2,supportValue2,supportTime2,supportEffectName2,supportPassiveEqType3,supportType3,supportValue3,supportTime3,supportEffectName3,supportPassiveSpAttackType,buffTableIds,lockBuffTypes,isTeleportation,exceedExtraText");
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
