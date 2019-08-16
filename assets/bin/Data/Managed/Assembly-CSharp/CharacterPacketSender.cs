using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterPacketSender : ObjectPacketSender
{
	protected float updatePositionTimer;

	protected bool actUpdateSendFlag;

	protected float actUpdateTimer;

	protected int moveMotion;

	protected int prevSendHp;

	protected Character character => (Character)base.owner;

	public abstract void OnLoadComplete(bool promise = true);

	public abstract void OnRecvLoadComplete(int to_client_id);

	public abstract void SendInitialize(int to_client_id = 0);

	public override void OnUpdate()
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		base.OnUpdate();
		if (actUpdateSendFlag)
		{
			float num = MonoBehaviourSingleton<InGameSettingsManager>.I.character.moveSendInterval;
			int i = 0;
			for (int count = character.periodicSyncOwnerList.Count; i < count; i++)
			{
				StageObject stageObject = character.periodicSyncOwnerList[i];
				if (stageObject.IsMirror() || stageObject.IsPuppet())
				{
					num = MonoBehaviourSingleton<InGameSettingsManager>.I.character.periodicSyncActionPositionCheckTime + MonoBehaviourSingleton<InGameSettingsManager>.I.character.periodicSyncActionPositionApplyTime;
					break;
				}
			}
			actUpdateTimer += Time.get_deltaTime();
			if (character.actionID == Character.ACTION_ID.MOVE && actUpdateTimer >= num)
			{
				Coop_Model_CharacterMoveVelocity coop_Model_CharacterMoveVelocity = new Coop_Model_CharacterMoveVelocity();
				coop_Model_CharacterMoveVelocity.id = base.owner.id;
				coop_Model_CharacterMoveVelocity.time = actUpdateTimer;
				coop_Model_CharacterMoveVelocity.pos = base.owner._position;
				coop_Model_CharacterMoveVelocity.motion_id = moveMotion;
				coop_Model_CharacterMoveVelocity.target_id = ((!(character.actionTarget != null)) ? (-1) : character.actionTarget.id);
				if (base.enableSend && base.owner.IsOriginal())
				{
					SendBroadcast(coop_Model_CharacterMoveVelocity);
				}
				StackActionHistory(coop_Model_CharacterMoveVelocity, is_act_model: true);
				actUpdateTimer = 0f;
			}
		}
		if (character.isControllable || character.enableMotionCancel)
		{
			PassNeedWaitSyncTime(Time.get_deltaTime());
		}
	}

	public virtual void OnSetActionTarget(StageObject target)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_CharacterActionTarget coop_Model_CharacterActionTarget = new Coop_Model_CharacterActionTarget();
			coop_Model_CharacterActionTarget.id = base.owner.id;
			coop_Model_CharacterActionTarget.target_id = ((!(target != null)) ? (-1) : target.id);
			SendBroadcast(coop_Model_CharacterActionTarget);
		}
	}

	public virtual void OnUpdateActionPosition(string trigger)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterUpdateActionPosition coop_Model_CharacterUpdateActionPosition = new Coop_Model_CharacterUpdateActionPosition();
		coop_Model_CharacterUpdateActionPosition.id = base.owner.id;
		coop_Model_CharacterUpdateActionPosition.trigger = trigger;
		coop_Model_CharacterUpdateActionPosition.act_pos = character.actionPosition;
		coop_Model_CharacterUpdateActionPosition.act_pos_f = character.actionPositionFlag;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterUpdateActionPosition);
		}
		StackActionHistory(coop_Model_CharacterUpdateActionPosition, is_act_model: false);
	}

	public virtual void OnUpdateDirection(string trigger)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterUpdateDirection coop_Model_CharacterUpdateDirection = new Coop_Model_CharacterUpdateDirection();
		coop_Model_CharacterUpdateDirection.id = base.owner.id;
		coop_Model_CharacterUpdateDirection.trigger = trigger;
		Coop_Model_CharacterUpdateDirection coop_Model_CharacterUpdateDirection2 = coop_Model_CharacterUpdateDirection;
		Quaternion rotation = base.owner._rotation;
		Vector3 eulerAngles = rotation.get_eulerAngles();
		coop_Model_CharacterUpdateDirection2.dir = eulerAngles.y;
		if (character.lerpRotateVec == Vector3.get_zero())
		{
			coop_Model_CharacterUpdateDirection.lerp_dir = coop_Model_CharacterUpdateDirection.dir;
		}
		else
		{
			Coop_Model_CharacterUpdateDirection coop_Model_CharacterUpdateDirection3 = coop_Model_CharacterUpdateDirection;
			Quaternion val = Quaternion.LookRotation(character.lerpRotateVec);
			Vector3 eulerAngles2 = val.get_eulerAngles();
			coop_Model_CharacterUpdateDirection3.lerp_dir = eulerAngles2.y;
		}
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterUpdateDirection);
		}
		StackActionHistory(coop_Model_CharacterUpdateDirection, is_act_model: false);
	}

	public virtual void OnPeriodicSyncActionPosition(Character.PeriodicSyncActionPositionInfo info)
	{
		Coop_Model_CharacterPeriodicSyncActionPosition coop_Model_CharacterPeriodicSyncActionPosition = new Coop_Model_CharacterPeriodicSyncActionPosition();
		coop_Model_CharacterPeriodicSyncActionPosition.id = base.owner.id;
		coop_Model_CharacterPeriodicSyncActionPosition.info = info;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterPeriodicSyncActionPosition);
		}
		StackActionHistory(coop_Model_CharacterPeriodicSyncActionPosition, is_act_model: false);
	}

	public virtual void OnActIdle(bool is_sync)
	{
		if (base.enableSend && base.owner.IsOriginal() && is_sync)
		{
			Coop_Model_CharacterIdle coop_Model_CharacterIdle = new Coop_Model_CharacterIdle();
			coop_Model_CharacterIdle.id = base.owner.id;
			coop_Model_CharacterIdle.SetSyncPosition(base.owner);
			SendBroadcast(coop_Model_CharacterIdle);
		}
		ClearActionHistory();
	}

	public virtual void OnActAttack(int id, bool sync_immediately, int syncRandomSeed = 0, string _motionLayerName = "", string _motionStateName = "")
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterAttack coop_Model_CharacterAttack = new Coop_Model_CharacterAttack();
		coop_Model_CharacterAttack.id = base.owner.id;
		coop_Model_CharacterAttack.SetSyncPosition(base.owner);
		coop_Model_CharacterAttack.attack_id = id;
		coop_Model_CharacterAttack.motionLayerName = _motionLayerName;
		coop_Model_CharacterAttack.motionStateName = _motionStateName;
		coop_Model_CharacterAttack.act_pos = character.actionPosition;
		coop_Model_CharacterAttack.act_pos_f = character.actionPositionFlag;
		coop_Model_CharacterAttack.sync_immediately = sync_immediately;
		coop_Model_CharacterAttack.syncRandomSeed = syncRandomSeed;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterAttack);
		}
		StackActionHistory(coop_Model_CharacterAttack, is_act_model: true);
	}

	public virtual void OnActMoveVelocity(int motion_id)
	{
		actUpdateTimer = 0f;
		actUpdateSendFlag = true;
		moveMotion = motion_id;
	}

	public virtual void OnActMoveToPosition(Vector3 target_pos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterMoveToPosition coop_Model_CharacterMoveToPosition = new Coop_Model_CharacterMoveToPosition();
		coop_Model_CharacterMoveToPosition.id = base.owner.id;
		coop_Model_CharacterMoveToPosition.SetSyncPosition(base.owner);
		coop_Model_CharacterMoveToPosition.target_pos = target_pos;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterMoveToPosition);
		}
		StackActionHistory(coop_Model_CharacterMoveToPosition, is_act_model: true);
	}

	public virtual void OnActMoveHoming(float max_length)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterMoveHoming coop_Model_CharacterMoveHoming = new Coop_Model_CharacterMoveHoming();
		coop_Model_CharacterMoveHoming.id = base.owner.id;
		coop_Model_CharacterMoveHoming.SetSyncPosition(base.owner);
		coop_Model_CharacterMoveHoming.act_pos = character.actionPosition;
		coop_Model_CharacterMoveHoming.act_pos_f = character.actionPositionFlag;
		coop_Model_CharacterMoveHoming.max_length = max_length;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterMoveHoming);
		}
		StackActionHistory(coop_Model_CharacterMoveHoming, is_act_model: true);
	}

	public virtual void OnActMoveSideways(int moveAngleSign)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterMoveSideways coop_Model_CharacterMoveSideways = new Coop_Model_CharacterMoveSideways();
		coop_Model_CharacterMoveSideways.id = base.owner.id;
		coop_Model_CharacterMoveSideways.SetSyncPosition(base.owner);
		coop_Model_CharacterMoveSideways.actionPos = character.actionPosition;
		coop_Model_CharacterMoveSideways.actionPosFlag = character.actionPositionFlag;
		coop_Model_CharacterMoveSideways.moveAngleSign = moveAngleSign;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterMoveSideways);
		}
		StackActionHistory(coop_Model_CharacterMoveSideways, is_act_model: true);
	}

	public virtual void OnActMovePoint(Vector3 targetPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterMovePoint coop_Model_CharacterMovePoint = new Coop_Model_CharacterMovePoint();
		coop_Model_CharacterMovePoint.id = base.owner.id;
		coop_Model_CharacterMovePoint.SetSyncPosition(base.owner);
		coop_Model_CharacterMovePoint.targetPos = targetPos;
		if (this.get_enabled() && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterMovePoint);
		}
		StackActionHistory(coop_Model_CharacterMovePoint, is_act_model: true);
	}

	public void OnActMoveLookAt(Vector3 moveLookAtPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterMoveLookAt coop_Model_CharacterMoveLookAt = new Coop_Model_CharacterMoveLookAt();
		coop_Model_CharacterMoveLookAt.id = base.owner.id;
		coop_Model_CharacterMoveLookAt.SetSyncPosition(base.owner);
		coop_Model_CharacterMoveLookAt.moveLookAtPos = moveLookAtPos;
		if (this.get_enabled() && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterMoveLookAt);
		}
		StackActionHistory(coop_Model_CharacterMoveLookAt, is_act_model: true);
	}

	public virtual void OnActRotate(float direction)
	{
		Coop_Model_CharacterRotate coop_Model_CharacterRotate = new Coop_Model_CharacterRotate();
		coop_Model_CharacterRotate.id = base.owner.id;
		coop_Model_CharacterRotate.SetSyncPosition(base.owner);
		coop_Model_CharacterRotate.target_dir = direction;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterRotate);
		}
		StackActionHistory(coop_Model_CharacterRotate, is_act_model: true);
	}

	public virtual void OnActRotateMotion(float direction)
	{
		Coop_Model_CharacterRotateMotion coop_Model_CharacterRotateMotion = new Coop_Model_CharacterRotateMotion();
		coop_Model_CharacterRotateMotion.id = base.owner.id;
		coop_Model_CharacterRotateMotion.SetSyncPosition(base.owner);
		coop_Model_CharacterRotateMotion.target_dir = direction;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterRotateMotion);
		}
		StackActionHistory(coop_Model_CharacterRotateMotion, is_act_model: true);
	}

	public void OnReactionDelay(List<Character.DelayReactionInfo> reactionList)
	{
		Coop_Model_CharacterReactionDelay coop_Model_CharacterReactionDelay = new Coop_Model_CharacterReactionDelay();
		coop_Model_CharacterReactionDelay.id = base.owner.id;
		coop_Model_CharacterReactionDelay.SetSyncPosition(base.owner);
		coop_Model_CharacterReactionDelay.reactionInfoList = reactionList;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_CharacterReactionDelay);
		}
		StackActionHistory(coop_Model_CharacterReactionDelay, is_act_model: true);
	}

	public void OnSendContinusAttackSync(ContinusAttackParam.SyncParam syncParam)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_CharacterContinusAttackSync coop_Model_CharacterContinusAttackSync = new Coop_Model_CharacterContinusAttackSync();
			coop_Model_CharacterContinusAttackSync.id = base.owner.id;
			coop_Model_CharacterContinusAttackSync.sync_param = syncParam;
			SendBroadcast(coop_Model_CharacterContinusAttackSync);
		}
	}

	public void OnSendBuffSync(BuffParam.BuffSyncParam sync_param)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_CharacterBuffSync coop_Model_CharacterBuffSync = new Coop_Model_CharacterBuffSync();
			coop_Model_CharacterBuffSync.id = base.owner.id;
			coop_Model_CharacterBuffSync.sync_param = sync_param;
			SendBroadcast(coop_Model_CharacterBuffSync);
		}
	}

	public void OnBuffReceive(BuffParam.BUFFTYPE type, int value, float time)
	{
		if (base.enableSend && (base.owner.IsPuppet() || base.owner.IsMirror()))
		{
			Coop_Model_CharacterBuffReceive coop_Model_CharacterBuffReceive = new Coop_Model_CharacterBuffReceive();
			coop_Model_CharacterBuffReceive.id = base.owner.id;
			coop_Model_CharacterBuffReceive.type = (int)type;
			coop_Model_CharacterBuffReceive.value = value;
			coop_Model_CharacterBuffReceive.time = time;
			SendTo(base.owner.coopClientId, coop_Model_CharacterBuffReceive);
		}
	}

	public void OnBuffRoutine(BuffParam.BUFFTYPE type, int value, int fromObjectID, int fromEquipIndex, int fromSkillIndex)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_CharacterBuffRoutine coop_Model_CharacterBuffRoutine = new Coop_Model_CharacterBuffRoutine();
			coop_Model_CharacterBuffRoutine.id = base.owner.id;
			coop_Model_CharacterBuffRoutine.type = (int)type;
			coop_Model_CharacterBuffRoutine.value = value;
			coop_Model_CharacterBuffRoutine.fromObjectID = fromObjectID;
			coop_Model_CharacterBuffRoutine.fromEquipIndex = fromEquipIndex;
			coop_Model_CharacterBuffRoutine.fromSkillIndex = fromSkillIndex;
			SendBroadcast(coop_Model_CharacterBuffRoutine);
		}
	}

	public virtual void OnEndAction()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		Character.ACTION_ID actionID = character.actionID;
		if (actionID == Character.ACTION_ID.MOVE && actUpdateSendFlag)
		{
			if (base.enableSend && base.owner.IsOriginal())
			{
				Coop_Model_CharacterMoveVelocityEnd coop_Model_CharacterMoveVelocityEnd = new Coop_Model_CharacterMoveVelocityEnd();
				coop_Model_CharacterMoveVelocityEnd.id = base.owner.id;
				coop_Model_CharacterMoveVelocityEnd.time = actUpdateTimer;
				coop_Model_CharacterMoveVelocityEnd.pos = base.owner._position;
				Coop_Model_CharacterMoveVelocityEnd coop_Model_CharacterMoveVelocityEnd2 = coop_Model_CharacterMoveVelocityEnd;
				Quaternion rotation = base.owner._rotation;
				Vector3 eulerAngles = rotation.get_eulerAngles();
				coop_Model_CharacterMoveVelocityEnd2.direction = eulerAngles.y;
				coop_Model_CharacterMoveVelocityEnd.sync_speed = character.moveSyncSpeed;
				coop_Model_CharacterMoveVelocityEnd.motion_id = moveMotion;
				SendBroadcast(coop_Model_CharacterMoveVelocityEnd);
			}
			actUpdateTimer = 0f;
			actUpdateSendFlag = false;
		}
	}

	public void OnActReaction(Character.ReactionInfo info, bool isSync)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Coop_Model_CharacterReaction coop_Model_CharacterReaction = new Coop_Model_CharacterReaction();
		coop_Model_CharacterReaction.id = base.owner.id;
		coop_Model_CharacterReaction.SetSyncPosition(base.owner);
		coop_Model_CharacterReaction.reactionType = (int)info.reactionType;
		coop_Model_CharacterReaction.blowForce = info.blowForce;
		coop_Model_CharacterReaction.loopTime = info.loopTime;
		coop_Model_CharacterReaction.targetId = info.targetId;
		coop_Model_CharacterReaction.deadReviveCount = info.deadReviveCount;
		if (isSync)
		{
			SendBroadcast(coop_Model_CharacterReaction);
		}
		StackActionHistory(coop_Model_CharacterReaction, is_act_model: true);
	}

	public void OnActDead()
	{
		Coop_Model_CharacterDead coop_Model_CharacterDead = new Coop_Model_CharacterDead();
		coop_Model_CharacterDead.id = base.owner.id;
		coop_Model_CharacterDead.SetSyncPosition(base.owner);
		if (base.enableSend)
		{
			SendBroadcast(coop_Model_CharacterDead, promise: true);
		}
		StackActionHistory(coop_Model_CharacterDead, is_act_model: true);
	}
}
