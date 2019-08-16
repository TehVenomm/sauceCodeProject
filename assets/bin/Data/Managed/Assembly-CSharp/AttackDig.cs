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

	public AttackDig()
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
		BulletData.BulletDig dataDig = bulletData.dataDig;
		m_aimAngleSpeed = dataDig.lookAtAngle * ((float)Math.PI / 180f);
		m_digData = dataDig;
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
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		if (m_isDeleted)
		{
			return;
		}
		m_aliveTimer -= Time.get_deltaTime();
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
		Vector3 position = m_targetObject.get_transform().get_position();
		position.y = 0f;
		switch (m_state)
		{
		case State.TRACKING:
		{
			LookAtTarget(position);
			Vector3 forward = m_cachedTransform.get_forward();
			Vector3 val = m_cachedTransform.get_position() + forward * m_moveSpeed * Time.get_deltaTime();
			val.y = m_digData.floatingHeight;
			m_cachedTransform.set_position(val);
			position.y = 0f;
			val.y = 0f;
			if (Vector3.Distance(position, val) <= m_digData.attackRange)
			{
				m_attackTimer = m_digData.attackDelay;
				ForwardState();
			}
			break;
		}
		case State.ATTACK:
			m_attackTimer -= Time.get_deltaTime();
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
		BulletData.BulletDig dataDig = bulletData.dataDig;
		if (dataDig == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.get_rotation();
		Vector3 position = m_cachedTransform.get_position();
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
