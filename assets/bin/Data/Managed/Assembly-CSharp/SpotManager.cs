using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpotManager
{
	public enum ICON_TYPE
	{
		NEW,
		CLEARED,
		HOME,
		NOT_OPENED,
		HARD,
		HARD_NEW,
		CHILD_REGION,
		INVISIBLE
	}

	public enum HAPPEN_CONDITION
	{
		ALL_CLEAR,
		NOT_CLEAR,
		NONE
	}

	[Serializable]
	public class Spot
	{
		public int id;

		public Vector3 originalPos;

		public Transform _transform;

		public ICON_TYPE type;

		public int mapNo;

		public void Update(Camera camera)
		{
			if (null != camera)
			{
				Vector3 position = camera.WorldToScreenPoint(originalPos);
				position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
				position.z = 0f;
				_transform.position = position;
			}
		}

		public Vector2 GetScreenPos()
		{
			Vector3 vector = MonoBehaviourSingleton<UIManager>.I.uiCamera.WorldToViewportPoint(_transform.position);
			return new Vector2(vector.x, vector.y);
		}

		public void SetIconSprite(string iconObjectName, Texture2D icon, int iconWidth, int iconHeight)
		{
			Transform transform = _transform.Find(iconObjectName);
			if (!(transform == null))
			{
				UITexture component = transform.gameObject.GetComponent<UITexture>();
				if (!(component == null))
				{
					component.mainTexture = icon;
					component.width = iconWidth;
					component.height = iconHeight;
				}
			}
		}

		public void ReleaseRegion(string name, Texture2D icon, string eventName)
		{
			Transform transform = _transform.Find("LBL_NAME");
			if (transform != null)
			{
				transform.GetComponent<UILabel>().text = name;
			}
			Transform transform2 = _transform.Find("SPR_ICON");
			if (transform2 == null)
			{
				return;
			}
			UITexture component = transform2.gameObject.GetComponent<UITexture>();
			if (!(component == null))
			{
				component.mainTexture = icon;
				UIGameSceneEventSender component2 = _transform.Find("SPR_BUTTON").GetComponent<UIGameSceneEventSender>();
				if (string.IsNullOrEmpty(eventName))
				{
					UnityEngine.Object.Destroy(component2.gameObject);
				}
				else
				{
					component2.eventName = eventName;
				}
			}
		}

		public void UpdateDeliveryTargetMarker(bool isExistDelivery)
		{
			Transform transform = _transform.Find("SPR_DELIVERY_TARGET");
			if (transform != null)
			{
				transform.gameObject.SetActive(isExistDelivery);
			}
		}
	}

	[SerializeField]
	private List<Spot> spots = new List<Spot>();

	private GameObject spotRootPrehab;

	private GameObject spotPrefab;

	private Camera targetCamera;

	public Transform spotRootTransform
	{
		get;
		set;
	}

	public int Count => spots.Count;

	public SpotManager(GameObject _spotRootPrefab, GameObject _spotPrefab, Camera _targetCamera)
	{
		spotRootPrehab = _spotRootPrefab;
		spotPrefab = _spotPrefab;
		targetCamera = _targetCamera;
	}

	public Transform SetRoot(Transform t)
	{
		if (spotRootTransform == null)
		{
			spotRootTransform = ResourceUtility.Realizes(spotRootPrehab, t);
		}
		else if (t != null)
		{
			spotRootTransform.parent = t;
		}
		return spotRootTransform;
	}

	public Spot FindSpot(int _id)
	{
		return spots.Find((Spot c) => c.id == _id);
	}

	public void ClearAllSpot()
	{
		for (int i = 0; i < spots.Count; i++)
		{
			if (spots[i] != null)
			{
				UnityEngine.Object.Destroy(spots[i]._transform.gameObject);
				spots[i] = null;
			}
		}
		spots.Clear();
	}

	public void CreateSpotRoot()
	{
		if (spotRootTransform == null)
		{
			spotRootTransform = ResourceUtility.Realizes(spotRootPrehab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform);
		}
	}

	public Spot AddSpot(int id, string name, Vector3 pos, ICON_TYPE icon, string event_name, bool isNew = false, bool canUnlockNewPortal = false, bool viewEnemyPopBallon = false, object _event = null, Texture2D dungeon_icon = null, bool isExistDelivery = false, HAPPEN_CONDITION happenQuestCondition = HAPPEN_CONDITION.NONE, int mapNo = 0)
	{
		CreateSpotRoot();
		Spot spot = new Spot();
		spot.id = id;
		spot.originalPos = pos;
		spot.type = icon;
		spot.mapNo = mapNo;
		spot._transform = ResourceUtility.Realizes(spotPrefab, spotRootTransform, 5);
		Transform transform = spot._transform.Find("LBL_NAME");
		if (transform != null)
		{
			UILabel component = transform.GetComponent<UILabel>();
			component.text = name;
			component.gameObject.SetActive(icon != ICON_TYPE.NOT_OPENED);
			Transform transform2 = transform.Find("SPR_NAME_BASE");
			if (transform2 != null)
			{
				transform2.GetComponent<UITexture>().width = component.width + 45;
			}
		}
		if (mapNo > 0)
		{
			transform = spot._transform.Find("LBL_LOCATION_NUMBER");
			if (transform != null)
			{
				transform.gameObject.SetActive(value: true);
				transform.gameObject.GetComponent<UILabel>().text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 25u) + mapNo.ToString();
			}
		}
		transform = spot._transform.Find("SPR_TWN_NEW");
		if (transform != null)
		{
			transform.gameObject.SetActive(isNew);
		}
		transform = spot._transform.Find("SPR_ICON_NEW");
		if (transform != null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.NEW);
		}
		transform = spot._transform.Find("SPR_ICON_CLEARED");
		if (transform != null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.CLEARED);
		}
		transform = spot._transform.Find("SPR_ICON_HOME");
		if (transform != null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.HOME);
		}
		transform = spot._transform.Find("SPR_ICON_NOT_OPENED");
		if (transform != null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.NOT_OPENED);
		}
		transform = spot._transform.Find("SPR_ICON_HARD");
		if (transform != null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.HARD || icon == ICON_TYPE.HARD_NEW);
			if (icon == ICON_TYPE.HARD)
			{
				Transform transform3 = transform.Find("DODAIADD");
				if (null != transform3)
				{
					transform3.gameObject.SetActive(value: false);
				}
			}
		}
		transform = spot._transform.Find("OBJ_NEW_PORTAL");
		if (transform != null)
		{
			transform.gameObject.SetActive(canUnlockNewPortal);
		}
		transform = spot._transform.Find("OBJ_POP_PORTAL");
		if (transform != null)
		{
			transform.gameObject.SetActive(viewEnemyPopBallon);
		}
		transform = spot._transform.Find("SPR_ICON_DUNGEON");
		if (transform != null)
		{
			UITexture component2 = transform.GetComponent<UITexture>();
			if (component2 != null && dungeon_icon != null && icon == ICON_TYPE.CHILD_REGION)
			{
				component2.mainTexture = dungeon_icon;
			}
		}
		transform = spot._transform.Find("SPR_DELIVERY_TARGET");
		if (transform != null)
		{
			transform.gameObject.SetActive(isExistDelivery);
		}
		transform = spot._transform.Find("SPR_SUBMISSION_CLEARED");
		if (transform != null)
		{
			transform.gameObject.SetActive(happenQuestCondition == HAPPEN_CONDITION.ALL_CLEAR);
		}
		transform = spot._transform.Find("SPR_SUBMISSION_NOT_CLEARED");
		if (transform != null)
		{
			transform.gameObject.SetActive(happenQuestCondition == HAPPEN_CONDITION.NOT_CLEAR);
		}
		UIGameSceneEventSender component3 = spot._transform.Find("SPR_BUTTON").GetComponent<UIGameSceneEventSender>();
		if (string.IsNullOrEmpty(event_name))
		{
			UnityEngine.Object.Destroy(component3.gameObject);
		}
		else
		{
			component3.eventName = event_name;
			component3.eventData = _event;
		}
		spots.Add(spot);
		return spot;
	}

	public void Update()
	{
		spots.ForEach(delegate(Spot spot)
		{
			spot.Update(targetCamera);
		});
	}

	public List<Spot> GetAllSpots()
	{
		return spots;
	}

	public Spot GetSpot(int regionId)
	{
		foreach (Spot spot in spots)
		{
			if (spot.id == regionId)
			{
				return spot;
			}
		}
		return null;
	}
}
