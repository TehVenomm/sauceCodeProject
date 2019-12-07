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

	public void Initialize(StageObject attacker, AttackInfo atkInfo, StageObject targetObj, Transform launchTrans, Vector3 offsetPos, Quaternion offsetRot)
	{
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
		m_cachedTransform = base.transform;
		m_cachedTransform.parent = (MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform);
		m_cachedTransform.position = launchTrans.position + launchTrans.rotation * offsetPos;
		m_cachedTransform.rotation = launchTrans.rotation * offsetRot;
		m_cachedTransform.localScale = bulletData.data.timeStartScale;
		Transform effect = EffectManager.GetEffect(bulletData.data.effectName, base.transform);
		if (effect != null)
		{
			effect.localPosition = bulletData.data.dispOffset;
			effect.localRotation = Quaternion.Euler(bulletData.data.dispRotation);
			effect.localScale = Vector3.one;
			m_effectObj = effect.gameObject;
			m_effectAnimator = m_effectObj.GetComponent<Animator>();
		}
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
		if (m_isDeleted)
		{
			return;
		}
		m_aliveTimer -= Time.deltaTime;
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
		Vector3 position = m_targetObject.transform.position;
		switch (m_state)
		{
		case State.TRACKING:
		{
			position.y = m_undeadData.floatingHeight;
			Vector3 normalized2 = (position - m_cachedTransform.position).normalized;
			Vector3 vector2 = m_cachedTransform.position + normalized2 * m_moveSpeed * Time.deltaTime;
			vector2 = new Vector3(vector2.x, vector2.y + Mathf.Sin(Time.time) * m_undeadData.floatingCoef, vector2.z);
			m_cachedTransform.position = vector2;
			position.y = 0f;
			vector2.y = 0f;
			if (Vector3.Distance(position, vector2) <= m_undeadData.attackRange)
			{
				m_attackIntervalTimer = m_undeadData.attackInterval;
				ForwardState();
			}
			break;
		}
		case State.ATTACK:
		{
			Vector3 vector = m_cachedTransform.position;
			position.y = 0f;
			vector.y = 0f;
			if (Vector3.Distance(position, vector) < 0.05f)
			{
				vector = m_cachedTransform.position;
				vector = new Vector3(vector.x, vector.y + Mathf.Sin(Time.time) * m_undeadData.floatingCoef * 0.3f, vector.z);
			}
			else
			{
				position.y = m_undeadData.floatingHeight;
				Vector3 normalized = (position - m_cachedTransform.position).normalized;
				vector = m_cachedTransform.position + normalized * m_moveSpeed * Time.deltaTime;
				vector = new Vector3(vector.x, vector.y + Mathf.Sin(Time.time) * m_undeadData.floatingCoef, vector.z);
			}
			m_cachedTransform.position = vector;
			position.y = 0f;
			vector.y = 0f;
			if (Vector3.Distance(position, vector) > m_undeadData.attackRange)
			{
				BackState();
				break;
			}
			m_attackIntervalTimer -= Time.deltaTime;
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
		Quaternion rotation = m_cachedTransform.rotation;
		Vector3 position = m_cachedTransform.position;
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
		Vector3 forward = m_cachedTransform.forward;
		Vector3 position = m_cachedTransform.position;
		Vector3 vector = targetPos - position;
		vector.Normalize();
		float num = m_aimAngleSpeed * Time.deltaTime;
		float num2 = Vector3.Dot(forward, vector);
		float num3 = Mathf.Acos(num2);
		if (num2 < 1f && num < num3 && num3 >= 0.00174532924f)
		{
			float num4 = num / num3;
			Quaternion rotation;
			if (num4 >= 1f)
			{
				rotation = Quaternion.LookRotation(vector);
			}
			else
			{
				Quaternion a = Quaternion.LookRotation(forward);
				Quaternion b = Quaternion.LookRotation(vector);
				rotation = Quaternion.Slerp(a, b, num4);
			}
			m_cachedTransform.rotation = rotation;
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
			Debug.LogWarning("Not found delete animation!!");
			Destroy();
		}
	}

	private void FuncDelete()
	{
		switch (m_state)
		{
		case State.TRACKING:
			if (m_effectAnimator == null)
			{
				ForwardState();
			}
			else if (m_effectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				ForwardState();
			}
			break;
		case State.ATTACK:
			Destroy();
			ForwardState();
			break;
		}
	}

	private void Destroy()
	{
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
				effect.position = m_cachedTransform.position;
				effect.rotation = m_cachedTransform.rotation;
			}
		}
		UnityEngine.Object.Destroy(base.gameObject);
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
