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

	public void Initialize(StageObject attacker, Transform parentTrans, AttackInfo atkInfo, int numLaser)
	{
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
			int attackLayer = (attacker is Player) ? 14 : 15;
			AttackHitInfo attackHitInfo = atkInfo as AttackHitInfo;
			if (attackHitInfo != null)
			{
				attackHitInfo.enableIdentityCheck = false;
			}
			m_atkInfoName = atkInfo.name;
			Transform transform = base.transform;
			if (m_laserData.isLinkPositionOnly)
			{
				transform.parent = (MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform);
				transform.position = parentTrans.position;
			}
			else
			{
				transform.parent = parentTrans;
				transform.localPosition = Vector3.zero;
			}
			transform.localRotation = Quaternion.identity;
			float radius = data.radius;
			float capsuleHeight = dataLaser.capsuleHeight;
			Vector3 offsetPosition = dataLaser.offsetPosition;
			float num = 360f / (float)numLaser;
			float num2 = 0f;
			for (int i = 0; i < numLaser; i++)
			{
				LaserAttackObject laserAttackObject = new GameObject("LaserAttackObject").AddComponent<LaserAttackObject>();
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
				if ((bool)m_laserAttackList[i].transform.GetChild(0).GetComponent<Animator>())
				{
					m_laserAttackList[i].transform.GetChild(0).GetComponent<Animator>().Play("END");
					continue;
				}
				m_laserAttackList[i].Destroy();
				m_laserAttackList[i] = null;
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
		Object.Destroy(base.gameObject);
	}

	public void RequestDestroy()
	{
		m_isRequestDelete = true;
	}

	public void AnimEnd()
	{
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
				if (m_laserAttackList[i] != null && m_laserAttackList[i].m_effectAnimator != null && m_laserAttackList[i].m_effectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					flag = false;
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
						m_laserAttackList[j].m_effectAnimator.Play("END");
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
		m_aliveTimer -= Time.deltaTime;
		if (m_aliveTimer <= 0f)
		{
			m_isAtkEnd = true;
		}
	}

	private void LateUpdate()
	{
		if (!IsDeleted)
		{
			BulletData.BulletLaser laserData = m_laserData;
			if (laserData.isLinkPositionOnly && m_parentTrans != null)
			{
				base.transform.position = m_parentTrans.position;
			}
			m_nowAngleSpeed += laserData.addAngleSpeed * Time.deltaTime;
			if (laserData.addAngleSpeed > 0f)
			{
				m_nowAngleSpeed = Mathf.Min(m_nowAngleSpeed, laserData.limitAngleSpeed);
			}
			else
			{
				m_nowAngleSpeed = Mathf.Max(m_nowAngleSpeed, 0f - laserData.limitAngleSpeed);
			}
			base.transform.localRotation = base.transform.localRotation * Quaternion.AngleAxis(m_nowAngleSpeed * Time.deltaTime, Vector3.up);
		}
	}
}
