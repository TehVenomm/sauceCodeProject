using System.Collections.Generic;
using UnityEngine;

public class FieldCarriableGimmickObject : FieldGimmickObject
{
	public static readonly string kCarryMarkerName = "ef_btl_target_lift_01";

	public static readonly string kEvolveMarkerName = "ef_btl_target_levelup_01";

	public static readonly string kEvolveEffectName = "ef_btl_trap_01_01";

	public static readonly int kEvolveSEId = 10000075;

	public static readonly Vector3 kCarryOffset = new Vector3(0f, 0f, 1.86f);

	public static readonly string kCarryNode = "R_Wep";

	private static readonly string kShadowNode = "shadow01";

	private static readonly float kRadius = 2f;

	private Transform parentTrans;

	private Transform carryMarkerTrans;

	private Transform evolveMarkerTrans;

	private Transform shadowTrans;

	protected int modelIndex;

	protected int[] modelIndexes;

	protected bool hasDeploied;

	protected int currentLv;

	protected int maxLv;

	public bool isCarrying
	{
		get;
		protected set;
	}

	protected override void Awake()
	{
		base.Awake();
		modelIndexes = new int[1];
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		parentTrans = GetTransform().parent;
		isCarrying = false;
		if (modelIndexes.Length != 0)
		{
			modelIndex = modelIndexes[0];
		}
		base.gameObject.SetActive(value: false);
		GetTransform().gameObject.SetActive(value: false);
	}

	protected override void ParseParam(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		List<int> list = new List<int>();
		list.Add(0);
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 == null || array2.Length != 2)
			{
				continue;
			}
			if (array2[0].StartsWith("mi"))
			{
				if (array2[0] == "mi0")
				{
					list[0] = int.Parse(array2[1]);
				}
				else
				{
					list.Add(int.Parse(array2[1]));
				}
			}
			string a = array2[0];
			if (a == "ml")
			{
				maxLv = Mathf.Max(0, int.Parse(array2[1]) - 1);
			}
		}
		modelIndexes = list.ToArray();
		list.Clear();
		list = null;
	}

	public override void RequestDestroy()
	{
		base.RequestDestroy();
		MonoBehaviourSingleton<InGameProgress>.I.RemoveFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, this);
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction(GetTargetActionId()))
		{
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 position = cameraTransform.position;
			Quaternion rotation = cameraTransform.rotation;
			Vector3 pos = (position - GetTransform().position).normalized + Vector3.up + GetTransform().position;
			if (self.carryingGimmickObject is FieldCarriableEvolveItemGimmickObject && CanEvolve())
			{
				if (evolveMarkerTrans == null)
				{
					evolveMarkerTrans = EffectManager.GetEffect(GetEvolveMarkerName(), GetTransform());
				}
				if (evolveMarkerTrans != null)
				{
					evolveMarkerTrans.Set(pos, rotation);
				}
				if (carryMarkerTrans != null)
				{
					EffectManager.ReleaseEffect(carryMarkerTrans.gameObject);
					carryMarkerTrans = null;
				}
				return;
			}
			if (!self.IsCarrying() && CanCarry())
			{
				if (carryMarkerTrans == null)
				{
					carryMarkerTrans = EffectManager.GetEffect(GetCarryMarkerName(), GetTransform());
				}
				if (carryMarkerTrans != null)
				{
					carryMarkerTrans.Set(pos, rotation);
				}
				if (evolveMarkerTrans != null)
				{
					EffectManager.ReleaseEffect(evolveMarkerTrans.gameObject);
					evolveMarkerTrans = null;
				}
				return;
			}
		}
		if (carryMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(carryMarkerTrans.gameObject);
			carryMarkerTrans = null;
		}
		if (evolveMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(evolveMarkerTrans.gameObject);
			evolveMarkerTrans = null;
		}
	}

	public override string GetObjectName()
	{
		return "CarriableGimmick";
	}

	public override bool IsSearchableNearest()
	{
		return CanCarry();
	}

	public override float GetTargetRadius()
	{
		return kRadius;
	}

	public override float GetTargetSqrRadius()
	{
		return kRadius * kRadius;
	}

	protected override void CreateModel()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(FieldGimmickObject.ConvertModelIndexToKey(m_gimmickType, modelIndex));
			if (loadObject != null)
			{
				modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, m_transform);
				shadowTrans = Utility.FindChild(modelTrans, kShadowNode);
			}
		}
	}

	public virtual Character.ACTION_ID GetTargetActionId()
	{
		return (Character.ACTION_ID)44;
	}

	public virtual string GetCarryMarkerName()
	{
		return kCarryMarkerName;
	}

	public virtual string GetEvolveMarkerName()
	{
		return kEvolveMarkerName;
	}

	public virtual bool CanCarry()
	{
		if (base.gameObject.activeSelf)
		{
			return !isCarrying;
		}
		return false;
	}

	public virtual bool CanEvolve()
	{
		if (currentLv < maxLv)
		{
			return currentLv < modelIndexes.Length - 1;
		}
		return false;
	}

	public virtual bool HasDeploied()
	{
		return hasDeploied;
	}

	public void StartCarry(Player player)
	{
		isCarrying = true;
		if (shadowTrans != null)
		{
			shadowTrans.gameObject.SetActive(value: false);
		}
		OnStartCarry(player);
	}

	public void EndCarry()
	{
		GetTransform().SetParent(parentTrans);
		Vector3 localPosition = GetTransform().localPosition;
		localPosition.y = 0f;
		GetTransform().localPosition = localPosition;
		Quaternion localRotation = GetTransform().localRotation;
		localRotation.x = 0f;
		localRotation.z = 0f;
		GetTransform().localRotation = localRotation;
		if (!hasDeploied)
		{
			if (IsDefenseTool() && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.CountDeploiedCarriableGimmick();
			}
			hasDeploied = true;
		}
		if (shadowTrans != null)
		{
			shadowTrans.gameObject.SetActive(value: true);
		}
		isCarrying = false;
		OnEndCarry();
	}

	protected virtual void OnStartCarry(Player owner)
	{
	}

	protected virtual void OnEndCarry()
	{
	}

	protected virtual bool IsDefenseTool()
	{
		return true;
	}

	public void Evolve()
	{
		if (CanEvolve())
		{
			Object.Destroy(modelTrans.gameObject);
			currentLv++;
			modelIndex = modelIndexes[currentLv];
			CreateModel();
			OnEvolved();
		}
	}

	protected virtual void OnEvolved()
	{
		EffectManager.OneShot(kEvolveEffectName, GetTransform().position, GetTransform().rotation);
		SoundManager.PlayOneShotSE(kEvolveSEId, GetTransform().position);
	}

	public static List<int> GetModelIndexes(string value2)
	{
		List<int> list = new List<int>();
		list.Add(0);
		if (!value2.IsNullOrWhiteSpace())
		{
			string[] array = value2.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':');
				if (array2 != null && array2.Length == 2 && array2[0].StartsWith("mi"))
				{
					if (array2[0] == "mi0")
					{
						list[0] = int.Parse(array2[1]);
					}
					else
					{
						list.Add(int.Parse(array2[1]));
					}
				}
			}
		}
		return list;
	}

	public void SetCarriableGimmickInfo(Coop_Model_StageInfo.FieldCarriableGimmickInfo info)
	{
		GetTransform().position = info.position;
		base.gameObject.SetActive(info.enable);
		if (currentLv != info.currentLv)
		{
			Object.Destroy(modelTrans.gameObject);
			modelIndex = modelIndexes[info.currentLv];
			CreateModel();
		}
		currentLv = info.currentLv;
	}

	public Coop_Model_StageInfo.FieldCarriableGimmickInfo GetCarriableGimmickInfo()
	{
		return new Coop_Model_StageInfo.FieldCarriableGimmickInfo
		{
			pointId = GetId(),
			position = GetTransform().position,
			enable = base.gameObject.activeSelf,
			currentLv = currentLv
		};
	}
}
