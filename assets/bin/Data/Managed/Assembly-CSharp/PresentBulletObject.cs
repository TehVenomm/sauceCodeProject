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

	public PresentBulletObject()
		: this()
	{
	}

	public void Initialize(int id, BulletData bulletData, Transform transform)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Log.Error(LOG.INGAME, "StageObjectManager is invalid. Can't initialize PresentBulletObject.");
			return;
		}
		this.get_gameObject().set_name("PresentBulletObject:" + id.ToString());
		m_presentBulletId = id;
		m_bulletData = bulletData;
		m_stageObjMgr = MonoBehaviourSingleton<StageObjectManager>.I;
		m_lifeSpan = m_bulletData.data.appearTime;
		if (m_bulletData.dataPresent != null)
		{
			m_lifeSpanType = m_bulletData.dataPresent.lifeSpanType;
			m_buffIds = m_bulletData.dataPresent.buffIds;
		}
		m_cachedTransform = this.get_transform();
		m_cachedTransform.set_parent(m_stageObjMgr._transform);
		m_cachedTransform.set_position(transform.get_position());
		m_cachedTransform.set_localScale(Vector3.get_one());
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			m_cachedEffectTransform = EffectManager.GetEffect(m_bulletData.data.effectName, MonoBehaviourSingleton<EffectManager>.I._transform);
		}
		if (m_cachedEffectTransform != null)
		{
			m_cachedEffectTransform.set_position(transform.get_position() + bulletData.data.dispOffset);
			m_cachedEffectTransform.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
			m_effectAnimator = m_cachedEffectTransform.get_gameObject().GetComponent<Animator>();
			m_effectCtrl = m_cachedEffectTransform.get_gameObject().GetComponent<EffectCtrl>();
		}
		this.get_gameObject().set_layer(31);
		m_ignoreLayerMask |= 41984;
		m_ignoreLayerMask |= 20480;
		m_ignoreLayerMask |= 2490880;
		m_cachedCollider = this.get_gameObject().AddComponent<BoxCollider>();
		m_cachedCollider.set_size(COLLIDER_SIZE);
		m_cachedCollider.set_center(COLLIDER_CENTER);
		m_cachedCollider.set_isTrigger(true);
		m_cachedCollider.set_enabled(false);
		m_state = STATE.ACTIVE;
	}

	public void SetPosition(Vector3 position)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		m_cachedTransform.set_position(position);
		if (m_cachedEffectTransform != null)
		{
			m_cachedEffectTransform.set_position(position + m_bulletData.data.dispOffset);
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (m_effectAnimator != null)
		{
			AnimatorStateInfo currentAnimatorStateInfo = m_effectAnimator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_fullPathHash() == ANIM_STATE_LOOP_INCLUDE_LAYER)
			{
				m_cachedCollider.set_enabled(true);
			}
		}
		if (m_state == STATE.ACTIVE && m_lifeSpanType == BulletData.BulletPresent.LIFE_SPAN_TYPE.TIME)
		{
			if (m_lifeSpan <= 0f)
			{
				OnDisappear();
			}
			m_lifeSpan -= Time.get_deltaTime();
		}
	}

	public void OnDisappear()
	{
		if (m_cachedCollider != null)
		{
			m_cachedCollider.set_enabled(false);
		}
		m_stageObjMgr.RemovePresentBulletObject(m_presentBulletId);
		if (m_cachedEffectTransform != null && m_cachedEffectTransform.get_gameObject() != null)
		{
			EffectManager.ReleaseEffect(m_cachedEffectTransform.get_gameObject());
		}
		if (this.get_gameObject() != null)
		{
			Object.Destroy(this.get_gameObject());
		}
	}

	public void OnPicked()
	{
		m_state = STATE.PICKED;
		if (m_cachedCollider != null)
		{
			m_cachedCollider.set_enabled(false);
		}
		this.StartCoroutine(OnPickedEffect());
	}

	private IEnumerator OnPickedEffect()
	{
		if (m_effectAnimator == null || m_effectCtrl == null)
		{
			yield break;
		}
		m_effectAnimator.Play(ANIM_STATE_PICKED, 0, 0f);
		yield return null;
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo = m_effectAnimator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_fullPathHash() != ANIM_STATE_PICKED_INCLUDE_LAYER)
			{
				yield return null;
				continue;
			}
			break;
		}
		while (true)
		{
			AnimatorStateInfo currentAnimatorStateInfo2 = m_effectAnimator.GetCurrentAnimatorStateInfo(0);
			if (!(currentAnimatorStateInfo2.get_normalizedTime() < 1f))
			{
				break;
			}
			yield return null;
		}
		if (m_effectCtrl.waitParticlePlaying)
		{
			for (int i = 0; i < m_effectCtrl.particles.Length; i++)
			{
				ParticleSystem ps = m_effectCtrl.particles[i];
				if (ps != null && ps.get_isPlaying())
				{
					ps.Stop(true);
					yield return null;
				}
			}
		}
		if (m_cachedEffectTransform != null && m_cachedEffectTransform.get_gameObject() != null)
		{
			bool flag = false;
			if (MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				flag = MonoBehaviourSingleton<EffectManager>.I.StockOrDestroy(m_cachedEffectTransform.get_gameObject(), no_stock_to_destroy: false);
			}
			if (!flag)
			{
				Object.Destroy(m_cachedEffectTransform.get_gameObject());
			}
		}
		if (this.get_gameObject() != null)
		{
			Object.Destroy(this.get_gameObject());
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		int layer = collider.get_gameObject().get_layer();
		if (((1 << layer) & m_ignoreLayerMask) > 0 || (layer == 8 && collider.get_gameObject().GetComponent<DangerRader>() != null) || m_state == STATE.PICKED)
		{
			return;
		}
		int num = 0;
		if (m_skillParam != null)
		{
			num = m_skillParam.healHp;
		}
		Self component = collider.get_gameObject().GetComponent<Self>();
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
