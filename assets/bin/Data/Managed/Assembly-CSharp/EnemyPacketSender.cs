using System.Collections.Generic;
using UnityEngine;

public class EnemyPacketSender : CharacterPacketSender
{
	protected Enemy enemy => (Enemy)base.owner;

	public override bool IsEnableWaitSync()
	{
		return true;
	}

	public override void OnLoadComplete(bool promise = true)
	{
		if (base.enableSend && base.owner.IsMirror())
		{
			Coop_Model_EnemyLoadComplete coop_Model_EnemyLoadComplete = new Coop_Model_EnemyLoadComplete();
			coop_Model_EnemyLoadComplete.id = base.owner.id;
			SendToExtra(base.owner.coopClientId, coop_Model_EnemyLoadComplete, promise);
		}
	}

	public override void OnRecvLoadComplete(int to_client_id)
	{
		if (base.owner.IsCoopNone())
		{
			base.owner.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
		}
		SendInitialize(to_client_id);
	}

	public override void SendInitialize(int to_client_id = 0)
	{
		if (!base.enableSend || !base.owner.IsOriginal())
		{
			return;
		}
		Coop_Model_EnemyInitialize model = new Coop_Model_EnemyInitialize();
		SetupEnemyInitializeModel(model, to_client_id != 0, resend: false);
		if (to_client_id == 0)
		{
			SendBroadcast(model, promise: true, null, delegate(Coop_Model_Base send_model)
			{
				SetupEnemyInitializeModel(send_model as Coop_Model_EnemyInitialize, send_to: false, resend: true);
				return true;
			});
		}
		else
		{
			SendToExtra(to_client_id, model, promise: true, null, delegate(Coop_Model_Base send_model)
			{
				SetupEnemyInitializeModel(send_model as Coop_Model_EnemyInitialize, send_to: true, resend: true);
				return true;
			});
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.SendSyncPlayerRecord(to_client_id);
			}
		}
		SendActionHistory(to_client_id);
	}

	public void SetupEnemyInitializeModel(Coop_Model_EnemyInitialize model, bool send_to, bool resend)
	{
		model.id = base.owner.id;
		if (!resend && actionHistoryData != null)
		{
			model.pos = actionHistoryData.startPos;
			model.dir = actionHistoryData.startDir;
		}
		else
		{
			model.SetSyncPosition(base.owner);
		}
		model.sid = enemy.id;
		model.hp = enemy.hp;
		model.hpMax = enemy.hpMax;
		model.hpDamageRate = enemy.damageHpRate;
		model.downTotal = enemy.downTotal;
		model.downCount = enemy.downCount;
		model.concussionTotal = enemy.concussionTotal;
		model.concussionMax = enemy.concussionMax;
		model.concussionExtend = enemy.concussionExtend;
		model.badStatusMax = enemy.badStatusMax;
		model.SetRegionWorks(enemy.regionWorks);
		model.nowAngryId = enemy.NowAngryID;
		model.execAngryIds = enemy.ExecAngryIDList;
		model.target_id = ((enemy.actionTarget != null) ? enemy.actionTarget.id : (-1));
		model.buff_sync_param = enemy.buffParam.CreateSyncParam();
		model.cntAtkSyncParam = enemy.continusAttackParam.CreateSyncParam();
		model.barrierHp = enemy.BarrierHp;
		model.isHiding = enemy.isHiding;
		model.shieldHp = enemy.ShieldHp;
		model.grabHp = enemy.GrabHp;
		model.bulletIndex = enemy.bulletIndex;
		model.walkSpeedRateFromTable = enemy.walkSpeedRateFromTable;
		model.aegisSetupParam = enemy.GetAegisSetupParam();
		model.changeElementIcon = enemy.changeElementIcon;
		model.changeWeakElementIcon = enemy.changeWeakElementIcon;
		model.changeToleranceRegionId = enemy.changeToleranceRegionId;
		model.changeToleranceScroll = enemy.changeToleranceScroll;
		model.shaderSyncParam = enemy.blendColorCtrl.GetShaderParamList();
		model.deadReviveCount = enemy.deadReviveCount;
		model.isFirstMadMode = enemy.isFirstMadMode;
		if (enemy.tailController != null)
		{
			model.tailPosList = enemy.tailController.PreviousPositionList;
		}
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			model.recoveredHP = MonoBehaviourSingleton<InGameRecorder>.I.GetEnemyRecoveredHpById(enemy.id);
		}
	}

	public virtual void OnUpdateBleedDamage(Enemy.BleedSyncData sync_data)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyUpdateBleedDamage coop_Model_EnemyUpdateBleedDamage = new Coop_Model_EnemyUpdateBleedDamage();
			coop_Model_EnemyUpdateBleedDamage.id = base.owner.id;
			coop_Model_EnemyUpdateBleedDamage.sync_data = sync_data;
			SendBroadcast(coop_Model_EnemyUpdateBleedDamage);
		}
	}

	public virtual void OnUpdateShadowSealing(Enemy.ShadowSealingSyncData sync_data)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyUpdateShadowSealing coop_Model_EnemyUpdateShadowSealing = new Coop_Model_EnemyUpdateShadowSealing();
			coop_Model_EnemyUpdateShadowSealing.id = base.owner.id;
			coop_Model_EnemyUpdateShadowSealing.sync_data = sync_data;
			SendBroadcast(coop_Model_EnemyUpdateShadowSealing);
		}
	}

	public virtual void OnUpdateBombArrow(int regionId)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyUpdateBombArrow coop_Model_EnemyUpdateBombArrow = new Coop_Model_EnemyUpdateBombArrow();
			coop_Model_EnemyUpdateBombArrow.id = base.owner.id;
			coop_Model_EnemyUpdateBombArrow.regionId = regionId;
			SendBroadcast(coop_Model_EnemyUpdateBombArrow);
		}
	}

	public virtual void OnActAngry(int angryActionId, uint angryId)
	{
		Coop_Model_EnemyAngry coop_Model_EnemyAngry = new Coop_Model_EnemyAngry();
		coop_Model_EnemyAngry.id = base.owner.id;
		coop_Model_EnemyAngry.angryActionId = angryActionId;
		coop_Model_EnemyAngry.angryId = angryId;
		coop_Model_EnemyAngry.execAngryIds = enemy.ExecAngryIDList;
		coop_Model_EnemyAngry.SetSyncPosition(base.owner);
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_EnemyAngry, promise: true);
		}
		StackActionHistory(coop_Model_EnemyAngry, is_act_model: true);
	}

	public virtual void OnActStep(int motion_id)
	{
		Coop_Model_EnemyStep coop_Model_EnemyStep = new Coop_Model_EnemyStep();
		coop_Model_EnemyStep.id = base.owner.id;
		coop_Model_EnemyStep.SetSyncPosition(base.owner);
		coop_Model_EnemyStep.motion_id = motion_id;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_EnemyStep);
		}
		StackActionHistory(coop_Model_EnemyStep, is_act_model: true);
	}

	public virtual void OnReviveRegion(int region_id)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyReviveRegion coop_Model_EnemyReviveRegion = new Coop_Model_EnemyReviveRegion();
			coop_Model_EnemyReviveRegion.id = base.owner.id;
			coop_Model_EnemyReviveRegion.region_id = region_id;
			SendBroadcast(coop_Model_EnemyReviveRegion, promise: true, null, delegate(Coop_Model_Base send_model)
			{
				if (base.owner == null)
				{
					return false;
				}
				Coop_Model_EnemyReviveRegion coop_Model_EnemyReviveRegion2 = send_model as Coop_Model_EnemyReviveRegion;
				return ((int)enemy.regionWorks[coop_Model_EnemyReviveRegion2.region_id].hp > 0) ? true : false;
			});
		}
		ClearActionHistory();
	}

	public virtual void OnSetWarp()
	{
		Coop_Model_EnemyWarp coop_Model_EnemyWarp = new Coop_Model_EnemyWarp();
		coop_Model_EnemyWarp.id = base.owner.id;
		coop_Model_EnemyWarp.SetSyncPosition(base.owner);
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_EnemyWarp);
		}
		StackActionHistory(coop_Model_EnemyWarp, is_act_model: false);
	}

	public void TargetRandamShotEvent(List<Enemy.RandomShotInfo.TargetInfo> targets)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyTargetShotEvent coop_Model_EnemyTargetShotEvent = new Coop_Model_EnemyTargetShotEvent();
			coop_Model_EnemyTargetShotEvent.id = base.owner.id;
			coop_Model_EnemyTargetShotEvent.targets = targets;
			SendBroadcast(coop_Model_EnemyTargetShotEvent);
		}
	}

	public void TargetRandamShotEvent(List<Vector3> points)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyRandomShotEvent coop_Model_EnemyRandomShotEvent = new Coop_Model_EnemyRandomShotEvent();
			coop_Model_EnemyRandomShotEvent.id = base.owner.id;
			coop_Model_EnemyRandomShotEvent.points = points;
			SendBroadcast(coop_Model_EnemyRandomShotEvent);
		}
	}

	public void OnReleaseGrabbed(float angle, float power)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyReleasedGrabbedPlayer coop_Model_EnemyReleasedGrabbedPlayer = new Coop_Model_EnemyReleasedGrabbedPlayer();
			coop_Model_EnemyReleasedGrabbedPlayer.id = base.owner.id;
			coop_Model_EnemyReleasedGrabbedPlayer.angle = angle;
			coop_Model_EnemyReleasedGrabbedPlayer.power = power;
			SendBroadcast(coop_Model_EnemyReleasedGrabbedPlayer);
		}
	}

	public void OnShotBullet(string atkName, List<Vector3> points, List<Quaternion> rots)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyShot coop_Model_EnemyShot = new Coop_Model_EnemyShot();
			coop_Model_EnemyShot.id = base.owner.id;
			coop_Model_EnemyShot.atkName = atkName;
			coop_Model_EnemyShot.posList = points;
			coop_Model_EnemyShot.rotList = rots;
			SendBroadcast(coop_Model_EnemyShot);
		}
	}

	public void OnCreateIceFloor(string atkName, List<Vector3> points, List<Quaternion> rots)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_CreateIceFloor coop_Model_CreateIceFloor = new Coop_Model_CreateIceFloor();
			coop_Model_CreateIceFloor.id = base.owner.id;
			coop_Model_CreateIceFloor.atkName = atkName;
			coop_Model_CreateIceFloor.posList = points;
			coop_Model_CreateIceFloor.rotList = rots;
			SendBroadcast(coop_Model_CreateIceFloor);
		}
	}

	public void OnCreateActionMine(string atkInfoName, int randSeed)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_ActionMine coop_Model_ActionMine = new Coop_Model_ActionMine();
			coop_Model_ActionMine.type = 3;
			coop_Model_ActionMine.id = base.owner.id;
			coop_Model_ActionMine.atkInfoName = atkInfoName;
			coop_Model_ActionMine.randSeed = randSeed;
			SendBroadcast(coop_Model_ActionMine);
		}
	}

	public void OnCreateReflectBullet(int objId, int randSeed)
	{
		if (base.enableSend && (base.owner.IsOriginal() || base.owner.IsMirror()))
		{
			Coop_Model_ActionMine coop_Model_ActionMine = new Coop_Model_ActionMine();
			coop_Model_ActionMine.type = 2;
			coop_Model_ActionMine.id = base.owner.id;
			coop_Model_ActionMine.objId = objId;
			coop_Model_ActionMine.randSeed = randSeed;
			SendBroadcast(coop_Model_ActionMine);
		}
	}

	public void OnDestroyActionMine(int objId, bool isExplode)
	{
		if (base.enableSend && (base.owner.IsOriginal() || base.owner.IsMirror()))
		{
			Coop_Model_ActionMine coop_Model_ActionMine = new Coop_Model_ActionMine();
			coop_Model_ActionMine.type = (isExplode ? 1 : 0);
			coop_Model_ActionMine.id = base.owner.id;
			coop_Model_ActionMine.objId = objId;
			SendBroadcast(coop_Model_ActionMine);
		}
	}

	public void OnReflectBulletAttack(string atkInfoName, string nodeName, int randSeed)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_ActionMine coop_Model_ActionMine = new Coop_Model_ActionMine();
			coop_Model_ActionMine.type = 2;
			coop_Model_ActionMine.id = base.owner.id;
			coop_Model_ActionMine.atkInfoName = atkInfoName;
			coop_Model_ActionMine.objId = -1;
			coop_Model_ActionMine.nodeName = nodeName;
			coop_Model_ActionMine.randSeed = randSeed;
			SendBroadcast(coop_Model_ActionMine);
		}
	}

	public void OnRecoverHp(int recoverValue)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyRecoverHp coop_Model_EnemyRecoverHp = new Coop_Model_EnemyRecoverHp();
			coop_Model_EnemyRecoverHp.id = base.owner.id;
			coop_Model_EnemyRecoverHp.value = recoverValue;
			SendBroadcast(coop_Model_EnemyRecoverHp);
		}
	}

	public void OnTurnUp()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemyTurnUp coop_Model_EnemyTurnUp = new Coop_Model_EnemyTurnUp();
			coop_Model_EnemyTurnUp.id = base.owner.id;
			SendBroadcast(coop_Model_EnemyTurnUp);
		}
	}

	public void OnEnemySyncTarget(StageObject target)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_EnemySyncTarget coop_Model_EnemySyncTarget = new Coop_Model_EnemySyncTarget();
			coop_Model_EnemySyncTarget.id = base.owner.id;
			coop_Model_EnemySyncTarget.targetId = ((target != null) ? target.id : (-1));
			SendBroadcast(coop_Model_EnemySyncTarget);
		}
	}

	public void OnEnemyRegionNodeActivate(int[] regionIDs, bool isRandom = false, int randomSelectedID = -1)
	{
		if (base.enabled && base.owner.IsOriginal())
		{
			Coop_Model_EnemyRegionNodeActivate coop_Model_EnemyRegionNodeActivate = new Coop_Model_EnemyRegionNodeActivate();
			coop_Model_EnemyRegionNodeActivate.id = base.owner.id;
			coop_Model_EnemyRegionNodeActivate.regionIDs = regionIDs;
			coop_Model_EnemyRegionNodeActivate.isRandom = isRandom;
			coop_Model_EnemyRegionNodeActivate.randomSelectedID = randomSelectedID;
			SendBroadcast(coop_Model_EnemyRegionNodeActivate);
		}
	}

	public void OnSummonAttack(int enemyId, int attackId, Vector3 pos, Vector3 rot, int targetId)
	{
		if (base.enabled && base.owner.IsOriginal())
		{
			Coop_Model_EnemySummonAttack coop_Model_EnemySummonAttack = new Coop_Model_EnemySummonAttack();
			coop_Model_EnemySummonAttack.id = base.owner.id;
			coop_Model_EnemySummonAttack.enemyId = enemyId;
			coop_Model_EnemySummonAttack.attackId = attackId;
			coop_Model_EnemySummonAttack.summonPos = pos;
			coop_Model_EnemySummonAttack.summonRot = rot;
			coop_Model_EnemySummonAttack.targetId = targetId;
			SendBroadcast(coop_Model_EnemySummonAttack);
		}
	}
}
