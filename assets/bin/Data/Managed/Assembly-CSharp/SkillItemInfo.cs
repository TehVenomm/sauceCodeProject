using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemInfo : ItemInfoBase<SkillItem>
{
	public enum EXPLANATION_COMMAND
	{
		ATK,
		DEF,
		HP,
		FIRE_ATK,
		WATER_ATK,
		THUNDER_ATK,
		SOIL_ATK,
		LIGHR_ATK,
		DARK_ATK,
		FIRE_DEF,
		WATER_DEF,
		THUNDER_DEF,
		SOIL_DEF,
		LIGHR_DEF,
		DARK_DEF,
		SKILL_ATK,
		SKILL_ATKRATE,
		HEAL_HP,
		SUPPORT_VALUE_1,
		SUPPORT_VALUE_2,
		SUPPORT_VALUE_3,
		SUPPORT_TIME_1,
		SUPPORT_TIME_2,
		SUPPORT_TIME_3
	}

	public delegate string GetReplaceString(EXPLANATION_COMMAND cmd);

	public int level;

	public int exceedCnt;

	public int exceedExp;

	public int exp;

	public int expPrev;

	public int expNext;

	public List<int> atkList;

	public List<int> defList;

	public int hp;

	public bool isFavorite;

	public float growCost;

	public int sellPrice;

	public int skillAtk;

	public int skillAtkRate;

	public int healHp;

	public int num;

	public uint itemId;

	public int[] supportValue;

	public float[] supportTime;

	public List<EquipSetSkillData> equipSetSkill;

	private bool isUpdateExplanationText;

	private string explanationText;

	private string exceedExplanationText;

	public SkillItemTable.SkillItemData tableData;

	public GrowSkillItemTable.GrowSkillItemData growData;

	public GrowSkillItemTable.GrowSkillItemData nextGrowData;

	private static readonly string[] EXPLANATION_COMMAND_LIST = new string[24]
	{
		"[atk]",
		"[def]",
		"[hp]",
		"[fireAtk]",
		"[waterAtk]",
		"[thunderAtk]",
		"[soilAtk]",
		"[lightAtk]",
		"[darkAtk]",
		"[fireDef]",
		"[waterDef]",
		"[thunderDef]",
		"[soilDef]",
		"[lightDef]",
		"[darkDef]",
		"[skillAtk]",
		"[skillAtkRate]",
		"[healHp]",
		"[supportValue1]",
		"[supportValue2]",
		"[supportValue3]",
		"[supportTime1]",
		"[supportTime2]",
		"[supportTime3]"
	};

	public bool isAttached => equipSetSkill != null && equipSetSkill.Count > 0;

	public int atk => atkList[0];

	public int def => defList[0];

	public bool IsCurrentEquipSetAttached => equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()) != null;

	public int elemAtk
	{
		get;
		private set;
	}

	public int elemDef
	{
		get;
		private set;
	}

	public int needExp
	{
		get;
		private set;
	}

	public int giveExp
	{
		get;
		private set;
	}

	public int giveExceedExp
	{
		get;
		private set;
	}

	public int exceedExpPrev
	{
		get;
		private set;
	}

	public int exceedExpNext
	{
		get;
		private set;
	}

	public SkillItemInfo()
	{
	}

	public SkillItemInfo(int index, int id, int lv, int exceed)
	{
		SkillItem skillItem = new SkillItem();
		skillItem.uniqId = "0";
		skillItem.skillItemId = id;
		skillItem.level = lv;
		skillItem.exceed = exceed;
		skillItem.is_locked = 0;
		skillItem.equipSlots = new List<SkillItem.EquipSetSlot>();
		SkillItem.EquipSetSlot item = new SkillItem.EquipSetSlot
		{
			euid = "0",
			setNo = 0,
			slotNo = index
		};
		skillItem.equipSlots.Add(item);
		skillItem.exp = 0;
		skillItem.expNext = 0;
		skillItem.expPrev = 0;
		skillItem.price = 0;
		skillItem.growCost = 0f;
		skillItem.exceedExp = 0;
		SetValue(skillItem);
		num = -1;
		itemId = 0u;
	}

	public override void SetValue(SkillItem recv_data)
	{
		if (recv_data.skillItemId >= 1001000 && recv_data.skillItemId <= 1001003)
		{
			recv_data.skillItemId = 401900000 + recv_data.skillItemId % 10 + 1;
		}
		base.uniqueID = ulong.Parse(recv_data.uniqId);
		base.tableID = (uint)recv_data.skillItemId;
		level = recv_data.level;
		exceedCnt = recv_data.exceed;
		exceedExp = recv_data.exceedExp;
		exp = recv_data.exp;
		expPrev = recv_data.expPrev;
		expNext = recv_data.expNext;
		isFavorite = (recv_data.is_locked != 0);
		growCost = recv_data.growCost;
		sellPrice = recv_data.price;
		equipSetSkill = new List<EquipSetSkillData>();
		foreach (SkillItem.EquipSetSlot equipSlot in recv_data.equipSlots)
		{
			if (ulong.TryParse(equipSlot.euid, out ulong result))
			{
				if (result != 0L)
				{
					equipSetSkill.Add(new EquipSetSkillData(equipSlot));
				}
			}
			else
			{
				Log.Error("parse error euid:{0}", equipSlot.euid);
			}
		}
		UpdateTableData();
		growData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(tableData.growID, level);
		nextGrowData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(tableData.growID, level + 1);
		atkList = new List<int>();
		defList = new List<int>();
		if (level > 1)
		{
			atkList.Add(GetGrowParamAtk(false));
			defList.Add(GetGrowParamDef(false));
			hp = GetGrowParamHp(false);
			int[] growParamElemAtk = GetGrowParamElemAtk(false);
			int[] growParamElemDef = GetGrowParamElemDef(false);
			int i = 0;
			for (int num = tableData.atkElement.Length; i < num; i++)
			{
				atkList.Add(growParamElemAtk[i]);
				defList.Add(growParamElemDef[i]);
			}
			elemAtk = Mathf.Max(growParamElemAtk);
			elemDef = Mathf.Max(growParamElemDef);
			skillAtk = GetGrowParamSkillAtk(false);
			skillAtkRate = GetGrowParamSkillAtkRate(false);
			healHp = GetGrowParamHealHp(false);
			supportValue = new int[3];
			for (int j = 0; j < 3; j++)
			{
				supportValue[j] = GetGrowParamSupprtValue(j, false);
			}
			supportTime = new float[3];
			for (int k = 0; k < 3; k++)
			{
				supportTime[k] = GetGrowParamSupprtTime(k, false);
			}
		}
		else
		{
			atkList.Add(tableData.baseAtk);
			defList.Add(tableData.baseDef);
			hp = tableData.baseHp;
			int l = 0;
			for (int num2 = tableData.atkElement.Length; l < num2; l++)
			{
				atkList.Add(tableData.atkElement[l]);
				defList.Add(tableData.defElement[l]);
			}
			elemAtk = Mathf.Max(tableData.atkElement);
			elemDef = Mathf.Max(tableData.defElement);
			skillAtk = tableData.skillAtk;
			skillAtkRate = tableData.skillAtkRate;
			healHp = tableData.healHp;
			supportValue = tableData.supportValue;
			supportTime = tableData.supportTime;
		}
		needExp = GetGrowParamNeedExp(false);
		giveExp = GetGrowParamGiveExp(false);
		giveExceedExp = Singleton<ExceedSkillItemTable>.I.GetExceedExp(this);
		exceedExpPrev = Singleton<ExceedSkillItemTable>.I.GetNeedExceedExp(tableData.rarity, exceedCnt);
		exceedExpNext = Singleton<ExceedSkillItemTable>.I.GetNeedExceedExp(tableData.rarity, exceedCnt + 1);
		isUpdateExplanationText = false;
		this.num = -1;
	}

	public void UpdateTableData()
	{
		tableData = Singleton<SkillItemTable>.I.GetSkillItemData(base.tableID);
	}

	public void UpdateEquipSetSkill(List<EquipSetSkillData> updateSkill)
	{
		equipSetSkill = updateSkill;
	}

	private int GetGrowParamAtk(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamAtk(tableData.baseAtk);
	}

	private int GetGrowParamDef(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamDef(tableData.baseDef);
	}

	private int GetGrowParamHp(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamHp(tableData.baseHp);
	}

	private int GetGrowParamSkillAtk(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamSkillAtk(tableData.skillAtk);
	}

	private int GetGrowParamSkillAtkRate(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamSkillAtkRate(tableData.skillAtkRate);
	}

	private int GetGrowParamHealHp(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamHealHp(tableData.healHp);
	}

	private int GetGrowParamSupprtValue(int index, bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamSupprtValue(tableData.supportValue, index);
	}

	private float GetGrowParamSupprtTime(int index, bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamSupprtTime(tableData.supportTime, index);
	}

	private int[] GetGrowParamElemAtk(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamElemAtk(tableData.atkElement);
	}

	private int[] GetGrowParamElemDef(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamElemDef(tableData.defElement);
	}

	private int GetGrowParamNeedExp(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamNeedExp(tableData.baseNeedExp);
	}

	private int GetGrowParamGiveExp(bool is_next_level = false)
	{
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = (!is_next_level) ? growData : nextGrowData;
		return growSkillItemData.GetGrowParamGiveExp(tableData.baseGiveExp);
	}

	public static InventoryList<SkillItemInfo, SkillItem> CreateList(List<SkillItem> recv_list)
	{
		InventoryList<SkillItemInfo, SkillItem> list = new InventoryList<SkillItemInfo, SkillItem>();
		recv_list.ForEach(delegate(SkillItem o)
		{
			list.Add(o);
		});
		return list;
	}

	public static InventoryList<SkillItemInfo, SkillItem> CreateListFromItem(List<Item> recv_list)
	{
		InventoryList<SkillItemInfo, SkillItem> list = new InventoryList<SkillItemInfo, SkillItem>();
		if (recv_list.IsNullOrEmpty())
		{
			return list;
		}
		recv_list.ForEach(delegate(Item o)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)o.itemId);
			if (itemData.type == ITEM_TYPE.MATERIAL_MAGI && o.num > 0)
			{
				SkillItem item = new SkillItem
				{
					uniqId = o.uniqId,
					skillItemId = o.itemId,
					level = (XorInt)1
				};
				list.Add(item);
				list.GetLastNode().Value.num = o.num;
				list.GetLastNode().Value.itemId = (uint)o.itemId;
			}
		});
		return list;
	}

	public static InventoryList<SkillItemInfo, SkillItem> CreateListFromItemInventory(InventoryList<ItemInfo, Item> itemInventory)
	{
		InventoryList<SkillItemInfo, SkillItem> inventoryList = new InventoryList<SkillItemInfo, SkillItem>();
		for (LinkedListNode<ItemInfo> linkedListNode = itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			ItemInfo value = linkedListNode.Value;
			if (value.tableData.type == ITEM_TYPE.MATERIAL_MAGI && value.GetNum() > 0)
			{
				SkillItem skillItem = new SkillItem();
				skillItem.uniqId = value.uniqueID.ToString();
				skillItem.skillItemId = (int)value.tableData.id;
				skillItem.level = 1;
				inventoryList.Add(skillItem);
				inventoryList.GetLastNode().Value.num = value.GetNum();
				inventoryList.GetLastNode().Value.itemId = value.tableData.id;
			}
		}
		return inventoryList;
	}

	public static List<SkillItemInfo> CreateSkillItemInfoList(List<CharaInfo.EquipItem> recv_list)
	{
		List<SkillItemInfo> list = new List<SkillItemInfo>();
		foreach (CharaInfo.EquipItem item2 in recv_list)
		{
			int num = 0;
			for (int i = 0; i < item2.sIds.Count; i++)
			{
				int exceed = 0;
				if (i < item2.sExs.Count)
				{
					exceed = item2.sExs[i];
				}
				SkillItemInfo item = new SkillItemInfo(num++, item2.sIds[i], item2.sLvs[i], exceed);
				list.Add(item);
			}
		}
		return list;
	}

	public bool IsLevelMax()
	{
		return level >= tableData.GetMaxLv(0);
	}

	public bool IsMaxExceed()
	{
		return exceedCnt >= GetExceedMaxCount();
	}

	public bool IsExistNextExceed()
	{
		return Singleton<ExceedSkillItemTable>.I.IsExistExceed(exceedCnt + 1);
	}

	public int GetExceedMaxCount()
	{
		return Singleton<ExceedSkillItemTable>.I.GetMaxExceedCount();
	}

	public int GetMaxLevel()
	{
		return tableData.GetMaxLv(0);
	}

	public bool IsExceeded()
	{
		return exceedCnt > 0;
	}

	public bool IsEnableExceed()
	{
		if (!IsLevelMax())
		{
			return false;
		}
		if (IsExistNextExceed())
		{
			switch (tableData.type)
			{
			case SKILL_SLOT_TYPE.ATTACK:
			case SKILL_SLOT_TYPE.SUPPORT:
			case SKILL_SLOT_TYPE.HEAL:
				return true;
			}
		}
		return false;
	}

	public static string GetExplanationText(string explanation_text, GetReplaceString callback)
	{
		int i = 0;
		for (int num = EXPLANATION_COMMAND_LIST.Length; i < num; i++)
		{
			if (explanation_text.Contains(EXPLANATION_COMMAND_LIST[i]))
			{
				explanation_text = explanation_text.Replace(EXPLANATION_COMMAND_LIST[i], callback((EXPLANATION_COMMAND)i));
			}
		}
		return explanation_text;
	}

	public static string GetExplanationStatusUpText(string explanation_text, string status_up_format, GetReplaceString callback)
	{
		int i = 0;
		for (int num = EXPLANATION_COMMAND_LIST.Length; i < num; i++)
		{
			if (explanation_text.Contains(EXPLANATION_COMMAND_LIST[i]))
			{
				explanation_text = explanation_text.Replace(EXPLANATION_COMMAND_LIST[i], string.Format(status_up_format, callback((EXPLANATION_COMMAND)i)));
			}
		}
		return explanation_text;
	}

	public string GetExplanationText(bool isShowExceed = false)
	{
		if (!isUpdateExplanationText)
		{
			explanationText = GetExplanationText(tableData.text, (EXPLANATION_COMMAND cmd) => GetStatusText(cmd));
			if (IsLevelMax() && IsExceeded())
			{
				exceedExplanationText = "\n" + GetExceedExplanationText(exceedCnt);
			}
			isUpdateExplanationText = true;
		}
		if (isShowExceed)
		{
			return explanationText + exceedExplanationText;
		}
		return explanationText;
	}

	public static string GetExceedExplanationText(int exceedCnt)
	{
		string text = StringTable.Format(STRING_CATEGORY.SMITH, 11u, StringTable.Format(STRING_CATEGORY.SMITH, 9u, exceedCnt), StringTable.Format(STRING_CATEGORY.SMITH, 8u, GetDecreaseUseGaugePercent(exceedCnt)));
		return UIUtility.GetColorText(text, ExceedSkillItemTable.color);
	}

	public string GetExplanationStatusUpText(string format, bool isExceed, bool isHideExplanation)
	{
		string empty = string.Empty;
		if (!isExceed)
		{
			return GetExplanationStatusUpText(tableData.text, format, (EXPLANATION_COMMAND cmd) => GetStatusText(cmd));
		}
		empty = ((!isHideExplanation) ? (GetExplanationText(tableData.text, (EXPLANATION_COMMAND cmd) => GetStatusText(cmd)) + "\n") : string.Empty);
		return empty + GetExceedExplanationText(exceedCnt);
	}

	private static int GetDecreaseUseGaugePercent(int exceedCnt)
	{
		ExceedSkillItemTable.ExceedSkillItemData exceedSkillItemData = Singleton<ExceedSkillItemTable>.I.GetExceedSkillItemData(exceedCnt);
		int result = 0;
		if (exceedSkillItemData != null)
		{
			result = exceedSkillItemData.GetDecreaseUseGaugePercent();
		}
		return result;
	}

	private string GetStatusText(EXPLANATION_COMMAND cmd)
	{
		switch (cmd)
		{
		case EXPLANATION_COMMAND.ATK:
			return atkList[0].ToString();
		case EXPLANATION_COMMAND.DEF:
			return defList[0].ToString();
		case EXPLANATION_COMMAND.HP:
			return hp.ToString();
		case EXPLANATION_COMMAND.FIRE_ATK:
			return atkList[1].ToString();
		case EXPLANATION_COMMAND.WATER_ATK:
			return atkList[2].ToString();
		case EXPLANATION_COMMAND.THUNDER_ATK:
			return atkList[3].ToString();
		case EXPLANATION_COMMAND.SOIL_ATK:
			return atkList[4].ToString();
		case EXPLANATION_COMMAND.LIGHR_ATK:
			return atkList[5].ToString();
		case EXPLANATION_COMMAND.DARK_ATK:
			return atkList[6].ToString();
		case EXPLANATION_COMMAND.FIRE_DEF:
			return defList[1].ToString();
		case EXPLANATION_COMMAND.WATER_DEF:
			return defList[2].ToString();
		case EXPLANATION_COMMAND.THUNDER_DEF:
			return defList[3].ToString();
		case EXPLANATION_COMMAND.SOIL_DEF:
			return defList[4].ToString();
		case EXPLANATION_COMMAND.LIGHR_DEF:
			return defList[5].ToString();
		case EXPLANATION_COMMAND.DARK_DEF:
			return defList[6].ToString();
		case EXPLANATION_COMMAND.SKILL_ATK:
			return Mathf.FloorToInt((float)skillAtk * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkDispRate).ToString();
		case EXPLANATION_COMMAND.SKILL_ATKRATE:
			return Mathf.FloorToInt((float)skillAtkRate * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkRateDispRate).ToString();
		case EXPLANATION_COMMAND.HEAL_HP:
			return healHp.ToString();
		case EXPLANATION_COMMAND.SUPPORT_VALUE_1:
			return supportValue[0].ToString();
		case EXPLANATION_COMMAND.SUPPORT_VALUE_2:
			return supportValue[1].ToString();
		case EXPLANATION_COMMAND.SUPPORT_VALUE_3:
			return supportValue[2].ToString();
		case EXPLANATION_COMMAND.SUPPORT_TIME_1:
			return supportTime[0].ToString();
		case EXPLANATION_COMMAND.SUPPORT_TIME_2:
			return supportTime[1].ToString();
		case EXPLANATION_COMMAND.SUPPORT_TIME_3:
			return supportTime[2].ToString();
		default:
			Log.Error("explanation command unsupported {0}", EXPLANATION_COMMAND_LIST[(int)cmd]);
			return EXPLANATION_COMMAND_LIST[(int)cmd];
		}
	}

	public static string GetExplanationText(SkillItemTable.SkillItemData table_data, int level)
	{
		GrowSkillItemTable.GrowSkillItemData grow_data = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(table_data.growID, level);
		string text = GetExplanationText(table_data.text, (EXPLANATION_COMMAND cmd) => GetStatusText(cmd, table_data, grow_data, level));
		int num = grow_data.exceedCnt;
		if (level >= table_data.GetMaxLv(0) && Singleton<ExceedSkillItemTable>.I.IsExistExceed(num + 1) && num > 0)
		{
			text = text + "\n" + GetExceedExplanationText(num);
		}
		return text;
	}

	public static string GetExplanationStatusUpText(SkillItemTable.SkillItemData table_data, int level, string status_up_format)
	{
		GrowSkillItemTable.GrowSkillItemData grow_data = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(table_data.growID, level);
		return GetExplanationStatusUpText(table_data.text, status_up_format, (EXPLANATION_COMMAND cmd) => GetStatusText(cmd, table_data, grow_data, level));
	}

	private static string GetStatusText(EXPLANATION_COMMAND cmd, SkillItemTable.SkillItemData table_data, GrowSkillItemTable.GrowSkillItemData grow_data, int level)
	{
		switch (cmd)
		{
		case EXPLANATION_COMMAND.ATK:
			return (level <= 1) ? table_data.baseAtk.ToString() : grow_data.GetGrowParamAtk(table_data.baseAtk).ToString();
		case EXPLANATION_COMMAND.DEF:
			return (level <= 1) ? table_data.baseDef.ToString() : grow_data.GetGrowParamDef(table_data.baseDef).ToString();
		case EXPLANATION_COMMAND.HP:
			return (level <= 1) ? table_data.baseHp.ToString() : grow_data.GetGrowParamHp(table_data.baseHp).ToString();
		case EXPLANATION_COMMAND.FIRE_ATK:
			return (level <= 1) ? table_data.atkElement[0].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[0].ToString();
		case EXPLANATION_COMMAND.WATER_ATK:
			return (level <= 1) ? table_data.atkElement[1].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[1].ToString();
		case EXPLANATION_COMMAND.THUNDER_ATK:
			return (level <= 1) ? table_data.atkElement[2].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[2].ToString();
		case EXPLANATION_COMMAND.SOIL_ATK:
			return (level <= 1) ? table_data.atkElement[3].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[3].ToString();
		case EXPLANATION_COMMAND.LIGHR_ATK:
			return (level <= 1) ? table_data.atkElement[4].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[4].ToString();
		case EXPLANATION_COMMAND.DARK_ATK:
			return (level <= 1) ? table_data.atkElement[5].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[5].ToString();
		case EXPLANATION_COMMAND.FIRE_DEF:
			return (level <= 1) ? table_data.defElement[0].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[0].ToString();
		case EXPLANATION_COMMAND.WATER_DEF:
			return (level <= 1) ? table_data.defElement[1].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[1].ToString();
		case EXPLANATION_COMMAND.THUNDER_DEF:
			return (level <= 1) ? table_data.defElement[2].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[2].ToString();
		case EXPLANATION_COMMAND.SOIL_DEF:
			return (level <= 1) ? table_data.defElement[3].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[3].ToString();
		case EXPLANATION_COMMAND.LIGHR_DEF:
			return (level <= 1) ? table_data.defElement[4].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[4].ToString();
		case EXPLANATION_COMMAND.DARK_DEF:
			return (level <= 1) ? table_data.defElement[5].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[5].ToString();
		case EXPLANATION_COMMAND.SKILL_ATK:
		{
			int num2 = (level <= 1) ? ((int)table_data.skillAtk) : grow_data.GetGrowParamSkillAtk(table_data.skillAtk);
			num2 = Mathf.FloorToInt((float)num2 * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkDispRate);
			return num2.ToString();
		}
		case EXPLANATION_COMMAND.SKILL_ATKRATE:
		{
			int num = (level <= 1) ? ((int)table_data.skillAtkRate) : grow_data.GetGrowParamSkillAtkRate(table_data.skillAtkRate);
			return Mathf.FloorToInt((float)num * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkRateDispRate).ToString();
		}
		case EXPLANATION_COMMAND.HEAL_HP:
			return (level <= 1) ? table_data.healHp.ToString() : grow_data.GetGrowParamHealHp(table_data.healHp).ToString();
		case EXPLANATION_COMMAND.SUPPORT_VALUE_1:
			return (level <= 1) ? table_data.supportValue[0].ToString() : grow_data.GetGrowParamSupprtValue(table_data.supportValue, 0).ToString();
		case EXPLANATION_COMMAND.SUPPORT_VALUE_2:
			return (level <= 1) ? table_data.supportValue[1].ToString() : grow_data.GetGrowParamSupprtValue(table_data.supportValue, 1).ToString();
		case EXPLANATION_COMMAND.SUPPORT_VALUE_3:
			return (level <= 1) ? table_data.supportValue[2].ToString() : grow_data.GetGrowParamSupprtValue(table_data.supportValue, 2).ToString();
		case EXPLANATION_COMMAND.SUPPORT_TIME_1:
			return (level <= 1) ? table_data.supportTime[0].ToString() : grow_data.GetGrowParamSupprtTime(table_data.supportTime, 0).ToString();
		case EXPLANATION_COMMAND.SUPPORT_TIME_2:
			return (level <= 1) ? table_data.supportTime[1].ToString() : grow_data.GetGrowParamSupprtTime(table_data.supportTime, 1).ToString();
		case EXPLANATION_COMMAND.SUPPORT_TIME_3:
			return (level <= 1) ? table_data.supportTime[2].ToString() : grow_data.GetGrowParamSupprtTime(table_data.supportTime, 2).ToString();
		default:
			Log.Error("explanation command unsupported {0}", EXPLANATION_COMMAND_LIST[(int)cmd]);
			return EXPLANATION_COMMAND_LIST[(int)cmd];
		}
	}

	public ItemStatus GetEquipTypeBuff(EQUIPMENT_TYPE type)
	{
		int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type);
		return GetEquipTypeSkillParam()[equipmentTypeIndex + 1];
	}

	public ItemStatus[] GetEquipTypeSkillParam()
	{
		ItemStatus[] array = new ItemStatus[MonoBehaviourSingleton<StatusManager>.I.ENABLE_EQUIP_TYPE_MAX + 1];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			array[i] = new ItemStatus();
		}
		if (!tableData.IsPassive())
		{
			return array;
		}
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(tableData.growID, level);
		if (growSkillItemData == null)
		{
			return array;
		}
		ENABLE_EQUIP_TYPE[] array2 = (ENABLE_EQUIP_TYPE[])Enum.GetValues(typeof(ENABLE_EQUIP_TYPE));
		int j = 0;
		for (int num2 = array2.Length; j < num2; j++)
		{
			ItemStatus itemStatus = array[j];
			for (int k = 0; k < 3; k++)
			{
				if (tableData.supportType[k] != BuffParam.BUFFTYPE.NONE && tableData.IsEnableSupportEquipType(array2[j], k))
				{
					int growParamSupprtValue = growSkillItemData.GetGrowParamSupprtValue(tableData.supportValue, k);
					switch (tableData.supportType[k])
					{
					case BuffParam.BUFFTYPE.DEFENCE_NORMAL:
						itemStatus.def += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_NORMAL:
						itemStatus.atk += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_FIRE:
						itemStatus.elemAtk[0] += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_WATER:
						itemStatus.elemAtk[1] += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_THUNDER:
						itemStatus.elemAtk[2] += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_SOIL:
						itemStatus.elemAtk[3] += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_LIGHT:
						itemStatus.elemAtk[4] += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_DARK:
						itemStatus.elemAtk[5] += growParamSupprtValue;
						break;
					case BuffParam.BUFFTYPE.ATTACK_ALLELEMENT:
						itemStatus.elemAtk[0] += (itemStatus.elemAtk[1] = (itemStatus.elemAtk[2] = (itemStatus.elemAtk[3] = (itemStatus.elemAtk[4] = (itemStatus.elemAtk[5] = growParamSupprtValue)))));
						break;
					}
				}
			}
		}
		return array;
	}
}
