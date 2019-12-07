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
		IFieldGimmickObject obj = transform.gameObject.AddComponent<T>() as IFieldGimmickObject;
		obj.SetTransform(transform);
		string objectName = obj.GetObjectName();
		if (!string.IsNullOrEmpty(objectName))
		{
			transform.name = objectName;
		}
		return obj;
	}

	public static uint ConvertModelIndexToKey(FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE type, int index)
	{
		return (uint)(index * GameDefine.kShiftIndex + type);
	}

	public static void CacheResources(LoadingQueue loadQueue, FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE type, string[] effectNameList, int[] seIdList, int[] modelIndexes)
	{
		foreach (int num in modelIndexes)
		{
			uint key;
			string text;
			switch (type)
			{
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
				key = FieldGimmickCannonField.ConvertModelIndexToKey(num);
				text = FieldGimmickCannonField.ConvertModelIndexToName(num);
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
				key = FieldWaveTargetObject.ConvertModelIndexToKey(num);
				text = FieldWaveTargetObject.ConvertModelIndexToName(num);
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
				key = (uint)type;
				text = FieldFishingGimmickObject.ConvertModelIndexToName(num);
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SUPPLY:
				key = FieldSupplyGimmickObject.ConvertModelIndexToKey(num);
				text = FieldSupplyGimmickObject.ConvertModelIndexToName(num);
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_EVOLVE_ITEM:
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_DECOY:
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BUFF_POINT:
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
				key = ConvertModelIndexToKey(type, num);
				text = ResourceName.GetFieldGimmickModel(type, num);
				break;
			case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST:
				key = ConvertModelIndexToKey(type, num);
				text = ResourceName.GetFieldGimmickModel(type, num);
				break;
			default:
				key = (uint)type;
				text = ResourceName.GetFieldGimmickModel(type, num);
				break;
			}
			if (MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(key) == null && !string.IsNullOrEmpty(text))
			{
				RESOURCE_CATEGORY category = RESOURCE_CATEGORY.STAGE_GIMMICK;
				if (type == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST)
				{
					category = RESOURCE_CATEGORY.INGAME_GATHER_POINT;
				}
				MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Add(key, loadQueue.Load(category, text));
			}
		}
		foreach (string name in effectNameList)
		{
			loadQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name);
		}
		foreach (int se_id in seIdList)
		{
			loadQueue.CacheSE(se_id);
		}
	}

	public virtual void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		m_pointData = pointData;
		m_id = (int)m_pointData.pointID;
		m_gimmickType = m_pointData.gimmickType;
		ParseParam(pointData.value2);
		CreateModel();
	}

	protected virtual void ParseParam(string value2)
	{
	}

	protected virtual void CreateModel()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get((uint)m_gimmickType);
			if (loadObject != null)
			{
				modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, m_transform);
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

	public virtual bool IsSearchableNearest()
	{
		return true;
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
