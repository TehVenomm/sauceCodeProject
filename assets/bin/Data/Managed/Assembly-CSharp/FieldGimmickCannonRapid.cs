using UnityEngine;

public class FieldGimmickCannonRapid : FieldGimmickCannonBase
{
	private readonly Vector3 OFFSET_LEFT = new Vector3(-0.4f, 0f, 0f);

	private readonly Vector3 OFFSET_RIGHT = new Vector3(0.4f, 0f, 0f);

	private readonly Vector3 OFFSET_ZERO = Vector3.zero;

	private Vector3[] offsetArray;

	private int shotSeId;

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForRapid;
		m_baseTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
		m_cannonTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
		offsetArray = new Vector3[3]
		{
			OFFSET_ZERO,
			OFFSET_RIGHT,
			OFFSET_LEFT
		};
		shotSeId = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForRapid;
	}

	public override void Shot()
	{
		if (!IsReadyForShot())
		{
			return;
		}
		if (base._animator != null)
		{
			base._animator.Play("Reaction", 0, 0f);
		}
		AttackInfo attackHitInfo = GetAttackHitInfo();
		if (attackHitInfo != null)
		{
			int num = Random.Range(0, 3);
			AttackCannonball.InitParamCannonball initParamCannonball = new AttackCannonball.InitParamCannonball();
			initParamCannonball.attacker = m_owner;
			initParamCannonball.atkInfo = attackHitInfo;
			initParamCannonball.launchTrans = m_cannonTrans;
			initParamCannonball.offsetPos = offsetArray[num];
			initParamCannonball.offsetRot = Quaternion.identity;
			initParamCannonball.shotRotation = m_cannonTrans.rotation;
			new GameObject("AttackCannonball").AddComponent<AttackCannonball>().Initialize(initParamCannonball);
			if (shotSeId > 0)
			{
				SoundManager.PlayOneShotSE(shotSeId, m_cannonTrans.position);
			}
			StartCoolTime();
			SetState(STATE.COOLTIME);
		}
	}

	protected override AttackInfo GetAttackHitInfo()
	{
		if (m_owner == null)
		{
			return null;
		}
		AttackInfo attackInfo = m_owner.GetAttackInfos().Find((AttackInfo info) => info.name == "cannonball_rapid");
		if (attackInfo == null)
		{
			return null;
		}
		return attackInfo;
	}
}
