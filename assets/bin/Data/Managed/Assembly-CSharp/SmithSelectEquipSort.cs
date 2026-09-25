// Decompiled with JetBrains decompiler
// Type: SmithSelectEquipSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class SmithSelectEquipSort : SortBase
{
  private SmithSelectEquipSort.UI?[] requirementButton;
  private SmithSelectEquipSort.UI[] ascButton;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    int num;
    switch (this.sortOrder.dialogType)
    {
      case SortBase.DIALOG_TYPE.WEAPON:
        num = 25020;
        break;
      default:
        num = 41436;
        break;
    }
    SmithSelectEquipSort.UI? label_enum = new SmithSelectEquipSort.UI?();
    int index = 0;
    for (int length = this.requirementButton.Length; index < length; ++index)
    {
      if (this.requirementButton[index].HasValue)
      {
        int event_data = 1 << index;
        if ((event_data & num) != 0)
        {
          bool flag = this.sortOrder.requirement == (SortBase.SORT_REQUIREMENT) event_data;
          this.SetEvent((Enum) (ValueType) this.requirementButton[index], "REQUIREMENT", event_data);
          this.SetToggle((Enum) (ValueType) this.requirementButton[index], flag);
          label_enum = this.requirementButton[index];
        }
        else
          this.SetActive((Enum) (ValueType) this.requirementButton[index], false);
      }
    }
    if (label_enum.HasValue)
    {
      this.GetComponent<UIGrid>((Enum) SmithSelectEquipSort.UI.GRD_REQUIREMENT).Reposition();
      this.GetCtrl((Enum) SmithSelectEquipSort.UI.OBJ_HEIGHT_ANCHOR).position = this.GetCtrl((Enum) (ValueType) label_enum).position;
    }
    int event_data1 = 0;
    for (int length = this.ascButton.Length; event_data1 < length; ++event_data1)
    {
      bool flag = false;
      if (event_data1 == 0 && this.sortOrder.orderTypeAsc || event_data1 == 1 && !this.sortOrder.orderTypeAsc)
        flag = true;
      this.SetEvent((Enum) this.ascButton[event_data1], "ORDER_TYPE", event_data1);
      this.SetToggle((Enum) this.ascButton[event_data1], flag);
    }
  }

  public SmithSelectEquipSort()
  {
    SmithSelectEquipSort.UI?[] nullableArray = new SmithSelectEquipSort.UI?[16 /*0x10*/];
    nullableArray[1] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_NUM);
    nullableArray[2] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_GET);
    nullableArray[3] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_RARITY);
    nullableArray[4] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_LEVEL);
    nullableArray[5] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_ATK);
    nullableArray[6] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_DEF);
    nullableArray[7] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_SELL);
    nullableArray[8] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_SOCKET);
    nullableArray[9] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_PRICE);
    nullableArray[12] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_HP);
    nullableArray[13] = new SmithSelectEquipSort.UI?(SmithSelectEquipSort.UI.BTN_ELEMENT);
    this.requirementButton = nullableArray;
    this.ascButton = new SmithSelectEquipSort.UI[2]
    {
      SmithSelectEquipSort.UI.BTN_ASC,
      SmithSelectEquipSort.UI.BTN_DESC
    };
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  protected enum UI
  {
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
