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
		loadedExploreMap_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"), false);
		loadedMarker_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarkerMini", false);
	}

	public void Initialize()
	{
		Transform transform = ResourceUtility.Realizes(loadedExploreMap_.loadedObject, base._transform, -1);
		transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		mapRoot_ = transform.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		spotsActive_ = new Transform[locations.Length];
		spotsInactive_ = new Transform[locations.Length];
		spotsSonar_ = new Transform[locations.Length];
		for (int i = 0; i < locations.Length; i++)
		{
			spotsActive_[i] = locations[i].transform.FindChild("ExploreSpotActiveMini");
			spotsInactive_[i] = locations[i].transform.FindChild("ExploreSpotInactiveMini");
			spotsSonar_[i] = locations[i].transform.FindChild("ExploreSpotSonarMini");
		}
		for (int j = 0; j < 4; j++)
		{
			playerMarkers_[j] = ResourceUtility.Realizes(loadedMarker_.loadedObject, base._transform, -1);
			ExplorePlayerMarkerMini component = playerMarkers_[j].GetComponent<ExplorePlayerMarkerMini>();
			component.SetIndex(j);
			playerMarkers_[j].gameObject.SetActive(false);
		}
		selfMarker_ = playerMarkers_[0];
		initialized_ = true;
	}

	private void LateUpdate()
	{
		if (initialized_)
		{
			frame_--;
			if (0 > frame_)
			{
				UpdateMarkers();
				frame_ = 30;
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
				if (!((Object)self == (Object)null))
				{
					Vector3 eulerAngles = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.eulerAngles;
					float y = eulerAngles.y;
					Vector3 localEulerAngles = self._transform.localEulerAngles;
					float z = y - localEulerAngles.y;
					selfMarker_.localEulerAngles = new Vector3(0f, 0f, z);
				}
			}
		}
	}

	private void UpdateMarkers()
	{
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int i = 0; i < spotsActive_.Length; i++)
		{
			spotsActive_[i].gameObject.SetActive(true);
			spotsInactive_[i].gameObject.SetActive(false);
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[i].mapId);
			if (fieldGimmickPointListByMapID != null && (Object)spotsSonar_[i] != (Object)null)
			{
				for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
				{
					if (fieldGimmickPointListByMapID[j].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
					{
						spotsSonar_[i].gameObject.SetActive(true);
						spotsActive_[i].gameObject.SetActive(false);
						spotsInactive_[i].gameObject.SetActive(false);
					}
					else
					{
						spotsSonar_[i].gameObject.SetActive(false);
					}
				}
			}
		}
		mapRoot_.UpdatePortals(true);
		for (int k = 0; k < playerMarkers_.Length; k++)
		{
			playerMarkers_[k].gameObject.SetActive(false);
		}
		mapRoot_.SetMarkers(playerMarkers_, true);
	}
}
