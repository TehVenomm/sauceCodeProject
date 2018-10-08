using UnityEngine;

public abstract class GatherPointObject : MonoBehaviour
{
	protected Transform modelView;

	protected Transform gatherEffect;

	protected Transform targetEffect;

	protected Self self;

	public FieldGimmickObject gimmick;

	public Transform _transform
	{
		get;
		private set;
	}

	public FieldMapTable.GatherPointTableData pointData
	{
		get;
		protected set;
	}

	public FieldMapTable.GatherPointViewTableData viewData
	{
		get;
		protected set;
	}

	public bool isGathered
	{
		get;
		protected set;
	}

	public static T Create<T>(FieldMapTable.GatherPointTableData point_data, Transform parent) where T : GatherPointObject
	{
		Transform transform = Utility.CreateGameObject("GatherPoint", parent, 9);
		transform.position = new Vector3(point_data.pointX, 0f, point_data.pointZ);
		transform.rotation = Quaternion.AngleAxis(point_data.pointDir, Vector3.up);
		T val = transform.gameObject.AddComponent<T>();
		if ((Object)val == (Object)null)
		{
			return (T)null;
		}
		val.Initialize(point_data);
		return val;
	}

	private void Awake()
	{
		_transform = base.transform;
	}

	public virtual void Initialize(FieldMapTable.GatherPointTableData point_data)
	{
		pointData = point_data;
		viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(pointData.viewID);
		if (viewData == null)
		{
			Log.Error(LOG.INGAME, "GatherPointObject::Initialize() viewData is null. pointID = {0}, viewID = {1}", pointData.pointID, pointData.viewID);
		}
		else
		{
			if (viewData.viewID != 0)
			{
				LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointModelTable.Get(viewData.viewID);
				modelView = ResourceUtility.Realizes(loadObject.loadedObject, _transform, -1);
			}
			if (!string.IsNullOrEmpty(viewData.gatherEffectName))
			{
				gatherEffect = EffectManager.GetEffect(viewData.gatherEffectName, _transform);
			}
			if (viewData.colRadius > 0f)
			{
				SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
				sphereCollider.center = new Vector3(0f, 0f, 0f);
				sphereCollider.radius = viewData.colRadius;
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			}
			CheckGather();
		}
	}

	public virtual void CheckGather()
	{
		UpdateView();
	}

	public virtual void Gather()
	{
	}

	public virtual void UpdateView()
	{
		if ((Object)gatherEffect != (Object)null)
		{
			gatherEffect.gameObject.SetActive(!isGathered);
		}
		if ((Object)modelView != (Object)null && !string.IsNullOrEmpty(viewData.modelHideNodeName))
		{
			Transform transform = Utility.Find(modelView, viewData.modelHideNodeName);
			if ((Object)transform != (Object)null)
			{
				transform.gameObject.SetActive(!isGathered);
			}
		}
	}

	public virtual void UpdateTargetMarker(bool is_near)
	{
		if (is_near && (Object)self != (Object)null && self.IsChangeableAction((Character.ACTION_ID)27))
		{
			if ((Object)targetEffect == (Object)null && !string.IsNullOrEmpty(viewData.targetEffectName))
			{
				targetEffect = EffectManager.GetEffect(viewData.targetEffectName, _transform);
			}
			if ((Object)targetEffect != (Object)null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized * viewData.targetEffectShift + Vector3.up * viewData.targetEffectHeight + _transform.position;
				targetEffect.Set(pos, rotation);
			}
		}
		else if ((Object)targetEffect != (Object)null)
		{
			EffectManager.ReleaseEffect(targetEffect.gameObject, true, false);
		}
	}
}
