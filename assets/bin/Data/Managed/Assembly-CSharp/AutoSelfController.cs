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

	private bool isGuard => base.self != null && base.self.actionID == (Character.ACTION_ID)18;

	private bool isMove => base.self != null && base.self.actionID == Character.ACTION_ID.MOVE;

	private bool isChangeableAttack => base.self != null && base.self.IsChangeableAction(Character.ACTION_ID.ATTACK);

	private bool isChangeableSpecialAction => base.self != null && base.self.IsChangeableAction((Character.ACTION_ID)32);

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
			OnChangeEnableControll(true);
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
		}
		else
		{
			if (base.self.actionID != (Character.ACTION_ID)26)
			{
				bool flag = false;
				if (base.self.isGuardWalk || base.self.actionID == (Character.ACTION_ID)18 || base.self.actionID == (Character.ACTION_ID)19)
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
	}

	protected override void OnDisable()
	{
		base.self.SetEnableTap(false);
		base.OnDisable();
	}

	public override void OnChangeEnableControll(bool enable)
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
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
		}
		else
		{
			if (mainCoroutine != null)
			{
				this.StopAllCoroutines();
				mainCoroutine = null;
			}
			if (isGuard)
			{
				base.self.ActIdle(false, -1f);
			}
		}
	}

	private IEnumerator AIMain()
	{
		while (base.brain == null || !base.brain.isInitialized)
		{
			yield return (object)0;
		}
		while (!base.self.isControllable)
		{
			yield return (object)0;
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
				yield return (object)0;
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
				yield return (object)0;
			}
		}
		mainCoroutine = null;
	}

	private void OnDead()
	{
		if (!base.self.isControllable && base.self.actionID == (Character.ACTION_ID)23 && base.self.rescueTime <= 0f && base.self.deadStartTime >= 0f && !base.self.isProgressStop() && !(base.self is Self))
		{
			base.self.DestroyObject();
		}
	}

	public void OnSkill()
	{
		if (autoBrain.skillCtr.IsAct && base.self.IsActSkillAction(autoBrain.skillCtr.skillIndex))
		{
			base.self.ActSkillAction(autoBrain.skillCtr.skillIndex, false);
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
			character.ActIdle(false, -1f);
		}
	}

	private void OnMoveStick(Vector2 stick_vec, Vector3 target_pos)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
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
		character.ActMoveVelocity((!base.parameter.enableRootMotion) ? val4 : Vector3.get_zero(), base.parameter.moveForwardSpeed, Character.MOTION_ID.WALK);
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
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
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
			character.LookAt(character._transform.get_position() + val3, false);
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
			if (base.self.actionID == (Character.ACTION_ID)18)
			{
				base.self.ActIdle(true, -1f);
			}
			if (base.self.enableInputCharge)
			{
				base.self.SetEnableTap(false);
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
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		if (!isActSpecialOneSwordSoul)
		{
			bool flag = false;
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && base.self.targetingPoint == null)
			{
				base.self.targetingPointList.Add(actionTargetPoint);
				flag = true;
			}
			if (base.self.enableTap)
			{
				base.self.SetEnableTap(false);
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
			else if (isChangeableAttack)
			{
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
				}
				else
				{
					string _motionLayerName = "Base Layer.";
					int normalAttackId = base.self.GetNormalAttackId(base.self.attackMode, base.self.spAttackType, base.self.extraAttackType, out _motionLayerName);
					Self self = base.self;
					string motionLayerName = _motionLayerName;
					self.ActAttack(normalAttackId, true, false, motionLayerName);
					base.brain.weaponCtrl.ComboOff();
					base.brain.weaponCtrl.SetBeforeAttackId(0);
				}
			}
		}
	}

	private IEnumerator WaitArmorBreakAttach()
	{
		yield return (object)new WaitForSeconds(0.1f);
		while (!base.self.CheckAvoidAttack())
		{
			yield return (object)null;
		}
		base.brain.weaponCtrl.AvoidAttackOff();
	}

	private void OnSpecialAttack()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		if (!base.self.isActSpecialAction && isChangeableSpecialAction)
		{
			base.self.SetEnableTap(true);
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
			{
				base.self.ActSpecialAction(true, true);
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
			{
				base.self.ActSpecialAction(true, true);
				this.StartCoroutine(ActSpecialPairSoulSword());
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				if (!isActSpecialOneSwordSoul)
				{
					this.StartCoroutine(ActSpecialOneSwordSoul());
				}
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
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
			}
			else
			{
				base.self.ActSpecialAction(true, true);
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
		}
		else if (base.self.enableInputCharge && base.self.GetChargingRate() >= base.brain.weaponCtrl.chargeRate)
		{
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
			base.self.SetEnableTap(false);
		}
	}

	private IEnumerator ActSpecialBurstReload()
	{
		isWaitingSpecial = true;
		base.self.ActSpecialAction(true, true);
		yield return (object)new WaitForSeconds(0.5f);
		isWaitingSpecial = false;
		base.self.SetEnableTap(false);
	}

	private IEnumerator ActSpecialBurstFire()
	{
		isWaitingSpecial = true;
		base.self.ActSpecialAction(true, true);
		yield return (object)new WaitForSeconds(0.5f);
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.targetingPoint != null)
		{
			Vector3 dir = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			dir.y = 0f;
			base.self.SetLerpRotation(dir.get_normalized());
		}
		base.self.SetEnableTap(false);
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
			Vector3 dir = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			dir.y = 0f;
			base.self.SetLerpRotation(dir.get_normalized());
		}
		isActSpecialOneSwordSoul = true;
		base.self.ActSpecialAction(true, true);
		yield return (object)new WaitForSeconds(2f);
		base.self.SetEnableTap(false);
		if (Utility.Dice100(65))
		{
			base.self.SetFlickDirection(FLICK_DIRECTION.FRONT);
			while (base.self.isActSpecialAction && !base.self.ActSpAttackContinue())
			{
				yield return (object)null;
			}
		}
		isActSpecialOneSwordSoul = false;
	}

	private IEnumerator ActSpecialPairSoulSword()
	{
		yield return (object)new WaitForSeconds(0.5f);
		base.self.SetEnableTap(false);
	}

	private void OnArrowAttack()
	{
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.isControllable)
		{
			base.self.SetEnableTap(true);
			string _motionLayerName = "Base Layer.";
			int normalAttackId = base.self.GetNormalAttackId(base.self.attackMode, base.self.spAttackType, base.self.extraAttackType, out _motionLayerName);
			Self self = base.self;
			string motionLayerName = _motionLayerName;
			self.ActAttack(normalAttackId, true, false, motionLayerName);
			if (base.brain.weaponCtrl.IsSpecial())
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
			base.self.SetEnableTap(false);
		}
	}

	private void OnGuard()
	{
		if (base.self.isGuardAttackMode && !base.self.isActSpecialAction && isChangeableSpecialAction)
		{
			base.self.ActSpecialAction(true, true);
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
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (!(base.self == null))
		{
			base.self.targetingPointList.Clear();
			if (!(target == null))
			{
				Enemy enemy = target as Enemy;
				if (!(enemy == null) && !enemy.isDead && enemy.enableTargetPoint)
				{
					TargetPoint[] targetPoints = enemy.targetPoints;
					if (targetPoints != null && targetPoints.Length != 0)
					{
						TargetPoint targetPoint = null;
						float num = 3.40282347E+38f;
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
			}
		}
	}
}
