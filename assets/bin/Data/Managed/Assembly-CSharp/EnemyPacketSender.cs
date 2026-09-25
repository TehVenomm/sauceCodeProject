// Decompiled with JetBrains decompiler
// Type: EnemyPacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EnemyPacketSender : CharacterPacketSender
{
  protected Enemy enemy => (Enemy) this.owner;

  public override bool IsEnableWaitSync() => true;

  public override void OnLoadComplete(bool promise = true)
  {
    if (!this.enableSend || !this.owner.IsMirror())
      return;
    Coop_Model_EnemyLoadComplete model = new Coop_Model_EnemyLoadComplete();
    model.id = this.owner.id;
    this.SendToExtra<Coop_Model_EnemyLoadComplete>(this.owner.coopClientId, model, promise);
  }

  public override void OnRecvLoadComplete(int to_client_id)
  {
    if (this.owner.IsCoopNone())
      this.owner.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
    this.SendInitialize(to_client_id);
  }

  public override void SendInitialize(int to_client_id = 0)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyInitialize model = new Coop_Model_EnemyInitialize();
    this.SetupEnemyInitializeModel(model, to_client_id != 0, false);
    if (to_client_id == 0)
    {
      this.SendBroadcast<Coop_Model_EnemyInitialize>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
      {
        this.SetupEnemyInitializeModel(send_model as Coop_Model_EnemyInitialize, false, true);
        return true;
      }));
    }
    else
    {
      this.SendToExtra<Coop_Model_EnemyInitialize>(to_client_id, model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
      {
        this.SetupEnemyInitializeModel(send_model as Coop_Model_EnemyInitialize, true, true);
        return true;
      }));
      if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
        MonoBehaviourSingleton<CoopManager>.I.coopStage.SendSyncPlayerRecord(to_client_id);
    }
    this.SendActionHistory(to_client_id);
  }

  public void SetupEnemyInitializeModel(
    Coop_Model_EnemyInitialize model,
    bool send_to,
    bool resend)
  {
    model.id = this.owner.id;
    if (!resend && this.actionHistoryData != null)
    {
      model.pos = this.actionHistoryData.startPos;
      model.dir = this.actionHistoryData.startDir;
    }
    else
      model.SetSyncPosition(this.owner);
    model.sid = this.enemy.id;
    model.hp = this.enemy.hp;
    model.hpMax = this.enemy.hpMax;
    model.hpDamageRate = this.enemy.damageHpRate;
    model.downTotal = this.enemy.downTotal;
    model.downCount = this.enemy.downCount;
    model.concussionTotal = this.enemy.concussionTotal;
    model.concussionMax = this.enemy.concussionMax;
    model.concussionExtend = this.enemy.concussionExtend;
    model.badStatusMax = this.enemy.badStatusMax;
    model.SetRegionWorks(this.enemy.regionWorks);
    model.nowAngryId = this.enemy.NowAngryID;
    model.execAngryIds = this.enemy.ExecAngryIDList;
    model.target_id = Object.op_Inequality((Object) this.enemy.actionTarget, (Object) null) ? this.enemy.actionTarget.id : -1;
    model.buff_sync_param = this.enemy.buffParam.CreateSyncParam();
    model.cntAtkSyncParam = this.enemy.continusAttackParam.CreateSyncParam();
    model.barrierHp = (int) this.enemy.BarrierHp;
    model.isHiding = this.enemy.isHiding;
    model.shieldHp = (int) this.enemy.ShieldHp;
    model.grabHp = (int) this.enemy.GrabHp;
    model.bulletIndex = this.enemy.bulletIndex;
    model.walkSpeedRateFromTable = this.enemy.walkSpeedRateFromTable;
    model.aegisSetupParam = this.enemy.GetAegisSetupParam();
    model.changeElementIcon = this.enemy.changeElementIcon;
    model.changeWeakElementIcon = this.enemy.changeWeakElementIcon;
    model.changeToleranceRegionId = this.enemy.changeToleranceRegionId;
    model.changeToleranceScroll = this.enemy.changeToleranceScroll;
    model.shaderSyncParam = this.enemy.blendColorCtrl.GetShaderParamList();
    model.deadReviveCount = this.enemy.deadReviveCount;
    model.isFirstMadMode = this.enemy.isFirstMadMode;
    if (Object.op_Inequality((Object) this.enemy.tailController, (Object) null))
      model.tailPosList = this.enemy.tailController.PreviousPositionList;
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    model.recoveredHP = MonoBehaviourSingleton<InGameRecorder>.I.GetEnemyRecoveredHpById(this.enemy.id);
  }

  public virtual void OnUpdateBleedDamage(Enemy.BleedSyncData sync_data)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyUpdateBleedDamage model = new Coop_Model_EnemyUpdateBleedDamage();
    model.id = this.owner.id;
    model.sync_data = sync_data;
    this.SendBroadcast<Coop_Model_EnemyUpdateBleedDamage>(model);
  }

  public virtual void OnUpdateShadowSealing(Enemy.ShadowSealingSyncData sync_data)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyUpdateShadowSealing model = new Coop_Model_EnemyUpdateShadowSealing();
    model.id = this.owner.id;
    model.sync_data = sync_data;
    this.SendBroadcast<Coop_Model_EnemyUpdateShadowSealing>(model);
  }

  public virtual void OnUpdateBombArrow(int regionId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyUpdateBombArrow model = new Coop_Model_EnemyUpdateBombArrow();
    model.id = this.owner.id;
    model.regionId = regionId;
    this.SendBroadcast<Coop_Model_EnemyUpdateBombArrow>(model);
  }

  public virtual void OnActAngry(int angryActionId, uint angryId)
  {
    Coop_Model_EnemyAngry coopModelEnemyAngry = new Coop_Model_EnemyAngry();
    coopModelEnemyAngry.id = this.owner.id;
    coopModelEnemyAngry.angryActionId = angryActionId;
    coopModelEnemyAngry.angryId = angryId;
    coopModelEnemyAngry.execAngryIds = this.enemy.ExecAngryIDList;
    coopModelEnemyAngry.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_EnemyAngry>(coopModelEnemyAngry, true);
    this.StackActionHistory((Coop_Model_ObjectBase) coopModelEnemyAngry, true);
  }

  public virtual void OnActStep(int motion_id)
  {
    Coop_Model_EnemyStep coopModelEnemyStep = new Coop_Model_EnemyStep();
    coopModelEnemyStep.id = this.owner.id;
    coopModelEnemyStep.SetSyncPosition(this.owner);
    coopModelEnemyStep.motion_id = motion_id;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_EnemyStep>(coopModelEnemyStep);
    this.StackActionHistory((Coop_Model_ObjectBase) coopModelEnemyStep, true);
  }

  public virtual void OnReviveRegion(int region_id)
  {
    if (this.enableSend && this.owner.IsOriginal())
    {
      Coop_Model_EnemyReviveRegion model = new Coop_Model_EnemyReviveRegion();
      model.id = this.owner.id;
      model.region_id = region_id;
      this.SendBroadcast<Coop_Model_EnemyReviveRegion>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model => !Object.op_Equality((Object) this.owner, (Object) null) && (int) this.enemy.regionWorks[(send_model as Coop_Model_EnemyReviveRegion).region_id].hp > 0));
    }
    this.ClearActionHistory();
  }

  public virtual void OnSetWarp()
  {
    Coop_Model_EnemyWarp coopModelEnemyWarp = new Coop_Model_EnemyWarp();
    coopModelEnemyWarp.id = this.owner.id;
    coopModelEnemyWarp.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_EnemyWarp>(coopModelEnemyWarp);
    this.StackActionHistory((Coop_Model_ObjectBase) coopModelEnemyWarp, false);
  }

  public void TargetRandamShotEvent(List<Enemy.RandomShotInfo.TargetInfo> targets)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyTargetShotEvent model = new Coop_Model_EnemyTargetShotEvent();
    model.id = this.owner.id;
    model.targets = targets;
    this.SendBroadcast<Coop_Model_EnemyTargetShotEvent>(model);
  }

  public void TargetRandamShotEvent(List<Vector3> points)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyRandomShotEvent model = new Coop_Model_EnemyRandomShotEvent();
    model.id = this.owner.id;
    model.points = points;
    this.SendBroadcast<Coop_Model_EnemyRandomShotEvent>(model);
  }

  public void OnReleaseGrabbed(float angle, float power)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyReleasedGrabbedPlayer model = new Coop_Model_EnemyReleasedGrabbedPlayer();
    model.id = this.owner.id;
    model.angle = angle;
    model.power = power;
    this.SendBroadcast<Coop_Model_EnemyReleasedGrabbedPlayer>(model);
  }

  public void OnShotBullet(string atkName, List<Vector3> points, List<Quaternion> rots)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyShot model = new Coop_Model_EnemyShot();
    model.id = this.owner.id;
    model.atkName = atkName;
    model.posList = points;
    model.rotList = rots;
    this.SendBroadcast<Coop_Model_EnemyShot>(model);
  }

  public void OnCreateIceFloor(string atkName, List<Vector3> points, List<Quaternion> rots)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_CreateIceFloor model = new Coop_Model_CreateIceFloor();
    model.id = this.owner.id;
    model.atkName = atkName;
    model.posList = points;
    model.rotList = rots;
    this.SendBroadcast<Coop_Model_CreateIceFloor>(model);
  }

  public void OnCreateActionMine(string atkInfoName, int randSeed)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ActionMine model = new Coop_Model_ActionMine();
    model.type = 3;
    model.id = this.owner.id;
    model.atkInfoName = atkInfoName;
    model.randSeed = randSeed;
    this.SendBroadcast<Coop_Model_ActionMine>(model);
  }

  public void OnCreateReflectBullet(int objId, int randSeed)
  {
    if (!this.enableSend || !this.owner.IsOriginal() && !this.owner.IsMirror())
      return;
    Coop_Model_ActionMine model = new Coop_Model_ActionMine();
    model.type = 2;
    model.id = this.owner.id;
    model.objId = objId;
    model.randSeed = randSeed;
    this.SendBroadcast<Coop_Model_ActionMine>(model);
  }

  public void OnDestroyActionMine(int objId, bool isExplode)
  {
    if (!this.enableSend || !this.owner.IsOriginal() && !this.owner.IsMirror())
      return;
    Coop_Model_ActionMine model = new Coop_Model_ActionMine();
    model.type = isExplode ? 1 : 0;
    model.id = this.owner.id;
    model.objId = objId;
    this.SendBroadcast<Coop_Model_ActionMine>(model);
  }

  public void OnReflectBulletAttack(string atkInfoName, string nodeName, int randSeed)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_ActionMine model = new Coop_Model_ActionMine();
    model.type = 2;
    model.id = this.owner.id;
    model.atkInfoName = atkInfoName;
    model.objId = -1;
    model.nodeName = nodeName;
    model.randSeed = randSeed;
    this.SendBroadcast<Coop_Model_ActionMine>(model);
  }

  public void OnRecoverHp(int recoverValue)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyRecoverHp model = new Coop_Model_EnemyRecoverHp();
    model.id = this.owner.id;
    model.value = recoverValue;
    this.SendBroadcast<Coop_Model_EnemyRecoverHp>(model);
  }

  public void OnTurnUp()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyTurnUp model = new Coop_Model_EnemyTurnUp();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_EnemyTurnUp>(model);
  }

  public void OnEnemySyncTarget(StageObject target)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemySyncTarget model = new Coop_Model_EnemySyncTarget();
    model.id = this.owner.id;
    model.targetId = Object.op_Inequality((Object) target, (Object) null) ? target.id : -1;
    this.SendBroadcast<Coop_Model_EnemySyncTarget>(model);
  }

  public void OnEnemyRegionNodeActivate(int[] regionIDs, bool isRandom = false, int randomSelectedID = -1)
  {
    if (!((Behaviour) this).enabled || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemyRegionNodeActivate model = new Coop_Model_EnemyRegionNodeActivate();
    model.id = this.owner.id;
    model.regionIDs = regionIDs;
    model.isRandom = isRandom;
    model.randomSelectedID = randomSelectedID;
    this.SendBroadcast<Coop_Model_EnemyRegionNodeActivate>(model);
  }

  public void OnSummonAttack(int enemyId, int attackId, Vector3 pos, Vector3 rot, int targetId)
  {
    if (!((Behaviour) this).enabled || !this.owner.IsOriginal())
      return;
    Coop_Model_EnemySummonAttack model = new Coop_Model_EnemySummonAttack();
    model.id = this.owner.id;
    model.enemyId = enemyId;
    model.attackId = attackId;
    model.summonPos = pos;
    model.summonRot = rot;
    model.targetId = targetId;
    this.SendBroadcast<Coop_Model_EnemySummonAttack>(model);
  }
}
