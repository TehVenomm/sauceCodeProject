using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentBulletObject : MonoBehaviour, IPresentBulletObject
{
	private enum STATE
	{
		INVALID,
		ACTIVE,
		PICKED
	}

	private const string OBJECT_NAME = "PresentBulletObject:";

	private static readonly int ANIM_STATE_PICKED = Animator.StringToHash("PICKED");

	private static readonly int ANIM_STATE_LOOP_INCLUDE_LAYER = Animator.StringToHash("Base Layer.LOOP");

	private static readonly int ANIM_STATE_PICKED_INCLUDE_LAYER = Animator.StringToHash("Base Layer.PICKED");

	private static readonly Vector3 COLLIDER_SIZE = new Vector3(1.2f, 1f, 1.2f);

	private static readonly Vector3 COLLIDER_CENTER = new Vector3(0f, 0.5f, 0f);

	private STATE m_state;

	private int m_presentBulletId;

	private BulletData m_bulletData;

	private Transform m_cachedTransform;

	private Transform m_cachedEffectTransform;

	private BoxCollider m_cachedCollider;

	private Animator m_effectAnimator;

	private StageObjectManager m_stageObjMgr;

	private SkillInfo.SkillParam m_skillParam;

	private int m_ignoreLayerMask;

	private float m_lifeSpan;

	private EffectCtrl m_effectCtrl;

	private List<int> m_buffIds = new List<int>();

	private BulletData.BulletPresent.LIFE_SPAN_TYPE m_lifeSpanType;

	private Character.HealData m_healData;

	public void Initialize(int id, BulletData bulletData, Transform transform)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize PresentBulletObject.");
			return;
		}
		base.gameObject.name = "PresentBulletObject:" + id.ToString();
		m_presentBulletId = id;
		m_bulletData = bulletData;
		m_stageObjMgr = MonoBehaviourSingleton<StageObjectManager>.I;
		m_lifeSpan = m_bulletData.data.appearTime;
		if (m_bulletData.dataPresent != null)
		{
			m_lifeSpanType = m_bulletData.dataPresent.lifeSpanType;
			m_buffIds = m_bulletData.dataPresent.buffIds;
		}
		m_cachedTransform = base.transform;
		m_cachedTransform.parent = m_stageObjMgr._transform;
		m_cachedTransform.position = transform.position;
		m_cachedTransform.localScale = Vector3.one;
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			m_cachedEffectTransform = EffectManager.GetEffect(m_bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
		}
		if (m_cachedEffectTransform != null)
		{
			m_cachedEffectTransform.position = transform.position + bulletData.data.dispOffset;
			m_cachedEffectTransform.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
			m_effectAnimator = m_cachedEffectTransform.gameObject.GetComponent<Animator>();
			m_effectCtrl = m_cachedEffectTransform.gameObject.GetComponent<EffectCtrl>();
		}
		base.gameObject.layer = 31;
		m_ignoreLayerMask |= 41984;
		m_ignoreLayerMask |= 20480;
		m_ignoreLayerMask |= 2490880;
		m_cachedCollider = base.gameObject.AddComponent<BoxCollider>();
		m_cachedCollider.size = COLLIDER_SIZE;
		m_cachedCollider.center = COLLIDER_CENTER;
		m_cachedCollider.isTrigger = true;
		m_cachedCollider.enabled = false;
		m_state = STATE.ACTIVE;
	}

	public void SetPosition(Vector3 position)
	{
		m_cachedTransform.position = position;
		if (m_cachedEffectTransform != null)
		{
			m_cachedEffectTransform.position = position + m_bulletData.data.dispOffset;
		}
	}

	public void SetSkillParam(SkillInfo.SkillParam skillParam)
	{
		m_skillParam = skillParam;
		m_healData = new Character.HealData(m_skillParam.healHp, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>
		{
			10
		});
	}

	public int GetPresentBulletId()
	{
		return m_presentBulletId;
	}

	private void Update()
	{
		if (m_effectAnimator != null && m_effectAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == ANIM_STATE_LOOP_INCLUDE_LAYER)
		{
			m_cachedCollider.enabled = true;
		}
		if (m_state == STATE.ACTIVE && m_lifeSpanType == BulletData.BulletPresent.LIFE_SPAN_TYPE.TIME)
		{
			if (m_lifeSpan <= 0f)
			{
				OnDisappear();
			}
			m_lifeSpan -= Time.deltaTime;
		}
	}

	public void OnDisappear()
	{
		if (m_cachedCollider != null)
		{
			m_cachedCollider.enabled = false;
		}
		m_stageObjMgr.RemovePresentBulletObject(m_presentBulletId);
		if (m_cachedEffectTransform != null && m_cachedEffectTransform.gameObject != null)
		{
			EffectManager.ReleaseEffect(m_cachedEffectTransform.gameObject);
		}
		if (base.gameObject != null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void OnPicked()
	{
		m_state = STATE.PICKED;
		if (m_cachedCollider != null)
		{
			m_cachedCollider.enabled = false;
		}
		StartCoroutine(OnPickedEffect());
	}

	private IEnumerator OnPickedEffect()
	{
		if (m_effectAnimator == null || m_effectCtrl == null)
		{
			yield break;
		}
		m_effectAnimator.Play(ANIM_STATE_PICKED, 0, 0f);
		yield return null;
		while (m_effectAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != ANIM_STATE_PICKED_INCLUDE_LAYER)
		{
			yield return null;
		}
		while (m_effectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		if (m_effectCtrl.waitParticlePlaying)
		{
			for (int i = 0; i < m_effectCtrl.particles.Length; i++)
			{
				ParticleSystem particleSystem = m_effectCtrl.particles[i];
				if (particleSystem != null && particleSystem.isPlaying)
				{
					particleSystem.Stop(withChildren: true);
					yield return null;
				}
			}
		}
		if (m_cachedEffectTransform != null && m_cachedEffectTransform.gameObject != null)
		{
			bool flag = false;
			if (MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				flag = MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(m_cachedEffectTransform.gameObject, no_stock_to_destroy: false);
			}
			if (!flag)
			{
				Object.Destroy(m_cachedEffectTransform.gameObject);
			}
		}
		if (base.gameObject != null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		int layer = collider.gameObject.layer;
		if (((1 << layer) & m_ignoreLayerMask) > 0 || (layer == 8 && collider.gameObject.GetComponent<DangerRader>() != null) || m_state == STATE.PICKED)
		{
			return;
		}
		if (m_skillParam != null)
		{
			_ = m_skillParam.healHp;
		}
		Self component = collider.gameObject.GetComponent<Self>();
		if (!(component != null))
		{
			return;
		}
		OnPicked();
		component.OnHealReceive(m_healData);
		if (!m_buffIds.IsNullOrEmpty())
		{
			for (int i = 0; i < m_buffIds.Count; i++)
			{
				component.StartBuffByBuffTableId(m_buffIds[i], m_skillParam);
			}
		}
		if (component.playerSender != null)
		{
			component.playerSender.OnPickPresentBullet(m_presentBulletId);
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.RemovePresentBulletObject(m_presentBulletId);
		}
	}
}
