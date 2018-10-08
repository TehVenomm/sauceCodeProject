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
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			if (null != camera)
			{
				Vector3 val = camera.WorldToScreenPoint(originalPos);
				val = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
				val.z = 0f;
				_transform.set_position(val);
			}
		}

		public Vector2 GetScreenPos()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val = MonoBehaviourSingleton<UIManager>.I.uiCamera.WorldToViewportPoint(_transform.get_position());
			return new Vector2(val.x, val.y);
		}

		public void SetIconSprite(string iconObjectName, Texture2D icon, int iconWidth, int iconHeight)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			Transform val = _transform.FindChild(iconObjectName);
			if (!(val == null))
			{
				UITexture component = val.get_gameObject().GetComponent<UITexture>();
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
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			Transform val = _transform.FindChild("LBL_NAME");
			if (val != null)
			{
				UILabel component = val.GetComponent<UILabel>();
				component.text = name;
			}
			Transform val2 = _transform.FindChild("SPR_ICON");
			if (!(val2 == null))
			{
				UITexture component2 = val2.get_gameObject().GetComponent<UITexture>();
				if (!(component2 == null))
				{
					component2.mainTexture = icon;
					UIGameSceneEventSender component3 = _transform.FindChild("SPR_BUTTON").GetComponent<UIGameSceneEventSender>();
					if (string.IsNullOrEmpty(eventName))
					{
						Object.Destroy(component3.get_gameObject());
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
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			Transform val = _transform.FindChild("SPR_DELIVERY_TARGET");
			if (val != null)
			{
				val.get_gameObject().SetActive(isExistDelivery);
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
			spotRootTransform = ResourceUtility.Realizes(spotRootPrehab, t, -1);
		}
		else if (t != null)
		{
			spotRootTransform.set_parent(t);
		}
		return spotRootTransform;
	}

	public Spot FindSpot(int _id)
	{
		return spots.Find((Spot c) => c.id == _id);
	}

	public void ClearAllSpot()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < spots.Count; i++)
		{
			if (spots[i] != null)
			{
				Object.Destroy(spots[i]._transform.get_gameObject());
				spots[i] = null;
			}
		}
		spots.Clear();
	}

	public void CreateSpotRoot()
	{
		if (spotRootTransform == null)
		{
			spotRootTransform = ResourceUtility.Realizes(spotRootPrehab, MonoBehaviourSingleton<UIManager>.I.uiRootTransform, -1);
		}
	}

	public Spot AddSpot(int id, string name, Vector3 pos, ICON_TYPE icon, string event_name, bool isNew = false, bool canUnlockNewPortal = false, bool viewEnemyPopBallon = false, object _event = null, Texture2D dungeon_icon = null, bool isExistDelivery = false, HAPPEN_CONDITION happenQuestCondition = HAPPEN_CONDITION.NONE, int mapNo = 0)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Expected O, but got Unknown
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		CreateSpotRoot();
		Spot spot = new Spot();
		spot.id = id;
		spot.originalPos = pos;
		spot.type = icon;
		spot.mapNo = mapNo;
		spot._transform = ResourceUtility.Realizes(spotPrefab, spotRootTransform, 5);
		Transform val = spot._transform.FindChild("LBL_NAME");
		if (val != null)
		{
			UILabel component = val.GetComponent<UILabel>();
			component.text = name;
			component.get_gameObject().SetActive(icon != ICON_TYPE.NOT_OPENED);
		}
		if (mapNo > 0)
		{
			val = spot._transform.FindChild("LBL_LOCATION_NUMBER");
			if (val != null)
			{
				val.get_gameObject().SetActive(true);
				UILabel component2 = val.get_gameObject().GetComponent<UILabel>();
				component2.text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 25u) + mapNo.ToString();
			}
		}
		val = spot._transform.FindChild("SPR_TWN_NEW");
		if (val != null)
		{
			val.get_gameObject().SetActive(isNew);
		}
		val = spot._transform.FindChild("SPR_ICON_NEW");
		if (val != null)
		{
			val.get_gameObject().SetActive(icon == ICON_TYPE.NEW);
		}
		val = spot._transform.FindChild("SPR_ICON_CLEARED");
		if (val != null)
		{
			val.get_gameObject().SetActive(icon == ICON_TYPE.CLEARED);
		}
		val = spot._transform.FindChild("SPR_ICON_HOME");
		if (val != null)
		{
			val.get_gameObject().SetActive(icon == ICON_TYPE.HOME);
		}
		val = spot._transform.FindChild("SPR_ICON_NOT_OPENED");
		if (val != null)
		{
			val.get_gameObject().SetActive(icon == ICON_TYPE.NOT_OPENED);
		}
		val = spot._transform.FindChild("SPR_ICON_HARD");
		if (val != null)
		{
			val.get_gameObject().SetActive(icon == ICON_TYPE.HARD || icon == ICON_TYPE.HARD_NEW);
			if (icon == ICON_TYPE.HARD)
			{
				Transform val2 = val.FindChild("DODAIADD");
				if (null != val2)
				{
					val2.get_gameObject().SetActive(false);
				}
			}
		}
		val = spot._transform.FindChild("OBJ_NEW_PORTAL");
		if (val != null)
		{
			val.get_gameObject().SetActive(canUnlockNewPortal);
		}
		val = spot._transform.FindChild("OBJ_POP_PORTAL");
		if (val != null)
		{
			val.get_gameObject().SetActive(viewEnemyPopBallon);
		}
		val = spot._transform.FindChild("SPR_ICON_DUNGEON");
		if (val != null)
		{
			UITexture component3 = val.GetComponent<UITexture>();
			if (component3 != null && dungeon_icon != null && icon == ICON_TYPE.CHILD_REGION)
			{
				component3.mainTexture = dungeon_icon;
			}
		}
		val = spot._transform.FindChild("SPR_DELIVERY_TARGET");
		if (val != null)
		{
			val.get_gameObject().SetActive(isExistDelivery);
		}
		val = spot._transform.FindChild("SPR_SUBMISSION_CLEARED");
		if (val != null)
		{
			val.get_gameObject().SetActive(happenQuestCondition == HAPPEN_CONDITION.ALL_CLEAR);
		}
		val = spot._transform.FindChild("SPR_SUBMISSION_NOT_CLEARED");
		if (val != null)
		{
			val.get_gameObject().SetActive(happenQuestCondition == HAPPEN_CONDITION.NOT_CLEAR);
		}
		UIGameSceneEventSender component4 = spot._transform.FindChild("SPR_BUTTON").GetComponent<UIGameSceneEventSender>();
		if (string.IsNullOrEmpty(event_name))
		{
			Object.Destroy(component4.get_gameObject());
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
