// Decompiled with JetBrains decompiler
// Type: ItemIconDetailAccessorySetupper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ItemIconDetailAccessorySetupper : ItemIconDetailSetuperBase
{
  public UILabel lblDescription;

  public override void Set(object[] data = null)
  {
    base.Set();
    AccessoryTable.AccessoryData accessoryData = data[0] as AccessoryTable.AccessoryData;
    this.SetName(accessoryData.name);
    this.SetVisibleBG(true);
    this.infoRootAry[0].SetActive(true);
    this.lblDescription.text = accessoryData.descript;
  }
}
