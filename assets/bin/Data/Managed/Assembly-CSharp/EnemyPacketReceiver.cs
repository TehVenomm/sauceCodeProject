using UnityEngine;

public class EnemyPacketReceiver : CharacterPacketReceiver
{
	protected Enemy enemy => (Enemy)base.owner;

	protected override bool CheckFilterPacket(CoopPacket packet)
	{
		FILTER_MODE filterMode = base.filterMode;
		if (filterMode == FILTER_MODE.WAIT_INITIALIZE && packet.packetType == PACKET_TYPE.ENEMY_INITIALIZE)
		{
			return true;
		}
		return base.CheckFilterPacket(packet);
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		switch (packet.packetType)
		{
		case PACKET_TYPE.ENEMY_LOAD_COMPLETE:
			if ((Object)enemy.enemySender != (Object)null)
			{
				enemy.enemySender.OnRecvLoadComplete(packet.fromClientId);
			}
			break;
		case PACKET_TYPE.ENEMY_INITIALIZE:
		{
			if (enemy.isLoading)
			{
				return false;
			}
			Coop_Model_EnemyInitialize model8 = packet.GetModel<Coop_Model_EnemyInitialize>();
			enemy.ApplySyncPosition(model8.pos, model8.dir, !enemy.isCoopInitialized);
			enemy.hp = model8.hp;
			enemy.hpMax = model8.hpMax;
			enemy.damageHpRate = model8.hpDamageRate;
			enemy.downTotal = model8.downTotal;
			enemy.downCount = model8.downCount;
			enemy.badStatusMax = model8.badStatusMax;
			enemy.NowAngryID = model8.nowAngryId;
			enemy.ExecAngryIDList = model8.execAngryIds;
			enemy.BarrierHp = model8.barrierHp;
			enemy.isHiding = model8.isHiding;
			enemy.ShieldHp = model8.shieldHp;
			enemy.GrabHp = model8.grabHp;
			enemy.bulletIndex = model8.bulletIndex;
			enemy.walkSpeedRateFromTable = model8.walkSpeedRateFromTable;
			enemy.SetupAegis(model8.aegisSetupParam);
			enemy.changeElementIcon = model8.changeElementIcon;
			enemy.changeWeakElementIcon = model8.changeWeakElementIcon;
			enemy.changeToleranceRegionId = model8.changeToleranceRegionId;
			enemy.changeToleranceScroll = model8.changeToleranceScroll;
			enemy.ProcessElementToleranceChange(enemy.changeToleranceRegionId, enemy.changeToleranceScroll);
			enemy.SetBlendColor(model8.shaderSyncParam);
			enemy.regionWorks.ApplyRegionWorks(model8);
			enemy.UpdateRegionVisual();
			StageObject target2 = null;
			if (model8.target_id >= 0)
			{
				target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model8.target_id);
			}
			enemy.SetActionTarget(target2, true);
			enemy.buffParam.SetSyncParam(model8.buff_sync_param, true);
			enemy.continusAttackParam.ApplySyncParam(model8.cntAtkSyncParam);
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(enemy);
			enemy.gameObject.SetActive(true);
			SetFilterMode(FILTER_MODE.NONE);
			enemy.isCoopInitialized = true;
			enemy.SetAppearPos(enemy._position);
			if (enemy.IsValidShield())
			{
				enemy.RequestShieldShaderEffect();
			}
			if ((Object)enemy.tailController != (Object)null)
			{
				enemy.tailController.SetPreviousPositionList(model8.tailPosList);
			}
			if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate == 0f)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate = enemy.damageHpRate;
			}
			if (enemy.isHideSpawn)
			{
				if (enemy.isHiding)
				{
					enemy.InitHide();
				}
				else
				{
					enemy.TurnUpImmediate();
				}
			}
			else
			{
				enemy.ActIdle(false, -1f);
			}
			break;
		}
		case PACKET_TYPE.ENEMY_UPDATE_BLEED_DAMAGE:
		{
			Coop_Model_EnemyUpdateBleedDamage model2 = packet.GetModel<Coop_Model_EnemyUpdateBleedDamage>();
			enemy.OnUpdateBleedDamage(model2.sync_data);
			break;
		}
		case PACKET_TYPE.ENEMY_UPDATE_SHADOWSEALING:
		{
			Coop_Model_EnemyUpdateShadowSealing model14 = packet.GetModel<Coop_Model_EnemyUpdateShadowSealing>();
			enemy.OnUpdateShadowSealing(model14.sync_data);
			break;
		}
		case PACKET_TYPE.ENEMY_STEP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyStep model3 = packet.GetModel<Coop_Model_EnemyStep>();
			enemy.ApplySyncPosition(model3.pos, model3.dir, false);
			enemy.ActStep(model3.motion_id);
			break;
		}
		case PACKET_TYPE.ENEMY_ANGRY:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyAngry model12 = packet.GetModel<Coop_Model_EnemyAngry>();
			enemy.ApplySyncPosition(model12.pos, model12.dir, false);
			enemy.ActAngry(model12.angryActionId, model12.angryId);
			enemy.ExecAngryIDList = model12.execAngryIds;
			break;
		}
		case PACKET_TYPE.ENEMY_REVIVE_REGION:
		{
			Coop_Model_EnemyReviveRegion model9 = packet.GetModel<Coop_Model_EnemyReviveRegion>();
			enemy.ReviveRegion(model9.region_id);
			break;
		}
		case PACKET_TYPE.ENEMY_WARP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyWarp model6 = packet.GetModel<Coop_Model_EnemyWarp>();
			enemy.ApplySyncPosition(model6.pos, model6.dir, true);
			enemy.SetWarp();
			break;
		}
		case PACKET_TYPE.ENEMY_TARGRTSHOT_EVENT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyTargetShotEvent model15 = packet.GetModel<Coop_Model_EnemyTargetShotEvent>();
			enemy.TargetRandamShotEvent(model15.targets);
			break;
		}
		case PACKET_TYPE.ENEMY_RANDOMSHOT_EVENT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyRandomShotEvent model11 = packet.GetModel<Coop_Model_EnemyRandomShotEvent>();
			enemy.PointRandamShotEvent(model11.points);
			break;
		}
		case PACKET_TYPE.ENEMY_RELEASE_GRABBED_PLAYER:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyReleasedGrabbedPlayer model7 = packet.GetModel<Coop_Model_EnemyReleasedGrabbedPlayer>();
			enemy.ActReleaseGrabbedPlayers(false, false, true, model7.angle, model7.power);
			break;
		}
		case PACKET_TYPE.ENEMY_SHOT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyShot model5 = packet.GetModel<Coop_Model_EnemyShot>();
			enemy.ActShotBullet(model5.atkName, model5.posList, model5.rotList);
			break;
		}
		case PACKET_TYPE.ENEMY_RECOVER_HP:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemyRecoverHp model16 = packet.GetModel<Coop_Model_EnemyRecoverHp>();
			enemy.RecoverHp(model16.value, true);
			break;
		}
		case PACKET_TYPE.CREATE_ICE_FLOOR:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_CreateIceFloor model13 = packet.GetModel<Coop_Model_CreateIceFloor>();
			enemy.ActCreateIceFloor(model13.atkName, model13.posList, model13.rotList);
			break;
		}
		case PACKET_TYPE.ACTION_MINE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_ActionMine model10 = packet.GetModel<Coop_Model_ActionMine>();
			switch (model10.type)
			{
			case 0:
				enemy.ActDestroyActionMine(model10.objId, false);
				break;
			case 1:
				enemy.ActDestroyActionMine(model10.objId, true);
				break;
			case 2:
				enemy.ActCreateReflectBullet(model10.atkInfoName, model10.nodeName, model10.objId, model10.randSeed);
				break;
			case 3:
				enemy.ActCreateActionMine(model10.atkInfoName, model10.randSeed);
				break;
			}
			break;
		}
		case PACKET_TYPE.ENEMY_TURN_UP:
			if (enemy.isDead)
			{
				return true;
			}
			enemy.TurnUp();
			break;
		case PACKET_TYPE.ENEMY_SYNC_TARGET:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemySyncTarget model4 = packet.GetModel<Coop_Model_EnemySyncTarget>();
			StageObject target = null;
			if (model4.targetId >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model4.targetId);
			}
			enemy.SetActionTarget(target, true);
			break;
		}
		case PACKET_TYPE.ENEMY_REGION_NODE_ACTIVATE:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemyRegionNodeActivate model = packet.GetModel<Coop_Model_EnemyRegionNodeActivate>();
			enemy.ActivateRegionNode(model.regionIDs, model.isRandom, model.randomSelectedID);
			break;
		}
		default:
			return base.HandleCoopEvent(packet);
		}
		return true;
	}
}
