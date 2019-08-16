using System.Collections;
using UnityEngine;

public class FieldHealingPointObject : FieldGimmickObject
{
	private readonly Color COLOR_CRYSTAL_IS_READY = new Color(0f, 14f / 51f, 47f / 255f);

	private readonly Color COLOR_CRYSTAL_IS_NOT_READY = new Color(0.117647059f, 0.117647059f, 0.117647059f);

	private readonly int STATE_END = Animator.StringToHash("END");

	private readonly int STATE_END_INCLUDE_LAYER = Animator.StringToHash("Base Layer.END");

	private readonly int STATE_HEAL_POINT_ACTIVE = Animator.StringToHash("CMN_healpoint01");

	public const string EFFECT_NAME_HEALING_POINT_FOR_BASE = "ef_btl_heal_spot_01_01";

	public const string EFFECT_NAME_HEALING_POINT_FOR_CRYSTAL = "ef_btl_heal_spot_01_02";

	public const int SE_HEALING = 30000038;

	private float m_healInterval;

	private float healTimer;

	private bool isReady = true;

	private Renderer[] rendererArray;

	private Transform effectTransForBase;

	private Transform effectTransForCrystal;

	private Animator animForModel;

	private Animator animForEffectOnBase;

	private Animator animForEffectOnCrystal;

	private Character.HealData healData;

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		m_healInterval = pointData.value1;
		animForModel = m_transform.GetComponentInChildren<Animator>();
		rendererArray = m_transform.GetComponentsInChildren<Renderer>(true);
		if (rendererArray != null)
		{
			SetReadyForHeal();
		}
		healData = new Character.HealData((!(MonoBehaviourSingleton<StageObjectManager>.I.self == null)) ? MonoBehaviourSingleton<StageObjectManager>.I.self.hpMax : 0, HEAL_TYPE.ALL_BADSTATUS, HEAL_EFFECT_TYPE.BASIS, null);
	}

	private void SetReadyForHeal()
	{
		isReady = true;
		healTimer = 0f;
		if (effectTransForBase != null)
		{
			if (effectTransForBase.get_gameObject() != null)
			{
				Object.Destroy(effectTransForBase.get_gameObject());
			}
			effectTransForBase = null;
		}
		if (effectTransForCrystal != null)
		{
			if (effectTransForCrystal.get_gameObject() != null)
			{
				Object.Destroy(effectTransForCrystal.get_gameObject());
			}
			effectTransForCrystal = null;
		}
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			effectTransForBase = EffectManager.GetEffect("ef_btl_heal_spot_01_01", modelTrans.Find("base01"));
			effectTransForCrystal = EffectManager.GetEffect("ef_btl_heal_spot_01_02", modelTrans.Find("crystal01"));
			if (effectTransForBase != null)
			{
				animForEffectOnBase = effectTransForBase.GetComponent<Animator>();
			}
			if (effectTransForCrystal != null)
			{
				animForEffectOnCrystal = effectTransForCrystal.GetComponent<Animator>();
			}
		}
		if (animForModel != null)
		{
			animForModel.Play(STATE_HEAL_POINT_ACTIVE, 0);
		}
		if (rendererArray != null)
		{
			Utility.MaterialForEach(rendererArray, delegate(Material material)
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				if (material.HasProperty("_SpeLightColor"))
				{
					material.SetColor("_SpeLightColor", COLOR_CRYSTAL_IS_READY);
				}
			});
		}
	}

	private void SetNotReadyForHeal()
	{
		isReady = false;
		if (animForModel != null)
		{
			animForModel.Play(STATE_END, 0);
		}
		this.StartCoroutine(EndEffectOnBase());
		this.StartCoroutine(EndEffectOnCrystal());
		if (rendererArray != null)
		{
			Utility.MaterialForEach(rendererArray, delegate(Material material)
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				if (material.HasProperty("_SpeLightColor"))
				{
					material.SetColor("_SpeLightColor", COLOR_CRYSTAL_IS_NOT_READY);
				}
			});
		}
	}

	private IEnumerator EndEffectOnBase()
	{
		if (animForEffectOnBase == null)
		{
			yield break;
		}
		animForEffectOnBase.Play(STATE_END, 0, 0f);
		yield return null;
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animForEffectOnBase.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_fullPathHash() != STATE_END_INCLUDE_LAYER)
			{
				yield return null;
				continue;
			}
			break;
		}
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = animForEffectOnBase.GetCurrentAnimatorStateInfo(0);
			if (!(currentAnimatorStateInfo2.get_normalizedTime() <= 1f))
			{
				break;
			}
			yield return null;
		}
		if (effectTransForBase != null)
		{
			Object.Destroy(effectTransForBase.get_gameObject());
			effectTransForBase = null;
		}
	}

	private IEnumerator EndEffectOnCrystal()
	{
		if (animForEffectOnCrystal == null)
		{
			yield break;
		}
		animForEffectOnCrystal.Play(STATE_END, 0, 0f);
		yield return null;
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animForEffectOnBase.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_fullPathHash() != STATE_END_INCLUDE_LAYER)
			{
				yield return null;
				continue;
			}
			break;
		}
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = animForEffectOnCrystal.GetCurrentAnimatorStateInfo(0);
			if (!(currentAnimatorStateInfo2.get_normalizedTime() <= 1f))
			{
				break;
			}
			yield return null;
		}
		if (effectTransForCrystal != null)
		{
			Object.Destroy(effectTransForCrystal.get_gameObject());
			effectTransForCrystal = null;
		}
	}

	private void Update()
	{
		if (!isReady)
		{
			healTimer += Time.get_deltaTime();
		}
		if (m_healInterval <= healTimer)
		{
			SetReadyForHeal();
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (!(collider.get_gameObject().GetComponent<Self>() == null) && MonoBehaviourSingleton<InGameProgress>.I.isBattleStart && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE && !MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!(self == null) && !self.isDead && isReady)
			{
				self.OnHealReceive(healData);
				SoundManager.PlayOneShotSE(30000038, self._position);
				SetNotReadyForHeal();
			}
		}
	}

	public override void RequestDestroy()
	{
		if (effectTransForBase != null)
		{
			if (effectTransForBase.get_gameObject() != null)
			{
				Object.Destroy(effectTransForBase.get_gameObject());
			}
			effectTransForBase = null;
		}
		if (effectTransForCrystal != null)
		{
			if (effectTransForCrystal.get_gameObject() != null)
			{
				Object.Destroy(effectTransForCrystal.get_gameObject());
			}
			effectTransForCrystal = null;
		}
		base.RequestDestroy();
	}

	public override string GetObjectName()
	{
		return "HealingPoint";
	}
}
