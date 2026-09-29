// Decompiled with JetBrains decompiler
// Type: CrystalShopSpecialStarter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class CrystalShopSpecialStarter : GameSection
{
  private ShopReceiver.PaymentPurchaseData purchaseData;

  public override void Initialize()
  {
    this.purchaseData = GameSection.GetEventData() as ShopReceiver.PaymentPurchaseData;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetModel((Enum) CrystalShopSpecialStarter.UI.OBJ_MODEL, "RoyalChest_Open");
  }

  public override void StartSection()
  {
    this.StartCoroutine(this.Wait(Singleton<ProductDataTable>.I.GetPack(this.purchaseData.productId).openAnimEndTime));
  }

  private IEnumerator Wait(float time)
  {
    yield return (object) new WaitForSeconds(time);
    this.RequestEvent("BUNDLE_NOTICE", (object) this.purchaseData);
    GameSection.BackSection();
  }

  private enum UI
  {
    OBJ_MODEL,
  }
}
