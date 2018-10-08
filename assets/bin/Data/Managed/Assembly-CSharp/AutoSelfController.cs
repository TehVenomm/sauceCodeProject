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

	private bool isAttack => (Object)base.self != (Object)null && base.self.actionID == Character.ACTION_ID.ATTACK;

	private bool isGuard => (Object)base.self != (Object)null && base.self.actionID == (Character.ACTION_ID)18;

	private bool isMove => (Object)base.self != (Object)null && base.self.actionID == Character.ACTION_ID.MOVE;

	private bool isChangeableAttack => (Object)base.self != (Object)null && base.self.IsChangeableAction(Character.ACTION_ID.ATTACK);

	private bool isChangeableSpecialAction => (Object)base.self != (Object)null && base.self.IsChangeableAction((Character.ACTION_ID)32);

	private StageObject target => (!((Object)base.brain != (Object)null)) ? null : base.brain.targetCtrl.GetCurrentTarget();

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
		if (enable && !CoopStageObjectUtility.CanControll(base.self))
		{
			Log.Error(LOG.INGAME, "NpcController:OnChangeEnableControll. field block enable. obj={0}", base.self);
			enable = false;
		}
		base.OnChangeEnableControll(enable);
		if (enable)
		{
			if (isStart && base.enabled && (Object)base.self != (Object)null && mainCoroutine == null)
			{
				mainCoroutine = AIMain();
				StartCoroutine(mainCoroutine);
			}
		}
		else
		{
			if (mainCoroutine != null)
			{
				StopAllCoroutines();
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
		while ((Object)base.brain == (Object)null || !base.brain.isInitialized)
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
		while (base.enabled)
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
			if ((Object)base.self.packetSender != (Object)null)
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
		stick_vec.Normalize();
		Vector3 position = base.transform.position;
		Vector3 vector = target_pos - position;
		vector.y = 0f;
		vector.Normalize();
		Vector3 a = Quaternion.Euler(0f, 90f, 0f) * vector;
		Vector3 a2 = vector;
		Vector3 vector2 = (!((Object)base.self.actionTarget != (Object)null)) ? (a * stick_vec.x * base.parameter.moveForwardSpeed + a2 * stick_vec.y * base.parameter.moveForwardSpeed) : (a * stick_vec.x * base.parameter.moveSideSpeed + a2 * stick_vec.y * base.parameter.moveForwardSpeed);
		character.ActMoveVelocity((!base.parameter.enableRootMotion) ? vector2 : Vector3.zero, base.parameter.moveForwardSpeed, Character.MOTION_ID.WALK);
		character.SetLerpRotation(vector2);
	}

	private void OnAvoid(PLACE avoid_place)
	{
		if (!((Object)target == (Object)null))
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
			character.LookAt(character._transform.position + b, false);
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
		if (!isActSpecialOneSwordSoul)
		{
			bool flag = false;
			if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && (Object)base.self.targetingPoint == (Object)null)
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
			else if (isChangeableAttack)
			{
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
				StartCoroutine(ActSpecialPairSoulSword());
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				if (!isActSpecialOneSwordSoul)
				{
					StartCoroutine(ActSpecialOneSwordSoul());
				}
			}
			else if (base.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
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
					if ((Object)base.self.targetingPoint != (Object)null)
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
					if ((Object)base.self.targetingPoint != (Object)null)
					{
						Vector3 vector = base.self.targetingPoint.GetTargetPoint() - base.self._position;
						vector.y = 0f;
						base.self.SetLerpRotation(vector.normalized);
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
		if ((Object)base.self.targetingPoint == (Object)null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if ((Object)base.self.targetingPoint != (Object)null)
		{
			Vector3 dir = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			dir.y = 0f;
			base.self.SetLerpRotation(dir.normalized);
		}
		base.self.SetEnableTap(false);
		isWaitingSpecial = false;
	}

	private IEnumerator ActSpecialOneSwordSoul()
	{
		if ((Object)base.self.targetingPoint == (Object)null)
		{
			base.self.targetingPointList.Add(actionTargetPoint);
		}
		if ((Object)base.self.targetingPoint != (Object)null)
		{
			Vector3 dir = base.self.targetingPoint.GetTargetPoint() - base.self._position;
			dir.y = 0f;
			base.self.SetLerpRotation(dir.normalized);
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
		if ((Object)base.self.targetingPoint == (Object)null)
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
				base.brain.weaponCtrl.SetChargeRate(Random.value);
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
		if (!((Object)base.self == (Object)null))
		{
			base.self.targetingPointList.Clear();
			if (!((Object)target == (Object)null))
			{
				Enemy enemy = target as Enemy;
				if (!((Object)enemy == (Object)null) && !enemy.isDead && enemy.enableTargetPoint)
				{
					TargetPoint[] targetPoints = enemy.targetPoints;
					if (targetPoints != null && targetPoints.Length != 0)
					{
						TargetPoint targetPoint = null;
						float num = 3.40282347E+38f;
						Vector3 position = base.self._transform.position;
						Vector2 b = position.ToVector2XZ();
						base.self.forwardXZ.Normalize();
						int i = 0;
						for (int num2 = targetPoints.Length; i < num2; i++)
						{
							TargetPoint targetPoint2 = targetPoints[i];
							if (targetPoint2.gameObject.activeInHierarchy)
							{
								Vector3 targetPoint3 = targetPoint2.GetTargetPoint();
								Vector2 a = targetPoint3.ToVector2XZ();
								float sqrMagnitude = (a - b).sqrMagnitude;
								if ((Object)targetPoint == (Object)null || sqrMagnitude < num)
								{
									targetPoint = targetPoint2;
									num = sqrMagnitude;
								}
							}
						}
						if ((Object)targetPoint != (Object)null)
						{
							base.self.targetingPointList.Add(targetPoint);
						}
					}
				}
			}
		}
	}
}
