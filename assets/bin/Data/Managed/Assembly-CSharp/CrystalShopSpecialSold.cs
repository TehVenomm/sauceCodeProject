// Decompiled with JetBrains decompiler
// Type: CrystalShopSpecialSold
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class CrystalShopSpecialSold : GameSection
{
  private Network.ProductData _productData;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    this._productData = GameSection.GetEventData() as Network.ProductData;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this._productData == null)
      return;
    this.SetLabelText((Enum) CrystalShopSpecialSold.UI.LBL_DAY, string.Format(this.sectionData.GetText("DAY"), (object) this._productData.remainingDay));
  }

  private void OnQuery_CLOSE() => GameSection.BackSection();

  private enum UI
  {
    LBL_DAY,
  }
}
