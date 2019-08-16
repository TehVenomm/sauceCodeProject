using Network;
using System.Collections;
using UnityEngine;

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

	public bool isLoading
	{
		get;
		private set;
	}

	public HomeFeatureBanner()
		: this()
	{
	}

	public void Setup(Transform parent, Vector3 position, Quaternion rotation)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		this.parent = parent;
		this.position = position;
		this.rotation = rotation;
		Reposition();
	}

	private void Reposition()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(modelTransform))
		{
			modelTransform.set_parent(parent);
			modelTransform.set_localPosition(position);
			modelTransform.set_rotation(rotation);
			modelTransform.set_localScale(Vector3.get_one());
		}
	}

	public void SetBanner(int bannerId)
	{
		if (currentBannerId == -1 || bannerId != currentBannerId)
		{
			Load(1, bannerId);
		}
	}

	private void Load(int variation, int bannerId)
	{
		isLoading = true;
		this.StartCoroutine(_Load(variation, bannerId));
	}

	private IEnumerator _Load(int variation, int bannerId)
	{
		LoadingQueue lo = new LoadingQueue(this);
		LoadObject lo_model = null;
		LoadObject lo_tex = null;
		bool loadModel = currentVariation != variation;
		bool loadTex = currentBannerId != bannerId;
		if (loadModel)
		{
			lo_model = lo.Load(RESOURCE_CATEGORY.NPC_MODEL, string.Format(MODEL_FORMAT, variation));
		}
		if (loadTex)
		{
			lo_tex = lo.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetHomePointSHopBannerImageName(bannerId));
		}
		if (lo.IsLoading())
		{
			yield return lo.Wait();
		}
		if (loadModel)
		{
			GameObject val = Object.Instantiate(lo_model.loadedObject) as GameObject;
			modelTransform = val.get_transform();
			bannerMaterial = FindBannerMaterial(val);
			Reposition();
			SetTouchEvent(val);
			currentVariation = variation;
		}
		if (loadTex)
		{
			if (Object.op_Implicit(bannerMaterial))
			{
				bannerMaterial.set_mainTexture(lo_tex.loadedObject as Texture2D);
			}
			currentBannerId = bannerId;
		}
		isLoading = false;
	}

	private void SetTouchEvent(GameObject go)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		GameObject val = new GameObject("BANNER_TOUCH_EVENT");
		Transform transform = val.get_transform();
		transform.set_parent(go.get_transform());
		transform.set_localPosition(Vector3.get_zero());
		transform.set_localRotation(Quaternion.get_identity());
		SetCollider(val);
		touchEvent = val.AddComponent<HomeStageTouchEvent>();
	}

	private void SetCollider(GameObject go)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		CapsuleCollider val = go.AddComponent<CapsuleCollider>();
		val.set_radius(0.5f);
		val.set_height(2.2f);
		val.set_direction(0);
		val.set_center(new Vector3(0f, 0.1f, 0f));
	}

	private void SetBannerEvent(HomeStageTouchEvent touchEvent, EventBanner banner)
	{
		switch (banner.LinkType)
		{
		case LINK_TYPE.GACHA:
			touchEvent.eventName = "BANNER_GACHA";
			touchEvent.eventData = banner.param;
			break;
		case LINK_TYPE.EVENT_DELIVERY:
			touchEvent.eventName = "BANNER_EVENT_DELIVERY";
			touchEvent.eventData = banner.param;
			break;
		case LINK_TYPE.NEWS:
			touchEvent.eventName = "BANNER_NEWS";
			touchEvent.eventData = banner.param;
			break;
		case LINK_TYPE.PAYMENT:
			touchEvent.eventName = "BANNER_CRYSTAL_SHOP";
			touchEvent.eventData = 0;
			break;
		case LINK_TYPE.EXPLORE_DELIVERY:
			touchEvent.eventName = "BANNER_EXPLORE_DELIVERY";
			touchEvent.eventData = banner.param;
			break;
		case LINK_TYPE.LOGIN_BONUS:
			touchEvent.eventName = "BANNER_LOGIN_BONUS";
			touchEvent.eventData = banner.param;
			break;
		default:
			touchEvent.eventName = "BANNER_NEWS";
			touchEvent.eventData = 0;
			break;
		}
	}

	private Material FindBannerMaterial(GameObject go)
	{
		Renderer componentInChildren = go.GetComponentInChildren<Renderer>();
		int i = 0;
		for (int num = componentInChildren.get_sharedMaterials().Length; i < num; i++)
		{
			if (componentInChildren.get_sharedMaterials()[i].get_name().ToLower().EndsWith("_banner"))
			{
				return componentInChildren.get_materials()[i];
			}
		}
		return null;
	}
}
