using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemInfo : ItemInfoBase<EquipItem>
{
	public EquipItemTable.EquipItemData tableData;

	private GrowEquipItemTable.GrowEquipItemData mGrowTableData;

	private GrowEquipItemTable.GrowEquipItemData mNextGrowTableData;

	private GrowEquipItemTable.GrowEquipItemNeedItemData mNextNeedTableData;

	private List<int> mAtkList;

	private List<int> mDefList;

	public int? mHp;

	private int? mElemAtk;

	private int? mElemDef;

	public EquipItemAbility[] ability;

	public AbilityItem abilityItem;

	private EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam;

	public int level
	{
		get;
		private set;
	}

	public int exceed
	{
		get;
		private set;
	}

	public bool isFavorite
	{
		get;
		private set;
	}

	public int sellPrice
	{
		get;
		private set;
	}

	public GrowEquipItemTable.GrowEquipItemData growTableData
	{
		get
		{
			if (mGrowTableData == null)
			{
				mGrowTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint)level);
			}
			return mGrowTableData;
		}
	}

	public GrowEquipItemTable.GrowEquipItemData nextGrowTableData
	{
		get
		{
			if (mNextGrowTableData == null)
			{
				mNextGrowTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint)(level + 1));
			}
			return mNextGrowTableData;
		}
	}

	public GrowEquipItemTable.GrowEquipItemNeedItemData nextNeedTableData
	{
		get
		{
			if (mNextNeedTableData == null)
			{
				mNextNeedTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemNeedUniqueItemData(tableData.needUniqueId, (uint)(level + 1));
				if (mNextNeedTableData == null)
				{
					mNextNeedTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemNeedItemData(tableData.needId, (uint)(level + 1));
				}
			}
			return mNextNeedTableData;
		}
	}

	public List<int> atkList
	{
		get
		{
			if (mAtkList == null)
			{
				mAtkList = GetAtkList();
			}
			return mAtkList;
		}
	}

	public List<int> defList
	{
		get
		{
			if (mDefList == null)
			{
				mDefList = GetDefList();
			}
			return mDefList;
		}
	}

	public int atk => atkList[0];

	public int def => defList[0];

	public int hp
	{
		get
		{
			if (!mHp.HasValue)
			{
				mHp = ((level > 1) ? GetGrowParamHp() : ((int)tableData.baseHp + (int)exceedParam.hp));
			}
			return mHp.Value;
		}
	}

	public int elemAtk
	{
		get
		{
			if (!mElemAtk.HasValue)
			{
				mElemAtk = Mathf.Max(atkList.GetRange(1, atkList.Count - 1).ToArray());
			}
			return mElemAtk.Value;
		}
	}

	public int elemDef
	{
		get
		{
			if (!mElemDef.HasValue)
			{
				mElemDef = Mathf.Max(defList.GetRange(1, defList.Count - 1).ToArray());
			}
			return mElemDef.Value;
		}
	}

	public EquipItemInfo()
	{
	}

	public EquipItemInfo(EquipItem recv_data)
	{
		SetValue(recv_data);
	}

	public EquipItemInfo(CharaInfo.EquipItem home_chara_equip_data)
	{
		EquipItem equipItem = new EquipItem
		{
			uniqId = "0",
			equipItemId = home_chara_equip_data.eId,
			level = home_chara_equip_data.lv,
			exceed = home_chara_equip_data.exceed,
			price = 0,
			is_locked = 0,
			ability = new List<EquipItem.Ability>()
		};
		int i = 0;
		for (int count = home_chara_equip_data.aIds.Count; i < count; i++)
		{
			EquipItem.Ability item = new EquipItem.Ability
			{
				id = home_chara_equip_data.aIds[i],
				pt = home_chara_equip_data.aPts[i]
			};
			equipItem.ability.Add(item);
		}
		equipItem.abilityItem = home_chara_equip_data.ai;
		SetValue(equipItem);
	}

	public EquipItemInfo(uint id)
	{
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(id);
		EquipItem equipItem = new EquipItem
		{
			uniqId = "0",
			equipItemId = (int)id,
			level = equipItemData.maxLv,
			exceed = 4,
			price = 0,
			is_locked = 0,
			ability = new List<EquipItem.Ability>()
		};
		int i = 0;
		for (int num = equipItemData.fixedAbility.Length; i < num; i++)
		{
			EquipItem.Ability item = new EquipItem.Ability
			{
				id = equipItemData.fixedAbility[i].id,
				pt = 1
			};
			equipItem.ability.Add(item);
		}
		SetValue(equipItem);
	}

	public override void SetValue(EquipItem recv_data)
	{
		ulong.TryParse(recv_data.uniqId, out ulong result);
		base.uniqueID = result;
		base.tableID = (uint)recv_data.equipItemId;
		level = recv_data.level;
		exceed = recv_data.exceed;
		sellPrice = recv_data.price;
		isFavorite = (recv_data.is_locked != 0);
		UpdateTableData();
		if (tableData == null)
		{
			Log.Error(LOG.RESOURCE, "table = null");
			return;
		}
		exceedParam = tableData.GetExceedParam((uint)recv_data.exceed);
		if (exceedParam == null)
		{
			exceedParam = new EquipItemExceedParamTable.EquipItemExceedParamAll();
		}
		int cnt = 0;
		int num = 0;
		if (exceedParam != null && exceedParam.ability.Length != 0)
		{
			num += exceedParam.ability.Length;
		}
		ability = new EquipItemAbility[recv_data.ability.Count + GetFixedAbilityCount() + num];
		for (int i = 0; i < tableData.fixedAbility.Length; i++)
		{
			if (!tableData.fixedAbility[i].vr)
			{
				ability[cnt++] = new EquipItemAbility((uint)tableData.fixedAbility[i].id, tableData.fixedAbility[i].pt);
			}
		}
		recv_data.ability.ForEach(delegate(EquipItem.Ability a)
		{
			ability[cnt++] = new EquipItemAbility((uint)a.id, a.pt);
		});
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				ability[cnt++] = new EquipItemAbility((uint)exceedParam.ability[j].id, exceedParam.ability[j].pt);
			}
		}
		abilityItem = recv_data.abilityItem;
	}

	public void SetDefaultData()
	{
		if (tableData != null)
		{
			mAtkList = new List<int>();
			mDefList = new List<int>();
			mElemAtk = null;
			mElemDef = null;
			mHp = null;
			mAtkList.Add(tableData.baseAtk);
			mDefList.Add(tableData.baseDef);
			level = 1;
			exceed = 0;
		}
	}

	public List<int> GetAtkList()
	{
		List<int> list = new List<int>();
		if (level > 1)
		{
			list.Add(GetGrowParamAtk());
			int[] growParamElemAtk = GetGrowParamElemAtk();
			int i = 0;
			for (int num = tableData.atkElement.Length; i < num; i++)
			{
				list.Add(growParamElemAtk[i]);
			}
		}
		else
		{
			list.Add((int)tableData.baseAtk + (int)exceedParam.atk);
			int j = 0;
			for (int num2 = tableData.atkElement.Length; j < num2; j++)
			{
				list.Add(tableData.atkElement[j] + exceedParam.atkElement[j]);
			}
		}
		return list;
	}

	public List<int> GetDefList()
	{
		List<int> list = new List<int>();
		if (level > 1)
		{
			list.Add(GetGrowParamDef());
			int[] growParamElemDef = GetGrowParamElemDef();
			int i = 0;
			for (int num = tableData.defElement.Length; i < num; i++)
			{
				if (tableData.isFormer)
				{
					growParamElemDef[i] *= 10;
				}
				list.Add(growParamElemDef[i]);
			}
		}
		else
		{
			list.Add((int)tableData.baseDef + (int)exceedParam.def);
			int j = 0;
			for (int num2 = tableData.defElement.Length; j < num2; j++)
			{
				int num3 = tableData.defElement[j] + exceedParam.defElement[j];
				if (tableData.isFormer)
				{
					num3 *= 10;
				}
				list.Add(num3);
			}
		}
		return list;
	}

	public void UpdateTableData()
	{
		tableData = Singleton<EquipItemTable>.I.GetEquipItemData(base.tableID);
		mDefList = null;
		mAtkList = null;
		mElemAtk = null;
		mElemDef = null;
	}

	public static InventoryList<EquipItemInfo, EquipItem> CreateList(List<EquipItem> recv_list)
	{
		InventoryList<EquipItemInfo, EquipItem> list = new InventoryList<EquipItemInfo, EquipItem>();
		recv_list.ForEach(delegate(EquipItem o)
		{
			list.Add(o);
		});
		return list;
	}

	public int GetElemAtkType()
	{
		if (level != 1)
		{
			return tableData.GetElemType(GetGrowParamElemAtk());
		}
		return tableData.GetElemAtkType((exceedParam != null) ? exceedParam.atkElement : null);
	}

	public int GetElemAtkTypePriorityToTable()
	{
		if (tableData.atkElementType != ELEMENT_TYPE.MAX)
		{
			return (int)tableData.atkElementType;
		}
		return GetElemAtkType();
	}

	public int GetElemDefType()
	{
		if (level != 1)
		{
			return tableData.GetElemType(GetGrowParamElemDef());
		}
		return tableData.GetElemDefType((exceedParam != null) ? exceedParam.defElement : null);
	}

	public int GetElemDefTypePriorityToTable()
	{
		if (tableData.defElementType != ELEMENT_TYPE.MAX)
		{
			return (int)tableData.defElementType;
		}
		return GetElemDefType();
	}

	public ELEMENT_TYPE GetTargetElement()
	{
		if (!tableData.IsWeapon())
		{
			return (ELEMENT_TYPE)GetElemDefType();
		}
		return (ELEMENT_TYPE)GetElemAtkType();
	}

	public ELEMENT_TYPE GetTargetElementPriorityToTable()
	{
		if (!tableData.IsWeapon())
		{
			return (ELEMENT_TYPE)GetElemDefTypePriorityToTable();
		}
		return (ELEMENT_TYPE)GetElemAtkTypePriorityToTable();
	}

	private int GetGrowParamAtk(bool is_next_level = false)
	{
		return (is_next_level ? nextGrowTableData : growTableData).GetGrowParamAtk(tableData.baseAtk) + (int)exceedParam.atk;
	}

	private int GetGrowParamDef(bool is_next_level = false)
	{
		return (is_next_level ? nextGrowTableData : growTableData).GetGrowParamDef(tableData.baseDef) + (int)exceedParam.def;
	}

	private int GetGrowParamHp(bool is_next_level = false)
	{
		return (is_next_level ? nextGrowTableData : growTableData).GetGrowParamHp(tableData.baseHp) + (int)exceedParam.hp;
	}

	private int[] GetGrowParamElemAtk(bool is_next_level = false)
	{
		int[] growParamElemAtk = (is_next_level ? nextGrowTableData : growTableData).GetGrowParamElemAtk(tableData.atkElement);
		int[] array = new int[tableData.atkElement.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i] = growParamElemAtk[i] + exceedParam.atkElement[i];
		}
		return array;
	}

	private int[] GetGrowParamElemDef(bool is_next_level = false)
	{
		int[] growParamElemDef = (is_next_level ? nextGrowTableData : growTableData).GetGrowParamElemDef(tableData.defElement);
		int[] array = new int[tableData.defElement.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i] = growParamElemDef[i] + exceedParam.defElement[i];
		}
		return array;
	}

	public int GetMaxSlot()
	{
		int num = tableData.maxSlot;
		if (exceed > 0)
		{
			EquipItemExceedParamTable.EquipItemExceedParamAll equipItemExceedParamAll = tableData.GetExceedParam((uint)exceed);
			if (equipItemExceedParamAll != null && equipItemExceedParamAll.skillSlot.Length != 0)
			{
				num += equipItemExceedParamAll.skillSlot.Length;
			}
		}
		return num;
	}

	public bool IsExceedSkillSlot(int index)
	{
		return index >= tableData.maxSlot;
	}

	public int GetExceedSkillSlotNo(int index)
	{
		if (!IsExceedSkillSlot(index))
		{
			return -1;
		}
		int num = index - tableData.maxSlot;
		int num2 = 0;
		int num3 = 0;
		for (int i = 1; i <= exceed; i++)
		{
			EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParam(tableData.exceedID, (uint)i);
			if (equipItemExceedParam == null)
			{
				return -1;
			}
			if (equipItemExceedParam.skillSlot.slotType != 0)
			{
				if (num2 == num)
				{
					num3 = (int)equipItemExceedParam.cnt;
					break;
				}
				num2++;
			}
		}
		if (num3 == 0)
		{
			return -1;
		}
		return num3 + 100;
	}

	public int GetExceedSkillIndex(int slotNo)
	{
		if (slotNo < 100)
		{
			return -1;
		}
		int num = slotNo - 100;
		if (num > 4)
		{
			return -1;
		}
		int num2 = tableData.maxSlot;
		for (int i = 1; i <= exceed; i++)
		{
			EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParam(tableData.exceedID, (uint)i);
			if (equipItemExceedParam == null)
			{
				return -1;
			}
			if (equipItemExceedParam.skillSlot.slotType != 0)
			{
				if (i == num)
				{
					return num2;
				}
				num2++;
			}
		}
		return -1;
	}

	public SkillItemInfo GetSkillItem(int index)
	{
		return GetSkillItem(index, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
	}

	public SkillItemInfo GetSkillItem(int index, int setNo)
	{
		if (GetMaxSlot() <= index)
		{
			Log.Warning("GetSkillItem :: index out of bounds :: uniqID = " + base.uniqueID + " : tableID = " + base.tableID);
			return null;
		}
		SkillItemInfo[] skillInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetSkillInventoryClone();
		SkillItemInfo skill_info = null;
		Array.ForEach(skillInventoryClone, delegate(SkillItemInfo skill_item)
		{
			if (skill_info == null)
			{
				bool flag = false;
				EquipSetSkillData equipSetSkillData = skill_item.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == setNo);
				if (equipSetSkillData != null && equipSetSkillData.equipItemUniqId == base.uniqueID)
				{
					if (equipSetSkillData.equipSlotNo == index)
					{
						flag = true;
					}
					else if (equipSetSkillData.equipSlotNo == GetExceedSkillSlotNo(index))
					{
						flag = true;
					}
				}
				if (flag)
				{
					skill_info = skill_item;
				}
			}
		});
		return skill_info;
	}

	public SkillItemInfo GetUniqueSkillItem(int index)
	{
		if (GetMaxSlot() <= index)
		{
			Log.Warning("GetUniqueSkillItem :: index out of bounds :: uniqID = " + base.uniqueID + " : tableID = " + base.tableID);
			return null;
		}
		SkillItemInfo[] skillInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetSkillInventoryClone();
		SkillItemInfo skill_info = null;
		Array.ForEach(skillInventoryClone, delegate(SkillItemInfo skill_item)
		{
			if (skill_info == null)
			{
				bool flag = false;
				EquipSetSkillData uniqueEquipSetSkill = skill_item.uniqueEquipSetSkill;
				if (uniqueEquipSetSkill != null && uniqueEquipSetSkill.equipItemUniqId == base.uniqueID)
				{
					if (uniqueEquipSetSkill.equipSlotNo == index)
					{
						flag = true;
					}
					else if (uniqueEquipSetSkill.equipSlotNo == GetExceedSkillSlotNo(index))
					{
						flag = true;
					}
				}
				if (flag)
				{
					skill_info = skill_item;
				}
			}
		});
		return skill_info;
	}

	public AbilityItemInfo GetAbilityItem()
	{
		AbilityItemInfo abilityItemInfo = MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll().Find((AbilityItemInfo x) => x.equipUniqueId == base.uniqueID && x.equipUniqueId != 0);
		if (abilityItemInfo != null)
		{
			return abilityItemInfo;
		}
		if (abilityItem != null && abilityItem.abilityItemId != 0)
		{
			AbilityItemInfo abilityItemInfo2 = new AbilityItemInfo();
			abilityItemInfo2.SetValue(abilityItem);
			return abilityItemInfo2;
		}
		return null;
	}

	public bool IsLevelMax()
	{
		return level >= tableData.maxLv;
	}

	public bool IsExceedMax()
	{
		if (tableData.exceedID == 0)
		{
			return true;
		}
		return exceed >= 4;
	}

	public bool IsLevelAndEvolveMax()
	{
		if (IsLevelMax())
		{
			return !tableData.IsEvolve();
		}
		return false;
	}

	public void GetAttachSkillBuffParam(out int[] atk, out int[] def, out int hp)
	{
		_GetAttachSkillBuffParam(out atk, out def, out hp);
	}

	private void _GetAttachSkillBuffParam(out int[] atk, out int[] def, out int hp)
	{
		atk = new int[7];
		def = new int[7];
		for (int i = 0; i < 7; i++)
		{
			atk[i] = 0;
			def[i] = 0;
		}
		hp = 0;
		int j = 0;
		for (int maxSlot = GetMaxSlot(); j < maxSlot; j++)
		{
			SkillItemInfo skillItem = GetSkillItem(j);
			if (skillItem != null && Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItem.tableData.growID, skillItem.level, skillItem.exceedCnt) != null)
			{
				hp += skillItem.hp;
				int k = 0;
				for (int num = 7; k < num; k++)
				{
					atk[k] += skillItem.atkList[k];
					def[k] += skillItem.defList[k];
				}
			}
		}
	}

	public ItemStatus GetEquipSkillParam()
	{
		ItemStatus itemStatus = new ItemStatus();
		GetAttachSkillBuffParam(out int[] atk, out int[] def, out int hp);
		itemStatus.atk = atk[0];
		itemStatus.def = def[0];
		itemStatus.hp = hp;
		int i = 0;
		for (int num = 6; i < num; i++)
		{
			itemStatus.elemAtk[i] = atk[i + 1];
			itemStatus.elemDef[i] = def[i + 1];
		}
		return itemStatus;
	}

	public ItemStatus[] GetEquipTypeSkillParam()
	{
		ItemStatus[] array = new ItemStatus[MonoBehaviourSingleton<StatusManager>.I.ENABLE_EQUIP_TYPE_MAX + 1];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i] = new ItemStatus();
		}
		int j = 0;
		for (int maxSlot = GetMaxSlot(); j < maxSlot; j++)
		{
			SkillItemInfo skillItem = GetSkillItem(j);
			if (skillItem != null)
			{
				ENABLE_EQUIP_TYPE[] obj = (ENABLE_EQUIP_TYPE[])Enum.GetValues(typeof(ENABLE_EQUIP_TYPE));
				int k = 0;
				for (int num2 = obj.Length; k < num2; k++)
				{
					ItemStatus obj2 = array[k];
					ItemStatus param = skillItem.GetEquipTypeSkillParam()[k];
					obj2.Add(param);
				}
			}
		}
		return array;
	}

	public int GetValidAbilityLength()
	{
		if (ability == null || ability.Length == 0)
		{
			return 0;
		}
		int num = 0;
		int i = 0;
		for (int num2 = ability.Length; i < num2; i++)
		{
			if (ability[i].id != 0)
			{
				num++;
			}
		}
		return num;
	}

	public EquipItemAbility[] GetValidAbility()
	{
		int validAbilityLength = GetValidAbilityLength();
		EquipItemAbility[] array = new EquipItemAbility[validAbilityLength];
		int num = 0;
		for (int i = 0; i < validAbilityLength; i++)
		{
			if (ability[i].id != 0)
			{
				array[num] = ability[i];
				num++;
			}
		}
		return array;
	}

	public int GetValidLotAbility()
	{
		return GetValidAbilityLength() - GetFixedAbilityCount();
	}

	public EquipItemAbility[] GetLotteryAbility()
	{
		EquipItemAbility[] validAbility = GetValidAbility();
		List<EquipItemAbility> list = new List<EquipItemAbility>(validAbility);
		Array.ForEach(tableData.fixedAbility, delegate(EquipItem.Ability fixed_ability)
		{
			if (fixed_ability != null && fixed_ability.id != 0 && fixed_ability.pt != 0 && !fixed_ability.vr)
			{
				int num2 = list.FindIndex((EquipItemAbility d) => d.id == fixed_ability.id && d.ap == fixed_ability.pt);
				if (num2 != -1)
				{
					list.RemoveAt(num2);
				}
			}
		});
		Array.ForEach(exceedParam.ability, delegate(EquipItem.Ability exceed_ability)
		{
			if (exceed_ability != null && exceed_ability.id != 0 && exceed_ability.pt != 0)
			{
				int num = list.FindIndex((EquipItemAbility d) => d.id == exceed_ability.id && d.ap == exceed_ability.pt);
				if (num != -1)
				{
					list.RemoveAt(num);
				}
			}
		});
		return list.ToArray();
	}

	public bool IsFixedAbility(int index)
	{
		if (0 > index || tableData.fixedAbility.Length <= index)
		{
			return false;
		}
		EquipItem.Ability ability = tableData.fixedAbility[index];
		EquipItemAbility equipItemAbility = GetValidAbility()[index];
		if (ability.id == equipItemAbility.id && ability.pt == equipItemAbility.ap)
		{
			return !ability.vr;
		}
		return false;
	}

	public int GetFixedAbilityCount()
	{
		int num = 0;
		for (int i = 0; i < tableData.fixedAbility.Length; i++)
		{
			if (!tableData.fixedAbility[i].vr)
			{
				num++;
			}
		}
		return num;
	}

	public bool HasNeedUpdateAbility()
	{
		for (int i = 0; i < ability.Length; i++)
		{
			AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(ability[i].id, ability[i].ap);
			if (abilityData == null)
			{
				continue;
			}
			for (int j = 0; j < abilityData.info.Length; j++)
			{
				if (abilityData.info[j].IsNeedUpdate())
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsActiveAbility()
	{
		for (int i = 0; i < this.ability.Length; i++)
		{
			AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(this.ability[i].id);
			if (ability != null && !ability.IsActive())
			{
				return false;
			}
		}
		return !HasNeedUpdateAbility();
	}
}
