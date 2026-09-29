// Decompiled with JetBrains decompiler
// Type: ExploreMapRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
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
  private Color _sonarBackGroundColor = new Color(0.63f, 0.63f, 0.63f, 0.0f);
  private bool isSetup;

  public ExploreMapLocation[] locations => this._locations;

  public Transform[] portals => this._portals;

  public UITexture mapTexture => this.map;

  public float landscapeSonarFov => this._landscapeSonarFov;

  public Color sonarBackGroundColor => this._sonarBackGroundColor;

  public bool showBattleMarker { get; private set; }

  public GameObject directionSonar { get; private set; }

  public bool IsSetup
  {
    get => this.isSetup;
    set => this.isSetup = value;
  }

  public ExploreMapLocation FindLocation(int id)
  {
    return Array.Find<ExploreMapLocation>(this.locations, (Predicate<ExploreMapLocation>) (l => l.mapId == id));
  }

  public Transform FindPortalNode(int mapId0, int mapId1)
  {
    ExploreMapLocation location1 = this.FindLocation(mapId0);
    ExploreMapLocation location2 = this.FindLocation(mapId1);
    if (Object.op_Equality((Object) null, (Object) location1) || Object.op_Equality((Object) null, (Object) location2))
      return (Transform) null;
    int locationIndex1 = ExploreMapRoot.GetLocationIndex(((Object) location1).name);
    int locationIndex2 = ExploreMapRoot.GetLocationIndex(((Object) location2).name);
    int num1 = Mathf.Min(locationIndex1, locationIndex2);
    int num2 = Mathf.Max(locationIndex1, locationIndex2);
    return ((Component) this).transform.Find("Road/" + $"Portal{num1.ToString()}_{num2.ToString()}");
  }

  public Transform FindNode(int mapId, out Vector3 offset, out bool isBattle)
  {
    offset = new Vector3(0.0f, 0.0f, 0.0f);
    isBattle = false;
    ExploreMapLocation location1 = this.FindLocation(mapId);
    if (Object.op_Inequality((Object) null, (Object) location1))
      return ((Component) location1).transform;
    if (MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId() == mapId)
    {
      isBattle = true;
      ExploreMapLocation location2 = this.FindLocation(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId());
      if (Object.op_Inequality((Object) location2, (Object) null))
        return ((Component) location2).transform;
    }
    return (Transform) null;
  }

  public void UpdatePortals(bool isMiniMap)
  {
    for (int index = 0; index < this.portals.Length; ++index)
    {
      Transform portal = this.portals[index];
      FieldMapTable.PortalTableData portalData = this.GetPortalData(((Object) portal).name);
      if (portalData != null)
      {
        ((Component) portal).gameObject.SetActive(true);
        UITexture[] componentsInChildren = ((Component) portal).GetComponentsInChildren<UITexture>();
        if (componentsInChildren == null || componentsInChildren.Length == 0)
          ((Component) portal).gameObject.SetActive(false);
        else if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledPortal(portalData.portalID))
        {
          componentsInChildren[0].color = this.passedColor;
          if (portalData.IsWarpPortal())
            componentsInChildren[0].color = this.warpColor;
        }
        else
        {
          componentsInChildren[0].color = this.unusedColor;
          ((Component) portal).gameObject.SetActive(isMiniMap);
        }
      }
    }
  }

  public void SetMarkers(Transform[] markers, bool isMiniMap)
  {
    int[] exploreDisplayIndices = MonoBehaviourSingleton<QuestManager>.I.GetExploreDisplayIndices();
    for (int statusIndex = 0; statusIndex < markers.Length; ++statusIndex)
    {
      int idx = exploreDisplayIndices[statusIndex];
      int exploreMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreMapId(statusIndex);
      if (0 <= exploreMapId)
      {
        Transform marker = markers[idx];
        Vector3 offset;
        bool isBattle;
        Transform node = this.FindNode(exploreMapId, out offset, out isBattle);
        if (Object.op_Inequality((Object) null, (Object) node))
        {
          ((Component) marker).gameObject.SetActive(true);
          Utility.Attach(node, ((Component) marker).transform);
          if (isMiniMap)
            ((Component) marker).GetComponent<ExplorePlayerMarkerMini>().SetIndex(idx);
          else
            ((Component) marker).GetComponent<ExplorePlayerMarker>().SetIndex(idx);
          marker.localPosition = Vector3.op_Addition(marker.localPosition, offset);
          this.showBattleMarker |= isBattle;
        }
      }
    }
  }

  public Vector3 GetPositionOnMap(int mapId)
  {
    if (mapId < 0)
      return Vector3.zero;
    Transform node = this.FindNode(mapId, out Vector3 _, out bool _);
    return Object.op_Equality((Object) node, (Object) null) ? Vector3.zero : node.localPosition;
  }

  private static int GetLocationIndex(string name) => int.Parse(name.Replace("Location", ""));

  public int[] GetMapIDsFromLocationNumbers(int[] numbers)
  {
    return new int[2]
    {
      this.locations[numbers[0]].mapId,
      this.locations[numbers[1]].mapId
    };
  }

  public FieldMapTable.PortalTableData GetPortalData(string portalName)
  {
    int[] locationNumbers = ExploreMapRoot.GetLocationNumbers(portalName);
    int index1 = locationNumbers[0];
    int index2 = locationNumbers[1];
    if (0 > index1 || this._locations.Length <= index1 || 0 > index2 || this._locations.Length <= index2)
      return (FieldMapTable.PortalTableData) null;
    ExploreMapLocation location = this._locations[index1];
    ExploreMapLocation loc1 = this._locations[index2];
    return Singleton<FieldMapTable>.I.GetPortalListByMapID((uint) location.mapId).Find((Predicate<FieldMapTable.PortalTableData>) (o => (long) o.dstMapID == (long) loc1.mapId));
  }

  public uint GetPortalID(string portalName)
  {
    int[] locationNumbers = ExploreMapRoot.GetLocationNumbers(portalName);
    int index1 = locationNumbers[0];
    int index2 = locationNumbers[1];
    if (0 > index1 || this._locations.Length <= index1 || 0 > index2 || this._locations.Length <= index2)
      return 0;
    ExploreMapLocation location = this._locations[index1];
    ExploreMapLocation loc1 = this._locations[index2];
    FieldMapTable.PortalTableData portalTableData = Singleton<FieldMapTable>.I.GetPortalListByMapID((uint) location.mapId).Find((Predicate<FieldMapTable.PortalTableData>) (o => (long) o.dstMapID == (long) loc1.mapId));
    return portalTableData == null ? 0U : portalTableData.portalID;
  }

  public static int[] GetLocationNumbers(string portalName)
  {
    string[] strArray = portalName.Replace("Portal", "").Split('_');
    return new int[2]
    {
      int.Parse(strArray[0]),
      int.Parse(strArray[1])
    };
  }

  public static int[] GetPortalIDsFromMapIDs(int[] mapIDs)
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return (int[]) null;
    if (mapIDs[0] == 0 || mapIDs[1] == 0)
      return (int[]) null;
    uint entranceMapID = (uint) mapIDs[0];
    uint exitMapID = (uint) mapIDs[1];
    uint entrancePortalID = 0;
    uint exitPortalID = 0;
    Singleton<FieldMapTable>.I.GetPortalListByMapID(entranceMapID).ForEach((Action<FieldMapTable.PortalTableData>) (o =>
    {
      if ((int) exitMapID != (int) o.dstMapID)
        return;
      entrancePortalID = o.portalID;
    }));
    List<FieldMapTable.PortalTableData> portalListByMapId = Singleton<FieldMapTable>.I.GetPortalListByMapID(exitMapID);
    if (portalListByMapId == null)
      return new int[0];
    portalListByMapId.ForEach((Action<FieldMapTable.PortalTableData>) (o =>
    {
      if ((int) entranceMapID != (int) o.dstMapID)
        return;
      exitPortalID = o.portalID;
    }));
    return new int[2]
    {
      (int) entrancePortalID,
      (int) exitPortalID
    };
  }

  public void SetDirectionSonar(GameObject sonar) => this.directionSonar = sonar;

  public float GetMapScale()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return 1f;
    return !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this._landscapeScale : this._portraitScale;
  }

  public float GetSonarOffset()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return 1f;
    return !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this._landscaleSonarOffset : this._portraitSonarOffset;
  }

  public Vector2 GetSonarScale()
  {
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      return Vector2.one;
    return !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this._landscaleSonarScale : this._portraitSonarScale;
  }

  public float GetSonarFov()
  {
    return !MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() || MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait ? this._portraitSonarFov : this._landscapeSonarFov;
  }
}
