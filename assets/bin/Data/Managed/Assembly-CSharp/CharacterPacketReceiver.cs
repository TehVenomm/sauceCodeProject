using UnityEngine;

public class CharacterPacketReceiver : ObjectPacketReceiver
{
	protected Character character => (Character)base.owner;

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		switch (packet.packetType)
		{
		case PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER:
			if (this.character.isDead)
			{
				return true;
			}
			return base.HandleCoopEvent(packet);
		case PACKET_TYPE.CHARACTER_ACTION_TARGET:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterActionTarget model17 = packet.GetModel<Coop_Model_CharacterActionTarget>();
			StageObject target2 = null;
			if (model17.target_id >= 0)
			{
				target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model17.target_id);
			}
			this.character.SetActionTarget(target2);
			break;
		}
		case PACKET_TYPE.CHARACTER_UPDATE_ACTION_POSITION:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterUpdateActionPosition model18 = packet.GetModel<Coop_Model_CharacterUpdateActionPosition>();
			this.character.SetActionPosition(model18.act_pos, model18.act_pos_f);
			this.character.UpdateActionPosition(model18.trigger);
			break;
		}
		case PACKET_TYPE.CHARACTER_UPDATE_DIRECTION:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterUpdateDirection model5 = packet.GetModel<Coop_Model_CharacterUpdateDirection>();
			this.character._rotation = Quaternion.AngleAxis(model5.dir, Vector3.get_up());
			this.character.SetLerpRotation(Quaternion.AngleAxis(model5.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			this.character.UpdateDirection(model5.trigger);
			break;
		}
		case PACKET_TYPE.CHARACTER_PERIODIC_SYNC_ACTION_POSITION:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterPeriodicSyncActionPosition model22 = packet.GetModel<Coop_Model_CharacterPeriodicSyncActionPosition>();
			this.character.AddPeriodicSyncActionPosition(model22.info);
			break;
		}
		case PACKET_TYPE.CHARACTER_IDLE:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterIdle model4 = packet.GetModel<Coop_Model_CharacterIdle>();
			this.character.ApplySyncPosition(model4.pos, model4.dir);
			this.character.ActIdle();
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_VELOCITY:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveVelocity model10 = packet.GetModel<Coop_Model_CharacterMoveVelocity>();
			StageObject target = null;
			if (model10.target_id >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model10.target_id);
			}
			this.character.SetActionTarget(target);
			this.character.ActMoveSyncVelocity(model10.time, model10.pos, model10.motion_id);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_VELOCITY_END:
		{
			if (this.character.isDead)
			{
				return true;
			}
			bool flag = false;
			if (this.character.actionID == Character.ACTION_ID.MOVE && this.character.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
			{
				flag = true;
			}
			Coop_Model_CharacterMoveVelocityEnd model8 = packet.GetModel<Coop_Model_CharacterMoveVelocityEnd>();
			if (flag)
			{
				this.character.SetMoveSyncVelocityEnd(model8.time, model8.pos, model8.direction, model8.sync_speed, model8.motion_id);
				break;
			}
			this.character.ActMoveSyncVelocity(0f, model8.pos, model8.motion_id);
			this.character.SetMoveSyncVelocityEnd(model8.time, model8.pos, model8.direction, model8.sync_speed, model8.motion_id);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_TO_POSITION:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveToPosition model2 = packet.GetModel<Coop_Model_CharacterMoveToPosition>();
			this.character.ApplySyncPosition(model2.pos, model2.dir);
			this.character.ActMoveToPosition(model2.target_pos);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_HOMING:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveHoming model20 = packet.GetModel<Coop_Model_CharacterMoveHoming>();
			this.character.ApplySyncPosition(model20.pos, model20.dir);
			this.character.ActMoveHoming(model20.max_length);
			this.character.SetActionPosition(model20.act_pos, model20.act_pos_f);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_SIDEWAYS:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveSideways model11 = packet.GetModel<Coop_Model_CharacterMoveSideways>();
			this.character.ApplySyncPosition(model11.pos, model11.dir);
			this.character.ActMoveSideways(model11.moveAngleSign, isPacket: true);
			this.character.SetActionPosition(model11.actionPos, model11.actionPosFlag);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_POINT:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMovePoint model7 = packet.GetModel<Coop_Model_CharacterMovePoint>();
			this.character.ApplySyncPosition(model7.pos, model7.dir);
			this.character.ActMovePoint(model7.targetPos);
			break;
		}
		case PACKET_TYPE.CHARACTER_MOVE_LOOKAT:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterMoveLookAt model3 = packet.GetModel<Coop_Model_CharacterMoveLookAt>();
			this.character.ApplySyncPosition(model3.pos, model3.dir);
			this.character.ActMoveLookAt(model3.moveLookAtPos, isPacket: true);
			break;
		}
		case PACKET_TYPE.CHARACTER_ROTATE:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterRotate model21 = packet.GetModel<Coop_Model_CharacterRotate>();
			this.character.ApplySyncPosition(model21.pos, model21.dir);
			this.character.ActRotateToDirection(model21.target_dir);
			break;
		}
		case PACKET_TYPE.CHARACTER_ROTATE_MOTION:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterRotateMotion model19 = packet.GetModel<Coop_Model_CharacterRotateMotion>();
			this.character.ApplySyncPosition(model19.pos, model19.dir);
			this.character.ActRotateMotionToDirection(model19.target_dir);
			break;
		}
		case PACKET_TYPE.CHARACTER_ATTACK:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterAttack model16 = packet.GetModel<Coop_Model_CharacterAttack>();
			this.character.SyncRandomSeed = model16.syncRandomSeed;
			this.character.ApplySyncPosition(model16.pos, model16.dir);
			Character character = this.character;
			int attack_id = model16.attack_id;
			string motionLayerName = model16.motionLayerName;
			string motionStateName = model16.motionStateName;
			character.ActAttack(attack_id, send_packet: true, sync_immediately: false, motionLayerName, motionStateName);
			this.character.SetActionPosition(model16.act_pos, model16.act_pos_f);
			break;
		}
		case PACKET_TYPE.CHARACTER_CONTINUS_ATTACK_SYNC:
		{
			Coop_Model_CharacterContinusAttackSync model15 = packet.GetModel<Coop_Model_CharacterContinusAttackSync>();
			this.character.ReceiveContinusAttackParam(model15.sync_param);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFSYNC:
		{
			Coop_Model_CharacterBuffSync model14 = packet.GetModel<Coop_Model_CharacterBuffSync>();
			this.character.buffParam.SetSyncParam(model14.sync_param);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFRECEIVE:
		{
			Coop_Model_CharacterBuffReceive model13 = packet.GetModel<Coop_Model_CharacterBuffReceive>();
			BuffParam.BuffData buffData2 = model13.Deserialize();
			this.character.OnBuffReceive(buffData2);
			break;
		}
		case PACKET_TYPE.CHARACTER_BUFFROUTINE:
		{
			Coop_Model_CharacterBuffRoutine model12 = packet.GetModel<Coop_Model_CharacterBuffRoutine>();
			BuffParam.BuffData buffData = model12.Deserialize();
			this.character.OnBuffRoutine(buffData, packet: true);
			break;
		}
		case PACKET_TYPE.CHARACTER_REACTION:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterReaction model9 = packet.GetModel<Coop_Model_CharacterReaction>();
			this.character.ApplySyncPosition(model9.pos, model9.dir);
			Character.ReactionInfo reactionInfo = new Character.ReactionInfo();
			reactionInfo.reactionType = (Character.REACTION_TYPE)model9.reactionType;
			reactionInfo.blowForce = model9.blowForce;
			reactionInfo.loopTime = model9.loopTime;
			reactionInfo.targetId = model9.targetId;
			reactionInfo.deadReviveCount = model9.deadReviveCount;
			this.character.ActReaction(reactionInfo);
			break;
		}
		case PACKET_TYPE.CHARACTER_REACTION_DELAY:
		{
			if (this.character.isDead)
			{
				return true;
			}
			Coop_Model_CharacterReactionDelay model6 = packet.GetModel<Coop_Model_CharacterReactionDelay>();
			this.character.ApplySyncPosition(model6.pos, model6.dir);
			this.character.OnReactionDelay(model6.reactionInfoList);
			break;
		}
		case PACKET_TYPE.CHARACTER_DEAD:
		{
			Coop_Model_CharacterDead model = packet.GetModel<Coop_Model_CharacterDead>();
			if (model == null)
			{
				return true;
			}
			this.character.ApplySyncPosition(model.pos, model.dir);
			if (this.character.isDead)
			{
				return true;
			}
			this.character.ActDead(force_sync: false, recieve: true);
			break;
		}
		default:
			return base.HandleCoopEvent(packet);
		}
		return true;
	}
}
