// Decompiled with JetBrains decompiler
// Type: ItemDetailEquipMaxParamDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemDetailEquipMaxParamDialog : ItemDetailEquipDialog
{
  private List<List<EquipItemTable.EquipItemData>> allEquipData;
  private int currentEvolveStage;
  private int currentEvolveIndex;
  private Transform root;
  private uint materialId;

  public override void Initialize()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    EquipItemTable.EquipItemData equipItemData = eventData[1] as EquipItemTable.EquipItemData;
    List<EquipItemTable.EquipItemData> tabledata = new List<EquipItemTable.EquipItemData>();
    tabledata.Add(equipItemData);
    this.allEquipData = new List<List<EquipItemTable.EquipItemData>>();
    this.allEquipData.Add(tabledata);
    this.AddNextEvolveDataRecursive(tabledata);
    this.currentEvolveStage = 0;
    this.currentEvolveIndex = 0;
    this.root = this.SetPrefab(this.collectUI, nameof (ItemDetailEquipMaxParamDialog));
    this.materialId = (uint) eventData[2];
    base.Initialize();
  }

  protected override void EquipTableParam(EquipItemTable.EquipItemData table_data)
  {
    base.EquipTableParam(table_data);
    GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(table_data.growID, (uint) table_data.maxLv);
    int growParamAtk = growEquipItemData.GetGrowParamAtk((int) table_data.baseAtk);
    int growParamDef = growEquipItemData.GetGrowParamDef((int) table_data.baseDef);
    int num1 = Mathf.Max(growEquipItemData.GetGrowParamElemAtk(table_data.atkElement));
    int num2 = Mathf.Max(growEquipItemData.GetGrowParamElemDef(table_data.defElement));
    int growParamHp = growEquipItemData.GetGrowParamHp((int) table_data.baseHp);
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_LV, true);
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.STR_ONLY_VISUAL, false);
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_LV_NOW, table_data.maxLv.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ATK, growParamAtk.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM, num1.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_DEF, growParamDef.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_ELEM_DEF, num2.ToString());
    this.SetLabelText(this.detailBase, (Enum) ItemDetailEquip.UI.LBL_HP, growParamHp.ToString());
    this.SetActive(this.detailBase, (Enum) ItemDetailEquip.UI.OBJ_FAVORITE_ROOT, false);
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.DisableMagiSlotButton();
    this.UpdatePaging();
  }

  private void DisableMagiSlotButton()
  {
    Transform t = this.GetCtrl((Enum) ItemDetailEquip.UI.OBJ_SKILL_BUTTON_ROOT).Find("SkillIconButton");
    this.SetEnabled<UIButton>(t, false);
    ((Collider) ((Component) t).GetComponent<BoxCollider>()).enabled = false;
  }

  private void UpdatePaging()
  {
    bool is_visible1 = this.allEquipData.Count + this.allEquipData[this.currentEvolveStage].Count >= 2;
    this.SetActive((Enum) ItemDetailEquip.UI.OBJ_EVOLVE_SELECT, true);
    this.SetActive((Enum) ItemDetailEquip.UI.OBJ_ARROW_BTN_ROOT, is_visible1);
    if (this.currentEvolveStage == 0)
    {
      this.SetActive((Enum) ItemDetailEquip.UI.LBL_EVOLVE_NORMAL, true);
      this.SetLabelText((Enum) ItemDetailEquip.UI.LBL_EVOLVE_NORMAL, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 13U));
      this.SetActive((Enum) ItemDetailEquip.UI.LBL_EVOLVE_ATTRIBUTE, false);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_EVOLVE_ELEM, false);
    }
    else
    {
      int elementPriorityToTable = (int) this.GetCurrentEquipItemData().GetTargetElementPriorityToTable();
      bool is_visible2 = elementPriorityToTable == 6;
      this.SetActive((Enum) ItemDetailEquip.UI.LBL_EVOLVE_NORMAL, is_visible2);
      this.SetActive((Enum) ItemDetailEquip.UI.LBL_EVOLVE_ATTRIBUTE, !is_visible2);
      this.SetActive((Enum) ItemDetailEquip.UI.SPR_EVOLVE_ELEM, !is_visible2);
      if (is_visible2)
      {
        this.SetLabelText((Enum) ItemDetailEquip.UI.LBL_EVOLVE_NORMAL, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 14U), (object) this.currentEvolveStage.ToString()));
      }
      else
      {
        this.SetLabelText((Enum) ItemDetailEquip.UI.LBL_EVOLVE_ATTRIBUTE, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 15U), (object) this.currentEvolveStage.ToString()));
        this.SetElementSprite((Enum) ItemDetailEquip.UI.SPR_EVOLVE_ELEM, elementPriorityToTable);
      }
    }
  }

  private void OnQuery_NEXT_EVOLVE()
  {
    ++this.currentEvolveIndex;
    if (this.currentEvolveIndex >= this.allEquipData[this.currentEvolveStage].Count)
    {
      this.currentEvolveIndex = 0;
      ++this.currentEvolveStage;
    }
    if (this.currentEvolveStage >= this.allEquipData.Count)
      this.currentEvolveStage = 0;
    this.UpdateDetail(this.GetCurrentEquipItemData());
  }

  private void OnQuery_PRE_EVOLVE()
  {
    bool flag = false;
    --this.currentEvolveIndex;
    if (this.currentEvolveIndex < 0)
    {
      this.currentEvolveIndex = 0;
      flag = true;
      --this.currentEvolveStage;
    }
    if (this.currentEvolveStage < 0)
      this.currentEvolveStage = this.allEquipData.Count - 1;
    if (flag)
      this.currentEvolveIndex = this.allEquipData[this.currentEvolveStage].Count - 1;
    this.UpdateDetail(this.GetCurrentEquipItemData());
  }

  private void OnQuery_LOTTERY_LIST()
  {
    CreateEquipItemTable.CreateEquipItemData createEquipItemByPart = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemByPart(this.materialId, this.GetCurrentEquipItemData().type);
    CreateEquipItemTable.CreateEquipItemData event_data = new CreateEquipItemTable.CreateEquipItemData();
    if (event_data == null)
    {
      GameSection.StopEvent();
    }
    else
    {
      event_data.id = createEquipItemByPart.id != 20160100U || this.currentEvolveStage != 3 || this.currentEvolveIndex != 0 ? (createEquipItemByPart.id != 21160100U || this.currentEvolveStage != 3 || this.currentEvolveIndex != 0 ? (createEquipItemByPart.id != 22160100U || this.currentEvolveStage != 3 || this.currentEvolveIndex != 0 ? (createEquipItemByPart.id != 23160100U || this.currentEvolveStage != 3 || this.currentEvolveIndex != 0 ? (createEquipItemByPart.id != 24160100U || this.currentEvolveStage != 3 || this.currentEvolveIndex != 0 ? createEquipItemByPart.id : 84160110U) : 83160110U) : 82160110U) : 81160110U) : 80160111U;
      GameSection.SetEventData((object) event_data);
    }
  }

  private EquipItemTable.EquipItemData GetCurrentEquipItemData()
  {
    return this.allEquipData[this.currentEvolveStage][this.currentEvolveIndex];
  }

  private void UpdateDetail(EquipItemTable.EquipItemData data)
  {
    this.detailItemData = (object) data;
    this.RefreshUI();
  }

  private EquipItemTable.EquipItemData[] GetNextEvolveData(EquipItemTable.EquipItemData tableData)
  {
    EvolveEquipItemTable.EvolveEquipItemData[] evolveTable = tableData.GetEvolveTable();
    if (evolveTable == null)
      return (EquipItemTable.EquipItemData[]) null;
    EquipItemTable.EquipItemData[] nextEvolveData = new EquipItemTable.EquipItemData[evolveTable.Length];
    for (int index = 0; index < evolveTable.Length; ++index)
      nextEvolveData[index] = Singleton<EquipItemTable>.I.GetEquipItemData(evolveTable[index].equipEvolveItemID);
    return nextEvolveData;
  }

  private void AddNextEvolveDataRecursive(List<EquipItemTable.EquipItemData> tabledata)
  {
    if (tabledata == null || tabledata.Count <= 0)
      return;
    List<EquipItemTable.EquipItemData> tabledata1 = new List<EquipItemTable.EquipItemData>();
    HashSet<uint> uintSet = new HashSet<uint>();
    int index1 = 0;
    for (int count = tabledata.Count; index1 < count; ++index1)
    {
      EquipItemTable.EquipItemData[] nextEvolveData = this.GetNextEvolveData(tabledata[index1]);
      if (nextEvolveData != null)
      {
        int index2 = 0;
        for (int length = nextEvolveData.Length; index2 < length; ++index2)
        {
          if (uintSet.Add(nextEvolveData[index2].id))
            tabledata1.Add(nextEvolveData[index2]);
        }
      }
    }
    if (tabledata1.Count >= 1)
      this.allEquipData.Add(tabledata1);
    this.AddNextEvolveDataRecursive(tabledata1);
  }
}
