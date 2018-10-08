using UnityEngine;

public class FieldGimmickObject : MonoBehaviour, IFieldGimmickObject
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

	public static IFieldGimmickObject Create<T>(FieldMapTable.FieldGimmickPointTableData pointData, int layer, Transform parent) where T : MonoBehaviour
	{
		if (pointData == null)
		{
			return null;
		}
		Transform transform = Utility.CreateGameObject("GimmickObject", parent, layer);
		transform.position = new Vector3(pointData.pointX, 0f, pointData.pointZ);
		transform.rotation = Quaternion.AngleAxis(pointData.pointDir, Vector3.up);
		IFieldGimmickObject fieldGimmickObject = transform.gameObject.AddComponent<T>() as IFieldGimmickObject;
		fieldGimmickObject.SetTransform(transform);
		string objectName = fieldGimmickObject.GetObjectName();
		if (!string.IsNullOrEmpty(objectName))
		{
			transform.name = objectName;
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
		Object.Destroy(base.gameObject);
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
		Utility.SetLayerWithChildren(base.transform, 19);
		SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
		sphereCollider.center = new Vector3(0f, 0f, 0f);
		sphereCollider.radius = 1.5f;
		sphereCollider.isTrigger = true;
	}
}
