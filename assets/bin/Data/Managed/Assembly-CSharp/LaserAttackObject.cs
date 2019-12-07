using UnityEngine;

public class LaserAttackObject : AttackColliderObject
{
	private GameObject m_effectLaser;

	public Animator m_effectAnimator;

	public CapsuleCollider m_capCollider;

	public void CreateEffect(BulletData.BulletBase bulletBase)
	{
		Transform effect = EffectManager.GetEffect(bulletBase.effectName);
		if (effect == null)
		{
			Log.Error("Failed to create effect for LaserAttackObject!!");
			return;
		}
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

	public override void Destroy()
	{
		if (m_effectLaser != null)
		{
			EffectManager.ReleaseEffect(m_effectLaser);
			m_effectLaser = null;
		}
		base.Destroy();
	}
}
