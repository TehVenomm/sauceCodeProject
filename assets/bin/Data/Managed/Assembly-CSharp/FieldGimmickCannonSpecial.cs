using System.Collections;
using UnityEngine;

public class FieldGimmickCannonSpecial : FieldGimmickCannonBase
{
	private static readonly string NAME_ATTACKINFO = "cannonball_special";

	private static readonly string NAME_NODE_SHOT_EFFECT = "Effect";

	private static readonly Vector3 OFFSET_BEAM_CHARGE_EFFECT = new Vector3(0f, 0f, 11f);

	public static readonly string NAME_EFFECT_CHARGE = "ef_btl_magibullet_cannon_01_01";

	private Transform m_launchTrans;

	private Transform m_effectChargeTrans;

	private float m_delayChangeCamera;

	private float m_durationChangeCamera;

	private int m_seIdShot;

	private int m_seIdCharge;

	private int m_seIdChargeMax;

	private int m_seIdOnBoard;

	private bool isPlayedChargeMaxSE;

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		m_launchTrans = modelTrans.Find(NAME_NODE_SHOT_EFFECT);
		m_delayChangeCamera = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.delayChangeCameraForSpecial;
		m_seIdShot = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecial;
		m_seIdCharge = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialCharge;
		m_seIdChargeMax = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialChargeMax;
		m_seIdOnBoard = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialOnBoard;
	}

	public override void OnBoard(Player player)
	{
		base.OnBoard(player);
		if (m_seIdOnBoard > 0)
		{
			SoundManager.PlayOneShotSE(m_seIdOnBoard, base._transform.position);
		}
		m_owner.SetCannonChargeMax(MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.chargeTimeMaxForSpecial);
	}

	public override void OnLeave()
	{
		base.OnLeave();
		ReleaseCharge();
	}

	public override void Shot()
	{
		if (IsReadyForShot() && (!(m_owner is Self) || m_owner.IsCannonFullCharged()))
		{
			AttackInfo attackHitInfo = GetAttackHitInfo();
			if (attackHitInfo != null)
			{
				AttackCannonBeam.InitParamCannonBeam initParamCannonBeam = new AttackCannonBeam.InitParamCannonBeam();
				initParamCannonBeam.attacker = m_owner;
				initParamCannonBeam.atkInfo = attackHitInfo;
				initParamCannonBeam.launchTrans = m_launchTrans;
				GameObject gameObject = new GameObject("AttackCannonBeam");
				AttackCannonBeam attackCannonBeam = gameObject.AddComponent<AttackCannonBeam>();
				attackCannonBeam.Initialize(initParamCannonBeam);
				if ((Object)attackHitInfo.bulletData != (Object)null && attackHitInfo.bulletData.data != null)
				{
					m_durationChangeCamera = attackHitInfo.bulletData.data.appearTime;
				}
				if (m_seIdShot > 0)
				{
					SoundManager.PlayOneShotSE(m_seIdShot, m_launchTrans.position);
				}
				StartCoroutine(DelayCameraChange());
				StartCoolTime();
				SetState(STATE.COOLTIME);
			}
		}
	}

	private IEnumerator DelayCameraChange()
	{
		if (m_owner is Self)
		{
			yield return (object)new WaitForSeconds(m_delayChangeCamera);
			if (!((Object)m_owner == (Object)null) && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM);
				yield return (object)new WaitForSeconds(m_durationChangeCamera);
				if (!((Object)m_owner == (Object)null))
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM_CHARGE);
				}
			}
		}
	}

	protected override AttackInfo GetAttackHitInfo()
	{
		if ((Object)m_owner == (Object)null)
		{
			return null;
		}
		AttackInfo attackInfo = m_owner.GetAttackInfos().Find((AttackInfo info) => info.name == NAME_ATTACKINFO);
		if (attackInfo == null)
		{
			return null;
		}
		return attackInfo;
	}

	protected override void UpdateStateStandBy()
	{
		SetState(STATE.READY);
	}

	protected override void UpdateStateReady()
	{
		if ((Object)m_owner != (Object)null && m_seIdChargeMax > 0 && m_owner.IsCannonFullCharged() && !isPlayedChargeMaxSE)
		{
			SoundManager.PlayOneShotSE(m_seIdChargeMax, base._transform.position);
			isPlayedChargeMaxSE = true;
		}
		if (IsRemainCoolTime())
		{
			SetState(STATE.COOLTIME);
		}
	}

	public override void ApplyCannonVector(Vector3 cannonVec)
	{
	}

	public void StartCharge()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			m_effectChargeTrans = EffectManager.GetEffect(NAME_EFFECT_CHARGE, m_launchTrans);
			m_effectChargeTrans.position += OFFSET_BEAM_CHARGE_EFFECT;
		}
		if (m_seIdCharge > 0)
		{
			SoundManager.PlayOneShotSE(m_seIdCharge, base._transform.position);
		}
	}

	public void ReleaseCharge()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && (Object)m_effectChargeTrans != (Object)null)
		{
			EffectManager.ReleaseEffect(m_effectChargeTrans.gameObject, true, false);
		}
		if ((Object)m_owner != (Object)null)
		{
			m_owner.ClearCannonChargeRate();
		}
		isPlayedChargeMaxSE = false;
	}
}
