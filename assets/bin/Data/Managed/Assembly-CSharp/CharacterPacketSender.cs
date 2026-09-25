// Decompiled with JetBrains decompiler
// Type: CharacterPacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class CharacterPacketSender : ObjectPacketSender
{
  protected float updatePositionTimer;
  protected bool actUpdateSendFlag;
  protected float actUpdateTimer;
  protected int moveMotion;
  protected int prevSendHp;

  protected Character character => (Character) this.owner;

  public abstract void OnLoadComplete(bool promise = true);

  public abstract void OnRecvLoadComplete(int to_client_id);

  public abstract void SendInitialize(int to_client_id = 0);

  public override void OnUpdate()
  {
    base.OnUpdate();
    if (this.actUpdateSendFlag)
    {
      float num = MonoBehaviourSingleton<InGameSettingsManager>.I.character.moveSendInterval;
      int index = 0;
      for (int count = this.character.periodicSyncOwnerList.Count; index < count; ++index)
      {
        StageObject periodicSyncOwner = (StageObject) this.character.periodicSyncOwnerList[index];
        if (periodicSyncOwner.IsMirror() || periodicSyncOwner.IsPuppet())
        {
          num = MonoBehaviourSingleton<InGameSettingsManager>.I.character.periodicSyncActionPositionCheckTime + MonoBehaviourSingleton<InGameSettingsManager>.I.character.periodicSyncActionPositionApplyTime;
          break;
        }
      }
      this.actUpdateTimer += Time.deltaTime;
      if (this.character.actionID == Character.ACTION_ID.MOVE && (double) this.actUpdateTimer >= (double) num)
      {
        Coop_Model_CharacterMoveVelocity characterMoveVelocity = new Coop_Model_CharacterMoveVelocity();
        characterMoveVelocity.id = this.owner.id;
        characterMoveVelocity.time = this.actUpdateTimer;
        characterMoveVelocity.pos = this.owner._position;
        characterMoveVelocity.motion_id = this.moveMotion;
        characterMoveVelocity.target_id = Object.op_Inequality((Object) this.character.actionTarget, (Object) null) ? this.character.actionTarget.id : -1;
        if (this.enableSend && this.owner.IsOriginal())
          this.SendBroadcast<Coop_Model_CharacterMoveVelocity>(characterMoveVelocity);
        this.StackActionHistory((Coop_Model_ObjectBase) characterMoveVelocity, true);
        this.actUpdateTimer = 0.0f;
      }
    }
    if (!this.character.isControllable && !this.character.enableMotionCancel)
      return;
    this.PassNeedWaitSyncTime(Time.deltaTime);
  }

  public virtual void OnSetActionTarget(StageObject target)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_CharacterActionTarget model = new Coop_Model_CharacterActionTarget();
    model.id = this.owner.id;
    model.target_id = Object.op_Inequality((Object) target, (Object) null) ? target.id : -1;
    this.SendBroadcast<Coop_Model_CharacterActionTarget>(model);
  }

  public virtual void OnUpdateActionPosition(string trigger)
  {
    Coop_Model_CharacterUpdateActionPosition updateActionPosition = new Coop_Model_CharacterUpdateActionPosition();
    updateActionPosition.id = this.owner.id;
    updateActionPosition.trigger = trigger;
    updateActionPosition.act_pos = this.character.actionPosition;
    updateActionPosition.act_pos_f = this.character.actionPositionFlag;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterUpdateActionPosition>(updateActionPosition);
    this.StackActionHistory((Coop_Model_ObjectBase) updateActionPosition, false);
  }

  public virtual void OnUpdateDirection(string trigger)
  {
    Coop_Model_CharacterUpdateDirection characterUpdateDirection1 = new Coop_Model_CharacterUpdateDirection();
    characterUpdateDirection1.id = this.owner.id;
    characterUpdateDirection1.trigger = trigger;
    Coop_Model_CharacterUpdateDirection characterUpdateDirection2 = characterUpdateDirection1;
    Quaternion rotation = this.owner._rotation;
    double y1 = (double) ((Quaternion) ref rotation).eulerAngles.y;
    characterUpdateDirection2.dir = (float) y1;
    if (Vector3.op_Equality(this.character.lerpRotateVec, Vector3.zero))
    {
      characterUpdateDirection1.lerp_dir = characterUpdateDirection1.dir;
    }
    else
    {
      Coop_Model_CharacterUpdateDirection characterUpdateDirection3 = characterUpdateDirection1;
      Quaternion quaternion = Quaternion.LookRotation(this.character.lerpRotateVec);
      double y2 = (double) ((Quaternion) ref quaternion).eulerAngles.y;
      characterUpdateDirection3.lerp_dir = (float) y2;
    }
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterUpdateDirection>(characterUpdateDirection1);
    this.StackActionHistory((Coop_Model_ObjectBase) characterUpdateDirection1, false);
  }

  public virtual void OnPeriodicSyncActionPosition(Character.PeriodicSyncActionPositionInfo info)
  {
    Coop_Model_CharacterPeriodicSyncActionPosition syncActionPosition = new Coop_Model_CharacterPeriodicSyncActionPosition();
    syncActionPosition.id = this.owner.id;
    syncActionPosition.info = info;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterPeriodicSyncActionPosition>(syncActionPosition);
    this.StackActionHistory((Coop_Model_ObjectBase) syncActionPosition, false);
  }

  public virtual void OnActIdle(bool is_sync)
  {
    if (this.enableSend && this.owner.IsOriginal() && is_sync)
    {
      Coop_Model_CharacterIdle model = new Coop_Model_CharacterIdle();
      model.id = this.owner.id;
      model.SetSyncPosition(this.owner);
      this.SendBroadcast<Coop_Model_CharacterIdle>(model);
    }
    this.ClearActionHistory();
  }

  public virtual void OnActAttack(
    int id,
    bool sync_immediately,
    int syncRandomSeed = 0,
    string _motionLayerName = "",
    string _motionStateName = "")
  {
    Coop_Model_CharacterAttack modelCharacterAttack = new Coop_Model_CharacterAttack();
    modelCharacterAttack.id = this.owner.id;
    modelCharacterAttack.SetSyncPosition(this.owner);
    modelCharacterAttack.attack_id = id;
    modelCharacterAttack.motionLayerName = _motionLayerName;
    modelCharacterAttack.motionStateName = _motionStateName;
    modelCharacterAttack.act_pos = this.character.actionPosition;
    modelCharacterAttack.act_pos_f = this.character.actionPositionFlag;
    modelCharacterAttack.sync_immediately = sync_immediately;
    modelCharacterAttack.syncRandomSeed = syncRandomSeed;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterAttack>(modelCharacterAttack);
    this.StackActionHistory((Coop_Model_ObjectBase) modelCharacterAttack, true);
  }

  public virtual void OnActMoveVelocity(int motion_id)
  {
    this.actUpdateTimer = 0.0f;
    this.actUpdateSendFlag = true;
    this.moveMotion = motion_id;
  }

  public virtual void OnActMoveToPosition(Vector3 target_pos)
  {
    Coop_Model_CharacterMoveToPosition characterMoveToPosition = new Coop_Model_CharacterMoveToPosition();
    characterMoveToPosition.id = this.owner.id;
    characterMoveToPosition.SetSyncPosition(this.owner);
    characterMoveToPosition.target_pos = target_pos;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterMoveToPosition>(characterMoveToPosition);
    this.StackActionHistory((Coop_Model_ObjectBase) characterMoveToPosition, true);
  }

  public virtual void OnActMoveHoming(float max_length)
  {
    Coop_Model_CharacterMoveHoming characterMoveHoming = new Coop_Model_CharacterMoveHoming();
    characterMoveHoming.id = this.owner.id;
    characterMoveHoming.SetSyncPosition(this.owner);
    characterMoveHoming.act_pos = this.character.actionPosition;
    characterMoveHoming.act_pos_f = this.character.actionPositionFlag;
    characterMoveHoming.max_length = max_length;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterMoveHoming>(characterMoveHoming);
    this.StackActionHistory((Coop_Model_ObjectBase) characterMoveHoming, true);
  }

  public virtual void OnActMoveSideways(int moveAngleSign)
  {
    Coop_Model_CharacterMoveSideways characterMoveSideways = new Coop_Model_CharacterMoveSideways();
    characterMoveSideways.id = this.owner.id;
    characterMoveSideways.SetSyncPosition(this.owner);
    characterMoveSideways.actionPos = this.character.actionPosition;
    characterMoveSideways.actionPosFlag = this.character.actionPositionFlag;
    characterMoveSideways.moveAngleSign = moveAngleSign;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterMoveSideways>(characterMoveSideways);
    this.StackActionHistory((Coop_Model_ObjectBase) characterMoveSideways, true);
  }

  public virtual void OnActMovePoint(Vector3 targetPos)
  {
    Coop_Model_CharacterMovePoint characterMovePoint = new Coop_Model_CharacterMovePoint();
    characterMovePoint.id = this.owner.id;
    characterMovePoint.SetSyncPosition(this.owner);
    characterMovePoint.targetPos = targetPos;
    if (((Behaviour) this).enabled && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterMovePoint>(characterMovePoint);
    this.StackActionHistory((Coop_Model_ObjectBase) characterMovePoint, true);
  }

  public void OnActMoveLookAt(Vector3 moveLookAtPos)
  {
    Coop_Model_CharacterMoveLookAt characterMoveLookAt = new Coop_Model_CharacterMoveLookAt();
    characterMoveLookAt.id = this.owner.id;
    characterMoveLookAt.SetSyncPosition(this.owner);
    characterMoveLookAt.moveLookAtPos = moveLookAtPos;
    if (((Behaviour) this).enabled && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterMoveLookAt>(characterMoveLookAt);
    this.StackActionHistory((Coop_Model_ObjectBase) characterMoveLookAt, true);
  }

  public virtual void OnActRotate(float direction)
  {
    Coop_Model_CharacterRotate modelCharacterRotate = new Coop_Model_CharacterRotate();
    modelCharacterRotate.id = this.owner.id;
    modelCharacterRotate.SetSyncPosition(this.owner);
    modelCharacterRotate.target_dir = direction;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterRotate>(modelCharacterRotate);
    this.StackActionHistory((Coop_Model_ObjectBase) modelCharacterRotate, true);
  }

  public virtual void OnActRotateMotion(float direction)
  {
    Coop_Model_CharacterRotateMotion characterRotateMotion = new Coop_Model_CharacterRotateMotion();
    characterRotateMotion.id = this.owner.id;
    characterRotateMotion.SetSyncPosition(this.owner);
    characterRotateMotion.target_dir = direction;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterRotateMotion>(characterRotateMotion);
    this.StackActionHistory((Coop_Model_ObjectBase) characterRotateMotion, true);
  }

  public void OnReactionDelay(List<Character.DelayReactionInfo> reactionList)
  {
    Coop_Model_CharacterReactionDelay characterReactionDelay = new Coop_Model_CharacterReactionDelay();
    characterReactionDelay.id = this.owner.id;
    characterReactionDelay.SetSyncPosition(this.owner);
    characterReactionDelay.reactionInfoList = reactionList;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_CharacterReactionDelay>(characterReactionDelay);
    this.StackActionHistory((Coop_Model_ObjectBase) characterReactionDelay, true);
  }

  public void OnSendContinusAttackSync(ContinusAttackParam.SyncParam syncParam)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_CharacterContinusAttackSync model = new Coop_Model_CharacterContinusAttackSync();
    model.id = this.owner.id;
    model.sync_param = syncParam;
    this.SendBroadcast<Coop_Model_CharacterContinusAttackSync>(model);
  }

  public void OnSendBuffSync(BuffParam.BuffSyncParam sync_param)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_CharacterBuffSync model = new Coop_Model_CharacterBuffSync();
    model.id = this.owner.id;
    model.sync_param = sync_param;
    this.SendBroadcast<Coop_Model_CharacterBuffSync>(model);
  }

  public void OnBuffReceive(BuffParam.BUFFTYPE type, int value, float time)
  {
    if (!this.enableSend || !this.owner.IsPuppet() && !this.owner.IsMirror())
      return;
    Coop_Model_CharacterBuffReceive model = new Coop_Model_CharacterBuffReceive();
    model.id = this.owner.id;
    model.type = (int) type;
    model.value = value;
    model.time = time;
    this.SendTo<Coop_Model_CharacterBuffReceive>(this.owner.coopClientId, model);
  }

  public void OnBuffRoutine(
    BuffParam.BUFFTYPE type,
    int value,
    int fromObjectID,
    int fromEquipIndex,
    int fromSkillIndex)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_CharacterBuffRoutine model = new Coop_Model_CharacterBuffRoutine();
    model.id = this.owner.id;
    model.type = (int) type;
    model.value = value;
    model.fromObjectID = fromObjectID;
    model.fromEquipIndex = fromEquipIndex;
    model.fromSkillIndex = fromSkillIndex;
    this.SendBroadcast<Coop_Model_CharacterBuffRoutine>(model);
  }

  public virtual void OnEndAction()
  {
    if (this.character.actionID != Character.ACTION_ID.MOVE || !this.actUpdateSendFlag)
      return;
    if (this.enableSend && this.owner.IsOriginal())
    {
      Coop_Model_CharacterMoveVelocityEnd model = new Coop_Model_CharacterMoveVelocityEnd();
      model.id = this.owner.id;
      model.time = this.actUpdateTimer;
      model.pos = this.owner._position;
      Coop_Model_CharacterMoveVelocityEnd characterMoveVelocityEnd = model;
      Quaternion rotation = this.owner._rotation;
      double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
      characterMoveVelocityEnd.direction = (float) y;
      model.sync_speed = this.character.moveSyncSpeed;
      model.motion_id = this.moveMotion;
      this.SendBroadcast<Coop_Model_CharacterMoveVelocityEnd>(model);
    }
    this.actUpdateTimer = 0.0f;
    this.actUpdateSendFlag = false;
  }

  public void OnActReaction(Character.ReactionInfo info, bool isSync)
  {
    Coop_Model_CharacterReaction characterReaction = new Coop_Model_CharacterReaction();
    characterReaction.id = this.owner.id;
    characterReaction.SetSyncPosition(this.owner);
    characterReaction.reactionType = (int) info.reactionType;
    characterReaction.blowForce = info.blowForce;
    characterReaction.loopTime = info.loopTime;
    characterReaction.targetId = info.targetId;
    characterReaction.deadReviveCount = info.deadReviveCount;
    if (isSync)
      this.SendBroadcast<Coop_Model_CharacterReaction>(characterReaction);
    this.StackActionHistory((Coop_Model_ObjectBase) characterReaction, true);
  }

  public void OnActDead()
  {
    Coop_Model_CharacterDead modelCharacterDead = new Coop_Model_CharacterDead();
    modelCharacterDead.id = this.owner.id;
    modelCharacterDead.SetSyncPosition(this.owner);
    if (this.enableSend)
      this.SendBroadcast<Coop_Model_CharacterDead>(modelCharacterDead, true);
    this.StackActionHistory((Coop_Model_ObjectBase) modelCharacterDead, true);
  }
}
