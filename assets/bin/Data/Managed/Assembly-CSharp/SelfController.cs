using System;
using UnityEngine;

public class SelfController : ControllerBase
{
	public enum COMMAND_TYPE
	{
		NONE = -1,
		ATTACK,
		AVOID,
		SKILL,
		SPECIAL_ACTION,
		CHANGE_WEAPON,
		GATHER,
		CANNON_STANDBY,
		CANNON_SHOT,
		SONAR,
		WARP,
		EVOLVE,
		EVOLVE_SPECIAL,
		READ_STORY,
		THS_BURST_RELOAD,
		GATHER_GIMMICK,
		BINGO,
		COOP_FISHING,
		FLICK_ACTION,
		CARRY,
		PORTAL_GIMMICK,
		QUEST_GIMMICK,
		TELEPORT_AVOID,
		RUSH_AVOID
	}

	public class Command
	{
		public COMMAND_TYPE type = COMMAND_TYPE.NONE;

		public Vector2 inputVec = Vector2.zero;

		public float deltaTime;

		public bool isTouchOn;

		public bool aimKeep;

		public int MotionId = -1;

		public int skillIndex = -1;

		public int weaponIndex = -1;

		public GatherPointObject gatherPoint;

		public IFieldGimmickObject fieldGimmickObject;
	}

	public enum FLICK_DIRECTION
	{
		NONE,
		FRONT,
		REAR,
		LEFT,
		RIGHT
	}

	protected InputManager.TouchInfo touchInfo;

	protected bool initTouch;

	protected bool enableTouch;

	protected bool isInputtedTouch;

	protected bool checkTouch;

	protected float touchAimAxisTime = -1f;

	protected COMMAND_TYPE lastActCommandType = COMMAND_TYPE.NONE;

	protected float lastActCommandTime = -1f;

	protected InputManager.TouchInfo moveStickInfo;

	private FLICK_DIRECTION flickDirection = FLICK_DIRECTION.FRONT;

	private static readonly float QUARTER_PI = (float)Math.PI / 4f;

	private static readonly float THREE_QUARTER_PI = (float)Math.PI * 3f / 4f;

	private Vector3 slideVerocity;

	private float slideTimer;

	private float slideSlowTimer;

	public InGameSettingsManager.SelfController parameter
	{
		get;
		private set;
	}

	public Self self
	{
		get;
		private set;
	}

	private InGameSettingsManager.DebuffParam debuffParam
	{
		get;
		set;
	}

	public Command nextCommand
	{
		get;
		protected set;
	}

	public SelfController()
	{
		nextCommand = null;
	}

	private void OnDestroy()
	{
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTap = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTap, new InputManager.OnTouchDelegate(OnTap));
		InputManager.OnFlick = (InputManager.OnFlickDelegate)Delegate.Remove(InputManager.OnFlick, new InputManager.OnFlickDelegate(OnFlick));
		InputManager.OnLongTouch = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnLongTouch, new InputManager.OnTouchDelegate(OnLongTouch));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController;
		debuffParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff;
		self = (character as Self);
		InputManager.OnTouchOn = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOn, new InputManager.OnTouchDelegate(OnTouchOn));
		InputManager.OnTap = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTap, new InputManager.OnTouchDelegate(OnTap));
		InputManager.OnFlick = (InputManager.OnFlickDelegate)Delegate.Combine(InputManager.OnFlick, new InputManager.OnFlickDelegate(OnFlick));
		InputManager.OnLongTouch = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnLongTouch, new InputManager.OnTouchDelegate(OnLongTouch));
		InputManager.OnTouchOff = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnTouchOff, new InputManager.OnTouchDelegate(OnTouchOff));
	}

	public override void OnChangeEnableControll(bool enable)
	{
		if (!(self == null))
		{
			CancelInput();
			if (touchInfo != null && initTouch)
			{
				touchInfo = null;
				enableTouch = false;
				isInputtedTouch = false;
				self.SetEnableTap(enable: false);
			}
			if (self.isGuardWalk || self.actionID == (Character.ACTION_ID)19)
			{
				self.ActIdle(is_sync: true);
			}
			moveStickInfo = null;
			lastActCommandType = COMMAND_TYPE.NONE;
			lastActCommandTime = -1f;
			base.OnChangeEnableControll(enable);
		}
	}

	public void SetCommand(Command command)
	{
		if (command != null && command.type != COMMAND_TYPE.NONE && IsCancelAble(command) && (lastActCommandType == COMMAND_TYPE.NONE || command.type != lastActCommandType || !(Time.time - lastActCommandTime < parameter.inputCommandIntervalTime[(int)command.type])))
		{
			lastActCommandType = COMMAND_TYPE.NONE;
			lastActCommandTime = -1f;
			CancelInput();
			if (CheckCommand(command))
			{
				ActCommand(command);
			}
			else
			{
				nextCommand = command;
			}
		}
	}

	public void CancelInput()
	{
		nextCommand = null;
		self.CancelAttackCombo();
	}

	public bool CheckCommand(Command command)
	{
		switch (command.type)
		{
		case COMMAND_TYPE.ATTACK:
			return self.IsChangeableAction(Character.ACTION_ID.ATTACK);
		case COMMAND_TYPE.AVOID:
			return self.IsChangeableAction(Character.ACTION_ID.MAX);
		case COMMAND_TYPE.SKILL:
			if (CheckCommandWithSkillTableData(command))
			{
				return true;
			}
			return self.IsChangeableAction((Character.ACTION_ID)22);
		case COMMAND_TYPE.SPECIAL_ACTION:
			return self.IsChangeableAction((Character.ACTION_ID)33);
		case COMMAND_TYPE.CHANGE_WEAPON:
			return self.IsChangeableAction((Character.ACTION_ID)27);
		case COMMAND_TYPE.GATHER:
			return self.IsChangeableAction((Character.ACTION_ID)28);
		case COMMAND_TYPE.CANNON_STANDBY:
			return self.IsChangeableAction((Character.ACTION_ID)31);
		case COMMAND_TYPE.CANNON_SHOT:
			return self.IsChangeableAction((Character.ACTION_ID)32);
		case COMMAND_TYPE.SONAR:
			return self.IsChangeableAction((Character.ACTION_ID)35);
		case COMMAND_TYPE.WARP:
			return self.IsChangeableAction((Character.ACTION_ID)36);
		case COMMAND_TYPE.EVOLVE:
			return self.IsChangeableAction((Character.ACTION_ID)37);
		case COMMAND_TYPE.EVOLVE_SPECIAL:
			return self.IsChangeableAction((Character.ACTION_ID)38);
		case COMMAND_TYPE.READ_STORY:
			return self.IsChangeableAction((Character.ACTION_ID)39);
		case COMMAND_TYPE.THS_BURST_RELOAD:
			return self.IsChangeableAction(Character.ACTION_ID.ATTACK, command.MotionId);
		case COMMAND_TYPE.GATHER_GIMMICK:
			return self.IsChangeableAction((Character.ACTION_ID)40);
		case COMMAND_TYPE.FLICK_ACTION:
			return self.IsChangeableAction((Character.ACTION_ID)42);
		case COMMAND_TYPE.COOP_FISHING:
			return self.IsChangeableAction((Character.ACTION_ID)41);
		case COMMAND_TYPE.CARRY:
			return self.IsChangeableAction((Character.ACTION_ID)44);
		case COMMAND_TYPE.TELEPORT_AVOID:
			return self.IsChangeableAction((Character.ACTION_ID)46);
		case COMMAND_TYPE.RUSH_AVOID:
			return self.IsChangeableAction((Character.ACTION_ID)49);
		case COMMAND_TYPE.QUEST_GIMMICK:
			return self.IsChangeableAction((Character.ACTION_ID)28);
		default:
			return true;
		}
	}

	public void ActCommand(Command command)
	{
		int num = 0;
		string text = "Base Layer.";
		switch (command.type)
		{
		case COMMAND_TYPE.ATTACK:
			if (command.aimKeep)
			{
				self.SetArrowAimKeep();
			}
			if (character is Player)
			{
				Player player = character as Player;
				if (player != null && player.HitSpearSpecialAction)
				{
					character.ActAttack(10);
					break;
				}
			}
			if (self.spearCtrl.IsEnableBurstCombo())
			{
				self.spearCtrl.ActAttackBurstCombo();
			}
			else
			{
				if (self.CheckAvoidAttack() || self.ActSpAttackContinue() || self.CheckAttackNext())
				{
					break;
				}
				if (self.IsAbleArrowSitShot())
				{
					if (self.IsActionFromAvoid())
					{
						self.ActAttack(98);
					}
					else
					{
						self.ActAttack(97);
					}
				}
				else if (self.IsAbleArrowRainShot())
				{
					if (self.IsActionFromAvoid())
					{
						self.ActAttack(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAttackId);
					}
					else
					{
						self.ActAttack(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowReloadRainShotAttackId);
					}
				}
				else
				{
					text = "Base Layer.";
					num = self.GetNormalAttackId(self.attackMode, self.spAttackType, self.extraAttackType, out text);
					character.ActAttack(num, send_packet: true, sync_immediately: false, text);
				}
			}
			break;
		case COMMAND_TYPE.AVOID:
		{
			Transform cameraTransform4 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right4 = cameraTransform4.right;
			Vector3 forward4 = cameraTransform4.forward;
			forward4.y = 0f;
			forward4.Normalize();
			Vector3 worldPosition3 = base.transform.position + (right4 * command.inputVec.x + forward4 * command.inputVec.y);
			base.transform.LookAt(worldPosition3);
			self.ActAvoid();
			slideVerocity = Vector3.zero;
			slideTimer = 0f;
			break;
		}
		case COMMAND_TYPE.WARP:
		{
			Transform cameraTransform3 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right3 = cameraTransform3.right;
			Vector3 forward3 = cameraTransform3.forward;
			forward3.y = 0f;
			forward3.Normalize();
			Vector3 worldPosition2 = base.transform.position + (right3 * command.inputVec.x + forward3 * command.inputVec.y);
			base.transform.LookAt(worldPosition2);
			self.ActWarp();
			slideVerocity = Vector3.zero;
			slideTimer = 0f;
			break;
		}
		case COMMAND_TYPE.SKILL:
			self.ActSkillAction(command.skillIndex);
			break;
		case COMMAND_TYPE.SPECIAL_ACTION:
			if (self.CheckAttackMode(Player.ATTACK_MODE.PAIR_SWORDS) && !self.pairSwordsCtrl.IsAbleToAlterSpAction())
			{
				self.ActSpecialAction(start_effect: false, isSuccess: false);
			}
			else if (!self.CheckWeaponActionForSpAction())
			{
				self.ActSpecialAction();
			}
			break;
		case COMMAND_TYPE.CHANGE_WEAPON:
			self.ActChangeWeapon(self.equipWeaponList[command.weaponIndex], command.weaponIndex);
			break;
		case COMMAND_TYPE.GATHER:
			if (self.nearGatherPoint == command.gatherPoint)
			{
				self.ActGather(self.nearGatherPoint);
			}
			break;
		case COMMAND_TYPE.SONAR:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActSonar(self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.CANNON_STANDBY:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActCannonStandby(self.nearFieldGimmick.GetId());
				if (self.nearFieldGimmick is FieldGimmickCannonSpecial)
				{
					self.SetCannonBeamMode();
				}
				else
				{
					self.SetCannonAimMode();
				}
			}
			break;
		case COMMAND_TYPE.CANNON_SHOT:
			if (!(self.targetFieldGimmickCannon is FieldGimmickCannonSpecial) || self.IsCannonFullCharged())
			{
				self.ActCannonShot();
			}
			break;
		case COMMAND_TYPE.EVOLVE:
			self.ActEvolve();
			break;
		case COMMAND_TYPE.EVOLVE_SPECIAL:
			self.ActEvolveSpecialAction();
			break;
		case COMMAND_TYPE.READ_STORY:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActReadStory(self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.THS_BURST_RELOAD:
			text = self.GetMotionLayerName(self.attackMode, self.spAttackType, command.MotionId);
			character.ActAttack(command.MotionId, send_packet: true, sync_immediately: false, text);
			break;
		case COMMAND_TYPE.GATHER_GIMMICK:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActGatherGimmick(self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.BINGO:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActBingo(self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.FLICK_ACTION:
			self.ActFlickAction(command.inputVec, isOriginal: true);
			slideVerocity = Vector3.zero;
			slideTimer = 0f;
			break;
		case COMMAND_TYPE.COOP_FISHING:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActCoopFishingStart(self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.CARRY:
			if (self.nearFieldGimmick != null)
			{
				InGameProgress.eFieldGimmick type = InGameProgress.eFieldGimmick.CarriableGimmick;
				if (self.nearFieldGimmick is FieldSupplyGimmickObject)
				{
					type = InGameProgress.eFieldGimmick.SupplyGimmick;
				}
				self.ActCarry(type, self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.PORTAL_GIMMICK:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActPortalGimmick(self.nearFieldGimmick.GetId());
			}
			break;
		case COMMAND_TYPE.TELEPORT_AVOID:
		{
			Transform cameraTransform2 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right2 = cameraTransform2.right;
			Vector3 forward2 = cameraTransform2.forward;
			forward2.y = 0f;
			forward2.Normalize();
			Vector3 worldPosition = base.transform.position + (right2 * command.inputVec.x + forward2 * command.inputVec.y);
			base.transform.LookAt(worldPosition);
			self.ActTeleportAvoid();
			slideVerocity = Vector3.zero;
			slideTimer = 0f;
			break;
		}
		case COMMAND_TYPE.RUSH_AVOID:
		{
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right = cameraTransform.right;
			Vector3 forward = cameraTransform.forward;
			forward.y = 0f;
			forward.Normalize();
			self.ActRushAvoid(right * command.inputVec.x + forward * command.inputVec.y);
			slideVerocity = Vector3.zero;
			slideTimer = 0f;
			break;
		}
		case COMMAND_TYPE.QUEST_GIMMICK:
			if (self.nearFieldGimmick == command.fieldGimmickObject)
			{
				self.ActQuestGimmick(self.nearFieldGimmick.GetId());
			}
			break;
		}
		if (command.isTouchOn)
		{
			enableTouch = false;
			isInputtedTouch = true;
		}
		else if (isInputtedTouch)
		{
			checkTouch = true;
		}
		lastActCommandType = command.type;
		lastActCommandTime = Time.time;
	}

	public bool IsCancelAble(Command check_command)
	{
		return !IsCancelNextNotCancel();
	}

	public bool IsCancelNextNotCancel()
	{
		if (nextCommand != null)
		{
			switch (nextCommand.type)
			{
			case COMMAND_TYPE.SKILL:
				return true;
			case COMMAND_TYPE.CHANGE_WEAPON:
				return true;
			}
		}
		return false;
	}

	private void OnTouchOn(InputManager.TouchInfo touch_info)
	{
		if (IsEnableControll() && !(character == null) && !character.isLoading && touchInfo == null)
		{
			touchInfo = touch_info;
			initTouch = false;
			enableTouch = false;
			isInputtedTouch = false;
			checkTouch = false;
		}
	}

	private void OnTap(InputManager.TouchInfo touch_info)
	{
		if (!IsEnableControll() || character == null || character.isLoading)
		{
			return;
		}
		if (self.isStunnedLoop)
		{
			CancelInput();
			self.ReduceStunnedTime();
			return;
		}
		if (self.actionID == (Character.ACTION_ID)16)
		{
			CancelInput();
			self.InputBlowClear();
			return;
		}
		if (self.IsCarrying())
		{
			if (self.enableCancelToCarryPut)
			{
				if (self.nearFieldGimmick is FieldCarriableGimmickObject && self.carryingGimmickObject is FieldCarriableEvolveItemGimmickObject && (self.nearFieldGimmick as FieldCarriableGimmickObject).CanEvolve())
				{
					self.ActCarryPut(self.nearFieldGimmick.GetId());
				}
				else
				{
					self.ActCarryPut();
				}
			}
			return;
		}
		if (self.targetFieldGimmickCannon != null && self.targetFieldGimmickCannon != null)
		{
			if (self.cannonState == Player.CANNON_STATE.READY)
			{
				Command command = new Command();
				command.type = COMMAND_TYPE.CANNON_SHOT;
				command.fieldGimmickObject = self.targetFieldGimmickCannon;
				SetCommand(command);
			}
			return;
		}
		if (self.nearFieldGimmick != null)
		{
			if (self.nearFieldGimmick is IFieldGimmickCannon)
			{
				IFieldGimmickCannon fieldGimmickCannon = self.nearFieldGimmick as IFieldGimmickCannon;
				if (fieldGimmickCannon != null && !fieldGimmickCannon.IsUsing() && fieldGimmickCannon.IsAbleToUse() && self.cannonState == Player.CANNON_STATE.NONE)
				{
					Command command2 = new Command();
					command2.type = COMMAND_TYPE.CANNON_STANDBY;
					command2.fieldGimmickObject = self.nearFieldGimmick;
					SetCommand(command2);
					return;
				}
			}
			if (self.nearFieldGimmick is FieldSonarObject)
			{
				SetCommandForFieldGimmick<FieldSonarObject>(COMMAND_TYPE.SONAR);
				return;
			}
			if (self.nearFieldGimmick is FieldReadStoryObject)
			{
				SetCommandForFieldGimmick<FieldReadStoryObject>(COMMAND_TYPE.READ_STORY);
				return;
			}
			if (self.nearFieldGimmick is FieldChatGimmickObject)
			{
				FieldChatGimmickObject fieldChatGimmickObject = self.nearFieldGimmick as FieldChatGimmickObject;
				if (fieldChatGimmickObject != null && fieldChatGimmickObject.StartChat())
				{
					return;
				}
			}
			if (self.nearFieldGimmick is FieldGimmickCoopFishing && self.fishingCtrl != null && !self.fishingCtrl.IsFishing())
			{
				SetCommandForFieldGimmick<FieldGimmickCoopFishing>(COMMAND_TYPE.COOP_FISHING);
				return;
			}
			if (self.nearFieldGimmick is FieldGatherGimmickObject)
			{
				FieldGatherGimmickObject fieldGatherGimmickObject = self.nearFieldGimmick as FieldGatherGimmickObject;
				GATHER_GIMMICK_TYPE gatherGimmickType = fieldGatherGimmickObject.GetGatherGimmickType();
				if ((uint)(gatherGimmickType - 1) <= 1u && fieldGatherGimmickObject != null && fieldGatherGimmickObject.CanUse() && self.fishingCtrl != null && !self.fishingCtrl.IsFishing())
				{
					Command command3 = new Command();
					command3.type = COMMAND_TYPE.GATHER_GIMMICK;
					command3.fieldGimmickObject = self.nearFieldGimmick;
					SetCommand(command3);
					return;
				}
			}
			if (self.nearFieldGimmick is FieldQuestGimmickObject && self.nearFieldGimmick as FieldQuestGimmickObject != null)
			{
				Command command4 = new Command();
				command4.type = COMMAND_TYPE.QUEST_GIMMICK;
				command4.fieldGimmickObject = self.nearFieldGimmick;
				SetCommand(command4);
				return;
			}
			if (self.nearFieldGimmick is FieldBingoObject)
			{
				SetCommandForFieldGimmick<FieldBingoObject>(COMMAND_TYPE.BINGO);
				return;
			}
			if (self.nearFieldGimmick is FieldCarriableGimmickObject)
			{
				SetCommandForFieldGimmick<FieldCarriableGimmickObject>(COMMAND_TYPE.CARRY);
				return;
			}
			if (self.nearFieldGimmick is FieldSupplyGimmickObject)
			{
				SetCommandForFieldGimmick<FieldSupplyGimmickObject>(COMMAND_TYPE.CARRY);
				return;
			}
			if (self.nearFieldGimmick is FieldPortalGimmickObject)
			{
				SetCommandForFieldGimmick<FieldPortalGimmickObject>(COMMAND_TYPE.PORTAL_GIMMICK);
				return;
			}
		}
		if (self.cannonState != 0 || self.spearCtrl.IsInputedSpAttackContinue())
		{
			return;
		}
		if (self.fishingCtrl != null && self.fishingCtrl.IsFishing())
		{
			self.fishingCtrl.Tap();
			return;
		}
		self.SetFlickDirection(FLICK_DIRECTION.FRONT);
		self.OnTap();
		if (self.nearGatherPoint != null)
		{
			Command command5 = new Command();
			command5.type = COMMAND_TYPE.GATHER;
			command5.gatherPoint = self.nearGatherPoint;
			SetCommand(command5);
		}
		self.InputNextTrigger();
		InputAttack();
	}

	private bool CheckCommandWithSkillTableData(Command command)
	{
		if (command.type != COMMAND_TYPE.SKILL)
		{
			return false;
		}
		if (self.actionID != Character.ACTION_ID.IDLE && self.actionID != Character.ACTION_ID.MOVE && self.actionID != Character.ACTION_ID.ATTACK && self.actionID != Character.ACTION_ID.MAX && self.actionID != (Character.ACTION_ID)36 && self.actionID != (Character.ACTION_ID)33 && self.actionID != (Character.ACTION_ID)37 && self.actionID != (Character.ACTION_ID)38 && self.actionID != (Character.ACTION_ID)42 && self.actionID != (Character.ACTION_ID)46 && self.actionID != (Character.ACTION_ID)49)
		{
			return false;
		}
		SkillInfo.SkillParam skillParam = self.GetSkillParam(command.skillIndex);
		if (skillParam == null || !skillParam.isValid)
		{
			return false;
		}
		SkillItemTable.SkillItemData tableData = skillParam.tableData;
		if (tableData == null)
		{
			return false;
		}
		if (tableData.isTeleportation)
		{
			return true;
		}
		return false;
	}

	private void SetCommandForFieldGimmick<T>(COMMAND_TYPE cmdType) where T : FieldGimmickObject
	{
		if (!((UnityEngine.Object)(self.nearFieldGimmick as T) == (UnityEngine.Object)null))
		{
			Command command = new Command();
			command.type = cmdType;
			command.fieldGimmickObject = self.nearFieldGimmick;
			SetCommand(command);
		}
	}

	private void InputAttack(bool is_touch_on = false)
	{
		if (self.controllerInputCombo && (character.actionID == Character.ACTION_ID.ATTACK || character.actionID == (Character.ACTION_ID)21 || (self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST) && character.actionID == (Character.ACTION_ID)42)))
		{
			CancelInput();
			if (self.InputAttackCombo())
			{
				if (is_touch_on)
				{
					enableTouch = false;
					isInputtedTouch = true;
				}
				else if (isInputtedTouch)
				{
					checkTouch = true;
				}
			}
		}
		else
		{
			Command command = new Command();
			command.type = COMMAND_TYPE.ATTACK;
			command.isTouchOn = is_touch_on;
			SetCommand(command);
		}
	}

	private void OnLongTouch(InputManager.TouchInfo touch_info)
	{
		if (IsEnableControll())
		{
			_ = (character == null);
		}
	}

	private void OnFlick(Vector2 flick_vec)
	{
		if (!IsEnableControll() || character == null || character.isLoading || self.IsCarrying())
		{
			return;
		}
		if (self.isStunnedLoop)
		{
			CancelInput();
			self.ReduceStunnedTime();
			return;
		}
		if (self.IsRestraint())
		{
			CancelInput();
			self.ReduceRestraintTime();
			return;
		}
		if (self.IsInkSplash())
		{
			self.ReduceInkSplashTime();
		}
		if (!self.spearCtrl.IsInputedSpAttackContinue() || self.enableCancelToAvoid)
		{
			self.SetArrowAimBossModeStartSign(flick_vec);
			Command command = new Command();
			if (self.CanSoulOneHandSwordFlickAction())
			{
				SetFlickDirection(flick_vec);
				self.SetFlickDirection(flickDirection);
				command.type = COMMAND_TYPE.ATTACK;
			}
			else if (self.CanFlickAction())
			{
				command.type = COMMAND_TYPE.FLICK_ACTION;
			}
			else if (self.CanEvolveSpecialFlickAction())
			{
				command.type = COMMAND_TYPE.EVOLVE_SPECIAL;
			}
			else if (self.enabledTeleportAvoid)
			{
				command.type = COMMAND_TYPE.TELEPORT_AVOID;
			}
			else if (self.enabledRushAvoid)
			{
				command.type = COMMAND_TYPE.RUSH_AVOID;
			}
			else if (self.buffParam.IsEnableBuff(BuffParam.BUFFTYPE.WARP_BY_AVOID))
			{
				command.type = COMMAND_TYPE.WARP;
			}
			else
			{
				command.type = COMMAND_TYPE.AVOID;
			}
			command.inputVec = flick_vec;
			SetCommand(command);
			slideTimer = 0f;
			slideVerocity = Vector3.zero;
		}
	}

	private void SetFlickDirection(Vector2 flick_vec)
	{
		if (flick_vec == Vector2.zero)
		{
			return;
		}
		float num = Mathf.Atan2(flick_vec.y, flick_vec.x);
		float num2 = Mathf.Abs(num);
		if (num > 0f)
		{
			if (num2 < QUARTER_PI)
			{
				flickDirection = FLICK_DIRECTION.RIGHT;
			}
			else if (num2 > THREE_QUARTER_PI)
			{
				flickDirection = FLICK_DIRECTION.LEFT;
			}
			else
			{
				flickDirection = FLICK_DIRECTION.FRONT;
			}
		}
		else if (num2 < QUARTER_PI)
		{
			flickDirection = FLICK_DIRECTION.RIGHT;
		}
		else if (num2 > THREE_QUARTER_PI)
		{
			flickDirection = FLICK_DIRECTION.LEFT;
		}
		else
		{
			flickDirection = FLICK_DIRECTION.REAR;
		}
	}

	private void OnTouchOff(InputManager.TouchInfo touch_info)
	{
		IsEnableControll();
	}

	protected override void Update()
	{
		InputManager.TouchInfo touchInfo = moveStickInfo;
		moveStickInfo = null;
		if ((self.actionID == (Character.ACTION_ID)20 || self.actionID == (Character.ACTION_ID)34 || self.actionID == (Character.ACTION_ID)21) && isInputtedTouch)
		{
			checkTouch = true;
		}
		if (nextCommand != null && (self == null || !self.IsHitStop()))
		{
			nextCommand.deltaTime += Time.deltaTime;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionType().IsDialog())
		{
			SetEnableControll(enable: false, DISABLE_FLAG.INPUT_DISABLE);
		}
		else
		{
			SetEnableControll(enable: true, DISABLE_FLAG.INPUT_DISABLE);
		}
		if (!IsEnableControll() || character == null || character.isLoading)
		{
			return;
		}
		base.Update();
		InputManager.TouchInfo stickInfo = MonoBehaviourSingleton<InputManager>.I.GetStickInfo();
		if (stickInfo != null)
		{
			self.SetInputAxis(stickInfo.axis);
		}
		bool flag = false;
		if (this.touchInfo != null)
		{
			bool activeAxis = this.touchInfo.activeAxis;
			float num = 0.5f;
			if (GameSaveData.instance != null)
			{
				num = GameSaveData.instance.touchInGameLong;
			}
			float num2 = parameter.inputLongTouchTimeLow + (parameter.inputLongTouchTimeHigh - parameter.inputLongTouchTimeLow) * num;
			if (!initTouch && !this.touchInfo.activeAxis && Time.time - this.touchInfo.beginTime >= num2)
			{
				initTouch = true;
				enableTouch = true;
				touchAimAxisTime = -1f;
				self.SetEnableTap(enable: true);
				if (self.targetFieldGimmickCannon is FieldGimmickCannonSpecial)
				{
					self.StartCannonCharge();
				}
			}
			else if (initTouch && checkTouch && (!this.touchInfo.activeAxis || self.attackMode == Player.ATTACK_MODE.ONE_HAND_SWORD))
			{
				enableTouch = true;
				touchAimAxisTime = -1f;
				self.SetEnableTap(enable: true);
			}
			checkTouch = false;
			if (initTouch)
			{
				bool flag2 = false;
				if (this.touchInfo.id == -1)
				{
					this.touchInfo = null;
					enableTouch = false;
					isInputtedTouch = false;
					flag2 = true;
					self.SetEnableTap(enable: false);
					self.ResetCannonShotAimEuler();
				}
				if (self.IsCarrying())
				{
					return;
				}
				if (self.IsAbleMoveCannon())
				{
					if ((self.targetFieldGimmickCannon is FieldGimmickCannonRapid || self.targetFieldGimmickCannon is FieldGimmickCannonField) && !self.targetFieldGimmickCannon.IsCooling())
					{
						Command command = new Command();
						command.type = COMMAND_TYPE.CANNON_SHOT;
						command.fieldGimmickObject = self.targetFieldGimmickCannon;
						SetCommand(command);
					}
					if (this.touchInfo != null && this.touchInfo.activeAxis)
					{
						self.UpdateCannonAimMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
						slideTimer = 0f;
						slideVerocity = Vector3.zero;
					}
					return;
				}
				if (self.IsChargingCannon() || self.IsEnableChangeActionByLongTap())
				{
					return;
				}
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.enable)
				{
					if (self.attackMode == Player.ATTACK_MODE.ARROW)
					{
						if (self.isArrowRainShot)
						{
							flag5 = true;
						}
						else if (StageObjectManager.CanTargetBoss)
						{
							flag3 = true;
						}
						else
						{
							flag4 = true;
						}
					}
					else if (self.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
					{
						flag6 = true;
					}
				}
				if (self.isGuardWalk || self.actionID == (Character.ACTION_ID)19 || self.actionID == (Character.ACTION_ID)20 || self.actionID == (Character.ACTION_ID)34)
				{
					flag = true;
				}
				if (flag3 | flag4 | flag5 | flag6)
				{
					if (flag2)
					{
						CancelInput();
					}
					else
					{
						if (enableTouch)
						{
							Command command2 = new Command();
							command2.type = (flag6 ? COMMAND_TYPE.SPECIAL_ACTION : COMMAND_TYPE.ATTACK);
							command2.aimKeep = (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimBossSettings.enableAimKeep && flag3);
							command2.isTouchOn = true;
							SetCommand(command2);
						}
						if (self.isArrowAimable)
						{
							bool flag7 = false;
							if (touchAimAxisTime >= 0f)
							{
								touchAimAxisTime += Time.deltaTime;
								if (touchAimAxisTime >= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.aimDelayTime)
								{
									flag7 = true;
								}
							}
							if (activeAxis && this.touchInfo != null && this.touchInfo.axisNoLimit.magnitude >= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.aimDragLength && touchAimAxisTime < 0f)
							{
								touchAimAxisTime = 0f;
							}
							if (flag3)
							{
								if (!self.isArrowAimBossMode)
								{
									if (MonoBehaviourSingleton<InGameCameraManager>.I.arrowCameraMode == 0)
									{
										if (flag7)
										{
											self.SetArrowAimBossMode(enable: true);
										}
									}
									else
									{
										self.SetArrowAimBossMode(enable: true);
									}
								}
								if (self.isArrowAimBossMode && !self.isArrowAimEnd)
								{
									self.UpdateArrowAimBossMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
								}
								if (self.isArrowAimLesserMode)
								{
									self.SetArrowAimLesserMode(enable: false);
								}
							}
							else
							{
								if (!self.isArrowAimLesserMode && (flag7 | flag6))
								{
									self.SetArrowAimLesserMode(enable: true);
								}
								if (self.isArrowAimLesserMode && !self.isArrowAimEnd)
								{
									self.UpdateArrowAimLesserMode(this.touchInfo.axisNoLimit);
								}
							}
						}
					}
					slideTimer = 0f;
					slideVerocity = Vector3.zero;
					return;
				}
				if (self.enabledOraclePairSwordsSP && stickInfo != null)
				{
					self.targetingPointList.Clear();
					Vector3 stickVec = GetStickVec(stickInfo.axis, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
					character.SetLerpRotation(stickVec);
					if (self.playerSender != null)
					{
						self.playerSender.OnSyncPosition();
					}
					return;
				}
				if ((!activeAxis || isInputtedTouch) | flag)
				{
					if (self.IsOnCannonMode())
					{
						self.ResetCannonShotAimEuler();
						return;
					}
					if (flag2)
					{
						if (self.actionID != (Character.ACTION_ID)21)
						{
							CancelInput();
							if (flag)
							{
								self.ActIdle((self.actionID != Character.ACTION_ID.MOVE) ? true : false);
							}
						}
						return;
					}
					if (enableTouch)
					{
						bool flag8 = false;
						if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.enable && self.IsExistSpecialAction())
						{
							flag8 = true;
						}
						if (flag8)
						{
							bool flag9 = self.actionID == (Character.ACTION_ID)21;
							if ((!self.isActSpecialAction || !self.isControllable) && !flag9)
							{
								Command command3 = new Command();
								command3.type = COMMAND_TYPE.SPECIAL_ACTION;
								if (self.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && self.IsActionFromAvoid() && self.enableSpAttackContinue)
								{
									command3.type = COMMAND_TYPE.ATTACK;
								}
								command3.isTouchOn = true;
								SetCommand(command3);
							}
						}
						else
						{
							InputAttack(is_touch_on: true);
						}
						if (!flag)
						{
							return;
						}
					}
				}
			}
			else
			{
				if (this.touchInfo.id > -1 && self.IsAbleMoveCannon())
				{
					if (this.touchInfo.activeAxis)
					{
						self.UpdateCannonAimMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
						slideTimer = 0f;
						slideVerocity = Vector3.zero;
					}
					return;
				}
				if (this.touchInfo.activeAxis || this.touchInfo.id == -1)
				{
					this.touchInfo = null;
					self.ResetCannonShotAimEuler();
				}
			}
		}
		if (!flag && nextCommand != null)
		{
			if (CheckCommand(nextCommand))
			{
				ActCommand(nextCommand);
				nextCommand = null;
				return;
			}
			if (nextCommand.deltaTime >= parameter.inputCommandValidTime[(int)nextCommand.type])
			{
				nextCommand = null;
			}
		}
		Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
		bool flag10 = false;
		if (stickInfo != null)
		{
			Vector2 axis = stickInfo.axis;
			float magnitude = axis.magnitude;
			float num3 = 0f;
			if (flag)
			{
				num3 = parameter.guardMoveThreshold;
			}
			if (Time.time - stickInfo.beginTime >= parameter.inputMoveStartTime)
			{
				if (touchInfo != stickInfo && IsCancelAble(null))
				{
					CancelInput();
				}
				moveStickInfo = stickInfo;
				if (magnitude > num3 && self.IsChangeableAction(Character.ACTION_ID.MOVE))
				{
					Vector3 right = cameraTransform.right;
					Vector3 forward = cameraTransform.forward;
					forward.y = 0f;
					forward.Normalize();
					if (parameter.alwaysTopSpeed)
					{
						axis.Normalize();
					}
					Vector3 vector = (!(self.actionTarget != null)) ? (right * axis.x * parameter.moveForwardSpeed + forward * axis.y * parameter.moveForwardSpeed) : (right * axis.x * parameter.moveSideSpeed + forward * axis.y * parameter.moveForwardSpeed);
					if (flag)
					{
						self.ActGuardWalk(parameter.enableRootMotion ? Vector3.zero : vector, parameter.guardMoveSyncSpeed, vector.normalized);
						vector = forward;
					}
					else if (self.IsCarrying())
					{
						self.ActCarryWalk(parameter.enableRootMotion ? Vector3.zero : vector, parameter.moveForwardSpeed, vector.normalized);
						vector = forward;
					}
					else
					{
						bool flag11 = parameter.enableRootMotion;
						if (IsValidSlideProc())
						{
							flag11 = false;
							slideSlowTimer += Time.deltaTime;
							float t = Mathf.Clamp01(slideSlowTimer / GetParamNeedTimeToMaxTime());
							vector *= Mathf.Lerp(0f, 1f, t);
						}
						Character.MOTION_ID motion_id = Character.MOTION_ID.WALK;
						character.ActMoveVelocity(flag11 ? Vector3.zero : vector, parameter.moveForwardSpeed, motion_id);
						character.SetLerpRotation(vector);
						slideTimer = 0f;
						slideVerocity = vector;
					}
					InGameTutorialManager component = MonoBehaviourSingleton<AppMain>.I.GetComponent<InGameTutorialManager>();
					if (vector.sqrMagnitude > 0f && component != null)
					{
						component.TutorialMoveTime += Time.deltaTime;
					}
					flag10 = true;
				}
			}
		}
		if (!flag10)
		{
			slideSlowTimer = 0f;
			if (character.actionID == Character.ACTION_ID.MOVE)
			{
				if (flag)
				{
					self.ActSpecialAction(start_effect: false);
				}
				else if (self.IsCarrying())
				{
					self.ActCarryIdle();
				}
				else
				{
					character.ActIdle();
				}
			}
			if (character.actionID == Character.ACTION_ID.MAX && !((Player)character).enableCancelToAttack)
			{
				slideVerocity = base.transform.forward * GetParamAvoidSpeed();
				slideTimer = 0f;
			}
			else
			{
				float paramSlideTime = GetParamSlideTime();
				if (IsValidSlideProc() && slideVerocity.sqrMagnitude > 0.1f && slideTimer < paramSlideTime)
				{
					slideTimer += Time.deltaTime;
					float num4 = slideTimer / paramSlideTime - 1f;
					Vector3 vector2 = -slideVerocity * (num4 * num4 * num4 + 1f) + slideVerocity;
					character.ActMoveInertia(ref vector2);
				}
				else
				{
					slideVerocity = Vector3.zero;
					slideTimer = 0f;
				}
			}
		}
		if (self.attackMode == Player.ATTACK_MODE.ARROW && moveStickInfo != null && moveStickInfo.axis != Vector2.zero)
		{
			self.SetArrowAimBossModeStartSign(moveStickInfo.axis);
		}
	}

	protected Vector3 GetStickVec(Vector2 stick_vec, Transform camera_transform)
	{
		Vector3 right = camera_transform.right;
		Vector3 forward = camera_transform.forward;
		forward.y = 0f;
		forward.Normalize();
		if (parameter.alwaysTopSpeed)
		{
			stick_vec.Normalize();
		}
		if (self.actionTarget != null)
		{
			return right * stick_vec.x * parameter.moveSideSpeed + forward * stick_vec.y * parameter.moveForwardSpeed;
		}
		return right * stick_vec.x * parameter.moveForwardSpeed + forward * stick_vec.y * parameter.moveForwardSpeed;
	}

	private float GetParamNeedTimeToMaxTime()
	{
		if (character.IsHittingIceFloor())
		{
			return parameter.needTimeToMaxTimeOnIce * (1f - character.buffParam.GetPassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
		}
		if (character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE))
		{
			return debuffParam.slideIceParam.needTimeToMaxTime * (1f - character.buffParam.GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
		}
		return debuffParam.slideParam.needTimeToMaxTime;
	}

	private float GetParamSlideTime()
	{
		if (character.IsHittingIceFloor())
		{
			return parameter.slideTime * (1f - character.buffParam.GetPassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
		}
		if (character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE))
		{
			return debuffParam.slideIceParam.slideTime * (1f - character.buffParam.GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
		}
		return debuffParam.slideParam.slideTime;
	}

	private float GetParamAvoidSpeed()
	{
		if (character.IsHittingIceFloor())
		{
			return parameter.avoidSpeedOnIce * (1f - character.buffParam.GetPassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
		}
		if (character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE))
		{
			return debuffParam.slideIceParam.avoidSpeed * (1f - character.buffParam.GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
		}
		return debuffParam.slideParam.avoidSpeed;
	}

	private bool IsValidSlideProc()
	{
		bool num = character.IsHittingIceFloor() || character.IsValidBuff(BuffParam.BUFFTYPE.SLIDE) || character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE);
		bool flag = self.thsCtrl.IsActiveIai() || self.snatchCtrl.IsMove() || self.snatchCtrl.IsMoveLoop();
		if (num && !flag)
		{
			return !character.isDead;
		}
		return false;
	}

	public bool OnSkillButtonPress(int skill_index)
	{
		if (nextCommand != null && nextCommand.type == COMMAND_TYPE.SKILL && nextCommand.skillIndex == skill_index)
		{
			return false;
		}
		if (!self.IsActSkillAction(skill_index))
		{
			return false;
		}
		Command command = new Command();
		command.type = COMMAND_TYPE.SKILL;
		command.skillIndex = skill_index;
		SetCommand(command);
		return true;
	}

	public bool OnWeaponChangeButtonPress(int weapon_index)
	{
		if (nextCommand != null && nextCommand.type == COMMAND_TYPE.CHANGE_WEAPON && nextCommand.weaponIndex == weapon_index)
		{
			return false;
		}
		if (weapon_index < 0 || self.equipWeaponList.Count <= weapon_index)
		{
			return false;
		}
		if (self.equipWeaponList[weapon_index] == null)
		{
			return false;
		}
		if (self.IsCarrying() || self.actionID == (Character.ACTION_ID)44)
		{
			return false;
		}
		Command command = new Command();
		command.type = COMMAND_TYPE.CHANGE_WEAPON;
		command.weaponIndex = weapon_index;
		SetCommand(command);
		return true;
	}

	public bool OnReserveBurstReloadMotion()
	{
		if (nextCommand != null && nextCommand.type == COMMAND_TYPE.THS_BURST_RELOAD)
		{
			return false;
		}
		Command command = new Command();
		command.type = COMMAND_TYPE.THS_BURST_RELOAD;
		command.MotionId = self.playerParameter.twoHandSwordActionInfo.burstTHSInfo.FirstReloadActionAttackID;
		SetCommand(command);
		return true;
	}

	public override void OnActReaction()
	{
		base.OnActReaction();
		CancelInput();
		self.CancelCannonMode();
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByPlayer();
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetPositionByPlayer();
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearCameraMode();
		}
	}
}
