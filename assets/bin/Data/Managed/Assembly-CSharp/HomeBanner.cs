// Decompiled with JetBrains decompiler
// Type: HomeBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeBanner : BaseBanner
{
  private Network.HomeBanner homeBanner;

  public override string overrideBackKeyEvent => "CLOSE";

  public override void UpdateUI()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.homeBannerList.Count <= 0)
      this.Close();
    else
      this.StartCoroutine(this.LoadImg(GameSection.GetEventData() as Network.HomeBanner));
  }

  private IEnumerator LoadImg(Network.HomeBanner _homeBanner)
  {
    if (_homeBanner != null)
    {
      this.homeBanner = _homeBanner;
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo = loadingQueue.Load(true, RESOURCE_CATEGORY.HOME_BANNER_ADS, ResourceName.GetHomeBanner(this.homeBanner.bannerId));
      yield return (object) loadingQueue.Wait();
      UITexture component = ((Component) this.FindCtrl(this._transform, (Enum) HomeBanner.UI.BANNER)).GetComponent<UITexture>();
      Texture2D loadedObject = lo.loadedObject as Texture2D;
      if (Object.op_Inequality((Object) loadedObject, (Object) null))
        component.mainTexture = (Texture) loadedObject;
      else
        component.mainTexture = Resources.Load("Texture/White") as Texture;
      Transform transform = ((Component) component).transform;
      switch (this.homeBanner.homeType)
      {
        case 0:
          this.SetEvent(transform, "CLOSE", 0);
          break;
        case 1:
          string str = !string.IsNullOrEmpty(this.homeBanner.targetString) ? this.homeBanner.targetString : "bundle";
          this.SetEvent(transform, "OPEN_CRYSTAL_SHOP", (object) str.Trim());
          break;
        case 2:
          this.SetEvent(transform, "OPEN_CRYSTAL_SHOP", 0);
          break;
        case 3:
          this.SetEvent(transform, "BANNER_GACHA", (object) GACHA_TYPE.QUEST);
          break;
        case 4:
          this.SetEvent(transform, "BANNER_GACHA", (object) GACHA_TYPE.SKILL);
          break;
        case 5:
          this.SetEvent(transform, "OPEN_BANNER_NEWS", (object) this.homeBanner.targetString);
          break;
      }
      lo = (LoadObject) null;
    }
  }

  public void OnQuery_OPEN_CRYSTAL_SHOP()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CrystalShop");
  }

  public void OnQuery_OPEN_BANNER_NEWS()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("CommonDialog", "AdsWebView");
  }

  private new enum UI
  {
    BANNER,
  }
}
