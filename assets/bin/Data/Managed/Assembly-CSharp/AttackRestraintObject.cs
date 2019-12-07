using UnityEngine;

public class AttackRestraintObject : MonoBehaviour
{
	public const string ANIM_STATE_LOOP = "LOOP";

	public const string ANIM_STATE_FLICK_ACTION = "ACT";

	public const string ANIM_STATE_END = "END";

	public const string EFFECT_CENTER_OBJECT_NAME = "Center";

	public const string EFFECT_NAME_FLICK_WARNING = "ef_btl_target_flick";

	private Player m_targetPlayer;

	private GameObject m_effectFlickWarning;

	private GameObject m_effectRestraint;

	private Transform m_effectCenterTrans;

	private TargetPoint m_targetPoint;

	private bool m_isValidFlickInput;

	private bool m_isDeleted;

	private bool m_isDisableRemoveRestraintByAttack;

	public TargetPoint BreakTargetPoint => m_targetPoint;

	public bool IsValidFlickInput => m_isValidFlickInput;

	private void Awake()
	{
		Utility.SetLayerWithChildren(base.transform, 11);
	}

	public void Initialize(Player targetPlayer, RestraintInfo restInfo)
	{
		Transform parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
		m_targetPlayer = targetPlayer;
		Transform transform = targetPlayer._transform;
		base.transform.parent = parent;
		Transform effect = EffectManager.GetEffect(restInfo.effectName, parent);
		if (effect != null)
		{
			effect.position = transform.position;
			effect.localScale = Vector3.one;
			effect.localRotation = Quaternion.identity;
			m_effectRestraint = effect.gameObject;
			m_effectCenterTrans = effect.Find("Center");
		}
		SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
		sphereCollider.isTrigger = true;
		sphereCollider.radius = restInfo.radius;
		m_isValidFlickInput = (restInfo.reduceTimeByFlick > 0f);
		m_isDisableRemoveRestraintByAttack = restInfo.isDisableRemoveByPlayerAttack;
		if (targetPlayer.objectType == StageObject.OBJECT_TYPE.SELF)
		{
			if (IsValidFlickInput)
			{
				Transform effect2 = EffectManager.GetEffect("ef_btl_target_flick", base.transform);
				if (effect2 != null)
				{
					effect2.localPosition = Vector3.zero;
					effect2.localScale = Vector3.one;
					effect2.localRotation = Quaternion.identity;
					m_effectFlickWarning = effect2.gameObject;
				}
			}
		}
		else if (!m_isDisableRemoveRestraintByAttack)
		{
			TargetPoint targetPoint = base.gameObject.AddComponent<TargetPoint>();
			targetPoint.markerZShift = 0f;
			targetPoint.offset = Vector3.up * 0.5f;
			targetPoint.regionID = -1;
			targetPoint.isTargetEnable = true;
			targetPoint.isAimEnable = true;
			targetPoint.bleedOffsetPos = Vector3.zero;
			targetPoint.bleedOffsetRot = Vector3.zero;
			targetPoint.aimMarkerPointRate = 1f;
			targetPoint.ForceDisplay();
			m_targetPoint = targetPoint;
		}
		AdjustPosition();
	}

	public void DeleteThis()
	{
		if (!m_isDeleted)
		{
			if (m_effectRestraint != null)
			{
				EffectManager.ReleaseEffect(m_effectRestraint);
				m_effectRestraint = null;
			}
			if (m_effectFlickWarning != null)
			{
				EffectManager.ReleaseEffect(m_effectFlickWarning);
				m_effectFlickWarning = null;
			}
			m_isDeleted = true;
			Object.Destroy(base.gameObject);
		}
	}

	private void LateUpdate()
	{
		AdjustPosition();
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (m_targetPlayer == null)
		{
			DeleteThis();
		}
		else if (CheckValidAttack(collider.gameObject))
		{
			m_targetPlayer.ActRestraintEnd();
			m_targetPlayer = null;
		}
	}

	public void OnFlick()
	{
		if (m_effectRestraint == null || !IsValidFlickInput)
		{
			return;
		}
		Animator component = m_effectRestraint.GetComponent<Animator>();
		if (!(component == null))
		{
			int num = Animator.StringToHash("ACT");
			if (component.HasState(0, num) && component.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("END"))
			{
				component.Play(num, 0, 0f);
				component.Update(0f);
			}
		}
	}

	private bool CheckValidAttack(GameObject hitObj)
	{
		if (m_isDisableRemoveRestraintByAttack)
		{
			return false;
		}
		if ((object)hitObj == null)
		{
			return false;
		}
		IAttackCollider component = hitObj.GetComponent<IAttackCollider>();
		if (component == null)
		{
			return false;
		}
		if (component is HealAttackObject)
		{
			return false;
		}
		StageObject fromObject = component.GetFromObject();
		if ((object)fromObject == null)
		{
			return false;
		}
		return fromObject is Player;
	}

	private void AdjustPosition()
	{
		if (!(m_effectCenterTrans == null))
		{
			Vector3 position = m_effectCenterTrans.position;
			if (m_targetPoint != null)
			{
				Vector3 position2 = position;
				position2.y -= 0.85f;
				m_targetPlayer._transform.position = position2;
			}
			base.transform.position = position;
		}
	}
}
