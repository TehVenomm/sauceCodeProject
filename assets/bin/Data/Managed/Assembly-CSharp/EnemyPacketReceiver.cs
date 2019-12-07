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
			if (enemy.enemySender != null)
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
			Coop_Model_EnemyInitialize model16 = packet.GetModel<Coop_Model_EnemyInitialize>();
			enemy.ApplySyncPosition(model16.pos, model16.dir, !enemy.isCoopInitialized);
			enemy.hp = model16.hp;
			enemy.hpMax = model16.hpMax;
			enemy.damageHpRate = model16.hpDamageRate;
			enemy.downTotal = model16.downTotal;
			enemy.downCount = model16.downCount;
			enemy.concussionTotal = model16.concussionTotal;
			enemy.concussionMax = model16.concussionMax;
			enemy.concussionExtend = model16.concussionExtend;
			enemy.badStatusMax = model16.badStatusMax;
			enemy.NowAngryID = model16.nowAngryId;
			enemy.ExecAngryIDList = model16.execAngryIds;
			enemy.BarrierHp = model16.barrierHp;
			enemy.isHiding = model16.isHiding;
			enemy.ShieldHp = model16.shieldHp;
			enemy.GrabHp = model16.grabHp;
			enemy.bulletIndex = model16.bulletIndex;
			enemy.walkSpeedRateFromTable = model16.walkSpeedRateFromTable;
			enemy.SetupAegis(model16.aegisSetupParam);
			enemy.changeElementIcon = model16.changeElementIcon;
			enemy.changeWeakElementIcon = model16.changeWeakElementIcon;
			enemy.changeToleranceRegionId = model16.changeToleranceRegionId;
			enemy.changeToleranceScroll = model16.changeToleranceScroll;
			enemy.ProcessElementToleranceChange(enemy.changeToleranceRegionId, enemy.changeToleranceScroll);
			enemy.SetBlendColor(model16.shaderSyncParam);
			enemy.deadReviveCount = model16.deadReviveCount;
			enemy.isFirstMadMode = model16.isFirstMadMode;
			enemy.regionWorks.ApplyRegionWorks(model16);
			enemy.UpdateRegionVisual();
			StageObject target3 = null;
			if (model16.target_id >= 0)
			{
				target3 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model16.target_id);
			}
			enemy.SetActionTarget(target3);
			enemy.buffParam.SetSyncParam(model16.buff_sync_param);
			enemy.continusAttackParam.ApplySyncParam(model16.cntAtkSyncParam);
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(enemy);
			enemy.gameObject.SetActive(value: true);
			SetFilterMode(FILTER_MODE.NONE);
			enemy.isCoopInitialized = true;
			enemy.SetAppearPos(enemy._position);
			if (enemy.IsValidShield())
			{
				enemy.RequestShieldShaderEffect();
			}
			if (enemy.tailController != null)
			{
				enemy.tailController.SetPreviousPositionList(model16.tailPosList);
			}
			if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate == 0f)
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate = enemy.damageHpRate;
			}
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.SetEnemyRecoveredHP(enemy.id, model16.recoveredHP);
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
				enemy.ActIdle();
			}
			break;
		}
		case PACKET_TYPE.ENEMY_UPDATE_BLEED_DAMAGE:
		{
			Coop_Model_EnemyUpdateBleedDamage model10 = packet.GetModel<Coop_Model_EnemyUpdateBleedDamage>();
			enemy.OnUpdateBleedDamage(model10.sync_data);
			break;
		}
		case PACKET_TYPE.ENEMY_UPDATE_SHADOWSEALING:
		{
			Coop_Model_EnemyUpdateShadowSealing model6 = packet.GetModel<Coop_Model_EnemyUpdateShadowSealing>();
			enemy.OnUpdateShadowSealing(model6.sync_data);
			break;
		}
		case PACKET_TYPE.ENEMY_STEP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyStep model11 = packet.GetModel<Coop_Model_EnemyStep>();
			enemy.ApplySyncPosition(model11.pos, model11.dir);
			enemy.ActStep(model11.motion_id);
			break;
		}
		case PACKET_TYPE.ENEMY_ANGRY:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyAngry model3 = packet.GetModel<Coop_Model_EnemyAngry>();
			enemy.ApplySyncPosition(model3.pos, model3.dir);
			enemy.ActAngry(model3.angryActionId, model3.angryId);
			enemy.ExecAngryIDList = model3.execAngryIds;
			break;
		}
		case PACKET_TYPE.ENEMY_REVIVE_REGION:
		{
			Coop_Model_EnemyReviveRegion model17 = packet.GetModel<Coop_Model_EnemyReviveRegion>();
			enemy.ReviveRegion(model17.region_id);
			break;
		}
		case PACKET_TYPE.ENEMY_WARP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyWarp model14 = packet.GetModel<Coop_Model_EnemyWarp>();
			enemy.ApplySyncPosition(model14.pos, model14.dir, force_sync: true);
			enemy.SetWarp();
			break;
		}
		case PACKET_TYPE.ENEMY_TARGRTSHOT_EVENT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyTargetShotEvent model8 = packet.GetModel<Coop_Model_EnemyTargetShotEvent>();
			enemy.TargetRandamShotEvent(model8.targets);
			break;
		}
		case PACKET_TYPE.ENEMY_RANDOMSHOT_EVENT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyRandomShotEvent model2 = packet.GetModel<Coop_Model_EnemyRandomShotEvent>();
			enemy.PointRandamShotEvent(model2.points);
			break;
		}
		case PACKET_TYPE.ENEMY_RELEASE_GRABBED_PLAYER:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyReleasedGrabbedPlayer model15 = packet.GetModel<Coop_Model_EnemyReleasedGrabbedPlayer>();
			enemy.ActReleaseGrabbedPlayers(isWeakHit: false, isSpWeakhit: false, forceRelease: true, model15.angle, model15.power);
			break;
		}
		case PACKET_TYPE.ENEMY_SHOT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_EnemyShot model13 = packet.GetModel<Coop_Model_EnemyShot>();
			enemy.ActShotBullet(model13.atkName, model13.posList, model13.rotList);
			break;
		}
		case PACKET_TYPE.ENEMY_RECOVER_HP:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemyRecoverHp model7 = packet.GetModel<Coop_Model_EnemyRecoverHp>();
			enemy.RecoverHp(model7.value, isSend: true);
			break;
		}
		case PACKET_TYPE.CREATE_ICE_FLOOR:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_CreateIceFloor model5 = packet.GetModel<Coop_Model_CreateIceFloor>();
			enemy.ActCreateIceFloor(model5.atkName, model5.posList, model5.rotList);
			break;
		}
		case PACKET_TYPE.ACTION_MINE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_ActionMine model18 = packet.GetModel<Coop_Model_ActionMine>();
			switch (model18.type)
			{
			case 0:
				enemy.ActDestroyActionMine(model18.objId, isExplode: false);
				break;
			case 1:
				enemy.ActDestroyActionMine(model18.objId, isExplode: true);
				break;
			case 2:
				enemy.ActCreateReflectBullet(model18.atkInfoName, model18.nodeName, model18.objId, model18.randSeed);
				break;
			case 3:
				enemy.ActCreateActionMine(model18.atkInfoName, model18.randSeed);
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
			Coop_Model_EnemySyncTarget model12 = packet.GetModel<Coop_Model_EnemySyncTarget>();
			StageObject target2 = null;
			if (model12.targetId >= 0)
			{
				target2 = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model12.targetId);
			}
			enemy.SetActionTarget(target2);
			break;
		}
		case PACKET_TYPE.ENEMY_REGION_NODE_ACTIVATE:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemyRegionNodeActivate model9 = packet.GetModel<Coop_Model_EnemyRegionNodeActivate>();
			enemy.ActivateRegionNode(model9.regionIDs, model9.isRandom, model9.randomSelectedID);
			break;
		}
		case PACKET_TYPE.ENEMY_SUMMON_ATTACK:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemySummonAttack model4 = packet.GetModel<Coop_Model_EnemySummonAttack>();
			StageObject target = null;
			if (model4.targetId > 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(model4.targetId);
			}
			enemy.SetActionTarget(target);
			enemy.ActSummonAttack(model4.enemyId, model4.attackId, model4.summonPos, model4.summonRot);
			break;
		}
		case PACKET_TYPE.ENEMY_UPDATE_BOMBARROW:
		{
			if (enemy.isDead)
			{
				return true;
			}
			Coop_Model_EnemyUpdateBombArrow model = packet.GetModel<Coop_Model_EnemyUpdateBombArrow>();
			enemy.OnUpdateBombArrow(model.regionId);
			break;
		}
		default:
			return base.HandleCoopEvent(packet);
		}
		return true;
	}
}
