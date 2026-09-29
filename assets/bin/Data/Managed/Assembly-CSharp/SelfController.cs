// Decompiled with JetBrains decompiler
// Type: SelfController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SelfController : ControllerBase
{
  protected InputManager.TouchInfo touchInfo;
  protected bool initTouch;
  protected bool enableTouch;
  protected bool isInputtedTouch;
  protected bool checkTouch;
  protected float touchAimAxisTime = -1f;
  protected SelfController.COMMAND_TYPE lastActCommandType = SelfController.COMMAND_TYPE.NONE;
  protected float lastActCommandTime = -1f;
  protected InputManager.TouchInfo moveStickInfo;
  private SelfController.FLICK_DIRECTION flickDirection = SelfController.FLICK_DIRECTION.FRONT;
  private static readonly float QUARTER_PI = 0.7853982f;
  private static readonly float THREE_QUARTER_PI = 2.3561945f;
  private Vector3 slideVerocity;
  private float slideTimer;
  private float slideSlowTimer;

  public InGameSettingsManager.SelfController parameter { get; private set; }

  public Self self { get; private set; }

  private InGameSettingsManager.DebuffParam debuffParam { get; set; }

  public SelfController.Command nextCommand { get; protected set; }

  public SelfController() => this.nextCommand = (SelfController.Command) null;

  private void OnDestroy()
  {
    InputManager.OnTouchOn -= new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTap -= new InputManager.OnTouchDelegate(this.OnTap);
    InputManager.OnFlick -= new InputManager.OnFlickDelegate(this.OnFlick);
    InputManager.OnLongTouch -= new InputManager.OnTouchDelegate(this.OnLongTouch);
    InputManager.OnTouchOff -= new InputManager.OnTouchDelegate(this.OnTouchOff);
  }

  protected override void Awake() => base.Awake();

  protected override void Start()
  {
    base.Start();
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController;
    this.debuffParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff;
    this.self = this.character as Self;
    InputManager.OnTouchOn += new InputManager.OnTouchDelegate(this.OnTouchOn);
    InputManager.OnTap += new InputManager.OnTouchDelegate(this.OnTap);
    InputManager.OnFlick += new InputManager.OnFlickDelegate(this.OnFlick);
    InputManager.OnLongTouch += new InputManager.OnTouchDelegate(this.OnLongTouch);
    InputManager.OnTouchOff += new InputManager.OnTouchDelegate(this.OnTouchOff);
  }

  public override void OnChangeEnableControll(bool enable)
  {
    if (Object.op_Equality((Object) this.self, (Object) null))
      return;
    this.CancelInput();
    if (this.touchInfo != null && this.initTouch)
    {
      this.touchInfo = (InputManager.TouchInfo) null;
      this.enableTouch = false;
      this.isInputtedTouch = false;
      this.self.SetEnableTap(false);
    }
    if (this.self.isGuardWalk || this.self.actionID == (Character.ACTION_ID) 19)
      this.self.ActIdle(true, -1f);
    this.moveStickInfo = (InputManager.TouchInfo) null;
    this.lastActCommandType = SelfController.COMMAND_TYPE.NONE;
    this.lastActCommandTime = -1f;
    base.OnChangeEnableControll(enable);
  }

  public void SetCommand(SelfController.Command command)
  {
    if (command == null || command.type == SelfController.COMMAND_TYPE.NONE || !this.IsCancelAble(command) || this.lastActCommandType != SelfController.COMMAND_TYPE.NONE && command.type == this.lastActCommandType && (double) Time.time - (double) this.lastActCommandTime < (double) this.parameter.inputCommandIntervalTime[(int) command.type])
      return;
    this.lastActCommandType = SelfController.COMMAND_TYPE.NONE;
    this.lastActCommandTime = -1f;
    this.CancelInput();
    if (this.CheckCommand(command))
      this.ActCommand(command);
    else
      this.nextCommand = command;
  }

  public void CancelInput()
  {
    this.nextCommand = (SelfController.Command) null;
    this.self.CancelAttackCombo();
  }

  public bool CheckCommand(SelfController.Command command)
  {
    switch (command.type)
    {
      case SelfController.COMMAND_TYPE.ATTACK:
        return this.self.IsChangeableAction(Character.ACTION_ID.ATTACK);
      case SelfController.COMMAND_TYPE.AVOID:
        return this.self.IsChangeableAction(Character.ACTION_ID.MAX);
      case SelfController.COMMAND_TYPE.SKILL:
        return this.CheckCommandWithSkillTableData(command) || this.self.IsChangeableAction((Character.ACTION_ID) 22);
      case SelfController.COMMAND_TYPE.SPECIAL_ACTION:
        return this.self.IsChangeableAction((Character.ACTION_ID) 33);
      case SelfController.COMMAND_TYPE.CHANGE_WEAPON:
        return this.self.IsChangeableAction((Character.ACTION_ID) 27);
      case SelfController.COMMAND_TYPE.GATHER:
        return this.self.IsChangeableAction((Character.ACTION_ID) 28);
      case SelfController.COMMAND_TYPE.CANNON_STANDBY:
        return this.self.IsChangeableAction((Character.ACTION_ID) 31 /*0x1F*/);
      case SelfController.COMMAND_TYPE.CANNON_SHOT:
        return this.self.IsChangeableAction((Character.ACTION_ID) 32 /*0x20*/);
      case SelfController.COMMAND_TYPE.SONAR:
        return this.self.IsChangeableAction((Character.ACTION_ID) 35);
      case SelfController.COMMAND_TYPE.WARP:
        return this.self.IsChangeableAction((Character.ACTION_ID) 36);
      case SelfController.COMMAND_TYPE.EVOLVE:
        return this.self.IsChangeableAction((Character.ACTION_ID) 37);
      case SelfController.COMMAND_TYPE.EVOLVE_SPECIAL:
        return this.self.IsChangeableAction((Character.ACTION_ID) 38);
      case SelfController.COMMAND_TYPE.READ_STORY:
        return this.self.IsChangeableAction((Character.ACTION_ID) 39);
      case SelfController.COMMAND_TYPE.THS_BURST_RELOAD:
        return this.self.IsChangeableAction(Character.ACTION_ID.ATTACK, command.MotionId);
      case SelfController.COMMAND_TYPE.GATHER_GIMMICK:
        return this.self.IsChangeableAction((Character.ACTION_ID) 40);
      case SelfController.COMMAND_TYPE.COOP_FISHING:
        return this.self.IsChangeableAction((Character.ACTION_ID) 41);
      case SelfController.COMMAND_TYPE.FLICK_ACTION:
        return this.self.IsChangeableAction((Character.ACTION_ID) 42);
      case SelfController.COMMAND_TYPE.CARRY:
        return this.self.IsChangeableAction((Character.ACTION_ID) 44);
      case SelfController.COMMAND_TYPE.QUEST_GIMMICK:
        return this.self.IsChangeableAction((Character.ACTION_ID) 28);
      case SelfController.COMMAND_TYPE.TELEPORT_AVOID:
        return this.self.IsChangeableAction((Character.ACTION_ID) 46);
      case SelfController.COMMAND_TYPE.RUSH_AVOID:
        return this.self.IsChangeableAction((Character.ACTION_ID) 49);
      default:
        return true;
    }
  }

  public void ActCommand(SelfController.Command command)
  {
    switch (command.type)
    {
      case SelfController.COMMAND_TYPE.ATTACK:
        if (command.aimKeep)
          this.self.SetArrowAimKeep();
        if (this.character is Player)
        {
          Player character = this.character as Player;
          if (Object.op_Inequality((Object) character, (Object) null) && character.HitSpearSpecialAction)
          {
            this.character.ActAttack(10);
            break;
          }
        }
        if (this.self.spearCtrl.IsEnableBurstCombo())
        {
          this.self.spearCtrl.ActAttackBurstCombo();
          break;
        }
        if (!this.self.CheckAvoidAttack() && !this.self.ActSpAttackContinue() && !this.self.CheckAttackNext())
        {
          if (this.self.IsAbleArrowSitShot())
          {
            if (this.self.IsActionFromAvoid())
            {
              this.self.ActAttack(98, true, false, "", "");
              break;
            }
            this.self.ActAttack(97, true, false, "", "");
            break;
          }
          if (this.self.IsAbleArrowRainShot())
          {
            if (this.self.IsActionFromAvoid())
            {
              this.self.ActAttack(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainShotAttackId, true, false, "", "");
              break;
            }
            this.self.ActAttack(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowReloadRainShotAttackId, true, false, "", "");
            break;
          }
          string _motionLayerName = "Base Layer.";
          this.character.ActAttack(this.self.GetNormalAttackId(this.self.attackMode, this.self.spAttackType, this.self.extraAttackType, out _motionLayerName), _motionLayerName: _motionLayerName);
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.AVOID:
        if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
        {
          Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
          Vector3 right = cameraTransform.right;
          Vector3 forward = cameraTransform.forward;
          forward.y = 0.0f;
          ((Vector3) ref forward).Normalize();
          ((Component) this).transform.LookAt(Vector3.op_Addition(((Component) this).transform.position, Vector3.op_Addition(Vector3.op_Multiply(right, command.inputVec.x), Vector3.op_Multiply(forward, command.inputVec.y))));
          this.self.ActAvoid();
          this.slideVerocity = Vector3.zero;
          this.slideTimer = 0.0f;
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.SKILL:
        this.self.ActSkillAction(command.skillIndex, false);
        break;
      case SelfController.COMMAND_TYPE.SPECIAL_ACTION:
        if (this.self.CheckAttackMode(Player.ATTACK_MODE.PAIR_SWORDS) && !this.self.pairSwordsCtrl.IsAbleToAlterSpAction())
        {
          this.self.ActSpecialAction(false, false);
          break;
        }
        if (!this.self.CheckWeaponActionForSpAction())
        {
          this.self.ActSpecialAction(true, true);
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.CHANGE_WEAPON:
        this.self.ActChangeWeapon(this.self.equipWeaponList[command.weaponIndex], command.weaponIndex);
        break;
      case SelfController.COMMAND_TYPE.GATHER:
        if (Object.op_Equality((Object) this.self.nearGatherPoint, (Object) command.gatherPoint))
        {
          this.self.ActGather(this.self.nearGatherPoint);
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.CANNON_STANDBY:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActCannonStandby(this.self.nearFieldGimmick.GetId());
          if (this.self.nearFieldGimmick is FieldGimmickCannonSpecial)
          {
            this.self.SetCannonBeamMode();
            break;
          }
          this.self.SetCannonAimMode();
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.CANNON_SHOT:
        if (!(this.self.targetFieldGimmickCannon is FieldGimmickCannonSpecial) || this.self.IsCannonFullCharged())
        {
          this.self.ActCannonShot();
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.SONAR:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActSonar(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.WARP:
        Transform cameraTransform1 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
        Vector3 right1 = cameraTransform1.right;
        Vector3 forward1 = cameraTransform1.forward;
        forward1.y = 0.0f;
        ((Vector3) ref forward1).Normalize();
        ((Component) this).transform.LookAt(Vector3.op_Addition(((Component) this).transform.position, Vector3.op_Addition(Vector3.op_Multiply(right1, command.inputVec.x), Vector3.op_Multiply(forward1, command.inputVec.y))));
        this.self.ActWarp();
        this.slideVerocity = Vector3.zero;
        this.slideTimer = 0.0f;
        break;
      case SelfController.COMMAND_TYPE.EVOLVE:
        this.self.ActEvolve();
        break;
      case SelfController.COMMAND_TYPE.EVOLVE_SPECIAL:
        this.self.ActEvolveSpecialAction();
        break;
      case SelfController.COMMAND_TYPE.READ_STORY:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActReadStory(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.THS_BURST_RELOAD:
        string motionLayerName = this.self.GetMotionLayerName(this.self.attackMode, this.self.spAttackType, command.MotionId);
        this.character.ActAttack(command.MotionId, _motionLayerName: motionLayerName);
        break;
      case SelfController.COMMAND_TYPE.GATHER_GIMMICK:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActGatherGimmick(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.BINGO:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActBingo(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.COOP_FISHING:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActCoopFishingStart(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.FLICK_ACTION:
        this.self.ActFlickAction(Vector2.op_Implicit(command.inputVec), true);
        this.slideVerocity = Vector3.zero;
        this.slideTimer = 0.0f;
        break;
      case SelfController.COMMAND_TYPE.CARRY:
        if (this.self.nearFieldGimmick != null)
        {
          InGameProgress.eFieldGimmick type = InGameProgress.eFieldGimmick.CarriableGimmick;
          if (this.self.nearFieldGimmick is FieldSupplyGimmickObject)
            type = InGameProgress.eFieldGimmick.SupplyGimmick;
          this.self.ActCarry(type, this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.PORTAL_GIMMICK:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActPortalGimmick(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.QUEST_GIMMICK:
        if (this.self.nearFieldGimmick == command.fieldGimmickObject)
        {
          this.self.ActQuestGimmick(this.self.nearFieldGimmick.GetId());
          break;
        }
        break;
      case SelfController.COMMAND_TYPE.TELEPORT_AVOID:
        Transform cameraTransform2 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
        Vector3 right2 = cameraTransform2.right;
        Vector3 forward2 = cameraTransform2.forward;
        forward2.y = 0.0f;
        ((Vector3) ref forward2).Normalize();
        ((Component) this).transform.LookAt(Vector3.op_Addition(((Component) this).transform.position, Vector3.op_Addition(Vector3.op_Multiply(right2, command.inputVec.x), Vector3.op_Multiply(forward2, command.inputVec.y))));
        this.self.ActTeleportAvoid();
        this.slideVerocity = Vector3.zero;
        this.slideTimer = 0.0f;
        break;
      case SelfController.COMMAND_TYPE.RUSH_AVOID:
        Transform cameraTransform3 = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
        Vector3 right3 = cameraTransform3.right;
        Vector3 forward3 = cameraTransform3.forward;
        forward3.y = 0.0f;
        ((Vector3) ref forward3).Normalize();
        this.self.ActRushAvoid(Vector3.op_Addition(Vector3.op_Multiply(right3, command.inputVec.x), Vector3.op_Multiply(forward3, command.inputVec.y)));
        this.slideVerocity = Vector3.zero;
        this.slideTimer = 0.0f;
        break;
    }
    if (command.isTouchOn)
    {
      this.enableTouch = false;
      this.isInputtedTouch = true;
    }
    else if (this.isInputtedTouch)
      this.checkTouch = true;
    this.lastActCommandType = command.type;
    this.lastActCommandTime = Time.time;
  }

  public bool IsCancelAble(SelfController.Command check_command) => !this.IsCancelNextNotCancel();

  public bool IsCancelNextNotCancel()
  {
    if (this.nextCommand != null)
    {
      switch (this.nextCommand.type)
      {
        case SelfController.COMMAND_TYPE.SKILL:
          return true;
        case SelfController.COMMAND_TYPE.CHANGE_WEAPON:
          return true;
      }
    }
    return false;
  }

  private void OnTouchOn(InputManager.TouchInfo touch_info)
  {
    if (!this.IsEnableControll() || Object.op_Equality((Object) this.character, (Object) null) || this.character.isLoading || this.touchInfo != null)
      return;
    this.touchInfo = touch_info;
    this.initTouch = false;
    this.enableTouch = false;
    this.isInputtedTouch = false;
    this.checkTouch = false;
  }

  private void OnTap(InputManager.TouchInfo touch_info)
  {
    if (!this.IsEnableControll() || Object.op_Equality((Object) this.character, (Object) null) || this.character.isLoading)
      return;
    if (this.self.isStunnedLoop)
    {
      this.CancelInput();
      this.self.ReduceStunnedTime();
    }
    else if (this.self.actionID == (Character.ACTION_ID) 16 /*0x10*/)
    {
      this.CancelInput();
      this.self.InputBlowClear();
    }
    else if (this.self.IsCarrying())
    {
      if (!this.self.enableCancelToCarryPut)
        return;
      if (this.self.nearFieldGimmick is FieldCarriableGimmickObject && this.self.carryingGimmickObject is FieldCarriableEvolveItemGimmickObject && (this.self.nearFieldGimmick as FieldCarriableGimmickObject).CanEvolve())
        this.self.ActCarryPut(this.self.nearFieldGimmick.GetId());
      else
        this.self.ActCarryPut();
    }
    else if (this.self.targetFieldGimmickCannon != null && this.self.targetFieldGimmickCannon != null)
    {
      if (this.self.cannonState != Player.CANNON_STATE.READY)
        return;
      this.SetCommand(new SelfController.Command()
      {
        type = SelfController.COMMAND_TYPE.CANNON_SHOT,
        fieldGimmickObject = (IFieldGimmickObject) this.self.targetFieldGimmickCannon
      });
    }
    else
    {
      if (this.self.nearFieldGimmick != null)
      {
        if (this.self.nearFieldGimmick is IFieldGimmickCannon && this.self.nearFieldGimmick is IFieldGimmickCannon nearFieldGimmick1 && !nearFieldGimmick1.IsUsing() && nearFieldGimmick1.IsAbleToUse() && this.self.cannonState == Player.CANNON_STATE.NONE)
        {
          this.SetCommand(new SelfController.Command()
          {
            type = SelfController.COMMAND_TYPE.CANNON_STANDBY,
            fieldGimmickObject = this.self.nearFieldGimmick
          });
          return;
        }
        if (this.self.nearFieldGimmick is FieldSonarObject)
        {
          this.SetCommandForFieldGimmick<FieldSonarObject>(SelfController.COMMAND_TYPE.SONAR);
          return;
        }
        if (this.self.nearFieldGimmick is FieldReadStoryObject)
        {
          this.SetCommandForFieldGimmick<FieldReadStoryObject>(SelfController.COMMAND_TYPE.READ_STORY);
          return;
        }
        if (this.self.nearFieldGimmick is FieldChatGimmickObject)
        {
          FieldChatGimmickObject nearFieldGimmick2 = this.self.nearFieldGimmick as FieldChatGimmickObject;
          if (Object.op_Inequality((Object) nearFieldGimmick2, (Object) null) && nearFieldGimmick2.StartChat())
            return;
        }
        if (this.self.nearFieldGimmick is FieldGimmickCoopFishing && this.self.fishingCtrl != null && !this.self.fishingCtrl.IsFishing())
        {
          this.SetCommandForFieldGimmick<FieldGimmickCoopFishing>(SelfController.COMMAND_TYPE.COOP_FISHING);
          return;
        }
        if (this.self.nearFieldGimmick is FieldGatherGimmickObject)
        {
          FieldGatherGimmickObject nearFieldGimmick3 = this.self.nearFieldGimmick as FieldGatherGimmickObject;
          switch (nearFieldGimmick3.GetGatherGimmickType())
          {
            case GATHER_GIMMICK_TYPE.FISHING:
            case GATHER_GIMMICK_TYPE.COOP_FISHING:
              if (Object.op_Inequality((Object) nearFieldGimmick3, (Object) null) && nearFieldGimmick3.CanUse() && this.self.fishingCtrl != null && !this.self.fishingCtrl.IsFishing())
              {
                this.SetCommand(new SelfController.Command()
                {
                  type = SelfController.COMMAND_TYPE.GATHER_GIMMICK,
                  fieldGimmickObject = this.self.nearFieldGimmick
                });
                return;
              }
              break;
          }
        }
        if (this.self.nearFieldGimmick is FieldQuestGimmickObject && Object.op_Inequality((Object) (this.self.nearFieldGimmick as FieldQuestGimmickObject), (Object) null))
        {
          this.SetCommand(new SelfController.Command()
          {
            type = SelfController.COMMAND_TYPE.QUEST_GIMMICK,
            fieldGimmickObject = this.self.nearFieldGimmick
          });
          return;
        }
        if (this.self.nearFieldGimmick is FieldBingoObject)
        {
          this.SetCommandForFieldGimmick<FieldBingoObject>(SelfController.COMMAND_TYPE.BINGO);
          return;
        }
        if (this.self.nearFieldGimmick is FieldCarriableGimmickObject)
        {
          this.SetCommandForFieldGimmick<FieldCarriableGimmickObject>(SelfController.COMMAND_TYPE.CARRY);
          return;
        }
        if (this.self.nearFieldGimmick is FieldSupplyGimmickObject)
        {
          this.SetCommandForFieldGimmick<FieldSupplyGimmickObject>(SelfController.COMMAND_TYPE.CARRY);
          return;
        }
        if (this.self.nearFieldGimmick is FieldPortalGimmickObject)
        {
          this.SetCommandForFieldGimmick<FieldPortalGimmickObject>(SelfController.COMMAND_TYPE.PORTAL_GIMMICK);
          return;
        }
      }
      if (this.self.cannonState != Player.CANNON_STATE.NONE || this.self.spearCtrl.IsInputedSpAttackContinue())
        return;
      if (this.self.fishingCtrl != null && this.self.fishingCtrl.IsFishing())
      {
        this.self.fishingCtrl.Tap();
      }
      else
      {
        this.self.SetFlickDirection(SelfController.FLICK_DIRECTION.FRONT);
        this.self.OnTap();
        if (Object.op_Inequality((Object) this.self.nearGatherPoint, (Object) null))
          this.SetCommand(new SelfController.Command()
          {
            type = SelfController.COMMAND_TYPE.GATHER,
            gatherPoint = this.self.nearGatherPoint
          });
        this.self.InputNextTrigger();
        this.InputAttack();
      }
    }
  }

  private bool CheckCommandWithSkillTableData(SelfController.Command command)
  {
    if (command.type != SelfController.COMMAND_TYPE.SKILL || this.self.actionID != Character.ACTION_ID.IDLE && this.self.actionID != Character.ACTION_ID.MOVE && this.self.actionID != Character.ACTION_ID.ATTACK && this.self.actionID != Character.ACTION_ID.MAX && this.self.actionID != (Character.ACTION_ID) 36 && this.self.actionID != (Character.ACTION_ID) 33 && this.self.actionID != (Character.ACTION_ID) 37 && this.self.actionID != (Character.ACTION_ID) 38 && this.self.actionID != (Character.ACTION_ID) 42 && this.self.actionID != (Character.ACTION_ID) 46 && this.self.actionID != (Character.ACTION_ID) 49)
      return false;
    SkillInfo.SkillParam skillParam = this.self.GetSkillParam(command.skillIndex);
    if (skillParam == null || !skillParam.isValid)
      return false;
    SkillItemTable.SkillItemData tableData = skillParam.tableData;
    return tableData != null && tableData.isTeleportation;
  }

  private void SetCommandForFieldGimmick<T>(SelfController.COMMAND_TYPE cmdType) where T : FieldGimmickObject
  {
    if (Object.op_Equality((Object) (object) (this.self.nearFieldGimmick as T), (Object) null))
      return;
    this.SetCommand(new SelfController.Command()
    {
      type = cmdType,
      fieldGimmickObject = this.self.nearFieldGimmick
    });
  }

  private void InputAttack(bool is_touch_on = false)
  {
    if (this.self.controllerInputCombo && (this.character.actionID == Character.ACTION_ID.ATTACK || this.character.actionID == (Character.ACTION_ID) 21 || this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST) && this.character.actionID == (Character.ACTION_ID) 42))
    {
      this.CancelInput();
      if (!this.self.InputAttackCombo())
        return;
      if (is_touch_on)
      {
        this.enableTouch = false;
        this.isInputtedTouch = true;
      }
      else
      {
        if (!this.isInputtedTouch)
          return;
        this.checkTouch = true;
      }
    }
    else
      this.SetCommand(new SelfController.Command()
      {
        type = SelfController.COMMAND_TYPE.ATTACK,
        isTouchOn = is_touch_on
      });
  }

  private void OnLongTouch(InputManager.TouchInfo touch_info)
  {
    if (!this.IsEnableControll())
      return;
    Object.op_Equality((Object) this.character, (Object) null);
  }

  private void OnFlick(Vector2 flick_vec)
  {
    if (!this.IsEnableControll() || Object.op_Equality((Object) this.character, (Object) null) || this.character.isLoading || this.self.IsCarrying())
      return;
    if (this.self.isStunnedLoop)
    {
      this.CancelInput();
      this.self.ReduceStunnedTime();
    }
    else if (this.self.IsRestraint())
    {
      this.CancelInput();
      this.self.ReduceRestraintTime();
    }
    else
    {
      if (this.self.IsInkSplash())
        this.self.ReduceInkSplashTime();
      if (this.self.spearCtrl.IsInputedSpAttackContinue() && !this.self.enableCancelToAvoid)
        return;
      this.self.SetArrowAimBossModeStartSign(flick_vec);
      SelfController.Command command = new SelfController.Command();
      if (this.self.CanSoulOneHandSwordFlickAction())
      {
        this.SetFlickDirection(flick_vec);
        this.self.SetFlickDirection(this.flickDirection);
        command.type = SelfController.COMMAND_TYPE.ATTACK;
      }
      else
        command.type = !this.self.CanFlickAction() ? (!this.self.CanEvolveSpecialFlickAction() ? (!this.self.enabledTeleportAvoid ? (!this.self.enabledRushAvoid ? (!this.self.buffParam.IsEnableBuff(BuffParam.BUFFTYPE.WARP_BY_AVOID) ? SelfController.COMMAND_TYPE.AVOID : SelfController.COMMAND_TYPE.WARP) : SelfController.COMMAND_TYPE.RUSH_AVOID) : SelfController.COMMAND_TYPE.TELEPORT_AVOID) : SelfController.COMMAND_TYPE.EVOLVE_SPECIAL) : SelfController.COMMAND_TYPE.FLICK_ACTION;
      command.inputVec = flick_vec;
      this.SetCommand(command);
      this.slideTimer = 0.0f;
      this.slideVerocity = Vector3.zero;
    }
  }

  private void SetFlickDirection(Vector2 flick_vec)
  {
    if (Vector2.op_Equality(flick_vec, Vector2.zero))
      return;
    double num1 = (double) Mathf.Atan2(flick_vec.y, flick_vec.x);
    float num2 = Mathf.Abs((float) num1);
    if (num1 > 0.0)
    {
      if ((double) num2 < (double) SelfController.QUARTER_PI)
        this.flickDirection = SelfController.FLICK_DIRECTION.RIGHT;
      else if ((double) num2 > (double) SelfController.THREE_QUARTER_PI)
        this.flickDirection = SelfController.FLICK_DIRECTION.LEFT;
      else
        this.flickDirection = SelfController.FLICK_DIRECTION.FRONT;
    }
    else if ((double) num2 < (double) SelfController.QUARTER_PI)
      this.flickDirection = SelfController.FLICK_DIRECTION.RIGHT;
    else if ((double) num2 > (double) SelfController.THREE_QUARTER_PI)
      this.flickDirection = SelfController.FLICK_DIRECTION.LEFT;
    else
      this.flickDirection = SelfController.FLICK_DIRECTION.REAR;
  }

  private void OnTouchOff(InputManager.TouchInfo touch_info) => this.IsEnableControll();

  protected override void Update()
  {
    InputManager.TouchInfo moveStickInfo = this.moveStickInfo;
    this.moveStickInfo = (InputManager.TouchInfo) null;
    if ((this.self.actionID == (Character.ACTION_ID) 20 || this.self.actionID == (Character.ACTION_ID) 34 || this.self.actionID == (Character.ACTION_ID) 21) && this.isInputtedTouch)
      this.checkTouch = true;
    if (this.nextCommand != null && (Object.op_Equality((Object) this.self, (Object) null) || !this.self.IsHitStop()))
      this.nextCommand.deltaTime += Time.deltaTime;
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionType().IsDialog())
      this.SetEnableControll(false, ControllerBase.DISABLE_FLAG.INPUT_DISABLE);
    else
      this.SetEnableControll(true, ControllerBase.DISABLE_FLAG.INPUT_DISABLE);
    if (!this.IsEnableControll() || Object.op_Equality((Object) this.character, (Object) null) || this.character.isLoading)
      return;
    base.Update();
    InputManager.TouchInfo stickInfo = MonoBehaviourSingleton<InputManager>.I.GetStickInfo();
    if (stickInfo != null)
      this.self.SetInputAxis(stickInfo.axis);
    bool flag1 = false;
    if (this.touchInfo != null)
    {
      bool activeAxis = this.touchInfo.activeAxis;
      float num1 = 0.5f;
      if (GameSaveData.instance != null)
        num1 = GameSaveData.instance.touchInGameLong;
      float num2 = this.parameter.inputLongTouchTimeLow + (this.parameter.inputLongTouchTimeHigh - this.parameter.inputLongTouchTimeLow) * num1;
      if (!this.initTouch && !this.touchInfo.activeAxis && (double) Time.time - (double) this.touchInfo.beginTime >= (double) num2)
      {
        this.initTouch = true;
        this.enableTouch = true;
        this.touchAimAxisTime = -1f;
        this.self.SetEnableTap(true);
        if (this.self.targetFieldGimmickCannon is FieldGimmickCannonSpecial)
          this.self.StartCannonCharge();
      }
      else if (this.initTouch && this.checkTouch && (!this.touchInfo.activeAxis || this.self.attackMode == Player.ATTACK_MODE.ONE_HAND_SWORD))
      {
        this.enableTouch = true;
        this.touchAimAxisTime = -1f;
        this.self.SetEnableTap(true);
      }
      this.checkTouch = false;
      if (this.initTouch)
      {
        bool flag2 = false;
        if (this.touchInfo.id == -1)
        {
          this.touchInfo = (InputManager.TouchInfo) null;
          this.enableTouch = false;
          this.isInputtedTouch = false;
          flag2 = true;
          this.self.SetEnableTap(false);
          this.self.ResetCannonShotAimEuler();
        }
        if (this.self.IsCarrying())
          return;
        if (this.self.IsAbleMoveCannon())
        {
          if ((this.self.targetFieldGimmickCannon is FieldGimmickCannonRapid || this.self.targetFieldGimmickCannon is FieldGimmickCannonField) && !this.self.targetFieldGimmickCannon.IsCooling())
            this.SetCommand(new SelfController.Command()
            {
              type = SelfController.COMMAND_TYPE.CANNON_SHOT,
              fieldGimmickObject = (IFieldGimmickObject) this.self.targetFieldGimmickCannon
            });
          if (this.touchInfo == null || !this.touchInfo.activeAxis)
            return;
          this.self.UpdateCannonAimMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
          this.slideTimer = 0.0f;
          this.slideVerocity = Vector3.zero;
          return;
        }
        if (this.self.IsChargingCannon() || this.self.IsEnableChangeActionByLongTap())
          return;
        bool flag3 = false;
        bool flag4 = false;
        bool flag5 = false;
        bool flag6 = false;
        if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.enable)
        {
          if (this.self.attackMode == Player.ATTACK_MODE.ARROW)
          {
            if (this.self.isArrowRainShot)
              flag5 = true;
            else if (StageObjectManager.CanTargetBoss)
              flag3 = true;
            else
              flag4 = true;
          }
          else if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
            flag6 = true;
        }
        if (this.self.isGuardWalk || this.self.actionID == (Character.ACTION_ID) 19 || this.self.actionID == (Character.ACTION_ID) 20 || this.self.actionID == (Character.ACTION_ID) 34)
          flag1 = true;
        if (flag3 | flag4 | flag5 | flag6)
        {
          if (flag2)
          {
            this.CancelInput();
          }
          else
          {
            if (this.enableTouch)
              this.SetCommand(new SelfController.Command()
              {
                type = flag6 ? SelfController.COMMAND_TYPE.SPECIAL_ACTION : SelfController.COMMAND_TYPE.ATTACK,
                aimKeep = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimBossSettings.enableAimKeep & flag3,
                isTouchOn = true
              });
            if (this.self.isArrowAimable)
            {
              bool flag7 = false;
              if ((double) this.touchAimAxisTime >= 0.0)
              {
                this.touchAimAxisTime += Time.deltaTime;
                if ((double) this.touchAimAxisTime >= (double) MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.aimDelayTime)
                  flag7 = true;
              }
              if (activeAxis && this.touchInfo != null && (double) ((Vector2) ref this.touchInfo.axisNoLimit).magnitude >= (double) MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.aimDragLength && (double) this.touchAimAxisTime < 0.0)
                this.touchAimAxisTime = 0.0f;
              if (flag3)
              {
                if (!this.self.isArrowAimBossMode)
                {
                  if (MonoBehaviourSingleton<InGameCameraManager>.I.arrowCameraMode == 0)
                  {
                    if (flag7)
                      this.self.SetArrowAimBossMode(true);
                  }
                  else
                    this.self.SetArrowAimBossMode(true);
                }
                if (this.self.isArrowAimBossMode && !this.self.isArrowAimEnd)
                  this.self.UpdateArrowAimBossMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
                if (this.self.isArrowAimLesserMode)
                  this.self.SetArrowAimLesserMode(false);
              }
              else
              {
                if (!this.self.isArrowAimLesserMode && flag7 | flag6)
                  this.self.SetArrowAimLesserMode(true);
                if (this.self.isArrowAimLesserMode && !this.self.isArrowAimEnd)
                  this.self.UpdateArrowAimLesserMode(this.touchInfo.axisNoLimit);
              }
            }
          }
          this.slideTimer = 0.0f;
          this.slideVerocity = Vector3.zero;
          return;
        }
        if (this.self.enabledOraclePairSwordsSP && stickInfo != null)
        {
          this.self.targetingPointList.Clear();
          this.character.SetLerpRotation(this.GetStickVec(stickInfo.axis, MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform));
          if (!Object.op_Inequality((Object) this.self.playerSender, (Object) null))
            return;
          this.self.playerSender.OnSyncPosition();
          return;
        }
        if (((!activeAxis ? 1 : (this.isInputtedTouch ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
        {
          if (this.self.IsOnCannonMode())
          {
            this.self.ResetCannonShotAimEuler();
            return;
          }
          if (flag2)
          {
            if (this.self.actionID == (Character.ACTION_ID) 21)
              return;
            this.CancelInput();
            if (!flag1)
              return;
            this.self.ActIdle(this.self.actionID != Character.ACTION_ID.MOVE, -1f);
            return;
          }
          if (this.enableTouch)
          {
            bool flag8 = false;
            if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.enable && this.self.IsExistSpecialAction())
              flag8 = true;
            if (flag8)
            {
              bool flag9 = this.self.actionID == (Character.ACTION_ID) 21;
              if ((!this.self.isActSpecialAction || !this.self.isControllable) && !flag9)
              {
                SelfController.Command command = new SelfController.Command();
                command.type = SelfController.COMMAND_TYPE.SPECIAL_ACTION;
                if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && this.self.IsActionFromAvoid() && this.self.enableSpAttackContinue)
                  command.type = SelfController.COMMAND_TYPE.ATTACK;
                command.isTouchOn = true;
                this.SetCommand(command);
              }
            }
            else
              this.InputAttack(true);
            if (!flag1)
              return;
          }
        }
      }
      else
      {
        if (this.touchInfo.id > -1 && this.self.IsAbleMoveCannon())
        {
          if (!this.touchInfo.activeAxis)
            return;
          this.self.UpdateCannonAimMode(this.touchInfo.axisNoLimit, this.touchInfo.position);
          this.slideTimer = 0.0f;
          this.slideVerocity = Vector3.zero;
          return;
        }
        if (this.touchInfo.activeAxis || this.touchInfo.id == -1)
        {
          this.touchInfo = (InputManager.TouchInfo) null;
          this.self.ResetCannonShotAimEuler();
        }
      }
    }
    if (!flag1 && this.nextCommand != null)
    {
      if (this.CheckCommand(this.nextCommand))
      {
        this.ActCommand(this.nextCommand);
        this.nextCommand = (SelfController.Command) null;
        return;
      }
      if ((double) this.nextCommand.deltaTime >= (double) this.parameter.inputCommandValidTime[(int) this.nextCommand.type])
        this.nextCommand = (SelfController.Command) null;
    }
    Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
    bool flag10 = false;
    if (stickInfo != null)
    {
      Vector2 axis = stickInfo.axis;
      float magnitude = ((Vector2) ref axis).magnitude;
      float num3 = 0.0f;
      if (flag1)
        num3 = this.parameter.guardMoveThreshold;
      if ((double) Time.time - (double) stickInfo.beginTime >= (double) this.parameter.inputMoveStartTime)
      {
        if (moveStickInfo != stickInfo && this.IsCancelAble((SelfController.Command) null))
          this.CancelInput();
        this.moveStickInfo = stickInfo;
        if ((double) magnitude > (double) num3 && this.self.IsChangeableAction(Character.ACTION_ID.MOVE))
        {
          Vector3 right = cameraTransform.right;
          Vector3 forward = cameraTransform.forward;
          forward.y = 0.0f;
          ((Vector3) ref forward).Normalize();
          if (this.parameter.alwaysTopSpeed)
            ((Vector2) ref axis).Normalize();
          Vector3 velocity = !Object.op_Inequality((Object) this.self.actionTarget, (Object) null) ? Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(right, axis.x), this.parameter.moveForwardSpeed), Vector3.op_Multiply(Vector3.op_Multiply(forward, axis.y), this.parameter.moveForwardSpeed)) : Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(right, axis.x), this.parameter.moveSideSpeed), Vector3.op_Multiply(Vector3.op_Multiply(forward, axis.y), this.parameter.moveForwardSpeed));
          if (flag1)
          {
            this.self.ActGuardWalk(this.parameter.enableRootMotion ? Vector3.zero : velocity, this.parameter.guardMoveSyncSpeed, ((Vector3) ref velocity).normalized);
            velocity = forward;
          }
          else if (this.self.IsCarrying())
          {
            this.self.ActCarryWalk(this.parameter.enableRootMotion ? Vector3.zero : velocity, this.parameter.moveForwardSpeed, ((Vector3) ref velocity).normalized);
            velocity = forward;
          }
          else
          {
            bool flag11 = this.parameter.enableRootMotion;
            if (this.IsValidSlideProc())
            {
              flag11 = false;
              this.slideSlowTimer += Time.deltaTime;
              float num4 = Mathf.Clamp01(this.slideSlowTimer / this.GetParamNeedTimeToMaxTime());
              velocity = Vector3.op_Multiply(velocity, Mathf.Lerp(0.0f, 1f, num4));
            }
            Character.MOTION_ID motion_id = Character.MOTION_ID.WALK;
            this.character.ActMoveVelocity(flag11 ? Vector3.zero : velocity, this.parameter.moveForwardSpeed, motion_id);
            this.character.SetLerpRotation(velocity);
            this.slideTimer = 0.0f;
            this.slideVerocity = velocity;
          }
          InGameTutorialManager component = ((Component) MonoBehaviourSingleton<AppMain>.I).GetComponent<InGameTutorialManager>();
          if ((double) ((Vector3) ref velocity).sqrMagnitude > 0.0 && Object.op_Inequality((Object) component, (Object) null))
            component.TutorialMoveTime += Time.deltaTime;
          flag10 = true;
        }
      }
    }
    if (!flag10)
    {
      this.slideSlowTimer = 0.0f;
      if (this.character.actionID == Character.ACTION_ID.MOVE)
      {
        if (flag1)
          this.self.ActSpecialAction(false, true);
        else if (this.self.IsCarrying())
          this.self.ActCarryIdle();
        else
          this.character.ActIdle();
      }
      if (this.character.actionID == Character.ACTION_ID.MAX && !((Player) this.character).enableCancelToAttack)
      {
        this.slideVerocity = Vector3.op_Multiply(((Component) this).transform.forward, this.GetParamAvoidSpeed());
        this.slideTimer = 0.0f;
      }
      else
      {
        float paramSlideTime = this.GetParamSlideTime();
        if (this.IsValidSlideProc() && (double) ((Vector3) ref this.slideVerocity).sqrMagnitude > 0.10000000149011612 && (double) this.slideTimer < (double) paramSlideTime)
        {
          this.slideTimer += Time.deltaTime;
          float num = (float) ((double) this.slideTimer / (double) paramSlideTime - 1.0);
          Vector3 slideVerocity = Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_UnaryNegation(this.slideVerocity), (float) ((double) num * (double) num * (double) num + 1.0)), this.slideVerocity);
          this.character.ActMoveInertia(ref slideVerocity);
        }
        else
        {
          this.slideVerocity = Vector3.zero;
          this.slideTimer = 0.0f;
        }
      }
    }
    if (this.self.attackMode != Player.ATTACK_MODE.ARROW || this.moveStickInfo == null || !Vector2.op_Inequality(this.moveStickInfo.axis, Vector2.zero))
      return;
    this.self.SetArrowAimBossModeStartSign(this.moveStickInfo.axis);
  }

  protected Vector3 GetStickVec(Vector2 stick_vec, Transform camera_transform)
  {
    Vector3 right = camera_transform.right;
    Vector3 forward = camera_transform.forward;
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    if (this.parameter.alwaysTopSpeed)
      ((Vector2) ref stick_vec).Normalize();
    return !Object.op_Inequality((Object) this.self.actionTarget, (Object) null) ? Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(right, stick_vec.x), this.parameter.moveForwardSpeed), Vector3.op_Multiply(Vector3.op_Multiply(forward, stick_vec.y), this.parameter.moveForwardSpeed)) : Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(right, stick_vec.x), this.parameter.moveSideSpeed), Vector3.op_Multiply(Vector3.op_Multiply(forward, stick_vec.y), this.parameter.moveForwardSpeed));
  }

  private float GetParamNeedTimeToMaxTime()
  {
    if (this.character.IsHittingIceFloor())
      return this.parameter.needTimeToMaxTimeOnIce * (1f - this.character.buffParam.GetPassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
    return this.character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE) ? this.debuffParam.slideIceParam.needTimeToMaxTime * (1f - this.character.buffParam.GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE)) : this.debuffParam.slideParam.needTimeToMaxTime;
  }

  private float GetParamSlideTime()
  {
    if (this.character.IsHittingIceFloor())
      return this.parameter.slideTime * (1f - this.character.buffParam.GetPassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
    return this.character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE) ? this.debuffParam.slideIceParam.slideTime * (1f - this.character.buffParam.GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE)) : this.debuffParam.slideParam.slideTime;
  }

  private float GetParamAvoidSpeed()
  {
    if (this.character.IsHittingIceFloor())
      return this.parameter.avoidSpeedOnIce * (1f - this.character.buffParam.GetPassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE));
    return this.character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE) ? this.debuffParam.slideIceParam.avoidSpeed * (1f - this.character.buffParam.GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE.SLIDE_ICE)) : this.debuffParam.slideParam.avoidSpeed;
  }

  private bool IsValidSlideProc()
  {
    int num = this.character.IsHittingIceFloor() || this.character.IsValidBuff(BuffParam.BUFFTYPE.SLIDE) ? 1 : (this.character.buffParam.IsValidFieldBuff(BuffParam.BUFFTYPE.SLIDE_ICE) ? 1 : 0);
    bool flag = this.self.thsCtrl.IsActiveIai() || this.self.snatchCtrl.IsMove() || this.self.snatchCtrl.IsMoveLoop();
    return num != 0 && !flag && !this.character.isDead;
  }

  public bool OnSkillButtonPress(int skill_index)
  {
    if (this.nextCommand != null && this.nextCommand.type == SelfController.COMMAND_TYPE.SKILL && this.nextCommand.skillIndex == skill_index || !this.self.IsActSkillAction(skill_index))
      return false;
    this.SetCommand(new SelfController.Command()
    {
      type = SelfController.COMMAND_TYPE.SKILL,
      skillIndex = skill_index
    });
    return true;
  }

  public bool OnWeaponChangeButtonPress(int weapon_index)
  {
    if (this.nextCommand != null && this.nextCommand.type == SelfController.COMMAND_TYPE.CHANGE_WEAPON && this.nextCommand.weaponIndex == weapon_index || weapon_index < 0 || this.self.equipWeaponList.Count <= weapon_index || this.self.equipWeaponList[weapon_index] == null || this.self.IsCarrying() || this.self.actionID == (Character.ACTION_ID) 44)
      return false;
    this.SetCommand(new SelfController.Command()
    {
      type = SelfController.COMMAND_TYPE.CHANGE_WEAPON,
      weaponIndex = weapon_index
    });
    return true;
  }

  public bool OnReserveBurstReloadMotion()
  {
    if (this.nextCommand != null && this.nextCommand.type == SelfController.COMMAND_TYPE.THS_BURST_RELOAD)
      return false;
    this.SetCommand(new SelfController.Command()
    {
      type = SelfController.COMMAND_TYPE.THS_BURST_RELOAD,
      MotionId = this.self.playerParameter.twoHandSwordActionInfo.burstTHSInfo.FirstReloadActionAttackID
    });
    return true;
  }

  public override void OnActReaction()
  {
    base.OnActReaction();
    this.CancelInput();
    this.self.CancelCannonMode();
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByPlayer();
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetPositionByPlayer();
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearCameraMode();
  }

  public enum COMMAND_TYPE
  {
    NONE = -1, // 0xFFFFFFFF
    ATTACK = 0,
    AVOID = 1,
    SKILL = 2,
    SPECIAL_ACTION = 3,
    CHANGE_WEAPON = 4,
    GATHER = 5,
    CANNON_STANDBY = 6,
    CANNON_SHOT = 7,
    SONAR = 8,
    WARP = 9,
    EVOLVE = 10, // 0x0000000A
    EVOLVE_SPECIAL = 11, // 0x0000000B
    READ_STORY = 12, // 0x0000000C
    THS_BURST_RELOAD = 13, // 0x0000000D
    GATHER_GIMMICK = 14, // 0x0000000E
    BINGO = 15, // 0x0000000F
    COOP_FISHING = 16, // 0x00000010
    FLICK_ACTION = 17, // 0x00000011
    CARRY = 18, // 0x00000012
    PORTAL_GIMMICK = 19, // 0x00000013
    QUEST_GIMMICK = 20, // 0x00000014
    TELEPORT_AVOID = 21, // 0x00000015
    RUSH_AVOID = 22, // 0x00000016
  }

  public class Command
  {
    public SelfController.COMMAND_TYPE type = SelfController.COMMAND_TYPE.NONE;
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
    RIGHT,
  }
}
