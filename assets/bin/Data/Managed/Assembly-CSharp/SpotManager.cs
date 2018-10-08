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
			if ((UnityEngine.Object)null != (UnityEngine.Object)camera)
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
			Transform transform = _transform.FindChild(iconObjectName);
			if (!((UnityEngine.Object)transform == (UnityEngine.Object)null))
			{
				UITexture component = transform.gameObject.GetComponent<UITexture>();
				if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
				{
					component.mainTexture = icon;
					component.width = iconWidth;
					component.height = iconHeight;
				}
			}
		}

		public void ReleaseRegion(string name, Texture2D icon, string eventName)
		{
			Transform transform = _transform.FindChild("LBL_NAME");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				UILabel component = transform.GetComponent<UILabel>();
				component.text = name;
			}
			Transform transform2 = _transform.FindChild("SPR_ICON");
			if (!((UnityEngine.Object)transform2 == (UnityEngine.Object)null))
			{
				UITexture component2 = transform2.gameObject.GetComponent<UITexture>();
				if (!((UnityEngine.Object)component2 == (UnityEngine.Object)null))
				{
					component2.mainTexture = icon;
					UIGameSceneEventSender component3 = _transform.FindChild("SPR_BUTTON").GetComponent<UIGameSceneEventSender>();
					if (string.IsNullOrEmpty(eventName))
					{
						UnityEngine.Object.Destroy(component3.gameObject);
					}
					else
					{
						component3.eventName = eventName;
					}
				}
			}
		}

		public void UpdateDeliveryTargetMarker(bool isExistDelivery)
		{
			Transform transform = _transform.FindChild("SPR_DELIVERY_TARGET");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)spotRootTransform == (UnityEngine.Object)null)
		{
			spotRootTransform = ResourceUtility.Realizes(spotRootPrehab, t, -1);
		}
		else if ((UnityEngine.Object)t != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)spotRootTransform == (UnityEngine.Object)null)
		{
			spotRootTransform = ResourceUtility.Realizes(spotRootPrehab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform, -1);
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
		Transform transform = spot._transform.FindChild("LBL_NAME");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			UILabel component = transform.GetComponent<UILabel>();
			component.text = name;
			component.gameObject.SetActive(icon != ICON_TYPE.NOT_OPENED);
		}
		if (mapNo > 0)
		{
			transform = spot._transform.FindChild("LBL_LOCATION_NUMBER");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				transform.gameObject.SetActive(true);
				UILabel component2 = transform.gameObject.GetComponent<UILabel>();
				component2.text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 25u) + mapNo.ToString();
			}
		}
		transform = spot._transform.FindChild("SPR_TWN_NEW");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(isNew);
		}
		transform = spot._transform.FindChild("SPR_ICON_NEW");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.NEW);
		}
		transform = spot._transform.FindChild("SPR_ICON_CLEARED");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.CLEARED);
		}
		transform = spot._transform.FindChild("SPR_ICON_HOME");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.HOME);
		}
		transform = spot._transform.FindChild("SPR_ICON_NOT_OPENED");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.NOT_OPENED);
		}
		transform = spot._transform.FindChild("SPR_ICON_HARD");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(icon == ICON_TYPE.HARD || icon == ICON_TYPE.HARD_NEW);
			if (icon == ICON_TYPE.HARD)
			{
				Transform transform2 = transform.FindChild("DODAIADD");
				if ((UnityEngine.Object)null != (UnityEngine.Object)transform2)
				{
					transform2.gameObject.SetActive(false);
				}
			}
		}
		transform = spot._transform.FindChild("OBJ_NEW_PORTAL");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(canUnlockNewPortal);
		}
		transform = spot._transform.FindChild("OBJ_POP_PORTAL");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(viewEnemyPopBallon);
		}
		transform = spot._transform.FindChild("SPR_ICON_DUNGEON");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			UITexture component3 = transform.GetComponent<UITexture>();
			if ((UnityEngine.Object)component3 != (UnityEngine.Object)null && (UnityEngine.Object)dungeon_icon != (UnityEngine.Object)null && icon == ICON_TYPE.CHILD_REGION)
			{
				component3.mainTexture = dungeon_icon;
			}
		}
		transform = spot._transform.FindChild("SPR_DELIVERY_TARGET");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(isExistDelivery);
		}
		transform = spot._transform.FindChild("SPR_SUBMISSION_CLEARED");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(happenQuestCondition == HAPPEN_CONDITION.ALL_CLEAR);
		}
		transform = spot._transform.FindChild("SPR_SUBMISSION_NOT_CLEARED");
		if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
		{
			transform.gameObject.SetActive(happenQuestCondition == HAPPEN_CONDITION.NOT_CLEAR);
		}
		UIGameSceneEventSender component4 = spot._transform.FindChild("SPR_BUTTON").GetComponent<UIGameSceneEventSender>();
		if (string.IsNullOrEmpty(event_name))
		{
			UnityEngine.Object.Destroy(component4.gameObject);
		}
		else
		{
			component4.eventName = event_name;
			component4.eventData = _event;
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
