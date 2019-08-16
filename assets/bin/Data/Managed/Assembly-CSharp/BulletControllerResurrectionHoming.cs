using UnityEngine;

public class BulletControllerResurrectionHoming : BulletControllerHealingHoming
{
	protected bool m_isAddBuffActionOnResurrected = true;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		BulletData.BulletResurrectionHoming dataResurrectionHomingBullet = bullet.dataResurrectionHomingBullet;
		if (dataResurrectionHomingBullet != null)
		{
			InitParam(dataResurrectionHomingBullet);
			m_isIgnoreColliderExceptTarget = dataResurrectionHomingBullet.isIgnoreColliderExceptTarget;
			m_isAlreadyDoneHitProcess = false;
			m_buffIdList = dataResurrectionHomingBullet.buffIds;
			m_isAddBuffActionOnResurrected = dataResurrectionHomingBullet.isAddBuffActionOnResurrected;
			Utility.SetLayerWithChildren(this.get_transform(), dataResurrectionHomingBullet.defaultGenerateLayer);
			m_effectAnimator = this.GetComponentInChildren<Animator>();
		}
	}

	public override void OnHit(Collider collider)
	{
		if (!m_isAlreadyDoneHitProcess)
		{
			m_isAlreadyDoneHitProcess = true;
			PlayOnHitAnimation();
			Player component = collider.get_gameObject().GetComponent<Player>();
			if (component != null && component.isDead)
			{
				component.OnResurrectionReceive();
				AddBuffActionOnActDeadStandUp(component);
			}
			else
			{
				HealAction();
				AddBuffAction(component);
			}
		}
	}

	protected void AddBuffActionOnActDeadStandUp(Player _player)
	{
		if (m_buffIdList != null && m_buffIdList.Count >= 1 && base.bulletSkillInfoParam != null)
		{
			int i = 0;
			for (int count = m_buffIdList.Count; i < count; i++)
			{
				_player.StartBuffByBuffTableIdOnActDeadStandUp(m_buffIdList[i], base.bulletSkillInfoParam);
			}
		}
	}
}
