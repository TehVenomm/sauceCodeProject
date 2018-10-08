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

	public void Setup(Transform parent, Vector3 position, Quaternion rotation)
	{
		this.parent = parent;
		this.position = position;
		this.rotation = rotation;
		Reposition();
	}

	private void Reposition()
	{
		if ((bool)modelTransform)
		{
			modelTransform.parent = parent;
			modelTransform.localPosition = position;
			modelTransform.rotation = rotation;
			modelTransform.localScale = Vector3.one;
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
		StartCoroutine(_Load(variation, bannerId));
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
			lo_model = lo.Load(RESOURCE_CATEGORY.NPC_MODEL, string.Format(MODEL_FORMAT, variation), false);
		}
		if (loadTex)
		{
			lo_tex = lo.Load(RESOURCE_CATEGORY.COMMON, ResourceName.GetHomePointSHopBannerImageName(bannerId), false);
		}
		if (lo.IsLoading())
		{
			yield return (object)lo.Wait();
		}
		if (loadModel)
		{
			GameObject model = Object.Instantiate(lo_model.loadedObject) as GameObject;
			modelTransform = model.transform;
			bannerMaterial = FindBannerMaterial(model);
			Reposition();
			SetTouchEvent(model);
			currentVariation = variation;
		}
		if (loadTex)
		{
			if ((bool)bannerMaterial)
			{
				bannerMaterial.mainTexture = (lo_tex.loadedObject as Texture2D);
			}
			currentBannerId = bannerId;
		}
		isLoading = false;
	}

	private void SetTouchEvent(GameObject go)
	{
		GameObject gameObject = new GameObject("BANNER_TOUCH_EVENT");
		Transform transform = gameObject.transform;
		transform.parent = go.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
		SetCollider(gameObject);
		touchEvent = gameObject.AddComponent<HomeStageTouchEvent>();
	}

	private void SetCollider(GameObject go)
	{
		CapsuleCollider capsuleCollider = go.AddComponent<CapsuleCollider>();
		capsuleCollider.radius = 0.5f;
		capsuleCollider.height = 2.2f;
		capsuleCollider.direction = 0;
		capsuleCollider.center = new Vector3(0f, 0.1f, 0f);
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
		for (int num = componentInChildren.sharedMaterials.Length; i < num; i++)
		{
			if (componentInChildren.sharedMaterials[i].name.ToLower().EndsWith("_banner"))
			{
				return componentInChildren.materials[i];
			}
		}
		return null;
	}
}
