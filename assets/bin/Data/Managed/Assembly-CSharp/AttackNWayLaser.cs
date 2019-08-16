using System.Collections.Generic;
using UnityEngine;

public class AttackNWayLaser : MonoBehaviour
{
	private List<LaserAttackObject> m_laserAttackList = new List<LaserAttackObject>();

	private BulletData.BulletLaser m_laserData;

	private StageObject m_attacker;

	private string m_atkInfoName = string.Empty;

	private Transform m_parentTrans;

	private float m_nowAngleSpeed;

	private float m_aliveTimer;

	private bool m_isDelete;

	private bool m_isRequestDelete;

	private bool m_isAnimEnd;

	private bool m_isAtkEnd;

	public string AttackInfoName => m_atkInfoName;

	public bool IsDeleted => m_isDelete;

	public bool IsAnimEnd
	{
		get
		{
			return m_isAnimEnd;
		}
		set
		{
			m_isAnimEnd = value;
		}
	}

	public AttackNWayLaser()
		: this()
	{
	}

	public void Initialize(StageObject attacker, Transform parentTrans, AttackInfo atkInfo, int numLaser)
	{
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = atkInfo.bulletData;
		if (bulletData == null)
		{
			return;
		}
		BulletData.BulletBase data = bulletData.data;
		BulletData.BulletLaser dataLaser = bulletData.dataLaser;
		if (dataLaser != null && data != null)
		{
			m_attacker = attacker;
			m_aliveTimer = data.appearTime;
			m_nowAngleSpeed = dataLaser.initAngleSpeed;
			m_laserData = dataLaser;
			m_parentTrans = parentTrans;
			int attackLayer = (!(attacker is Player)) ? 15 : 14;
			AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				attackHitInfo.enableIdentityCheck = false;
			}
			m_atkInfoName = atkInfo.name;
			Transform transform = this.get_transform();
			if (m_laserData.isLinkPositionOnly)
			{
				transform.set_parent((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
				transform.set_position(parentTrans.get_position());
			}
			else
			{
				transform.set_parent(parentTrans);
				transform.set_localPosition(Vector3.get_zero());
			}
			transform.set_localRotation(Quaternion.get_identity());
			float radius = data.radius;
			float capsuleHeight = dataLaser.capsuleHeight;
			Vector3 offsetPosition = dataLaser.offsetPosition;
			float num = 360f / (float)numLaser;
			float num2 = 0f;
			for (int i = 0; i < numLaser; i++)
			{
				GameObject val = new GameObject("LaserAttackObject");
				LaserAttackObject laserAttackObject = val.AddComponent<LaserAttackObject>();
				laserAttackObject.Initialize(attacker, transform, atkInfo, offsetPosition, new Vector3(0f, num2, 0f), radius, capsuleHeight, attackLayer);
				laserAttackObject.CreateEffect(data);
				m_laserAttackList.Add(laserAttackObject);
				num2 += num;
			}
		}
	}

	public void Destroy()
	{
		if (IsDeleted)
		{
			return;
		}
		int count = m_laserAttackList.Count;
		for (int i = 0; i < count; i++)
		{
			if (m_laserAttackList[i] != null)
			{
				if (Object.op_Implicit(m_laserAttackList[i].get_transform().GetChild(0).GetComponent<Animator>()))
				{
					Animator component = m_laserAttackList[i].get_transform().GetChild(0).GetComponent<Animator>();
					component.Play("END");
				}
				else
				{
					m_laserAttackList[i].Destroy();
					m_laserAttackList[i] = null;
				}
			}
		}
		m_laserAttackList.Clear();
		if (m_attacker != null)
		{
			Enemy enemy = m_attacker as Enemy;
			if (enemy != null)
			{
				enemy.OnDestroyLaser(this);
			}
			Player player = m_attacker as Player;
			if (player != null)
			{
				player.OnDestroyLaser(this);
			}
		}
		m_isDelete = true;
		Object.Destroy(this.get_gameObject());
	}

	public void RequestDestroy()
	{
		m_isRequestDelete = true;
	}

	public void AnimEnd()
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		if (IsDeleted || !m_isAtkEnd)
		{
			return;
		}
		bool flag = true;
		int count = m_laserAttackList.Count;
		if (IsAnimEnd)
		{
			for (int i = 0; i < count; i++)
			{
				if (m_laserAttackList[i] != null && m_laserAttackList[i].m_effectAnimator != null)
				{
					Animator effectAnimator = m_laserAttackList[i].m_effectAnimator;
					AnimatorStateInfo currentAnimatorStateInfo = effectAnimator.GetCurrentAnimatorStateInfo(0);
					if (currentAnimatorStateInfo.get_normalizedTime() < 1f)
					{
						flag = false;
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				if (m_laserAttackList[j] != null)
				{
					if (m_laserAttackList[j].m_capCollider != null)
					{
						Object.Destroy(m_laserAttackList[j].m_capCollider);
						m_laserAttackList[j].m_capCollider = null;
					}
					if (m_laserAttackList[j].m_effectAnimator != null)
					{
						Animator effectAnimator2 = m_laserAttackList[j].m_effectAnimator;
						effectAnimator2.Play("END");
						flag = false;
					}
				}
			}
			IsAnimEnd = true;
		}
		if (flag)
		{
			Destroy();
		}
	}

	private void Update()
	{
		if (IsDeleted)
		{
			return;
		}
		if (m_isAtkEnd)
		{
			AnimEnd();
			return;
		}
		if (m_isRequestDelete)
		{
			m_aliveTimer = 0f;
		}
		m_aliveTimer -= Time.get_deltaTime();
		if (m_aliveTimer <= 0f)
		{
			m_isAtkEnd = true;
		}
	}

	private void LateUpdate()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDeleted)
		{
			BulletData.BulletLaser laserData = m_laserData;
			if (laserData.isLinkPositionOnly && m_parentTrans != null)
			{
				this.get_transform().set_position(m_parentTrans.get_position());
			}
			m_nowAngleSpeed += laserData.addAngleSpeed * Time.get_deltaTime();
			if (laserData.addAngleSpeed > 0f)
			{
				m_nowAngleSpeed = Mathf.Min(m_nowAngleSpeed, laserData.limitAngleSpeed);
			}
			else
			{
				m_nowAngleSpeed = Mathf.Max(m_nowAngleSpeed, 0f - laserData.limitAngleSpeed);
			}
			this.get_transform().set_localRotation(this.get_transform().get_localRotation() * Quaternion.AngleAxis(m_nowAngleSpeed * Time.get_deltaTime(), Vector3.get_up()));
		}
	}
}
