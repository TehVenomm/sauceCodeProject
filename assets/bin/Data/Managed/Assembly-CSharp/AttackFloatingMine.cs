using System;
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

		public Vector3 offsetPos = Vector3.get_zero();

		public Quaternion offsetRot = Quaternion.get_identity();
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

	public AttackFloatingMine()
		: this()
	{
	}

	public void Initialize(InitParamFloatingMine initParam)
	{
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		if (initParam.atkInfo == null)
		{
			return;
		}
		m_atkInfo = initParam.atkInfo;
		AttackHitInfo attackHitInfo = m_atkInfo as AttackHitInfo;
		if (attackHitInfo != null)
		{
			attackHitInfo.enableIdentityCheck = false;
		}
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletBase data = bulletData.data;
		if (data == null)
		{
			return;
		}
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
			m_cachedTransform = this.get_transform();
			m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
			Transform launchTrans = initParam.launchTrans;
			m_cachedTransform.set_position(launchTrans.get_position() + launchTrans.get_rotation() * initParam.offsetPos);
			m_cachedTransform.set_rotation(launchTrans.get_rotation() * initParam.offsetRot);
			m_cachedTransform.set_localScale(data.timeStartScale);
			Transform effect = EffectManager.GetEffect(data.effectName, this.get_transform());
			effect.set_localPosition(data.dispOffset);
			effect.set_localRotation(Quaternion.Euler(data.dispRotation));
			effect.set_localScale(Vector3.get_one());
			m_effectObj = effect.get_gameObject();
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
			GameObject val = new GameObject("MineAttackObject");
			MineAttackObject mineAttackObject = val.AddComponent<MineAttackObject>();
			mineAttackObject.Initialize(m_attacker, m_cachedTransform, m_atkInfo, hitOffset, Vector3.get_zero(), radius, height, 31);
			mineAttackObject.SetIgnoreLayerMask(num);
			m_mineAttackObj = mineAttackObject;
			RequestMain();
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
		if (!(m_effectObj == null))
		{
			EffectManager.ReleaseEffect(m_effectObj);
			m_effectObj = null;
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
		m_attacker = null;
		Object.Destroy(this.get_gameObject());
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
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
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
		m_unbreakableTimer -= Time.get_deltaTime();
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
			Vector3 forward = m_cachedTransform.get_forward();
			Vector3 position2 = m_cachedTransform.get_position() + forward * (m_moveSpeed * Time.get_deltaTime());
			if (m_mineData != null)
			{
				position2.y = m_mineData.floatingHeight;
				if (m_mineData.floatingRate > 0f)
				{
					position2.y += Mathf.Sin((float)Math.PI * Mathf.PingPong(Time.get_time(), m_mineData.floatingRate));
				}
			}
			m_cachedTransform.set_position(position2);
			m_moveSpeed -= Time.get_deltaTime() * m_slowDownRate;
			if (m_moveSpeed <= 0f)
			{
				m_moveSpeed = 0f;
				ForwardState();
			}
			break;
		}
		case 2:
		{
			Vector3 position = m_cachedTransform.get_position();
			if (m_mineData != null)
			{
				position.y = m_mineData.floatingHeight;
				if (m_mineData.floatingRate > 0f)
				{
					position.y += Mathf.Sin((float)Math.PI * Mathf.PingPong(Time.get_time(), m_mineData.floatingRate));
				}
			}
			m_cachedTransform.set_position(position);
			break;
		}
		}
	}

	private void RequestDestroy(bool isExplode = true)
	{
		if (m_func == Function.DELETE || m_isDeleted)
		{
			return;
		}
		RequestFunction(Function.DELETE);
		if (isExplode)
		{
			CreateExplosion();
			if (m_mineAttackObj != null)
			{
				m_mineAttackObj.ResetHit();
			}
		}
		if (m_effectDeleteAnimator == null)
		{
			Destroy();
			return;
		}
		string text = (!isExplode) ? "END" : string.Empty;
		if (string.IsNullOrEmpty(text))
		{
			Destroy();
			return;
		}
		m_effectDeleteAnimHash = Animator.StringToHash(text);
		if (m_effectDeleteAnimator.HasState(0, m_effectDeleteAnimHash))
		{
			m_effectDeleteAnimator.Play(m_effectDeleteAnimHash, 0, 0f);
		}
	}

	private void FuncDelete()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		switch (m_state)
		{
		case 1:
		{
			if (m_effectDeleteAnimator == null)
			{
				ForwardState();
				break;
			}
			AnimatorStateInfo currentAnimatorStateInfo = m_effectDeleteAnimator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
			{
				ForwardState();
			}
			break;
		}
		case 2:
			Destroy();
			ForwardState();
			break;
		}
	}

	private AnimEventShot CreateExplosion()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = m_atkInfo.bulletData;
		if (bulletData == null)
		{
			return null;
		}
		BulletData.BulletMine dataMine = bulletData.dataMine;
		if (bulletData == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.get_rotation();
		Vector3 position = m_cachedTransform.get_position();
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(dataMine.explodeBullet, m_attacker, m_atkInfo, position, rotation);
		if (animEventShot == null)
		{
			return null;
		}
		return animEventShot;
	}

	private bool IsAvoidExplode(int layer)
	{
		if (m_mineAttackObj == null)
		{
			return false;
		}
		if (layer == 8 && !(m_mineAttackObj.hitPlayer == null) && m_mineAttackObj.hitPlayer.isActSpecialAction && m_mineAttackObj.hitPlayer.attackMode == Player.ATTACK_MODE.SPEAR)
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
