// Decompiled with JetBrains decompiler
// Type: QuestResultDropItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestResultDropItemDetail : GameSection
{
  protected SortCompareData iconData;

  public override void Initialize()
  {
    this.iconData = (SortCompareData) GameSection.GetEventData();
    base.Initialize();
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    if (this.iconData == null)
      return;
    if (this.iconData is ItemSortData)
    {
      this.SetActive((Enum) QuestResultDropItemDetail.UI.OBJ_EQUIP, false);
      this.SetActive((Enum) QuestResultDropItemDetail.UI.OBJ_ITEM, true);
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(this.iconData.GetTableID());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_ITEM_NAME, this.iconData.GetName());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_TEXT, itemData.text);
    }
    else if (this.iconData is EquipItemSortData)
    {
      this.SetActive((Enum) QuestResultDropItemDetail.UI.OBJ_EQUIP, true);
      this.SetActive((Enum) QuestResultDropItemDetail.UI.OBJ_ITEM, false);
      EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(this.iconData.GetTableID());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_ITEM_NAME, equipItemData.name);
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_ATK, equipItemData.baseAtk.ToString());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_DEF, equipItemData.baseDef.ToString());
      int fixedSkillLength = equipItemData.fixedSkillLength;
      QuestResultDropItemDetail.UI[] uiArray = new QuestResultDropItemDetail.UI[3]
      {
        QuestResultDropItemDetail.UI.LBL_SKILL_A,
        QuestResultDropItemDetail.UI.LBL_SKILL_B,
        QuestResultDropItemDetail.UI.LBL_SKILL_C
      };
      for (int index = 0; index < 3; ++index)
      {
        if (index < fixedSkillLength)
        {
          SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(equipItemData.GetSkillSlot(0)[index].skill_id);
          if (skillItemData != null)
          {
            this.SetActive((Enum) uiArray[index], true);
            this.SetLabelText((Enum) uiArray[index], skillItemData.name);
          }
          else
            this.SetActive((Enum) uiArray[index], false);
        }
        else
          this.SetActive((Enum) uiArray[index], false);
      }
    }
    else
    {
      this.SetActive((Enum) QuestResultDropItemDetail.UI.OBJ_EQUIP, true);
      this.SetActive((Enum) QuestResultDropItemDetail.UI.OBJ_ITEM, false);
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(this.iconData.GetTableID());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_ITEM_NAME, skillItemData.name);
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_ATK, skillItemData.baseAtk.ToString());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_DEF, skillItemData.baseDef.ToString());
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_SKILL_A, string.Empty);
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_SKILL_B, string.Empty);
      this.SetLabelText((Enum) QuestResultDropItemDetail.UI.LBL_SKILL_C, string.Empty);
    }
  }

  private enum UI
  {
    LBL_ITEM_NAME,
    OBJ_ITEM,
    LBL_TEXT,
    OBJ_EQUIP,
    LBL_ATK,
    LBL_DEF,
    LBL_SKILL_A,
    LBL_SKILL_B,
    LBL_SKILL_C,
  }
}
