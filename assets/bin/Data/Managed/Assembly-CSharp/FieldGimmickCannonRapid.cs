using UnityEngine;

public class FieldGimmickCannonRapid : FieldGimmickCannonBase
{
	private readonly Vector3 OFFSET_LEFT = new Vector3(-0.4f, 0f, 0f);

	private readonly Vector3 OFFSET_RIGHT = new Vector3(0.4f, 0f, 0f);

	private readonly Vector3 OFFSET_ZERO = Vector3.get_zero();

	private Vector3[] offsetArray;

	private int shotSeId;

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(pointData);
		m_coolTime = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.coolTimeForRapid;
		m_baseTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot");
		m_cannonTrans = modelTrans.Find("CMN_cannon01_Origin/Move/Root/base/rot/cannon_rot");
		offsetArray = (Vector3[])new Vector3[3]
		{
			OFFSET_ZERO,
			OFFSET_RIGHT,
			OFFSET_LEFT
		};
		shotSeId = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForRapid;
	}

	public override void Shot()
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
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
			initParamCannonball.offsetRot = Quaternion.get_identity();
			initParamCannonball.shotRotation = m_cannonTrans.get_rotation();
			GameObject val = new GameObject("AttackCannonball");
			AttackCannonball attackCannonball = val.AddComponent<AttackCannonball>();
			attackCannonball.Initialize(initParamCannonball);
			if (shotSeId > 0)
			{
				SoundManager.PlayOneShotSE(shotSeId, m_cannonTrans.get_position());
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
