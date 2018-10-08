using UnityEngine;

public class AttackFloatingMine : MonoBehaviour
{
	public enum Function
	{
		NONE,
		MAIN,
		DELETE
	}

	public class InitParamFloatingMine
	{
		public StageObject attacker;

		public AttackInfo atkInfo;

		public Transform launchTrans;

		public Vector3 offsetPos = Vector3.zero;

		public Quaternion offsetRot = Quaternion.identity;
	}

	public const string ANIM_STATE_END = "END";

	private BulletData.BulletMine m_mineData;

	private StageObject m_attacker;

	private AttackInfo m_atkInfo;

	private Transform m_cachedTransform;

	private GameObject m_effectObj;

	private string m_landHitEffectName = string.Empty;

	private float m_moveSpeed;

	private float m_slowDownRate;

	private float m_aliveTimer;

	private int m_effectDeleteAnimHash;

	private Animator m_effectDeleteAnimator;

	private Function m_func;

	private int m_state;

	private bool m_isDeleted;

	private MineAttackObject m_mineAttackObj;

	private float m_unbreakableTimer;

	public void Initialize(InitParamFloatingMine initParam)
	{
		if (initParam.atkInfo != null)
		{
			m_atkInfo = initParam.atkInfo;
			AttackHitInfo attackHitInfo = m_atkInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				attackHitInfo.enableIdentityCheck = false;
			}
			BulletData bulletData = m_atkInfo.bulletData;
			if (!((Object)bulletData == (Object)null))
			{
				BulletData.BulletBase data = bulletData.data;
				if (data != null)
				{
					BulletData.BulletMine dataMine = bulletData.dataMine;
					if (dataMine != null)
					{
						m_attacker = initParam.attacker;
						m_landHitEffectName = data.landHiteffectName;
						m_aliveTimer = data.appearTime;
						m_moveSpeed = data.speed;
						m_slowDownRate = dataMine.slowDownRate;
						m_unbreakableTimer = dataMine.unbrakableTime;
						m_mineData = dataMine;
						m_isDeleted = false;
						m_cachedTransform = base.transform;
						m_cachedTransform.parent = ((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
						Transform launchTrans = initParam.launchTrans;
						m_cachedTransform.position = launchTrans.position + launchTrans.rotation * initParam.offsetPos;
						m_cachedTransform.rotation = launchTrans.rotation * initParam.offsetRot;
						m_cachedTransform.localScale = data.timeStartScale;
						Transform effect = EffectManager.GetEffect(data.effectName, base.transform);
						effect.localPosition = data.dispOffset;
						effect.localRotation = Quaternion.Euler(data.dispRotation);
						effect.localScale = Vector3.one;
						m_effectObj = effect.gameObject;
						m_effectDeleteAnimator = m_effectObj.GetComponent<Animator>();
						float radius = data.radius;
						float height = 0f;
						Vector3 hitOffset = data.hitOffset;
						int num = 0;
						if (dataMine.isIgnoreHitEnemyAttack)
						{
							num |= 0xA000;
						}
						if (dataMine.isIgnoreHitEnemyMove)
						{
							num |= 0x400;
						}
						GameObject gameObject = new GameObject("MineAttackObject");
						MineAttackObject mineAttackObject = gameObject.AddComponent<MineAttackObject>();
						mineAttackObject.Initialize(m_attacker, m_cachedTransform, m_atkInfo, hitOffset, Vector3.zero, radius, height, 31);
						mineAttackObject.SetIgnoreLayerMask(num);
						m_mineAttackObj = mineAttackObject;
						RequestMain();
					}
				}
			}
		}
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

	private void OnDestroy()
	{
		if (!((Object)m_effectObj == (Object)null))
		{
			EffectManager.ReleaseEffect(m_effectObj, true, false);
			m_effectObj = null;
		}
	}

	private void Destroy()
	{
		if (!m_isDeleted)
		{
			m_isDeleted = true;
			if (!string.IsNullOrEmpty(m_landHitEffectName))
			{
				Transform effect = EffectManager.GetEffect(m_landHitEffectName, null);
				if ((Object)effect != (Object)null)
				{
					effect.position = m_cachedTransform.position;
					effect.rotation = m_cachedTransform.rotation;
				}
			}
			m_attacker = null;
			Object.Destroy(base.gameObject);
		}
	}

	private void SetState(int state)
	{
		m_state = state;
	}

	private void ForwardState()
	{
		m_state++;
	}

	private void BackState()
	{
		if (m_state > 0)
		{
			m_state--;
		}
	}

	private void RequestFunction(Function func)
	{
		m_func = func;
		SetState(1);
	}

	private void RequestMain()
	{
		RequestFunction(Function.MAIN);
	}

	private void FuncMain()
	{
		if (!m_isDeleted)
		{
			m_aliveTimer -= Time.deltaTime;
			if (m_aliveTimer <= 0f)
			{
				RequestDestroy(true);
			}
			else
			{
				m_unbreakableTimer -= Time.deltaTime;
				if (m_mineAttackObj.isHit)
				{
					if (m_unbreakableTimer <= 0f)
					{
						bool isExplode = false;
						if (!IsAvoidExplode(m_mineAttackObj.hitLayer) && IsLayerExplode(m_mineAttackObj.hitLayer))
						{
							isExplode = true;
						}
						RequestDestroy(isExplode);
						return;
					}
					m_mineAttackObj.ResetHit();
				}
				switch (m_state)
				{
				case 1:
				{
					Vector3 forward = m_cachedTransform.forward;
					Vector3 position2 = m_cachedTransform.position + forward * (m_moveSpeed * Time.deltaTime);
					if (m_mineData != null)
					{
						position2.y = m_mineData.floatingHeight;
						if (m_mineData.floatingRate > 0f)
						{
							position2.y += Mathf.Sin(3.14159274f * Mathf.PingPong(Time.time, m_mineData.floatingRate));
						}
					}
					m_cachedTransform.position = position2;
					m_moveSpeed -= Time.deltaTime * m_slowDownRate;
					if (m_moveSpeed <= 0f)
					{
						m_moveSpeed = 0f;
						ForwardState();
					}
					break;
				}
				case 2:
				{
					Vector3 position = m_cachedTransform.position;
					if (m_mineData != null)
					{
						position.y = m_mineData.floatingHeight;
						if (m_mineData.floatingRate > 0f)
						{
							position.y += Mathf.Sin(3.14159274f * Mathf.PingPong(Time.time, m_mineData.floatingRate));
						}
					}
					m_cachedTransform.position = position;
					break;
				}
				}
			}
		}
	}

	private void RequestDestroy(bool isExplode = true)
	{
		if (m_func != Function.DELETE && !m_isDeleted)
		{
			RequestFunction(Function.DELETE);
			if (isExplode)
			{
				CreateExplosion();
				if ((Object)m_mineAttackObj != (Object)null)
				{
					m_mineAttackObj.ResetHit();
				}
			}
			if ((Object)m_effectDeleteAnimator == (Object)null)
			{
				Destroy();
			}
			else
			{
				string text = (!isExplode) ? "END" : string.Empty;
				if (string.IsNullOrEmpty(text))
				{
					Destroy();
				}
				else
				{
					m_effectDeleteAnimHash = Animator.StringToHash(text);
					if (m_effectDeleteAnimator.HasState(0, m_effectDeleteAnimHash))
					{
						m_effectDeleteAnimator.Play(m_effectDeleteAnimHash, 0, 0f);
					}
				}
			}
		}
	}

	private void FuncDelete()
	{
		switch (m_state)
		{
		case 1:
			if ((Object)m_effectDeleteAnimator == (Object)null)
			{
				ForwardState();
			}
			else if (m_effectDeleteAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				ForwardState();
			}
			break;
		case 2:
			Destroy();
			ForwardState();
			break;
		}
	}

	private AnimEventShot CreateExplosion()
	{
		BulletData bulletData = m_atkInfo.bulletData;
		if ((Object)bulletData == (Object)null)
		{
			return null;
		}
		BulletData.BulletMine dataMine = bulletData.dataMine;
		if ((Object)bulletData == (Object)null)
		{
			return null;
		}
		if ((Object)m_attacker == (Object)null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.rotation;
		Vector3 position = m_cachedTransform.position;
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataMine.explodeBullet, m_attacker, m_atkInfo, position, rotation, null, Player.ATTACK_MODE.NONE, null);
		if ((Object)animEventShot == (Object)null)
		{
			return null;
		}
		return animEventShot;
	}

	private bool IsAvoidExplode(int layer)
	{
		if ((Object)m_mineAttackObj == (Object)null)
		{
			return false;
		}
		if (layer == 8 && !((Object)m_mineAttackObj.hitPlayer == (Object)null) && m_mineAttackObj.hitPlayer.isActSpecialAction && m_mineAttackObj.hitPlayer.attackMode == Player.ATTACK_MODE.SPEAR)
		{
			return true;
		}
		return false;
	}

	private bool IsLayerExplode(int layer)
	{
		switch (layer)
		{
		case 8:
		case 9:
		case 13:
		case 15:
		case 17:
		case 18:
		case 21:
			return true;
		default:
			return false;
		}
	}
}
