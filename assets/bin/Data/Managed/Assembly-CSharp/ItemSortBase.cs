// Decompiled with JetBrains decompiler
// Type: ItemSortBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemSortBase : SortBase
{
  private static readonly ItemSortBase.UI[] rarityButton = new ItemSortBase.UI[7]
  {
    ItemSortBase.UI.BTN_N,
    ItemSortBase.UI.BTN_HN,
    ItemSortBase.UI.BTN_R,
    ItemSortBase.UI.BTN_HR,
    ItemSortBase.UI.BTN_SR,
    ItemSortBase.UI.BTN_HSR,
    ItemSortBase.UI.BTN_SSR
  };
  private static readonly ItemSortBase.UI[] materialButton = new ItemSortBase.UI[5]
  {
    ItemSortBase.UI.BTN_COMMON,
    ItemSortBase.UI.BTN_UNIQUE,
    ItemSortBase.UI.BTN_LITHOGRAPH,
    ItemSortBase.UI.BTN_EQUIP,
    ItemSortBase.UI.BTN_METAL
  };
  private static readonly ItemSortBase.UI[] equipFilterButton = new ItemSortBase.UI[6]
  {
    ItemSortBase.UI.BTN_EQUIP_PAY,
    ItemSortBase.UI.BTN_EQUIP_NO_PAY,
    ItemSortBase.UI.BTN_EQUIP_CREATABLE,
    ItemSortBase.UI.BTN_EQUIP_NO_CREATABLE,
    ItemSortBase.UI.BTN_EQUIP_OBTAINED,
    ItemSortBase.UI.BTN_EQUIP_NO_OBTAINED
  };
  private static readonly ItemSortBase.UI?[] requirementButton;
  private static readonly ItemSortBase.UI[] elementButton;
  private static readonly ItemSortBase.UI[] ascButton;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.MATERIAL)
    {
      this.SetActive((Enum) ItemSortBase.UI.MATERIAL_ROOT, true);
      this.SetActive((Enum) ItemSortBase.UI.RARITY_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.EQUIP_FILTER_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.ELEMENT_ROOT, true);
      int event_data1 = 0;
      for (int length = ItemSortBase.materialButton.Length; event_data1 < length; ++event_data1)
      {
        bool flag = (this.sortOrder.type & 1 << event_data1) != 0;
        this.SetEvent((Enum) ItemSortBase.materialButton[event_data1], "MATERIAL", event_data1);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.materialButton[event_data1]).parent, flag);
      }
      int event_data2 = 0;
      for (int length = ItemSortBase.elementButton.Length; event_data2 < length; ++event_data2)
      {
        bool flag = (this.sortOrder.element & 1 << event_data2) != 0;
        this.SetEvent((Enum) ItemSortBase.elementButton[event_data2], "ELEMENT", event_data2);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.elementButton[event_data2]).parent, flag);
      }
      GameObject.Find("ItemSortFrame").gameObject.GetComponent<UIWidget>().height = 633;
      this.GetCtrl((Enum) ItemSortBase.UI.ELEMENT_ROOT).localPosition = new Vector3(0.0f, -153f, 0.0f);
      GameObject.Find("sort").gameObject.transform.localPosition = new Vector3(0.0f, -322f, 0.0f);
    }
    else if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON || this.sortOrder.dialogType == SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR)
    {
      this.SetActive((Enum) ItemSortBase.UI.MATERIAL_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.EQUIP_FILTER_ROOT, true);
      this.SetActive((Enum) ItemSortBase.UI.RARITY_ROOT, true);
      this.SetActive((Enum) ItemSortBase.UI.ELEMENT_ROOT, true);
      int event_data3 = 0;
      for (int length = ItemSortBase.rarityButton.Length; event_data3 < length; ++event_data3)
      {
        bool flag = (this.sortOrder.rarity & 1 << event_data3) != 0;
        this.SetEvent((Enum) ItemSortBase.rarityButton[event_data3], "RARITY", event_data3);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.rarityButton[event_data3]).parent, flag);
      }
      int event_data4 = 0;
      for (int length = ItemSortBase.equipFilterButton.Length; event_data4 < length; ++event_data4)
      {
        bool flag = (this.sortOrder.equipFilter & 1 << event_data4) != 0;
        this.SetEvent((Enum) ItemSortBase.equipFilterButton[event_data4], "EQUIPFILTER", event_data4);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.equipFilterButton[event_data4]).parent, flag);
      }
      int event_data5 = 0;
      for (int length = ItemSortBase.elementButton.Length; event_data5 < length; ++event_data5)
      {
        bool flag = (this.sortOrder.element & 1 << event_data5) != 0;
        this.SetEvent((Enum) ItemSortBase.elementButton[event_data5], "ELEMENT", event_data5);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.elementButton[event_data5]).parent, flag);
      }
      UIWidget component = GameObject.Find("ItemSortFrame").gameObject.GetComponent<UIWidget>();
      Transform transform1 = ((Component) component).transform;
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(transform1.localPosition.x, transform1.localPosition.y + 40f, transform1.localPosition.z);
      transform1.localPosition = vector3_1;
      component.height = 750;
      Transform transform2 = GameObject.Find("sort").gameObject.transform;
      Vector3 vector3_2;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_2).\u002Ector(transform2.localPosition.x, transform2.localPosition.y - 240f, transform2.localPosition.z);
      transform2.localPosition = vector3_2;
    }
    else if (this.sortOrder.dialogType == SortBase.DIALOG_TYPE.ABILITY_ITEM)
    {
      this.SetActive((Enum) ItemSortBase.UI.MATERIAL_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.EQUIP_FILTER_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.RARITY_ROOT, true);
      this.SetActive((Enum) ItemSortBase.UI.ELEMENT_ROOT, true);
      int event_data6 = 0;
      for (int length = ItemSortBase.rarityButton.Length; event_data6 < length; ++event_data6)
      {
        bool flag = (this.sortOrder.rarity & 1 << event_data6) != 0;
        this.SetEvent((Enum) ItemSortBase.rarityButton[event_data6], "RARITY", event_data6);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.rarityButton[event_data6]).parent, flag);
      }
      int event_data7 = 0;
      for (int length = ItemSortBase.elementButton.Length; event_data7 < length; ++event_data7)
      {
        bool flag = (this.sortOrder.element & 1 << event_data7) != 0;
        this.SetEvent((Enum) ItemSortBase.elementButton[event_data7], "ELEMENT", event_data7);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.elementButton[event_data7]).parent, flag);
      }
      GameObject.Find("ItemSortFrame").gameObject.GetComponent<UIWidget>().height = 583;
      this.GetCtrl((Enum) ItemSortBase.UI.ELEMENT_ROOT).localPosition = new Vector3(0.0f, -98f, 0.0f);
      GameObject.Find("sort").gameObject.transform.localPosition = new Vector3(0.0f, -264f, 0.0f);
    }
    else
    {
      this.SetActive((Enum) ItemSortBase.UI.MATERIAL_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.EQUIP_FILTER_ROOT, false);
      this.SetActive((Enum) ItemSortBase.UI.RARITY_ROOT, true);
      this.SetActive((Enum) ItemSortBase.UI.ELEMENT_ROOT, false);
      int event_data = 0;
      for (int length = ItemSortBase.rarityButton.Length; event_data < length; ++event_data)
      {
        bool flag = (this.sortOrder.rarity & 1 << event_data) != 0;
        this.SetEvent((Enum) ItemSortBase.rarityButton[event_data], "RARITY", event_data);
        this.SetToggle(this.GetCtrl((Enum) ItemSortBase.rarityButton[event_data]).parent, flag);
      }
    }
    int num;
    switch (this.sortOrder.dialogType)
    {
      case SortBase.DIALOG_TYPE.STORAGE_EQUIP:
      case SortBase.DIALOG_TYPE.STORAGE_SKILL:
        num = 8604;
        break;
      case SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON:
        num = 8488;
        break;
      case SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR:
        num = 8520;
        break;
      case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON:
        num = 8489;
        break;
      case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR:
        num = 8521;
        break;
      default:
        num = 138;
        break;
    }
    ItemSortBase.UI? label_enum = new ItemSortBase.UI?();
    int index = 0;
    for (int length = ItemSortBase.requirementButton.Length; index < length; ++index)
    {
      if (ItemSortBase.requirementButton[index].HasValue)
      {
        int event_data = 1 << index;
        if ((event_data & num) != 0)
        {
          bool flag = this.sortOrder.requirement == (SortBase.SORT_REQUIREMENT) event_data;
          this.SetEvent((Enum) (ValueType) ItemSortBase.requirementButton[index], "REQUIREMENT", event_data);
          this.SetToggle((Enum) (ValueType) ItemSortBase.requirementButton[index], flag);
          label_enum = ItemSortBase.requirementButton[index];
        }
        else
          this.SetActive((Enum) (ValueType) ItemSortBase.requirementButton[index], false);
      }
    }
    if (label_enum.HasValue)
    {
      this.GetComponent<UIGrid>((Enum) ItemSortBase.UI.GRD_REQUIREMENT).Reposition();
      this.GetCtrl((Enum) ItemSortBase.UI.OBJ_HEIGHT_ANCHOR).position = this.GetCtrl((Enum) (ValueType) label_enum).position;
    }
    int event_data8 = 0;
    for (int length = ItemSortBase.ascButton.Length; event_data8 < length; ++event_data8)
    {
      bool flag = false;
      if (event_data8 == 0 && this.sortOrder.orderTypeAsc || event_data8 == 1 && !this.sortOrder.orderTypeAsc)
        flag = true;
      this.SetEvent((Enum) ItemSortBase.ascButton[event_data8], "ORDER_TYPE", event_data8);
      this.SetToggle((Enum) ItemSortBase.ascButton[event_data8], flag);
    }
  }

  private void OnQuery_RARITY()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Rarity(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) ItemSortBase.rarityButton[_index]).parent, _is_enable);
  }

  private void OnQuery_MATERIAL()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Type(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) ItemSortBase.materialButton[_index]).parent, _is_enable);
  }

  private void OnQuery_EQUIPFILTER()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_EquipFilter(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) ItemSortBase.equipFilterButton[_index]).parent, _is_enable);
  }

  private void OnQuery_ELEMENT()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Element(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) ItemSortBase.elementButton[_index]).parent, _is_enable);
  }

  static ItemSortBase()
  {
    ItemSortBase.UI?[] nullableArray = new ItemSortBase.UI?[14];
    nullableArray[0] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_ID);
    nullableArray[1] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_NUM);
    nullableArray[2] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_GET);
    nullableArray[3] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_RARITY);
    nullableArray[4] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_LEVEL);
    nullableArray[5] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_ATK);
    nullableArray[6] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_DEF);
    nullableArray[7] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_SELL);
    nullableArray[8] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_SOCKET);
    nullableArray[9] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_PRICE);
    nullableArray[13] = new ItemSortBase.UI?(ItemSortBase.UI.BTN_ELEMENT);
    ItemSortBase.requirementButton = nullableArray;
    ItemSortBase.elementButton = new ItemSortBase.UI[7]
    {
      ItemSortBase.UI.BTN_FIRE,
      ItemSortBase.UI.BTN_WATER,
      ItemSortBase.UI.BTN_THUNDER,
      ItemSortBase.UI.BTN_SOIL,
      ItemSortBase.UI.BTN_LIGHT,
      ItemSortBase.UI.BTN_DARK,
      ItemSortBase.UI.BTN_NO_ELEMENT
    };
    ItemSortBase.ascButton = new ItemSortBase.UI[2]
    {
      ItemSortBase.UI.BTN_ASC,
      ItemSortBase.UI.BTN_DESC
    };
  }

  private enum UI
  {
    MATERIAL_ROOT,
    RARITY_ROOT,
    EQUIP_FILTER_ROOT,
    ELEMENT_ROOT,
    BTN_N,
    BTN_HN,
    BTN_R,
    BTN_HR,
    BTN_SR,
    BTN_HSR,
    BTN_SSR,
    BTN_COMMON,
    BTN_UNIQUE,
    BTN_LITHOGRAPH,
    BTN_EQUIP,
    BTN_METAL,
    BTN_EQUIP_PAY,
    BTN_EQUIP_NO_PAY,
    BTN_EQUIP_CREATABLE,
    BTN_EQUIP_NO_CREATABLE,
    BTN_EQUIP_OBTAINED,
    BTN_EQUIP_NO_OBTAINED,
    BTN_FIRE,
    BTN_WATER,
    BTN_THUNDER,
    BTN_SOIL,
    BTN_LIGHT,
    BTN_DARK,
    BTN_NO_ELEMENT,
    BTN_ID,
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
    BTN_ASC,
    BTN_DESC,
    OBJ_HEIGHT_ANCHOR,
    GRD_REQUIREMENT,
  }
}
