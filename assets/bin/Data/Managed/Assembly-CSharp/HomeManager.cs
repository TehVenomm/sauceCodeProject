// Decompiled with JetBrains decompiler
// Type: HomeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeManager : MonoBehaviourSingleton<HomeManager>, IHomeManager
{
  public bool IsJumpToGacha { get; set; }

  public bool IsInitialized { get; private set; }

  public HomeCamera HomeCamera { get; private set; }

  public IHomePeople IHomePeople { get; private set; }

  public HomeFeatureBanner HomeFeatureBanner { get; private set; }

  public bool IsPointShopOpen { get; private set; }

  public int PointShopBannerId { get; private set; }

  public void SetPointShop(bool isOpen, int bannerId)
  {
    this.IsPointShopOpen = isOpen;
    this.PointShopBannerId = bannerId;
  }

  public void SendDebugAddPointShopPoint(int pointShopId, int addPoint, Action<bool> call_back)
  {
    Protocol.Send<DebugAddPointShopPointModel.RequestSendForm, DebugAddPointShopPointModel>(DebugAddPointShopPointModel.URL, new DebugAddPointShopPointModel.RequestSendForm()
    {
      point = addPoint,
      pointShopId = pointShopId
    }, (Action<DebugAddPointShopPointModel>) (ret => call_back(ErrorCodeChecker.IsSuccess(ret.Error))));
  }

  public OutGameSettingsManager.HomeScene GetSceneSetting()
  {
    return MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene;
  }

  private IEnumerator Start()
  {
    while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
      yield return (object) null;
    this.HomeCamera = ((Component) this).gameObject.AddComponent<HomeCamera>();
    this.IHomePeople = (IHomePeople) ((Component) this).gameObject.AddComponent<HomePeople>();
    this.HomeFeatureBanner = ((Component) this).gameObject.AddComponent<HomeFeatureBanner>();
    while (!this.HomeCamera.isInitialized)
      yield return (object) null;
    while (this.IHomePeople.isInitialized)
      yield return (object) null;
    this.IsInitialized = true;
  }
}
