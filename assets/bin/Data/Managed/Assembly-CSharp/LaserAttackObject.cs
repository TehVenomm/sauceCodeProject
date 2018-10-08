using UnityEngine;

public class LaserAttackObject : AttackColliderObject
{
	private GameObject m_effectLaser;

	public Animator m_effectAnimator;

	public CapsuleCollider m_capCollider;

	public void CreateEffect(BulletData.BulletBase bulletBase)
	{
		Transform effect = EffectManager.GetEffect(bulletBase.effectName, null);
		if ((Object)effect == (Object)null)
		{
			Log.Error("Failed to create effect for LaserAttackObject!!");
		}
		else
		{
			effect.parent = base.transform;
			effect.localPosition = bulletBase.dispOffset;
			effect.localRotation = Quaternion.Euler(bulletBase.dispRotation);
			m_effectLaser = effect.gameObject;
			if ((bool)m_effectLaser.GetComponent<Animator>())
			{
				m_effectAnimator = m_effectLaser.GetComponent<Animator>();
			}
			if ((bool)GetComponent<CapsuleCollider>())
			{
				m_capCollider = GetComponent<CapsuleCollider>();
			}
		}
	}

	public override void Destroy()
	{
		if ((Object)m_effectLaser != (Object)null)
		{
			EffectManager.ReleaseEffect(m_effectLaser, true, false);
			m_effectLaser = null;
		}
		base.Destroy();
	}
}
