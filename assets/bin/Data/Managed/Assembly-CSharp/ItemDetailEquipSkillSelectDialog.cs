// Decompiled with JetBrains decompiler
// Type: ItemDetailEquipSkillSelectDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemDetailEquipSkillSelectDialog : ItemDetailEquipSkillSelect
{
  private object initData;

  public override void Initialize()
  {
    this.initData = GameSection.GetEventData();
    base.Initialize();
  }

  protected override bool CheckApplicationVersion()
  {
    return (this.selectSkillItem == null || MonoBehaviourSingleton<GameSceneManager>.I.CheckSkillItemAndOpenUpdateAppDialog(this.selectSkillItem.tableData, new System.Action(this.OnCancelSelect))) && base.CheckApplicationVersion();
  }

  private void OnCancelSelect()
  {
    this.selectSkillItem = this.equipSkillItem;
    this.selectIndex = this.GetSelectItemIndex(this.selectSkillItem);
    this.SetDirty((Enum) SkillSelectBaseSecond.UI.GRD_INVENTORY);
    this.RefreshUI();
  }

  protected override object[] CreateDetailEventData(int index)
  {
    return new object[2]
    {
      (object) this.callSection,
      this.inventory.datas[index].GetItemData()
    };
  }

  private void OnQuery_DETAIL()
  {
    Debug.LogWarning((object) nameof (OnQuery_DETAIL));
    this.selectIndex = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) new ItemDetailSkillSimpleDialog.InitParam((object) this.CreateDetailEventData(this.selectIndex), this.initData));
  }
}
