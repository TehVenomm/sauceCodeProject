// Decompiled with JetBrains decompiler
// Type: ItemIconDetailItemSetupper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ItemIconDetailItemSetupper : ItemIconDetailSetuperBase
{
  public UILabel lblNum;
  public UILabel lblDescription;
  public UILabel lblEndDate;

  public override void Set(object[] data = null)
  {
    base.Set();
    ItemTable.ItemData itemData = data[0] as ItemTable.ItemData;
    int num1 = (int) data[1];
    int num2 = (bool) data[2] ? 1 : 0;
    this.SetName(itemData.name);
    this.SetVisibleBG(true);
    if (num2 != 0)
    {
      this.SetActiveInfo(0);
      this.lblNum.text = num1.ToString();
      DateTime dateTime = new DateTime();
      if (itemData.endDate != dateTime)
        this.lblEndDate.text = "Valid till " + itemData.endDate.ToString("yyyy/MM/dd HH:mm");
      else
        this.lblEndDate.text = "";
    }
    else
    {
      this.SetActiveInfo(1);
      this.SetDescription(itemData.text);
    }
  }

  public void SetActiveInfo(int activeIndex)
  {
    for (int index = 0; index < this.infoRootAry.Length; ++index)
      this.infoRootAry[index].SetActive(index == activeIndex);
  }

  public void SetDescription(string text) => this.lblDescription.text = text;
}
