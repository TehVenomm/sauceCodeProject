// Decompiled with JetBrains decompiler
// Type: NpcController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class NpcController : ControllerBase
{
  protected IEnumerator mainCoroutine;
  protected bool isStart;
  protected float startWaitTime;
  private bool isPose;

  private Player player { get; set; }

  public NonPlayer nonPlayer { get; set; }

  public NpcBrain npcBrain { get; private set; }

  public InGameSettingsManager.NpcController parameter { get; private set; }

  public InGameSettingsManager.SelfController selfParameter { get; private set; }

  private bool isAttack
  {
    get
    {
      return Object.op_Inequality((Object) this.player, (Object) null) && this.player.actionID == Character.ACTION_ID.ATTACK;
    }
  }

  private bool isGuard
  {
    get
    {
      return Object.op_Inequality((Object) this.player, (Object) null) && this.player.actionID == (Character.ACTION_ID) 19;
    }
  }

  private bool isMove
  {
    get
    {
      return Object.op_Inequality((Object) this.player, (Object) null) && this.player.actionID == Character.ACTION_ID.MOVE;
    }
  }

  private bool isChangeableAttack
  {
    get
    {
      return Object.op_Inequality((Object) this.player, (Object) null) && this.player.IsChangeableAction(Character.ACTION_ID.ATTACK);
    }
  }

  private bool isChangeableSpecialAction
  {
    get
    {
      return Object.op_Inequality((Object) this.player, (Object) null) && this.player.IsChangeableAction((Character.ACTION_ID) 33);
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
    this.player = this.character as Player;
    this.npcBrain = this.AttachBrain<NpcBrain>();
    this.nonPlayer = ((Component) this).gameObject.GetComponent<NonPlayer>();
  }

  protected override void Start()
  {
    base.Start();
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.npcController;
    this.selfParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController;
    this.isStart = true;
    if (this.IsEnableControll())
      this.OnChangeEnableControll(true);
    if (!Object.op_Inequality((Object) this.player, (Object) null) || !this.player.IsCarrying())
      return;
    this.player.carryingGimmickObject.EndCarry();
  }

  protected override void Update()
  {
    base.Update();
    if (!this.IsEnableControll())
      return;
    this.OnDead();
  }

  public override void OnChangeEnableControll(bool enable)
  {
    if (enable && !CoopStageObjectUtility.CanControll((StageObject) this.player))
    {
      Log.Error(LOG.INGAME, "NpcController:OnChangeEnableControll. field block enable. obj={0}", (object) this.player);
      enable = false;
    }
    base.OnChangeEnableControll(enable);
    if (enable)
    {
      if (!this.isStart || !((Behaviour) this).enabled || !Object.op_Inequality((Object) this.player, (Object) null) || this.mainCoroutine != null)
        return;
      this.startWaitTime = this.parameter.startWaitTime;
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
      this.player.ActIdle(false, -1f);
    }
  }

  public override void OnActReaction()
  {
    base.OnActReaction();
    if (!this.IsEnableControll() || this.mainCoroutine == null)
      return;
    this.StopAllCoroutines();
    this.startWaitTime = this.parameter.afterReactionWaitTime;
    this.mainCoroutine = this.AIMain();
    this.StartCoroutine(this.mainCoroutine);
  }

  private IEnumerator AIMain()
  {
    while (this.isPose)
      yield return (object) null;
    while (Object.op_Equality((Object) this.brain, (Object) null) || !this.brain.isInitialized)
      yield return (object) 0;
    while (!this.player.isControllable)
      yield return (object) 0;
    if ((double) this.startWaitTime > 0.0)
      yield return (object) new WaitForSeconds(this.startWaitTime);
    while (((Behaviour) this).enabled)
    {
      while (this.player.IsMirror())
        yield return (object) new WaitForSeconds(1f);
      this.OnMove();
      this.OnWeapon();
      float num = 0.0f;
      if (Object.op_Inequality((Object) this.player.packetSender, (Object) null))
        num = this.player.packetSender.GetWaitTime(0.0f);
      if ((double) num > 0.0)
        yield return (object) new WaitForSeconds(num);
      else
        yield return (object) 0;
    }
    this.mainCoroutine = (IEnumerator) null;
  }

  private void OnDead()
  {
    if (this.player.isControllable || this.player.actionID != (Character.ACTION_ID) 24 || this.player.IsAutoReviving() || (double) this.player.rescueTime > 0.0 || (double) this.player.deadStartTime < 0.0 || this.player.isProgressStop() || this.player is Self)
      return;
    this.player.DestroyObject();
  }

  private void OnMove()
  {
    bool flag = false;
    if (this.brain.moveCtrl.IsAvoid())
    {
      if (!this.player.IsChangeableAction(Character.ACTION_ID.MAX))
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
    Vector3 position = ((Component) this).transform.position;
    Vector3 vector3_1 = Vector3.op_Subtraction(target_pos, position);
    vector3_1.y = 0.0f;
    ((Vector3) ref vector3_1).Normalize();
    Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.Euler(0.0f, 90f, 0.0f), vector3_1);
    Vector3 vector3_3 = vector3_1;
    double x = (double) stick_vec.x;
    Vector3 vector3_4 = Vector3.op_Addition(Vector3.op_Multiply(Vector3.op_Multiply(vector3_2, (float) x), this.selfParameter.moveSideSpeed), Vector3.op_Multiply(Vector3.op_Multiply(vector3_3, stick_vec.y), this.selfParameter.moveForwardSpeed));
    this.character.ActMoveVelocity(vector3_4, this.selfParameter.moveForwardSpeed);
    this.character.SetLerpRotation(vector3_4);
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
    this.player.ActAvoid();
  }

  private void OnWeapon()
  {
    if (this.brain.weaponCtrl.IsAttack())
    {
      if (this.player.attackMode == Player.ATTACK_MODE.ARROW)
        this.OnArrowAttack();
      else
        this.OnAttack();
    }
    else if (this.brain.weaponCtrl.IsSpecial())
    {
      if (this.player.attackMode == Player.ATTACK_MODE.ARROW)
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
      if (this.player.actionID == (Character.ACTION_ID) 19)
        this.player.ActIdle(true, -1f);
      if (this.player.enableInputCharge)
        this.player.SetEnableTap(false);
    }
    if (this.brain.weaponCtrl.changeIndex < 0)
      return;
    this.OnChangeWeapon();
  }

  private void OnAttack()
  {
    if (this.player.enableTap)
      this.player.SetEnableTap(false);
    if (this.isAttack && this.player.enableInputCombo)
    {
      if (this.brain.weaponCtrl.beforeAttackId != 0 && this.brain.weaponCtrl.beforeAttackId == this.player.attackID)
        return;
      this.brain.weaponCtrl.ComboOn();
      this.brain.weaponCtrl.SetBeforeAttackId(this.player.attackID);
      this.player.InputAttackCombo();
    }
    else
    {
      if (!this.isChangeableAttack)
        return;
      this.character.ActAttack(0);
      this.brain.weaponCtrl.ComboOff();
      this.brain.weaponCtrl.SetBeforeAttackId(0);
    }
  }

  private void OnSpecialAttack()
  {
    if (!this.player.isActSpecialAction && this.isChangeableSpecialAction)
    {
      this.player.SetEnableTap(true);
      this.player.ActSpecialAction();
      this.brain.weaponCtrl.SetChargeRate(1f);
    }
    else
    {
      if (!this.player.enableInputCharge || (double) this.player.GetChargingRate() < (double) this.brain.weaponCtrl.chargeRate)
        return;
      this.player.SetEnableTap(false);
    }
  }

  private void OnArrowAttack()
  {
    if (this.player.isControllable)
    {
      this.player.SetEnableTap(true);
      this.player.ActAttack(0, true, false, "", "");
      if (this.brain.weaponCtrl.IsSpecial())
        this.brain.weaponCtrl.SetChargeRate(1f);
      else
        this.brain.weaponCtrl.SetChargeRate(Random.value);
    }
    else
    {
      if (!this.player.enableInputCharge || (double) this.player.GetChargingRate() < (double) this.brain.weaponCtrl.chargeRate)
        return;
      this.player.SetEnableTap(false);
    }
  }

  private void OnGuard()
  {
    if (!this.player.CheckAttackMode(Player.ATTACK_MODE.ONE_HAND_SWORD) || this.player.isActSpecialAction || !this.isChangeableSpecialAction)
      return;
    this.player.ActSpecialAction();
  }

  private void OnChangeWeapon()
  {
    int changeIndex = this.brain.weaponCtrl.changeIndex;
    if (changeIndex < 0 || this.player.equipWeaponList.Count <= changeIndex || changeIndex == this.player.weaponIndex || this.player.equipWeaponList[changeIndex] == null)
      return;
    this.player.ActChangeWeapon(this.player.equipWeaponList[changeIndex], changeIndex);
    this.brain.weaponCtrl.ResetChangeIndex();
  }

  private void LateUpdate() => this.UpdateRegionTarget();

  private void UpdateRegionTarget()
  {
    if (Object.op_Equality((Object) this.player, (Object) null))
      return;
    this.player.targetingPointList.Clear();
    if (Object.op_Equality((Object) this.player.actionTarget, (Object) null))
      return;
    Enemy actionTarget = this.player.actionTarget as Enemy;
    if (Object.op_Equality((Object) actionTarget, (Object) null) || actionTarget.isDead || !actionTarget.enableTargetPoint)
      return;
    TargetPoint[] targetPoints = actionTarget.targetPoints;
    if (targetPoints == null || targetPoints.Length == 0)
      return;
    TargetPoint targetPoint1 = (TargetPoint) null;
    float num = float.MaxValue;
    Vector2 vector2Xz = this.player._transform.position.ToVector2XZ();
    Vector2 forwardXz = this.player.forwardXZ;
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
    this.player.targetingPointList.Add(targetPoint1);
  }

  public void UseSkill()
  {
    if (Object.op_Inequality((Object) this.nonPlayer, (Object) null))
      this.nonPlayer = ((Component) this).gameObject.GetComponent<NonPlayer>();
    Debug.Log((object) nameof (UseSkill));
    this.nonPlayer.NPCSkillAction(0);
  }

  public void SetPose(bool isActivePose) => this.isPose = isActivePose;
}
