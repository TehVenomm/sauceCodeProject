// Decompiled with JetBrains decompiler
// Type: AutoSelfController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class AutoSelfController : SelfController
{
  protected IEnumerator mainCoroutine;
  protected bool isStart;
  protected float startWaitTime;
  public TargetPoint actionTargetPoint;
  private bool isWaitingSpecial;
  private bool isActSpecialOneSwordSoul;

  public AutoBrain autoBrain { get; private set; }

  public InGameSettingsManager.NpcController npcParameter { get; private set; }

  private bool isAttack
  {
    get
    {
      return Object.op_Inequality((Object) this.self, (Object) null) && this.self.actionID == Character.ACTION_ID.ATTACK;
    }
  }

  private bool isGuard
  {
    get
    {
      return Object.op_Inequality((Object) this.self, (Object) null) && this.self.actionID == (Character.ACTION_ID) 19;
    }
  }

  private bool isMove
  {
    get
    {
      return Object.op_Inequality((Object) this.self, (Object) null) && this.self.actionID == Character.ACTION_ID.MOVE;
    }
  }

  private bool isChangeableAttack
  {
    get
    {
      return Object.op_Inequality((Object) this.self, (Object) null) && this.self.IsChangeableAction(Character.ACTION_ID.ATTACK);
    }
  }

  private bool isChangeableSpecialAction
  {
    get
    {
      return Object.op_Inequality((Object) this.self, (Object) null) && this.self.IsChangeableAction((Character.ACTION_ID) 33);
    }
  }

  private StageObject target
  {
    get
    {
      return !Object.op_Inequality((Object) this.brain, (Object) null) ? (StageObject) null : this.brain.targetCtrl.GetCurrentTarget();
    }
  }

  protected override void Awake()
  {
    base.Awake();
    this.autoBrain = this.AttachBrain<AutoBrain>();
  }

  protected override void Start()
  {
    base.Start();
    this.npcParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.npcController;
    this.isStart = true;
    if (!this.IsEnableControll())
      return;
    this.OnChangeEnableControll(true);
  }

  private bool IsTouchedInAutoMode()
  {
    InputManager.TouchInfo stickInfo = MonoBehaviourSingleton<InputManager>.I.GetStickInfo();
    return this.touchInfo != null || stickInfo != null;
  }

  protected override void Update()
  {
    if (this.IsTouchedInAutoMode())
    {
      base.Update();
    }
    else
    {
      if (this.self.actionID != (Character.ACTION_ID) 27)
      {
        bool flag = false;
        if (this.self.isGuardWalk || this.self.actionID == (Character.ACTION_ID) 19 || this.self.actionID == (Character.ACTION_ID) 20)
          flag = true;
        if (!flag && this.nextCommand != null)
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
      }
      if (!this.IsEnableControll())
        return;
      this.OnDead();
    }
  }

  protected override void OnDisable()
  {
    this.self.SetEnableTap(false);
    base.OnDisable();
  }

  public override void OnChangeEnableControll(bool enable)
  {
    if (enable && !CoopStageObjectUtility.CanControll((StageObject) this.self))
    {
      Log.Error(LOG.INGAME, "NpcController:OnChangeEnableControll. field block enable. obj={0}", (object) this.self);
      enable = false;
    }
    base.OnChangeEnableControll(enable);
    if (enable)
    {
      if (!this.isStart || !((Behaviour) this).enabled || !Object.op_Inequality((Object) this.self, (Object) null) || this.mainCoroutine != null)
        return;
      this.mainCoroutine = this.AIMain();
      this.StartCoroutine(this.mainCoroutine);
    }
    else
    {
      if (this.mainCoroutine != null)
      {
        this.StopAllCoroutines();
        this.mainCoroutine = (IEnumerator) null;
      }
      if (!this.isGuard)
        return;
      this.self.ActIdle(false, -1f);
    }
  }

  private IEnumerator AIMain()
  {
    while (Object.op_Equality((Object) this.brain, (Object) null) || !this.brain.isInitialized)
      yield return (object) 0;
    while (!this.self.isControllable)
      yield return (object) 0;
    if ((double) this.startWaitTime > 0.0)
      yield return (object) new WaitForSeconds(this.startWaitTime);
    while (((Behaviour) this).enabled)
    {
      while (this.self.IsMirror())
        yield return (object) new WaitForSeconds(1f);
      while (this.IsTouchedInAutoMode())
        yield return (object) 0;
      this.OnMove();
      this.OnWeapon();
      float num = 0.0f;
      if (Object.op_Inequality((Object) this.self.packetSender, (Object) null))
        num = this.self.packetSender.GetWaitTime(0.0f);
      if ((double) num > 0.0)
        yield return (object) new WaitForSeconds(num);
      else
        yield return (object) 0;
    }
    this.mainCoroutine = (IEnumerator) null;
  }

  private void OnDead()
  {
    if (this.self.isControllable || this.self.actionID != (Character.ACTION_ID) 24 || (double) this.self.rescueTime > 0.0 || (double) this.self.deadStartTime < 0.0 || this.self.isProgressStop() || this.self != null)
      return;
    this.self.DestroyObject();
  }

  public void OnSkill()
  {
    if (!this.autoBrain.skillCtr.IsAct || !this.self.IsActSkillAction(this.autoBrain.skillCtr.skillIndex))
      return;
    this.self.ActSkillAction(this.autoBrain.skillCtr.skillIndex, false);
    this.autoBrain.skillCtr.RemoveSkillIndex();
  }

  private void OnMove()
  {
    bool flag = true;
    if (this.brain.moveCtrl.IsAvoid())
    {
      if (!this.self.IsChangeableAction(Character.ACTION_ID.MAX))
        return;
      this.OnAvoid(this.brain.moveCtrl.avoidPlace);
      flag = true;
    }
    else if (this.brain.moveCtrl.IsSeek())
    {
      if (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
        return;
      this.OnMoveStick(this.brain.moveCtrl.stickVec, this.brain.moveCtrl.targetPos);
      flag = true;
    }
    else if (this.brain.moveCtrl.IsStop())
      flag = false;
    if (flag || !this.isMove)
      return;
    this.character.ActIdle();
  }

  private void OnMoveStick(Vector2 stick_vec, Vector3 target_pos)
  {
    ((Vector2) ref stick_vec).Normalize();
    Vector3 position = ((Component) this).transform.position;
    Vector3 vector3_1 = Vector3.op_Subtraction(target_pos, position);
    vector3_1.y = 0.0f;
    ((Vector3) ref vector3_1).Normalize();
    Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.Euler(0.0f, 90f, 0.0f), vector3_1);
    Vector3 vector3_3 = vector3_1;
    Vector3 velocity = !Object.op_Inequality((Object) this.self.actionTarget, (Object) null) ? Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(vector3_2, stick_vec.x), this.parameter.moveForwardSpeed), Vector3.op_Multiply(Vector3.op_Multiply(vector3_3, stick_vec.y), this.parameter.moveForwardSpeed)) : Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(vector3_2, stick_vec.x), this.parameter.moveSideSpeed), Vector3.op_Multiply(Vector3.op_Multiply(vector3_3, stick_vec.y), this.parameter.moveForwardSpeed));
    this.character.ActMoveVelocity(this.parameter.enableRootMotion ? Vector3.zero : velocity, this.parameter.moveForwardSpeed);
    this.character.SetLerpRotation(velocity);
  }

  private void OnAvoid(PLACE avoid_place)
  {
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    Vector3 vector3_1 = Vector3.op_Subtraction(this.target._transform.position, this.character._transform.position);
    vector3_1.y = 0.0f;
    Quaternion quaternion = Quaternion.LookRotation(vector3_1);
    Vector3 vector3_2 = Vector3.zero;
    switch (avoid_place)
    {
      case PLACE.FRONT:
        vector3_2 = Quaternion.op_Multiply(quaternion, Vector3.forward);
        break;
      case PLACE.RIGHT:
        vector3_2 = Quaternion.op_Multiply(quaternion, Vector3.right);
        break;
      case PLACE.LEFT:
        vector3_2 = Quaternion.op_Multiply(quaternion, Vector3.left);
        break;
      case PLACE.BACK:
        vector3_2 = Quaternion.op_Multiply(quaternion, Vector3.back);
        break;
    }
    this.character.LookAt(Vector3.op_Addition(this.character._transform.position, vector3_2), false);
    this.self.ActAvoid();
  }

  private void OnWeapon()
  {
    if (this.brain.weaponCtrl.IsAttack())
    {
      if (this.self.attackMode == Player.ATTACK_MODE.ARROW)
        this.OnArrowAttack();
      else
        this.OnAttack();
    }
    else if (this.brain.weaponCtrl.IsSpecial())
    {
      if (this.self.attackMode == Player.ATTACK_MODE.ARROW)
        this.OnArrowAttack();
      else
        this.OnSpecialAttack();
    }
    else if (this.brain.weaponCtrl.IsGuard())
    {
      this.OnGuard();
    }
    else
    {
      if (this.self.actionID == (Character.ACTION_ID) 19)
        this.self.ActIdle(true, -1f);
      if (this.self.enableInputCharge)
        this.self.SetEnableTap(false);
    }
    if (this.brain.weaponCtrl.changeIndex < 0)
      return;
    this.OnChangeWeapon();
  }

  private void OnAttack()
  {
    if (this.isActSpecialOneSwordSoul)
      return;
    bool flag = false;
    if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && Object.op_Equality((Object) this.self.targetingPoint, (Object) null))
    {
      this.self.targetingPointList.Add(this.actionTargetPoint);
      flag = true;
    }
    if (this.self.enableTap)
      this.self.SetEnableTap(false);
    if (this.isAttack && this.self.enableInputCombo)
    {
      if (flag)
      {
        Vector3 vector3 = Vector3.op_Subtraction(this.self.targetingPoint.GetTargetPoint(), this.self._position);
        vector3.y = 0.0f;
        this.self.SetLerpRotation(((Vector3) ref vector3).normalized);
      }
      if (this.brain.weaponCtrl.beforeAttackId != 0 && this.brain.weaponCtrl.beforeAttackId == this.self.attackID)
        return;
      this.brain.weaponCtrl.ComboOn();
      this.brain.weaponCtrl.SetBeforeAttackId(this.self.attackID);
      this.self.InputAttackCombo();
    }
    else
    {
      if (!this.isChangeableAttack)
        return;
      if (flag)
      {
        Vector3 vector3 = Vector3.op_Subtraction(this.self.targetingPoint.GetTargetPoint(), this.self._position);
        vector3.y = 0.0f;
        this.self.SetLerpRotation(((Vector3) ref vector3).normalized);
      }
      if (this.brain.weaponCtrl.IsAvoidAttack())
      {
        if (this.self.actionID == Character.ACTION_ID.MAX)
          return;
        this.self.ActAvoid();
        this.StartCoroutine(this.WaitArmorBreakAttach());
      }
      else
      {
        string _motionLayerName = "Base Layer.";
        this.self.ActAttack(this.self.GetNormalAttackId(this.self.attackMode, this.self.spAttackType, this.self.extraAttackType, out _motionLayerName), true, false, _motionLayerName, "");
        this.brain.weaponCtrl.ComboOff();
        this.brain.weaponCtrl.SetBeforeAttackId(0);
      }
    }
  }

  private IEnumerator WaitArmorBreakAttach()
  {
    yield return (object) new WaitForSeconds(0.1f);
    while (!this.self.CheckAvoidAttack())
      yield return (object) null;
    this.brain.weaponCtrl.AvoidAttackOff();
  }

  private void OnSpecialAttack()
  {
    if (!this.self.isActSpecialAction && this.isChangeableSpecialAction)
    {
      this.self.SetEnableTap(true);
      if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
        this.self.ActSpecialAction(true, true);
      else if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
      {
        this.self.ActSpecialAction(true, true);
        this.StartCoroutine(this.ActSpecialPairSoulSword());
      }
      else if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
      {
        if (this.isActSpecialOneSwordSoul)
          return;
        this.StartCoroutine(this.ActSpecialOneSwordSoul());
      }
      else if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
      {
        if (this.isWaitingSpecial)
          return;
        if (this.self.thsCtrl.IsRequiredReloadAction())
          this.StartCoroutine(this.ActSpecialBurstReload());
        else
          this.StartCoroutine(this.ActSpecialBurstFire());
      }
      else
      {
        this.self.ActSpecialAction(true, true);
        if (this.self.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
        {
          if (this.self.spAttackType != SP_ATTACK_TYPE.NONE)
            this.brain.weaponCtrl.SetChargeRate(1f);
          else
            this.brain.weaponCtrl.SetChargeRate(0.5f);
        }
        else if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
          this.brain.weaponCtrl.SetChargeRate(0.5f);
        else
          this.brain.weaponCtrl.SetChargeRate(1f);
      }
    }
    else
    {
      if (!this.self.enableInputCharge || (double) this.self.GetChargingRate() < (double) this.brain.weaponCtrl.chargeRate)
        return;
      if (this.self.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
      {
        switch (this.self.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
            if (Object.op_Inequality((Object) this.self.targetingPoint, (Object) null))
            {
              Vector3 vector3 = Vector3.op_Subtraction(this.self.targetingPoint.GetTargetPoint(), this.self._position);
              vector3.y = 0.0f;
              this.self.SetLerpRotation(((Vector3) ref vector3).normalized);
              break;
            }
            break;
          case SP_ATTACK_TYPE.HEAT:
            if (Object.op_Inequality((Object) this.self.targetingPoint, (Object) null))
            {
              Vector3 vec = Vector3.op_Subtraction(this.self.targetingPoint.GetTargetPoint(), this.self._position);
              vec.y = 0.0f;
              this.self.SetSpearCursorPos(vec);
              break;
            }
            Vector3 vec1 = Vector3.op_Subtraction(this.actionTargetPoint.GetTargetPoint(), this.self._position);
            vec1.y = 0.0f;
            this.self.SetSpearCursorPos(vec1);
            break;
        }
      }
      this.self.SetEnableTap(false);
    }
  }

  private IEnumerator ActSpecialBurstReload()
  {
    this.isWaitingSpecial = true;
    this.self.ActSpecialAction(true, true);
    yield return (object) new WaitForSeconds(0.5f);
    this.isWaitingSpecial = false;
    this.self.SetEnableTap(false);
  }

  private IEnumerator ActSpecialBurstFire()
  {
    this.isWaitingSpecial = true;
    this.self.ActSpecialAction(true, true);
    yield return (object) new WaitForSeconds(0.5f);
    if (Object.op_Equality((Object) this.self.targetingPoint, (Object) null))
      this.self.targetingPointList.Add(this.actionTargetPoint);
    if (Object.op_Inequality((Object) this.self.targetingPoint, (Object) null))
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.self.targetingPoint.GetTargetPoint(), this.self._position);
      vector3.y = 0.0f;
      this.self.SetLerpRotation(((Vector3) ref vector3).normalized);
    }
    this.self.SetEnableTap(false);
    this.isWaitingSpecial = false;
  }

  private IEnumerator ActSpecialOneSwordSoul()
  {
    if (Object.op_Equality((Object) this.self.targetingPoint, (Object) null))
      this.self.targetingPointList.Add(this.actionTargetPoint);
    if (Object.op_Inequality((Object) this.self.targetingPoint, (Object) null))
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.self.targetingPoint.GetTargetPoint(), this.self._position);
      vector3.y = 0.0f;
      this.self.SetLerpRotation(((Vector3) ref vector3).normalized);
    }
    this.isActSpecialOneSwordSoul = true;
    this.self.ActSpecialAction(true, true);
    yield return (object) new WaitForSeconds(2f);
    this.self.SetEnableTap(false);
    if (Utility.Dice100(65))
    {
      this.self.SetFlickDirection(SelfController.FLICK_DIRECTION.FRONT);
      while (this.self.isActSpecialAction && !this.self.ActSpAttackContinue())
        yield return (object) null;
    }
    this.isActSpecialOneSwordSoul = false;
  }

  private IEnumerator ActSpecialPairSoulSword()
  {
    yield return (object) new WaitForSeconds(0.5f);
    this.self.SetEnableTap(false);
  }

  private void OnArrowAttack()
  {
    if (Object.op_Equality((Object) this.self.targetingPoint, (Object) null))
      this.self.targetingPointList.Add(this.actionTargetPoint);
    if (this.self.isControllable)
    {
      this.self.SetEnableTap(true);
      string _motionLayerName = "Base Layer.";
      this.self.ActAttack(this.self.GetNormalAttackId(this.self.attackMode, this.self.spAttackType, this.self.extraAttackType, out _motionLayerName), true, false, _motionLayerName, "");
      if (this.brain.weaponCtrl.IsSpecial())
        this.brain.weaponCtrl.SetChargeRate(1f);
      else if (this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
        this.brain.weaponCtrl.SetChargeRate(1f);
      else
        this.brain.weaponCtrl.SetChargeRate(Random.value);
    }
    else if (this.self.enableInputCharge && (double) this.self.GetChargingRate() >= (double) this.brain.weaponCtrl.chargeRate)
    {
      this.self.SetEnableTap(false);
    }
    else
    {
      if (!this.self.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
        return;
      if (!this.self.isArrowAimLesserMode)
        this.self.SetArrowAimLesserMode(true);
      Vector3 zero = Vector3.zero;
      Vector3 vector3_1 = !Object.op_Inequality((Object) this.self.targetingPoint, (Object) null) ? this.actionTargetPoint.GetTargetPoint() : this.self.targetingPoint.GetTargetPoint();
      Vector3 lesserCursorEffect = this.self.GetArrowAimLesserCursorEffect();
      if ((double) Vector3.Distance(lesserCursorEffect, vector3_1) > 1.0)
      {
        Vector3 vector3_2 = Vector3.op_Subtraction(lesserCursorEffect, vector3_1);
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(vector3_2.x, vector3_2.z);
        this.self.UpdateArrowAimLesserMode(((Vector2) ref vector2).normalized);
      }
      else
        this.self.UpdateArrowAimLesserMode(Vector2.zero);
    }
  }

  private void OnGuard()
  {
    if (!this.self.isGuardAttackMode || this.self.isActSpecialAction || !this.isChangeableSpecialAction)
      return;
    this.self.ActSpecialAction(true, true);
  }

  private void OnChangeWeapon()
  {
    int changeIndex = this.brain.weaponCtrl.changeIndex;
    if (changeIndex < 0 || this.self.equipWeaponList.Count <= changeIndex || changeIndex == this.self.weaponIndex || this.self.equipWeaponList[changeIndex] == null)
      return;
    this.self.ActChangeWeapon(this.self.equipWeaponList[changeIndex], changeIndex);
    this.brain.weaponCtrl.ResetChangeIndex();
  }

  public void UpdateTarget()
  {
    this.brain.targetCtrl.UpdateTarget();
    if (this.self.attackMode != Player.ATTACK_MODE.ARROW)
      return;
    this.UpdateRegionTarget();
  }

  private void UpdateRegionTarget()
  {
    if (Object.op_Equality((Object) this.self, (Object) null))
      return;
    this.self.targetingPointList.Clear();
    if (Object.op_Equality((Object) this.target, (Object) null))
      return;
    Enemy target = this.target as Enemy;
    if (Object.op_Equality((Object) target, (Object) null) || target.isDead || !target.enableTargetPoint)
      return;
    TargetPoint[] targetPoints = target.targetPoints;
    if (targetPoints == null || targetPoints.Length == 0)
      return;
    TargetPoint targetPoint1 = (TargetPoint) null;
    float num = float.MaxValue;
    Vector2 vector2Xz = this.self._transform.position.ToVector2XZ();
    Vector2 forwardXz = this.self.forwardXZ;
    ((Vector2) ref forwardXz).Normalize();
    int index = 0;
    for (int length = targetPoints.Length; index < length; ++index)
    {
      TargetPoint targetPoint2 = targetPoints[index];
      if (((Component) targetPoint2).gameObject.activeInHierarchy)
      {
        Vector2 vector2 = Vector2.op_Subtraction(targetPoint2.GetTargetPoint().ToVector2XZ(), vector2Xz);
        float sqrMagnitude = ((Vector2) ref vector2).sqrMagnitude;
        if (Object.op_Equality((Object) targetPoint1, (Object) null) || (double) sqrMagnitude < (double) num)
        {
          targetPoint1 = targetPoint2;
          num = sqrMagnitude;
        }
      }
    }
    if (!Object.op_Inequality((Object) targetPoint1, (Object) null))
      return;
    this.self.targetingPointList.Add(targetPoint1);
  }
}
