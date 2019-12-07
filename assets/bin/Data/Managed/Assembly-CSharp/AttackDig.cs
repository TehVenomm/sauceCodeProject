using System;
using UnityEngine;

public class AttackDig : MonoBehaviour
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

	public const string ANIM_STATE_BREAK = "BREAK";

	private const string ANIM_STATE_END = "END";

	private BulletData.BulletDig m_digData;

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

	private float m_attackTimer;

	private bool m_isCreatedBullet;

	public string AttackInfoName => m_atkInfo.name;

	public bool IsDeleted => m_isDeleted;

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
		BulletData.BulletDig dataDig = bulletData.dataDig;
		m_aimAngleSpeed = dataDig.lookAtAngle * ((float)Math.PI / 180f);
		m_digData = dataDig;
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
			m_targetObject = targetObj;
		}
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
			RequestDestroy(isPlayBreakEffect: false);
			return;
		}
		if (m_targetObject == null)
		{
			RequestDestroy(isPlayBreakEffect: false);
			return;
		}
		if (m_isCreatedBullet)
		{
			RequestDestroy(isPlayBreakEffect: false);
			return;
		}
		Vector3 position = m_targetObject.transform.position;
		position.y = 0f;
		switch (m_state)
		{
		case State.TRACKING:
		{
			LookAtTarget(position);
			Vector3 forward = m_cachedTransform.forward;
			Vector3 vector = m_cachedTransform.position + forward * m_moveSpeed * Time.deltaTime;
			vector.y = m_digData.floatingHeight;
			m_cachedTransform.position = vector;
			position.y = 0f;
			vector.y = 0f;
			if (Vector3.Distance(position, vector) <= m_digData.attackRange)
			{
				m_attackTimer = m_digData.attackDelay;
				ForwardState();
			}
			break;
		}
		case State.ATTACK:
			m_attackTimer -= Time.deltaTime;
			if (!(m_attackTimer > 0f))
			{
				Player player = m_targetObject as Player;
				if (player == null || player.isDead)
				{
					RequestDestroy(isPlayBreakEffect: false);
				}
				else
				{
					CreateBullet();
				}
			}
			break;
		}
	}

	private AnimEventShot CreateBullet()
	{
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return null;
		}
		BulletData.BulletDig dataDig = bulletData.dataDig;
		if (dataDig == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.rotation;
		Vector3 position = m_cachedTransform.position;
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataDig.flyOutBullet, m_attacker, m_atkInfo, position, rotation);
		if (animEventShot == null)
		{
			Log.Error("Failed to create AnimEventShot for Dig!!");
			return null;
		}
		m_isCreatedBullet = true;
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

	public void RequestDestroy(bool isPlayBreakEffect = true)
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
		if (isPlayBreakEffect)
		{
			m_effectDeleteAnimHash = Animator.StringToHash("BREAK");
		}
		else
		{
			m_effectDeleteAnimHash = Animator.StringToHash("END");
		}
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
