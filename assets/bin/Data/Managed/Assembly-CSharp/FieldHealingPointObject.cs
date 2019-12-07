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
		rendererArray = m_transform.GetComponentsInChildren<Renderer>(includeInactive: true);
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
			if (effectTransForBase.gameObject != null)
			{
				Object.Destroy(effectTransForBase.gameObject);
			}
			effectTransForBase = null;
		}
		if (effectTransForCrystal != null)
		{
			if (effectTransForCrystal.gameObject != null)
			{
				Object.Destroy(effectTransForCrystal.gameObject);
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
		StartCoroutine(EndEffectOnBase());
		StartCoroutine(EndEffectOnCrystal());
		if (rendererArray != null)
		{
			Utility.MaterialForEach(rendererArray, delegate(Material material)
			{
				if (material.HasProperty("_SpeLightColor"))
				{
					material.SetColor("_SpeLightColor", COLOR_CRYSTAL_IS_NOT_READY);
				}
			});
		}
	}

	private IEnumerator EndEffectOnBase()
	{
		if (!(animForEffectOnBase == null))
		{
			animForEffectOnBase.Play(STATE_END, 0, 0f);
			yield return null;
			while (animForEffectOnBase.GetCurrentAnimatorStateInfo(0).fullPathHash != STATE_END_INCLUDE_LAYER)
			{
				yield return null;
			}
			while (animForEffectOnBase.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
			{
				yield return null;
			}
			if (effectTransForBase != null)
			{
				Object.Destroy(effectTransForBase.gameObject);
				effectTransForBase = null;
			}
		}
	}

	private IEnumerator EndEffectOnCrystal()
	{
		if (!(animForEffectOnCrystal == null))
		{
			animForEffectOnCrystal.Play(STATE_END, 0, 0f);
			yield return null;
			while (animForEffectOnBase.GetCurrentAnimatorStateInfo(0).fullPathHash != STATE_END_INCLUDE_LAYER)
			{
				yield return null;
			}
			while (animForEffectOnCrystal.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
			{
				yield return null;
			}
			if (effectTransForCrystal != null)
			{
				Object.Destroy(effectTransForCrystal.gameObject);
				effectTransForCrystal = null;
			}
		}
	}

	private void Update()
	{
		if (!isReady)
		{
			healTimer += Time.deltaTime;
		}
		if (m_healInterval <= healTimer)
		{
			SetReadyForHeal();
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (!(collider.gameObject.GetComponent<Self>() == null) && MonoBehaviourSingleton<InGameProgress>.I.isBattleStart && MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE && !MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection && MonoBehaviourSingleton<StageObjectManager>.IsValid())
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
			if (effectTransForBase.gameObject != null)
			{
				Object.Destroy(effectTransForBase.gameObject);
			}
			effectTransForBase = null;
		}
		if (effectTransForCrystal != null)
		{
			if (effectTransForCrystal.gameObject != null)
			{
				Object.Destroy(effectTransForCrystal.gameObject);
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
