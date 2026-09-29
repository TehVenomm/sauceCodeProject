// Decompiled with JetBrains decompiler
// Type: SnatchController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class SnatchController
{
  private int[] attackIds = new int[5]
  {
    0,
    14,
    15,
    16 /*0x10*/,
    17
  };
  private static readonly int HASH_ANIMATOR_MOVE_LOOP = Animator.StringToHash("attack_94_atk_pose");
  private InGameSettingsManager.Player.OneHandSwordActionInfo ohsInfo;
  private bool isCtrlActive;
  private Player owner;
  private Enemy target;
  private Transform snatchBulletTrans;
  private Transform snatchTrans;
  private Transform handTrans;
  private SnatchLineRenderer renderer;
  private bool isReached;
  private bool isHit;
  private bool isShotReleased;
  private bool isLoopStart;
  private float animStateTimer;
  private float animStateTimeLimit;
  private float animStateTimerForMoveLoop;
  private float animStateTimeLimitForMoveLoop;

  public SnatchController.STATE state { get; private set; }

  public void Init(Player player, SnatchLineRenderer lineRenderer)
  {
    this.ohsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
    this.owner = player;
    this.renderer = lineRenderer;
    this.animStateTimeLimit = this.ohsInfo.Soul_AnimStateTimeLimit;
    this.animStateTimeLimitForMoveLoop = this.ohsInfo.Soul_AnimStateTimeLimitForMoveLoop;
    this.SetState(SnatchController.STATE.NONE);
  }

  public void OnLoadComplete() => this.isCtrlActive = true;

  public void Update()
  {
    if (!this.isCtrlActive)
      return;
    this.animStateTimer += Time.deltaTime;
    if (Object.op_Inequality((Object) this.snatchTrans, (Object) null))
      this.snatchTrans.rotation = this.owner._rotation;
    if (Object.op_Inequality((Object) this.handTrans, (Object) null))
      this.renderer.SetPositonStart(this.handTrans.position);
    if (Object.op_Inequality((Object) this.snatchTrans, (Object) null))
      this.renderer.SetPositionEnd(this.snatchTrans.position);
    else if (Object.op_Inequality((Object) this.snatchBulletTrans, (Object) null))
      this.renderer.SetPositionEnd(this.snatchBulletTrans.position);
    switch (this.state)
    {
      case SnatchController.STATE.NONE:
        if (!Object.op_Inequality((Object) this.owner, (Object) null) || !Object.op_Inequality((Object) this.owner.animator, (Object) null))
          break;
        AnimatorStateInfo animatorStateInfo = this.owner.animator.GetCurrentAnimatorStateInfo(0);
        if (((AnimatorStateInfo) ref animatorStateInfo).shortNameHash != SnatchController.HASH_ANIMATOR_MOVE_LOOP)
          break;
        this.animStateTimerForMoveLoop += Time.deltaTime;
        if ((double) this.animStateTimerForMoveLoop <= (double) this.animStateTimeLimitForMoveLoop)
          break;
        this.owner.SetNextTrigger();
        this.animStateTimerForMoveLoop = 0.0f;
        break;
      case SnatchController.STATE.SHOT:
      case SnatchController.STATE.SHOT_RELEASE:
        if (this.IsReached())
        {
          this.owner.OnSnatchMoveEnd(1);
          break;
        }
        if (!this.IsAnimStateTimeLimit())
          break;
        this.owner.OnSnatchMoveEnd(1);
        break;
      case SnatchController.STATE.SNATCH:
        if (!Object.op_Inequality((Object) this.target, (Object) null) || !this.target.isDead)
          break;
        this.owner.SetNextTrigger(1);
        this.Cancel();
        break;
      case SnatchController.STATE.MOVE:
        this.owner.CheckSnatchMove();
        break;
      case SnatchController.STATE.MOVE_LOOP:
        if (this.owner.IsArrivalPosition(this.GetSnatchPos()))
        {
          this.owner.OnSnatchMoveEnd();
          this.animStateTimerForMoveLoop = 0.0f;
          break;
        }
        if (!this.owner.IsCoopNone() && !this.owner.IsOriginal())
          break;
        this.animStateTimerForMoveLoop += Time.deltaTime;
        if ((double) this.animStateTimerForMoveLoop <= (double) this.animStateTimeLimitForMoveLoop)
          break;
        this.owner.OnSnatchMoveEnd();
        this.animStateTimerForMoveLoop = 0.0f;
        break;
    }
  }

  public void Cancel() => this.OnArrive();

  public void OnShot() => this.SetState(SnatchController.STATE.SHOT);

  public void OnHit(int enemyId, Vector3 hitPoint)
  {
    this.target = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId) as Enemy;
    if (FieldManager.IsValidInGameNoBoss() && !this.owner.IsCoopNone() && !this.owner.IsOriginal())
      this.target = (Enemy) null;
    if (this.isHit)
      return;
    this.isHit = true;
    if (MonoBehaviourSingleton<SoundManager>.IsValid())
      SoundManager.PlayOneShotSE(this.ohsInfo.Soul_SnatchHitSeId, hitPoint);
    EffectManager.OneShot(this.owner.isBoostMode ? this.ohsInfo.Soul_SnatchHitEffectOnBoostMode : this.ohsInfo.Soul_SnatchHitEffect, hitPoint, Quaternion.identity);
    this.snatchTrans = EffectManager.GetEffect(this.ohsInfo.Soul_SnatchHitRemainEffect);
    this.renderer.SetPositionEnd(this.snatchTrans.position);
    Vector3 vector3_1 = Vector3.zero;
    Vector3 vector3_2 = Vector3.op_Subtraction(this.owner._position, hitPoint);
    if ((double) ((Vector3) ref vector3_2).magnitude > (double) this.ohsInfo.Soul_MoveStopRange)
    {
      vector3_2 = Vector3.op_Subtraction(this.owner._position, hitPoint);
      vector3_1 = Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, this.ohsInfo.Soul_MoveStopRange);
    }
    this.snatchTrans.position = Vector3.op_Addition(hitPoint, vector3_1);
    this.snatchTrans.rotation = this.owner._rotation;
    if (Object.op_Inequality((Object) this.target, (Object) null))
    {
      this.snatchTrans.parent = this.target._transform;
      this.target.stackBuffCtrl.IncrementStackCount(StackBuffController.STACK_TYPE.SNATCH);
    }
    switch (this.state)
    {
      case SnatchController.STATE.SHOT_RELEASE:
        if (Object.op_Inequality((Object) this.target, (Object) null))
          this.target.stackBuffCtrl.DecrementStackCount(StackBuffController.STACK_TYPE.SNATCH);
        this.isShotReleased = true;
        this.SetState(SnatchController.STATE.MOVE);
        goto case SnatchController.STATE.MOVE;
      case SnatchController.STATE.MOVE:
        if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
          break;
        this.owner.playerSender.OnSnatch(enemyId, hitPoint);
        break;
      default:
        this.owner.SetNextTrigger();
        this.SetState(SnatchController.STATE.SNATCH);
        goto case SnatchController.STATE.MOVE;
    }
  }

  public void OnRelease()
  {
    switch (this.state)
    {
      case SnatchController.STATE.SHOT:
        this.SetState(SnatchController.STATE.SHOT_RELEASE);
        break;
      case SnatchController.STATE.SNATCH:
        this.SetState(SnatchController.STATE.MOVE);
        break;
    }
  }

  public void OnArrive()
  {
    if (Object.op_Inequality((Object) this.snatchTrans, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.snatchTrans).gameObject);
      this.snatchTrans = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.target, (Object) null))
      this.target.stackBuffCtrl.DecrementStackCount(StackBuffController.STACK_TYPE.SNATCH);
    if (Object.op_Inequality((Object) this.renderer, (Object) null))
      this.renderer.SetInvisible();
    this.target = (Enemy) null;
    this.isReached = false;
    this.isShotReleased = false;
    this.isHit = false;
    this.isLoopStart = false;
    this.animStateTimer = 0.0f;
    this.SetState(SnatchController.STATE.NONE);
  }

  public void OnReach() => this.isReached = true;

  private void SetState(SnatchController.STATE state) => this.state = state;

  public void StartMoveLoop() => this.SetState(SnatchController.STATE.MOVE_LOOP);

  public void SetSnatchBulletTrans(Transform trans)
  {
    this.snatchBulletTrans = trans;
    this.handTrans = this.owner.FindNode("L_Hand");
    this.renderer.SetPositonStart(this.handTrans.position);
    this.renderer.SetPositionEnd(trans.position);
    this.renderer.SetVisible();
  }

  public void ActivateLoopStart() => this.isLoopStart = true;

  public Vector3 GetSnatchPos()
  {
    if (Object.op_Equality((Object) this.snatchTrans, (Object) null))
      return Vector3.zero;
    Vector3 position = this.snatchTrans.position;
    position.y = 0.0f;
    return position;
  }

  public bool GetSnatchPos(out Vector3 pos)
  {
    pos = Vector3.zero;
    bool snatchPos = false;
    if (this.isCtrlActive && Object.op_Inequality((Object) this.snatchTrans, (Object) null))
    {
      pos = this.snatchTrans.position;
      pos.y = 0.0f;
      snatchPos = true;
    }
    return snatchPos;
  }

  public int GetAttackId(SelfController.FLICK_DIRECTION direction)
  {
    return this.attackIds[(int) direction];
  }

  public bool IsFlickedAttack(int attackID) => Array.IndexOf<int>(this.attackIds, attackID) > 0;

  public bool IsSnatching() => this.state == SnatchController.STATE.SNATCH;

  public bool IsMove() => this.state == SnatchController.STATE.MOVE;

  public bool IsMoveLoop() => this.state == SnatchController.STATE.MOVE_LOOP;

  public bool IsMoveLoopStart() => this.isLoopStart;

  public bool IsReached() => this.isReached;

  public bool IsShotReleased() => this.isShotReleased;

  public bool IsAnimStateTimeLimit()
  {
    return (double) this.animStateTimer > (double) this.animStateTimeLimit;
  }

  public enum STATE
  {
    NONE,
    SHOT,
    SHOT_RELEASE,
    SNATCH,
    MOVE,
    MOVE_LOOP,
  }
}
