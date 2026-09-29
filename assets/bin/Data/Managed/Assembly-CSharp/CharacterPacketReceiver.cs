// Decompiled with JetBrains decompiler
// Type: CharacterPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class CharacterPacketReceiver : ObjectPacketReceiver
{
  protected Character character => (Character) this.owner;

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    switch (packet.packetType)
    {
      case PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER:
        return this.character.isDead || base.HandleCoopEvent(packet);
      case PACKET_TYPE.CHARACTER_ACTION_TARGET:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterActionTarget model1 = packet.GetModel<Coop_Model_CharacterActionTarget>();
        StageObject target1 = (StageObject) null;
        if (model1.target_id >= 0)
          target1 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model1.target_id);
        this.character.SetActionTarget(target1);
        break;
      case PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterUpdateActionPosition model2 = packet.GetModel<Coop_Model_CharacterUpdateActionPosition>();
        this.character.SetActionPosition(model2.act_pos, model2.act_pos_f);
        this.character.UpdateActionPosition(model2.trigger);
        break;
      case PACKET_TYPE.CHARACTER_UPDATE_DIRECTION:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterUpdateDirection model3 = packet.GetModel<Coop_Model_CharacterUpdateDirection>();
        this.character._rotation = Quaternion.AngleAxis(model3.dir, Vector3.up);
        this.character.SetLerpRotation(Quaternion.op_Multiply(Quaternion.AngleAxis(model3.lerp_dir, Vector3.up), Vector3.forward));
        this.character.UpdateDirection(model3.trigger);
        break;
      case PACKET_TYPE.CHARACTER_PERIODIC_SYNC_ACTION_POSITION:
        if (this.character.isDead)
          return true;
        this.character.AddPeriodicSyncActionPosition(packet.GetModel<Coop_Model_CharacterPeriodicSyncActionPosition>().info);
        break;
      case PACKET_TYPE.CHARACTER_IDLE:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterIdle model4 = packet.GetModel<Coop_Model_CharacterIdle>();
        this.character.ApplySyncPosition(model4.pos, model4.dir);
        this.character.ActIdle();
        break;
      case PACKET_TYPE.CHARACTER_MOVE_VELOCITY:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterMoveVelocity model5 = packet.GetModel<Coop_Model_CharacterMoveVelocity>();
        StageObject target2 = (StageObject) null;
        if (model5.target_id >= 0)
          target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model5.target_id);
        this.character.SetActionTarget(target2);
        this.character.ActMoveSyncVelocity(model5.time, model5.pos, model5.motion_id);
        break;
      case PACKET_TYPE.CHARACTER_MOVE_VELOCITY_END:
        if (this.character.isDead)
          return true;
        bool flag = false;
        if (this.character.actionID == Character.ACTION_ID.MOVE && this.character.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
          flag = true;
        Coop_Model_CharacterMoveVelocityEnd model6 = packet.GetModel<Coop_Model_CharacterMoveVelocityEnd>();
        if (flag)
        {
          this.character.SetMoveSyncVelocityEnd(model6.time, model6.pos, model6.direction, model6.sync_speed, model6.motion_id);
          break;
        }
        this.character.ActMoveSyncVelocity(0.0f, model6.pos, model6.motion_id);
        this.character.SetMoveSyncVelocityEnd(model6.time, model6.pos, model6.direction, model6.sync_speed, model6.motion_id);
        break;
      case PACKET_TYPE.CHARACTER_MOVE_TO_POSITION:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterMoveToPosition model7 = packet.GetModel<Coop_Model_CharacterMoveToPosition>();
        this.character.ApplySyncPosition(model7.pos, model7.dir);
        this.character.ActMoveToPosition(model7.target_pos);
        break;
      case PACKET_TYPE.CHARACTER_MOVE_HOMING:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterMoveHoming model8 = packet.GetModel<Coop_Model_CharacterMoveHoming>();
        this.character.ApplySyncPosition(model8.pos, model8.dir);
        this.character.ActMoveHoming(model8.max_length);
        this.character.SetActionPosition(model8.act_pos, model8.act_pos_f);
        break;
      case PACKET_TYPE.CHARACTER_ROTATE:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterRotate model9 = packet.GetModel<Coop_Model_CharacterRotate>();
        this.character.ApplySyncPosition(model9.pos, model9.dir);
        this.character.ActRotateToDirection(model9.target_dir);
        break;
      case PACKET_TYPE.CHARACTER_ROTATE_MOTION:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterRotateMotion model10 = packet.GetModel<Coop_Model_CharacterRotateMotion>();
        this.character.ApplySyncPosition(model10.pos, model10.dir);
        this.character.ActRotateMotionToDirection(model10.target_dir);
        break;
      case PACKET_TYPE.CHARACTER_ATTACK:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterAttack model11 = packet.GetModel<Coop_Model_CharacterAttack>();
        this.character.SyncRandomSeed = model11.syncRandomSeed;
        this.character.ApplySyncPosition(model11.pos, model11.dir);
        this.character.ActAttack(model11.attack_id, _motionLayerName: model11.motionLayerName, _motionStateName: model11.motionStateName);
        this.character.SetActionPosition(model11.act_pos, model11.act_pos_f);
        break;
      case PACKET_TYPE.CHARACTER_BUFFSYNC:
        this.character.buffParam.SetSyncParam(packet.GetModel<Coop_Model_CharacterBuffSync>().sync_param);
        break;
      case PACKET_TYPE.CHARACTER_BUFFRECEIVE:
        this.character.OnBuffReceive(packet.GetModel<Coop_Model_CharacterBuffReceive>().Deserialize());
        break;
      case PACKET_TYPE.CHARACTER_BUFFROUTINE:
        this.character.OnBuffRoutine(packet.GetModel<Coop_Model_CharacterBuffRoutine>().Deserialize(), true);
        break;
      case PACKET_TYPE.CHARACTER_REACTION:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterReaction model12 = packet.GetModel<Coop_Model_CharacterReaction>();
        this.character.ApplySyncPosition(model12.pos, model12.dir);
        this.character.ActReaction(new Character.ReactionInfo()
        {
          reactionType = (Character.REACTION_TYPE) model12.reactionType,
          blowForce = model12.blowForce,
          loopTime = model12.loopTime,
          targetId = model12.targetId,
          deadReviveCount = model12.deadReviveCount
        });
        break;
      case PACKET_TYPE.CHARACTER_REACTION_DELAY:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterReactionDelay model13 = packet.GetModel<Coop_Model_CharacterReactionDelay>();
        this.character.ApplySyncPosition(model13.pos, model13.dir);
        this.character.OnReactionDelay(model13.reactionInfoList);
        break;
      case PACKET_TYPE.CHARACTER_DEAD:
        Coop_Model_CharacterDead model14 = packet.GetModel<Coop_Model_CharacterDead>();
        if (model14 == null)
          return true;
        this.character.ApplySyncPosition(model14.pos, model14.dir);
        if (this.character.isDead)
          return true;
        this.character.ActDead(recieve: true);
        break;
      case PACKET_TYPE.CHARACTER_CONTINUS_ATTACK_SYNC:
        this.character.ReceiveContinusAttackParam(packet.GetModel<Coop_Model_CharacterContinusAttackSync>().sync_param);
        break;
      case PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterMoveSideways model15 = packet.GetModel<Coop_Model_CharacterMoveSideways>();
        this.character.ApplySyncPosition(model15.pos, model15.dir);
        this.character.ActMoveSideways(model15.moveAngleSign, true);
        this.character.SetActionPosition(model15.actionPos, model15.actionPosFlag);
        break;
      case PACKET_TYPE.CHARACTER_MOVE_POINT:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterMovePoint model16 = packet.GetModel<Coop_Model_CharacterMovePoint>();
        this.character.ApplySyncPosition(model16.pos, model16.dir);
        this.character.ActMovePoint(model16.targetPos);
        break;
      case PACKET_TYPE.CHARACTER_MOVE_LOOKAT:
        if (this.character.isDead)
          return true;
        Coop_Model_CharacterMoveLookAt model17 = packet.GetModel<Coop_Model_CharacterMoveLookAt>();
        this.character.ApplySyncPosition(model17.pos, model17.dir);
        this.character.ActMoveLookAt(model17.moveLookAtPos, true);
        break;
      default:
        return base.HandleCoopEvent(packet);
    }
    return true;
  }
}
