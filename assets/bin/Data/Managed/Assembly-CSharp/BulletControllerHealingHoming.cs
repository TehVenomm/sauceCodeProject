using System.Collections.Generic;
using UnityEngine;

public class BulletControllerHealingHoming : BulletControllerHoming
{
	protected const float PERCENT = 0.01f;

	private static readonly int ANIM_STATE_PICKED = Animator.StringToHash("PICKED");

	protected bool m_isIgnoreColliderExceptTarget = true;

	protected bool m_isAlreadyDoneHitProcess;

	protected Animator m_effectAnimator;

	protected List<int> m_buffIdList;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
		BulletData.BulletHealingHoming dataHealingHomingBullet = bullet.dataHealingHomingBullet;
		if (dataHealingHomingBullet != null)
		{
			InitParam(dataHealingHomingBullet);
			m_isIgnoreColliderExceptTarget = dataHealingHomingBullet.isIgnoreColliderExceptTarget;
			m_isAlreadyDoneHitProcess = false;
			m_buffIdList = dataHealingHomingBullet.buffIds;
			Utility.SetLayerWithChildren(this.get_transform(), dataHealingHomingBullet.defaultGenerateLayer);
			m_effectAnimator = this.GetComponentInChildren<Animator>();
		}
	}

	public override bool IsHit(Collider collider)
	{
		StageObject targetObject = base.targetObject;
		if (m_isIgnoreColliderExceptTarget && targetObject != null && targetObject.get_name() != collider.get_name())
		{
			return false;
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		if (!m_isAlreadyDoneHitProcess)
		{
			m_isAlreadyDoneHitProcess = true;
			PlayOnHitAnimation();
			HealAction();
			Player component = collider.get_gameObject().GetComponent<Player>();
			AddBuffAction(component);
			base.OnHit(collider);
		}
	}

	protected void PlayOnHitAnimation()
	{
		if (bulletObject != null)
		{
			bulletObject.SetDisablePlayEndAnim();
		}
		if (!(m_effectAnimator == null))
		{
			m_effectAnimator.Play(ANIM_STATE_PICKED, 0, 0f);
		}
	}

	protected void HealAction()
	{
		if (base.bulletSkillInfoParam != null)
		{
			StageObject targetObject = base.targetObject;
			if (!(targetObject == null) && (targetObject.IsCoopNone() || targetObject.IsOriginal()) && targetObject is Player)
			{
				Character.HealData healData = new Character.HealData(base.bulletSkillInfoParam.healHp, base.bulletSkillInfoParam.tableData.healType, HEAL_EFFECT_TYPE.BASIS, new List<int>
				{
					10
				});
				(targetObject as Player).OnHealReceive(healData);
			}
		}
	}

	protected void AddBuffAction(Player _player)
	{
		if (m_buffIdList != null && m_buffIdList.Count >= 1 && base.bulletSkillInfoParam != null && !(_player == null) && !_player.isDead)
		{
			int i = 0;
			for (int count = m_buffIdList.Count; i < count; i++)
			{
				_player.StartBuffByBuffTableId(m_buffIdList[i], base.bulletSkillInfoParam);
			}
		}
	}
}
