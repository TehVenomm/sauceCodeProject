using UnityEngine;

public class FieldGimmickObject : IFieldGimmickObject
{
	protected int m_id;

	protected Transform m_transform;

	protected FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE m_gimmickType;

	protected Transform modelTrans;

	public FieldMapTable.FieldGimmickPointTableData m_pointData
	{
		get;
		protected set;
	}

	public FieldGimmickObject()
		: this()
	{
	}

	public static IFieldGimmickObject Create<T>(FieldMapTable.FieldGimmickPointTableData pointData, int layer, Transform parent) where T : MonoBehaviour
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (pointData == null)
		{
			return null;
		}
		Transform val = Utility.CreateGameObject("GimmickObject", parent, layer);
		val.set_position(new Vector3(pointData.pointX, 0f, pointData.pointZ));
		val.set_rotation(Quaternion.AngleAxis(pointData.pointDir, Vector3.get_up()));
		IFieldGimmickObject fieldGimmickObject = val.get_gameObject().AddComponent<T>() as IFieldGimmickObject;
		fieldGimmickObject.SetTransform(val);
		string objectName = fieldGimmickObject.GetObjectName();
		if (!string.IsNullOrEmpty(objectName))
		{
			val.set_name(objectName);
		}
		return fieldGimmickObject;
	}

	public static void CacheResources(LoadingQueue loadQueue, FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE type, string[] effectNameList, int[] seIdList, int modelIndex)
	{
		uint key;
		string text;
		if (type == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD)
		{
			key = FieldGimmickCannonField.ConvertModelIndexToKey(modelIndex);
			text = FieldGimmickCannonField.ConvertModelIndexToName(modelIndex);
		}
		else
		{
			key = (uint)type;
			text = ResourceName.GetFieldGimmickModel(type);
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(key) == null)
		{
			if (!string.IsNullOrEmpty(text))
			{
				MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Add(key, loadQueue.Load(RESOURCE_CATEGORY.STAGE_GIMMICK, text, false));
			}
			foreach (string name in effectNameList)
			{
				loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
			}
			foreach (int se_id in seIdList)
			{
				loadQueue.CacheSE(se_id, null);
			}
		}
	}

	public virtual void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		m_pointData = pointData;
		m_id = (int)m_pointData.pointID;
		m_gimmickType = m_pointData.gimmickType;
		CreateModel();
	}

	protected virtual void CreateModel()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get((uint)m_gimmickType);
			if (loadObject != null)
			{
				modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, m_transform, -1);
			}
		}
	}

	public virtual int GetId()
	{
		return m_id;
	}

	public virtual Transform GetTransform()
	{
		return m_transform;
	}

	public virtual void RequestDestroy()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Object.Destroy(this.get_gameObject());
	}

	public virtual void OnNotify(object value)
	{
	}

	public FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE GetGimmickType()
	{
		return m_gimmickType;
	}

	public virtual string GetObjectName()
	{
		return string.Empty;
	}

	public virtual void SetTransform(Transform trans)
	{
		m_transform = trans;
	}

	public virtual float GetTargetRadius()
	{
		return 2f;
	}

	public virtual float GetTargetSqrRadius()
	{
		return 4f;
	}

	public virtual void UpdateTargetMarker(bool isNear)
	{
	}

	protected virtual void Awake()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Utility.SetLayerWithChildren(this.get_transform(), 19);
		SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
		val.set_center(new Vector3(0f, 0f, 0f));
		val.set_radius(1.5f);
		val.set_isTrigger(true);
	}
}
