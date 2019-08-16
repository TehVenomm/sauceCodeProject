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
		parentTrans = GetTransform().get_parent();
		isCarrying = false;
		if (modelIndexes.Length > 0)
		{
			modelIndex = modelIndexes[0];
		}
		this.get_gameObject().SetActive(false);
		GetTransform().get_gameObject().SetActive(false);
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
			string text = array2[0];
			if (text != null && text == "ml")
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
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction(GetTargetActionId()))
		{
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 position = cameraTransform.get_position();
			Quaternion rotation = cameraTransform.get_rotation();
			Vector3 val = position - GetTransform().get_position();
			Vector3 pos = val.get_normalized() + Vector3.get_up() + GetTransform().get_position();
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
					EffectManager.ReleaseEffect(carryMarkerTrans.get_gameObject());
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
					EffectManager.ReleaseEffect(evolveMarkerTrans.get_gameObject());
					evolveMarkerTrans = null;
				}
				return;
			}
		}
		if (carryMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(carryMarkerTrans.get_gameObject());
			carryMarkerTrans = null;
		}
		if (evolveMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(evolveMarkerTrans.get_gameObject());
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
		return this.get_gameObject().get_activeSelf() && !isCarrying;
	}

	public virtual bool CanEvolve()
	{
		return currentLv < maxLv && currentLv < modelIndexes.Length - 1;
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
			shadowTrans.get_gameObject().SetActive(false);
		}
		OnStartCarry(player);
	}

	public void EndCarry()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		GetTransform().SetParent(parentTrans);
		Vector3 localPosition = GetTransform().get_localPosition();
		localPosition.y = 0f;
		GetTransform().set_localPosition(localPosition);
		Quaternion localRotation = GetTransform().get_localRotation();
		localRotation.x = 0f;
		localRotation.z = 0f;
		GetTransform().set_localRotation(localRotation);
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
			shadowTrans.get_gameObject().SetActive(true);
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
			Object.Destroy(modelTrans.get_gameObject());
			currentLv++;
			modelIndex = modelIndexes[currentLv];
			CreateModel();
			OnEvolved();
		}
	}

	protected virtual void OnEvolved()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		EffectManager.OneShot(kEvolveEffectName, GetTransform().get_position(), GetTransform().get_rotation());
		SoundManager.PlayOneShotSE(kEvolveSEId, GetTransform().get_position());
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		GetTransform().set_position(info.position);
		this.get_gameObject().SetActive(info.enable);
		if (currentLv != info.currentLv)
		{
			Object.Destroy(modelTrans.get_gameObject());
			modelIndex = modelIndexes[info.currentLv];
			CreateModel();
		}
		currentLv = info.currentLv;
	}

	public Coop_Model_StageInfo.FieldCarriableGimmickInfo GetCarriableGimmickInfo()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_StageInfo.FieldCarriableGimmickInfo fieldCarriableGimmickInfo = new Coop_Model_StageInfo.FieldCarriableGimmickInfo();
		fieldCarriableGimmickInfo.pointId = GetId();
		fieldCarriableGimmickInfo.position = GetTransform().get_position();
		fieldCarriableGimmickInfo.enable = this.get_gameObject().get_activeSelf();
		fieldCarriableGimmickInfo.currentLv = currentLv;
		return fieldCarriableGimmickInfo;
	}
}
