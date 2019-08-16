using System.Collections;
using UnityEngine;

public class AttackTrackingTarget : MonoBehaviour
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

	private bool m_isPerfectTrack;

	private bool m_isTargetDead;

	private Vector3 m_targetingPoint = Vector3.get_zero();

	private AtkAttribute[] m_exAtkList;

	private Player.ATTACK_MODE m_attackMode;

	private bool m_isTracking;

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
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		if (attacker == null)
		{
			RequestDestroy();
			return;
		}
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
			return;
		}
		m_aliveTimer = bulletData.data.appearTime;
		BulletData.BulletTracking dataTracking = bulletData.dataTracking;
		if (dataTracking == null)
		{
			RequestDestroy();
			return;
		}
		m_trackingData = dataTracking;
		m_isDeleted = false;
		m_isEmitting = false;
		m_moveThreshold = dataTracking.moveThreshold;
		m_attackIntervalTimer = dataTracking.attackInterval;
		m_emitInterval = dataTracking.emitInterval;
		m_emissionNum = dataTracking.emissionNum;
		m_isPerfectTrack = dataTracking.isPerfectTrack;
		if (m_emissionNum > 0)
		{
			m_exAtkList = new AtkAttribute[m_emissionNum];
			m_moveSpeed = bulletData.data.speed;
			m_cachedTransform = this.get_transform();
			m_cachedTransform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
			Vector3 position = Vector3.get_zero();
			if (m_attackerPlayer != null && m_attackerPlayer.IsValidBuffBlind())
			{
				m_target = null;
				m_targetingPoint = Vector3.get_zero();
			}
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
			m_isTracking = true;
			RequestMain();
		}
		else
		{
			Log.Error("BulletTracking.emissionNum is zero!!");
			RequestDestroy();
		}
	}

	public void TrackOff()
	{
		m_isTracking = false;
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
			EffectManager.ReleaseEffect(m_effectObj);
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
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
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
		if ((m_target != null || m_targetingPoint != Vector3.get_zero()) && m_isTracking)
		{
			if (m_isPerfectTrack)
			{
				m_cachedTransform.set_position(val);
			}
			else
			{
				Vector3 forward = m_cachedTransform.get_forward();
				Vector3 val3 = m_cachedTransform.get_position() + forward * (m_moveSpeed * Time.get_deltaTime());
				if (Vector3.Distance(val, val3) >= m_moveThreshold)
				{
					m_cachedTransform.set_position(val3);
				}
			}
		}
		if (m_isEmitting)
		{
			if (!m_isShooting)
			{
				this.StartCoroutine(ShotBullets());
				m_isShooting = true;
			}
			return;
		}
		m_attackIntervalTimer -= Time.get_deltaTime();
		if (m_attackIntervalTimer <= 0f)
		{
			m_attackIntervalTimer = m_trackingData.attackInterval;
			m_isEmitting = true;
		}
	}

	private void RequestDestroy()
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
			Destroy();
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
		if (m_isDeleted)
		{
			return;
		}
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
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
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
		AttackInfo attackInfo = m_attacker.FindAttackInfo(m_atkInfoNames[index]);
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
		AnimEventShot animEventShot = AnimEventShot.CreateByExternalBulletData(m_trackingData.emissionBullet, m_attacker, attackInfo, position, rotation, atk, m_attackMode);
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
