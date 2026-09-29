// Decompiled with JetBrains decompiler
// Type: ItemDetailSellQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ItemDetailSellQuest : ItemDetailSellBase
{
  public override void UpdateUI()
  {
    if (!(this.data is QuestSortData) || MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(this.data.GetTableID()) == null)
      return;
    base.UpdateUI();
    this.SetActive((Enum) ItemDetailSellBase.UI.OBJ_MONEY_ROOT, false);
  }

  protected void OnQuery_SALE()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.data,
      (object) this.GetSliderNum()
    });
  }
}
