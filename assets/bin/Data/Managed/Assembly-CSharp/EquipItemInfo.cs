// Decompiled with JetBrains decompiler
// Type: EquipItemInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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

  public int level { get; private set; }

  public int exceed { get; private set; }

  public bool isFavorite { get; private set; }

  public int sellPrice { get; private set; }

  public GrowEquipItemTable.GrowEquipItemData growTableData
  {
    get
    {
      if (this.mGrowTableData == null)
        this.mGrowTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(this.tableData.growID, (uint) this.level);
      return this.mGrowTableData;
    }
  }

  public GrowEquipItemTable.GrowEquipItemData nextGrowTableData
  {
    get
    {
      if (this.mNextGrowTableData == null)
        this.mNextGrowTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(this.tableData.growID, (uint) (this.level + 1));
      return this.mNextGrowTableData;
    }
  }

  public GrowEquipItemTable.GrowEquipItemNeedItemData nextNeedTableData
  {
    get
    {
      if (this.mNextNeedTableData == null)
      {
        this.mNextNeedTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemNeedUniqueItemData(this.tableData.needUniqueId, (uint) (this.level + 1));
        if (this.mNextNeedTableData == null)
          this.mNextNeedTableData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemNeedItemData(this.tableData.needId, (uint) (this.level + 1));
      }
      return this.mNextNeedTableData;
    }
  }

  public List<int> atkList
  {
    get
    {
      if (this.mAtkList == null)
        this.mAtkList = this.GetAtkList();
      return this.mAtkList;
    }
  }

  public List<int> defList
  {
    get
    {
      if (this.mDefList == null)
        this.mDefList = this.GetDefList();
      return this.mDefList;
    }
  }

  public int atk => this.atkList[0];

  public int def => this.defList[0];

  public int hp
  {
    get
    {
      if (!this.mHp.HasValue)
        this.mHp = new int?(this.level > 1 ? this.GetGrowParamHp() : (int) this.tableData.baseHp + (int) this.exceedParam.hp);
      return this.mHp.Value;
    }
  }

  public int elemAtk
  {
    get
    {
      if (!this.mElemAtk.HasValue)
        this.mElemAtk = new int?(Mathf.Max(this.atkList.GetRange(1, this.atkList.Count - 1).ToArray()));
      return this.mElemAtk.Value;
    }
  }

  public int elemDef
  {
    get
    {
      if (!this.mElemDef.HasValue)
        this.mElemDef = new int?(Mathf.Max(this.defList.GetRange(1, this.defList.Count - 1).ToArray()));
      return this.mElemDef.Value;
    }
  }

  public EquipItemInfo()
  {
  }

  public EquipItemInfo(EquipItem recv_data) => this.SetValue(recv_data);

  public EquipItemInfo(CharaInfo.EquipItem home_chara_equip_data)
  {
    EquipItem recv = new EquipItem();
    recv.uniqId = "0";
    recv.equipItemId = home_chara_equip_data.eId;
    recv.level = (XorInt) home_chara_equip_data.lv;
    recv.exceed = home_chara_equip_data.exceed;
    recv.price = 0;
    recv.is_locked = 0;
    recv.ability = new List<EquipItem.Ability>();
    int index = 0;
    for (int count = home_chara_equip_data.aIds.Count; index < count; ++index)
      recv.ability.Add(new EquipItem.Ability()
      {
        id = home_chara_equip_data.aIds[index],
        pt = home_chara_equip_data.aPts[index]
      });
    recv.abilityItem = home_chara_equip_data.ai;
    this.SetValue(recv);
  }

  public EquipItemInfo(uint id)
  {
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(id);
    EquipItem recv = new EquipItem();
    recv.uniqId = "0";
    recv.equipItemId = (int) id;
    recv.level = (XorInt) equipItemData.maxLv;
    recv.exceed = 4;
    recv.price = 0;
    recv.is_locked = 0;
    recv.ability = new List<EquipItem.Ability>();
    int index = 0;
    for (int length = equipItemData.fixedAbility.Length; index < length; ++index)
      recv.ability.Add(new EquipItem.Ability()
      {
        id = equipItemData.fixedAbility[index].id,
        pt = 1
      });
    this.SetValue(recv);
  }

  public override void SetValue(EquipItem recv_data)
  {
    ulong result;
    ulong.TryParse(recv_data.uniqId, out result);
    this.uniqueID = result;
    this.tableID = (uint) recv_data.equipItemId;
    this.level = (int) recv_data.level;
    this.exceed = recv_data.exceed;
    this.sellPrice = recv_data.price;
    this.isFavorite = recv_data.is_locked != 0;
    this.UpdateTableData();
    if (this.tableData == null)
    {
      Log.Error(LOG.RESOURCE, "table = null");
    }
    else
    {
      this.exceedParam = this.tableData.GetExceedParam((uint) recv_data.exceed);
      if (this.exceedParam == null)
        this.exceedParam = new EquipItemExceedParamTable.EquipItemExceedParamAll();
      int cnt = 0;
      int num = 0;
      if (this.exceedParam != null && this.exceedParam.ability.Length != 0)
        num += this.exceedParam.ability.Length;
      this.ability = new EquipItemAbility[recv_data.ability.Count + this.GetFixedAbilityCount() + num];
      for (int index = 0; index < this.tableData.fixedAbility.Length; ++index)
      {
        if (!this.tableData.fixedAbility[index].vr)
          this.ability[cnt++] = new EquipItemAbility((uint) this.tableData.fixedAbility[index].id, this.tableData.fixedAbility[index].pt);
      }
      recv_data.ability.ForEach((Action<EquipItem.Ability>) (a => this.ability[cnt++] = new EquipItemAbility((uint) a.id, a.pt)));
      if (num > 0)
      {
        for (int index = 0; index < num; ++index)
          this.ability[cnt++] = new EquipItemAbility((uint) this.exceedParam.ability[index].id, this.exceedParam.ability[index].pt);
      }
      this.abilityItem = recv_data.abilityItem;
    }
  }

  public void SetDefaultData()
  {
    if (this.tableData == null)
      return;
    this.mAtkList = new List<int>();
    this.mDefList = new List<int>();
    this.mElemAtk = new int?();
    this.mElemDef = new int?();
    this.mHp = new int?();
    this.mAtkList.Add((int) this.tableData.baseAtk);
    this.mDefList.Add((int) this.tableData.baseDef);
    this.level = 1;
    this.exceed = 0;
  }

  public List<int> GetAtkList()
  {
    List<int> atkList = new List<int>();
    if (this.level > 1)
    {
      atkList.Add(this.GetGrowParamAtk());
      int[] growParamElemAtk = this.GetGrowParamElemAtk();
      int index = 0;
      for (int length = this.tableData.atkElement.Length; index < length; ++index)
        atkList.Add(growParamElemAtk[index]);
    }
    else
    {
      atkList.Add((int) this.tableData.baseAtk + (int) this.exceedParam.atk);
      int index = 0;
      for (int length = this.tableData.atkElement.Length; index < length; ++index)
        atkList.Add(this.tableData.atkElement[index] + this.exceedParam.atkElement[index]);
    }
    return atkList;
  }

  public List<int> GetDefList()
  {
    List<int> defList = new List<int>();
    if (this.level > 1)
    {
      defList.Add(this.GetGrowParamDef());
      int[] growParamElemDef = this.GetGrowParamElemDef();
      int index = 0;
      for (int length = this.tableData.defElement.Length; index < length; ++index)
      {
        if (this.tableData.isFormer)
          growParamElemDef[index] *= 10;
        defList.Add(growParamElemDef[index]);
      }
    }
    else
    {
      defList.Add((int) this.tableData.baseDef + (int) this.exceedParam.def);
      int index = 0;
      for (int length = this.tableData.defElement.Length; index < length; ++index)
      {
        int num = this.tableData.defElement[index] + this.exceedParam.defElement[index];
        if (this.tableData.isFormer)
          num *= 10;
        defList.Add(num);
      }
    }
    return defList;
  }

  public void UpdateTableData()
  {
    this.tableData = Singleton<EquipItemTable>.I.GetEquipItemData(this.tableID);
    this.mDefList = (List<int>) null;
    this.mAtkList = (List<int>) null;
    this.mElemAtk = new int?();
    this.mElemDef = new int?();
  }

  public static InventoryList<EquipItemInfo, EquipItem> CreateList(List<EquipItem> recv_list)
  {
    InventoryList<EquipItemInfo, EquipItem> list = new InventoryList<EquipItemInfo, EquipItem>();
    recv_list.ForEach((Action<EquipItem>) (o => list.Add(o)));
    return list;
  }

  public int GetElemAtkType()
  {
    return this.level != 1 ? this.tableData.GetElemType(this.GetGrowParamElemAtk()) : this.tableData.GetElemAtkType(this.exceedParam != null ? this.exceedParam.atkElement : (int[]) null);
  }

  public int GetElemAtkTypePriorityToTable()
  {
    return this.tableData.atkElementType != ELEMENT_TYPE.MAX ? (int) this.tableData.atkElementType : this.GetElemAtkType();
  }

  public int GetElemDefType()
  {
    return this.level != 1 ? this.tableData.GetElemType(this.GetGrowParamElemDef()) : this.tableData.GetElemDefType(this.exceedParam != null ? this.exceedParam.defElement : (int[]) null);
  }

  public int GetElemDefTypePriorityToTable()
  {
    return this.tableData.defElementType != ELEMENT_TYPE.MAX ? (int) this.tableData.defElementType : this.GetElemDefType();
  }

  public ELEMENT_TYPE GetTargetElement()
  {
    return !this.tableData.IsWeapon() ? (ELEMENT_TYPE) this.GetElemDefType() : (ELEMENT_TYPE) this.GetElemAtkType();
  }

  public ELEMENT_TYPE GetTargetElementPriorityToTable()
  {
    return !this.tableData.IsWeapon() ? (ELEMENT_TYPE) this.GetElemDefTypePriorityToTable() : (ELEMENT_TYPE) this.GetElemAtkTypePriorityToTable();
  }

  private int GetGrowParamAtk(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowTableData : this.growTableData).GetGrowParamAtk((int) this.tableData.baseAtk) + (int) this.exceedParam.atk;
  }

  private int GetGrowParamDef(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowTableData : this.growTableData).GetGrowParamDef((int) this.tableData.baseDef) + (int) this.exceedParam.def;
  }

  private int GetGrowParamHp(bool is_next_level = false)
  {
    return (is_next_level ? this.nextGrowTableData : this.growTableData).GetGrowParamHp((int) this.tableData.baseHp) + (int) this.exceedParam.hp;
  }

  private int[] GetGrowParamElemAtk(bool is_next_level = false)
  {
    int[] growParamElemAtk1 = (is_next_level ? this.nextGrowTableData : this.growTableData).GetGrowParamElemAtk(this.tableData.atkElement);
    int[] growParamElemAtk2 = new int[this.tableData.atkElement.Length];
    int index = 0;
    for (int length = growParamElemAtk2.Length; index < length; ++index)
      growParamElemAtk2[index] = growParamElemAtk1[index] + this.exceedParam.atkElement[index];
    return growParamElemAtk2;
  }

  private int[] GetGrowParamElemDef(bool is_next_level = false)
  {
    int[] growParamElemDef1 = (is_next_level ? this.nextGrowTableData : this.growTableData).GetGrowParamElemDef(this.tableData.defElement);
    int[] growParamElemDef2 = new int[this.tableData.defElement.Length];
    int index = 0;
    for (int length = growParamElemDef2.Length; index < length; ++index)
      growParamElemDef2[index] = growParamElemDef1[index] + this.exceedParam.defElement[index];
    return growParamElemDef2;
  }

  public int GetMaxSlot()
  {
    int maxSlot = this.tableData.maxSlot;
    if (this.exceed > 0)
    {
      EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = this.tableData.GetExceedParam((uint) this.exceed);
      if (exceedParam != null && exceedParam.skillSlot.Length != 0)
        maxSlot += exceedParam.skillSlot.Length;
    }
    return maxSlot;
  }

  public bool IsExceedSkillSlot(int index) => index >= this.tableData.maxSlot;

  public int GetExceedSkillSlotNo(int index)
  {
    if (!this.IsExceedSkillSlot(index))
      return -1;
    int num1 = index - this.tableData.maxSlot;
    int num2 = 0;
    int num3 = 0;
    for (int exceedCnt = 1; exceedCnt <= this.exceed; ++exceedCnt)
    {
      EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParam(this.tableData.exceedID, (uint) exceedCnt);
      if (equipItemExceedParam == null)
        return -1;
      if (equipItemExceedParam.skillSlot.slotType != SKILL_SLOT_TYPE.NONE)
      {
        if (num2 == num1)
        {
          num3 = (int) equipItemExceedParam.cnt;
          break;
        }
        ++num2;
      }
    }
    return num3 == 0 ? -1 : num3 + 100;
  }

  public int GetExceedSkillIndex(int slotNo)
  {
    if (slotNo < 100)
      return -1;
    int num = slotNo - 100;
    if (num > 4)
      return -1;
    int maxSlot = this.tableData.maxSlot;
    for (int exceedCnt = 1; exceedCnt <= this.exceed; ++exceedCnt)
    {
      EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParam(this.tableData.exceedID, (uint) exceedCnt);
      if (equipItemExceedParam == null)
        return -1;
      if (equipItemExceedParam.skillSlot.slotType != SKILL_SLOT_TYPE.NONE)
      {
        if (exceedCnt == num)
          return maxSlot;
        ++maxSlot;
      }
    }
    return -1;
  }

  public SkillItemInfo GetSkillItem(int index)
  {
    return this.GetSkillItem(index, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
  }

  public SkillItemInfo GetSkillItem(int index, int setNo)
  {
    if (this.GetMaxSlot() <= index)
    {
      Log.Warning($"GetSkillItem :: index out of bounds :: uniqID = {(object) this.uniqueID} : tableID = {(object) this.tableID}");
      return (SkillItemInfo) null;
    }
    SkillItemInfo[] skillInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetSkillInventoryClone();
    SkillItemInfo skill_info = (SkillItemInfo) null;
    Action<SkillItemInfo> action = (Action<SkillItemInfo>) (skill_item =>
    {
      if (skill_info != null)
        return;
      bool flag = false;
      EquipSetSkillData equipSetSkillData = skill_item.equipSetSkill.Find((Predicate<EquipSetSkillData>) (x => x.equipSetNo == setNo));
      if (equipSetSkillData != null && (long) equipSetSkillData.equipItemUniqId == (long) this.uniqueID)
      {
        if (equipSetSkillData.equipSlotNo == index)
          flag = true;
        else if (equipSetSkillData.equipSlotNo == this.GetExceedSkillSlotNo(index))
          flag = true;
      }
      if (!flag)
        return;
      skill_info = skill_item;
    });
    Array.ForEach<SkillItemInfo>(skillInventoryClone, action);
    return skill_info;
  }

  public SkillItemInfo GetUniqueSkillItem(int index)
  {
    if (this.GetMaxSlot() <= index)
    {
      Log.Warning($"GetUniqueSkillItem :: index out of bounds :: uniqID = {(object) this.uniqueID} : tableID = {(object) this.tableID}");
      return (SkillItemInfo) null;
    }
    SkillItemInfo[] skillInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetSkillInventoryClone();
    SkillItemInfo skill_info = (SkillItemInfo) null;
    Action<SkillItemInfo> action = (Action<SkillItemInfo>) (skill_item =>
    {
      if (skill_info != null)
        return;
      bool flag = false;
      EquipSetSkillData uniqueEquipSetSkill = skill_item.uniqueEquipSetSkill;
      if (uniqueEquipSetSkill != null && (long) uniqueEquipSetSkill.equipItemUniqId == (long) this.uniqueID)
      {
        if (uniqueEquipSetSkill.equipSlotNo == index)
          flag = true;
        else if (uniqueEquipSetSkill.equipSlotNo == this.GetExceedSkillSlotNo(index))
          flag = true;
      }
      if (!flag)
        return;
      skill_info = skill_item;
    });
    Array.ForEach<SkillItemInfo>(skillInventoryClone, action);
    return skill_info;
  }

  public AbilityItemInfo GetAbilityItem()
  {
    AbilityItemInfo abilityItem1 = MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll().Find((Predicate<AbilityItemInfo>) (x => (long) x.equipUniqueId == (long) this.uniqueID && x.equipUniqueId > 0UL));
    if (abilityItem1 != null)
      return abilityItem1;
    if (this.abilityItem == null || this.abilityItem.abilityItemId == 0)
      return (AbilityItemInfo) null;
    AbilityItemInfo abilityItem2 = new AbilityItemInfo();
    abilityItem2.SetValue(this.abilityItem);
    return abilityItem2;
  }

  public bool IsLevelMax() => this.level >= this.tableData.maxLv;

  public bool IsExceedMax() => this.tableData.exceedID == 0U || this.exceed >= 4;

  public bool IsLevelAndEvolveMax() => this.IsLevelMax() && !this.tableData.IsEvolve();

  public void GetAttachSkillBuffParam(out int[] atk, out int[] def, out int hp)
  {
    this._GetAttachSkillBuffParam(out atk, out def, out hp);
  }

  private void _GetAttachSkillBuffParam(out int[] atk, out int[] def, out int hp)
  {
    atk = new int[7];
    def = new int[7];
    for (int index = 0; index < 7; ++index)
    {
      atk[index] = 0;
      def[index] = 0;
    }
    hp = 0;
    int index1 = 0;
    for (int maxSlot = this.GetMaxSlot(); index1 < maxSlot; ++index1)
    {
      SkillItemInfo skillItem = this.GetSkillItem(index1);
      if (skillItem != null && Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItem.tableData.growID, skillItem.level, skillItem.exceedCnt) != null)
      {
        hp += skillItem.hp;
        int index2 = 0;
        for (int index3 = 7; index2 < index3; ++index2)
        {
          atk[index2] += skillItem.atkList[index2];
          def[index2] += skillItem.defList[index2];
        }
      }
    }
  }

  public ItemStatus GetEquipSkillParam()
  {
    ItemStatus equipSkillParam = new ItemStatus();
    int[] atk;
    int[] def;
    int hp;
    this.GetAttachSkillBuffParam(out atk, out def, out hp);
    equipSkillParam.atk = atk[0];
    equipSkillParam.def = def[0];
    equipSkillParam.hp = hp;
    int index1 = 0;
    for (int index2 = 6; index1 < index2; ++index1)
    {
      equipSkillParam.elemAtk[index1] = atk[index1 + 1];
      equipSkillParam.elemDef[index1] = def[index1 + 1];
    }
    return equipSkillParam;
  }

  public ItemStatus[] GetEquipTypeSkillParam()
  {
    ItemStatus[] equipTypeSkillParam = new ItemStatus[MonoBehaviourSingleton<StatusManager>.I.ENABLE_EQUIP_TYPE_MAX + 1];
    int index1 = 0;
    for (int length = equipTypeSkillParam.Length; index1 < length; ++index1)
      equipTypeSkillParam[index1] = new ItemStatus();
    int index2 = 0;
    for (int maxSlot = this.GetMaxSlot(); index2 < maxSlot; ++index2)
    {
      SkillItemInfo skillItem = this.GetSkillItem(index2);
      if (skillItem != null)
      {
        ENABLE_EQUIP_TYPE[] values = (ENABLE_EQUIP_TYPE[]) Enum.GetValues(typeof (ENABLE_EQUIP_TYPE));
        int index3 = 0;
        for (int length = values.Length; index3 < length; ++index3)
          equipTypeSkillParam[index3].Add(skillItem.GetEquipTypeSkillParam()[index3]);
      }
    }
    return equipTypeSkillParam;
  }

  public int GetValidAbilityLength()
  {
    if (this.ability == null || this.ability.Length == 0)
      return 0;
    int validAbilityLength = 0;
    int index = 0;
    for (int length = this.ability.Length; index < length; ++index)
    {
      if (this.ability[index].id != 0U)
        ++validAbilityLength;
    }
    return validAbilityLength;
  }

  public EquipItemAbility[] GetValidAbility()
  {
    int validAbilityLength = this.GetValidAbilityLength();
    EquipItemAbility[] validAbility = new EquipItemAbility[validAbilityLength];
    int index1 = 0;
    for (int index2 = 0; index2 < validAbilityLength; ++index2)
    {
      if (this.ability[index2].id != 0U)
      {
        validAbility[index1] = this.ability[index2];
        ++index1;
      }
    }
    return validAbility;
  }

  public int GetValidLotAbility() => this.GetValidAbilityLength() - this.GetFixedAbilityCount();

  public EquipItemAbility[] GetLotteryAbility()
  {
    List<EquipItemAbility> list = new List<EquipItemAbility>((IEnumerable<EquipItemAbility>) this.GetValidAbility());
    Array.ForEach<EquipItem.Ability>(this.tableData.fixedAbility, (Action<EquipItem.Ability>) (fixed_ability =>
    {
      if (fixed_ability == null || fixed_ability.id == 0 || fixed_ability.pt == 0 || fixed_ability.vr)
        return;
      int index = list.FindIndex((Predicate<EquipItemAbility>) (d => (long) d.id == (long) fixed_ability.id && d.ap == fixed_ability.pt));
      if (index == -1)
        return;
      list.RemoveAt(index);
    }));
    Array.ForEach<EquipItem.Ability>(this.exceedParam.ability, (Action<EquipItem.Ability>) (exceed_ability =>
    {
      if (exceed_ability == null || exceed_ability.id == 0 || exceed_ability.pt == 0)
        return;
      int index = list.FindIndex((Predicate<EquipItemAbility>) (d => (long) d.id == (long) exceed_ability.id && d.ap == exceed_ability.pt));
      if (index == -1)
        return;
      list.RemoveAt(index);
    }));
    return list.ToArray();
  }

  public bool IsFixedAbility(int index)
  {
    if (0 > index || this.tableData.fixedAbility.Length <= index)
      return false;
    EquipItem.Ability ability = this.tableData.fixedAbility[index];
    EquipItemAbility equipItemAbility = this.GetValidAbility()[index];
    return (long) ability.id == (long) equipItemAbility.id && ability.pt == equipItemAbility.ap && !ability.vr;
  }

  public int GetFixedAbilityCount()
  {
    int fixedAbilityCount = 0;
    for (int index = 0; index < this.tableData.fixedAbility.Length; ++index)
    {
      if (!this.tableData.fixedAbility[index].vr)
        ++fixedAbilityCount;
    }
    return fixedAbilityCount;
  }

  public bool HasNeedUpdateAbility()
  {
    for (int index1 = 0; index1 < this.ability.Length; ++index1)
    {
      AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(this.ability[index1].id, this.ability[index1].ap);
      if (abilityData != null)
      {
        for (int index2 = 0; index2 < abilityData.info.Length; ++index2)
        {
          if (abilityData.info[index2].IsNeedUpdate())
            return true;
        }
      }
    }
    return false;
  }

  public bool IsActiveAbility()
  {
    for (int index = 0; index < this.ability.Length; ++index)
    {
      AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(this.ability[index].id);
      if (ability != null && !ability.IsActive())
        return false;
    }
    return !this.HasNeedUpdateAbility();
  }
}
