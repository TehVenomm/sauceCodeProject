using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExploreMapRoot : MonoBehaviour
{
	[SerializeField]
	private float _portraitScale = 1f;

	[SerializeField]
	private float _landscapeScale = 1f;

	[SerializeField]
	private ExploreMapLocation[] _locations;

	[SerializeField]
	private Transform[] _portals;

	[SerializeField]
	private UITexture map;

	[SerializeField]
	private Color unusedColor = Color.black;

	[SerializeField]
	private Color passedColor = Color.black;

	[SerializeField]
	private Color warpColor = Color.black;

	[SerializeField]
	private float _portraitSonarOffset = 0.9f;

	[SerializeField]
	private float _landscaleSonarOffset = 0.9f;

	[SerializeField]
	private Vector2 _portraitSonarScale = new Vector2(0.6f, 0.6f);

	[SerializeField]
	private Vector2 _landscaleSonarScale = new Vector2(0.42f, 0.42f);

	[SerializeField]
	private float _portraitSonarFov = 100f;

	[SerializeField]
	private float _landscapeSonarFov = 40f;

	[SerializeField]
	private Color _sonarBackGroundColor = new Color(0.63f, 0.63f, 0.63f, 0f);

	private bool isSetup;

	public ExploreMapLocation[] locations => _locations;

	public Transform[] portals => _portals;

	public UITexture mapTexture => map;

	public float landscapeSonarFov => _landscapeSonarFov;

	public Color sonarBackGroundColor => _sonarBackGroundColor;

	public bool showBattleMarker
	{
		get;
		private set;
	}

	public GameObject directionSonar
	{
		get;
		private set;
	}

	public bool IsSetup
	{
		get
		{
			return isSetup;
		}
		set
		{
			isSetup = value;
		}
	}

	public ExploreMapLocation FindLocation(int id)
	{
		return Array.Find(locations, (ExploreMapLocation l) => l.mapId == id);
	}

	public Transform FindPortalNode(int mapId0, int mapId1)
	{
		ExploreMapLocation exploreMapLocation = FindLocation(mapId0);
		ExploreMapLocation exploreMapLocation2 = FindLocation(mapId1);
		if (null == exploreMapLocation || null == exploreMapLocation2)
		{
			return null;
		}
		int locationIndex = GetLocationIndex(exploreMapLocation.name);
		int locationIndex2 = GetLocationIndex(exploreMapLocation2.name);
		int num = Mathf.Min(locationIndex, locationIndex2);
		string str2 = string.Concat(str3: Mathf.Max(locationIndex, locationIndex2).ToString(), str0: "Portal", str1: num.ToString(), str2: "_");
		return base.transform.Find("Road/" + str2);
	}

	public Transform FindNode(int mapId, out Vector3 offset, out bool isBattle)
	{
		offset = new Vector3(0f, 0f, 0f);
		isBattle = false;
		ExploreMapLocation exploreMapLocation = FindLocation(mapId);
		if (null != exploreMapLocation)
		{
			return exploreMapLocation.transform;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId() == mapId)
		{
			isBattle = true;
			exploreMapLocation = FindLocation(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId());
			if (exploreMapLocation != null)
			{
				return exploreMapLocation.transform;
			}
		}
		return null;
	}

	public void UpdatePortals(bool isMiniMap)
	{
		for (int i = 0; i < portals.Length; i++)
		{
			Transform transform = portals[i];
			string name = transform.name;
			FieldMapTable.PortalTableData portalData = GetPortalData(name);
			if (portalData == null)
			{
				continue;
			}
			transform.gameObject.SetActive(value: true);
			UITexture[] componentsInChildren = transform.GetComponentsInChildren<UITexture>();
			if (componentsInChildren == null || componentsInChildren.Length == 0)
			{
				transform.gameObject.SetActive(value: false);
			}
			else if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal(portalData.portalID))
			{
				componentsInChildren[0].color = passedColor;
				if (portalData.IsWarpPortal())
				{
					componentsInChildren[0].color = warpColor;
				}
			}
			else
			{
				componentsInChildren[0].color = unusedColor;
				transform.gameObject.SetActive(isMiniMap);
			}
		}
	}

	public void SetMarkers(Transform[] markers, bool isMiniMap)
	{
		int[] exploreDisplayIndices = MonoBehaviourSingleton<QuestManager>.I.GetExploreDisplayIndices();
		for (int i = 0; i < markers.Length; i++)
		{
			int num = exploreDisplayIndices[i];
			int exploreMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreMapId(i);
			if (0 > exploreMapId)
			{
				continue;
			}
			Transform transform = markers[num];
			Vector3 offset;
			bool isBattle;
			Transform transform2 = FindNode(exploreMapId, out offset, out isBattle);
			if (null != transform2)
			{
				transform.gameObject.SetActive(value: true);
				Utility.Attach(transform2, transform.transform);
				if (isMiniMap)
				{
					transform.GetComponent<ExplorePlayerMarkerMini>().SetIndex(num);
				}
				else
				{
					transform.GetComponent<ExplorePlayerMarker>().SetIndex(num);
				}
				transform.localPosition += offset;
				showBattleMarker |= isBattle;
			}
		}
	}

	public Vector3 GetPositionOnMap(int mapId)
	{
		if (mapId < 0)
		{
			return Vector3.zero;
		}
		Vector3 offset;
		bool isBattle;
		Transform transform = FindNode(mapId, out offset, out isBattle);
		if (transform == null)
		{
			return Vector3.zero;
		}
		return transform.localPosition;
	}

	private static int GetLocationIndex(string name)
	{
		return int.Parse(name.Replace("Location", ""));
	}

	public int[] GetMapIDsFromLocationNumbers(int[] numbers)
	{
		ExploreMapLocation exploreMapLocation = locations[numbers[0]];
		ExploreMapLocation exploreMapLocation2 = locations[numbers[1]];
		return new int[2]
		{
			exploreMapLocation.mapId,
			exploreMapLocation2.mapId
		};
	}

	public FieldMapTable.PortalTableData GetPortalData(string portalName)
	{
		int[] locationNumbers = GetLocationNumbers(portalName);
		int num = locationNumbers[0];
		int num2 = locationNumbers[1];
		if (0 > num || _locations.Length <= num || 0 > num2 || _locations.Length <= num2)
		{
			return null;
		}
		ExploreMapLocation exploreMapLocation = _locations[num];
		ExploreMapLocation loc = _locations[num2];
		return Singleton<FieldMapTable>.I.GetPortalListByMapID((uint)exploreMapLocation.mapId).Find((FieldMapTable.PortalTableData o) => (o.dstMapID == loc.mapId) ? true : false);
	}

	public uint GetPortalID(string portalName)
	{
		int[] locationNumbers = GetLocationNumbers(portalName);
		int num = locationNumbers[0];
		int num2 = locationNumbers[1];
		if (0 > num || _locations.Length <= num || 0 > num2 || _locations.Length <= num2)
		{
			return 0u;
		}
		ExploreMapLocation exploreMapLocation = _locations[num];
		ExploreMapLocation loc = _locations[num2];
		return Singleton<FieldMapTable>.I.GetPortalListByMapID((uint)exploreMapLocation.mapId).Find((FieldMapTable.PortalTableData o) => (o.dstMapID == loc.mapId) ? true : false)?.portalID ?? 0;
	}

	public static int[] GetLocationNumbers(string portalName)
	{
		string[] array = portalName.Replace("Portal", "").Split('_');
		return new int[2]
		{
			int.Parse(array[0]),
			int.Parse(array[1])
		};
	}

	public static int[] GetPortalIDsFromMapIDs(int[] mapIDs)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return null;
		}
		if (mapIDs[0] == 0 || mapIDs[1] == 0)
		{
			return null;
		}
		uint entranceMapID = (uint)mapIDs[0];
		uint exitMapID = (uint)mapIDs[1];
		uint entrancePortalID = 0u;
		uint exitPortalID = 0u;
		List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID(entranceMapID);
		portalListByMapID.ForEach(delegate(FieldMapTable.PortalTableData o)
		{
			if (exitMapID == o.dstMapID)
			{
				entrancePortalID = o.portalID;
			}
		});
		portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID(exitMapID);
		if (portalListByMapID == null)
		{
			return new int[0];
		}
		portalListByMapID.ForEach(delegate(FieldMapTable.PortalTableData o)
		{
			if (entranceMapID == o.dstMapID)
			{
				exitPortalID = o.portalID;
			}
		});
		return new int[2]
		{
			(int)entrancePortalID,
			(int)exitPortalID
		};
	}

	public void SetDirectionSonar(GameObject sonar)
	{
		directionSonar = sonar;
	}

	public float GetMapScale()
	{
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return 1f;
		}
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
		{
			return _landscapeScale;
		}
		return _portraitScale;
	}

	public float GetSonarOffset()
	{
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return 1f;
		}
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
		{
			return _landscaleSonarOffset;
		}
		return _portraitSonarOffset;
	}

	public Vector2 GetSonarScale()
	{
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return Vector2.one;
		}
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
		{
			return _landscaleSonarScale;
		}
		return _portraitSonarScale;
	}

	public float GetSonarFov()
	{
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return _portraitSonarFov;
		}
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
		{
			return _landscapeSonarFov;
		}
		return _portraitSonarFov;
	}
}
