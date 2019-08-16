using System;
using UnityEngine;

public class AttackUndead : MonoBehaviour
{
	private enum Function
	{
		NONE,
		MAIN,
		DELETE
	}

	private enum State
	{
		NONE,
		TRACKING,
		ATTACK
	}

	private const float AIM_RAD_MIN = 0.00174532924f;

	private const string ANIM_STATE_END = "END";

	private const float FLOATING_RATE_IN_ATTACK = 0.3f;

	private const float STOP_UNDEAD_DISTANCE = 0.05f;

	private BulletData.BulletUndead m_undeadData;

	private StageObject m_attacker;

	private AttackInfo m_atkInfo;

	private Transform m_cachedTransform;

	private StageObject m_targetObject;

	private GameObject m_effectObj;

	private string m_landHitEffectName = string.Empty;

	private float m_aimAngleSpeed;

	private float m_moveSpeed;

	private float m_aliveTimer;

	private bool m_isDeleted;

	private Function m_func;

	private State m_state;

	private int m_effectDeleteAnimHash;

	private Animator m_effectAnimator;

	private float m_attackIntervalTimer;

	public AttackUndead()
		: this()
	{
	}

	public void Initialize(StageObject attacker, AttackInfo atkInfo, StageObject targetObj, Transform launchTrans, Vector3 offsetPos, Quaternion offsetRot)
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		m_attacker = attacker;
		m_atkInfo = atkInfo;
		AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
		if (attackHitInfo != null)
		{
			attackHitInfo.enableIdentityCheck = false;
		}
		BulletData bulletData = atkInfo.bulletData;
		m_landHitEffectName = bulletData.data.landHiteffectName;
		m_aliveTimer = bulletData.data.appearTime;
		m_moveSpeed = bulletData.data.speed;
		BulletData.BulletUndead dataUndead = bulletData.dataUndead;
		m_aimAngleSpeed = dataUndead.lookAtAngle * ((float)Math.PI / 180f);
		m_undeadData = dataUndead;
		m_isDeleted = false;
		m_cachedTransform = this.get_transform();
		m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
		m_cachedTransform.set_position(launchTrans.get_position() + launchTrans.get_rotation() * offsetPos);
		m_cachedTransform.set_rotation(launchTrans.get_rotation() * offsetRot);
		m_cachedTransform.set_localScale(bulletData.data.timeStartScale);
		Transform effect = EffectManager.GetEffect(bulletData.data.effectName, this.get_transform());
		effect.set_localPosition(bulletData.data.dispOffset);
		effect.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
		effect.set_localScale(Vector3.get_one());
		m_effectObj = effect.get_gameObject();
		m_effectAnimator = m_effectObj.GetComponent<Animator>();
		m_targetObject = targetObj;
		RequestMain();
	}

	private void Update()
	{
		switch (m_func)
		{
		case Function.MAIN:
			FuncMain();
			break;
		case Function.DELETE:
			FuncDelete();
			break;
		}
	}

	private void RequestMain()
	{
		RequestFunction(Function.MAIN);
	}

	private void FuncMain()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		if (m_isDeleted)
		{
			return;
		}
		m_aliveTimer -= Time.get_deltaTime();
		if (m_aliveTimer <= 0f)
		{
			RequestDestroy();
			return;
		}
		if (m_targetObject == null)
		{
			RequestDestroy();
			return;
		}
		Vector3 position = m_targetObject.get_transform().get_position();
		switch (m_state)
		{
		case State.TRACKING:
		{
			position.y = m_undeadData.floatingHeight;
			Vector3 val3 = position - m_cachedTransform.get_position();
			Vector3 normalized2 = val3.get_normalized();
			Vector3 val4 = m_cachedTransform.get_position() + normalized2 * m_moveSpeed * Time.get_deltaTime();
			val4._002Ector(val4.x, val4.y + Mathf.Sin(Time.get_time()) * m_undeadData.floatingCoef, val4.z);
			m_cachedTransform.set_position(val4);
			position.y = 0f;
			val4.y = 0f;
			if (Vector3.Distance(position, val4) <= m_undeadData.attackRange)
			{
				m_attackIntervalTimer = m_undeadData.attackInterval;
				ForwardState();
			}
			break;
		}
		case State.ATTACK:
		{
			Vector3 val = m_cachedTransform.get_position();
			position.y = 0f;
			val.y = 0f;
			if (Vector3.Distance(position, val) < 0.05f)
			{
				val = m_cachedTransform.get_position();
				val._002Ector(val.x, val.y + Mathf.Sin(Time.get_time()) * m_undeadData.floatingCoef * 0.3f, val.z);
			}
			else
			{
				position.y = m_undeadData.floatingHeight;
				Vector3 val2 = position - m_cachedTransform.get_position();
				Vector3 normalized = val2.get_normalized();
				val = m_cachedTransform.get_position() + normalized * m_moveSpeed * Time.get_deltaTime();
				val._002Ector(val.x, val.y + Mathf.Sin(Time.get_time()) * m_undeadData.floatingCoef, val.z);
			}
			m_cachedTransform.set_position(val);
			position.y = 0f;
			val.y = 0f;
			if (Vector3.Distance(position, val) > m_undeadData.attackRange)
			{
				BackState();
				break;
			}
			m_attackIntervalTimer -= Time.get_deltaTime();
			if (!(m_attackIntervalTimer > 0f))
			{
				m_attackIntervalTimer = m_undeadData.attackInterval;
				Player player = m_targetObject as Player;
				if (player == null || player.isDead)
				{
					RequestDestroy();
				}
				else
				{
					CreateBullet();
				}
			}
			break;
		}
		}
	}

	private AnimEventShot CreateBullet()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return null;
		}
		BulletData.BulletUndead dataUndead = bulletData.dataUndead;
		if (dataUndead == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.get_rotation();
		Vector3 position = m_cachedTransform.get_position();
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataUndead.closeBullet, m_attacker, m_atkInfo, position, rotation);
		if (animEventShot == null)
		{
			Log.Error("Failed to create AnimEventShot for Undead!!");
			return null;
		}
		return animEventShot;
	}

	private void LookAtTarget(Vector3 targetPos)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Vector3 forward = m_cachedTransform.get_forward();
		Vector3 position = m_cachedTransform.get_position();
		Vector3 val = targetPos - position;
		val.Normalize();
		float num = m_aimAngleSpeed * Time.get_deltaTime();
		float num2 = Vector3.Dot(forward, val);
		float num3 = Mathf.Acos(num2);
		if (num2 < 1f && num < num3 && num3 >= 0.00174532924f)
		{
			float num4 = num / num3;
			Quaternion rotation;
			if (num4 >= 1f)
			{
				rotation = Quaternion.LookRotation(val);
			}
			else
			{
				Quaternion val2 = Quaternion.LookRotation(forward);
				Quaternion val3 = Quaternion.LookRotation(val);
				rotation = Quaternion.Slerp(val2, val3, num4);
			}
			m_cachedTransform.set_rotation(rotation);
		}
	}

	public void RequestDestroy()
	{
		if (m_func == Function.DELETE || m_isDeleted)
		{
			return;
		}
		RequestFunction(Function.DELETE);
		if (m_effectAnimator == null)
		{
			Destroy();
			return;
		}
		m_effectDeleteAnimHash = Animator.StringToHash("END");
		if (m_effectAnimator.HasState(0, m_effectDeleteAnimHash))
		{
			m_effectAnimator.Play(m_effectDeleteAnimHash, 0, 0f);
			m_effectAnimator.Update(0f);
		}
		else
		{
			Debug.LogWarning((object)"Not found delete animation!!");
			Destroy();
		}
	}

	private void FuncDelete()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		switch (m_state)
		{
		case State.TRACKING:
		{
			if (m_effectAnimator == null)
			{
				ForwardState();
				break;
			}
			AnimatorStateInfo currentAnimatorStateInfo = m_effectAnimator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
			{
				ForwardState();
			}
			break;
		}
		case State.ATTACK:
			Destroy();
			ForwardState();
			break;
		}
	}

	private void Destroy()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		if (m_isDeleted)
		{
			return;
		}
		m_isDeleted = true;
		if (!string.IsNullOrEmpty(m_landHitEffectName))
		{
			Transform effect = EffectManager.GetEffect(m_landHitEffectName);
			if (effect != null)
			{
				effect.set_position(m_cachedTransform.get_position());
				effect.set_rotation(m_cachedTransform.get_rotation());
			}
		}
		Object.Destroy(this.get_gameObject());
	}

	private void OnDestroy()
	{
		if (m_effectObj != null)
		{
			EffectManager.ReleaseEffect(m_effectObj);
			m_effectObj = null;
		}
	}

	private void RequestFunction(Function func)
	{
		m_func = func;
		SetState(State.TRACKING);
	}

	private void SetState(State state)
	{
		m_state = state;
	}

	private void ForwardState()
	{
		m_state++;
	}

	private void BackState()
	{
		if (m_state > State.NONE)
		{
			m_state--;
		}
	}
}
