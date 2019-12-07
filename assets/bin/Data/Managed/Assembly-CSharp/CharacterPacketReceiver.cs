using UnityEngine;

public class CharacterPacketReceiver : ObjectPacketReceiver
{
	protected Character character => (Character)base.owner;

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		switch (packet.packetType)
		{
		case PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER:
			if (character.isDead)
			{
				return true;
			}
			return base.HandleCoopEvent(packet);
		case PACKET_TYPE.CHARACTER_ACTION_TARGET:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterActionTarget model15 = packet.GetModel<Coop_Model_CharacterActionTarget>();
			StageObject target2 = null;
			if (model15.target_id >= 0)
			{
				target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model15.target_id);
			}
			character.SetActionTarget(target2);
			break;
		}
		case PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterUpdateActionPosition model16 = packet.GetModel<Coop_Model_CharacterUpdateActionPosition>();
			character.SetActionPosition(model16.act_pos, model16.act_pos_f);
			character.UpdateActionPosition(model16.trigger);
			break;
		}
		case PACKET_TYPE.CHARACTER_UPDATE_DIRECTION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterUpdateDirection model5 = packet.GetModel<Coop_Model_CharacterUpdateDirection>();
			character._rotation = Quaternion.AngleAxis(model5.dir, Vector3.up);
			character.SetLerpRotation(Quaternion.AngleAxis(model5.lerp_dir, Vector3.up) * Vector3.forward);
			character.UpdateDirection(model5.trigger);
			break;
		}
		case PACKET_TYPE.CHARACTER_PERIODIC_SYNC_ACTION_POSITION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterPeriodicSyncActionPosition model20 = packet.GetModel<Coop_Model_CharacterPeriodicSyncActionPosition>();
			character.AddPeriodicSyncActionPosition(model20.info);
			break;
		}
		case PACKET_TYPE.CHARACTER_IDLE:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterIdle model4 = packet.GetModel<Coop_Model_CharacterIdle>();
			character.ApplySyncPosition(model4.pos, model4.dir);
			character.ActIdle();
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_VELOCITY:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveVelocity model10 = packet.GetModel<Coop_Model_CharacterMoveVelocity>();
			StageObject target = null;
			if (model10.target_id >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model10.target_id);
			}
			character.SetActionTarget(target);
			character.ActMoveSyncVelocity(model10.time, model10.pos, model10.motion_id);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_VELOCITY_END:
		{
			if (character.isDead)
			{
				return true;
			}
			bool flag = false;
			if (character.actionID == Character.ACTION_ID.MOVE && character.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
			{
				flag = true;
			}
			Coop_Model_CharacterMoveVelocityEnd model8 = packet.GetModel<Coop_Model_CharacterMoveVelocityEnd>();
			if (flag)
			{
				character.SetMoveSyncVelocityEnd(model8.time, model8.pos, model8.direction, model8.sync_speed, model8.motion_id);
				break;
			}
			character.ActMoveSyncVelocity(0f, model8.pos, model8.motion_id);
			character.SetMoveSyncVelocityEnd(model8.time, model8.pos, model8.direction, model8.sync_speed, model8.motion_id);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_TO_POSITION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveToPosition model2 = packet.GetModel<Coop_Model_CharacterMoveToPosition>();
			character.ApplySyncPosition(model2.pos, model2.dir);
			character.ActMoveToPosition(model2.target_pos);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_HOMING:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveHoming model18 = packet.GetModel<Coop_Model_CharacterMoveHoming>();
			character.ApplySyncPosition(model18.pos, model18.dir);
			character.ActMoveHoming(model18.max_length);
			character.SetActionPosition(model18.act_pos, model18.act_pos_f);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveSideways model11 = packet.GetModel<Coop_Model_CharacterMoveSideways>();
			character.ApplySyncPosition(model11.pos, model11.dir);
			character.ActMoveSideways(model11.moveAngleSign, isPacket: true);
			character.SetActionPosition(model11.actionPos, model11.actionPosFlag);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_POINT:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMovePoint model7 = packet.GetModel<Coop_Model_CharacterMovePoint>();
			character.ApplySyncPosition(model7.pos, model7.dir);
			character.ActMovePoint(model7.targetPos);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_LOOKAT:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveLookAt model3 = packet.GetModel<Coop_Model_CharacterMoveLookAt>();
			character.ApplySyncPosition(model3.pos, model3.dir);
			character.ActMoveLookAt(model3.moveLookAtPos, isPacket: true);
			break;
		}
		case PACKET_TYPE.CHARACTER_ROTATE:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterRotate model19 = packet.GetModel<Coop_Model_CharacterRotate>();
			character.ApplySyncPosition(model19.pos, model19.dir);
			character.ActRotateToDirection(model19.target_dir);
			break;
		}
		case PACKET_TYPE.CHARACTER_ROTATE_MOTION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterRotateMotion model17 = packet.GetModel<Coop_Model_CharacterRotateMotion>();
			character.ApplySyncPosition(model17.pos, model17.dir);
			character.ActRotateMotionToDirection(model17.target_dir);
			break;
		}
		case PACKET_TYPE.CHARACTER_ATTACK:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterAttack model14 = packet.GetModel<Coop_Model_CharacterAttack>();
			character.SyncRandomSeed = model14.syncRandomSeed;
			character.ApplySyncPosition(model14.pos, model14.dir);
			character.ActAttack(model14.attack_id, send_packet: true, sync_immediately: false, model14.motionLayerName, model14.motionStateName);
			character.SetActionPosition(model14.act_pos, model14.act_pos_f);
			break;
		}
		case PACKET_TYPE.CHARACTER_CONTINUS_ATTACK_SYNC:
		{
			Coop_Model_CharacterContinusAttackSync model13 = packet.GetModel<Coop_Model_CharacterContinusAttackSync>();
			character.ReceiveContinusAttackParam(model13.sync_param);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFSYNC:
		{
			Coop_Model_CharacterBuffSync model12 = packet.GetModel<Coop_Model_CharacterBuffSync>();
			character.buffParam.SetSyncParam(model12.sync_param);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFRECEIVE:
		{
			BuffParam.BuffData buffData2 = packet.GetModel<Coop_Model_CharacterBuffReceive>().Deserialize();
			character.OnBuffReceive(buffData2);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFROUTINE:
		{
			BuffParam.BuffData buffData = packet.GetModel<Coop_Model_CharacterBuffRoutine>().Deserialize();
			character.OnBuffRoutine(buffData, packet: true);
			break;
		}
		case PACKET_TYPE.CHARACTER_REACTION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterReaction model9 = packet.GetModel<Coop_Model_CharacterReaction>();
			character.ApplySyncPosition(model9.pos, model9.dir);
			Character.ReactionInfo reactionInfo = new Character.ReactionInfo();
			reactionInfo.reactionType = (Character.REACTION_TYPE)model9.reactionType;
			reactionInfo.blowForce = model9.blowForce;
			reactionInfo.loopTime = model9.loopTime;
			reactionInfo.targetId = model9.targetId;
			reactionInfo.deadReviveCount = model9.deadReviveCount;
			character.ActReaction(reactionInfo);
			break;
		}
		case PACKET_TYPE.CHARACTER_REACTION_DELAY:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterReactionDelay model6 = packet.GetModel<Coop_Model_CharacterReactionDelay>();
			character.ApplySyncPosition(model6.pos, model6.dir);
			character.OnReactionDelay(model6.reactionInfoList);
			break;
		}
		case PACKET_TYPE.CHARACTER_DEAD:
		{
			Coop_Model_CharacterDead model = packet.GetModel<Coop_Model_CharacterDead>();
			if (model == null)
			{
				return true;
			}
			character.ApplySyncPosition(model.pos, model.dir);
			if (character.isDead)
			{
				return true;
			}
			character.ActDead(force_sync: false, recieve: true);
			break;
		}
		default:
			return base.HandleCoopEvent(packet);
		}
		return true;
	}
}
