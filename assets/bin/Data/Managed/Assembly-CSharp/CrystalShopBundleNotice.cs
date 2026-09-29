// Decompiled with JetBrains decompiler
// Type: CrystalShopBundleNotice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

#nullable disable
public class CrystalShopBundleNotice : GameSection
{
  private ShopReceiver.PaymentPurchaseData purchaseData;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    this.purchaseData = GameSection.GetEventData() as ShopReceiver.PaymentPurchaseData;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    if (this.purchaseData == null)
      return;
    this.SetLabelText((Enum) CrystalShopBundleNotice.UI.LBL_BONUS_NAME, this.purchaseData.productName);
    StringBuilder sb = new StringBuilder();
    int count = this.purchaseData.bundle.Length;
    int index = 0;
    Array.ForEach<ShopReceiver.PaymentPurchaseData.PaymentItemData>(this.purchaseData.bundle, (Action<ShopReceiver.PaymentPurchaseData.PaymentItemData>) (o =>
    {
      ++index;
      if (index < count)
        sb.AppendLine(o.name);
      else
        sb.Append(o.name);
    }));
    this.SetLabelText((Enum) CrystalShopBundleNotice.UI.ProvisionalLabel, sb.ToString());
    this.UpdateAnchors();
  }

  private void OnQuery_CLOSE() => GameSection.BackSection();

  private enum UI
  {
    LBL_BONUS_NAME,
    ProvisionalLabel,
  }
}
