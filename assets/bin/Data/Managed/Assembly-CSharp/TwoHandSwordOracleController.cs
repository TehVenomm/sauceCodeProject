using System;

public class TwoHandSwordOracleController : IWeaponController
{
	public const int TWOHANDSWORD_ORACLE_BASE_ATTACK_ID = 60;

	public const int TWOHANDSWORD_ORACLE_BASE_ATTACK_COMBO_02_ID = 61;

	public const int TWOHANDSWORD_ORACLE_BASE_ATTACK_COMBO_03_ID = 62;

	public const int TWOHANDSWORD_ORACLE_SP_ATTACK_ID = 63;

	public const int TWOHANDSWORD_ORACLE_AVOID_ATTACK_ID = 64;

	private Player owner;

	private InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleActionInfo;

	private AnimEventData.EventData playingVernierEffectEventData;

	private bool isHorizontalAttack;

	private bool isHorizontalMax;

	private bool isHorizontalFollow;

	private int countHorizontal;

	private bool isHitHorizontalAttack;

	private bool isTapHorizontalAttack;

	public bool IsHorizontalAttack => isHorizontalAttack;

	public TwoHandSwordOracleController()
	{
		owner = null;
	}

	public void Init(Player _player)
	{
		owner = _player;
		oracleActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.oracleTHSInfo;
	}

	public void OnLoadComplete()
	{
	}

	public void Update()
	{
	}

	public void OnEndAction()
	{
		EndVernierEffect();
		EndHorizontal();
	}

	public void OnActDead()
	{
	}

	public void OnActReaction()
	{
	}

	public void OnActAvoid()
	{
	}

	public void OnActSkillAction()
	{
	}

	public void OnRelease()
	{
	}

	public void OnActAttack(int id)
	{
	}

	public void OnBuffStart(BuffParam.BuffData data)
	{
	}

	public void OnBuffEnd(BuffParam.BUFFTYPE type)
	{
	}

	public void OnChangeWeapon()
	{
	}

	public void OnAttackedHitFix(AttackedHitStatusFix status)
	{
	}

	public static bool IsOracleLayerAttackId(int attackId)
	{
		return 60 <= attackId && attackId <= 64;
	}

	public bool IsEnableChangeActionByLongTap()
	{
		if (owner == null)
		{
			return false;
		}
		Self self = owner as Self;
		if (self != null)
		{
			if (self.controllerInputCombo && self.actionID == Character.ACTION_ID.ATTACK)
			{
				int attackID = self.attackID;
				if (attackID == 61)
				{
					return self.InputAttackCombo();
				}
			}
			if (self.IsActionFromAvoid() && self.enableSpAttackContinue)
			{
				int num = 64;
				string motionLayerName = owner.GetMotionLayerName(owner.attackMode, owner.spAttackType, num);
				Player player = owner;
				int id = num;
				string motionLayerName2 = motionLayerName;
				player.ActAttack(id, send_packet: true, sync_immediately: false, motionLayerName2, string.Empty);
				return true;
			}
		}
		return false;
	}

	public void StartVernierEffect(bool isMax)
	{
		string text = null;
		if (isMax)
		{
			int currentWeaponElement = owner.GetCurrentWeaponElement();
			if (currentWeaponElement < oracleActionInfo.maxVernierEffectNames.Length)
			{
				text = oracleActionInfo.maxVernierEffectNames[currentWeaponElement];
			}
		}
		else
		{
			text = oracleActionInfo.normalVernierEffectName;
		}
		if (playingVernierEffectEventData != null)
		{
			if (!(playingVernierEffectEventData.stringArgs[0] != text))
			{
				return;
			}
			EndVernierEffect();
		}
		if (text != null)
		{
			AnimEventData.EventData eventData = new AnimEventData.EventData();
			eventData.id = AnimEventFormat.ID.EFFECT;
			eventData.stringArgs = new string[2]
			{
				text,
				GameDefine.PLAYER_WEAPON_PARENT_NODE_RIGHT
			};
			eventData.intArgs = new int[3]
			{
				0,
				0,
				oracleActionInfo.isPlayOtherPlayerVernierEffect ? 1 : 0
			};
			eventData.floatArgs = new float[7]
			{
				1f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f
			};
			owner.OnAnimEvent(eventData);
			playingVernierEffectEventData = eventData;
			AnimEventData.EventData eventData2 = new AnimEventData.EventData();
			eventData2.id = AnimEventFormat.ID.SE_ONESHOT;
			eventData2.stringArgs = new string[1]
			{
				string.Empty
			};
			eventData2.intArgs = new int[1]
			{
				(!isMax) ? oracleActionInfo.normalVernierSeId : oracleActionInfo.maxVernierSeId
			};
			owner.OnAnimEvent(eventData2);
		}
	}

	public void EndVernierEffect()
	{
		if (playingVernierEffectEventData != null)
		{
			AnimEventData.EventData eventData = new AnimEventData.EventData();
			eventData.id = AnimEventFormat.ID.EFFECT_DELETE;
			eventData.stringArgs = playingVernierEffectEventData.stringArgs;
			owner.OnAnimEvent(eventData);
			playingVernierEffectEventData = null;
		}
	}

	public void StartHorizontal(bool isMax)
	{
		isHorizontalAttack = true;
		isHorizontalMax = isMax;
		isHorizontalFollow = false;
		countHorizontal = 0;
		isHitHorizontalAttack = false;
		isTapHorizontalAttack = false;
		owner.UpdateAnimatorSpeed();
	}

	public void HitHorizontal()
	{
		isHitHorizontalAttack = true;
	}

	public void CheckNextHorizontal()
	{
		if (isHorizontalAttack)
		{
			isTapHorizontalAttack = true;
		}
	}

	public bool NextHorizontal()
	{
		bool flag = isHorizontalAttack;
		bool flag2 = false;
		Self self = owner as Self;
		if (flag && self != null && (!isTapHorizontalAttack || (oracleActionInfo.needHitHorizontal && !isHitHorizontalAttack)))
		{
			flag = false;
		}
		if (flag)
		{
			if (EnableHorizontalNext())
			{
				countHorizontal++;
				isHitHorizontalAttack = false;
				isTapHorizontalAttack = false;
				owner.UpdateAnimatorSpeed();
			}
			else if (owner.IsCoopNone() || owner.IsOriginal())
			{
				flag = false;
				flag2 = true;
			}
		}
		if (flag2 || !flag)
		{
			SetHorizontalNextMotion(flag2);
		}
		return flag && self != null;
	}

	private bool EnableHorizontalNext()
	{
		if (isHorizontalMax)
		{
			if (oracleActionInfo.horizontalMaxSpinInfoList == null)
			{
				return false;
			}
			if (countHorizontal >= oracleActionInfo.horizontalMaxSpinInfoList.Length)
			{
				return false;
			}
		}
		else
		{
			if (oracleActionInfo.horizontalSpinInfoList == null)
			{
				return false;
			}
			if (countHorizontal >= oracleActionInfo.horizontalSpinInfoList.Length)
			{
				return false;
			}
		}
		return true;
	}

	public void SetHorizontalNextMotion(bool isFinish)
	{
		if (isHorizontalAttack && !isHorizontalFollow)
		{
			isHorizontalFollow = true;
			owner.SetNextTrigger(isFinish ? 1 : 0);
			owner.UpdateAnimatorSpeed();
			if (owner.playerSender != null)
			{
				owner.playerSender.OnSetHorizontalNextMotion(isFinish);
			}
		}
	}

	public void EndHorizontal()
	{
		isHorizontalAttack = false;
		isHitHorizontalAttack = false;
	}

	public float GetHorizontalSpeed()
	{
		if (isHorizontalAttack && !isHorizontalFollow)
		{
			if (countHorizontal <= 0)
			{
				if (isHorizontalMax)
				{
					return oracleActionInfo.horizontalMaxFirstSpeed * owner.buffParam.GetOracleThsHorizontalSpeedRate();
				}
				return 1f * owner.buffParam.GetOracleThsHorizontalSpeedRate();
			}
			int num = countHorizontal - 1;
			InGameSettingsManager.Player.OracleTwoHandSwordActionInfo.HorizontalSpinInfo[] array = (!isHorizontalMax) ? oracleActionInfo.horizontalSpinInfoList : oracleActionInfo.horizontalMaxSpinInfoList;
			if (num < array.Length)
			{
				return array[num].spinSpeedRate * owner.buffParam.GetOracleThsHorizontalSpeedRate();
			}
		}
		return 1f;
	}

	public bool GetHorizontalDamageUpRate(string attackInfoName, ref float value)
	{
		value = 1f;
		if (!isHorizontalAttack)
		{
			return false;
		}
		if (oracleActionInfo.horizontalDamageUpAttackInfoNames == null)
		{
			return false;
		}
		if (!Array.Exists(oracleActionInfo.horizontalDamageUpAttackInfoNames, (string s) => s == attackInfoName))
		{
			return false;
		}
		if (countHorizontal <= 0)
		{
			return false;
		}
		int num = countHorizontal - 1;
		InGameSettingsManager.Player.OracleTwoHandSwordActionInfo.HorizontalSpinInfo[] array = (!isHorizontalMax) ? oracleActionInfo.horizontalSpinInfoList : oracleActionInfo.horizontalMaxSpinInfoList;
		if (num < array.Length)
		{
			value = array[num].damageRate;
			return true;
		}
		return false;
	}

	public bool GetChargeNormalDamageUpRate(AttackHitInfo.ATTACK_TYPE attackType, ref float value)
	{
		value = 1f;
		switch (attackType)
		{
		case AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE:
			value = oracleActionInfo.chargeBaseDmgRate;
			return true;
		case AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE_MAX:
			value = oracleActionInfo.maxChargeBaseDmgRate;
			return true;
		default:
			return false;
		}
	}

	public bool GetChargeElementDamageUpRate(AttackHitInfo.ATTACK_TYPE attackType, ref float value)
	{
		value = 1f;
		switch (attackType)
		{
		case AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE:
			value = oracleActionInfo.chargeElementDmgRate;
			return true;
		case AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE_MAX:
			value = oracleActionInfo.maxChargeElementDmgRate;
			return true;
		default:
			return false;
		}
	}

	public bool GetConcussionEnemyElementDamageUpRate(Enemy enemy, ref float value)
	{
		value = 1f;
		if (enemy.IsConcussion())
		{
			value = oracleActionInfo.concussionEnemyElementDmgRate;
			return true;
		}
		return false;
	}
}
