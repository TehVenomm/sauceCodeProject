// Decompiled with JetBrains decompiler
// Type: EnemyPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyPacketReceiver : CharacterPacketReceiver
{
  protected Enemy enemy => (Enemy) this.owner;

  protected override bool CheckFilterPacket(CoopPacket packet)
  {
    return this.filterMode == ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE && packet.packetType == PACKET_TYPE.ENEMY_INITIALIZE || base.CheckFilterPacket(packet);
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    switch (packet.packetType)
    {
      case PACKET_TYPE.ENEMY_LOAD_COMPLETE:
        if (Object.op_Inequality((Object) this.enemy.enemySender, (Object) null))
        {
          this.enemy.enemySender.OnRecvLoadComplete(packet.fromClientId);
          break;
        }
        break;
      case PACKET_TYPE.ENEMY_INITIALIZE:
        if (this.enemy.isLoading)
          return false;
        Coop_Model_EnemyInitialize model1 = packet.GetModel<Coop_Model_EnemyInitialize>();
        this.enemy.ApplySyncPosition(model1.pos, model1.dir, !this.enemy.isCoopInitialized);
        this.enemy.hp = model1.hp;
        this.enemy.hpMax = model1.hpMax;
        this.enemy.damageHpRate = model1.hpDamageRate;
        this.enemy.downTotal = model1.downTotal;
        this.enemy.downCount = model1.downCount;
        this.enemy.concussionTotal = model1.concussionTotal;
        this.enemy.concussionMax = model1.concussionMax;
        this.enemy.concussionExtend = model1.concussionExtend;
        this.enemy.badStatusMax = model1.badStatusMax;
        this.enemy.NowAngryID = model1.nowAngryId;
        this.enemy.ExecAngryIDList = model1.execAngryIds;
        this.enemy.BarrierHp = (XorInt) model1.barrierHp;
        this.enemy.isHiding = model1.isHiding;
        this.enemy.ShieldHp = (XorInt) model1.shieldHp;
        this.enemy.GrabHp = (XorInt) model1.grabHp;
        this.enemy.bulletIndex = model1.bulletIndex;
        this.enemy.walkSpeedRateFromTable = model1.walkSpeedRateFromTable;
        this.enemy.SetupAegis(model1.aegisSetupParam);
        this.enemy.changeElementIcon = model1.changeElementIcon;
        this.enemy.changeWeakElementIcon = model1.changeWeakElementIcon;
        this.enemy.changeToleranceRegionId = model1.changeToleranceRegionId;
        this.enemy.changeToleranceScroll = model1.changeToleranceScroll;
        this.enemy.ProcessElementToleranceChange(this.enemy.changeToleranceRegionId, this.enemy.changeToleranceScroll);
        this.enemy.SetBlendColor(model1.shaderSyncParam);
        this.enemy.deadReviveCount = model1.deadReviveCount;
        this.enemy.isFirstMadMode = model1.isFirstMadMode;
        this.enemy.regionWorks.ApplyRegionWorks(model1);
        this.enemy.UpdateRegionVisual();
        StageObject target1 = (StageObject) null;
        if (model1.target_id >= 0)
          target1 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model1.target_id);
        this.enemy.SetActionTarget(target1, true);
        this.enemy.buffParam.SetSyncParam(model1.buff_sync_param);
        this.enemy.continusAttackParam.ApplySyncParam(model1.cntAtkSyncParam);
        MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) this.enemy);
        ((Component) this.enemy).gameObject.SetActive(true);
        this.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.NONE);
        this.enemy.isCoopInitialized = true;
        this.enemy.SetAppearPos(this.enemy._position);
        if (this.enemy.IsValidShield())
          this.enemy.RequestShieldShaderEffect();
        if (Object.op_Inequality((Object) this.enemy.tailController, (Object) null))
          this.enemy.tailController.SetPreviousPositionList(model1.tailPosList);
        if (MonoBehaviourSingleton<CoopManager>.IsValid() && (double) MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate == 0.0)
          MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate = this.enemy.damageHpRate;
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
          MonoBehaviourSingleton<InGameRecorder>.I.SetEnemyRecoveredHP(this.enemy.id, model1.recoveredHP);
        if (this.enemy.isHideSpawn)
        {
          if (this.enemy.isHiding)
          {
            this.enemy.InitHide();
            break;
          }
          this.enemy.TurnUpImmediate();
          break;
        }
        this.enemy.ActIdle();
        break;
      case PACKET_TYPE.ENEMY_STEP:
        if (this.character.isDead)
          return true;
        Coop_Model_EnemyStep model2 = packet.GetModel<Coop_Model_EnemyStep>();
        this.enemy.ApplySyncPosition(model2.pos, model2.dir, false);
        this.enemy.ActStep(model2.motion_id);
        break;
      case PACKET_TYPE.ENEMY_REVIVE_REGION:
        this.enemy.ReviveRegion(packet.GetModel<Coop_Model_EnemyReviveRegion>().region_id);
        break;
      case PACKET_TYPE.ENEMY_WARP:
        if (this.character.isDead)
          return true;
        Coop_Model_EnemyWarp model3 = packet.GetModel<Coop_Model_EnemyWarp>();
        this.enemy.ApplySyncPosition(model3.pos, model3.dir, true);
        this.enemy.SetWarp();
        break;
      case PACKET_TYPE.ENEMY_TARGRTSHOT_EVENT:
        if (this.character.isDead)
          return true;
        this.enemy.TargetRandamShotEvent(packet.GetModel<Coop_Model_EnemyTargetShotEvent>().targets);
        break;
      case PACKET_TYPE.ENEMY_RANDOMSHOT_EVENT:
        if (this.character.isDead)
          return true;
        this.enemy.PointRandamShotEvent(packet.GetModel<Coop_Model_EnemyRandomShotEvent>().points);
        break;
      case PACKET_TYPE.ENEMY_UPDATE_BLEED_DAMAGE:
        this.enemy.OnUpdateBleedDamage(packet.GetModel<Coop_Model_EnemyUpdateBleedDamage>().sync_data);
        break;
      case PACKET_TYPE.ENEMY_ANGRY:
        if (this.character.isDead)
          return true;
        Coop_Model_EnemyAngry model4 = packet.GetModel<Coop_Model_EnemyAngry>();
        this.enemy.ApplySyncPosition(model4.pos, model4.dir, false);
        this.enemy.ActAngry(model4.angryActionId, model4.angryId);
        this.enemy.ExecAngryIDList = model4.execAngryIds;
        break;
      case PACKET_TYPE.ENEMY_RELEASE_GRABBED_PLAYER:
        if (this.character.isDead)
          return true;
        Coop_Model_EnemyReleasedGrabbedPlayer model5 = packet.GetModel<Coop_Model_EnemyReleasedGrabbedPlayer>();
        this.enemy.ActReleaseGrabbedPlayers(false, false, true, model5.angle, model5.power);
        break;
      case PACKET_TYPE.ENEMY_SHOT:
        if (this.character.isDead)
          return true;
        Coop_Model_EnemyShot model6 = packet.GetModel<Coop_Model_EnemyShot>();
        this.enemy.ActShotBullet(model6.atkName, model6.posList, model6.rotList);
        break;
      case PACKET_TYPE.CREATE_ICE_FLOOR:
        if (this.character.isDead)
          return true;
        Coop_Model_CreateIceFloor model7 = packet.GetModel<Coop_Model_CreateIceFloor>();
        this.enemy.ActCreateIceFloor(model7.atkName, model7.posList, model7.rotList);
        break;
      case PACKET_TYPE.ACTION_MINE:
        if (this.character.isDead)
          return true;
        Coop_Model_ActionMine model8 = packet.GetModel<Coop_Model_ActionMine>();
        switch (model8.type)
        {
          case 0:
            this.enemy.ActDestroyActionMine(model8.objId, false);
            break;
          case 1:
            this.enemy.ActDestroyActionMine(model8.objId, true);
            break;
          case 2:
            this.enemy.ActCreateReflectBullet(model8.atkInfoName, model8.nodeName, model8.objId, model8.randSeed);
            break;
          case 3:
            this.enemy.ActCreateActionMine(model8.atkInfoName, model8.randSeed);
            break;
        }
        break;
      case PACKET_TYPE.ENEMY_RECOVER_HP:
        if (this.enemy.isDead)
          return true;
        this.enemy.RecoverHp(packet.GetModel<Coop_Model_EnemyRecoverHp>().value, true);
        break;
      case PACKET_TYPE.ENEMY_TURN_UP:
        if (this.enemy.isDead)
          return true;
        this.enemy.TurnUp();
        break;
      case PACKET_TYPE.ENEMY_UPDATE_SHADOWSEALING:
        this.enemy.OnUpdateShadowSealing(packet.GetModel<Coop_Model_EnemyUpdateShadowSealing>().sync_data);
        break;
      case PACKET_TYPE.ENEMY_SYNC_TARGET:
        if (this.enemy.isDead)
          return true;
        Coop_Model_EnemySyncTarget model9 = packet.GetModel<Coop_Model_EnemySyncTarget>();
        StageObject target2 = (StageObject) null;
        if (model9.targetId >= 0)
          target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model9.targetId);
        this.enemy.SetActionTarget(target2, true);
        break;
      case PACKET_TYPE.ENEMY_REGION_NODE_ACTIVATE:
        if (this.enemy.isDead)
          return true;
        Coop_Model_EnemyRegionNodeActivate model10 = packet.GetModel<Coop_Model_EnemyRegionNodeActivate>();
        this.enemy.ActivateRegionNode(model10.regionIDs, model10.isRandom, model10.randomSelectedID);
        break;
      case PACKET_TYPE.ENEMY_SUMMON_ATTACK:
        if (this.enemy.isDead)
          return true;
        Coop_Model_EnemySummonAttack model11 = packet.GetModel<Coop_Model_EnemySummonAttack>();
        StageObject target3 = (StageObject) null;
        if (model11.targetId > 0)
          target3 = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(model11.targetId);
        this.enemy.SetActionTarget(target3, true);
        this.enemy.ActSummonAttack(model11.enemyId, model11.attackId, model11.summonPos, model11.summonRot);
        break;
      case PACKET_TYPE.ENEMY_UPDATE_BOMBARROW:
        if (this.enemy.isDead)
          return true;
        this.enemy.OnUpdateBombArrow(packet.GetModel<Coop_Model_EnemyUpdateBombArrow>().regionId);
        break;
      default:
        return base.HandleCoopEvent(packet);
    }
    return true;
  }
}
