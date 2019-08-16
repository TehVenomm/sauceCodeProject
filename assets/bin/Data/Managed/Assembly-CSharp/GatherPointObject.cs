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

	protected GatherPointObject()
		: this()
	{
	}

	public static T Create<T>(FieldMapTable.GatherPointTableData point_data, Transform parent) where T : GatherPointObject
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Transform val = Utility.CreateGameObject("GatherPoint", parent, 9);
		val.set_position(new Vector3(point_data.pointX, 0f, point_data.pointZ));
		val.set_rotation(Quaternion.AngleAxis(point_data.pointDir, Vector3.get_up()));
		T val2 = val.get_gameObject().AddComponent<T>();
		if (val2 == null)
		{
			return (T)null;
		}
		val2.Initialize(point_data);
		return val2;
	}

	private void Awake()
	{
		_transform = this.get_transform();
	}

	public virtual void Initialize(FieldMapTable.GatherPointTableData point_data)
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		pointData = point_data;
		viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(pointData.viewID);
		if (viewData == null)
		{
			Log.Error(LOG.INGAME, "GatherPointObject::Initialize() viewData is null. pointID = {0}, viewID = {1}", pointData.pointID, pointData.viewID);
			return;
		}
		if (viewData.viewID != 0)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointModelTable.Get(viewData.viewID);
			modelView = ResourceUtility.Realizes(loadObject.loadedObject, _transform);
		}
		if (!string.IsNullOrEmpty(viewData.gatherEffectName))
		{
			gatherEffect = EffectManager.GetEffect(viewData.gatherEffectName, _transform);
		}
		if (viewData.colRadius > 0f)
		{
			SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
			val.set_center(new Vector3(0f, 0f, 0f));
			val.set_radius(viewData.colRadius);
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		}
		CheckGather();
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
		if (gatherEffect != null)
		{
			gatherEffect.get_gameObject().SetActive(!isGathered);
		}
		if (modelView != null && !string.IsNullOrEmpty(viewData.modelHideNodeName))
		{
			Transform val = Utility.Find(modelView, viewData.modelHideNodeName);
			if (val != null)
			{
				val.get_gameObject().SetActive(!isGathered);
			}
		}
	}

	public virtual void UpdateTargetMarker(bool is_near)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		if (is_near && self != null && self.IsChangeableAction((Character.ACTION_ID)28))
		{
			if (targetEffect == null && !string.IsNullOrEmpty(viewData.targetEffectName))
			{
				targetEffect = EffectManager.GetEffect(viewData.targetEffectName, _transform);
			}
			if (targetEffect != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - _transform.get_position();
				Vector3 pos = val.get_normalized() * viewData.targetEffectShift + Vector3.get_up() * viewData.targetEffectHeight + _transform.get_position();
				targetEffect.Set(pos, rotation);
			}
		}
		else if (targetEffect != null)
		{
			EffectManager.ReleaseEffect(targetEffect.get_gameObject());
		}
	}
}
