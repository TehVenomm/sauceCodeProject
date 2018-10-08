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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		base.OnBoard(player);
		if (m_seIdOnBoard > 0)
		{
			SoundManager.PlayOneShotSE(m_seIdOnBoard, base._transform.get_position());
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
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		if (IsReadyForShot() && (!(m_owner is Self) || m_owner.IsCannonFullCharged()))
		{
			AttackInfo attackHitInfo = GetAttackHitInfo();
			if (attackHitInfo != null)
			{
				AttackCannonBeam.InitParamCannonBeam initParamCannonBeam = new AttackCannonBeam.InitParamCannonBeam();
				initParamCannonBeam.attacker = m_owner;
				initParamCannonBeam.atkInfo = attackHitInfo;
				initParamCannonBeam.launchTrans = m_launchTrans;
				GameObject val = new GameObject("AttackCannonBeam");
				AttackCannonBeam attackCannonBeam = val.AddComponent<AttackCannonBeam>();
				attackCannonBeam.Initialize(initParamCannonBeam);
				if (attackHitInfo.bulletData != null && attackHitInfo.bulletData.data != null)
				{
					m_durationChangeCamera = attackHitInfo.bulletData.data.appearTime;
				}
				if (m_seIdShot > 0)
				{
					SoundManager.PlayOneShotSE(m_seIdShot, m_launchTrans.get_position());
				}
				this.StartCoroutine(DelayCameraChange());
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
			if (!(m_owner == null) && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM);
				yield return (object)new WaitForSeconds(m_durationChangeCamera);
				if (!(m_owner == null))
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM_CHARGE);
				}
			}
		}
	}

	protected override AttackInfo GetAttackHitInfo()
	{
		if (m_owner == null)
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
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (m_owner != null && m_seIdChargeMax > 0 && m_owner.IsCannonFullCharged() && !isPlayedChargeMaxSE)
		{
			SoundManager.PlayOneShotSE(m_seIdChargeMax, base._transform.get_position());
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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			m_effectChargeTrans = EffectManager.GetEffect(NAME_EFFECT_CHARGE, m_launchTrans);
			Transform effectChargeTrans = m_effectChargeTrans;
			effectChargeTrans.set_position(effectChargeTrans.get_position() + OFFSET_BEAM_CHARGE_EFFECT);
		}
		if (m_seIdCharge > 0)
		{
			SoundManager.PlayOneShotSE(m_seIdCharge, base._transform.get_position());
		}
	}

	public void ReleaseCharge()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && m_effectChargeTrans != null)
		{
			EffectManager.ReleaseEffect(m_effectChargeTrans.get_gameObject(), true, false);
		}
		if (m_owner != null)
		{
			m_owner.ClearCannonChargeRate();
		}
		isPlayedChargeMaxSE = false;
	}
}
