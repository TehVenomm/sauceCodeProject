using UnityEngine;

public class BulletControllerObstacleCylinder : BulletControllerBase
{
	private Collider m_collider;

	private Rigidbody m_rigidbody;

	private BoxCollider[] m_colliderList;

	private BulletData.BulletObstacleCylinder m_bulletObstacleCylinder;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		m_collider = GetComponent<Collider>();
		if ((Object)m_collider != (Object)null)
		{
			m_collider.enabled = false;
		}
		m_bulletObstacleCylinder = bullet.dataObstacleCylinder;
		if (m_bulletObstacleCylinder != null)
		{
			m_rigidbody = GetComponent<Rigidbody>();
			if ((Object)m_rigidbody != (Object)null)
			{
				m_rigidbody.useGravity = false;
				m_rigidbody.isKinematic = true;
			}
			float num = (float)(360 / m_bulletObstacleCylinder.colliderNum);
			float num2 = num * 0.0174532924f;
			float radius = m_bulletObstacleCylinder.radius;
			for (int i = 0; i < m_bulletObstacleCylinder.colliderNum; i++)
			{
				GameObject gameObject = new GameObject("Collider");
				gameObject.transform.parent = base.transform;
				float x = radius * Mathf.Sin(num2 * (float)(i + 1));
				float z = radius * Mathf.Cos(num2 * (float)(i + 1));
				gameObject.transform.localPosition = new Vector3(x, 0f, z);
				gameObject.transform.localRotation = Quaternion.Euler(0f, num * (float)(i + 1), 0f);
				BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
				boxCollider.size = m_bulletObstacleCylinder.size;
				boxCollider.center = m_bulletObstacleCylinder.center;
			}
			Utility.SetLayerWithChildren(base.transform, 18);
		}
	}

	public override void Update()
	{
		base.Update();
		if (m_bulletObstacleCylinder == null)
		{
			return;
		}
	}

	public override void OnShot()
	{
		Utility.SetLayerWithChildren(base._transform, 18);
	}
}
