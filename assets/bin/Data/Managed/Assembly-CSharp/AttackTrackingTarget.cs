using System.Collections;
using UnityEngine;

public class AttackTrackingTarget
{
	public enum Function
	{
		NONE,
		MAIN,
		DELETE
	}

	private const string ANIM_STATE_DISAPPEAR = "END";

	private const float Z_OFFSET_FOR_NO_TARGET = 2f;

	private BulletData.BulletTracking m_trackingData;

	private StageObject m_attacker;

	private Player m_attackerPlayer;

	private Enemy m_attackerEnemy;

	private AttackInfo m_atkInfo;

	private string[] m_atkInfoNames;

	private Transform m_cachedTransform;

	private StageObject m_target;

	private GameObject m_effectObj;

	private Animator m_effectAnimator;

	private int m_effectDeleteAnimHash;

	private Function m_func;

	private bool m_isDeleted;

	private bool m_isEmitting;

	private bool m_isShooting;

	private float m_aliveTimer;

	private float m_attackIntervalTimer;

	private float m_emitInterval;

	private int m_emissionNum;

	private float m_moveSpeed;

	private float m_moveThreshold;

	private bool m_isTargetDead;

	private Vector3 m_targetingPoint = Vector3.get_zero();

	private AtkAttribute[] m_exAtkList;

	private Player.ATTACK_MODE m_attackMode;

	public SkillInfo.SkillParam SkillParamForBullet
	{
		get;
		private set;
	}

	public AtkAttribute PlayerAttackAttribute
	{
		get;
		private set;
	}

	public bool IsReplaceSkill
	{
		get;
		private set;
	}

	public AttackTrackingTarget()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	public void Initialize(StageObject attacker, StageObject target, AttackInfo atkInfo)
	{
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		if (attacker == null)
		{
			RequestDestroy();
		}
		else
		{
			m_atkInfo = atkInfo;
			m_attacker = attacker;
			m_target = target;
			if (m_attacker is Player)
			{
				Player player = m_attacker as Player;
				if (player != null)
				{
					m_attackerPlayer = player;
					m_attackMode = player.attackMode;
				}
			}
			if (m_attacker is Enemy)
			{
				Enemy enemy = m_attacker as Enemy;
				if (enemy != null)
				{
					m_attackerEnemy = enemy;
				}
			}
			BulletData bulletData = null;
			if (atkInfo != null)
			{
				bulletData = atkInfo.bulletData;
			}
			else
			{
				Player player2 = attacker as Player;
				if (player2 != null)
				{
					AtkAttribute atk = new AtkAttribute();
					player2.GetAtk(null, ref atk);
					PlayerAttackAttribute = atk;
					SkillInfo.SkillParam actSkillParam = player2.skillInfo.actSkillParam;
					if (actSkillParam != null)
					{
						bulletData = actSkillParam.bullet;
						m_atkInfoNames = actSkillParam.tableData.attackInfoNames;
						SkillParamForBullet = actSkillParam;
					}
					if (player2.targetingPoint != null)
					{
						m_targetingPoint = player2.targetingPoint.param.targetPos;
					}
				}
			}
			if (bulletData == null)
			{
				RequestDestroy();
			}
			else
			{
				m_aliveTimer = bulletData.data.appearTime;
				BulletData.BulletTracking dataTracking = bulletData.dataTracking;
				if (dataTracking == null)
				{
					RequestDestroy();
				}
				else
				{
					m_trackingData = dataTracking;
					m_isDeleted = false;
					m_isEmitting = false;
					m_moveThreshold = dataTracking.moveThreshold;
					m_attackIntervalTimer = dataTracking.attackInterval;
					m_emitInterval = dataTracking.emitInterval;
					m_emissionNum = dataTracking.emissionNum;
					if (m_emissionNum > 0)
					{
						m_exAtkList = new AtkAttribute[m_emissionNum];
						m_moveSpeed = bulletData.data.speed;
						m_cachedTransform = this.get_transform();
						m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
						Vector3 position = Vector3.get_zero();
						position = ((m_target != null) ? m_target._position : ((!(m_targetingPoint != Vector3.get_zero())) ? (m_attacker._position + m_attacker._forward * 2f) : m_targetingPoint));
						position.y = 0f;
						m_cachedTransform.set_position(position);
						m_cachedTransform.set_rotation(Quaternion.get_identity());
						Transform effect = EffectManager.GetEffect(bulletData.data.effectName, this.get_transform());
						effect.set_localPosition(bulletData.data.dispOffset);
						effect.set_localRotation(Quaternion.Euler(bulletData.data.dispRotation));
						effect.set_localScale(Vector3.get_one());
						m_effectObj = effect.get_gameObject();
						m_effectAnimator = m_effectObj.GetComponent<Animator>();
						RequestMain();
					}
					else
					{
						Log.Error("BulletTracking.emissionNum is zero!!");
						RequestDestroy();
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
		if (m_effectObj != null)
		{
			EffectManager.ReleaseEffect(m_effectObj, true, false);
			m_effectObj = null;
		}
	}

	private void RequestFunction(Function func)
	{
		m_func = func;
	}

	private void RequestMain()
	{
		RequestFunction(Function.MAIN);
	}

	private void FuncMain()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		if (!m_isDeleted)
		{
			m_aliveTimer -= Time.get_deltaTime();
			if (m_aliveTimer <= 0f)
			{
				RequestDestroy();
			}
			else
			{
				Vector3 val = Vector3.get_zero();
				if (m_target != null)
				{
					Character character = m_target as Character;
					if (character != null && character.isDead)
					{
						m_isTargetDead = true;
					}
					val = m_target.get_transform().get_position();
					if (m_isTargetDead)
					{
						val = m_cachedTransform.get_position();
					}
				}
				else if (m_targetingPoint != Vector3.get_zero())
				{
					val = m_targetingPoint;
				}
				val.y = 0f;
				Vector3 position = m_cachedTransform.get_position();
				Vector3 val2 = val - position;
				val2.Normalize();
				if (val2 != Vector3.get_zero())
				{
					Quaternion rotation = Quaternion.LookRotation(val2);
					m_cachedTransform.set_rotation(rotation);
				}
				if (m_target != null || m_targetingPoint != Vector3.get_zero())
				{
					Vector3 forward = m_cachedTransform.get_forward();
					Vector3 val3 = m_cachedTransform.get_position() + forward * (m_moveSpeed * Time.get_deltaTime());
					if (Vector3.Distance(val, val3) >= m_moveThreshold)
					{
						m_cachedTransform.set_position(val3);
					}
				}
				if (m_isEmitting)
				{
					if (!m_isShooting)
					{
						this.StartCoroutine(ShotBullets());
						m_isShooting = true;
					}
				}
				else
				{
					m_attackIntervalTimer -= Time.get_deltaTime();
					if (m_attackIntervalTimer <= 0f)
					{
						m_attackIntervalTimer = m_trackingData.attackInterval;
						m_isEmitting = true;
					}
				}
			}
		}
	}

	private void RequestDestroy()
	{
		if (m_func != Function.DELETE && !m_isDeleted)
		{
			RequestFunction(Function.DELETE);
			if (m_effectAnimator == null)
			{
				Destroy();
			}
			else
			{
				m_effectDeleteAnimHash = Animator.StringToHash("END");
				if (m_effectAnimator.HasState(0, m_effectDeleteAnimHash))
				{
					m_effectAnimator.Play(m_effectDeleteAnimHash, 0, 0f);
					m_effectAnimator.Update(0f);
				}
				else
				{
					Destroy();
				}
			}
		}
	}

	private void FuncDelete()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (m_effectAnimator == null)
		{
			Destroy();
		}
		AnimatorStateInfo currentAnimatorStateInfo = m_effectAnimator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.get_normalizedTime() >= 1f)
		{
			Destroy();
		}
	}

	private void Destroy()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (!m_isDeleted)
		{
			m_isDeleted = true;
			if (m_attacker != null)
			{
				if (m_attackerPlayer != null)
				{
					m_attackerPlayer.TrackingTargetBullet = null;
				}
				m_attacker = null;
			}
			Object.Destroy(this.get_gameObject());
		}
	}

	private IEnumerator ShotBullets()
	{
		for (int i = 0; i < m_emissionNum; i++)
		{
			CreateBullet(i);
			yield return (object)new WaitForSeconds(m_emitInterval);
		}
		m_isEmitting = false;
		m_isShooting = false;
	}

	private AnimEventShot CreateBullet(int index)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		if (m_trackingData == null)
		{
			return null;
		}
		if (m_attacker == null)
		{
			return null;
		}
		if (m_atkInfoNames == null || m_atkInfoNames.Length <= 0)
		{
			return null;
		}
		if (m_atkInfoNames.Length < index)
		{
			return null;
		}
		AttackInfo attackInfo = m_attacker.FindAttackInfo(m_atkInfoNames[index], true, false);
		if (attackInfo == null)
		{
			return null;
		}
		Quaternion rotation = m_cachedTransform.get_rotation();
		Vector3 position = m_cachedTransform.get_position();
		if (m_attackerPlayer != null)
		{
			IsReplaceSkill = true;
		}
		AtkAttribute atk = m_exAtkList[index];
		if (atk == null)
		{
			atk = new AtkAttribute();
			m_attacker.GetAtk(attackInfo as AttackHitInfo, ref atk);
			m_exAtkList[index] = atk;
		}
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(m_trackingData.emissionBullet, m_attacker, attackInfo, position, rotation, atk, m_attackMode, null);
		if (animEventShot == null)
		{
			Log.Error("Failed to create AnimEventShot for tracking bullet!");
			return null;
		}
		if (m_attackerPlayer != null)
		{
			IsReplaceSkill = false;
		}
		return animEventShot;
	}
}
