using Network;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

public class EquipItemExceedParamTable : Singleton<EquipItemExceedParamTable>, IDataTable
{
	public class EquipItemExceedParamBase : EquipItemTable.EquipItemDataUtil
	{
		public XorInt atk = 0;

		public XorInt def = 0;

		public XorInt hp = 0;

		public int[] atkElement;

		public int[] defElement;

		public int GetElemAtk(int[] base_elem)
		{
			int elemAtkType = GetElemAtkType(base_elem);
			switch (elemAtkType)
			{
			case -1:
				return atkElement[0];
			case 6:
				return 0;
			default:
				return atkElement[elemAtkType] + base_elem[elemAtkType];
			}
		}

		public int GetElemDef(int[] base_elem)
		{
			int elemDefType = GetElemDefType(base_elem);
			switch (elemDefType)
			{
			case -1:
				return defElement[0];
			case 6:
				return 0;
			default:
				return defElement[elemDefType] + base_elem[elemDefType];
			}
		}

		public int GetElemAtkType(int[] base_elem)
		{
			if (base_elem == null)
			{
				return 6;
			}
			int[] array = new int[base_elem.Length];
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				array[i] = base_elem[i] + atkElement[i];
			}
			return GetElemType(array);
		}

		public int GetElemDefType(int[] base_elem)
		{
			if (base_elem == null)
			{
				return 6;
			}
			int[] array = new int[base_elem.Length];
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				array[i] = base_elem[i] + defElement[i];
			}
			return GetElemType(array);
		}
	}

	public class EquipItemExceedParam : EquipItemExceedParamBase
	{
		public uint exceedId;

		public uint cnt;

		public SkillItemTable.SkillSlotData skillSlot;

		public EquipItem.Ability ability;

		private string paramName;

		public const string NT = "exceedId,cnt,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType,abilityId,abilityPoint";

		public EquipItemExceedParam()
		{
			atk = 0;
			def = 0;
			hp = 0;
			atkElement = new int[6];
			defElement = new int[6];
			for (int i = 0; i < 6; i++)
			{
				atkElement[i] = 0;
				defElement[i] = 0;
			}
			skillSlot = new SkillItemTable.SkillSlotData();
			skillSlot.slotType = SKILL_SLOT_TYPE.NONE;
			skillSlot.skill_id = 0u;
			ability = new EquipItem.Ability();
			ability.id = 0;
			ability.pt = 0;
		}

		public string GetExceedParamName()
		{
			if (paramName != null)
			{
				return paramName;
			}
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			if ((int)atk > 0)
			{
				stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 1u, atk));
				stringBuilder.Append(" ");
			}
			if ((int)def > 0)
			{
				stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 2u, def));
				stringBuilder.Append(" ");
			}
			if ((int)hp > 0)
			{
				stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 3u, hp));
				stringBuilder.Append(" ");
			}
			int i = 0;
			for (int num = atkElement.Length; i < num; i++)
			{
				if (atkElement[i] > 0)
				{
					stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 4u, StringTable.Get(STRING_CATEGORY.ELEMENT, (uint)i), atkElement[i]));
					stringBuilder.Append(" ");
				}
			}
			int j = 0;
			for (int num2 = defElement.Length; j < num2; j++)
			{
				if (defElement[j] > 0)
				{
					stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 5u, StringTable.Get(STRING_CATEGORY.ELEMENT, (uint)j), defElement[j]));
					stringBuilder.Append(" ");
				}
			}
			if (skillSlot.slotType != 0)
			{
				stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 6u, StringTable.Get(STRING_CATEGORY.SKILL, (uint)skillSlot.slotType)));
				stringBuilder.Append(" ");
			}
			if (this.ability.id != 0)
			{
				AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility((uint)this.ability.id);
				stringBuilder.Append(StringTable.Format(STRING_CATEGORY.SMITH, 7u, ability.name, this.ability.pt));
			}
			paramName = stringBuilder.ToString();
			return paramName;
		}

		public static bool cb(CSVReader csv_reader, EquipItemExceedParam data, ref uint key1, ref uint key2)
		{
			data.exceedId = key1;
			data.cnt = key2;
			csv_reader.Pop(ref data.atk);
			csv_reader.Pop(ref data.def);
			csv_reader.Pop(ref data.hp);
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
			data.skillSlot = new SkillItemTable.SkillSlotData();
			csv_reader.Pop(ref data.skillSlot.slotType);
			csv_reader.Pop(ref data.ability.id);
			csv_reader.Pop(ref data.ability.pt);
			return true;
		}
	}

	public class EquipItemExceedParamAll : EquipItemExceedParamBase
	{
		public SkillItemTable.SkillSlotData[] skillSlot;

		public EquipItem.Ability[] ability;

		public EquipItemExceedParamAll()
		{
			atk = 0;
			def = 0;
			hp = 0;
			atkElement = new int[6];
			defElement = new int[6];
			for (int i = 0; i < 6; i++)
			{
				atkElement[i] = 0;
				defElement[i] = 0;
			}
			skillSlot = new SkillItemTable.SkillSlotData[0];
			ability = new EquipItem.Ability[0];
		}
	}

	private DoubleUIntKeyTable<EquipItemExceedParam> tableData;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedParam> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedParam> _003C_003Ef__mg_0024cache1;

	public void CreateTable(string csv_text)
	{
		tableData = TableUtility.CreateDoubleUIntKeyTable<EquipItemExceedParam>(csv_text, EquipItemExceedParam.cb, "exceedId,cnt,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType,abilityId,abilityPoint", null);
		tableData.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(tableData, csv_text, EquipItemExceedParam.cb, "exceedId,cnt,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType,abilityId,abilityPoint", null);
	}

	public EquipItemExceedParam GetEquipItemExceedParam(uint exceedId, uint exceedCnt)
	{
		if (tableData == null)
		{
			return null;
		}
		UIntKeyTable<EquipItemExceedParam> uIntKeyTable = tableData.Get(exceedId);
		if (uIntKeyTable == null)
		{
			return null;
		}
		EquipItemExceedParam equipItemExceedParam = uIntKeyTable.Get(exceedCnt);
		if (equipItemExceedParam == null)
		{
			Log.Warning("EquipItemExceedParamTable is NULL :: exceedID = {0}, exceedCount = {1}", exceedId, exceedCnt);
		}
		return equipItemExceedParam;
	}

	public EquipItemExceedParamAll GetEquipItemExceedParamAll(uint exceedId, uint exceedCnt)
	{
		if (tableData == null)
		{
			return null;
		}
		UIntKeyTable<EquipItemExceedParam> uIntKeyTable = tableData.Get(exceedId);
		if (uIntKeyTable == null)
		{
			return null;
		}
		EquipItemExceedParamAll result = new EquipItemExceedParamAll();
		List<SkillItemTable.SkillSlotData> slotList = new List<SkillItemTable.SkillSlotData>();
		List<EquipItem.Ability> abilityList = new List<EquipItem.Ability>();
		uIntKeyTable.ForEach(delegate(EquipItemExceedParam param)
		{
			if (param.cnt <= (int)exceedCnt)
			{
				EquipItemExceedParamAll equipItemExceedParamAll = result;
				equipItemExceedParamAll.atk = (int)equipItemExceedParamAll.atk + (int)param.atk;
				EquipItemExceedParamAll equipItemExceedParamAll2 = result;
				equipItemExceedParamAll2.def = (int)equipItemExceedParamAll2.def + (int)param.def;
				EquipItemExceedParamAll equipItemExceedParamAll3 = result;
				equipItemExceedParamAll3.hp = (int)equipItemExceedParamAll3.hp + (int)param.hp;
				for (int i = 0; i < 6; i++)
				{
					result.atkElement[i] += param.atkElement[i];
					result.defElement[i] += param.defElement[i];
				}
				if (param.skillSlot.slotType != 0)
				{
					slotList.Add(param.skillSlot);
				}
				if (param.ability.id > 0)
				{
					abilityList.Add(param.ability);
				}
			}
		});
		result.skillSlot = slotList.ToArray();
		result.ability = abilityList.ToArray();
		return result;
	}
}
