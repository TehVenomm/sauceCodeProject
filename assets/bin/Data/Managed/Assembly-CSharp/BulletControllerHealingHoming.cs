using System.Collections.Generic;
using UnityEngine;

public class BulletControllerHealingHoming : BulletControllerHoming
{
	protected const float PERCENT = 0.01f;

	private static readonly int ANIM_STATE_PICKED = Animator.StringToHash("PICKED");

	protected bool m_isIgnoreColliderExceptTarget = true;

	protected bool m_isAlreadyDoneHitProcess;

	protected Animator m_effectAnimator;

	private List<int> m_buffIdList;

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
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
		StageObject target = GetTarget();
		if (m_isIgnoreColliderExceptTarget && target != null && target.get_name() != collider.get_name())
		{
			return false;
		}
		return true;
	}

	public override void OnHit(Collider collider)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
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
			StageObject target = GetTarget();
			if (!(target == null) && (target.IsCoopNone() || target.IsOriginal()) && target is Player)
			{
				(target as Player).OnHealReceive(base.bulletSkillInfoParam.healHp, base.bulletSkillInfoParam.tableData.healType, HEAL_EFFECT_TYPE.BASIS, true);
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
				int num = m_buffIdList[i];
				if (Singleton<BuffTable>.IsValid() && num > 0)
				{
					BuffTable.BuffData data = Singleton<BuffTable>.I.GetData((uint)num);
					if (data != null)
					{
						BuffParam.BuffData buffData = new BuffParam.BuffData();
						buffData.type = data.type;
						buffData.interval = data.interval;
						buffData.valueType = data.valueType;
						buffData.time = data.duration;
						float num2 = (float)data.value;
						GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, base.bulletSkillInfoParam.baseInfo.level);
						if (growSkillItemData != null)
						{
							buffData.time = data.duration * (float)(int)growSkillItemData.supprtTime[0].rate * 0.01f + (float)growSkillItemData.supprtTime[0].add;
							num2 = (float)(data.value * (int)growSkillItemData.supprtValue[0].rate) * 0.01f + (float)(int)growSkillItemData.supprtValue[0].add;
						}
						if (buffData.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(buffData.type))
						{
							num2 = (float)_player.hpMax * num2 * 0.01f;
						}
						buffData.value = Mathf.FloorToInt(num2);
						_player.OnBuffStart(buffData);
					}
				}
			}
		}
	}
}
