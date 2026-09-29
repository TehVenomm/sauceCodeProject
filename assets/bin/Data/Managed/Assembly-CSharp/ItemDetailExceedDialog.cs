// Decompiled with JetBrains decompiler
// Type: ItemDetailExceedDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ItemDetailExceedDialog : SmithExceedDialog
{
  protected override bool IsValidExceedSection() => false;

  protected override void SetupExceedData()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    this.itemTable = eventData[0] as EquipItemTable.EquipItemData;
    this.exceedCount = (int) eventData[1];
  }
}
