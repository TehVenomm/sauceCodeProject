// Decompiled with JetBrains decompiler
// Type: AccessorySortBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AccessorySortBase : SortBase
{
  private static readonly AccessorySortBase.UI[] rarityButton = new AccessorySortBase.UI[7]
  {
    AccessorySortBase.UI.BTN_N,
    AccessorySortBase.UI.BTN_HN,
    AccessorySortBase.UI.BTN_R,
    AccessorySortBase.UI.BTN_HR,
    AccessorySortBase.UI.BTN_SR,
    AccessorySortBase.UI.BTN_HSR,
    AccessorySortBase.UI.BTN_SSR
  };
  private static readonly AccessorySortBase.UI?[] requirementButton;
  private static readonly AccessorySortBase.UI[] ascButton;

  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    int event_data1 = 0;
    for (int length = AccessorySortBase.rarityButton.Length; event_data1 < length; ++event_data1)
    {
      bool flag = (this.sortOrder.rarity & 1 << event_data1) != 0;
      this.SetEvent((Enum) AccessorySortBase.rarityButton[event_data1], "RARITY", event_data1);
      Transform ctrl = this.GetCtrl((Enum) AccessorySortBase.rarityButton[event_data1]);
      if (Object.op_Inequality((Object) ctrl, (Object) null))
        this.SetToggle(ctrl.parent, flag);
    }
    int index = 0;
    for (int length = AccessorySortBase.requirementButton.Length; index < length; ++index)
    {
      if (AccessorySortBase.requirementButton[index].HasValue)
      {
        int event_data2 = 1 << index;
        bool flag = this.sortOrder.requirement == (SortBase.SORT_REQUIREMENT) event_data2;
        this.SetEvent((Enum) (ValueType) AccessorySortBase.requirementButton[index], "REQUIREMENT", event_data2);
        this.SetToggle((Enum) (ValueType) AccessorySortBase.requirementButton[index], flag);
      }
    }
    int event_data3 = 0;
    for (int length = AccessorySortBase.ascButton.Length; event_data3 < length; ++event_data3)
    {
      bool flag = event_data3 == 0 && this.sortOrder.orderTypeAsc || event_data3 == 1 && !this.sortOrder.orderTypeAsc;
      this.SetEvent((Enum) AccessorySortBase.ascButton[event_data3], "ORDER_TYPE", event_data3);
      this.SetToggle((Enum) AccessorySortBase.ascButton[event_data3], flag);
    }
  }

  private void OnQuery_RARITY()
  {
    int _index;
    bool _is_enable;
    this.OnQueryEvent_Rarity(out _index, out _is_enable);
    this.SetToggle(this.GetCtrl((Enum) AccessorySortBase.rarityButton[_index]).parent, _is_enable);
  }

  static AccessorySortBase()
  {
    AccessorySortBase.UI?[] nullableArray = new AccessorySortBase.UI?[4];
    nullableArray[2] = new AccessorySortBase.UI?(AccessorySortBase.UI.BTN_GET);
    nullableArray[3] = new AccessorySortBase.UI?(AccessorySortBase.UI.BTN_RARITY);
    AccessorySortBase.requirementButton = nullableArray;
    AccessorySortBase.ascButton = new AccessorySortBase.UI[2]
    {
      AccessorySortBase.UI.BTN_ASC,
      AccessorySortBase.UI.BTN_DESC
    };
  }

  private enum UI
  {
    BTN_N,
    BTN_HN,
    BTN_R,
    BTN_HR,
    BTN_SR,
    BTN_HSR,
    BTN_SSR,
    BTN_GET,
    BTN_RARITY,
    BTN_SELL,
    BTN_ASC,
    BTN_DESC,
  }
}
