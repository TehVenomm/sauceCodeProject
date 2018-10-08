using UnityEngine;

public class BulletControllerObstacleCylinder : BulletControllerBase
{
	private Collider m_collider;

	private Rigidbody m_rigidbody;

	private BoxCollider[] m_colliderList;

	private BulletData.BulletObstacleCylinder m_bulletObstacleCylinder;

	private int colliderNum = 1;

	private float lifeTime;

	private float lifeTimeCounter;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		m_collider = this.GetComponent<Collider>();
		if (m_collider != null)
		{
			m_collider.set_enabled(false);
		}
		m_bulletObstacleCylinder = bullet.dataObstacleCylinder;
		if (m_bulletObstacleCylinder != null)
		{
			m_rigidbody = this.GetComponent<Rigidbody>();
			if (m_rigidbody != null)
			{
				m_rigidbody.set_useGravity(false);
				m_rigidbody.set_isKinematic(true);
			}
			float num = (float)(360 / m_bulletObstacleCylinder.colliderNum);
			float num2 = num * 0.0174532924f;
			float radius = m_bulletObstacleCylinder.radius;
			for (int i = 0; i < m_bulletObstacleCylinder.colliderNum; i++)
			{
				GameObject val = new GameObject("Collider");
				val.get_transform().set_parent(this.get_transform());
				float num3 = radius * Mathf.Sin(num2 * (float)(i + 1));
				float num4 = radius * Mathf.Cos(num2 * (float)(i + 1));
				val.get_transform().set_localPosition(new Vector3(num3, 0f, num4));
				val.get_transform().set_localRotation(Quaternion.Euler(0f, num * (float)(i + 1), 0f));
				BoxCollider val2 = val.AddComponent<BoxCollider>();
				val2.set_size(m_bulletObstacleCylinder.size);
				val2.set_center(m_bulletObstacleCylinder.center);
			}
			Utility.SetLayerWithChildren(this.get_transform(), 18);
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
