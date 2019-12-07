using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBannerView : UIBehaviour
{
	private enum UI
	{
		SCR_LIST1,
		SCR_LIST2,
		WRP_EVENT_BANNER1,
		WRP_EVENT_BANNER2,
		NORMAL_CLOTH
	}

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
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo_event_banner = loadingQueue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, "EventBanner");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		mEventBannerPrefab = (lo_event_banner.loadedObject as GameObject);
		AddPrefab(mEventBannerPrefab, lo_event_banner.PopInstantiatedGameObject());
		mCenterOnChild1 = GetCtrl(UI.WRP_EVENT_BANNER1).GetComponent<UICenterOnChild>();
		mCenterOnChild1.onCenter = OnCenter1;
		mCenterOnChild2 = GetCtrl(UI.WRP_EVENT_BANNER2).GetComponent<UICenterOnChild>();
		mCenterOnChild2.onCenter = OnCenter2;
	}

	public override void UpdateUI()
	{
		if (!(mEventBannerPrefab == null))
		{
			UpdateEventBannerAll();
			sctList1 = GetComponent<UIScrollView>(UI.SCR_LIST1);
			sctList2 = GetComponent<UIScrollView>(UI.SCR_LIST2);
			base.UpdateUI();
		}
	}

	public override void OnNotify(GameSection.NOTIFY_FLAG flags)
	{
		if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER) != (GameSection.NOTIFY_FLAG)0L)
		{
			updateEventBanner = true;
		}
		base.OnNotify(flags);
	}

	private void UpdateEventBannerAll()
	{
		if (!updateEventBanner)
		{
			return;
		}
		updateEventBanner = false;
		if (MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count <= 0)
		{
			Close();
			return;
		}
		GetCtrl(UI.WRP_EVENT_BANNER1).DestroyChildren();
		GetCtrl(UI.WRP_EVENT_BANNER2).DestroyChildren();
		foreach (IEnumerator value in loadingRoutines.Values)
		{
			StopCoroutine(value);
		}
		loadingRoutines.Clear();
		UIWidget refWidget = base._transform.GetComponentInChildren<UIWidget>();
		bannerNum1 = ((MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count > 1) ? 1 : MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count);
		SetWrapContent(UI.WRP_EVENT_BANNER1, "EventBanner", bannerNum1, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			EventBanner banner2 = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList[i];
			SetBannerEvent(t.GetChild(0), banner2);
			Renderer r2 = t.GetComponentInChildren<Renderer>();
			UIWidget uIWidget2 = refWidget;
			uIWidget2.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIWidget2.onRender, (UIDrawCall.OnRenderCallback)delegate(Material mat)
			{
				r2.material.renderQueue = mat.renderQueue;
			});
			r2.material.color = Color.clear;
			SetBanner(t, UI.NORMAL_CLOTH, enabled: false);
			t.gameObject.SetActive(value: false);
			IEnumerator enumerator3 = LoadImg(t, banner2, i, mCenterIndex1);
			loadingRoutines.Add(t, enumerator3);
			StartCoroutine(enumerator3);
		});
		bannerNum2 = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count - bannerNum1;
		if (bannerNum2 > 5)
		{
			bannerNum2 = 5;
		}
		SetWrapContent(UI.WRP_EVENT_BANNER2, "EventBanner", bannerNum2, reset: true, delegate(int i, Transform t, bool is_recycle)
		{
			EventBanner banner = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList[i + bannerNum1];
			SetBannerEvent(t.GetChild(0), banner);
			Renderer r = t.GetComponentInChildren<Renderer>();
			UIWidget uIWidget = refWidget;
			uIWidget.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uIWidget.onRender, (UIDrawCall.OnRenderCallback)delegate(Material mat)
			{
				r.material.renderQueue = mat.renderQueue;
			});
			r.material.color = Color.clear;
			SetBanner(t, UI.NORMAL_CLOTH, enabled: false);
			t.gameObject.SetActive(value: false);
			IEnumerator enumerator2 = LoadImg(t, banner, i, mCenterIndex2);
			loadingRoutines.Add(t, enumerator2);
			StartCoroutine(enumerator2);
		});
		mCenterIndex1 = 0;
		mCenterIndex2 = 0;
		timer1 = 0f;
		timer2 = 0f;
	}

	private IEnumerator LoadImg(Transform t, EventBanner banner, int index, int centerIndex)
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject lo = (banner.LinkType != LINK_TYPE.NEWS) ? loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, ResourceName.GetHomeBannerImage(banner.bannerId)) : loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, ResourceName.GetHomeBannerImage(banner.bannerId) + "_result");
		yield return loadingQueue.Wait();
		Transform transform = FindCtrl(t, UI.NORMAL_CLOTH);
		Texture2D mainTexture = lo.loadedObject as Texture2D;
		transform.gameObject.SetActive(value: true);
		transform.GetComponent<Cloth>().enabled = true;
		Renderer component = transform.GetComponent<Renderer>();
		Material mat = component.material;
		mat.mainTexture = mainTexture;
		component.material = mat;
		yield return null;
		if (index == centerIndex)
		{
			mat.color = Color.white;
			t.gameObject.SetActive(value: true);
		}
		loadingRoutines.Remove(t);
	}

	private void SetBannerEvent(Transform t, EventBanner banner)
	{
		switch (banner.LinkType)
		{
		case LINK_TYPE.GACHA:
			SetEvent(t, "BANNER_GACHA", banner.param);
			break;
		case LINK_TYPE.EVENT_DELIVERY:
			SetEvent(t, "BANNER_EVENT_DELIVERY", banner.param);
			break;
		case LINK_TYPE.NEWS:
			SetEvent(t, "BANNER_NEWS", banner.param);
			break;
		case LINK_TYPE.PAYMENT:
			SetEvent(t, "BANNER_CRYSTAL_SHOP", 0);
			break;
		case LINK_TYPE.EXPLORE_DELIVERY:
			SetEvent(t, "BANNER_EXPLORE_DELIVERY", banner.param);
			break;
		case LINK_TYPE.LOGIN_BONUS:
			SetEvent(t, "BANNER_LOGIN_BONUS", banner.param);
			break;
		default:
			SetEvent(t, "BANNER_NEWS", 0);
			break;
		}
	}

	public void NextBanner(int targetBanner, bool forward = true)
	{
		if (targetBanner == 1)
		{
			timer1 = 0f;
			if (bannerNum1 > 1)
			{
				int index = forward ? ((mCenterIndex1 + 1) % bannerNum1) : ((bannerNum1 + mCenterIndex1 - 1) % bannerNum1);
				StartCoroutine(ChangeBanner(mCenterOnChild1.transform.GetChild(mCenterIndex1), mCenterOnChild1.transform.GetChild(index)));
				mCenterIndex1 = index;
			}
		}
		if (targetBanner == 2)
		{
			timer2 = 0f;
			if (bannerNum2 > 1)
			{
				int index2 = forward ? ((mCenterIndex2 + 1) % bannerNum2) : ((bannerNum2 + mCenterIndex2 - 1) % bannerNum2);
				StartCoroutine(ChangeBanner(mCenterOnChild2.transform.GetChild(mCenterIndex2), mCenterOnChild2.transform.GetChild(index2)));
				mCenterIndex2 = index2;
			}
		}
	}

	private void Update()
	{
		if (base.state != STATE.OPEN)
		{
			return;
		}
		if (sctList2 != null && sctList2.isDragging)
		{
			timer2 = 0f;
			return;
		}
		timer2 += Time.deltaTime;
		if (timer2 >= 5f)
		{
			NextBanner(2);
		}
	}

	private IEnumerator ChangeBanner(Transform fromBanner, Transform toBanner)
	{
		Color c = Color.white;
		Renderer componentInChildren = fromBanner.GetComponentInChildren<Renderer>();
		Renderer componentInChildren2 = toBanner.GetComponentInChildren<Renderer>();
		if (!(componentInChildren == null) && !(componentInChildren2 == null))
		{
			Material fmat = componentInChildren.material;
			Material tmat = componentInChildren2.material;
			tmat.color = c;
			toBanner.GetComponentInChildren<Cloth>().enabled = true;
			toBanner.gameObject.SetActive(value: true);
			fromBanner.localPosition = new Vector3(0f, 0f, 0f);
			toBanner.localPosition = new Vector3(0f, 0f, 0f);
			float time = 0.34f;
			while (time > 0f)
			{
				time -= Time.deltaTime;
				float num = c.a = time / 0.34f;
				fmat.color = c;
				c.a = 1f - num;
				tmat.color = c;
				yield return null;
			}
			c.a = 0f;
			fmat.color = c;
			fromBanner.GetComponentInChildren<Cloth>().enabled = false;
			fromBanner.gameObject.SetActive(value: false);
		}
	}

	private void OnCenter1(GameObject obj)
	{
		mCenterIndex1 = int.Parse(obj.name);
	}

	private void OnCenter2(GameObject obj)
	{
		mCenterIndex2 = int.Parse(obj.name);
	}

	protected override void OnOpen()
	{
		timer1 = 0f;
		timer2 = 0f;
		updateEventBanner = true;
	}

	private void SetBanner(Transform t, UI enumValue, bool enabled)
	{
		Transform transform = FindCtrl(t, enumValue);
		transform.gameObject.SetActive(enabled);
		transform.GetComponent<Cloth>().enabled = enabled;
	}
}
