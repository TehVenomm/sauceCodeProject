// Decompiled with JetBrains decompiler
// Type: HomeFeatureBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections;
using UnityEngine;

#nullable disable
public class HomeFeatureBanner : MonoBehaviour
{
  private string MODEL_FORMAT = "NPC011_{0:000}";
  private Transform parent;
  private Vector3 position;
  private Quaternion rotation;
  private HomeStageTouchEvent touchEvent;
  private int currentVariation = -1;
  private int currentBannerId = -1;
  private Transform modelTransform;
  private Material bannerMaterial;

  public bool isLoading { get; private set; }

  public void Setup(Transform parent, Vector3 position, Quaternion rotation)
  {
    this.parent = parent;
    this.position = position;
    this.rotation = rotation;
    this.Reposition();
  }

  private void Reposition()
  {
    if (!Object.op_Implicit((Object) this.modelTransform))
      return;
    this.modelTransform.parent = this.parent;
    this.modelTransform.localPosition = this.position;
    this.modelTransform.rotation = this.rotation;
    this.modelTransform.localScale = Vector3.one;
  }

  public void SetBanner(int bannerId)
  {
    if (this.currentBannerId != -1 && bannerId == this.currentBannerId)
      return;
    this.Load(1, bannerId);
  }

  private void Load(int variation, int bannerId)
  {
    this.isLoading = true;
    this.StartCoroutine(this._Load(variation, bannerId));
  }

  private IEnumerator _Load(int variation, int bannerId)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_model = (LoadObject) null;
    LoadObject lo_tex = (LoadObject) null;
    bool loadModel = this.currentVariation != variation;
    bool loadTex = this.currentBannerId != bannerId;
    if (loadModel)
      lo_model = loadingQueue.Load(RESOURCE_CATEGORY.NPC_MODEL, string.Format(this.MODEL_FORMAT, (object) variation));
    if (loadTex)
      lo_tex = loadingQueue.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetHomePointSHopBannerImageName(bannerId));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    if (loadModel)
    {
      GameObject go = Object.Instantiate(lo_model.loadedObject) as GameObject;
      this.modelTransform = go.transform;
      this.bannerMaterial = this.FindBannerMaterial(go);
      this.Reposition();
      this.SetTouchEvent(go);
      this.currentVariation = variation;
    }
    if (loadTex)
    {
      if (Object.op_Implicit((Object) this.bannerMaterial))
        this.bannerMaterial.mainTexture = (Texture) (lo_tex.loadedObject as Texture2D);
      this.currentBannerId = bannerId;
    }
    this.isLoading = false;
  }

  private void SetTouchEvent(GameObject go)
  {
    GameObject go1 = new GameObject("BANNER_TOUCH_EVENT");
    Transform transform = go1.transform;
    transform.parent = go.transform;
    transform.localPosition = Vector3.zero;
    transform.localRotation = Quaternion.identity;
    this.SetCollider(go1);
    this.touchEvent = go1.AddComponent<HomeStageTouchEvent>();
  }

  private void SetCollider(GameObject go)
  {
    CapsuleCollider capsuleCollider = go.AddComponent<CapsuleCollider>();
    capsuleCollider.radius = 0.5f;
    capsuleCollider.height = 2.2f;
    capsuleCollider.direction = 0;
    capsuleCollider.center = new Vector3(0.0f, 0.1f, 0.0f);
  }

  private void SetBannerEvent(HomeStageTouchEvent touchEvent, EventBanner banner)
  {
    switch (banner.LinkType)
    {
      case LINK_TYPE.PAYMENT:
        touchEvent.eventName = "BANNER_CRYSTAL_SHOP";
        touchEvent.eventData = (object) 0;
        break;
      case LINK_TYPE.GACHA:
        touchEvent.eventName = "BANNER_GACHA";
        touchEvent.eventData = (object) banner.param;
        break;
      case LINK_TYPE.NEWS:
        touchEvent.eventName = "BANNER_NEWS";
        touchEvent.eventData = (object) banner.param;
        break;
      case LINK_TYPE.EVENT_DELIVERY:
        touchEvent.eventName = "BANNER_EVENT_DELIVERY";
        touchEvent.eventData = (object) banner.param;
        break;
      case LINK_TYPE.EXPLORE_DELIVERY:
        touchEvent.eventName = "BANNER_EXPLORE_DELIVERY";
        touchEvent.eventData = (object) banner.param;
        break;
      case LINK_TYPE.LOGIN_BONUS:
        touchEvent.eventName = "BANNER_LOGIN_BONUS";
        touchEvent.eventData = (object) banner.param;
        break;
      default:
        touchEvent.eventName = "BANNER_NEWS";
        touchEvent.eventData = (object) 0;
        break;
    }
  }

  private Material FindBannerMaterial(GameObject go)
  {
    Renderer componentInChildren = go.GetComponentInChildren<Renderer>();
    int index = 0;
    for (int length = componentInChildren.sharedMaterials.Length; index < length; ++index)
    {
      if (((Object) componentInChildren.sharedMaterials[index]).name.ToLower().EndsWith("_banner"))
        return componentInChildren.materials[index];
    }
    return (Material) null;
  }
}
