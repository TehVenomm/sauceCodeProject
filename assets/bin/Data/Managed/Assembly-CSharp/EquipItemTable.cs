// Decompiled with JetBrains decompiler
// Type: EquipItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EquipItemTable : Singleton<EquipItemTable>, IDataTable
{
  private UIntKeyTable<EquipItemTable.EquipItemData> equipItemTable;
  private List<EquipItemTable.EquipItemData> equipList;

  public void CreateTable(string csv_table)
  {
    this.equipItemTable = TableUtility.CreateUIntKeyTable<EquipItemTable.EquipItemData>(csv_table, new TableUtility.CallBackUIntKeyReadCSV<EquipItemTable.EquipItemData>(EquipItemTable.EquipItemData.cb), "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId,exAttackType");
  }

  public void CreateTable(string csv_table, TableUtility.Progress progress)
  {
    this.equipItemTable = TableUtility.CreateUIntKeyTable<EquipItemTable.EquipItemData>(csv_table, new TableUtility.CallBackUIntKeyReadCSV<EquipItemTable.EquipItemData>(EquipItemTable.EquipItemData.cb), "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId,exAttackType", progress);
    this.equipItemTable.TrimExcess();
  }

  public void AddTable(string csv_table)
  {
    TableUtility.AddUIntKeyTable<EquipItemTable.EquipItemData>(this.equipItemTable, csv_table, new TableUtility.CallBackUIntKeyReadCSV<EquipItemTable.EquipItemData>(EquipItemTable.EquipItemData.cb), "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId,exAttackType");
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
    this.equipList = new List<EquipItemTable.EquipItemData>(this.equipItemTable.GetCount());
    this.ForEach((Action<EquipItemTable.EquipItemData>) (data =>
    {
      if (!data.CanCollecting())
        return;
      this.equipList.Add(data);
    }));
    this.equipList.Sort((IComparer<EquipItemTable.EquipItemData>) new EquipItemTable.ListCompare());
  }

  public void ForEach(Action<EquipItemTable.EquipItemData> cb) => this.equipItemTable.ForEach(cb);

  public EquipItemTable.EquipItemData GetEquipItemData(uint id)
  {
    if (this.equipItemTable == null)
      return (EquipItemTable.EquipItemData) null;
    EquipItemTable.EquipItemData equipItemData = this.equipItemTable.Get(id);
    if (equipItemData == null)
    {
      Log.TableError((object) this, id);
      equipItemData = new EquipItemTable.EquipItemData();
      equipItemData.name = Log.NON_DATA_NAME;
    }
    return equipItemData;
  }

  public int GetEquipListCount() => this.equipList == null ? 0 : this.equipList.Count;

  public EquipItemTable.EquipItemData GetEquipListData(int index)
  {
    return this.equipList == null || this.equipList.Count <= index ? (EquipItemTable.EquipItemData) null : this.equipList[index];
  }

  public static int GetIdFromIconId(int iconId) => iconId % 100000000;

  public class EquipItemDataUtil
  {
    public int GetElemType(int[] elem)
    {
      if (elem == null || elem.Length == 0)
        return 6;
      bool flag = true;
      int num1 = -1;
      int num2 = 0;
      int index = 0;
      for (int length = elem.Length; index < length; ++index)
      {
        if (num2 <= elem[index] && elem[index] > 0)
        {
          num2 = elem[index];
          num1 = index;
        }
        if (elem[index] == 0 & flag)
          flag = false;
      }
      if (num1 == -1)
        return 6;
      return flag ? -1 : num1;
    }
  }

  public class EquipItemData : EquipItemTable.EquipItemDataUtil
  {
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
    public EquipItemTable.EquipItemData.Obtained obtained;
    public int damageDistanceId;
    public ELEMENT_TYPE atkElementType;
    public ELEMENT_TYPE defElementType;
    public bool isFormer;
    public SP_ATTACK_TYPE spAttackType;
    public int spAttackRate;
    public uint shadowEvolveEquipItemId;
    public uint evolveId;
    public EXTRA_ATTACK_TYPE exAttackType;
    private bool? isEvolve;
    public const string NT = "equipItemId,appVer,type,getType,eventId,name,rarity,modelID0,modelID1,colorAttr,R,G,B,R2,G2,B2,R3,G3,B3,EfID,EfP,EfR,EfG,EfB,iconId,maxLv,growId,needId,needUniqueId,exceedId,shadowEvolveEquipItemId,atk,def,hp,fireAtk,waterAtk,thunderAtk,earthAtk,lightAtk,darkAtk,fireDef,waterDef,thunderDef,earthDef,lightDef,darkDef,skillType_0,skillItemId_0,skillType_1,skillItemId_1,skillType_2,skillItemId_2,skillType_3,skillItemId_3,skillType_4,skillItemId_4,skillType_5,skillItemId_5,skillType_6,skillItemId_6,skillType_7,skillItemId_7,skillType_8,skillItemId_8,abilityId_0,abilityPoint_0,variant_0,abilityId_1,abilityPoint_1,variant_1,abilityId_2,abilityPoint_2,variant_2,price,listId,obtained,damageDistanceId,atkElementType,defElementType,isFormer,spAttackType,spAttackRate,evolveId,exAttackType";

    public string GetExceedParamName(int exceed_cnt)
    {
      if (exceed_cnt == 0)
        return string.Empty;
      EquipItemExceedParamTable.EquipItemExceedParam equipItemExceedParam = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParam(this.exceedID, (uint) exceed_cnt);
      return equipItemExceedParam == null ? string.Empty : equipItemExceedParam.GetExceedParamName();
    }

    public SkillItemTable.SkillSlotData[] GetSkillSlot(int exceed_cnt)
    {
      int length = this._skillSlot.Length;
      SkillItemTable.SkillSlotData[] array = new SkillItemTable.SkillSlotData[length];
      for (int index = 0; index < length; ++index)
        array[index] = this._skillSlot[index];
      EquipItemExceedParamTable.EquipItemExceedParamAll itemExceedParamAll = Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParamAll(this.exceedID, (uint) exceed_cnt);
      if (itemExceedParamAll != null && itemExceedParamAll.skillSlot.Length != 0)
      {
        Array.Resize<SkillItemTable.SkillSlotData>(ref array, length + itemExceedParamAll.skillSlot.Length);
        for (int index = 0; index < itemExceedParamAll.skillSlot.Length; ++index)
          array[length + index] = itemExceedParamAll.skillSlot[index];
      }
      return array;
    }

    public int baseElemAtk => this.atkElement == null ? 0 : Mathf.Max(this.atkElement);

    public int baseElemDef => this.defElement == null ? 0 : Mathf.Max(this.defElement);

    public int GetModelID(int sex) => sex == 0 ? this.modelID0 : this.modelID1;

    public int GetIconID()
    {
      return MonoBehaviourSingleton<UserInfoManager>.IsValid() ? this.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex) : this.GetIconID(0);
    }

    public int GetIconID(int sex)
    {
      uint num = this.id;
      if (this.__iconID > 0)
        num = (uint) this.__iconID;
      return this.modelID0 == this.modelID1 || sex == 0 ? (int) num + 100000000 : (int) num + 200000000;
    }

    public EquipModelTable.Data GetModelData(int sex)
    {
      return Singleton<EquipModelTable>.I.Get(this.type, this.GetModelID(sex));
    }

    public bool IsWeapon()
    {
      return this.type >= EQUIPMENT_TYPE.ONE_HAND_SWORD && this.type <= EQUIPMENT_TYPE.ARROW;
    }

    public bool IsVisual()
    {
      return this.type >= EQUIPMENT_TYPE.VISUAL_ARMOR && this.type <= EQUIPMENT_TYPE.VISUAL_LEG;
    }

    public bool IsEvolve()
    {
      if (!this.isEvolve.HasValue)
        this.isEvolve = new bool?(this.GetEvolveTable() != null);
      bool? isEvolve = this.isEvolve;
      bool flag = true;
      return isEvolve.GetValueOrDefault() == flag & isEvolve.HasValue;
    }

    public bool IsShadow() => this.shadowEvolveEquipItemId > 0U;

    public bool IsEquipableAbilityItem()
    {
      return !this.IsWeapon() && (this.getType == GET_TYPE.PAY || this.IsShadow());
    }

    public EvolveEquipItemTable.EvolveEquipItemData[] GetEvolveTable()
    {
      return Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemData(this.id);
    }

    public EquipItemTable.EquipItemData GetBaseEquipTable()
    {
      EvolveEquipItemTable.EvolveEquipItemData fromEvolveEquipId = Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemDataFromEvolveEquipId(this.id);
      return fromEvolveEquipId != null ? Singleton<EquipItemTable>.I.GetEquipItemData(fromEvolveEquipId.equipBaseItemID) : this;
    }

    public EquipItemTable.EquipItemData GetRootEquipTable()
    {
      EquipItemTable.EquipItemData rootEquipTable = this.GetBaseEquipTable();
      if (rootEquipTable == null)
        return (EquipItemTable.EquipItemData) null;
      while (true)
      {
        EquipItemTable.EquipItemData baseEquipTable = rootEquipTable.GetBaseEquipTable();
        if ((int) baseEquipTable.id != (int) rootEquipTable.id)
          rootEquipTable = baseEquipTable;
        else
          break;
      }
      return rootEquipTable;
    }

    public bool IsRevertable() => this.GetRootLithograph() != null && this.getType == GET_TYPE.PAY;

    public ItemTable.ItemData GetRootLithograph()
    {
      NeedMaterial[] rootMaterials = this.GetRootMaterials();
      if (rootMaterials == null)
        return (ItemTable.ItemData) null;
      ItemTable i = Singleton<ItemTable>.I;
      int length = rootMaterials.Length;
      for (int index = 0; index < length; ++index)
      {
        uint itemId = rootMaterials[index].itemID;
        ItemTable.ItemData itemData = i.GetItemData(itemId);
        if (itemData.type == ITEM_TYPE.LITHOGRAPH)
          return itemData;
      }
      return (ItemTable.ItemData) null;
    }

    public NeedMaterial[] GetRootMaterials()
    {
      uint id = this.GetRootEquipTable().id;
      return Singleton<CreateEquipItemTable>.I.GetCreateItemDataByEquipItem(id)?.needMaterial;
    }

    public EquipItemTable.EquipItemData GetShadowEvolveEquipTable()
    {
      return !this.IsShadow() ? (EquipItemTable.EquipItemData) null : Singleton<EquipItemTable>.I.GetEquipItemData(this.shadowEvolveEquipItemId);
    }

    public EvolveEquipItemTable.EvolveEquipItemData GetEvolveTable(uint id)
    {
      EvolveEquipItemTable.EvolveEquipItemData evolveTable = (EvolveEquipItemTable.EvolveEquipItemData) null;
      EvolveEquipItemTable.EvolveEquipItemData[] evolveEquipItemData = Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemData(id);
      if (evolveEquipItemData != null)
      {
        int index = 0;
        for (int length = evolveEquipItemData.Length; index < length; ++index)
        {
          if ((int) evolveEquipItemData[index].id == (int) id)
          {
            evolveTable = evolveEquipItemData[index];
            break;
          }
        }
      }
      return evolveTable;
    }

    public EquipItemStatus GetDefaultSkillBuffParam()
    {
      EquipItemStatus defaultSkillBuffParam = new EquipItemStatus();
      int[] atk;
      int[] def;
      int hp;
      this._GetDefaultSkillBuffParam(out atk, out def, out hp);
      defaultSkillBuffParam.atk = atk[0];
      defaultSkillBuffParam.def = def[0];
      defaultSkillBuffParam.hp = hp;
      int index1 = 0;
      for (int index2 = 6; index1 < index2; ++index1)
      {
        defaultSkillBuffParam.elemAtk[index1] = atk[index1 + 1];
        defaultSkillBuffParam.elemDef[index1] = def[index1 + 1];
      }
      return defaultSkillBuffParam;
    }

    private void _GetDefaultSkillBuffParam(out int[] atk, out int[] def, out int hp)
    {
      atk = new int[7];
      def = new int[7];
      int index1 = 0;
      for (int index2 = 7; index1 < index2; ++index1)
      {
        atk[index1] = 0;
        def[index1] = 0;
      }
      hp = 0;
      int index3 = 0;
      for (int maxSlot = this.maxSlot; index3 < maxSlot; ++index3)
      {
        SkillItemTable.SkillSlotData skillSlotData = this.GetSkillSlot(0)[index3];
        if (skillSlotData != null && skillSlotData.skill_id != 0U)
        {
          SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(skillSlotData.skill_id);
          if (skillItemData != null)
          {
            GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, 1, 0);
            if (growSkillItemData != null)
            {
              atk[0] += growSkillItemData.GetGrowParamAtk((int) skillItemData.baseAtk);
              def[0] += growSkillItemData.GetGrowParamDef((int) skillItemData.baseDef);
              hp += growSkillItemData.GetGrowParamHp((int) skillItemData.baseHp);
              int[] growParamElemAtk = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
              int[] growParamElemDef = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
              int index4 = 1;
              for (int index5 = 7; index4 < index5; ++index4)
              {
                atk[index4] += growParamElemAtk[index4 - 1];
                def[index4] += growParamElemDef[index4 - 1];
              }
            }
          }
        }
      }
    }

    public int GetElemAtkType(int[] exceed_elem = null)
    {
      if (exceed_elem == null)
        return this.GetElemType(this.atkElement);
      int[] elem = new int[this.atkElement.Length];
      int index = 0;
      for (int length = elem.Length; index < length; ++index)
        elem[index] = this.atkElement[index] + exceed_elem[index];
      return this.GetElemType(elem);
    }

    public int GetElemAtkTypePriorityToTable(int[] exceed_elem = null)
    {
      return this.atkElementType != ELEMENT_TYPE.MAX ? (int) this.atkElementType : this.GetElemAtkType(exceed_elem);
    }

    public int GetElemDefType(int[] exceed_elem = null)
    {
      if (exceed_elem == null)
        return this.GetElemType(this.defElement);
      int[] elem = new int[this.defElement.Length];
      int index = 0;
      for (int length = elem.Length; index < length; ++index)
        elem[index] = this.defElement[index] + exceed_elem[index];
      return this.GetElemType(elem);
    }

    public int GetElemDefTypePriorityToTable(int[] exceed_elem = null)
    {
      return this.defElementType != ELEMENT_TYPE.MAX ? (int) this.defElementType : this.GetElemDefType(exceed_elem);
    }

    public ELEMENT_TYPE GetTargetElement(int exceed_cnt)
    {
      bool flag = this.IsWeapon();
      if (exceed_cnt > 0)
      {
        EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = this.GetExceedParam((uint) exceed_cnt);
        if (exceedParam != null)
          return !flag ? (ELEMENT_TYPE) exceedParam.GetElemDefType(this.defElement) : (ELEMENT_TYPE) exceedParam.GetElemAtkType(this.atkElement);
      }
      return !flag ? (ELEMENT_TYPE) this.GetElemDefType() : (ELEMENT_TYPE) this.GetElemAtkType();
    }

    public ELEMENT_TYPE GetTargetElementPriorityToTable()
    {
      return !this.IsWeapon() ? (ELEMENT_TYPE) this.GetElemDefTypePriorityToTable() : (ELEMENT_TYPE) this.GetElemAtkTypePriorityToTable();
    }

    public void GetMaxAtk(out int _atk, out int _elem_atk, out ELEMENT_TYPE _element)
    {
      GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(this.growID, (uint) this.maxLv);
      _atk = growEquipItemData.GetGrowParamAtk((int) this.baseAtk);
      int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(this.atkElement);
      _elem_atk = 0;
      _element = ELEMENT_TYPE.MAX;
      int index = 0;
      for (int length = growParamElemAtk.Length; index < length; ++index)
      {
        if (_elem_atk < growParamElemAtk[index])
        {
          _elem_atk = growParamElemAtk[index];
          _element = (ELEMENT_TYPE) index;
        }
      }
    }

    public void GetMaxDef(out int _def, out int _elem_def, out ELEMENT_TYPE _element)
    {
      GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(this.growID, (uint) this.maxLv);
      _def = growEquipItemData.GetGrowParamDef((int) this.baseDef);
      int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(this.defElement);
      _elem_def = 0;
      _element = ELEMENT_TYPE.MAX;
      int index = 0;
      for (int length = growParamElemDef.Length; index < length; ++index)
      {
        if (_elem_def < growParamElemDef[index])
        {
          _elem_def = growParamElemDef[index];
          _element = (ELEMENT_TYPE) index;
        }
      }
    }

    public bool IsEnableNowApplicationVersion() => AppMain.CheckApplicationVersion(this.appVer);

    public bool CanCollecting()
    {
      return this.obtained.category.Length > 0 && this.obtained.flag >= 0 && this.obtained.flag < 64 /*0x40*/ && !this.IsShadow();
    }

    public EquipItemExceedParamTable.EquipItemExceedParamAll GetExceedParam(uint exceed)
    {
      return Singleton<EquipItemExceedParamTable>.I.GetEquipItemExceedParamAll(this.exceedID, exceed);
    }

    public static bool cb(CSVReader csv_reader, EquipItemTable.EquipItemData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.appVer);
      csv_reader.Pop<EQUIPMENT_TYPE>(ref data.type);
      csv_reader.Pop<GET_TYPE>(ref data.getType);
      csv_reader.Pop(ref data.eventId);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop<RARITY_TYPE>(ref data.rarity);
      csv_reader.Pop(ref data.modelID0);
      data.modelID1 = data.modelID0;
      csv_reader.Pop(ref data.modelID1);
      string empty1 = string.Empty;
      int id = -1;
      csv_reader.Pop(ref empty1);
      if (empty1.Length > 1)
        id = (int) Enum.Parse(typeof (ELEMENT_TYPE), empty1);
      if (!(bool) csv_reader.PopColor24(ref data.modelColor0))
        data.modelColor0 = id == -1 ? NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.modelBaseColor) : NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetModelElementColor(id));
      csv_reader.PopColor24(ref data.modelColor1);
      if (!(bool) csv_reader.PopColor24(ref data.modelColor2))
        data.modelColor2 = id == -1 ? NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.modelBaseColor2) : NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetModelElementColor2(id));
      csv_reader.Pop(ref data.effectID);
      data.effectParam = 1f;
      csv_reader.Pop(ref data.effectParam);
      if (!(bool) csv_reader.PopColor24(ref data.effectColor))
        data.effectColor = id == -1 ? NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.modelBaseColor) : NGUIMath.ColorToInt(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.GetModelElementColor(id));
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
      for (int index = 0; index < 6; ++index)
        csv_reader.Pop(ref data.atkElement[index]);
      for (int index = 0; index < 6; ++index)
        csv_reader.Pop(ref data.defElement[index]);
      List<SkillItemTable.SkillSlotData> skillSlotDataList = new List<SkillItemTable.SkillSlotData>();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < 9; ++index)
      {
        string empty2 = string.Empty;
        uint num3 = 0;
        csv_reader.Pop(ref empty2);
        csv_reader.Pop(ref num3);
        if (!string.IsNullOrEmpty(empty2))
        {
          SkillItemTable.SkillSlotData skillSlotData = new SkillItemTable.SkillSlotData();
          skillSlotData.slotType = (SKILL_SLOT_TYPE) Enum.Parse(typeof (SKILL_SLOT_TYPE), empty2);
          if (skillSlotData.slotType != SKILL_SLOT_TYPE.NONE)
          {
            skillSlotData.skill_id = num3;
            skillSlotDataList.Add(skillSlotData);
            ++num2;
            if (num3 != 0U)
              ++num1;
          }
        }
      }
      data._skillSlot = skillSlotDataList.ToArray();
      data.fixedSkillLength = num1;
      data.maxSlot = num2;
      int[] numArray1 = new int[3];
      int[] numArray2 = new int[3];
      int[] numArray3 = new int[3];
      int length = 0;
      for (int index = 0; index < 3; ++index)
      {
        csv_reader.Pop(ref numArray1[index]);
        csv_reader.Pop(ref numArray2[index]);
        csv_reader.Pop(ref numArray3[index]);
        if (numArray1[index] != 0 && numArray2[index] != 0)
          ++length;
      }
      data.fixedAbility = new EquipItem.Ability[length];
      for (int index = 0; index < length; ++index)
      {
        data.fixedAbility[index] = new EquipItem.Ability();
        data.fixedAbility[index].id = numArray1[index];
        data.fixedAbility[index].pt = numArray2[index];
        data.fixedAbility[index].vr = 0 < numArray3[index];
      }
      csv_reader.Pop(ref data.sale);
      csv_reader.Pop(ref data.listId);
      string obtained = "";
      csv_reader.Pop(ref obtained);
      data.obtained = new EquipItemTable.EquipItemData.Obtained(obtained);
      if (!(bool) csv_reader.Pop(ref data.damageDistanceId))
        data.damageDistanceId = data.type != EQUIPMENT_TYPE.ARROW ? -1 : 0;
      csv_reader.PopEnum<ELEMENT_TYPE>(ref data.atkElementType, ELEMENT_TYPE.MAX);
      csv_reader.PopEnum<ELEMENT_TYPE>(ref data.defElementType, ELEMENT_TYPE.MAX);
      csv_reader.Pop(ref data.isFormer);
      csv_reader.PopEnum<SP_ATTACK_TYPE>(ref data.spAttackType, SP_ATTACK_TYPE.NONE);
      csv_reader.Pop(ref data.spAttackRate);
      csv_reader.Pop(ref data.evolveId);
      csv_reader.PopEnum<EXTRA_ATTACK_TYPE>(ref data.exAttackType, EXTRA_ATTACK_TYPE.NONE);
      return true;
    }

    public class Obtained
    {
      public string category = "";
      public int flag = -1;
      private const int CATEGORY_MAX = 64 /*0x40*/;
      private const int ALPHABET_MAX = 26;

      public Obtained(string obtained)
      {
        if (obtained.Length == 0)
          return;
        string s = (string) null;
        int num = 0;
        for (int length = obtained.Length; num < length; ++num)
        {
          if (char.IsNumber(obtained[num]))
          {
            this.category = obtained.Substring(0, num).ToUpper();
            s = obtained.Substring(num);
            break;
          }
        }
        if (s == null)
          return;
        int.TryParse(s, out this.flag);
      }

      public int GetSequenceNumber()
      {
        int flag = this.flag;
        if (!string.IsNullOrEmpty(this.category))
        {
          string upper = this.category.ToUpper();
          int index1 = 0;
          for (int length = upper.Length; index1 < length; ++index1)
          {
            int num1 = (int) upper[index1] - 65;
            int num2 = length - index1;
            if (num2 > 0)
            {
              int num3 = 64 /*0x40*/;
              int num4 = 0;
              for (int index2 = num2 - 1; num4 < index2; ++num4)
                num3 *= 26;
              if (num3 > 64 /*0x40*/)
                ++num1;
              flag += num1 * num3;
            }
            else
              flag += num1;
          }
        }
        return flag;
      }
    }
  }

  public class ListCompare : IComparer<EquipItemTable.EquipItemData>
  {
    public int Compare(EquipItemTable.EquipItemData data1, EquipItemTable.EquipItemData data2)
    {
      return data1.listId < data2.listId ? -1 : 1;
    }
  }
}
