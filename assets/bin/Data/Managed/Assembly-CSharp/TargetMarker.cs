using System.Collections.Generic;
using UnityEngine;

public class TargetMarker
{
	public enum EFFECT_TYPE
	{
		NONE = -1,
		NORMAL,
		WEAK,
		DOWN,
		WEAK_SIGN,
		DOWN_SIGN,
		AIM,
		AIM_CHARGE_MAX,
		WEAK_SP_ONE_HAND_SWORD,
		WEAK_SP_TWO_HAND_SWORD,
		WEAK_SP_SPEAR,
		WEAK_SP_PAIR_SWORDS,
		WEAK_SP_ARROW,
		WEAK_SP_ONE_HAND_SWORD_SIGN,
		WEAK_SP_TWO_HAND_SWORD_SIGN,
		WEAK_SP_SPEAR_SIGN,
		WEAK_SP_PAIR_SWORDS_SIGN,
		WEAK_SP_ARROW_SIGN,
		WEAK_ELEMENT_ATTACK,
		WEAK_ELEMENT_SKILL_ATTACK,
		WEAK_SKILL_ATTACK,
		WEAK_HEAL_ATTACK,
		AIM_HEAT,
		AIM_HEAT_CHARGE_MAX,
		WEAK_CANNON,
		AIM_SOUL,
		WEAK_ELEMENT_SP_ATTACK
	}

	public class UpdateParam
	{
		public TargetPoint targetPoint;

		public bool targeting;

		public bool isLock;

		public Enemy.WEAK_STATE weakState;

		public int weakSubParam = -1;

		public bool playSign;

		public SP_ATTACK_TYPE spAttackType;

		public bool isAimArrow;

		public bool isAimMode;

		public bool isAimChargeMax;

		public float markerScale = 1f;

		public int validElementType = -1;

		public bool isMultiLockMax;
	}

	public const string CANNON_MARKER_CRITICAL = "ef_btl_target_cannon_02";

	public const string CANNON_MARKER_GRAB = "ef_btl_target_cannon_03";

	protected Transform transform;

	protected Transform effectTransform;

	private EFFECT_TYPE effectType;

	protected Transform multiLockTransform;

	private MultiLockMarker multiLock;

	public TargetPoint point
	{
		get;
		protected set;
	}

	public TargetMarker(Transform transform)
	{
		point = null;
		SetParentTransform(transform);
	}

	public void SetParentTransform(Transform _transform)
	{
		transform = _transform;
		if (effectTransform != null)
		{
			Utility.Attach(transform, effectTransform);
		}
		if (multiLockTransform != null)
		{
			Utility.Attach(transform, multiLockTransform);
		}
	}

	public void UnableMarker()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		effectType = EFFECT_TYPE.NONE;
		if (effectTransform != null)
		{
			EffectManager.ReleaseEffect(effectTransform.get_gameObject(), true, false);
		}
		if (multiLockTransform != null)
		{
			EffectManager.ReleaseEffect(multiLockTransform.get_gameObject(), true, false);
		}
		effectTransform = null;
		multiLockTransform = null;
		multiLock = null;
	}

	public bool UpdateMarker(UpdateParam param)
	{
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		if (param == null)
		{
			return false;
		}
		bool result = false;
		EFFECT_TYPE eFFECT_TYPE = EFFECT_TYPE.NONE;
		switch (param.weakState)
		{
		case Enemy.WEAK_STATE.WEAK:
			eFFECT_TYPE = EFFECT_TYPE.WEAK;
			break;
		case Enemy.WEAK_STATE.DOWN:
			eFFECT_TYPE = EFFECT_TYPE.DOWN;
			break;
		case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
		case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
		case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
			eFFECT_TYPE = (EFFECT_TYPE)((!param.isAimMode || !param.targeting || param.weakSubParam == 5) ? ((param.weakState != Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK) ? (7 + param.weakSubParam - 1) : 25) : 0);
			break;
		case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
			eFFECT_TYPE = EFFECT_TYPE.WEAK_ELEMENT_ATTACK;
			break;
		case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
			eFFECT_TYPE = EFFECT_TYPE.WEAK_ELEMENT_SKILL_ATTACK;
			break;
		case Enemy.WEAK_STATE.WEAK_SKILL_ATTACK:
			eFFECT_TYPE = EFFECT_TYPE.WEAK_SKILL_ATTACK;
			break;
		case Enemy.WEAK_STATE.WEAK_HEAL_ATTACK:
			eFFECT_TYPE = EFFECT_TYPE.WEAK_HEAL_ATTACK;
			break;
		case Enemy.WEAK_STATE.WEAK_CANNON:
			eFFECT_TYPE = EFFECT_TYPE.WEAK_CANNON;
			break;
		default:
			if (param.targeting)
			{
				eFFECT_TYPE = EFFECT_TYPE.NORMAL;
			}
			break;
		}
		if (param.isAimMode && (eFFECT_TYPE == EFFECT_TYPE.NONE || eFFECT_TYPE == EFFECT_TYPE.NORMAL))
		{
			switch (param.spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
				eFFECT_TYPE = ((!param.isAimChargeMax) ? EFFECT_TYPE.AIM_HEAT : EFFECT_TYPE.AIM_HEAT_CHARGE_MAX);
				break;
			default:
				eFFECT_TYPE = ((!param.isAimChargeMax) ? EFFECT_TYPE.AIM : EFFECT_TYPE.AIM_CHARGE_MAX);
				break;
			case SP_ATTACK_TYPE.SOUL:
				break;
			}
		}
		bool flag = false;
		if (param.isAimArrow && param.spAttackType == SP_ATTACK_TYPE.SOUL)
		{
			switch (eFFECT_TYPE)
			{
			case EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK:
				if (param.weakSubParam == 5)
				{
					break;
				}
				goto default;
			default:
				eFFECT_TYPE = EFFECT_TYPE.NONE;
				break;
			case EFFECT_TYPE.WEAK:
			case EFFECT_TYPE.WEAK_SP_ARROW:
				break;
			}
			flag = true;
		}
		if (effectType != eFFECT_TYPE || point != param.targetPoint || !param.targetPoint.param.isShowRange)
		{
			if (effectTransform != null)
			{
				EffectManager.ReleaseEffect(effectTransform.get_gameObject(), true, false);
			}
			effectTransform = null;
		}
		if (effectTransform == null && eFFECT_TYPE != EFFECT_TYPE.NONE && param.targetPoint.param.isShowRange)
		{
			string text = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int)eFFECT_TYPE];
			switch (eFFECT_TYPE)
			{
			case EFFECT_TYPE.WEAK_ELEMENT_ATTACK:
			case EFFECT_TYPE.WEAK_ELEMENT_SKILL_ATTACK:
				text = ((param.validElementType >= 0) ? (text + param.validElementType.ToString()) : string.Empty);
				break;
			case EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK:
				text = string.Format(text, param.weakSubParam - 1, param.validElementType);
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				effectTransform = EffectManager.GetEffect(text, transform);
			}
			result = true;
		}
		if (param.playSign && param.targetPoint.param.isShowRange)
		{
			EFFECT_TYPE eFFECT_TYPE2 = EFFECT_TYPE.NONE;
			switch (param.weakState)
			{
			case Enemy.WEAK_STATE.WEAK:
				eFFECT_TYPE2 = EFFECT_TYPE.WEAK_SIGN;
				break;
			case Enemy.WEAK_STATE.DOWN:
				eFFECT_TYPE2 = EFFECT_TYPE.DOWN_SIGN;
				break;
			case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
			case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
				eFFECT_TYPE2 = (EFFECT_TYPE)(12 + param.weakSubParam - 1);
				break;
			}
			if (eFFECT_TYPE2 != EFFECT_TYPE.NONE)
			{
				string text2 = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int)eFFECT_TYPE2];
				if (!string.IsNullOrEmpty(text2))
				{
					Transform effect = EffectManager.GetEffect(text2, transform);
					if (effect != null)
					{
						effect.Set(param.targetPoint.param.markerPos, param.targetPoint.param.markerRot);
					}
					result = true;
				}
			}
		}
		if (flag && param.targetPoint.param.isShowRange)
		{
			if (!param.isMultiLockMax && multiLockTransform == null)
			{
				string text3 = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[24];
				if (!text3.IsNullOrWhiteSpace())
				{
					multiLockTransform = EffectManager.GetEffect(text3, transform);
					multiLock = multiLockTransform.GetComponentInChildren<MultiLockMarker>();
					if (multiLock != null)
					{
						multiLock.Init();
					}
					result = true;
				}
			}
		}
		else
		{
			if (multiLockTransform != null)
			{
				EffectManager.ReleaseEffect(multiLockTransform.get_gameObject(), true, false);
			}
			multiLockTransform = null;
			multiLock = null;
		}
		effectType = eFFECT_TYPE;
		point = param.targetPoint;
		if (effectTransform != null)
		{
			effectTransform.Set(point.param.markerPos, point.param.markerRot);
			effectTransform.set_localScale(Vector3.get_one() * param.markerScale);
		}
		if (multiLockTransform != null)
		{
			multiLockTransform.Set(point.param.markerPos, point.param.markerRot);
			multiLockTransform.set_localScale(Vector3.get_one() * param.markerScale);
		}
		return result;
	}

	public void UpdateByTargetPoint(TargetPoint targetPoint, string effectName)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (effectTransform == null)
		{
			effectTransform = EffectManager.GetEffect(effectName, transform);
		}
		Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
		Vector3 targetPoint2 = targetPoint.GetTargetPoint();
		Vector3 val = cameraTransform.get_position() - targetPoint2;
		Vector3 pos = val.get_normalized() * targetPoint.scaledMarkerZShift + targetPoint2;
		Quaternion rotation = cameraTransform.get_rotation();
		effectTransform.Set(pos, rotation);
	}

	public MultiLockMarker GetMultiLock()
	{
		return multiLock;
	}

	public void HideMultiLock()
	{
		if (!(multiLock == null))
		{
			multiLock.Hide();
		}
	}

	public void ResetMultiLock()
	{
		if (!(multiLock == null))
		{
			multiLock.Reset();
		}
	}

	public void EndMultiLockBoost(bool isHide)
	{
		if (!(multiLock == null))
		{
			multiLock.EndBoost(isHide);
		}
	}

	public List<int> GetMultiLockOrder()
	{
		if (multiLock == null)
		{
			return null;
		}
		return multiLock.lockOrder;
	}

	public int GetMultiLockNum()
	{
		if (multiLock == null)
		{
			return 0;
		}
		return multiLock.lockOrder.Count;
	}
}
