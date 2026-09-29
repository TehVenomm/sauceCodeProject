// Decompiled with JetBrains decompiler
// Type: HomeIAPPopAd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeIAPPopAd : GameSection
{
  private string productId = "";

  public override void Initialize()
  {
    this.productId = GameSection.GetEventData() as string;
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    ProductDataTable.PackInfo pack = Singleton<ProductDataTable>.I.GetPack(this.productId);
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loTex = loadingQueue.Load(RESOURCE_CATEGORY.GACHA_POP_UP_ADVERTISEMENT, pack.popupAdsBanner);
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (Object.op_Inequality(loTex.loadedObject, (Object) null))
      this.SetTexture((Enum) HomeIAPPopAd.UI.TEX_MAIN, loTex.loadedObject as Texture);
    base.Initialize();
  }

  public override void UpdateUI() => base.UpdateUI();

  private void OnQuery_OK() => this.DispatchEvent("CRYSTAL_SHOP", (object) this.productId);

  protected enum UI
  {
    OBJ_FRAME,
    TEX_MAIN,
  }
}
