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
		BINGO
	}

	public class Command
	{
		public COMMAND_TYPE type = COMMAND_TYPE.NONE;

		public Vector2 inputVec = Vector2.get_zero();

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

	private static readonly float QUARTER_PI = 0.7853982f;

	private static readonly float THREE_QUARTER_PI = 2.3561945f;

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
				self.SetEnableTap(false);
			}
			if (self.isGuardWalk || self.actionID == (Character.ACTION_ID)18)
			{
				self.ActIdle(true, -1f);
			}
			moveStickInfo = null;
			lastActCommandType = COMMAND_TYPE.NONE;
			lastActCommandTime = -1f;
			base.OnChangeEnableControll(enable);
		}
	}

	public void SetCommand(Command command)
	{
		if (command != null && command.type != COMMAND_TYPE.NONE && IsCancelAble(command) && (lastActCommandType == COMMAND_TYPE.NONE || command.type != lastActCommandType || !(Time.get_time() - lastActCommandTime < parameter.inputCommandIntervalTime[(int)command.type])))
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
			return self.IsChangeableAction((Character.ACTION_ID)21);
		case COMMAND_TYPE.SPECIAL_ACTION:
			return self.IsChangeableAction((Character.ACTION_ID)32);
		case COMMAND_TYPE.CHANGE_WEAPON:
			return self.IsChangeableAction((Character.ACTION_ID)26);
		case COMMAND_TYPE.GATHER:
			return self.IsChangeableAction((Character.ACTION_ID)27);
		case COMMAND_TYPE.CANNON_STANDBY:
			return self.IsChangeableAction((Character.ACTION_ID)30);
		case COMMAND_TYPE.CANNON_SHOT:
			return self.IsChangeableAction((Character.ACTION_ID)31);
		case COMMAND_TYPE.SONAR:
			return self.IsChangeableAction((Character.ACTION_ID)34);
		case COMMAND_TYPE.WARP:
			return self.IsChangeableAction((Character.ACTION_ID)35);
		case COMMAND_TYPE.EVOLVE:
			return self.IsChangeableAction((Character.ACTION_ID)36);
		case COMMAND_TYPE.EVOLVE_SPECIAL:
			return self.IsChangeableAction((Character.ACTION_ID)37);
		case COMMAND_TYPE.READ_STORY:
			return self.IsChangeableAction((Character.ACTION_ID)38);
		case COMMAND_TYPE.THS_BURST_RELOAD:
			return self.IsChangeableAction(Character.ACTION_ID.ATTACK, command.MotionId);
		case COMMAND_TYPE.GATHER_GIMMICK:
			return self.IsChangeableAction((Character.ACTION_ID)39);
		default:
			return true;
		}
	}

	public void ActCommand(Command command)
	{
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		string _motionLayerName = "Base Layer.";
		switch (command.type)
		{
		case COMMAND_TYPE.ATTACK:
			if (command.aimKeep)
			{
				self.SetArrowAimKeep();
			}
			if (base.character is Player)
			{
				Player player = base.character as Player;
				if (player != null && player.HitSpearSpecialAction)
				{
					base.character.ActAttack(10, true, false, string.Empty);
					break;
				}
			}
			if (!self.CheckAvoidAttack() && !self.ActSpAttackContinue() && !self.CheckAttackNext())
			{
				if (self.IsAbleArrowSitShot())
				{
					if (self.IsActionFromAvoid())
					{
						self.ActAttack(98, true, false, string.Empty);
					}
					else
					{
						self.ActAttack(97, true, false, string.Empty);
					}
				}
				else
				{
					_motionLayerName = "Base Layer.";
					num = self.GetNormalAttackId(self.attackMode, self.spAttackType, self.extraAttackType, out _motionLayerName);
					Character character = base.character;
					string motionLayerName2 = _motionLayerName;
					character.ActAttack(num, true, false, motionLayerName2);
				}
			}
			break;
		case COMMAND_TYPE.AVOID:
		{
			Transform cameraTransform2 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right2 = cameraTransform2.get_right();
			Vector3 forward2 = cameraTransform2.get_forward();
			forward2.y = 0f;
			forward2.Normalize();
			Vector3 val2 = this.get_transform().get_position() + (right2 * command.inputVec.x + forward2 * command.inputVec.y);
			this.get_transform().LookAt(val2);
			self.ActAvoid();
			slideVerocity = Vector3.get_zero();
			slideTimer = 0f;
			break;
		}
		case COMMAND_TYPE.WARP:
		{
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Vector3 right = cameraTransform.get_right();
			Vector3 forward = cameraTransform.get_forward();
			forward.y = 0f;
			forward.Normalize();
			Vector3 val = this.get_transform().get_position() + (right * command.inputVec.x + forward * command.inputVec.y);
			this.get_transform().LookAt(val);
			self.ActWarp();
			slideVerocity = Vector3.get_zero();
			slideTimer = 0f;
			break;
		}
		case COMMAND_TYPE.SKILL:
			self.ActSkillAction(command.skillIndex, false);
			break;
		case COMMAND_TYPE.SPECIAL_ACTION:
			if (!self.pairSwordsCtrl.IsAbleToAlterSpAction())
			{
				self.ActSpecialAction(false, false);
			}
			else
			{
				self.ActSpecialAction(true, true);
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
			_motionLayerName = self.GetMotionLayerName(self.attackMode, self.spAttackType, command.MotionId);
			base.character.ActAttack(_motionLayerName: _motionLayerName, id: command.MotionId, send_packet: true, sync_immediately: false);
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
		lastActCommandTime = Time.get_time();
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
		if (IsEnableControll() && !(character == null) && !character.isLoading)
		{
			if (self.isStunnedLoop)
			{
				CancelInput();
				self.ReduceStunnedTime();
			}
			else if (self.actionID == (Character.ACTION_ID)15)
			{
				CancelInput();
				self.InputBlowClear();
			}
			else if (self.targetFieldGimmickCannon != null && self.targetFieldGimmickCannon is IFieldGimmickCannon)
			{
				if (self.cannonState == Player.CANNON_STATE.READY)
				{
					Command command = new Command();
					command.type = COMMAND_TYPE.CANNON_SHOT;
					command.fieldGimmickObject = self.targetFieldGimmickCannon;
					SetCommand(command);
				}
			}
			else
			{
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
					if (self.nearFieldGimmick is FieldGatherGimmickObject)
					{
						FieldGatherGimmickObject fieldGatherGimmickObject = self.nearFieldGimmick as FieldGatherGimmickObject;
						if (fieldGatherGimmickObject != null && fieldGatherGimmickObject.canUse() && self.fishingCtrl != null && !self.fishingCtrl.IsFishing())
						{
							Command command3 = new Command();
							command3.type = COMMAND_TYPE.GATHER_GIMMICK;
							command3.fieldGimmickObject = self.nearFieldGimmick;
							SetCommand(command3);
							return;
						}
					}
					if (self.nearFieldGimmick is FieldBingoObject)
					{
						FieldBingoObject fieldBingoObject = self.nearFieldGimmick as FieldBingoObject;
						if (fieldBingoObject != null)
						{
							Command command4 = new Command();
							command4.type = COMMAND_TYPE.BINGO;
							command4.fieldGimmickObject = self.nearFieldGimmick;
							SetCommand(command4);
							return;
						}
					}
				}
				if (self.cannonState == Player.CANNON_STATE.NONE && !self.spearCtrl.IsInputedSpAttackContinue())
				{
					if (self.fishingCtrl != null && self.fishingCtrl.IsFishing())
					{
						self.fishingCtrl.Tap();
					}
					else
					{
						self.SetFlickDirection(FLICK_DIRECTION.FRONT);
						self.OnTap();
						if (self.nearGatherPoint != null)
						{
							Command command5 = new Command();
							command5.type = COMMAND_TYPE.GATHER;
							command5.gatherPoint = self.nearGatherPoint;
							SetCommand(command5);
						}
						InputAttack(false);
					}
				}
			}
		}
	}

	private void SetCommandForFieldGimmick<T>(COMMAND_TYPE cmdType) where T : FieldGimmickObject
	{
		T val = self.nearFieldGimmick as T;
		if (!(val == null))
		{
			Command command = new Command();
			command.type = cmdType;
			command.fieldGimmickObject = self.nearFieldGimmick;
			SetCommand(command);
		}
	}

	private void InputAttack(bool is_touch_on = false)
	{
		if (self.controllerInputCombo && (character.actionID == Character.ACTION_ID.ATTACK || character.actionID == (Character.ACTION_ID)20))
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
		if (IsEnableControll() && character == null)
		{
			return;
		}
	}

	private void OnFlick(Vector2 flick_vec)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		if (IsEnableControll() && !(character == null) && !character.isLoading)
		{
			if (self.isStunnedLoop)
			{
				CancelInput();
				self.ReduceStunnedTime();
			}
			else if (self.IsRestraint())
			{
				CancelInput();
				self.ReduceRestraintTime();
			}
			else
			{
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
					else if (self.CanEvolveSpecialFlickAction())
					{
						command.type = COMMAND_TYPE.EVOLVE_SPECIAL;
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
					slideVerocity = Vector3.get_zero();
				}
			}
		}
	}

	private void SetFlickDirection(Vector2 flick_vec)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (!(flick_vec == Vector2.get_zero()))
		{
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
	}

	private void OnTouchOff(InputManager.TouchInfo touch_info)
	{
		if (!IsEnableControll())
		{
			return;
		}
	}

	protected override void Update()
	{
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		InputManager.TouchInfo touchInfo = moveStickInfo;
		moveStickInfo = null;
		if ((self.actionID == (Character.ACTION_ID)19 || self.actionID == (Character.ACTION_ID)33 || self.actionID == (Character.ACTION_ID)20) && isInputtedTouch)
		{
			checkTouch = true;
		}
		if (nextCommand != null && (self == null || !self.IsHitStop()))
		{
			nextCommand.deltaTime += Time.get_deltaTime();
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionType().IsDialog())
		{
			SetEnableControll(false, DISABLE_FLAG.INPUT_DISABLE);
		}
		else
		{
			SetEnableControll(true, DISABLE_FLAG.INPUT_DISABLE);
		}
		if (IsEnableControll() && !(character == null) && !character.isLoading)
		{
			base.Update();
			InputManager i = MonoBehaviourSingleton<InputManager>.I;
			InputManager.TouchInfo stickInfo = i.GetStickInfo();
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
				if (!initTouch && !this.touchInfo.activeAxis && Time.get_time() - this.touchInfo.beginTime >= num2)
				{
					initTouch = true;
					enableTouch = true;
					touchAimAxisTime = -1f;
					self.SetEnableTap(true);
					if (self.targetFieldGimmickCannon is FieldGimmickCannonSpecial)
					{
						self.StartCannonCharge();
					}
				}
				else if (initTouch && checkTouch && (!this.touchInfo.activeAxis || self.attackMode == Player.ATTACK_MODE.ONE_HAND_SWORD))
				{
					enableTouch = true;
					touchAimAxisTime = -1f;
					self.SetEnableTap(true);
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
						self.SetEnableTap(false);
						self.ResetCannonShotAimEuler();
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
							slideVerocity = Vector3.get_zero();
						}
						return;
					}
					if (self.IsChargingCannon())
					{
						return;
					}
					if (self.IsEnableChangeActionByLongTap())
					{
						return;
					}
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.enable)
					{
						if (self.attackMode == Player.ATTACK_MODE.ARROW)
						{
							if (MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
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
							flag5 = true;
						}
					}
					if (self.isGuardWalk || self.actionID == (Character.ACTION_ID)18 || self.actionID == (Character.ACTION_ID)19 || self.actionID == (Character.ACTION_ID)33)
					{
						flag = true;
					}
					if (flag3 || flag4 || flag5)
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
								command2.type = (flag5 ? COMMAND_TYPE.SPECIAL_ACTION : COMMAND_TYPE.ATTACK);
								command2.aimKeep = (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimBossSettings.enableAimKeep && flag3);
								command2.isTouchOn = true;
								SetCommand(command2);
							}
							if (self.isArrowAimable)
							{
								bool flag6 = false;
								if (touchAimAxisTime >= 0f)
								{
									touchAimAxisTime += Time.get_deltaTime();
									if (touchAimAxisTime >= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.aimDelayTime)
									{
										flag6 = true;
									}
								}
								if (activeAxis && this.touchInfo != null && this.touchInfo.axisNoLimit.get_magnitude() >= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.aimDragLength && touchAimAxisTime < 0f)
								{
									touchAimAxisTime = 0f;
								}
								if (flag3)
								{
									if (!self.isArrowAimBossMode)
									{
										if (MonoBehaviourSingleton<InGameCameraManager>.I.arrowCameraMode == 0)
										{
											if (flag6)
											{
												self.SetArrowAimBossMode(true);
											}
										}
										else
										{
											self.SetArrowAimBossMode(true);
										}
									}
									if (self.isArrowAimBossMode && !self.isArrowAimEnd)
									{
										self.UpdateArrowAimBossMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
									}
								}
								else
								{
									if (!self.isArrowAimLesserMode && (flag6 || flag5))
									{
										self.SetArrowAimLesserMode(true);
									}
									if (self.isArrowAimLesserMode && !self.isArrowAimEnd)
									{
										self.UpdateArrowAimLesserMode(this.touchInfo.axisNoLimit);
									}
								}
							}
						}
						slideTimer = 0f;
						slideVerocity = Vector3.get_zero();
						return;
					}
					if (!activeAxis || isInputtedTouch || flag)
					{
						if (self.IsOnCannonMode())
						{
							self.ResetCannonShotAimEuler();
							return;
						}
						if (flag2)
						{
							if (self.actionID != (Character.ACTION_ID)20)
							{
								CancelInput();
								if (flag)
								{
									self.ActIdle((self.actionID != Character.ACTION_ID.MOVE) ? true : false, -1f);
								}
							}
							return;
						}
						if (enableTouch)
						{
							bool flag7 = false;
							if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.enable && self.IsExistSpecialAction())
							{
								flag7 = true;
							}
							if (flag7)
							{
								bool flag8 = self.actionID == (Character.ACTION_ID)20;
								if ((!self.isActSpecialAction || !self.isControllable) && !flag8)
								{
									Command command3 = new Command();
									command3.type = COMMAND_TYPE.SPECIAL_ACTION;
									command3.isTouchOn = true;
									SetCommand(command3);
								}
							}
							else
							{
								InputAttack(true);
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
							slideVerocity = Vector3.get_zero();
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
			bool flag9 = false;
			if (stickInfo != null)
			{
				Vector2 axis = stickInfo.axis;
				float magnitude = axis.get_magnitude();
				float num3 = 0f;
				if (flag)
				{
					num3 = parameter.guardMoveThreshold;
				}
				if (Time.get_time() - stickInfo.beginTime >= parameter.inputMoveStartTime)
				{
					if (touchInfo != stickInfo && IsCancelAble(null))
					{
						CancelInput();
					}
					moveStickInfo = stickInfo;
					if (magnitude > num3 && self.IsChangeableAction(Character.ACTION_ID.MOVE))
					{
						Vector3 right = cameraTransform.get_right();
						Vector3 forward = cameraTransform.get_forward();
						forward.y = 0f;
						forward.Normalize();
						if (parameter.alwaysTopSpeed)
						{
							axis.Normalize();
						}
						Vector3 val = (!(self.actionTarget != null)) ? (right * axis.x * parameter.moveForwardSpeed + forward * axis.y * parameter.moveForwardSpeed) : (right * axis.x * parameter.moveSideSpeed + forward * axis.y * parameter.moveForwardSpeed);
						if (flag)
						{
							self.ActGuardWalk((!parameter.enableRootMotion) ? val : Vector3.get_zero(), parameter.guardMoveSyncSpeed, val.get_normalized());
							val = forward;
						}
						else
						{
							bool flag10 = parameter.enableRootMotion;
							if (IsValidSlideProc())
							{
								flag10 = false;
								slideSlowTimer += Time.get_deltaTime();
								float num4 = Mathf.Clamp01(slideSlowTimer / GetParamNeedTimeToMaxTime());
								val *= Mathf.Lerp(0f, 1f, num4);
							}
							Character.MOTION_ID motion_id = Character.MOTION_ID.WALK;
							character.ActMoveVelocity((!flag10) ? val : Vector3.get_zero(), parameter.moveForwardSpeed, motion_id);
							character.SetLerpRotation(val);
							slideTimer = 0f;
							slideVerocity = val;
						}
						InGameTutorialManager component = MonoBehaviourSingleton<AppMain>.I.GetComponent<InGameTutorialManager>();
						if (val.get_sqrMagnitude() > 0f && component != null)
						{
							component.TutorialMoveTime += Time.get_deltaTime();
						}
						flag9 = true;
					}
				}
			}
			if (!flag9)
			{
				slideSlowTimer = 0f;
				if (character.actionID == Character.ACTION_ID.MOVE)
				{
					if (flag)
					{
						self.ActSpecialAction(false, true);
					}
					else
					{
						character.ActIdle(false, -1f);
					}
				}
				if (character.actionID == Character.ACTION_ID.MAX && !((Player)character).enableCancelToAttack)
				{
					slideVerocity = this.get_transform().get_forward() * GetParamAvoidSpeed();
					slideTimer = 0f;
				}
				else
				{
					float paramSlideTime = GetParamSlideTime();
					if (IsValidSlideProc() && slideVerocity.get_sqrMagnitude() > 0.1f && slideTimer < paramSlideTime)
					{
						slideTimer += Time.get_deltaTime();
						float num5 = slideTimer / paramSlideTime - 1f;
						Vector3 val2 = -slideVerocity * (num5 * num5 * num5 + 1f) + slideVerocity;
						character.ActMoveInertia(ref val2);
					}
					else
					{
						slideVerocity = Vector3.get_zero();
						slideTimer = 0f;
					}
				}
			}
			if (self.attackMode == Player.ATTACK_MODE.ARROW && moveStickInfo != null && moveStickInfo.axis != Vector2.get_zero())
			{
				self.SetArrowAimBossModeStartSign(moveStickInfo.axis);
			}
		}
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
		bool flag = character.IsHittingIceFloor() || character.IsValidBuff(BuffParam.BUFFTYPE.SLIDE) || character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE);
		bool flag2 = self.thsCtrl.IsActiveIai() || self.snatchCtrl.IsMove() || self.snatchCtrl.IsMoveLoop();
		return flag && !flag2 && !character.isDead;
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
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearStopCameraMode();
		}
	}
}