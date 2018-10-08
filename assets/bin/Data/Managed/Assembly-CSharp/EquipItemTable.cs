using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemTable : Singleton<EquipItemTable>, IDataTable
{
	public class EquipItemDataUtil
	{
		public int GetElemType(int[] elem)
		{
			if (elem == null || elem.Length == 0)
			{
				return 6;
			}
			bool flag = true;
			int num = -1;
			int num2 = 0;
			int i = 0;
			for (int num3 = elem.Length; i < num3; i++)
			{
				if (num2 <= elem[i] && elem[i] > 0)
				{
					num2 = elem[i];
					num = i;
				}
				if (elem[i] == 0 && flag)
				{
					flag = false;
				}
			}
			if (num == -1)
			{
				return 6;
			}
			if (flag)
			{
				return -1;
			}
			return num;
		}
	}

	public class EquipItemData : EquipItemDataUtil
	{
		public class Obtained
		{
			private const int CATEGORY_MAX = 64;

			private const int ALPHABET_MAX = 26;

			public string category = string.Empty;

			public int flag = -1;

			public Obtained(string obtained)
			{
				if (obtained.Length != 0)
				{
					string text = null;
					int i = 0;
					for (int length = obtained.Length; i < length; i++)
					{
						if (char.IsNumber(obtained[i]))
						{
							category = obtained.Substring(0, i).ToUpper();
							text = obtained.Substring(i);
							break;
						}
					}
					if (text != null)
					{
						int.TryParse(text, out flag);
					}
				}
			}

			public int GetSequenceNumber()
			{
				int num = flag;
				if (!string.IsNullOrEmpty(category))
				{
					string text = category.ToUpper();
					int i = 0;
					for (int length = text.Length; i < length; i++)
					{
						char c = text[i];
						int num2 = c - 65;
						int num3 = length - i;
						if (num3 > 0)
						{
							int num4 = 64;
							int j = 0;
							for (int num5 = num3 - 1; j < num5; j++)
							{
								num4 *= 26;
							}
							if (num4 > 64)
							{
								num2++;
							}
							num += num2 * num4;
						}
						else
						{
							num += num2;
						}
					}
				}
				return num;
			}
		}

		public const string NT = "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId";

		public uint id;

		public string appVer;

		public EQUIPMENT_TYPE type;

		public GET_TYPE getType;

		public int eventId;

		public string name;

		public RARITY_TYPE rarity;

		public int modelID0;

		public int modelID1;

		public int modelColor0;

		public int modelColor1;

		public int modelColor2;

		public int effectColor;

		public float effectParam;

		public byte effectID;

		public int __iconID;

		public int maxLv;

		public uint growID;

		public uint needId;

		public uint needUniqueId;

		public uint exceedID;

		public XorInt baseAtk;

		public XorInt baseDef;

		public XorInt baseHp;

		public int[] atkElement;

		public int[] defElement;

		public int maxSlot;

		public int fixedSkillLength;

		public int sale;

		private SkillItemTable.SkillSlotData[] _skillSlot;

		public EquipItem.Ability[] fixedAbility;

		public int listId;

		public Obtained obtained;

		public int damageDistanceId;

		public ELEMENT_TYPE atkElementType;

		public ELEMENT_TYPE defElementType;

		public bool isFormer;

		public SP_ATTACK_TYPE spAttackType;

		public int spAttackRate;

		public uint shadowEvolveEquipItemId;

		public uint evolveId;

		private bool? isEvolve;

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

		public string GetExceedParamName(int exceed_cnt)
		{
			if (exceed_cnt == 0)
			{
				return string.Empty;
			}
			EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParam(exceedID, (uint)exceed_cnt);
			if (equipItemExceedParam == null)
			{
				return string.Empty;
			}
			return equipItemExceedParam.GetExceedParamName();
		}

		public SkillItemTable.SkillSlotData[] GetSkillSlot(int exceed_cnt)
		{
			int num = _skillSlot.Length;
			SkillItemTable.SkillSlotData[] array = new SkillItemTable.SkillSlotData[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = _skillSlot[i];
			}
			EquipItemExceedParamTable.EquipItemExceedParamAll equipItemExceedParamAll = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParamAll(exceedID, (uint)exceed_cnt);
			if (equipItemExceedParamAll != null && equipItemExceedParamAll.skillSlot.Length > 0)
			{
				Array.Resize(ref array, num + equipItemExceedParamAll.skillSlot.Length);
				for (int j = 0; j < equipItemExceedParamAll.skillSlot.Length; j++)
				{
					array[num + j] = equipItemExceedParamAll.skillSlot[j];
				}
			}
			return array;
		}

		public int GetModelID(int sex)
		{
			if (sex == 0)
			{
				return modelID0;
			}
			return modelID1;
		}

		public int GetIconID()
		{
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				return GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
			}
			return GetIconID(0);
		}

		public int GetIconID(int sex)
		{
			uint _iconID = id;
			if (__iconID > 0)
			{
				_iconID = (uint)__iconID;
			}
			if (modelID0 == modelID1 || sex == 0)
			{
				return (int)(_iconID + 100000000);
			}
			return (int)(_iconID + 200000000);
		}

		public EquipModelTable.Data GetModelData(int sex)
		{
			return Singleton<EquipModelTable>.I.Get(type, GetModelID(sex));
		}

		public bool IsWeapon()
		{
			return type >= EQUIPMENT_TYPE.ONE_HAND_SWORD && type <= EQUIPMENT_TYPE.ARROW;
		}

		public bool IsVisual()
		{
			return type >= EQUIPMENT_TYPE.VISUAL_ARMOR && type <= EQUIPMENT_TYPE.VISUAL_LEG;
		}

		public bool IsEvolve()
		{
			bool? nullable = isEvolve;
			if (!nullable.HasValue)
			{
				isEvolve = (GetEvolveTable() != null);
			}
			return isEvolve == true;
		}

		public bool IsShadow()
		{
			return shadowEvolveEquipItemId != 0;
		}

		public bool IsEquipableAbilityItem()
		{
			if (!IsWeapon() && (getType == GET_TYPE.PAY || IsShadow()))
			{
				return true;
			}
			return false;
		}

		public EvolveEquipItemTable.EvolveEquipItemData[] GetEvolveTable()
		{
			return Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemData(id);
		}

		public EquipItemData GetBaseEquipTable()
		{
			EvolveEquipItemTable.EvolveEquipItemData evolveEquipItemDataFromEvolveEquipId = Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemDataFromEvolveEquipId(id);
			if (evolveEquipItemDataFromEvolveEquipId != null)
			{
				return Singleton<EquipItemTable>.I.GetEquipItemData(evolveEquipItemDataFromEvolveEquipId.equipBaseItemID);
			}
			return this;
		}

		public EquipItemData GetRootEquipTable()
		{
			EquipItemData equipItemData = GetBaseEquipTable();
			if (equipItemData != null)
			{
				while (true)
				{
					EquipItemData baseEquipTable = equipItemData.GetBaseEquipTable();
					if (baseEquipTable.id == equipItemData.id)
					{
						break;
					}
					equipItemData = baseEquipTable;
				}
				return equipItemData;
			}
			return null;
		}

		public EquipItemData GetShadowEvolveEquipTable()
		{
			if (!IsShadow())
			{
				return null;
			}
			return Singleton<EquipItemTable>.I.GetEquipItemData(shadowEvolveEquipItemId);
		}

		public EvolveEquipItemTable.EvolveEquipItemData GetEvolveTable(uint id)
		{
			EvolveEquipItemTable.EvolveEquipItemData result = null;
			EvolveEquipItemTable.EvolveEquipItemData[] evolveEquipItemData = Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemData(id);
			if (evolveEquipItemData != null)
			{
				int i = 0;
				for (int num = evolveEquipItemData.Length; i < num; i++)
				{
					if (evolveEquipItemData[i].id == id)
					{
						result = evolveEquipItemData[i];
						break;
					}
				}
			}
			return result;
		}

		public EquipItemStatus GetDefaultSkillBuffParam()
		{
			EquipItemStatus equipItemStatus = new EquipItemStatus();
			_GetDefaultSkillBuffParam(out int[] atk, out int[] def, out int hp);
			equipItemStatus.atk = atk[0];
			equipItemStatus.def = def[0];
			equipItemStatus.hp = hp;
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				equipItemStatus.elemAtk[i] = atk[i + 1];
				equipItemStatus.elemDef[i] = def[i + 1];
			}
			return equipItemStatus;
		}

		private void _GetDefaultSkillBuffParam(out int[] atk, out int[] def, out int hp)
		{
			atk = new int[7];
			def = new int[7];
			int i = 0;
			for (int num = 7; i < num; i++)
			{
				atk[i] = 0;
				def[i] = 0;
			}
			hp = 0;
			int j = 0;
			for (int num2 = maxSlot; j < num2; j++)
			{
				SkillItemTable.SkillSlotData skillSlotData = GetSkillSlot(0)[j];
				if (skillSlotData != null && skillSlotData.skill_id != 0)
				{
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skillSlotData.skill_id);
					if (skillItemData != null)
					{
						GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, 1);
						if (growSkillItemData != null)
						{
							atk[0] += growSkillItemData.GetGrowParamAtk(skillItemData.baseAtk);
							def[0] += growSkillItemData.GetGrowParamDef(skillItemData.baseDef);
							hp += growSkillItemData.GetGrowParamHp(skillItemData.baseHp);
							int[] growParamElemAtk = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
							int[] growParamElemDef = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
							int k = 1;
							for (int num3 = 7; k < num3; k++)
							{
								atk[k] += growParamElemAtk[k - 1];
								def[k] += growParamElemDef[k - 1];
							}
						}
					}
				}
			}
		}

		public int GetElemAtkType(int[] exceed_elem = null)
		{
			if (exceed_elem == null)
			{
				return GetElemType(atkElement);
			}
			int[] array = new int[atkElement.Length];
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				array[i] = atkElement[i] + exceed_elem[i];
			}
			return GetElemType(array);
		}

		public int GetElemAtkTypePriorityToTable(int[] exceed_elem = null)
		{
			if (atkElementType != ELEMENT_TYPE.MAX)
			{
				return (int)atkElementType;
			}
			return GetElemAtkType(exceed_elem);
		}

		public int GetElemDefType(int[] exceed_elem = null)
		{
			if (exceed_elem == null)
			{
				return GetElemType(defElement);
			}
			int[] array = new int[defElement.Length];
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				array[i] = defElement[i] + exceed_elem[i];
			}
			return GetElemType(array);
		}

		public int GetElemDefTypePriorityToTable(int[] exceed_elem = null)
		{
			if (defElementType != ELEMENT_TYPE.MAX)
			{
				return (int)defElementType;
			}
			return GetElemDefType(exceed_elem);
		}

		public ELEMENT_TYPE GetTargetElement(int exceed_cnt)
		{
			bool flag = IsWeapon();
			if (exceed_cnt > 0)
			{
				EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = GetExceedParam((uint)exceed_cnt);
				if (exceedParam != null)
				{
					return (ELEMENT_TYPE)((!flag) ? exceedParam.GetElemDefType(defElement) : exceedParam.GetElemAtkType(atkElement));
				}
			}
			return (ELEMENT_TYPE)((!flag) ? GetElemDefType(null) : GetElemAtkType(null));
		}

		public ELEMENT_TYPE GetTargetElementPriorityToTable()
		{
			return (ELEMENT_TYPE)((!IsWeapon()) ? GetElemDefTypePriorityToTable(null) : GetElemAtkTypePriorityToTable(null));
		}

		public void GetMaxAtk(out int _atk, out int _elem_atk, out ELEMENT_TYPE _element)
		{
			GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(growID, (uint)maxLv);
			_atk = growEquipItemData.GetGrowParamAtk(baseAtk);
			int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(atkElement);
			_elem_atk = 0;
			_element = ELEMENT_TYPE.MAX;
			int i = 0;
			for (int num = growParamElemAtk.Length; i < num; i++)
			{
				if (_elem_atk < growParamElemAtk[i])
				{
					_elem_atk = growParamElemAtk[i];
					_element = (ELEMENT_TYPE)i;
				}
			}
		}

		public void GetMaxDef(out int _def, out int _elem_def, out ELEMENT_TYPE _element)
		{
			GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(growID, (uint)maxLv);
			_def = growEquipItemData.GetGrowParamDef(baseDef);
			int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(defElement);
			_elem_def = 0;
			_element = ELEMENT_TYPE.MAX;
			int i = 0;
			for (int num = growParamElemDef.Length; i < num; i++)
			{
				if (_elem_def < growParamElemDef[i])
				{
					_elem_def = growParamElemDef[i];
					_element = (ELEMENT_TYPE)i;
				}
			}
		}

		public bool IsEnableNowApplicationVersion()
		{
			return AppMain.CheckApplicationVersion(appVer);
		}

		public bool CanCollecting()
		{
			return obtained.category.Length > 0 && obtained.flag >= 0 && obtained.flag < 64 && !IsShadow();
		}

		public EquipItemExceedParamTable.EquipItemExceedParamAll GetExceedParam(uint exceed)
		{
			return Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParamAll(exceedID, exceed);
		}

		public static bool cb(CSVReader csv_reader, EquipItemData data, ref uint key)
		{
			//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_013c: Unknown result type (might be due to invalid IL or missing references)
			//IL_015b: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
			data.id = key;
			csv_reader.Pop(ref data.appVer);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.getType);
			csv_reader.Pop(ref data.eventId);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.rarity);
			csv_reader.Pop(ref data.modelID0);
			data.modelID1 = data.modelID0;
			csv_reader.Pop(ref data.modelID1);
			string value = string.Empty;
			int num = -1;
			csv_reader.Pop(ref value);
			if (value.Length > 1)
			{
				num = (int)Enum.Parse(typeof(ELEMENT_TYPE), value);
			}
			if (!(bool)csv_reader.PopColor24(ref data.modelColor0))
			{
				if (num != -1)
				{
					data.modelColor0 = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetModelElementColor(num));
				}
				else
				{
					data.modelColor0 = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.modelBaseColor);
				}
			}
			csv_reader.PopColor24(ref data.modelColor1);
			if (!(bool)csv_reader.PopColor24(ref data.modelColor2))
			{
				if (num != -1)
				{
					data.modelColor2 = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetModelElementColor2(num));
				}
				else
				{
					data.modelColor2 = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.modelBaseColor2);
				}
			}
			csv_reader.Pop(ref data.effectID);
			data.effectParam = 1f;
			csv_reader.Pop(ref data.effectParam);
			if (!(bool)csv_reader.PopColor24(ref data.effectColor))
			{
				if (num != -1)
				{
					data.effectColor = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetModelElementColor(num));
				}
				else
				{
					data.effectColor = NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.modelBaseColor);
				}
			}
			csv_reader.Pop(ref data.__iconID);
			csv_reader.Pop(ref data.maxLv);
			csv_reader.Pop(ref data.growID);
			csv_reader.Pop(ref data.needId);
			csv_reader.Pop(ref data.needUniqueId);
			csv_reader.Pop(ref data.exceedID);
			csv_reader.Pop(ref data.shadowEvolveEquipItemId);
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
			List<SkillItemTable.SkillSlotData> list = new List<SkillItemTable.SkillSlotData>();
			int num2 = 0;
			int num3 = 0;
			for (int k = 0; k < 9; k++)
			{
				string value2 = string.Empty;
				uint value3 = 0u;
				csv_reader.Pop(ref value2);
				csv_reader.Pop(ref value3);
				if (!string.IsNullOrEmpty(value2))
				{
					SkillItemTable.SkillSlotData skillSlotData = new SkillItemTable.SkillSlotData();
					skillSlotData.slotType = (SKILL_SLOT_TYPE)(int)Enum.Parse(typeof(SKILL_SLOT_TYPE), value2);
					if (skillSlotData.slotType != 0)
					{
						skillSlotData.skill_id = value3;
						list.Add(skillSlotData);
						num3++;
						if (value3 != 0)
						{
							num2++;
						}
					}
				}
			}
			data._skillSlot = list.ToArray();
			data.fixedSkillLength = num2;
			data.maxSlot = num3;
			int[] array = new int[3];
			int[] array2 = new int[3];
			int[] array3 = new int[3];
			int num4 = 0;
			for (int l = 0; l < 3; l++)
			{
				csv_reader.Pop(ref array[l]);
				csv_reader.Pop(ref array2[l]);
				csv_reader.Pop(ref array3[l]);
				if (array[l] != 0 && array2[l] != 0)
				{
					num4++;
				}
			}
			data.fixedAbility = new EquipItem.Ability[num4];
			for (int m = 0; m < num4; m++)
			{
				data.fixedAbility[m] = new EquipItem.Ability();
				data.fixedAbility[m].id = array[m];
				data.fixedAbility[m].pt = array2[m];
				data.fixedAbility[m].vr = (0 < array3[m]);
			}
			csv_reader.Pop(ref data.sale);
			csv_reader.Pop(ref data.listId);
			string value4 = string.Empty;
			csv_reader.Pop(ref value4);
			data.obtained = new Obtained(value4);
			if (!(bool)csv_reader.Pop(ref data.damageDistanceId))
			{
				if (data.type == EQUIPMENT_TYPE.ARROW)
				{
					data.damageDistanceId = 0;
				}
				else
				{
					data.damageDistanceId = -1;
				}
			}
			csv_reader.PopEnum(ref data.atkElementType, ELEMENT_TYPE.MAX);
			csv_reader.PopEnum(ref data.defElementType, ELEMENT_TYPE.MAX);
			csv_reader.Pop(ref data.isFormer);
			csv_reader.PopEnum(ref data.spAttackType, SP_ATTACK_TYPE.NONE);
			csv_reader.Pop(ref data.spAttackRate);
			csv_reader.Pop(ref data.evolveId);
			return true;
		}
	}

	public class ListCompare : IComparer<EquipItemData>
	{
		public int Compare(EquipItemData data1, EquipItemData data2)
		{
			if (data1.listId < data2.listId)
			{
				return -1;
			}
			return 1;
		}
	}

	private UIntKeyTable<EquipItemData> equipItemTable;

	private List<EquipItemData> equipList;

	public void CreateTable(string csv_table)
	{
		equipItemTable = TableUtility.CreateUIntKeyTable<EquipItemData>(csv_table, EquipItemData.cb, "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId", null);
	}

	public void CreateTable(string csv_table, TableUtility.Progress progress)
	{
		equipItemTable = TableUtility.CreateUIntKeyTable<EquipItemData>(csv_table, EquipItemData.cb, "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId", progress);
		equipItemTable.TrimExcess();
	}

	public void AddTable(string csv_table)
	{
		TableUtility.AddUIntKeyTable(equipItemTable, csv_table, EquipItemData.cb, "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId", null);
	}

	public bool IsWeapon(EQUIPMENT_TYPE type)
	{
		return type >= EQUIPMENT_TYPE.ONE_HAND_SWORD && type <= EQUIPMENT_TYPE.ARROW;
	}

	public bool IsVisual(EQUIPMENT_TYPE type)
	{
		return type >= EQUIPMENT_TYPE.VISUAL_ARMOR && type <= EQUIPMENT_TYPE.VISUAL_LEG;
	}

	public void CreateTableForEquipList()
	{
		equipList = new List<EquipItemData>(equipItemTable.GetCount());
		ForEach(delegate(EquipItemData data)
		{
			if (data.CanCollecting())
			{
				equipList.Add(data);
			}
		});
		ListCompare comparer = new ListCompare();
		equipList.Sort(comparer);
	}

	public void ForEach(Action<EquipItemData> cb)
	{
		equipItemTable.ForEach(cb);
	}

	public EquipItemData GetEquipItemData(uint id)
	{
		if (equipItemTable == null)
		{
			return null;
		}
		EquipItemData equipItemData = equipItemTable.Get(id);
		if (equipItemData == null)
		{
			Log.TableError(this, id);
			equipItemData = new EquipItemData();
			equipItemData.name = Log.NON_DATA_NAME;
		}
		return equipItemData;
	}

	public int GetEquipListCount()
	{
		if (equipList == null)
		{
			return 0;
		}
		return equipList.Count;
	}

	public EquipItemData GetEquipListData(int index)
	{
		if (equipList == null || equipList.Count <= index)
		{
			return null;
		}
		return equipList[index];
	}

	public static int GetIdFromIconId(int iconId)
	{
		return iconId % 100000000;
	}
}
