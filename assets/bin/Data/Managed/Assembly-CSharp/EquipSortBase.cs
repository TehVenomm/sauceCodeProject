// Decompiled with JetBrains decompiler
// Type: EquipSortBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class EquipSortBase : SortBase
{
  private EquipSortBase.UI[] rarityButton;
  private EquipSortBase.UI?[] typeButton;
  private EquipSortBase.UI?[] typeSkillButton;
  private EquipSortBase.UI?[] typeSkillGrayButton;
  private EquipSortBase.UI?[] elementButton;
  private EquipSortBase.UI?[] requirementButton;
  private EquipSortBase.UI[] ascButton;
  private EquipSortBase.UI[] equipChangeSortBaseFilterButton;
  protected int visible_type_flag_skill;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.SetActive((Enum) EquipSortBase.UI.OBJ_TYPE, false);
    if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.STORAGE_SKILL || this.sortOrder.dialogType == SortBase.DIALOG_TYPE.SKILL)
    {
      this.SetActive((Enum) EquipSortBase.UI.OBJ_TYPE_SKILL, true);
      this.SetActive((Enum) EquipSortBase.UI.OBJ_TYPE_EQUIP, false);
      UIWidget component = this.GetComponent<UIWidget>((Enum) EquipSortBase.UI.OBJ_FRAME);
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.height = 820;
        ((Component) component).transform.localPosition = new Vector3(0.0f, 410f, 0.0f);
        component.UpdateAnchors();
      }
    }
    else
    {
      this.SetActive((Enum) EquipSortBase.UI.OBJ_TYPE_SKILL, false);
      this.SetActive((Enum) EquipSortBase.UI.OBJ_TYPE_EQUIP, true);
      if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.WEAPON || this.sortOrder.dialogType == SortBase.DIALOG_TYPE.ARMOR || this.sortOrder.dialogType == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON || this.sortOrder.dialogType == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR)
      {
        int num1 = 0;
        if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON || this.sortOrder.dialogType == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR)
        {
          int event_data = 0;
          for (int length = this.equipChangeSortBaseFilterButton.Length; event_data < length; ++event_data)
          {
            bool flag = (this.sortOrder.equipFilter & 1 << event_data) != 0;
            this.SetEvent((Enum) this.equipChangeSortBaseFilterButton[event_data], "EQUIP_FILTER", event_data);
            this.SetToggle(this.GetCtrl((Enum) this.equipChangeSortBaseFilterButton[event_data]).parent, flag);
          }
          num1 = 95;
          this.SetActive((Enum) EquipSortBase.UI.OBJ_TYPE, true);
        }
        int num2 = num1 / 2;
        UIWidget component = this.GetComponent<UIWidget>((Enum) EquipSortBase.UI.Frame);
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.height = 703 + num2;
          ((Component) component).transform.localPosition = new Vector3(0.0f, 349f + (float) num2, 0.0f);
          component.UpdateAnchors();
        }
      }
      else
      {
        UIWidget component = this.GetComponent<UIWidget>((Enum) EquipSortBase.UI.OBJ_FRAME);
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.height = 770;
          ((Component) component).transform.localPosition = new Vector3(0.0f, 385f, 0.0f);
          component.UpdateAnchors();
        }
      }
    }
    int event_data1 = 0;
    for (int length = this.rarityButton.Length; event_data1 < length; ++event_data1)
    {
      bool flag = (this.sortOrder.rarity & 1 << event_data1) != 0;
      this.SetEvent((Enum) this.rarityButton[event_data1], "RARITY", event_data1);
      this.SetToggle(this.GetCtrl((Enum) this.rarityButton[event_data1]).parent, flag);
    }
    EquipSortBase.UI?[] nullableArray1 = this.typeButton;
    EquipSortBase.UI?[] nullableArray2 = (EquipSortBase.UI?[]) null;
    EquipSortBase.UI?[] elementButton = this.elementButton;
    string event_name = "TYPE";
    int num3;
    int num4;
    switch (this.sortOrder.dialogType)
    {
      case SortBase.DIALOG_TYPE.WEAPON:
      case SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON:
        num3 = 31 /*0x1F*/;
        num4 = 25020;
        nullableArray1 = (EquipSortBase.UI?[]) null;
        break;
      case SortBase.DIALOG_TYPE.SKILL:
      case SortBase.DIALOG_TYPE.STORAGE_SKILL:
        num3 = !(MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "SmithGrowSkillSelectMaterial") ? 135 : this.visible_type_flag_skill;
        num4 = 69884;
        event_name = "SKILL_TYPE";
        if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.STORAGE_SKILL)
        {
          nullableArray1 = this.typeSkillButton;
          nullableArray2 = this.typeSkillGrayButton;
          break;
        }
        nullableArray1 = (EquipSortBase.UI?[]) null;
        nullableArray2 = (EquipSortBase.UI?[]) null;
        break;
      case SortBase.DIALOG_TYPE.STORAGE_EQUIP:
        num3 = 511 /*0x01FF*/;
        num4 = 8604;
        break;
      default:
        num3 = 0;
        num4 = 41436;
        nullableArray1 = (EquipSortBase.UI?[]) null;
        break;
    }
    if (nullableArray1 != null)
    {
      int event_data2 = 0;
      for (int length = nullableArray1.Length; event_data2 < length; ++event_data2)
      {
        if (nullableArray1[event_data2].HasValue)
        {
          if ((num3 & 1 << event_data2) != 0)
          {
            bool flag = (this.sortOrder.type & 1 << event_data2) != 0;
            this.SetEvent((Enum) (ValueType) nullableArray1[event_data2], event_name, event_data2);
            this.SetToggle(this.GetCtrl((Enum) (ValueType) nullableArray1[event_data2]).parent, flag);
          }
          else if (MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory() == "SmithGrowSkillSelectMaterial")
          {
            if (nullableArray2 != null && event_data2 < nullableArray2.Length && nullableArray2[event_data2].HasValue)
            {
              this.SetActive((Enum) (ValueType) nullableArray1[event_data2], false);
              this.SetActive((Enum) (ValueType) nullableArray2[event_data2], true);
            }
          }
          else
          {
            this.SetActive((Enum) (ValueType) nullableArray1[event_data2], false);
            this.SetActive((Enum) (ValueType) nullableArray2[event_data2], false);
          }
        }
      }
    }
    if (elementButton != null)
    {
      int event_data3 = 0;
      for (int length = elementButton.Length; event_data3 < length; ++event_data3)
      {
        bool flag = (this.sortOrder.element & 1 << event_data3) != 0;
        this.SetEvent((Enum) (ValueType) elementButton[event_data3], "ELEMENT", event_data3);
        this.SetToggle(this.GetCtrl((Enum) (ValueType) elementButton[event_data3]).parent, flag);
      }
    }
    EquipSortBase.UI? label_enum = new EquipSortBase.UI?();
    int index = 0;
    for (int length = this.requirementButton.Length; index < length; ++index)
    {
      if (this.requirementButton[index].HasValue)
      {
        int event_data4 = 1 << index;
        if ((event_data4 & num4) != 0)
        {
          bool flag = this.sortOrder.requirement == (SortBase.SORT_REQUIREMENT) event_data4;
          this.SetEvent((Enum) (ValueType) this.requirementButton[index], "REQUIREMENT", event_data4);
          this.SetToggle((Enum) (ValueType) this.requirementButton[index], flag);
          label_enum = this.requirementButton[index];
        }
        else
          this.SetActive((Enum) (ValueType) this.requirementButton[index], false);
      }
    }
    if (label_enum.HasValue)
    {
      this.GetComponent<UIGrid>((Enum) EquipSortBase.UI.GRD_REQUIREMENT).Reposition();
      this.GetCtrl((Enum) EquipSortBase.UI.OBJ_HEIGHT_ANCHOR).position = this.GetCtrl((Enum) (ValueType) label_enum).position;
    }
    int event_data5 = 0;
    for (int length = this.ascButton.Length; event_data5 < length; ++event_data5)
    {
      bool flag = false;
      if (event_data5 == 0 && this.sortOrder.orderTypeAsc || event_data5 == 1 && !this.sortOrder.orderTypeAsc)
        flag = true;
      this.SetEvent((Enum) this.ascButton[event_data5], "ORDER_TYPE", event_data5);
      this.SetToggle((Enum) this.ascButton[event_data5], flag);
    }
  }

  protected void OnQuery_RARITY()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Rarity(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) this.rarityButton[_index]).parent, _is_enable);
  }

  protected void OnQuery_TYPE()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Type(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) (ValueType) this.typeButton[_index]).parent, _is_enable);
  }

  protected void OnQuery_SKILL_TYPE()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Type(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) (ValueType) this.typeSkillButton[_index]).parent, _is_enable);
  }

  protected void OnQuery_ELEMENT()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Element(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) (ValueType) this.elementButton[_index]).parent, _is_enable);
  }

  protected void OnQuery_EQUIP_FILTER()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_EquipFilter(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) this.equipChangeSortBaseFilterButton[_index]).parent, _is_enable);
  }

  public EquipSortBase()
  {
    EquipSortBase.UI?[] nullableArray1 = new EquipSortBase.UI?[11];
    nullableArray1[0] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_ATTACK);
    nullableArray1[1] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_SUPPORT);
    nullableArray1[2] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_HEAL);
    nullableArray1[7] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_PASSIVE);
    nullableArray1[10] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_GROW);
    this.typeSkillButton = nullableArray1;
    EquipSortBase.UI?[] nullableArray2 = new EquipSortBase.UI?[11];
    nullableArray2[0] = new EquipSortBase.UI?(EquipSortBase.UI.SPR_ATTACK_GRAY);
    nullableArray2[1] = new EquipSortBase.UI?(EquipSortBase.UI.SPR_SUPPORT_GRAY);
    nullableArray2[2] = new EquipSortBase.UI?(EquipSortBase.UI.SPR_HEAL_GRAY);
    nullableArray2[7] = new EquipSortBase.UI?(EquipSortBase.UI.SPR_PASSIVE_GRAY);
    nullableArray2[10] = new EquipSortBase.UI?(EquipSortBase.UI.SPR_GROW_GRAY);
    this.typeSkillGrayButton = nullableArray2;
    this.elementButton = new EquipSortBase.UI?[7]
    {
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_FIRE),
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_WATER),
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_THUNDER),
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_SOIL),
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_LIGHT),
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_DARK),
      new EquipSortBase.UI?(EquipSortBase.UI.BTN_NO_ELEMENT)
    };
    EquipSortBase.UI?[] nullableArray3 = new EquipSortBase.UI?[17];
    nullableArray3[1] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_NUM);
    nullableArray3[2] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_GET);
    nullableArray3[3] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_RARITY);
    nullableArray3[4] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_LEVEL);
    nullableArray3[5] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_ATK);
    nullableArray3[6] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_DEF);
    nullableArray3[7] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_SELL);
    nullableArray3[8] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_SOCKET);
    nullableArray3[9] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_PRICE);
    nullableArray3[12] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_HP);
    nullableArray3[13] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_ELEMENT);
    nullableArray3[14] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_ELEM_ATK);
    nullableArray3[15] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_ELEM_DEF);
    nullableArray3[16 /*0x10*/] = new EquipSortBase.UI?(EquipSortBase.UI.BTN_SKILL_TYPE);
    this.requirementButton = nullableArray3;
    this.ascButton = new EquipSortBase.UI[2]
    {
      EquipSortBase.UI.BTN_ASC,
      EquipSortBase.UI.BTN_DESC
    };
    this.equipChangeSortBaseFilterButton = new EquipSortBase.UI[2]
    {
      EquipSortBase.UI.BTN_EQUIP_PAY,
      EquipSortBase.UI.BTN_EQUIP_NO_PAY
    };
    this.visible_type_flag_skill = 1159;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  protected enum UI
  {
    LBL_FAVORITE,
    BTN_FAVORITE,
    OBJ_FRAME,
    LBL_ONLY_EQUIP,
    BTN_ONLY_EQUIP,
    BTN_N,
    BTN_HN,
    BTN_R,
    BTN_HR,
    BTN_SR,
    BTN_HSR,
    BTN_SSR,
    OBJ_TYPE_EQUIP,
    BTN_ONE_HAND_SWORD,
    BTN_TWO_HAND_SWORD,
    BTN_SPEAR,
    BTN_PAIR_SWORD,
    BTN_ARROW,
    BTN_ARMOR,
    BTN_HELM,
    BTN_ARM,
    BTN_LEG,
    OBJ_TYPE_SKILL,
    BTN_ATTACK,
    BTN_SUPPORT,
    BTN_HEAL,
    BTN_PASSIVE,
    BTN_GROW,
    SPR_ATTACK_GRAY,
    SPR_SUPPORT_GRAY,
    SPR_HEAL_GRAY,
    SPR_PASSIVE_GRAY,
    SPR_GROW_GRAY,
    BTN_FIRE,
    BTN_WATER,
    BTN_THUNDER,
    BTN_SOIL,
    BTN_LIGHT,
    BTN_DARK,
    BTN_NO_ELEMENT,
    BTN_NUM,
    BTN_GET,
    BTN_RARITY,
    BTN_LEVEL,
    BTN_ATK,
    BTN_DEF,
    BTN_SELL,
    BTN_SOCKET,
    BTN_PRICE,
    BTN_HP,
    BTN_ELEMENT,
    BTN_ELEM_ATK,
    BTN_ELEM_DEF,
    BTN_SKILL_TYPE,
    BTN_ASC,
    BTN_DESC,
    OBJ_HEIGHT_ANCHOR,
    GRD_REQUIREMENT,
    BTN_EQUIP_PAY,
    BTN_EQUIP_NO_PAY,
    OBJ_TYPE,
    OBJ_SORT_ROOT,
    SPR_SORT_UNDER_LINE,
    Frame,
  }
}
