// Decompiled with JetBrains decompiler
// Type: gogame.GoWrapTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
namespace gogame;

public class GoWrapTest : MonoBehaviour, IGoWrapDelegate
{
  private void Start()
  {
    GoWrap.INSTANCE.setGuid("GUID123");
    GoWrap.INSTANCE.setDelegate((IGoWrapDelegate) this);
  }

  public void ShowGoWrapDebugMenu() => GoWrapDebugMenu.ShowGoWrapDebugMenu();

  public void didCompleteRewardedAd(string rewardId, int rewardQuantity)
  {
    Debug.Log((object) $"[goWrap/test] didCompleteRewardedAd({rewardId}, {rewardQuantity})");
  }

  public void onMenuOpened() => Debug.Log((object) "[goWrap/test] onMenuOpened()");

  public void onMenuClosed() => Debug.Log((object) "[goWrap/test] onMenuClosed()");

  public void onCustomUrl(string url) => Debug.Log((object) $"[goWrap/test] onCustomUrl({url})");

  public void onOffersAvailable() => Debug.Log((object) "[goWrap/test] onOffersAvailable()");
}
