// Decompiled with JetBrains decompiler
// Type: EnemyController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyController : ControllerBase
{
  protected IEnumerator mainCoroutine;
  protected bool isStart;
  protected float startWaitTime;
  private float turnUpTimer;

  private Enemy enemy { get; set; }

  private EnemyBrain enemyBrain { get; set; }

  public InGameSettingsManager.Enemy enemyParameter { get; private set; }

  public InGameSettingsManager.EnemyController parameter { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.enemy = this.character as Enemy;
    this.enemyBrain = this.AttachBrain<EnemyBrain>();
    this.enemyParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy;
  }

  protected override void Start()
  {
    base.Start();
    this.parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.enemyController;
    this.isStart = true;
    if (!this.IsEnableControll())
      return;
    this.OnChangeEnableControll(true);
  }

  public override void SetEnableControll(bool enable, ControllerBase.DISABLE_FLAG flag = ControllerBase.DISABLE_FLAG.DEFAULT)
  {
    base.SetEnableControll(enable, flag);
  }

  public override void OnChangeEnableControll(bool enable)
  {
    base.OnChangeEnableControll(enable);
    if (enable)
    {
      if (!this.isStart || !((Behaviour) this).enabled || !Object.op_Inequality((Object) this.enemy, (Object) null) || this.mainCoroutine != null)
        return;
      this.startWaitTime = this.parameter.startWaitTime;
      this.mainCoroutine = this.AIMain();
      this.StartCoroutine(this.mainCoroutine);
    }
    else
    {
      if (this.mainCoroutine == null)
        return;
      this.StopAllCoroutines();
      this.mainCoroutine = (IEnumerator) null;
    }
  }

  public override void OnActReaction()
  {
    base.OnActReaction();
    if (this.IsEnableControll() && this.mainCoroutine != null)
    {
      this.StopAllCoroutines();
      this.startWaitTime = this.parameter.afterReactionWaitTime;
      this.mainCoroutine = this.AIMain();
      this.StartCoroutine(this.mainCoroutine);
    }
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByEnemy();
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetPositionByEnemy();
  }

  public void OnSetDecoy()
  {
    if (this.enemy.actionID == Character.ACTION_ID.MOVE || this.enemy.actionID == Character.ACTION_ID.ROTATE)
      this.enemy.ActIdle();
    this.enemyBrain.SetNearDecoyTarget();
  }

  public void OnCheckMissDecoy(StageObject decoyObj) => this.enemyBrain.MissDecoyTarget(decoyObj);

  public void Reset()
  {
    this.brain.ResetInitialized();
    this.StopAllCoroutines();
    this.mainCoroutine = (IEnumerator) null;
  }

  public override void OnCharacterInitialized() => base.OnCharacterInitialized();

  protected override void Update()
  {
    base.Update();
    if (!this.enemy.isHiding || !this.enemy.IsOriginal() && !this.enemy.IsCoopNone())
      return;
    if ((double) this.turnUpTimer >= 0.5)
    {
      if (this.brain.opponentMem != null)
        this.brain.opponentMem.Update();
      StageObject targetObjectOfNearest = this.brain.targetCtrl.GetTargetObjectOfNearest();
      if (Object.op_Inequality((Object) targetObjectOfNearest, (Object) null) && (double) Vector2.Distance(targetObjectOfNearest._position.ToVector2XZ(), this.enemy._position.ToVector2XZ()) <= (double) this.enemy.turnUpDistance)
        this.enemy.TurnUp();
      this.turnUpTimer = 0.0f;
    }
    this.turnUpTimer += Time.deltaTime;
  }

  private IEnumerator AIMain()
  {
    while (Object.op_Equality((Object) this.brain, (Object) null) || !this.brain.isInitialized)
      yield return (object) 0;
    while (!this.character.isControllable)
      yield return (object) 0;
    if ((double) this.startWaitTime > 0.0)
      yield return (object) new WaitForSeconds(this.startWaitTime);
    this.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION);
    while (((Behaviour) this).enabled)
    {
      while (this.character.IsMirror())
        yield return (object) new WaitForSeconds(1f);
      while (this.enemy.isHiding || this.enemy.IsHideMotionPlaying())
        yield return (object) null;
      yield return (object) this.StartCoroutine(this.OnAction());
      yield return (object) 0;
    }
    this.mainCoroutine = (IEnumerator) null;
  }

  public IEnumerator OnAction()
  {
    bool is_action = false;
    if (this.brain.moveCtrl.IsRotate())
    {
      is_action = true;
      if (this.brain.isNonActive)
        yield return (object) this.StartCoroutine(this.OnRotate());
      else
        yield return (object) this.StartCoroutine(this.OnRotateOfAction());
      while (this.character.actionID == Character.ACTION_ID.ROTATE)
        yield return (object) 0;
    }
    if (this.brain.moveCtrl.IsSeek())
    {
      is_action = true;
      if (this.brain.isNonActive)
        yield return (object) this.StartCoroutine(this.OnMove());
      else
        yield return (object) this.StartCoroutine(this.OnMoveOfAction());
      while (this.character.actionID == Character.ACTION_ID.MOVE)
        yield return (object) 0;
    }
    if (this.brain.weaponCtrl.IsAttack())
    {
      is_action = true;
      yield return (object) this.StartCoroutine(this.OnAttackOfAction());
    }
    if (is_action)
      this.brain.HandleEvent(BRAIN_EVENT.END_ENEMY_ACTION);
  }

  public IEnumerator OnRotateOfAction()
  {
    if (this.enemyBrain.actionCtrl.nowAction.data.isRotate)
      yield return (object) this.StartCoroutine(this.OnRotateToTarget());
  }

  public IEnumerator OnMoveOfAction()
  {
    EnemyActionController.ActionInfo nowAction = this.enemyBrain.actionCtrl.nowAction;
    if (nowAction.data.isMove && (double) this.brain.targetCtrl.GetDistance() >= (double) nowAction.data.atkRange)
    {
      if (this.brain.param.moveParam.enableActionMoveHoming)
        yield return (object) this.StartCoroutine(this.OnMoveHoming());
      else
        yield return (object) this.StartCoroutine(this.OnMoveToTarget());
    }
  }

  private IEnumerator OnAttackOfAction()
  {
    EnemyActionTable.EnemyActionData nowActData = this.enemyBrain.actionCtrl.nowAction.data;
    if ((double) nowActData.lotteryWaitInterval > 0.0)
      nowActData.lotteryWaitTime = Time.time;
    int i = 0;
    for (int n = nowActData.combiActionTypeInfos.Length; i < n; ++i)
    {
      EnemyActionTable.ActionTypeInfo combiActionTypeInfo = nowActData.combiActionTypeInfos[i];
      switch (combiActionTypeInfo.type)
      {
        case EnemyActionTable.ACTION_TYPE.STEP:
          yield return (object) this.StartCoroutine(this.OnStep());
          break;
        case EnemyActionTable.ACTION_TYPE.STEP_BACK:
          yield return (object) this.StartCoroutine(this.OnStep(true));
          break;
        case EnemyActionTable.ACTION_TYPE.ROTATE:
          yield return (object) this.StartCoroutine(this.OnRotateToTarget());
          break;
        case EnemyActionTable.ACTION_TYPE.MOVE:
          yield return (object) this.StartCoroutine(this.OnMoveToTarget());
          break;
        case EnemyActionTable.ACTION_TYPE.MOVE_HOMING:
          yield return (object) this.StartCoroutine(this.OnMoveHoming());
          break;
        case EnemyActionTable.ACTION_TYPE.ANGRY:
          yield return (object) this.StartCoroutine(this.OnAngry(combiActionTypeInfo, nowActData.angryId));
          break;
        case EnemyActionTable.ACTION_TYPE.ATTACK:
          yield return (object) this.StartCoroutine(this.OnAtk(combiActionTypeInfo));
          break;
        case EnemyActionTable.ACTION_TYPE.MOVE_SIDE:
          yield return (object) this.StartCoroutine(this.OnMoveSideways());
          break;
        case EnemyActionTable.ACTION_TYPE.MOVE_POINT:
          yield return (object) this.StartCoroutine(this.OnMovePoint());
          break;
        case EnemyActionTable.ACTION_TYPE.MOVE_LOOKAT:
          yield return (object) this.StartCoroutine(this.OnMoveLookAt());
          break;
      }
    }
    while (!this.character.IsChangeableAction(Character.ACTION_ID.NONE))
      yield return (object) 0;
    float base_time = nowActData.afterWaitTime;
    if (this.enemyParameter != null)
      base_time = base_time + this.enemyParameter.baseAfterWaitTime + this.GetAfterWaitTimeByLv((int) this.enemy.enemyLevel);
    if (Object.op_Inequality((Object) this.enemy.packetSender, (Object) null))
      base_time = this.enemy.packetSender.GetWaitTime(base_time);
    if ((double) base_time > 0.0)
      yield return (object) new WaitForSeconds(base_time);
  }

  private float GetAfterWaitTimeByLv(int lv)
  {
    if (this.enemyParameter == null || ((IList<float>) this.enemyParameter.afterWaitTimeThresholdsByLv).IsNullOrEmpty<float>() || ((IList<float>) this.enemyParameter.afterWaitTimesByLv).IsNullOrEmpty<float>() || this.enemyParameter.afterWaitTimeThresholdsByLv.Length >= this.enemyParameter.afterWaitTimesByLv.Length)
      return 0.0f;
    int index1 = this.enemyParameter.afterWaitTimesByLv.Length - 1;
    int index2 = 0;
    for (int length = this.enemyParameter.afterWaitTimeThresholdsByLv.Length; index2 < length; ++index2)
    {
      if ((double) lv <= (double) this.enemyParameter.afterWaitTimeThresholdsByLv[index2])
      {
        index1 = index2;
        break;
      }
    }
    return this.enemyParameter.afterWaitTimesByLv[index1];
  }

  public IEnumerator OnStep(bool is_back = false)
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MAX))
      yield return (object) 0;
    Enemy.SUB_MOTION_ID motion_id = Enemy.SUB_MOTION_ID.STEP;
    if (is_back)
      motion_id = Enemy.SUB_MOTION_ID.STEP_BACK;
    this.enemy.ActStep((int) motion_id);
  }

  public IEnumerator OnAtk(EnemyActionTable.ActionTypeInfo actionTypeInfo)
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.ATTACK))
      yield return (object) 0;
    this.character.ActAttack(actionTypeInfo.id);
  }

  public IEnumerator OnAngry(EnemyActionTable.ActionTypeInfo actionTypeInfo, uint angryId)
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.PARALYZE | Character.ACTION_ID.FREEZE))
      yield return (object) 0;
    this.enemy.ActAngry(actionTypeInfo.id, angryId);
  }

  public IEnumerator OnRotateToTarget()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.ROTATE))
      yield return (object) 0;
    if (!this.character.isControllable)
      this.brain.opponentMem.Update();
    if (!this.brain.targetCtrl.IsPlaceTarget(PLACE.FRONT))
    {
      if (this.brain.param.moveParam.motionRotate)
        this.character.ActRotateMotionToTarget();
      else
        this.character.ActRotateToTarget();
    }
  }

  public IEnumerator OnMoveToTarget()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
      yield return (object) 0;
    if (!this.character.isControllable)
      this.brain.opponentMem.Update();
    float distance = this.brain.targetCtrl.GetDistance();
    if ((double) distance >= (double) this.character.moveStopRange)
    {
      float max_length = distance + this.brain.param.moveParam.moveOverDistance;
      if ((double) max_length >= (double) this.brain.param.moveParam.moveMaxLength)
        max_length = this.brain.param.moveParam.moveMaxLength;
      float length = 0.0f;
      if (this.enemyBrain.actionCtrl.GetMoveMaxLength(ref length))
        max_length = length;
      this.character.ActMoveToTarget(max_length);
    }
  }

  public IEnumerator OnRotate()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.ROTATE))
      yield return (object) 0;
    Vector3 vector3 = Vector3.op_Subtraction(this.brain.moveCtrl.targetPos, this.character._position);
    vector3.y = 0.0f;
    if (!Vector3.op_Equality(vector3, Vector3.zero))
    {
      Quaternion quaternion = Quaternion.LookRotation(vector3);
      this.character.ActRotateToDirection(((Quaternion) ref quaternion).eulerAngles.y);
    }
  }

  public IEnumerator OnMove()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
      yield return (object) 0;
    Vector3 vector3 = Vector3.op_Subtraction(this.brain.moveCtrl.targetPos, this.character._position);
    vector3.y = 0.0f;
    float moveMaxLength = this.brain.param.moveParam.moveMaxLength;
    if ((double) moveMaxLength > 0.0)
    {
      float magnitude = ((Vector3) ref vector3).magnitude;
      if ((double) magnitude > (double) moveMaxLength)
        vector3 = Vector3.op_Multiply(vector3, moveMaxLength / magnitude);
    }
    this.character.ActMoveToPosition(Vector3.op_Addition(this.character._position, vector3));
  }

  public IEnumerator OnMoveHoming()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
      yield return (object) 0;
    if (!this.character.isControllable)
      this.brain.opponentMem.Update();
    if ((double) this.brain.targetCtrl.GetDistance() >= (double) this.character.moveStopRange)
      this.character.ActMoveHoming(this.brain.param.moveParam.moveHomingMaxLength);
  }

  public IEnumerator OnMoveSideways()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
      yield return (object) 0;
    if (!this.character.isControllable)
      this.brain.opponentMem.Update();
    this.character.ActMoveSideways();
  }

  public IEnumerator OnMovePoint()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
      yield return (object) 0;
    if (!this.character.isControllable)
      this.brain.opponentMem.Update();
    this.enemy.ActMovePoint(this.enemy.movePointPos);
  }

  public IEnumerator OnMoveLookAt()
  {
    while (!this.character.IsChangeableAction(Character.ACTION_ID.MOVE))
      yield return (object) 0;
    if (!this.character.isControllable)
      this.brain.opponentMem.Update();
    this.enemy.ActMoveLookAt(this.enemy.movePointPos, false);
  }

  public void OnHitAttack(StageObject target)
  {
    this.brain.HandleEvent(BRAIN_EVENT.OWN_ATTACK_HIT, (object) target);
  }

  public void OnReviveRegion(int regionId)
  {
    this.brain.HandleEvent(BRAIN_EVENT.REVIVE_REGION, (object) regionId);
  }
}
