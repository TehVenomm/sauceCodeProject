using System.Collections.Generic;
using UnityEngine;

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
		base.gameObject.SetActive(MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExplore() && !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap());
	}

	public void Preload(LoadingQueue loadQueue)
	{
		uint regionId = MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId;
		loadedExploreMap_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"));
		loadedMarker_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarkerMini");
	}

	public void Initialize()
	{
		Transform transform = ResourceUtility.Realizes(loadedExploreMap_.loadedObject, base._transform);
		transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		mapRoot_ = transform.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		spotsActive_ = new Transform[locations.Length];
		spotsInactive_ = new Transform[locations.Length];
		spotsSonar_ = new Transform[locations.Length];
		for (int i = 0; i < locations.Length; i++)
		{
			spotsActive_[i] = locations[i].transform.Find("ExploreSpotActiveMini");
			spotsInactive_[i] = locations[i].transform.Find("ExploreSpotInactiveMini");
			spotsSonar_[i] = locations[i].transform.Find("ExploreSpotSonarMini");
		}
		for (int j = 0; j < 4; j++)
		{
			playerMarkers_[j] = ResourceUtility.Realizes(loadedMarker_.loadedObject, base._transform);
			playerMarkers_[j].GetComponent<ExplorePlayerMarkerMini>().SetIndex(j);
			playerMarkers_[j].gameObject.SetActive(value: false);
		}
		selfMarker_ = playerMarkers_[0];
		initialized_ = true;
	}

	private void LateUpdate()
	{
		if (!initialized_)
		{
			return;
		}
		frame_--;
		if (0 > frame_)
		{
			UpdateMarkers();
			frame_ = 30;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!(self == null))
			{
				float z = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles.y - self._transform.localEulerAngles.y;
				selfMarker_.localEulerAngles = new Vector3(0f, 0f, z);
			}
		}
	}

	private void UpdateMarkers()
	{
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int i = 0; i < spotsActive_.Length; i++)
		{
			spotsActive_[i].gameObject.SetActive(value: true);
			spotsInactive_[i].gameObject.SetActive(value: false);
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[i].mapId);
			if (fieldGimmickPointListByMapID == null || !(spotsSonar_[i] != null))
			{
				continue;
			}
			for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
			{
				if (fieldGimmickPointListByMapID[j].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
				{
					spotsSonar_[i].gameObject.SetActive(value: true);
					spotsActive_[i].gameObject.SetActive(value: false);
					spotsInactive_[i].gameObject.SetActive(value: false);
				}
				else
				{
					spotsSonar_[i].gameObject.SetActive(value: false);
				}
			}
		}
		mapRoot_.UpdatePortals(isMiniMap: true);
		for (int k = 0; k < playerMarkers_.Length; k++)
		{
			playerMarkers_[k].gameObject.SetActive(value: false);
		}
		mapRoot_.SetMarkers(playerMarkers_, isMiniMap: true);
	}
}
