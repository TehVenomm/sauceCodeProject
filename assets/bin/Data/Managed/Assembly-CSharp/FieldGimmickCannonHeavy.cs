using UnityEngine;

public class FieldGimmickCannonHeavy : FieldGimmickCannonBase
{
	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForHeavy;
		m_baseTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
		m_cannonTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
	}

	public override void Shot()
	{
		if (IsReadyForShot())
		{
			if (base._animator != null)
			{
				base._animator.Play("Reaction", 0, 0f);
			}
			AttackInfo attackHitInfo = GetAttackHitInfo();
			if (attackHitInfo != null)
			{
				AttackCannonball.InitParamCannonball initParamCannonball = new AttackCannonball.InitParamCannonball();
				initParamCannonball.attacker = m_owner;
				initParamCannonball.atkInfo = attackHitInfo;
				initParamCannonball.launchTrans = m_cannonTrans;
				initParamCannonball.offsetPos = Vector3.zero;
				initParamCannonball.offsetRot = Quaternion.identity;
				initParamCannonball.shotRotation = m_cannonTrans.rotation;
				new GameObject("HeavyCannonball").AddComponent<AttackCannonball>().Initialize(initParamCannonball);
				StartCoolTime();
				SetState(STATE.COOLTIME);
			}
		}
	}

	protected override AttackInfo GetAttackHitInfo()
	{
		if (m_owner == null)
		{
			return null;
		}
		AttackInfo attackInfo = m_owner.GetAttackInfos().Find((AttackInfo info) => info.name == "cannonball_heavy");
		if (attackInfo == null)
		{
			return null;
		}
		return attackInfo;
	}
}
