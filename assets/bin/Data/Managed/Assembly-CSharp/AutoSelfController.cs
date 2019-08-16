using System.Collections;
using UnityEngine;

public class AutoSelfController : SelfController
{
	protected IEnumerator mainCoroutine;

	protected bool isStart;

	protected float startWaitTime;

	public TargetPoint actionTargetPoint;

	private bool isWaitingSpecial;

	private bool isActSpecialOneSwordSoul;

	public AutoBrain autoBrain
	{
		get;
		private set;
	}

	public InGameSettingsManager.NpcController npcParameter
	{
		get;
		private set;
	}

	private bool isAttack => base.self != null && base.self.actionID == Character.ACTION_ID.ATTACK;

	private bool isGuard => base.self != null && base.self.actionID == (Character.ACTION_ID)19;

	private bool isMove => base.self != null && base.self.actionID == Character.ACTION_ID.MOVE;

	private bool isChangeableAttack => base.self != null && base.self.IsChangeableAction(Character.ACTION_ID.ATTACK);

	private bool isChangeableSpecialAction => base.self != null && base.self.IsChangeableAction((Character.ACTION_ID)33);

	private StageObject target => (!(base.brain != null)) ? null : base.brain.targetCtrl.GetCurrentTarget();

	protected override void Awake()
	{
		base.Awake();
		autoBrain = AttachBrain<AutoBrain>();
	}

	protected override void Start()
	{
		base.Start();
		npcParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.npcController;
		isStart = true;
		if (IsEnableControll())
		{
			OnChangeEnableControll(enable: true);
		}
	}

	private bool IsTouchedInAutoMode()
	{
		InputManager.TouchInfo stickInfo = MonoBehaviourSingleton<InputManager>.I.GetStickInfo();
		return touchInfo != null || stickInfo != null;
	}

	protected override void Update()
	{
		if (IsTouchedInAutoMode())
		{
			base.Update();
			return;
		}
		if (base.self.actionID != (Character.ACTION_ID)27)
		{
			bool flag = false;
			if (base.self.isGuardWalk || base.self.actionID == (Character.ACTION_ID)19 || base.self.actionID == (Character.ACTION_ID)20)
			{
				flag = true;
			}
			if (!flag && base.nextCommand != null)
			{
				if (CheckCommand(base.nextCommand))
				{
					ActCommand(base.nextCommand);
					base.nextCommand = null;
					return;
				}
				if (base.nextCommand.deltaTime >= base.parameter.inputCommandValidTime[(int)base.nextCommand.type])
				{
					base.nextCommand = null;
				}
			}
		}
		if (IsEnableControll())
		{
			OnDead();
		}
	}

	protected override void OnDisable()
	{
		base.self.SetEnableTap(enable: false);
		base.OnDisable();
	}

	public override void OnChangeEnableControll(bool enable)
	{
		if (enable && !CoopStageObjectUtility.CanControll(base.self))
		{
			Log.Error(LOG.INGAME, "NpcController:OnChangeEnableControll. field block enable. obj={0}", base.self);
			enable = false;
		}
		base.OnChangeEnableControll(enable);
		if (enable)
		{
			if (isStart && this.get_enabled() && base.self != null && mainCoroutine == null)
			{
				mainCoroutine = AIMain();
				this.StartCoroutine(mainCoroutine);
			}
			return;
		}
		if (mainCoroutine != null)
		{
			this.StopAllCoroutines();
			mainCoroutine = null;
		}
		if (isGuard)
		{
			base.self.ActIdle();
		}
	}

	private IEnumerator AIMain()
	{
		while (base.brain == null || !base.brain.isInitialized)
		{
			yield return 0;
		}
		while (!base.self.isControllable)
		{
			yield return 0;
		}
		if (startWaitTime > 0f)
		{
			yield return (object)new WaitForSeconds(startWaitTime);
		}
		while (this.get_enabled())
		{
			while (base.self.IsMirror())
			{
				yield return (object)new WaitForSeconds(1f);
			}
			while (IsTouchedInAutoMode())
			{
				yield return 0;
			}
			OnMove();
			OnWeapon();
			float time = 0f;
			if (base.self.packetSender != null)
			{
				time = base.self.packetSender.GetWaitTime(0f);
			}
			if (time > 0f)
			{
				yield return (object)new WaitForSeconds(time);
			}
			else
			{
				yield return 0;
			}
		}
		mainCoroutine = null;
	}

	private void OnDead()
	{
		if (!base.self.isControllable && base.self.actionID == (Character.ACTION_ID)24 && base.self.rescueTime <= 0f && base.self.deadStartTime >= 0f && !base.self.isProgressStop() && base.self == null)
		{
			base.self.DestroyObject();
		}
	}

	public void OnSkill()
	{
		if (autoBrain.skillCtr.IsAct && base.self.IsActSkillAction(autoBrain.skillCtr.skillIndex))
		{
			base.self.ActSkillAction(autoBrain.skillCtr.skillIndex);
			autoBrain.skillCtr.RemoveSkillIndex();
		}
	}

	private void OnMove()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		if (base.brain.moveCtrl.IsAvoid())
		{
			if (!base.self.IsChangeableAction(Character.ACTION_ID.MAX))
			{
				return;
			}
			OnAvoid(base.brain.moveCtrl.avoidPlace);
			flag = true;
		}
		else if (base.brain.moveCtrl.IsSeek())
		{
			if (!character.IsChangeableAction(Character.ACTION_ID.MOVE))
			{
				return;
			}
			OnMoveStick(base.brain.moveCtrl.stickVec, base.brain.moveCtrl.targetPos);
			flag = true;
		}
		else if (base.brain.moveCtrl.IsStop())
		{
			flag = false;
		}
		if (!flag && isMove)
		{
			character.ActIdle();
		}
	}

	private void OnMoveStick(Vector2 stick_vec, Vector3 target_pos)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		stick_vec.Normalize();
		Vector3 position = this.get_transform().get_position();
		Vector3 val = target_pos - position;
		val.y = 0f;
		val.Normalize();
		Vector3 val2 = Quaternion.Euler(0f, 90f, 0f) * val;
		Vector3 val3 = val;
		Vector3 val4 = (!(base.self.actionTarget != null)) ? (val2 * stick_vec.x * base.parameter.moveForwardSpeed + val3 * stick_vec.y * base.parameter.moveForwardSpeed) : (val2 * stick_vec.x * base.parameter.moveSideSpeed + val3 * stick_vec.y * base.parameter.moveForwardSpeed);
		character.ActMoveVelocity((!base.parameter.enableRootMotion) ? val4 : Vector3.get_zero(), base.parameter.moveForwardSpeed);
		character.SetLerpRotation(val4);
	}

	private void OnAvoid(PLACE avoid_place)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (!(target == null))
		{
			Vector3 val = target._transform.get_position() - character._transform.get_position();
			val.y = 0f;
			Quaternion val2 = Quaternion.LookRotation(val);
			Vector3 val3 = Vector3.get_zero();
			switch (avoid_place)
			{
			case PLACE.FRONT:
				val3 = val2 * Vector3.get_forward();
				break;
			case PLACE.BACK:
				val3 = val2 * Vector3.get_back();
				break;
			case PLACE.LEFT:
				val3 = val2 * Vector3.get_left();
				break;
			case PLACE.RIGHT:
				val3 = val2 * Vector3.get_right();
				break;
			}
			character.LookAt(character._transform.get_position() + val3);
			base.self.ActAvoid();
		}
	}

	private void OnWeapon()
	{
		if (base.brain.weaponCtrl.IsAttack())
		{
			if (base.self.attackMode == Player.ATTACK_MODE.ARROW)
			{
				OnArrowAttack();
			}
			else
			{
				OnAttack();
			}
		}
		else if (base.brain.weaponCtrl.IsSpecial())
		{
			if (base.self.attackMode == Player.ATTACK_MODE.ARROW)
			{
				OnArrowAttack();
			}
			else
			{
				OnSpecialAttack();
			}
		}
		else if (base.brain.weaponCtrl.IsGuard())
		{
			OnGuard();
		}
		else
		{
			if (base.self.actionID == (Character.ACTION_ID)19)
			{
				base.self.ActIdle(is_sync: true);
			}
			if (base.self.enableInputCharge)
			{
				base.self.SetEnableTap(enable: false);
			}
		}
		if (base.brain.weaponCtrl.changeIndex >= 0)
		{
			OnChangeWeapon();
		}
	}

	private void OnAttack()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		if (isActSpecialOneSwordSoul)
		{
			return;
		}
		bool flag = false;
		if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
			flag = true;
		}
		if (base.self.enableTap)
		{
			base.self.SetEnableTap(enable: false);
		}
		if (isAttack && base.self.enableInputCombo)
		{
			if (flag)
			{
				Vector3 val = base.self.targetingPoint.GetTargetPoint() - base.self._position;
				val.y = 0f;
				base.self.SetLerpRotation(val.get_normalized());
			}
			if (base.brain.weaponCtrl.beforeAttackId == 0 || base.brain.weaponCtrl.beforeAttackId != base.self.attackID)
			{
				base.brain.weaponCtrl.ComboOn();
				base.brain.weaponCtrl.SetBeforeAttackId(base.self.attackID);
				base.self.InputAttackCombo();
			}
		}
		else
		{
			if (!isChangeableAttack)
			{
				return;
			}
			if (flag)
			{
				Vector3 val2 = base.self.targetingPoint.GetTargetPoint() - base.self._position;
				val2.y = 0f;
				base.self.SetLerpRotation(val2.get_normalized());
			}
			if (base.brain.weaponCtrl.IsAvoidAttack())
			{
				if (base.self.actionID != Character.ACTION_ID.MAX)
				{
					base.self.ActAvoid();
					this.StartCoroutine(WaitArmorBreakAttach());
				}
				return;
			}
			string _motionLayerName = "Base Layer.";
			int normalAttackId = base.self.GetNormalAttackId(base.self.attackMode, base.self.spAttackType, base.self.extraAttackType, out _motionLayerName);
			Self self = base.self;
			int id = normalAttackId;
			string motionLayerName = _motionLayerName;
			self.ActAttack(id, send_packet: true, sync_immediately: false, motionLayerName, string.Empty);
			base.brain.weaponCtrl.ComboOff();
			base.brain.weaponCtrl.SetBeforeAttackId(0);
		}
	}

	private IEnumerator WaitArmorBreakAttach()
	{
		yield return (object)new WaitForSeconds(0.1f);
		while (!base.self.CheckAvoidAttack())
		{
			yield return null;
		}
		base.brain.weaponCtrl.AvoidAttackOff();
	}

	private void OnSpecialAttack()
	{
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		if (!base.self.isActSpecialAction && isChangeableSpecialAction)
		{
			base.self.SetEnableTap(enable: true);
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
			{
				base.self.ActSpecialAction();
				return;
			}
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
			{
				base.self.ActSpecialAction();
				this.StartCoroutine(ActSpecialPairSoulSword());
				return;
			}
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				if (!isActSpecialOneSwordSoul)
				{
					this.StartCoroutine(ActSpecialOneSwordSoul());
				}
				return;
			}
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
			{
				if (!isWaitingSpecial)
				{
					if (base.self.thsCtrl.IsRequiredReloadAction())
					{
						this.StartCoroutine(ActSpecialBurstReload());
					}
					else
					{
						this.StartCoroutine(ActSpecialBurstFire());
					}
				}
				return;
			}
			base.self.ActSpecialAction();
			if (base.self.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
			{
				switch (base.self.spAttackType)
				{
				case SP_ATTACK_TYPE.HEAT:
					base.brain.weaponCtrl.SetChargeRate(1f);
					break;
				case SP_ATTACK_TYPE.NONE:
					base.brain.weaponCtrl.SetChargeRate(0.5f);
					break;
				default:
					base.brain.weaponCtrl.SetChargeRate(1f);
					break;
				}
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				base.brain.weaponCtrl.SetChargeRate(0.5f);
			}
			else
			{
				base.brain.weaponCtrl.SetChargeRate(1f);
			}
		}
		else
		{
			if (!base.self.enableInputCharge || !(base.self.GetChargingRate() >= base.brain.weaponCtrl.chargeRate))
			{
				return;
			}
			if (base.self.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
			{
				switch (base.self.spAttackType)
				{
				case SP_ATTACK_TYPE.HEAT:
					if (base.self.targetingPoint != null)
					{
						Vector3 spearCursorPos = base.self.targetingPoint.GetTargetPoint() - base.self._position;
						spearCursorPos.y = 0f;
						base.self.SetSpearCursorPos(spearCursorPos);
					}
					else
					{
						Vector3 spearCursorPos2 = actionTargetPoint.GetTargetPoint() - base.self._position;
						spearCursorPos2.y = 0f;
						base.self.SetSpearCursorPos(spearCursorPos2);
					}
					break;
				case SP_ATTACK_TYPE.NONE:
					if (base.self.targetingPoint != null)
					{
						Vector3 val = base.self.targetingPoint.GetTargetPoint() - base.self._position;
						val.y = 0f;
						base.self.SetLerpRotation(val.get_normalized());
					}
					break;
				}
			}
			base.self.SetEnableTap(enable: false);
		}
	}

	private IEnumerator ActSpecialBurstReload()
	{
		isWaitingSpecial = true;
		base.self.ActSpecialAction();
		yield return (object)new WaitForSeconds(0.5f);
		isWaitingSpecial = false;
		base.self.SetEnableTap(enable: false);
	}

	private IEnumerator ActSpecialBurstFire()
	{
		isWaitingSpecial = true;
		base.self.ActSpecialAction();
		yield return (object)new WaitForSeconds(0.5f);
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.targetingPoint != null)
		{
			Vector3 val = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			val.y = 0f;
			base.self.SetLerpRotation(val.get_normalized());
		}
		base.self.SetEnableTap(enable: false);
		isWaitingSpecial = false;
	}

	private IEnumerator ActSpecialOneSwordSoul()
	{
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.targetingPoint != null)
		{
			Vector3 val = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			val.y = 0f;
			base.self.SetLerpRotation(val.get_normalized());
		}
		isActSpecialOneSwordSoul = true;
		base.self.ActSpecialAction();
		yield return (object)new WaitForSeconds(2f);
		base.self.SetEnableTap(enable: false);
		if (Utility.Dice100(65))
		{
			base.self.SetFlickDirection(FLICK_DIRECTION.FRONT);
			while (base.self.isActSpecialAction && !base.self.ActSpAttackContinue())
			{
				yield return null;
			}
		}
		isActSpecialOneSwordSoul = false;
	}

	private IEnumerator ActSpecialPairSoulSword()
	{
		yield return (object)new WaitForSeconds(0.5f);
		base.self.SetEnableTap(enable: false);
	}

	private void OnArrowAttack()
	{
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.isControllable)
		{
			base.self.SetEnableTap(enable: true);
			string _motionLayerName = "Base Layer.";
			int normalAttackId = base.self.GetNormalAttackId(base.self.attackMode, base.self.spAttackType, base.self.extraAttackType, out _motionLayerName);
			Self self = base.self;
			int id = normalAttackId;
			string motionLayerName = _motionLayerName;
			self.ActAttack(id, send_packet: true, sync_immediately: false, motionLayerName, string.Empty);
			if (base.brain.weaponCtrl.IsSpecial())
			{
				base.brain.weaponCtrl.SetChargeRate(1f);
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
			{
				base.brain.weaponCtrl.SetChargeRate(1f);
			}
			else
			{
				base.brain.weaponCtrl.SetChargeRate(Random.get_value());
			}
		}
		else if (base.self.enableInputCharge && base.self.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
			base.self.SetEnableTap(enable: false);
		}
		else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
		{
			if (!base.self.isArrowAimLesserMode)
			{
				base.self.SetArrowAimLesserMode(enable: true);
			}
			Vector3 zero = Vector3.get_zero();
			zero = ((!(base.self.targetingPoint != null)) ? actionTargetPoint.GetTargetPoint() : base.self.targetingPoint.GetTargetPoint());
			Vector3 arrowAimLesserCursorEffect = base.self.GetArrowAimLesserCursorEffect();
			if (Vector3.Distance(arrowAimLesserCursorEffect, zero) > 1f)
			{
				Vector3 val = arrowAimLesserCursorEffect - zero;
				Vector2 val2 = default(Vector2);
				val2._002Ector(val.x, val.z);
				base.self.UpdateArrowAimLesserMode(val2.get_normalized());
			}
			else
			{
				base.self.UpdateArrowAimLesserMode(Vector2.get_zero());
			}
		}
	}

	private void OnGuard()
	{
		if (base.self.isGuardAttackMode && !base.self.isActSpecialAction && isChangeableSpecialAction)
		{
			base.self.ActSpecialAction();
		}
	}

	private void OnChangeWeapon()
	{
		int changeIndex = base.brain.weaponCtrl.changeIndex;
		if (changeIndex >= 0 && base.self.equipWeaponList.Count > changeIndex && changeIndex != base.self.weaponIndex && base.self.equipWeaponList[changeIndex] != null)
		{
			base.self.ActChangeWeapon(base.self.equipWeaponList[changeIndex], changeIndex);
			base.brain.weaponCtrl.ResetChangeIndex();
		}
	}

	public void UpdateTarget()
	{
		base.brain.targetCtrl.UpdateTarget();
		if (base.self.attackMode == Player.ATTACK_MODE.ARROW)
		{
			UpdateRegionTarget();
		}
	}

	private void UpdateRegionTarget()
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (base.self == null)
		{
			return;
		}
		base.self.targetingPointList.Clear();
		if (target == null)
		{
			return;
		}
		Enemy enemy = target as Enemy;
		if (enemy == null || enemy.isDead || !enemy.enableTargetPoint)
		{
			return;
		}
		TargetPoint[] targetPoints = enemy.targetPoints;
		if (targetPoints == null || targetPoints.Length == 0)
		{
			return;
		}
		TargetPoint targetPoint = null;
		float num = float.MaxValue;
		Vector3 position = base.self._transform.get_position();
		Vector2 val = position.ToVector2XZ();
		Vector2 forwardXZ = base.self.forwardXZ;
		forwardXZ.Normalize();
		int i = 0;
		for (int num2 = targetPoints.Length; i < num2; i++)
		{
			TargetPoint targetPoint2 = targetPoints[i];
			if (targetPoint2.get_gameObject().get_activeInHierarchy())
			{
				Vector3 targetPoint3 = targetPoint2.GetTargetPoint();
				Vector2 val2 = targetPoint3.ToVector2XZ();
				Vector2 val3 = val2 - val;
				float sqrMagnitude = val3.get_sqrMagnitude();
				if (targetPoint == null || sqrMagnitude < num)
				{
					targetPoint = targetPoint2;
					num = sqrMagnitude;
				}
			}
		}
		if (targetPoint != null)
		{
			base.self.targetingPointList.Add(targetPoint);
		}
	}
}
