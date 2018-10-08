using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExploreMapRoot
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
	private Color unusedColor;

	[SerializeField]
	private Color passedColor;

	[SerializeField]
	private Color warpColor;

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

	public ExploreMapRoot()
		: this()
	{
	}//IL_0037: Unknown result type (might be due to invalid IL or missing references)
	//IL_003c: Unknown result type (might be due to invalid IL or missing references)
	//IL_004c: Unknown result type (might be due to invalid IL or missing references)
	//IL_0051: Unknown result type (might be due to invalid IL or missing references)
	//IL_0081: Unknown result type (might be due to invalid IL or missing references)
	//IL_0086: Unknown result type (might be due to invalid IL or missing references)


	public ExploreMapLocation FindLocation(int id)
	{
		return Array.Find(locations, (ExploreMapLocation l) => l.mapId == id);
	}

	public Transform FindPortalNode(int mapId0, int mapId1)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		ExploreMapLocation exploreMapLocation = FindLocation(mapId0);
		ExploreMapLocation exploreMapLocation2 = FindLocation(mapId1);
		if (null == exploreMapLocation || null == exploreMapLocation2)
		{
			return null;
		}
		int locationIndex = GetLocationIndex(exploreMapLocation.get_name());
		int locationIndex2 = GetLocationIndex(exploreMapLocation2.get_name());
		int num = Mathf.Min(locationIndex, locationIndex2);
		int num2 = Mathf.Max(locationIndex, locationIndex2);
		string str = "Portal" + num.ToString() + "_" + num2.ToString();
		return this.get_transform().FindChild("Road/" + str);
	}

	public Transform FindNode(int mapId, out Vector3 offset, out bool isBattle)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		offset._002Ector(0f, 0f, 0f);
		isBattle = false;
		ExploreMapLocation exploreMapLocation = FindLocation(mapId);
		if (null != exploreMapLocation)
		{
			return exploreMapLocation.get_transform();
		}
		if (MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId() == mapId)
		{
			isBattle = true;
			exploreMapLocation = FindLocation(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId());
			if (exploreMapLocation != null)
			{
				return exploreMapLocation.get_transform();
			}
		}
		return null;
	}

	public void UpdatePortals(bool isMiniMap)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < portals.Length; i++)
		{
			Transform val = portals[i];
			string name = val.get_name();
			FieldMapTable.PortalTableData portalData = GetPortalData(name);
			if (portalData != null)
			{
				val.get_gameObject().SetActive(true);
				UITexture[] componentsInChildren = val.GetComponentsInChildren<UITexture>();
				if (componentsInChildren == null || componentsInChildren.Length == 0)
				{
					val.get_gameObject().SetActive(false);
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
					val.get_gameObject().SetActive(isMiniMap);
				}
			}
		}
	}

	public void SetMarkers(Transform[] markers, bool isMiniMap)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		int[] exploreDisplayIndices = MonoBehaviourSingleton<QuestManager>.I.GetExploreDisplayIndices();
		for (int i = 0; i < markers.Length; i++)
		{
			int num = exploreDisplayIndices[i];
			int exploreMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreMapId(i);
			if (0 <= exploreMapId)
			{
				Transform val = markers[num];
				Vector3 offset;
				bool isBattle;
				Transform val2 = FindNode(exploreMapId, out offset, out isBattle);
				if (null != val2)
				{
					val.get_gameObject().SetActive(true);
					Utility.Attach(val2, val.get_transform());
					if (isMiniMap)
					{
						val.GetComponent<ExplorePlayerMarkerMini>().SetIndex(num);
					}
					else
					{
						val.GetComponent<ExplorePlayerMarker>().SetIndex(num);
					}
					val.set_localPosition(val.get_localPosition() + offset);
					showBattleMarker |= isBattle;
				}
			}
		}
	}

	public Vector3 GetPositionOnMap(int mapId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (mapId < 0)
		{
			return Vector3.get_zero();
		}
		Vector3 offset;
		bool isBattle;
		Transform val = FindNode(mapId, out offset, out isBattle);
		if (val == null)
		{
			return Vector3.get_zero();
		}
		return val.get_localPosition();
	}

	private static int GetLocationIndex(string name)
	{
		string s = name.Replace("Location", string.Empty);
		return int.Parse(s);
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
		List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint)exploreMapLocation.mapId, false);
		return portalListByMapID.Find(delegate(FieldMapTable.PortalTableData o)
		{
			if (o.dstMapID == loc.mapId)
			{
				return true;
			}
			return false;
		});
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
		List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint)exploreMapLocation.mapId, false);
		return portalListByMapID.Find(delegate(FieldMapTable.PortalTableData o)
		{
			if (o.dstMapID == loc.mapId)
			{
				return true;
			}
			return false;
		})?.portalID ?? 0;
	}

	public static int[] GetLocationNumbers(string portalName)
	{
		string[] array = portalName.Replace("Portal", string.Empty).Split('_');
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
		List<FieldMapTable.PortalTableData> portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID(entranceMapID, false);
		portalListByMapID.ForEach(delegate(FieldMapTable.PortalTableData o)
		{
			if (exitMapID == o.dstMapID)
			{
				entrancePortalID = o.portalID;
			}
		});
		portalListByMapID = Singleton<FieldMapTable>.I.GetPortalListByMapID(exitMapID, false);
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
		return (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? _landscapeScale : _portraitScale;
	}

	public float GetSonarOffset()
	{
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return 1f;
		}
		return (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? _landscaleSonarOffset : _portraitSonarOffset;
	}

	public Vector2 GetSonarScale()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return Vector2.get_one();
		}
		return (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? _landscaleSonarScale : _portraitSonarScale;
	}

	public float GetSonarFov()
	{
		if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			return _portraitSonarFov;
		}
		return (!MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait) ? _landscapeSonarFov : _portraitSonarFov;
	}
}
