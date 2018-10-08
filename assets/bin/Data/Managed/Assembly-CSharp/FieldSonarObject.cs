using System.Collections;
using UnityEngine;

public class FieldSonarObject : FieldGimmickObject
{
	public const string SonarEffectName = "ef_btl_sonar_01";

	public const string SonarTouchEffectName = "ef_btl_sonar_02";

	public const int SonarSE = 40000107;

	private Transform sonarEffect;

	private Transform sonarTouchEffect;

	private Transform targetMarker;

	private Transform _transform;

	private bool acting;

	public override void UpdateTargetMarker(bool isNear)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && self != null && self.IsChangeableAction((Character.ACTION_ID)34))
		{
			string sonarTargetEffect = ResourceName.GetSonarTargetEffect();
			if (targetMarker == null && !string.IsNullOrEmpty(sonarTargetEffect))
			{
				targetMarker = EffectManager.GetEffect(sonarTargetEffect, _transform);
			}
			if (targetMarker != null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.get_position();
				Quaternion rotation = cameraTransform.get_rotation();
				Vector3 val = position - _transform.get_position();
				Vector3 pos = val.get_normalized() + Vector3.get_up() + _transform.get_position();
				targetMarker.Set(pos, rotation);
			}
		}
		else if (targetMarker != null)
		{
			EffectManager.ReleaseEffect(targetMarker.get_gameObject(), true, false);
		}
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		if (MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			MonoBehaviourSingleton<UIStatusGizmoManager>.I.CreateSonar(this);
		}
		base.Initialize(pointData);
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			sonarEffect = EffectManager.GetEffect("ef_btl_sonar_01", modelTrans);
		}
	}

	public void StartSonar()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (IsValidSonar())
		{
			this.StartCoroutine(ActSonar());
		}
	}

	public override void RequestDestroy()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (sonarEffect != null)
		{
			if (sonarEffect.get_gameObject() != null)
			{
				Object.Destroy(sonarEffect.get_gameObject());
			}
			sonarEffect = null;
		}
		if (sonarTouchEffect != null)
		{
			if (sonarTouchEffect.get_gameObject() != null)
			{
				Object.Destroy(sonarTouchEffect.get_gameObject());
			}
			sonarTouchEffect = null;
		}
		base.RequestDestroy();
	}

	public override string GetObjectName()
	{
		return "Sonar";
	}

	protected override void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		_transform = this.get_transform();
		Utility.SetLayerWithChildren(this.get_transform(), 19);
	}

	private IEnumerator ActSonar()
	{
		acting = true;
		SoundManager.PlayOneShotSE(40000107, MonoBehaviourSingleton<StageObjectManager>.I.self._position);
		if (sonarTouchEffect == null)
		{
			sonarTouchEffect = EffectManager.GetEffect("ef_btl_sonar_02", modelTrans);
		}
		sonarTouchEffect.get_gameObject().SetActive(true);
		yield return (object)new WaitForSeconds(0.9f);
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("FieldSonarObject.OnTriggerEnter", this.get_gameObject(), "EXPLOREMAP", ExploreMap.OPEN_MAP_TYPE.SONAR, null, true);
		}
		acting = false;
	}

	private bool IsValidSonar()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.I.isBattleStart)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.progressEndType != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self == null)
		{
			return false;
		}
		if (self.isDead)
		{
			return false;
		}
		if (acting)
		{
			return false;
		}
		return true;
	}
}
