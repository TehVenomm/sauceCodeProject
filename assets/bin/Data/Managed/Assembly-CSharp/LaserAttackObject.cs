using UnityEngine;

public class LaserAttackObject : AttackColliderObject
{
	private GameObject m_effectLaser;

	public Animator m_effectAnimator;

	public CapsuleCollider m_capCollider;

	public void CreateEffect(BulletData.BulletBase bulletBase)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = EffectManager.GetEffect(bulletBase.effectName);
		if (effect == null)
		{
			Log.Error("Failed to create effect for LaserAttackObject!!");
			return;
		}
		effect.set_parent(this.get_transform());
		effect.set_localPosition(bulletBase.dispOffset);
		effect.set_localRotation(Quaternion.Euler(bulletBase.dispRotation));
		m_effectLaser = effect.get_gameObject();
		if (Object.op_Implicit(m_effectLaser.GetComponent<Animator>()))
		{
			m_effectAnimator = m_effectLaser.GetComponent<Animator>();
		}
		if (Object.op_Implicit(this.GetComponent<CapsuleCollider>()))
		{
			m_capCollider = this.GetComponent<CapsuleCollider>();
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
