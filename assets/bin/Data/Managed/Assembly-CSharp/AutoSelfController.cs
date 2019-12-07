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

	private bool isAttack
	{
		get
		{
			if (!(base.self != null))
			{
				return false;
			}
			return base.self.actionID == Character.ACTION_ID.ATTACK;
		}
	}

	private bool isGuard
	{
		get
		{
			if (!(base.self != null))
			{
				return false;
			}
			return base.self.actionID == (Character.ACTION_ID)19;
		}
	}

	private bool isMove
	{
		get
		{
			if (!(base.self != null))
			{
				return false;
			}
			return base.self.actionID == Character.ACTION_ID.MOVE;
		}
	}

	private bool isChangeableAttack
	{
		get
		{
			if (!(base.self != null))
			{
				return false;
			}
			return base.self.IsChangeableAction(Character.ACTION_ID.ATTACK);
		}
	}

	private bool isChangeableSpecialAction
	{
		get
		{
			if (!(base.self != null))
			{
				return false;
			}
			return base.self.IsChangeableAction((Character.ACTION_ID)33);
		}
	}

	private StageObject target
	{
		get
		{
			if (!(base.brain != null))
			{
				return null;
			}
			return base.brain.targetCtrl.GetCurrentTarget();
		}
	}

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
		if (touchInfo == null)
		{
			return stickInfo != null;
		}
		return true;
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
			if (isStart && base.enabled && base.self != null && mainCoroutine == null)
			{
				mainCoroutine = AIMain();
				StartCoroutine(mainCoroutine);
			}
			return;
		}
		if (mainCoroutine != null)
		{
			StopAllCoroutines();
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
			yield return new WaitForSeconds(startWaitTime);
		}
		while (base.enabled)
		{
			while (base.self.IsMirror())
			{
				yield return new WaitForSeconds(1f);
			}
			while (IsTouchedInAutoMode())
			{
				yield return 0;
			}
			OnMove();
			OnWeapon();
			float num = 0f;
			if (base.self.packetSender != null)
			{
				num = base.self.packetSender.GetWaitTime(0f);
			}
			if (num > 0f)
			{
				yield return new WaitForSeconds(num);
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
		if (!base.self.isControllable && base.self.actionID == (Character.ACTION_ID)24 && base.self.rescueTime <= 0f && base.self.deadStartTime >= 0f && !base.self.isProgressStop() && (object)base.self == null)
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
		stick_vec.Normalize();
		Vector3 position = base.transform.position;
		Vector3 vector = target_pos - position;
		vector.y = 0f;
		vector.Normalize();
		Vector3 a = Quaternion.Euler(0f, 90f, 0f) * vector;
		Vector3 a2 = vector;
		Vector3 vector2 = (!(base.self.actionTarget != null)) ? (a * stick_vec.x * base.parameter.moveForwardSpeed + a2 * stick_vec.y * base.parameter.moveForwardSpeed) : (a * stick_vec.x * base.parameter.moveSideSpeed + a2 * stick_vec.y * base.parameter.moveForwardSpeed);
		character.ActMoveVelocity(base.parameter.enableRootMotion ? Vector3.zero : vector2, base.parameter.moveForwardSpeed);
		character.SetLerpRotation(vector2);
	}

	private void OnAvoid(PLACE avoid_place)
	{
		if (!(target == null))
		{
			Vector3 forward = target._transform.position - character._transform.position;
			forward.y = 0f;
			Quaternion rotation = Quaternion.LookRotation(forward);
			Vector3 b = Vector3.zero;
			switch (avoid_place)
			{
			case PLACE.FRONT:
				b = rotation * Vector3.forward;
				break;
			case PLACE.BACK:
				b = rotation * Vector3.back;
				break;
			case PLACE.LEFT:
				b = rotation * Vector3.left;
				break;
			case PLACE.RIGHT:
				b = rotation * Vector3.right;
				break;
			}
			character.LookAt(character._transform.position + b);
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
				Vector3 vector = base.self.targetingPoint.GetTargetPoint() - base.self._position;
				vector.y = 0f;
				base.self.SetLerpRotation(vector.normalized);
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
				Vector3 vector2 = base.self.targetingPoint.GetTargetPoint() - base.self._position;
				vector2.y = 0f;
				base.self.SetLerpRotation(vector2.normalized);
			}
			if (base.brain.weaponCtrl.IsAvoidAttack())
			{
				if (base.self.actionID != Character.ACTION_ID.MAX)
				{
					base.self.ActAvoid();
					StartCoroutine(WaitArmorBreakAttach());
				}
			}
			else
			{
				string _motionLayerName = "Base Layer.";
				int normalAttackId = base.self.GetNormalAttackId(base.self.attackMode, base.self.spAttackType, base.self.extraAttackType, out _motionLayerName);
				base.self.ActAttack(normalAttackId, send_packet: true, sync_immediately: false, _motionLayerName);
				base.brain.weaponCtrl.ComboOff();
				base.brain.weaponCtrl.SetBeforeAttackId(0);
			}
		}
	}

	private IEnumerator WaitArmorBreakAttach()
	{
		yield return new WaitForSeconds(0.1f);
		while (!base.self.CheckAvoidAttack())
		{
			yield return null;
		}
		base.brain.weaponCtrl.AvoidAttackOff();
	}

	private void OnSpecialAttack()
	{
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
				StartCoroutine(ActSpecialPairSoulSword());
				return;
			}
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				if (!isActSpecialOneSwordSoul)
				{
					StartCoroutine(ActSpecialOneSwordSoul());
				}
				return;
			}
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
			{
				if (!isWaitingSpecial)
				{
					if (base.self.thsCtrl.IsRequiredReloadAction())
					{
						StartCoroutine(ActSpecialBurstReload());
					}
					else
					{
						StartCoroutine(ActSpecialBurstFire());
					}
				}
				return;
			}
			base.self.ActSpecialAction();
			if (base.self.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
			{
				if (base.self.spAttackType != 0)
				{
					_ = 1;
					base.brain.weaponCtrl.SetChargeRate(1f);
				}
				else
				{
					base.brain.weaponCtrl.SetChargeRate(0.5f);
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
						Vector3 vector = base.self.targetingPoint.GetTargetPoint() - base.self._position;
						vector.y = 0f;
						base.self.SetLerpRotation(vector.normalized);
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
		yield return new WaitForSeconds(0.5f);
		isWaitingSpecial = false;
		base.self.SetEnableTap(enable: false);
	}

	private IEnumerator ActSpecialBurstFire()
	{
		isWaitingSpecial = true;
		base.self.ActSpecialAction();
		yield return new WaitForSeconds(0.5f);
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.targetingPoint != null)
		{
			Vector3 vector = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			vector.y = 0f;
			base.self.SetLerpRotation(vector.normalized);
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
			Vector3 vector = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			vector.y = 0f;
			base.self.SetLerpRotation(vector.normalized);
		}
		isActSpecialOneSwordSoul = true;
		base.self.ActSpecialAction();
		yield return new WaitForSeconds(2f);
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
		yield return new WaitForSeconds(0.5f);
		base.self.SetEnableTap(enable: false);
	}

	private void OnArrowAttack()
	{
		if (base.self.targetingPoint == null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if (base.self.isControllable)
		{
			base.self.SetEnableTap(enable: true);
			string _motionLayerName = "Base Layer.";
			int normalAttackId = base.self.GetNormalAttackId(base.self.attackMode, base.self.spAttackType, base.self.extraAttackType, out _motionLayerName);
			base.self.ActAttack(normalAttackId, send_packet: true, sync_immediately: false, _motionLayerName);
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
				base.brain.weaponCtrl.SetChargeRate(Random.value);
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
			Vector3 zero = Vector3.zero;
			zero = ((!(base.self.targetingPoint != null)) ? actionTargetPoint.GetTargetPoint() : base.self.targetingPoint.GetTargetPoint());
			Vector3 arrowAimLesserCursorEffect = base.self.GetArrowAimLesserCursorEffect();
			if (Vector3.Distance(arrowAimLesserCursorEffect, zero) > 1f)
			{
				Vector3 vector = arrowAimLesserCursorEffect - zero;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				base.self.UpdateArrowAimLesserMode(vector2.normalized);
			}
			else
			{
				base.self.UpdateArrowAimLesserMode(Vector2.zero);
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
		Vector2 b = base.self._transform.position.ToVector2XZ();
		base.self.forwardXZ.Normalize();
		int i = 0;
		for (int num2 = targetPoints.Length; i < num2; i++)
		{
			TargetPoint targetPoint2 = targetPoints[i];
			if (targetPoint2.gameObject.activeInHierarchy)
			{
				float sqrMagnitude = (targetPoint2.GetTargetPoint().ToVector2XZ() - b).sqrMagnitude;
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
