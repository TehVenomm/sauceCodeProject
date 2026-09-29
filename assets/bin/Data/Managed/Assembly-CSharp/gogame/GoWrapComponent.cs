// Decompiled with JetBrains decompiler
// Type: gogame.GoWrapComponent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace gogame;

public class GoWrapComponent : MonoBehaviour
{
  private void Start() => GoWrap.INSTANCE.initGoWrap(((Object) this).name);

  private bool hasDelegate() => GoWrap.INSTANCE.getDelegate() != null;

  public void handleAdsCompletedWithReward(string message)
  {
    JSONObject jsonObject = new JSONObject(message);
    string field1;
    jsonObject.GetField(out field1, "rewardId", "DEFAULT");
    int field2;
    jsonObject.GetField(out field2, "rewardQuantity", -1);
    if (!this.hasDelegate())
      return;
    GoWrap.INSTANCE.getDelegate().didCompleteRewardedAd(field1, field2);
  }

  public void handleMenuOpened(string message)
  {
    if (!this.hasDelegate())
      return;
    GoWrap.INSTANCE.getDelegate().onMenuOpened();
  }

  public void handleMenuClosed(string message)
  {
    if (!this.hasDelegate())
      return;
    GoWrap.INSTANCE.getDelegate().onMenuClosed();
  }

  public void handleOnCustomUrl(string message)
  {
    if (!this.hasDelegate())
      return;
    GoWrap.INSTANCE.getDelegate().onCustomUrl(message);
  }

  public void handleOnOffersAvailable(string message)
  {
    if (!this.hasDelegate())
      return;
    GoWrap.INSTANCE.getDelegate().onOffersAvailable();
  }
}
