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

	public FieldGimmickObject()
		: this()
	{
	}

	public static IFieldGimmickObject Create<T>(FieldMapTable.FieldGimmickPointTableData pointData, int layer, Transform parent) where T : MonoBehaviour
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
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

	public virtual bool IsSearchableNearest()
	{
		return true;
	}

	protected virtual void Awake()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Utility.SetLayerWithChildren(this.get_transform(), 19);
		SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
		val.set_center(new Vector3(0f, 0f, 0f));
		val.set_radius(1.5f);
		val.set_isTrigger(true);
	}
}
