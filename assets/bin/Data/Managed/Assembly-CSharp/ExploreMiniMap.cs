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

	private Transform[] playerMarkers_ = (Transform[])new Transform[4];

	private Transform selfMarker_;

	private int frame_;

	private bool initialized_;

	protected override void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().SetActive(MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExplore() && !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap());
	}

	public void Preload(LoadingQueue loadQueue)
	{
		uint regionId = MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId;
		loadedExploreMap_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"), false);
		loadedMarker_ = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarkerMini", false);
	}

	public void Initialize()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(loadedExploreMap_.loadedObject, base._transform, -1);
		val.set_localScale(new Vector3(0.3f, 0.3f, 1f));
		mapRoot_ = val.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		spotsActive_ = (Transform[])new Transform[locations.Length];
		spotsInactive_ = (Transform[])new Transform[locations.Length];
		spotsSonar_ = (Transform[])new Transform[locations.Length];
		for (int i = 0; i < locations.Length; i++)
		{
			spotsActive_[i] = locations[i].get_transform().FindChild("ExploreSpotActiveMini");
			spotsInactive_[i] = locations[i].get_transform().FindChild("ExploreSpotInactiveMini");
			spotsSonar_[i] = locations[i].get_transform().FindChild("ExploreSpotSonarMini");
		}
		for (int j = 0; j < 4; j++)
		{
			playerMarkers_[j] = ResourceUtility.Realizes(loadedMarker_.loadedObject, base._transform, -1);
			ExplorePlayerMarkerMini component = playerMarkers_[j].GetComponent<ExplorePlayerMarkerMini>();
			component.SetIndex(j);
			playerMarkers_[j].get_gameObject().SetActive(false);
		}
		selfMarker_ = playerMarkers_[0];
		initialized_ = true;
	}

	private void LateUpdate()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
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
				if (!(self == null))
				{
					Vector3 eulerAngles = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_eulerAngles();
					float y = eulerAngles.y;
					Vector3 localEulerAngles = self._transform.get_localEulerAngles();
					float num = y - localEulerAngles.y;
					selfMarker_.set_localEulerAngles(new Vector3(0f, 0f, num));
				}
			}
		}
	}

	private void UpdateMarkers()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int i = 0; i < spotsActive_.Length; i++)
		{
			spotsActive_[i].get_gameObject().SetActive(true);
			spotsInactive_[i].get_gameObject().SetActive(false);
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[i].mapId);
			if (fieldGimmickPointListByMapID != null && spotsSonar_[i] != null)
			{
				for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
				{
					if (fieldGimmickPointListByMapID[j].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
					{
						spotsSonar_[i].get_gameObject().SetActive(true);
						spotsActive_[i].get_gameObject().SetActive(false);
						spotsInactive_[i].get_gameObject().SetActive(false);
					}
					else
					{
						spotsSonar_[i].get_gameObject().SetActive(false);
					}
				}
			}
		}
		mapRoot_.UpdatePortals(true);
		for (int k = 0; k < playerMarkers_.Length; k++)
		{
			playerMarkers_[k].get_gameObject().SetActive(false);
		}
		mapRoot_.SetMarkers(playerMarkers_, true);
	}
}
