using UnityEngine;

public class AttackRestraintObject
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

	public AttackRestraintObject()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		Utility.SetLayerWithChildren(this.get_transform(), 11);
	}

	public void Initialize(Player targetPlayer, RestraintInfo restInfo)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		Transform val = (!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
		m_targetPlayer = targetPlayer;
		Transform transform = targetPlayer._transform;
		Transform val2 = this.get_transform();
		val2.set_parent(val);
		Transform effect = EffectManager.GetEffect(restInfo.effectName, val);
		effect.set_position(transform.get_position());
		effect.set_localScale(Vector3.get_one());
		effect.set_localRotation(Quaternion.get_identity());
		m_effectRestraint = effect.get_gameObject();
		m_effectCenterTrans = effect.FindChild("Center");
		SphereCollider val3 = this.get_gameObject().AddComponent<SphereCollider>();
		val3.set_isTrigger(true);
		val3.set_radius(restInfo.radius);
		m_isValidFlickInput = (restInfo.reduceTimeByFlick > 0f);
		m_isDisableRemoveRestraintByAttack = restInfo.isDisableRemoveByPlayerAttack;
		if (targetPlayer.objectType == StageObject.OBJECT_TYPE.SELF)
		{
			if (IsValidFlickInput)
			{
				Transform effect2 = EffectManager.GetEffect("ef_btl_target_flick", this.get_transform());
				if (effect2 != null)
				{
					effect2.set_localPosition(Vector3.get_zero());
					effect2.set_localScale(Vector3.get_one());
					effect2.set_localRotation(Quaternion.get_identity());
					m_effectFlickWarning = effect2.get_gameObject();
				}
			}
		}
		else if (!m_isDisableRemoveRestraintByAttack)
		{
			TargetPoint targetPoint = this.get_gameObject().AddComponent<TargetPoint>();
			targetPoint.markerZShift = 0f;
			targetPoint.offset = Vector3.get_up() * 0.5f;
			targetPoint.regionID = -1;
			targetPoint.isTargetEnable = true;
			targetPoint.isAimEnable = true;
			targetPoint.bleedOffsetPos = Vector3.get_zero();
			targetPoint.bleedOffsetRot = Vector3.get_zero();
			targetPoint.aimMarkerPointRate = 1f;
			targetPoint.ForceDisplay();
			m_targetPoint = targetPoint;
		}
		AdjustPosition();
	}

	public void DeleteThis()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		if (!m_isDeleted)
		{
			if (m_effectRestraint != null)
			{
				EffectManager.ReleaseEffect(m_effectRestraint, true, false);
				m_effectRestraint = null;
			}
			if (m_effectFlickWarning != null)
			{
				EffectManager.ReleaseEffect(m_effectFlickWarning, true, false);
				m_effectFlickWarning = null;
			}
			m_isDeleted = true;
			Object.Destroy(this.get_gameObject());
		}
	}

	private void LateUpdate()
	{
		AdjustPosition();
	}

	private void OnTriggerEnter(Collider collider)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (m_targetPlayer == null)
		{
			DeleteThis();
		}
		else if (CheckValidAttack(collider.get_gameObject()))
		{
			m_targetPlayer.ActRestraintEnd();
			m_targetPlayer = null;
		}
	}

	public void OnFlick()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_effectRestraint == null) && IsValidFlickInput)
		{
			Animator component = m_effectRestraint.GetComponent<Animator>();
			if (!(component == null))
			{
				int num = Animator.StringToHash("ACT");
				if (component.HasState(0, num))
				{
					AnimatorStateInfo currentAnimatorStateInfo = component.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.get_fullPathHash() != Animator.StringToHash("END"))
					{
						component.Play(num, 0, 0f);
						component.Update(0f);
					}
				}
			}
		}
	}

	private bool CheckValidAttack(GameObject hitObj)
	{
		if (m_isDisableRemoveRestraintByAttack)
		{
			return false;
		}
		if (object.ReferenceEquals(hitObj, null))
		{
			return false;
		}
		IAttackCollider component = hitObj.GetComponent<IAttackCollider>();
		if (object.ReferenceEquals(component, null))
		{
			return false;
		}
		if (component is HealAttackObject)
		{
			return false;
		}
		StageObject fromObject = component.GetFromObject();
		if (object.ReferenceEquals(fromObject, null))
		{
			return false;
		}
		return fromObject is Player;
	}

	private void AdjustPosition()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_effectCenterTrans == null))
		{
			Vector3 position = m_effectCenterTrans.get_position();
			if (m_targetPoint != null)
			{
				Vector3 position2 = position;
				position2.y -= 0.85f;
				m_targetPlayer._transform.set_position(position2);
			}
			this.get_transform().set_position(position);
		}
	}
}
