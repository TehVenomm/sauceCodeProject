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
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (isNear && (Object)self != (Object)null && self.IsChangeableAction((Character.ACTION_ID)34))
		{
			string sonarTargetEffect = ResourceName.GetSonarTargetEffect();
			if ((Object)targetMarker == (Object)null && !string.IsNullOrEmpty(sonarTargetEffect))
			{
				targetMarker = EffectManager.GetEffect(sonarTargetEffect, _transform);
			}
			if ((Object)targetMarker != (Object)null)
			{
				Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
				Vector3 position = cameraTransform.position;
				Quaternion rotation = cameraTransform.rotation;
				Vector3 pos = (position - _transform.position).normalized + Vector3.up + _transform.position;
				targetMarker.Set(pos, rotation);
			}
		}
		else if ((Object)targetMarker != (Object)null)
		{
			EffectManager.ReleaseEffect(targetMarker.gameObject, true, false);
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
		if (IsValidSonar())
		{
			StartCoroutine(ActSonar());
		}
	}

	public override void RequestDestroy()
	{
		if ((Object)sonarEffect != (Object)null)
		{
			if ((Object)sonarEffect.gameObject != (Object)null)
			{
				Object.Destroy(sonarEffect.gameObject);
			}
			sonarEffect = null;
		}
		if ((Object)sonarTouchEffect != (Object)null)
		{
			if ((Object)sonarTouchEffect.gameObject != (Object)null)
			{
				Object.Destroy(sonarTouchEffect.gameObject);
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
		_transform = base.transform;
		Utility.SetLayerWithChildren(base.transform, 19);
	}

	private IEnumerator ActSonar()
	{
		acting = true;
		SoundManager.PlayOneShotSE(40000107, MonoBehaviourSingleton<StageObjectManager>.I.self._position);
		if ((Object)sonarTouchEffect == (Object)null)
		{
			sonarTouchEffect = EffectManager.GetEffect("ef_btl_sonar_02", modelTrans);
		}
		sonarTouchEffect.gameObject.SetActive(true);
		yield return (object)new WaitForSeconds(0.9f);
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("FieldSonarObject.OnTriggerEnter", base.gameObject, "EXPLOREMAP", ExploreMap.OPEN_MAP_TYPE.SONAR, null, true);
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
		if ((Object)self == (Object)null)
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
