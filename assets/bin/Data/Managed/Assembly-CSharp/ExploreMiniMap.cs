// Decompiled with JetBrains decompiler
// Type: ExploreMiniMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ExploreMiniMap : MonoBehaviourSingleton<ExploreMiniMap>
{
  private LoadObject loadedExploreMap_;
  private LoadObject loadedMarker_;
  private ExploreMapRoot mapRoot_;
  private Transform[] spotsActive_;
  private Transform[] spotsInactive_;
  private Transform[] spotsSonar_;
  private Transform[] playerMarkers_ = new Transform[4];
  private Transform selfMarker_;
  private int frame_;
  private bool initialized_;

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.SetActive(MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExplore() && !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap());
  }

  public void Preload(LoadingQueue loadQueue)
  {
    uint regionId = MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId;
    this.loadedExploreMap_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"));
    this.loadedMarker_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarkerMini");
  }

  public void Initialize()
  {
    Transform transform = ResourceUtility.Realizes(this.loadedExploreMap_.loadedObject, this._transform);
    transform.localScale = new Vector3(0.3f, 0.3f, 1f);
    this.mapRoot_ = ((Component) transform).GetComponent<ExploreMapRoot>();
    ExploreMapLocation[] locations = this.mapRoot_.locations;
    this.spotsActive_ = new Transform[locations.Length];
    this.spotsInactive_ = new Transform[locations.Length];
    this.spotsSonar_ = new Transform[locations.Length];
    for (int index = 0; index < locations.Length; ++index)
    {
      this.spotsActive_[index] = ((Component) locations[index]).transform.Find("ExploreSpotActiveMini");
      this.spotsInactive_[index] = ((Component) locations[index]).transform.Find("ExploreSpotInactiveMini");
      this.spotsSonar_[index] = ((Component) locations[index]).transform.Find("ExploreSpotSonarMini");
    }
    for (int idx = 0; idx < 4; ++idx)
    {
      this.playerMarkers_[idx] = ResourceUtility.Realizes(this.loadedMarker_.loadedObject, this._transform);
      ((Component) this.playerMarkers_[idx]).GetComponent<ExplorePlayerMarkerMini>().SetIndex(idx);
      ((Component) this.playerMarkers_[idx]).gameObject.SetActive(false);
    }
    this.selfMarker_ = this.playerMarkers_[0];
    this.initialized_ = true;
  }

  private void LateUpdate()
  {
    if (!this.initialized_)
      return;
    --this.frame_;
    if (0 > this.frame_)
    {
      this.UpdateMarkers();
      this.frame_ = 30;
    }
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null))
      return;
    this.selfMarker_.localEulerAngles = new Vector3(0.0f, 0.0f, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles.y - self._transform.localEulerAngles.y);
  }

  private void UpdateMarkers()
  {
    ExploreMapLocation[] locations = this.mapRoot_.locations;
    for (int index1 = 0; index1 < this.spotsActive_.Length; ++index1)
    {
      ((Component) this.spotsActive_[index1]).gameObject.SetActive(true);
      ((Component) this.spotsInactive_[index1]).gameObject.SetActive(false);
      List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint) locations[index1].mapId);
      if (pointListByMapId != null && Object.op_Inequality((Object) this.spotsSonar_[index1], (Object) null))
      {
        for (int index2 = 0; index2 < pointListByMapId.Count; ++index2)
        {
          if (pointListByMapId[index2].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
          {
            ((Component) this.spotsSonar_[index1]).gameObject.SetActive(true);
            ((Component) this.spotsActive_[index1]).gameObject.SetActive(false);
            ((Component) this.spotsInactive_[index1]).gameObject.SetActive(false);
          }
          else
            ((Component) this.spotsSonar_[index1]).gameObject.SetActive(false);
        }
      }
    }
    this.mapRoot_.UpdatePortals(true);
    for (int index = 0; index < this.playerMarkers_.Length; ++index)
      ((Component) this.playerMarkers_[index]).gameObject.SetActive(false);
    this.mapRoot_.SetMarkers(this.playerMarkers_, true);
  }
}
