using System.Collections.Generic;
using UnityEngine;

public class FieldSupplyGimmickObject : FieldGimmickObject
{
	public class ActiveCondition
	{
		public float maxDefenseTargetHp = 100f;

		public int minDestroiedEnemyCount;

		public float minElapsedTime;

		public int minDeploiedGimmickNum;
	}

	public static readonly string kSupplyMarkerName = "ef_btl_target_unknown_01";

	public static readonly string kBreakEffectName = "ef_btl_bg_woodbreak_01";

	public static readonly int kBreakSEId = 20000039;

	private static readonly int kShiftIndex = 1000;

	private static readonly int kIntervalFrameForObserve = 30;

	private static readonly float kRadius = 2f;

	public int modelIndex;

	protected float coolTime;

	protected float maxCoolTime = 3f;

	protected List<uint> supplyGimmickIds;

	protected int suppliedCount;

	protected bool canUse;

	protected Transform targetMarkerTrans;

	protected bool isHost;

	protected List<ActiveCondition> activeConditions;

	protected override void Awake()
	{
		base.Awake();
		supplyGimmickIds = new List<uint>();
		activeConditions = new List<ActiveCondition>();
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		modelTrans.get_gameObject().SetActive(false);
	}

	protected override void ParseParam(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return;
		}
		int result = 1;
		uint result2 = 0u;
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2)
			{
				switch (array2[0])
				{
				case "ct":
					float.TryParse(array2[1], out maxCoolTime);
					break;
				case "mi":
					int.TryParse(array2[1], out modelIndex);
					break;
				case "n":
					int.TryParse(array2[1], out result);
					break;
				case "id":
					uint.TryParse(array2[1], out result2);
					break;
				}
			}
		}
		for (int j = 0; j < result; j++)
		{
			activeConditions.Add(new ActiveCondition());
			supplyGimmickIds.Add(result2);
		}
		string[] array3 = new string[5]
		{
			"id",
			"hp",
			"de",
			"et",
			"gn"
		};
		for (int k = 0; k < array.Length; k++)
		{
			string[] array4 = array[k].Split(':');
			if (array4 == null || array4.Length != 2)
			{
				continue;
			}
			for (int l = 0; l < array3.Length; l++)
			{
				int result3 = 0;
				if (array4[0].StartsWith(array3[l]) && int.TryParse(array4[0].Replace(array3[l], string.Empty), out result3) && result3 < result)
				{
					switch (array3[l])
					{
					case "id":
						supplyGimmickIds[result3] = uint.Parse(array4[1]);
						break;
					case "hp":
						activeConditions[result3].maxDefenseTargetHp = float.Parse(array4[1]);
						break;
					case "de":
						activeConditions[result3].minDestroiedEnemyCount = int.Parse(array4[1]);
						break;
					case "et":
						activeConditions[result3].minElapsedTime = float.Parse(array4[1]);
						break;
					case "gn":
						activeConditions[result3].minDeploiedGimmickNum = int.Parse(array4[1]);
						break;
					}
				}
			}
		}
	}

	protected override void CreateModel()
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable != null)
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickModelTable.Get(ConvertModelIndexToKey(modelIndex));
			if (loadObject != null)
			{
				modelTrans = ResourceUtility.Realizes(loadObject.loadedObject, m_transform);
			}
		}
	}

	public static uint ConvertModelIndexToKey(int idx)
	{
		return (uint)(idx * kShiftIndex + 20);
	}

	public static string ConvertModelIndexToName(int idx)
	{
		return $"CMN_supply{idx + 1:D2}";
	}

	public static int GetModelIndex(string value2)
	{
		if (value2.IsNullOrWhiteSpace())
		{
			return 0;
		}
		string[] array = value2.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2 != null && array2.Length == 2 && array2[0] == "mi")
			{
				return int.Parse(array2[1]);
			}
		}
		return 0;
	}

	public override void UpdateTargetMarker(bool isNear)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (CanUse() && isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)44))
		{
			if (targetMarkerTrans == null)
			{
				targetMarkerTrans = EffectManager.GetEffect(kSupplyMarkerName, GetTransform());
			}
			if (targetMarkerTrans != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - GetTransform().get_position();
				Vector3 pos = val.get_normalized() * kRadius + Vector3.get_up() + GetTransform().get_position();
				targetMarkerTrans.Set(pos, rotation);
			}
		}
		else if (targetMarkerTrans != null)
		{
			EffectManager.ReleaseEffect(targetMarkerTrans.get_gameObject());
		}
	}

	private void LateUpdate()
	{
		if (coolTime > 0f)
		{
			coolTime -= Time.get_deltaTime();
			coolTime = Mathf.Max(0f, coolTime);
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && Time.get_frameCount() % kIntervalFrameForObserve == 0)
		{
			FieldCarriableGimmickObject fieldCarriableGimmickObject = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, (int)supplyGimmickIds[suppliedCount]) as FieldCarriableGimmickObject;
			canUse = (coolTime <= 0f && supplyGimmickIds.Count > suppliedCount && fieldCarriableGimmickObject != null && !fieldCarriableGimmickObject.get_gameObject().get_activeSelf() && MonoBehaviourSingleton<StageObjectManager>.I.GetWaveMatchTargetHpRate() <= activeConditions[suppliedCount].maxDefenseTargetHp && MonoBehaviourSingleton<InGameProgress>.I.partyDefeatCount >= activeConditions[suppliedCount].minDestroiedEnemyCount && MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime() >= activeConditions[suppliedCount].minElapsedTime && MonoBehaviourSingleton<InGameProgress>.I.GetCarriableGimmickDeploiedCount() >= activeConditions[suppliedCount].minDeploiedGimmickNum);
		}
		if (!modelTrans.get_gameObject().get_activeSelf() && CanUse())
		{
			Active();
		}
		else if (modelTrans.get_gameObject().get_activeSelf() && !CanUse())
		{
			modelTrans.get_gameObject().SetActive(false);
		}
	}

	public virtual void Active()
	{
		modelTrans.get_gameObject().SetActive(true);
		canUse = true;
		OnActive();
		if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.packetSender.OnActiveSupply(GetId());
		}
	}

	protected virtual void OnActive()
	{
		MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.PlaySupplyInformation();
		SoundManager.PlayOneshotJingle(40000155);
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Attach(this);
		}
	}

	public override float GetTargetSqrRadius()
	{
		return kRadius * kRadius;
	}

	public virtual bool CanUse()
	{
		return canUse;
	}

	public override bool IsSearchableNearest()
	{
		return CanUse() && modelTrans.get_gameObject().get_activeSelf();
	}

	public override void RequestDestroy()
	{
		base.RequestDestroy();
		MonoBehaviourSingleton<InGameProgress>.I.RemoveFieldGimmickObj(InGameProgress.eFieldGimmick.SupplyGimmick, this);
	}

	public FieldCarriableGimmickObject SupplyGimmick()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		if (!CanUse())
		{
			return null;
		}
		coolTime = maxCoolTime;
		FieldCarriableGimmickObject fieldCarriableGimmickObject = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, (int)supplyGimmickIds[suppliedCount]) as FieldCarriableGimmickObject;
		if (fieldCarriableGimmickObject == null)
		{
			return null;
		}
		fieldCarriableGimmickObject.GetTransform().set_position(GetTransform().get_position());
		fieldCarriableGimmickObject.GetTransform().set_rotation(GetTransform().get_rotation());
		fieldCarriableGimmickObject.get_gameObject().SetActive(true);
		if (MonoBehaviourSingleton<MiniMap>.IsValid())
		{
			MonoBehaviourSingleton<MiniMap>.I.Detach(this);
		}
		suppliedCount++;
		if (suppliedCount >= supplyGimmickIds.Count)
		{
			RequestDestroy();
		}
		else
		{
			modelTrans.get_gameObject().SetActive(false);
			canUse = false;
		}
		EffectManager.OneShot(kBreakEffectName, GetTransform().get_position(), GetTransform().get_rotation());
		SoundManager.PlayOneShotSE(kBreakSEId, GetTransform().get_position());
		return fieldCarriableGimmickObject;
	}

	public void SetSupplyGimmickInfo(Coop_Model_StageInfo.FieldSupplyGimmickInfo info)
	{
		suppliedCount = info.suppliedCount;
		if (info.canUse)
		{
			Active();
			return;
		}
		modelTrans.get_gameObject().SetActive(false);
		canUse = false;
	}

	public Coop_Model_StageInfo.FieldSupplyGimmickInfo GetSupplyGimmickInfo()
	{
		Coop_Model_StageInfo.FieldSupplyGimmickInfo fieldSupplyGimmickInfo = new Coop_Model_StageInfo.FieldSupplyGimmickInfo();
		fieldSupplyGimmickInfo.pointId = GetId();
		fieldSupplyGimmickInfo.suppliedCount = suppliedCount;
		fieldSupplyGimmickInfo.canUse = canUse;
		return fieldSupplyGimmickInfo;
	}
}
