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

	private const int SHOW_BANNER1_NUM = 1;

	private const int SHOW_BANNER2_NUM = 5;

	private const float BANNER_AUTO_SCROLL_INTERVAL = 5f;

	private Dictionary<Transform, IEnumerator> loadingRoutines = new Dictionary<Transform, IEnumerator>();

	private UIScrollView sctList1;

	private UIScrollView sctList2;

	private GameObject mEventBannerPrefab;

	private UICenterOnChild mCenterOnChild1;

	private UICenterOnChild mCenterOnChild2;

	private GameObject[] indexList;

	private int bannerNum1;

	private int bannerNum2;

	private bool updateEventBanner = true;

	private int mCenterIndex1;

	private int mCenterIndex2;

	private float timer1;

	private float timer2;

	protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return GameSection.NOTIFY_FLAG.UPDATE_EVENT_BANNER;
	}

	private IEnumerator Start()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_event_banner = load_queue.LoadAndInstantiate(RESOURCE_CATEGORY.UI, "EventBanner");
		if (load_queue.IsLoading())
		{
			yield return (object)load_queue.Wait();
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
			sctList1 = base.GetComponent<UIScrollView>((Enum)UI.SCR_LIST1);
			sctList2 = base.GetComponent<UIScrollView>((Enum)UI.SCR_LIST2);
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

	private unsafe void UpdateEventBannerAll()
	{
		if (updateEventBanner)
		{
			updateEventBanner = false;
			if (MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList == null || MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count <= 0)
			{
				Close(UITransition.TYPE.CLOSE);
			}
			else
			{
				GetCtrl(UI.WRP_EVENT_BANNER1).DestroyChildren();
				GetCtrl(UI.WRP_EVENT_BANNER2).DestroyChildren();
				foreach (IEnumerator value in loadingRoutines.Values)
				{
					this.StopCoroutine(value);
				}
				loadingRoutines.Clear();
				UIWidget refWidget = base._transform.GetComponentInChildren<UIWidget>();
				bannerNum1 = ((MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count > 1) ? 1 : MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count);
				_003CUpdateEventBannerAll_003Ec__AnonStorey2DE _003CUpdateEventBannerAll_003Ec__AnonStorey2DE;
				SetWrapContent(UI.WRP_EVENT_BANNER1, "EventBanner", bannerNum1, true, new Action<int, Transform, bool>((object)_003CUpdateEventBannerAll_003Ec__AnonStorey2DE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				bannerNum2 = MonoBehaviourSingleton<UserInfoManager>.I.eventBannerList.Count - bannerNum1;
				if (bannerNum2 > 5)
				{
					bannerNum2 = 5;
				}
				SetWrapContent(UI.WRP_EVENT_BANNER2, "EventBanner", bannerNum2, true, new Action<int, Transform, bool>((object)_003CUpdateEventBannerAll_003Ec__AnonStorey2DE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				mCenterIndex1 = 0;
				mCenterIndex2 = 0;
				timer1 = 0f;
				timer2 = 0f;
			}
		}
	}

	private IEnumerator LoadImg(Transform t, EventBanner banner, int index, int centerIndex)
	{
		LoadingQueue load = new LoadingQueue(this);
		LoadObject lo;
		if (banner.LinkType == LINK_TYPE.NEWS)
		{
			if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest != null)
			{
				Hash128 assetBundleHash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.HOME_BANNER_IMAGE.ToAssetBundleName(ResourceName.GetHomeBannerImage(banner.bannerId) + "_result"));
				if (assetBundleHash.get_isValid())
				{
					lo = load.Load(true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, ResourceName.GetHomeBannerImage(banner.bannerId) + "_result", false);
					goto IL_01c2;
				}
			}
			lo = load.Load(true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, "HBI_Default", false);
			Log.Error("Missing HBI image: " + ResourceName.GetHomeBannerImage(banner.bannerId));
		}
		else
		{
			if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest != null)
			{
				Hash128 assetBundleHash2 = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.HOME_BANNER_IMAGE.ToAssetBundleName(ResourceName.GetHomeBannerImage(banner.bannerId)));
				if (assetBundleHash2.get_isValid())
				{
					lo = load.Load(true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, ResourceName.GetHomeBannerImage(banner.bannerId), false);
					goto IL_01c2;
				}
			}
			lo = load.Load(true, RESOURCE_CATEGORY.HOME_BANNER_IMAGE, "HBI_Default", false);
			Log.Error("Missing HBI image: " + ResourceName.GetHomeBannerImage(banner.bannerId));
		}
		goto IL_01c2;
		IL_01c2:
		yield return (object)load.Wait();
		Transform bannerTrans = FindCtrl(t, UI.NORMAL_CLOTH);
		Texture2D tex = lo.loadedObject as Texture2D;
		bannerTrans.get_gameObject().SetActive(true);
		bannerTrans.GetComponent<Cloth>().set_enabled(true);
		Renderer r = bannerTrans.GetComponent<Renderer>();
		Material mat = r.get_material();
		mat.set_mainTexture(tex);
		r.set_material(mat);
		yield return (object)null;
		if (index == centerIndex)
		{
			mat.set_color(Color.get_white());
			t.get_gameObject().SetActive(true);
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
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0078: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0102: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if (targetBanner == 1)
		{
			timer1 = 0f;
			if (bannerNum1 > 1)
			{
				int num = (!forward) ? ((bannerNum1 + mCenterIndex1 - 1) % bannerNum1) : ((mCenterIndex1 + 1) % bannerNum1);
				this.StartCoroutine(ChangeBanner(mCenterOnChild1.get_transform().GetChild(mCenterIndex1), mCenterOnChild1.get_transform().GetChild(num)));
				mCenterIndex1 = num;
			}
		}
		if (targetBanner == 2)
		{
			timer2 = 0f;
			if (bannerNum2 > 1)
			{
				int num2 = (!forward) ? ((bannerNum2 + mCenterIndex2 - 1) % bannerNum2) : ((mCenterIndex2 + 1) % bannerNum2);
				this.StartCoroutine(ChangeBanner(mCenterOnChild2.get_transform().GetChild(mCenterIndex2), mCenterOnChild2.get_transform().GetChild(num2)));
				mCenterIndex2 = num2;
			}
		}
	}

	private void Update()
	{
		if (base.state == STATE.OPEN)
		{
			if (sctList2 != null && sctList2.isDragging)
			{
				timer2 = 0f;
			}
			else
			{
				timer2 += Time.get_deltaTime();
				if (timer2 >= 5f)
				{
					NextBanner(2, true);
				}
			}
		}
	}

	private IEnumerator ChangeBanner(Transform fromBanner, Transform toBanner)
	{
		Color c = Color.get_white();
		Renderer fr = fromBanner.GetComponentInChildren<Renderer>();
		Renderer tr = toBanner.GetComponentInChildren<Renderer>();
		if (!(fr == null) && !(tr == null))
		{
			Material fmat = fr.get_material();
			Material tmat = tr.get_material();
			tmat.set_color(c);
			toBanner.GetComponentInChildren<Cloth>().set_enabled(true);
			toBanner.get_gameObject().SetActive(true);
			fromBanner.set_localPosition(new Vector3(0f, 0f, 0f));
			toBanner.set_localPosition(new Vector3(0f, 0f, 0f));
			float time = 0.34f;
			while (time > 0f)
			{
				time -= Time.get_deltaTime();
				float alpha = c.a = time / 0.34f;
				fmat.set_color(c);
				c.a = 1f - alpha;
				tmat.set_color(c);
				yield return (object)null;
			}
			c.a = 0f;
			fmat.set_color(c);
			fromBanner.GetComponentInChildren<Cloth>().set_enabled(false);
			fromBanner.get_gameObject().SetActive(false);
		}
	}

	private void OnCenter1(GameObject obj)
	{
		mCenterIndex1 = int.Parse(obj.get_name());
	}

	private void OnCenter2(GameObject obj)
	{
		mCenterIndex2 = int.Parse(obj.get_name());
	}

	protected override void OnOpen()
	{
		timer1 = 0f;
		timer2 = 0f;
		updateEventBanner = true;
	}

	private void SetBanner(Transform t, UI enumValue, bool enabled)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Transform val = FindCtrl(t, enumValue);
		val.get_gameObject().SetActive(enabled);
		val.GetComponent<Cloth>().set_enabled(enabled);
	}
}
