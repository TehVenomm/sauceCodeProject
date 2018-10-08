using UnityEngine;

public class CharacterPacketReceiver : ObjectPacketReceiver
{
	protected Character character => (Character)base.owner;

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
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
			Coop_Model_CharacterActionTarget model17 = packet.GetModel<Coop_Model_CharacterActionTarget>();
			StageObject target2 = null;
			if (model17.target_id >= 0)
			{
				target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model17.target_id);
			}
			character.SetActionTarget(target2, true);
			break;
		}
		case PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterUpdateActionPosition model18 = packet.GetModel<Coop_Model_CharacterUpdateActionPosition>();
			character.SetActionPosition(model18.act_pos, model18.act_pos_f);
			character.UpdateActionPosition(model18.trigger);
			break;
		}
		case PACKET_TYPE.CHARACTER_UPDATE_DIRECTION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterUpdateDirection model5 = packet.GetModel<Coop_Model_CharacterUpdateDirection>();
			character._rotation = Quaternion.AngleAxis(model5.dir, Vector3.get_up());
			character.SetLerpRotation(Quaternion.AngleAxis(model5.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			character.UpdateDirection(model5.trigger);
			break;
		}
		case PACKET_TYPE.CHARACTER_PERIODIC_SYNC_ACTION_POSITION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterPeriodicSyncActionPosition model22 = packet.GetModel<Coop_Model_CharacterPeriodicSyncActionPosition>();
			character.AddPeriodicSyncActionPosition(model22.info);
			break;
		}
		case PACKET_TYPE.CHARACTER_IDLE:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterIdle model4 = packet.GetModel<Coop_Model_CharacterIdle>();
			character.ApplySyncPosition(model4.pos, model4.dir, false);
			character.ActIdle(false, -1f);
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
			character.SetActionTarget(target, true);
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
			}
			else
			{
				character.ActMoveSyncVelocity(0f, model8.pos, model8.motion_id);
				character.SetMoveSyncVelocityEnd(model8.time, model8.pos, model8.direction, model8.sync_speed, model8.motion_id);
			}
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_TO_POSITION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveToPosition model2 = packet.GetModel<Coop_Model_CharacterMoveToPosition>();
			character.ApplySyncPosition(model2.pos, model2.dir, false);
			character.ActMoveToPosition(model2.target_pos, false);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_HOMING:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveHoming model20 = packet.GetModel<Coop_Model_CharacterMoveHoming>();
			character.ApplySyncPosition(model20.pos, model20.dir, false);
			character.ActMoveHoming(model20.max_length);
			character.SetActionPosition(model20.act_pos, model20.act_pos_f);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveSideways model11 = packet.GetModel<Coop_Model_CharacterMoveSideways>();
			character.ApplySyncPosition(model11.pos, model11.dir, false);
			character.ActMoveSideways(model11.moveAngleSign, true);
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
			character.ApplySyncPosition(model7.pos, model7.dir, false);
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
			character.ApplySyncPosition(model3.pos, model3.dir, false);
			character.ActMoveLookAt(model3.moveLookAtPos, true);
			break;
		}
		case PACKET_TYPE.CHARACTER_ROTATE:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterRotate model21 = packet.GetModel<Coop_Model_CharacterRotate>();
			character.ApplySyncPosition(model21.pos, model21.dir, false);
			character.ActRotateToDirection(model21.target_dir);
			break;
		}
		case PACKET_TYPE.CHARACTER_ROTATE_MOTION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterRotateMotion model19 = packet.GetModel<Coop_Model_CharacterRotateMotion>();
			character.ApplySyncPosition(model19.pos, model19.dir, false);
			character.ActRotateMotionToDirection(model19.target_dir);
			break;
		}
		case PACKET_TYPE.CHARACTER_ATTACK:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterAttack model16 = packet.GetModel<Coop_Model_CharacterAttack>();
			character.SyncRandomSeed = model16.syncRandomSeed;
			character.ApplySyncPosition(model16.pos, model16.dir, false);
			character.ActAttack(model16.attack_id, true, false);
			character.SetActionPosition(model16.act_pos, model16.act_pos_f);
			break;
		}
		case PACKET_TYPE.CHARACTER_CONTINUS_ATTACK_SYNC:
		{
			Coop_Model_CharacterContinusAttackSync model15 = packet.GetModel<Coop_Model_CharacterContinusAttackSync>();
			character.ReceiveContinusAttackParam(model15.sync_param);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFSYNC:
		{
			Coop_Model_CharacterBuffSync model14 = packet.GetModel<Coop_Model_CharacterBuffSync>();
			character.buffParam.SetSyncParam(model14.sync_param, true);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFRECEIVE:
		{
			Coop_Model_CharacterBuffReceive model13 = packet.GetModel<Coop_Model_CharacterBuffReceive>();
			BuffParam.BuffData buffData2 = model13.Deserialize();
			character.OnBuffReceive(buffData2);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFROUTINE:
		{
			Coop_Model_CharacterBuffRoutine model12 = packet.GetModel<Coop_Model_CharacterBuffRoutine>();
			BuffParam.BuffData buffData = model12.Deserialize();
			character.OnBuffRoutine(buffData, true);
			break;
		}
		case PACKET_TYPE.CHARACTER_REACTION:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterReaction model9 = packet.GetModel<Coop_Model_CharacterReaction>();
			character.ApplySyncPosition(model9.pos, model9.dir, false);
			Character.ReactionInfo reactionInfo = new Character.ReactionInfo();
			reactionInfo.reactionType = (Character.REACTION_TYPE)model9.reactionType;
			reactionInfo.blowForce = model9.blowForce;
			reactionInfo.loopTime = model9.loopTime;
			reactionInfo.targetId = model9.targetId;
			character.ActReaction(reactionInfo, false);
			break;
		}
		case PACKET_TYPE.CHARACTER_REACTION_DELAY:
		{
			if (character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterReactionDelay model6 = packet.GetModel<Coop_Model_CharacterReactionDelay>();
			character.ApplySyncPosition(model6.pos, model6.dir, false);
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
			character.ApplySyncPosition(model.pos, model.dir, false);
			if (character.isDead)
			{
				return true;
			}
			character.ActDead(false, true);
			break;
		}
		default:
			return base.HandleCoopEvent(packet);
		}
		return true;
	}
}
