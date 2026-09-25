// Decompiled with JetBrains decompiler
// Type: EventBannerView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EventBannerView : UIBehaviour
{
  private Dictionary<Transform, IEnumerator> loadingRoutines = new Dictionary<Transform, IEnumerator>();
  private UIScrollView sctList1;
  private UIScrollView sctList2;
  private GameObject mEventBannerPrefab;
  private UICenterOnChild mCenterOnChild1;
  private UICenterOnChild mCenterOnChild2;
  private GameObject[] indexList;
  private const int SHOW_BANNER1_NUM = 1;
  private const int SHOW_BANNER2_NUM = 5;
  private int bannerNum1;
  private int bannerNum2;
  private bool updateEventBanner = true;
  private int mCenterIndex1;
  private int mCenterIndex2;
  private const float BANNER_AUTO_SCROLL_INTERVAL = 5f;
  private float timer1;
  private float timer2;

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER;
  }

  private IEnumerator Start()
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo_event_banner = (LoadObject) loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, "EventBanner");
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.mEventBannerPrefab = lo_event_banner.loadedObject as GameObject;
    this.AddPrefab(this.mEventBannerPrefab, lo_event_banner.PopInstantiatedGameObject());
    this.mCenterOnChild1 = ((Component) this.GetCtrl((Enum) EventBannerView.UI.WRP_EVENT_BANNER1)).GetComponent<UICenterOnChild>();
    this.mCenterOnChild1.onCenter = new UICenterOnChild.OnCenterCallback(this.OnCenter1);
    this.mCenterOnChild2 = ((Component) this.GetCtrl((Enum) EventBannerView.UI.WRP_EVENT_BANNER2)).GetComponent<UICenterOnChild>();
    this.mCenterOnChild2.onCenter = new UICenterOnChild.OnCenterCallback(this.OnCenter2);
  }

  public override void UpdateUI()
  {
    if (Object.op_Equality((Object) this.mEventBannerPrefab, (Object) null))
      return;
    this.UpdateEventBannerAll();
    this.sctList1 = this.GetComponent<UIScrollView>((Enum) EventBannerView.UI.SCR_LIST1);
    this.sctList2 = this.GetComponent<UIScrollView>((Enum) EventBannerView.UI.SCR_LIST2);
    base.UpdateUI();
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER) != (GameSection.NOTIFY_FLAG) 0)
      this.updateEventBanner = true;
    base.OnNotify(flags);
  }

  private void UpdateEventBannerAll()
  {
    if (!this.updateEventBanner)
      return;
    this.updateEventBanner = false;
    if (MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count <= 0)
    {
      this.Close();
    }
    else
    {
      this.GetCtrl((Enum) EventBannerView.UI.WRP_EVENT_BANNER1).DestroyChildren();
      this.GetCtrl((Enum) EventBannerView.UI.WRP_EVENT_BANNER2).DestroyChildren();
      foreach (IEnumerator enumerator in this.loadingRoutines.Values)
        this.StopCoroutine(enumerator);
      this.loadingRoutines.Clear();
      UIWidget refWidget = ((Component) this._transform).GetComponentInChildren<UIWidget>();
      this.bannerNum1 = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count > 1 ? 1 : MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count;
      this.SetWrapContent((Enum) EventBannerView.UI.WRP_EVENT_BANNER1, "EventBanner", this.bannerNum1, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        EventBanner eventBanner = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList[i];
        this.SetBannerEvent(t.GetChild(0), eventBanner);
        Renderer r = ((Component) t).GetComponentInChildren<Renderer>();
        refWidget.onRender += (UIDrawCall.OnRenderCallback) (mat => r.material.renderQueue = mat.renderQueue);
        r.material.color = Color.clear;
        this.SetBanner(t, EventBannerView.UI.NORMAL_CLOTH, false);
        ((Component) t).gameObject.SetActive(false);
        IEnumerator enumerator = this.LoadImg(t, eventBanner, i, this.mCenterIndex1);
        this.loadingRoutines.Add(t, enumerator);
        this.StartCoroutine(enumerator);
      }));
      this.bannerNum2 = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count - this.bannerNum1;
      if (this.bannerNum2 > 5)
        this.bannerNum2 = 5;
      this.SetWrapContent((Enum) EventBannerView.UI.WRP_EVENT_BANNER2, "EventBanner", this.bannerNum2, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        EventBanner eventBanner = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList[i + this.bannerNum1];
        this.SetBannerEvent(t.GetChild(0), eventBanner);
        Renderer r = ((Component) t).GetComponentInChildren<Renderer>();
        refWidget.onRender += (UIDrawCall.OnRenderCallback) (mat => r.material.renderQueue = mat.renderQueue);
        r.material.color = Color.clear;
        this.SetBanner(t, EventBannerView.UI.NORMAL_CLOTH, false);
        ((Component) t).gameObject.SetActive(false);
        IEnumerator enumerator = this.LoadImg(t, eventBanner, i, this.mCenterIndex2);
        this.loadingRoutines.Add(t, enumerator);
        this.StartCoroutine(enumerator);
      }));
      this.mCenterIndex1 = 0;
      this.mCenterIndex2 = 0;
      this.timer1 = 0.0f;
      this.timer2 = 0.0f;
    }
  }

  private IEnumerator LoadImg(Transform t, EventBanner banner, int index, int centerIndex)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject lo = banner.LinkType != LINK_TYPE.NEWS ? loadingQueue.Load(true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, ResourceName.GetHomeBannerImage(banner.bannerId)) : loadingQueue.Load(true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, ResourceName.GetHomeBannerImage(banner.bannerId) + "_result");
    yield return (object) loadingQueue.Wait();
    Transform ctrl = this.FindCtrl(t, (Enum) EventBannerView.UI.NORMAL_CLOTH);
    Texture2D loadedObject = lo.loadedObject as Texture2D;
    ((Component) ctrl).gameObject.SetActive(true);
    ((Component) ctrl).GetComponent<Cloth>().enabled = true;
    Renderer component = ((Component) ctrl).GetComponent<Renderer>();
    Material mat = component.material;
    mat.mainTexture = (Texture) loadedObject;
    component.material = mat;
    yield return (object) null;
    if (index == centerIndex)
    {
      mat.color = Color.white;
      ((Component) t).gameObject.SetActive(true);
    }
    this.loadingRoutines.Remove(t);
  }

  private void SetBannerEvent(Transform t, EventBanner banner)
  {
    switch (banner.LinkType)
    {
      case LINK_TYPE.PAYMENT:
        this.SetEvent(t, "BANNER_CRYSTAL_SHOP", 0);
        break;
      case LINK_TYPE.GACHA:
        this.SetEvent(t, "BANNER_GACHA", banner.param);
        break;
      case LINK_TYPE.NEWS:
        this.SetEvent(t, "BANNER_NEWS", banner.param);
        break;
      case LINK_TYPE.EVENT_DELIVERY:
        this.SetEvent(t, "BANNER_EVENT_DELIVERY", banner.param);
        break;
      case LINK_TYPE.EXPLORE_DELIVERY:
        this.SetEvent(t, "BANNER_EXPLORE_DELIVERY", banner.param);
        break;
      case LINK_TYPE.LOGIN_BONUS:
        this.SetEvent(t, "BANNER_LOGIN_BONUS", banner.param);
        break;
      default:
        this.SetEvent(t, "BANNER_NEWS", 0);
        break;
    }
  }

  public void NextBanner(int targetBanner, bool forward = true)
  {
    if (targetBanner == 1)
    {
      this.timer1 = 0.0f;
      if (this.bannerNum1 > 1)
      {
        int num = forward ? (this.mCenterIndex1 + 1) % this.bannerNum1 : (this.bannerNum1 + this.mCenterIndex1 - 1) % this.bannerNum1;
        this.StartCoroutine(this.ChangeBanner(((Component) this.mCenterOnChild1).transform.GetChild(this.mCenterIndex1), ((Component) this.mCenterOnChild1).transform.GetChild(num)));
        this.mCenterIndex1 = num;
      }
    }
    if (targetBanner != 2)
      return;
    this.timer2 = 0.0f;
    if (this.bannerNum2 <= 1)
      return;
    int num1 = forward ? (this.mCenterIndex2 + 1) % this.bannerNum2 : (this.bannerNum2 + this.mCenterIndex2 - 1) % this.bannerNum2;
    this.StartCoroutine(this.ChangeBanner(((Component) this.mCenterOnChild2).transform.GetChild(this.mCenterIndex2), ((Component) this.mCenterOnChild2).transform.GetChild(num1)));
    this.mCenterIndex2 = num1;
  }

  private void Update()
  {
    if (this.state != UIBehaviour.STATE.OPEN)
      return;
    if (Object.op_Inequality((Object) this.sctList2, (Object) null) && this.sctList2.isDragging)
    {
      this.timer2 = 0.0f;
    }
    else
    {
      this.timer2 += Time.deltaTime;
      if ((double) this.timer2 < 5.0)
        return;
      this.NextBanner(2);
    }
  }

  private IEnumerator ChangeBanner(Transform fromBanner, Transform toBanner)
  {
    Color c = Color.white;
    Renderer componentInChildren1 = ((Component) fromBanner).GetComponentInChildren<Renderer>();
    Renderer componentInChildren2 = ((Component) toBanner).GetComponentInChildren<Renderer>();
    if (!Object.op_Equality((Object) componentInChildren1, (Object) null) && !Object.op_Equality((Object) componentInChildren2, (Object) null))
    {
      Material fmat = componentInChildren1.material;
      Material tmat = componentInChildren2.material;
      tmat.color = c;
      ((Component) toBanner).GetComponentInChildren<Cloth>().enabled = true;
      ((Component) toBanner).gameObject.SetActive(true);
      fromBanner.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
      toBanner.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
      float time = 0.34f;
      while ((double) time > 0.0)
      {
        time -= Time.deltaTime;
        float num = time / 0.34f;
        c.a = num;
        fmat.color = c;
        c.a = 1f - num;
        tmat.color = c;
        yield return (object) null;
      }
      c.a = 0.0f;
      fmat.color = c;
      ((Component) fromBanner).GetComponentInChildren<Cloth>().enabled = false;
      ((Component) fromBanner).gameObject.SetActive(false);
    }
  }

  private void OnCenter1(GameObject obj) => this.mCenterIndex1 = int.Parse(((Object) obj).name);

  private void OnCenter2(GameObject obj) => this.mCenterIndex2 = int.Parse(((Object) obj).name);

  protected override void OnOpen()
  {
    this.timer1 = 0.0f;
    this.timer2 = 0.0f;
    this.updateEventBanner = true;
  }

  private void SetBanner(Transform t, EventBannerView.UI enumValue, bool enabled)
  {
    Transform ctrl = this.FindCtrl(t, (Enum) enumValue);
    ((Component) ctrl).gameObject.SetActive(enabled);
    ((Component) ctrl).GetComponent<Cloth>().enabled = enabled;
  }

  private enum UI
  {
    SCR_LIST1,
    SCR_LIST2,
    WRP_EVENT_BANNER1,
    WRP_EVENT_BANNER2,
    NORMAL_CLOTH,
  }
}
