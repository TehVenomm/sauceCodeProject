// Decompiled with JetBrains decompiler
// Type: SkillItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SkillItemInfo : ItemInfoBase<Network.SkillItem>
{
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
  public EquipSetSkillData uniqueEquipSetSkill;
  public string exceedExtraText;
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

  public bool isAttached => this.equipSetSkill != null && this.equipSetSkill.Count > 0;

  public bool isUniqueAttached
  {
    get => this.uniqueEquipSetSkill != null && this.uniqueEquipSetSkill.equipItemUniqId > 0UL;
  }

  public int atk => this.atkList[0];

  public int def => this.defList[0];

  public bool IsCurrentEquipSetAttached
  {
    get
    {
      return this.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo())) != null;
    }
  }

  public bool IsUniqueEquipSetAttached => this.uniqueEquipSetSkill.equipItemUniqId > 0UL;

  public int elemAtk { get; private set; }

  public int elemDef { get; private set; }

  public int needExp { get; private set; }

  public int giveExp { get; private set; }

  public int giveExceedExp { get; private set; }

  public int giveSameSkillExceedExp { get; private set; }

  public int exceedExpPrev { get; private set; }

  public int exceedExpNext { get; private set; }

  public SkillItemInfo()
  {
  }

  public SkillItemInfo(int index, int id, int lv, int exceed)
  {
    Network.SkillItem recv = new Network.SkillItem()
    {
      uniqId = "0",
      skillItemId = id,
      level = (XorInt) lv,
      exceed = exceed,
      is_locked = 0,
      equipSlots = new List<Network.SkillItem.EquipSetSlot>()
    };
    recv.equipSlots.Add(new Network.SkillItem.EquipSetSlot()
    {
      euid = "0",
      setNo = 0,
      slotNo = index
    });
    recv.uniqueEquipSlots = new Network.SkillItem.UniqueEquipSetSlot();
    recv.uniqueEquipSlots.euid = "0";
    recv.uniqueEquipSlots.slotNo = index;
    recv.exp = 0;
    recv.expNext = 0;
    recv.expPrev = 0;
    recv.price = 0;
    recv.growCost = 0.0f;
    recv.exceedExp = 0;
    this.SetValue(recv);
    this.num = -1;
    this.itemId = 0U;
  }

  public override void SetValue(Network.SkillItem recv_data)
  {
    if (recv_data.skillItemId >= 1001000 && recv_data.skillItemId <= 1001003)
      recv_data.skillItemId = 401900000 + recv_data.skillItemId % 10 + 1;
    this.uniqueID = ulong.Parse(recv_data.uniqId);
    this.tableID = (uint) recv_data.skillItemId;
    this.level = (int) recv_data.level;
    this.exceedCnt = recv_data.exceed;
    this.exceedExp = recv_data.exceedExp;
    this.exp = recv_data.exp;
    this.expPrev = recv_data.expPrev;
    this.expNext = recv_data.expNext;
    this.isFavorite = recv_data.is_locked != 0;
    this.growCost = recv_data.growCost;
    this.sellPrice = recv_data.price;
    this.equipSetSkill = new List<EquipSetSkillData>();
    foreach (Network.SkillItem.EquipSetSlot equipSlot in recv_data.equipSlots)
    {
      ulong result;
      if (ulong.TryParse(equipSlot.euid, out result))
      {
        if (result != 0UL)
          this.equipSetSkill.Add(new EquipSetSkillData(equipSlot));
      }
      else
        Log.Error("parse error euid:{0}", (object) equipSlot.euid);
    }
    this.uniqueEquipSetSkill = new EquipSetSkillData(recv_data.uniqueEquipSlots);
    this.UpdateTableData();
    this.growData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(this.tableData.growID, this.level, this.exceedCnt);
    this.nextGrowData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(this.tableData.growID, this.level + 1, this.exceedCnt);
    this.atkList = new List<int>();
    this.defList = new List<int>();
    if (this.level > 1)
    {
      this.atkList.Add(this.GetGrowParamAtk());
      this.defList.Add(this.GetGrowParamDef());
      this.hp = this.GetGrowParamHp();
      int[] growParamElemAtk = this.GetGrowParamElemAtk();
      int[] growParamElemDef = this.GetGrowParamElemDef();
      int index1 = 0;
      for (int length = this.tableData.atkElement.Length; index1 < length; ++index1)
      {
        this.atkList.Add(growParamElemAtk[index1]);
        this.defList.Add(growParamElemDef[index1]);
      }
      this.elemAtk = Mathf.Max(growParamElemAtk);
      this.elemDef = Mathf.Max(growParamElemDef);
      this.skillAtk = this.GetGrowParamSkillAtk();
      this.skillAtkRate = this.GetGrowParamSkillAtkRate();
      this.healHp = this.GetGrowParamHealHp();
      this.supportValue = new int[3];
      for (int index2 = 0; index2 < 3; ++index2)
        this.supportValue[index2] = this.GetGrowParamSupprtValue(index2);
      this.supportTime = new float[3];
      for (int index3 = 0; index3 < 3; ++index3)
        this.supportTime[index3] = this.GetGrowParamSupprtTime(index3);
    }
    else
    {
      this.atkList.Add((int) this.tableData.baseAtk);
      this.defList.Add((int) this.tableData.baseDef);
      this.hp = (int) this.tableData.baseHp;
      int index = 0;
      for (int length = this.tableData.atkElement.Length; index < length; ++index)
      {
        this.atkList.Add(this.tableData.atkElement[index]);
        this.defList.Add(this.tableData.defElement[index]);
      }
      this.elemAtk = Mathf.Max(this.tableData.atkElement);
      this.elemDef = Mathf.Max(this.tableData.defElement);
      this.skillAtk = (int) this.tableData.skillAtk;
      this.skillAtkRate = (int) this.tableData.skillAtkRate;
      this.healHp = (int) this.tableData.healHp;
      this.supportValue = this.tableData.supportValue;
      this.supportTime = this.tableData.supportTime;
    }
    this.needExp = this.GetGrowParamNeedExp();
    this.giveExp = this.GetGrowParamGiveExp();
    this.giveExceedExp = Singleton<ExceedSkillItemTable>.I.GetExceedExp(this);
    this.giveSameSkillExceedExp = (int) ((double) this.giveExceedExp * (double) Singleton<ExceedSkillItemTable>.I.GetExceedRaritySamePointRate(this.tableData.rarity));
    this.exceedExpPrev = Singleton<ExceedSkillItemTable>.I.GetNeedExceedExp(this.tableData.rarity, this.exceedCnt);
    this.exceedExpNext = Singleton<ExceedSkillItemTable>.I.GetNeedExceedExp(this.tableData.rarity, this.exceedCnt + 1);
    this.isUpdateExplanationText = false;
    this.num = -1;
  }

  public void UpdateTableData()
  {
    this.tableData = Singleton<SkillItemTable>.I.GetSkillItemData(this.tableID);
  }

  public void UpdateEquipSetSkill(List<EquipSetSkillData> updateSkill)
  {
    this.equipSetSkill = updateSkill;
  }

  public void UpdateUniqueEquipSetSkill(EquipSetSkillData updateSkill)
  {
    this.uniqueEquipSetSkill = updateSkill;
  }

  private int GetGrowParamAtk(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamAtk((int) this.tableData.baseAtk);
  }

  private int GetGrowParamDef(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamDef((int) this.tableData.baseDef);
  }

  private int GetGrowParamHp(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamHp((int) this.tableData.baseHp);
  }

  private int GetGrowParamSkillAtk(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamSkillAtk((int) this.tableData.skillAtk);
  }

  private int GetGrowParamSkillAtkRate(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamSkillAtkRate((int) this.tableData.skillAtkRate);
  }

  private int GetGrowParamHealHp(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamHealHp((int) this.tableData.healHp);
  }

  private int GetGrowParamSupprtValue(int index, bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamSupprtValue(this.tableData.supportValue, index);
  }

  private float GetGrowParamSupprtTime(int index, bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamSupprtTime(this.tableData.supportTime, index);
  }

  private int[] GetGrowParamElemAtk(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamElemAtk(this.tableData.atkElement);
  }

  private int[] GetGrowParamElemDef(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamElemDef(this.tableData.defElement);
  }

  private int GetGrowParamNeedExp(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamNeedExp(this.tableData.baseNeedExp);
  }

  private int GetGrowParamGiveExp(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowData : this.growData).GetGrowParamGiveExp(this.tableData.baseGiveExp);
  }

  public static InventoryList<SkillItemInfo, Network.SkillItem> CreateList(List<Network.SkillItem> recv_list)
  {
    InventoryList<SkillItemInfo, Network.SkillItem> list = new InventoryList<SkillItemInfo, Network.SkillItem>();
    recv_list.ForEach((Action<Network.SkillItem>) (o => list.Add(o)));
    return list;
  }

  public static InventoryList<SkillItemInfo, Network.SkillItem> CreateListFromItem(
    List<Network.Item> recv_list)
  {
    InventoryList<SkillItemInfo, Network.SkillItem> list = new InventoryList<SkillItemInfo, Network.SkillItem>();
    if (recv_list.IsNullOrEmpty<Network.Item>())
      return list;
    recv_list.ForEach((Action<Network.Item>) (o =>
    {
      if (Singleton<ItemTable>.I.GetItemData((uint) o.itemId).type != ITEM_TYPE.MATERIAL_MAGI || o.num <= 0)
        return;
      list.Add(new Network.SkillItem()
      {
        uniqId = o.uniqId,
        skillItemId = o.itemId,
        level = (XorInt) 1
      });
      list.GetLastNode().Value.num = o.num;
      list.GetLastNode().Value.itemId = (uint) o.itemId;
    }));
    return list;
  }

  public static InventoryList<SkillItemInfo, Network.SkillItem> CreateListFromItemInventory(
    InventoryList<ItemInfo, Network.Item> itemInventory)
  {
    InventoryList<SkillItemInfo, Network.SkillItem> fromItemInventory = new InventoryList<SkillItemInfo, Network.SkillItem>();
    for (LinkedListNode<ItemInfo> linkedListNode = itemInventory.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      ItemInfo itemInfo = linkedListNode.Value;
      if (itemInfo.tableData.type == ITEM_TYPE.MATERIAL_MAGI && itemInfo.GetNum() > 0)
      {
        fromItemInventory.Add(new Network.SkillItem()
        {
          uniqId = itemInfo.uniqueID.ToString(),
          skillItemId = (int) itemInfo.tableData.id,
          level = (XorInt) 1
        });
        fromItemInventory.GetLastNode().Value.num = itemInfo.GetNum();
        fromItemInventory.GetLastNode().Value.itemId = itemInfo.tableData.id;
      }
    }
    return fromItemInventory;
  }

  public static List<SkillItemInfo> CreateSkillItemInfoList(List<CharaInfo.EquipItem> recv_list)
  {
    List<SkillItemInfo> skillItemInfoList = new List<SkillItemInfo>();
    foreach (CharaInfo.EquipItem recv in recv_list)
    {
      int num = 0;
      for (int index = 0; index < recv.sIds.Count; ++index)
      {
        int exceed = 0;
        if (index < recv.sExs.Count)
          exceed = recv.sExs[index];
        SkillItemInfo skillItemInfo = new SkillItemInfo(num++, recv.sIds[index], recv.sLvs[index], exceed);
        skillItemInfoList.Add(skillItemInfo);
      }
    }
    return skillItemInfoList;
  }

  public bool IsLevelMax() => this.level >= this.tableData.GetMaxLv(0);

  public bool IsMaxExceed() => this.exceedCnt >= this.GetExceedMaxCount();

  public bool IsExistNextExceed()
  {
    return Singleton<ExceedSkillItemTable>.I.IsExistExceed(this.exceedCnt + 1);
  }

  public int GetExceedMaxCount() => Singleton<ExceedSkillItemTable>.I.GetMaxExceedCount();

  public int GetMaxLevel() => this.tableData.GetMaxLv(0);

  public bool IsExceeded() => this.exceedCnt > 0;

  public bool IsEnableExceed()
  {
    if (!this.IsLevelMax() || !this.IsExistNextExceed())
      return false;
    switch (this.tableData.type)
    {
      case SKILL_SLOT_TYPE.ATTACK:
      case SKILL_SLOT_TYPE.SUPPORT:
      case SKILL_SLOT_TYPE.HEAL:
        return true;
      default:
        return false;
    }
  }

  public static string GetExplanationText(
    string explanation_text,
    SkillItemInfo.GetReplaceString callback)
  {
    int cmd = 0;
    for (int length = SkillItemInfo.EXPLANATION_COMMAND_LIST.Length; cmd < length; ++cmd)
    {
      if (explanation_text.Contains(SkillItemInfo.EXPLANATION_COMMAND_LIST[cmd]))
        explanation_text = explanation_text.Replace(SkillItemInfo.EXPLANATION_COMMAND_LIST[cmd], callback((SkillItemInfo.EXPLANATION_COMMAND) cmd));
    }
    return explanation_text;
  }

  public static string GetExplanationStatusUpText(
    string explanation_text,
    string status_up_format,
    SkillItemInfo.GetReplaceString callback)
  {
    int cmd = 0;
    for (int length = SkillItemInfo.EXPLANATION_COMMAND_LIST.Length; cmd < length; ++cmd)
    {
      if (explanation_text.Contains(SkillItemInfo.EXPLANATION_COMMAND_LIST[cmd]))
        explanation_text = explanation_text.Replace(SkillItemInfo.EXPLANATION_COMMAND_LIST[cmd], string.Format(status_up_format, (object) callback((SkillItemInfo.EXPLANATION_COMMAND) cmd)));
    }
    return explanation_text;
  }

  public string GetExplanationText(bool isShowExceed = false)
  {
    if (!this.isUpdateExplanationText)
    {
      this.explanationText = SkillItemInfo.GetExplanationText(this.tableData.text, (SkillItemInfo.GetReplaceString) (cmd => this.GetStatusText(cmd)));
      if (this.IsLevelMax() && this.IsExceeded())
        this.exceedExplanationText = "\n" + SkillItemInfo.GetExceedExplanationText(this.tableData, this.level, this.exceedCnt);
      this.isUpdateExplanationText = true;
    }
    return isShowExceed ? this.explanationText + this.exceedExplanationText : this.explanationText;
  }

  public string GetExceedExtraText()
  {
    return SkillItemInfo.GetExceedExtraText(this.tableData, this.level, this.exceedCnt);
  }

  public static string GetExceedExplanationText(
    SkillItemTable.SkillItemData data,
    int level,
    int exceedCnt)
  {
    string str = StringTable.Format(STRING_CATEGORY.SMITH, 8U, (object) SkillItemInfo.GetDecreaseUseGaugePercent(exceedCnt));
    string exceedExtraText = SkillItemInfo.GetExceedExtraText(data, level, exceedCnt);
    if (!string.IsNullOrEmpty(exceedExtraText))
      str = $"{str}/{exceedExtraText}";
    return UIUtility.GetColorText(StringTable.Format(STRING_CATEGORY.SMITH, 11U, (object) StringTable.Format(STRING_CATEGORY.SMITH, 9U, (object) exceedCnt), (object) str), ExceedSkillItemTable.color);
  }

  public string GetExplanationStatusUpText(string format, bool isExceed, bool isHideExplanation)
  {
    return !isExceed ? SkillItemInfo.GetExplanationStatusUpText(this.tableData.text, format, (SkillItemInfo.GetReplaceString) (cmd => this.GetStatusText(cmd))) : (!isHideExplanation ? SkillItemInfo.GetExplanationText(this.tableData.text, (SkillItemInfo.GetReplaceString) (cmd => this.GetStatusText(cmd))) + "\n" : "") + SkillItemInfo.GetExceedExplanationText(this.tableData, this.level, this.exceedCnt);
  }

  private static int GetDecreaseUseGaugePercent(int exceedCnt)
  {
    ExceedSkillItemTable.ExceedSkillItemData exceedSkillItemData = Singleton<ExceedSkillItemTable>.I.GetExceedSkillItemData(exceedCnt);
    int decreaseUseGaugePercent = 0;
    if (exceedSkillItemData != null)
      decreaseUseGaugePercent = exceedSkillItemData.GetDecreaseUseGaugePercent();
    return decreaseUseGaugePercent;
  }

  private string GetStatusText(SkillItemInfo.EXPLANATION_COMMAND cmd)
  {
    switch (cmd)
    {
      case SkillItemInfo.EXPLANATION_COMMAND.ATK:
        return this.atkList[0].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.DEF:
        return this.defList[0].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.HP:
        return this.hp.ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.FIRE_ATK:
        return this.atkList[1].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.WATER_ATK:
        return this.atkList[2].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.THUNDER_ATK:
        return this.atkList[3].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SOIL_ATK:
        return this.atkList[4].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.LIGHR_ATK:
        return this.atkList[5].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.DARK_ATK:
        return this.atkList[6].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.FIRE_DEF:
        return this.defList[1].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.WATER_DEF:
        return this.defList[2].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.THUNDER_DEF:
        return this.defList[3].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SOIL_DEF:
        return this.defList[4].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.LIGHR_DEF:
        return this.defList[5].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.DARK_DEF:
        return this.defList[6].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SKILL_ATK:
        return Mathf.FloorToInt((float) this.skillAtk * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkDispRate).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SKILL_ATKRATE:
        return Mathf.FloorToInt((float) this.skillAtkRate * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkRateDispRate).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.HEAL_HP:
        return this.healHp.ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_VALUE_1:
        return this.supportValue[0].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_VALUE_2:
        return this.supportValue[1].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_VALUE_3:
        return this.supportValue[2].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_TIME_1:
        return this.supportTime[0].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_TIME_2:
        return this.supportTime[1].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_TIME_3:
        return this.supportTime[2].ToString();
      default:
        Log.Error("explanation command unsupported {0}", (object) SkillItemInfo.EXPLANATION_COMMAND_LIST[(int) cmd]);
        return SkillItemInfo.EXPLANATION_COMMAND_LIST[(int) cmd];
    }
  }

  public static string GetExplanationText(
    SkillItemTable.SkillItemData table_data,
    int level,
    int exceedCnt)
  {
    GrowSkillItemTable.GrowSkillItemData grow_data = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(table_data.growID, level, exceedCnt);
    string explanationText = SkillItemInfo.GetExplanationText(table_data.text, (SkillItemInfo.GetReplaceString) (cmd => SkillItemInfo.GetStatusText(cmd, table_data, grow_data, level)));
    if (level >= table_data.GetMaxLv(0) && Singleton<ExceedSkillItemTable>.I.IsExistExceed(exceedCnt + 1) && exceedCnt > 0)
      explanationText = $"{explanationText}\n{SkillItemInfo.GetExceedExplanationText(table_data, level, exceedCnt)}";
    return explanationText;
  }

  public static string GetExplanationStatusUpText(
    SkillItemTable.SkillItemData table_data,
    int level,
    int exceedCnt,
    string status_up_format)
  {
    GrowSkillItemTable.GrowSkillItemData grow_data = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(table_data.growID, level, exceedCnt);
    return SkillItemInfo.GetExplanationStatusUpText(table_data.text, status_up_format, (SkillItemInfo.GetReplaceString) (cmd => SkillItemInfo.GetStatusText(cmd, table_data, grow_data, level)));
  }

  public static string GetExceedExtraText(
    SkillItemTable.SkillItemData data,
    int level,
    int exceedCnt)
  {
    GrowSkillItemTable.GrowSkillItemData grow_data = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, level, exceedCnt);
    return SkillItemInfo.GetExplanationText(data.exceedExtraText, (SkillItemInfo.GetReplaceString) (cmd => SkillItemInfo.GetStatusText(cmd, data, grow_data, level)));
  }

  private static string GetStatusText(
    SkillItemInfo.EXPLANATION_COMMAND cmd,
    SkillItemTable.SkillItemData table_data,
    GrowSkillItemTable.GrowSkillItemData grow_data,
    int level)
  {
    switch (cmd)
    {
      case SkillItemInfo.EXPLANATION_COMMAND.ATK:
        return level <= 1 ? table_data.baseAtk.ToString() : grow_data.GetGrowParamAtk((int) table_data.baseAtk).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.DEF:
        return level <= 1 ? table_data.baseDef.ToString() : grow_data.GetGrowParamDef((int) table_data.baseDef).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.HP:
        return level <= 1 ? table_data.baseHp.ToString() : grow_data.GetGrowParamHp((int) table_data.baseHp).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.FIRE_ATK:
        return level <= 1 ? table_data.atkElement[0].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[0].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.WATER_ATK:
        return level <= 1 ? table_data.atkElement[1].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[1].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.THUNDER_ATK:
        return level <= 1 ? table_data.atkElement[2].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[2].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SOIL_ATK:
        return level <= 1 ? table_data.atkElement[3].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[3].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.LIGHR_ATK:
        return level <= 1 ? table_data.atkElement[4].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[4].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.DARK_ATK:
        return level <= 1 ? table_data.atkElement[5].ToString() : grow_data.GetGrowParamElemAtk(table_data.atkElement)[5].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.FIRE_DEF:
        return level <= 1 ? table_data.defElement[0].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[0].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.WATER_DEF:
        return level <= 1 ? table_data.defElement[1].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[1].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.THUNDER_DEF:
        return level <= 1 ? table_data.defElement[2].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[2].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SOIL_DEF:
        return level <= 1 ? table_data.defElement[3].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[3].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.LIGHR_DEF:
        return level <= 1 ? table_data.defElement[4].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[4].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.DARK_DEF:
        return level <= 1 ? table_data.defElement[5].ToString() : grow_data.GetGrowParamElemDef(table_data.defElement)[5].ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SKILL_ATK:
        return Mathf.FloorToInt((level > 1 ? (float) grow_data.GetGrowParamSkillAtk((int) table_data.skillAtk) : (float) (int) table_data.skillAtk) * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkDispRate).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SKILL_ATKRATE:
        return Mathf.FloorToInt((level > 1 ? (float) grow_data.GetGrowParamSkillAtkRate((int) table_data.skillAtkRate) : (float) (int) table_data.skillAtkRate) * MonoBehaviourSingleton<GlobalSettingsManager>.I.skillItem.explanationAtkRateDispRate).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.HEAL_HP:
        return level <= 1 ? table_data.healHp.ToString() : grow_data.GetGrowParamHealHp((int) table_data.healHp).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_VALUE_1:
        return level <= 1 ? table_data.supportValue[0].ToString() : grow_data.GetGrowParamSupprtValue(table_data.supportValue, 0).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_VALUE_2:
        return level <= 1 ? table_data.supportValue[1].ToString() : grow_data.GetGrowParamSupprtValue(table_data.supportValue, 1).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_VALUE_3:
        return level <= 1 ? table_data.supportValue[2].ToString() : grow_data.GetGrowParamSupprtValue(table_data.supportValue, 2).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_TIME_1:
        return level <= 1 ? table_data.supportTime[0].ToString() : grow_data.GetGrowParamSupprtTime(table_data.supportTime, 0).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_TIME_2:
        return level <= 1 ? table_data.supportTime[1].ToString() : grow_data.GetGrowParamSupprtTime(table_data.supportTime, 1).ToString();
      case SkillItemInfo.EXPLANATION_COMMAND.SUPPORT_TIME_3:
        return level <= 1 ? table_data.supportTime[2].ToString() : grow_data.GetGrowParamSupprtTime(table_data.supportTime, 2).ToString();
      default:
        Log.Error("explanation command unsupported {0}", (object) SkillItemInfo.EXPLANATION_COMMAND_LIST[(int) cmd]);
        return SkillItemInfo.EXPLANATION_COMMAND_LIST[(int) cmd];
    }
  }

  public ItemStatus GetEquipTypeBuff(EQUIPMENT_TYPE type)
  {
    int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(type);
    return this.GetEquipTypeSkillParam()[equipmentTypeIndex + 1];
  }

  public ItemStatus[] GetEquipTypeSkillParam()
  {
    ItemStatus[] equipTypeSkillParam = new ItemStatus[MonoBehaviourSingleton<StatusManager>.I.ENABLE_EQUIP_TYPE_MAX + 1];
    int index1 = 0;
    for (int length = equipTypeSkillParam.Length; index1 < length; ++index1)
      equipTypeSkillParam[index1] = new ItemStatus();
    if (!this.tableData.IsPassive())
      return equipTypeSkillParam;
    GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(this.tableData.growID, this.level, this.exceedCnt);
    if (growSkillItemData == null)
      return equipTypeSkillParam;
    ENABLE_EQUIP_TYPE[] values = (ENABLE_EQUIP_TYPE[]) Enum.GetValues(typeof (ENABLE_EQUIP_TYPE));
    int index2 = 0;
    for (int length = values.Length; index2 < length; ++index2)
    {
      ItemStatus itemStatus = equipTypeSkillParam[index2];
      for (int index3 = 0; index3 < 3; ++index3)
      {
        if (this.tableData.supportType[index3] != BuffParam.BUFFTYPE.NONE && this.tableData.IsEnableSupportEquipType(values[index2], index3))
        {
          int paramSupprtValue = growSkillItemData.GetGrowParamSupprtValue(this.tableData.supportValue, index3);
          switch (this.tableData.supportType[index3])
          {
            case BuffParam.BUFFTYPE.ATTACK_NORMAL:
              itemStatus.atk += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_FIRE:
              itemStatus.elemAtk[0] += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_WATER:
              itemStatus.elemAtk[1] += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_THUNDER:
              itemStatus.elemAtk[2] += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_SOIL:
              itemStatus.elemAtk[3] += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_LIGHT:
              itemStatus.elemAtk[4] += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_DARK:
              itemStatus.elemAtk[5] += paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.ATTACK_ALLELEMENT:
              itemStatus.elemAtk[0] += itemStatus.elemAtk[1] = itemStatus.elemAtk[2] = itemStatus.elemAtk[3] = itemStatus.elemAtk[4] = itemStatus.elemAtk[5] = paramSupprtValue;
              continue;
            case BuffParam.BUFFTYPE.DEFENCE_NORMAL:
              itemStatus.def += paramSupprtValue;
              continue;
            default:
              continue;
          }
        }
      }
    }
    return equipTypeSkillParam;
  }

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
    SUPPORT_TIME_3,
  }

  public delegate string GetReplaceString(SkillItemInfo.EXPLANATION_COMMAND cmd);
}
