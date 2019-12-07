using UnityEngine;

public class PlayerPacketReceiver : CharacterPacketReceiver
{
	protected Player player => (Player)base.owner;

	protected override bool CheckFilterPacket(CoopPacket packet)
	{
		FILTER_MODE filterMode = base.filterMode;
		if (filterMode == FILTER_MODE.WAIT_INITIALIZE && packet.packetType == PACKET_TYPE.PLAYER_INITIALIZE)
		{
			return true;
		}
		return base.CheckFilterPacket(packet);
	}

	protected override bool HandleCoopEvent(CoopPacket packet)
	{
		switch (packet.packetType)
		{
		case PACKET_TYPE.PLAYER_LOAD_COMPLETE:
			if (!player.isSetAppearPos)
			{
				return false;
			}
			if (player.playerSender != null)
			{
				player.playerSender.OnRecvLoadComplete(packet.fromClientId);
			}
			break;
		case PACKET_TYPE.PLAYER_INITIALIZE:
		{
			if (player.isLoading)
			{
				return false;
			}
			Coop_Model_PlayerInitialize model4 = packet.GetModel<Coop_Model_PlayerInitialize>();
			player.ApplySyncPosition(model4.pos, model4.dir, force_sync: true);
			player.hp = model4.hp;
			player.healHp = model4.healHp;
			player.StopCounter(model4.stopcounter);
			StageObject target = null;
			if (model4.target_id >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model4.target_id);
			}
			player.SetActionTarget(target);
			player.buffParam.SetSyncParam(model4.buff_sync_param);
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncOwnerData(model4.id);
			}
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player);
			player.gameObject.SetActive(value: true);
			SetFilterMode(FILTER_MODE.NONE);
			player.isCoopInitialized = true;
			player.SetAppearPos(player._position);
			bool flag = false;
			if (player.weaponData == null != (model4.weapon_item == null))
			{
				flag = true;
			}
			else if (player.weaponData != null && player.weaponData.eId != model4.weapon_item.eId)
			{
				flag = true;
			}
			else if (player.weaponIndex != model4.weapon_index)
			{
				flag = true;
			}
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
			if (coopClient != null && !coopClient.IsBattleStart())
			{
				player.WaitBattleStart();
				player.gameObject.SetActive(value: false);
				MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject(player);
			}
			else if (flag)
			{
				player.LoadWeapon(model4.weapon_item, model4.weapon_index, delegate
				{
					player.ActBattleStart(effect_only: true);
				});
			}
			else
			{
				player.SetNowWeapon(model4.weapon_item, model4.weapon_index, player.uniqueEquipmentIndex);
				player.InitParameter();
				if (player.hp <= 0)
				{
					player.ActBattleStart(effect_only: true);
					if (!player.isDead)
					{
						player.ActDeadLoop();
					}
				}
				else if (player.fishingCtrl != null && model4.fishingState > 0 && model4.gatherGimmickId > 0)
				{
					player.ActBattleStart(effect_only: true);
					FishingController.eState fishingState = (FishingController.eState)model4.fishingState;
					if (fishingState == FishingController.eState.Coop)
					{
						player.ActCoopFishingStart(model4.gatherGimmickId);
						player.SetLerpRotation(Vector3.zero);
					}
					else
					{
						player.ActGatherGimmick(model4.gatherGimmickId);
						player.SetLerpRotation(Vector3.zero);
						player.fishingCtrl.ChangeState(fishingState);
						if (fishingState == FishingController.eState.Fight)
						{
							player.PlayMotion("fishing_send");
							player.EventActionRendererON(null);
							SoundManager.PlayLoopSE(player.fishingCtrl.GetSeId(2), player, player.FindNode(""));
						}
					}
				}
				else if (model4.carryingGimmickId > 0)
				{
					player.ActCarry(InGameProgress.eFieldGimmick.CarriableGimmick, model4.carryingGimmickId);
				}
				else if (model4.act_battle_start)
				{
					player.ActBattleStart();
				}
				else
				{
					player.ActBattleStart(effect_only: true);
					player.ActIdle();
				}
			}
			player.SetSyncUsingCannon(model4.cannonId);
			player.bulletIndex = model4.bulletIndex;
			break;
		}
		case PACKET_TYPE.PLAYER_ATTACK_COMBO:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerAttackCombo model49 = packet.GetModel<Coop_Model_PlayerAttackCombo>();
			base.owner._position = model49.pos;
			base.owner._rotation = Quaternion.AngleAxis(model49.dir, Vector3.up);
			player.ActAttack(model49.attack_id, send_packet: true, sync_immediately: false, model49.motionLayerName, model49.motionStateName);
			player.SetActionPosition(model49.act_pos, model49.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeRelease model42 = packet.GetModel<Coop_Model_PlayerChargeRelease>();
			player.ApplySyncExRush(model42.isExRushCharge);
			player.ApplySyncPosition(model42.pos, model42.dir);
			player.SetChargeRelease(model42.charge_rate);
			player.SetLerpRotation(Quaternion.AngleAxis(model42.lerp_dir, Vector3.up) * Vector3.forward);
			player.SetActionPosition(model42.act_pos, model42.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_RESTRAINT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRestraint model7 = packet.GetModel<Coop_Model_PlayerRestraint>();
			RestraintInfo restraintInfo = new RestraintInfo();
			restraintInfo.enable = true;
			restraintInfo.duration = model7.duration;
			restraintInfo.damageInterval = model7.damageInterval;
			restraintInfo.damageRate = model7.damageRate;
			restraintInfo.reduceTimeByFlick = model7.reduceTimeByFlick;
			restraintInfo.effectName = model7.effectName;
			restraintInfo.isStopMotion = model7.isStopMotion;
			restraintInfo.isDisableRemoveByPlayerAttack = model7.isDisableRemoveByPlayerAttack;
			player.ActRestraint(restraintInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_RESTRAINT_END:
			if (base.character.isDead)
			{
				return true;
			}
			player.ActRestraintEnd();
			break;
		case PACKET_TYPE.PLAYER_AVOID:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerAvoid model16 = packet.GetModel<Coop_Model_PlayerAvoid>();
			player.ApplySyncPosition(model16.pos, model16.dir);
			player.ActAvoid();
			break;
		}
		case PACKET_TYPE.PLAYER_WARP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerWarp model53 = packet.GetModel<Coop_Model_PlayerWarp>();
			player.ApplySyncPosition(model53.pos, model53.dir);
			player.ActWarp();
			break;
		}
		case PACKET_TYPE.PLAYER_BLOW_CLEAR:
			if (base.character.isDead)
			{
				return true;
			}
			player.InputBlowClear();
			break;
		case PACKET_TYPE.PLAYER_STUNNED_END:
			if (base.character.isDead)
			{
				return true;
			}
			player.SetStunnedEnd();
			break;
		case PACKET_TYPE.PLAYER_DEAD_COUNT:
		{
			Coop_Model_PlayerDeadCount model20 = packet.GetModel<Coop_Model_PlayerDeadCount>();
			player.DeadCount(model20.remaind_time, model20.stop, model20.requested);
			break;
		}
		case PACKET_TYPE.PLAYER_DEAD_COUNT_REQUEST:
			packet.GetModel<Coop_Model_PlayerDeadCountRequest>();
			if (player.rescueTime > 0f && !player.IsPrayed() && !player.isWaitingResurrectionHoming)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnDeadCountRequest(packet.fromClientId, player);
			}
			break;
		case PACKET_TYPE.PLAYER_DEAD_STANDUP:
		{
			Coop_Model_PlayerDeadStandup model57 = packet.GetModel<Coop_Model_PlayerDeadStandup>();
			player.ActDeadStandup(model57.standupHp, model57.cType);
			break;
		}
		case PACKET_TYPE.PLAYER_STOP_COUNTER:
		{
			Coop_Model_PlayerStopCounter model54 = packet.GetModel<Coop_Model_PlayerStopCounter>();
			player.StopCounter(model54.stop);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGather model27 = packet.GetModel<Coop_Model_PlayerGather>();
			GatherPointObject gatherPointObject = null;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.gatherPointList != null)
			{
				int i = 0;
				for (int count = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList.Count; i < count; i++)
				{
					if (model27.point_id == (int)MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i].pointData.pointID)
					{
						gatherPointObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i];
						break;
					}
				}
			}
			if (gatherPointObject != null)
			{
				player.ApplySyncPosition(model27.pos, model27.dir);
				player.ActGather(gatherPointObject);
				player.SetActionPosition(model27.act_pos, model27.act_pos_f);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_SKILL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSkillAction model13 = packet.GetModel<Coop_Model_PlayerSkillAction>();
			player.ApplySyncPosition(model13.pos, model13.dir);
			player.ActSkillAction(model13.skill_index, model13.isUsingSecondGrade);
			player.SetActionPosition(model13.act_pos, model13.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_HEAL:
		{
			Coop_Model_PlayerGetHeal model11 = packet.GetModel<Coop_Model_PlayerGetHeal>();
			player.ExecHealHp(model11.Deserialize(), !model11.receive);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialAction model67 = packet.GetModel<Coop_Model_PlayerSpecialAction>();
			player.ApplySyncPosition(model67.pos, model67.dir);
			player.ActSpecialAction(model67.start_effect, model67.isSuccess);
			player.SetActionPosition(model67.act_pos, model67.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialActionContinue model56 = packet.GetModel<Coop_Model_PlayerSpecialActionContinue>();
			player.ApplySyncPosition(model56.pos, model56.dir);
			player.ActSpAttackContinue();
			player.SetActionPosition(model56.act_pos, model56.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotArrow model50 = packet.GetModel<Coop_Model_PlayerShotArrow>();
			player.ApplySyncPosition(model50.pos, model50.dir);
			AttackInfo attack_info = player.FindAttackInfoExternal(model50.attack_name, fix_rate: true, model50.attack_rate);
			player.ShotArrow(model50.shot_pos, model50.shot_rot, attack_info, model50.is_sit_shot, model50.is_aim_end);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotSoulArrow model40 = packet.GetModel<Coop_Model_PlayerShotSoulArrow>();
			player.ApplySyncPosition(model40.pos, model40.dir);
			player.ShotSoulArrowPuppet(model40.shotPos, model40.bowRot, model40.targetPosList);
			break;
		}
		case PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO:
		{
			Coop_Model_PlayerUpdateSkillInfo model37 = packet.GetModel<Coop_Model_PlayerUpdateSkillInfo>();
			player.skillInfo.SetSettingsInfo(model37.settings_info, player.equipWeaponList);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_START:
		{
			Coop_Model_PlayerPrayerStart model36 = packet.GetModel<Coop_Model_PlayerPrayerStart>();
			Player.PrayInfo prayInfo2 = new Player.PrayInfo();
			prayInfo2.targetId = model36.sid;
			prayInfo2.reason = (Player.PRAY_REASON)model36.reason;
			player.OnPrayerStart(prayInfo2);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_END:
		{
			Coop_Model_PlayerPrayerEnd model34 = packet.GetModel<Coop_Model_PlayerPrayerEnd>();
			Player.PrayInfo prayInfo = new Player.PrayInfo();
			prayInfo.targetId = model34.sid;
			prayInfo.reason = (Player.PRAY_REASON)model34.reason;
			player.OnPrayerEnd(prayInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_BOOST:
		{
			Coop_Model_PlayerPrayerBoost model32 = packet.GetModel<Coop_Model_PlayerPrayerBoost>();
			player.OnChangeBoostPray(model32.sid, model32.boostPrayInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_CHANGE_WEAPON:
			if (base.character.isDead)
			{
				return true;
			}
			player.ActChangeWeapon(null, -1);
			break;
		case PACKET_TYPE.PLAYER_APPLY_CHANGE_WEAPON:
		{
			Coop_Model_PlayerApplyChangeWeapon model22 = packet.GetModel<Coop_Model_PlayerApplyChangeWeapon>();
			if (player.weaponData.eId == (uint)model22.item.eId && player.weaponIndex == model22.index)
			{
				return true;
			}
			player.ApplyChangeWeapon(model22.item, model22.index);
			break;
		}
		case PACKET_TYPE.PLAYER_SETSTATUS:
		{
			Coop_Model_PlayerSetStatus model17 = packet.GetModel<Coop_Model_PlayerSetStatus>();
			player.OnSetPlayerStatus(model17.level, model17.atk, model17.def, model17.hp);
			if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.LEVEL_UP, player);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_GET_RAREDROP:
		{
			if (!MonoBehaviourSingleton<UIInGameMessageBar>.IsValid())
			{
				break;
			}
			Coop_Model_PlayerGetRareDrop model8 = packet.GetModel<Coop_Model_PlayerGetRareDrop>();
			string text = null;
			switch (model8.type)
			{
			case 5:
			{
				SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)model8.item_id);
				if (skillItemData != null)
				{
					text = skillItemData.name;
				}
				break;
			}
			case 4:
			{
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)model8.item_id);
				if (equipItemData != null)
				{
					text = equipItemData.name;
				}
				break;
			}
			case 14:
			{
				AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint)model8.item_id);
				if (data != null)
				{
					text = data.name;
				}
				break;
			}
			default:
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)model8.item_id);
				if (itemData != null)
				{
					text = itemData.name;
				}
				break;
			}
			}
			if (text != null)
			{
				MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(player.charaName, StringTable.Format(STRING_CATEGORY.IN_GAME, 4000u, text));
			}
			break;
		}
		case PACKET_TYPE.PLAYER_GRABBED:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGrabbed model3 = packet.GetModel<Coop_Model_PlayerGrabbed>();
			GrabInfo grabInfo = new GrabInfo();
			grabInfo.parentNode = model3.nodeName;
			grabInfo.duration = model3.duration;
			grabInfo.drainAttackId = model3.drainAtkId;
			player.ActGrabbedStart(model3.enemyId, grabInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_GRABBED_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGrabbedEnd model65 = packet.GetModel<Coop_Model_PlayerGrabbedEnd>();
			player.ActGrabbedEnd(model65.angle, model65.power);
			break;
		}
		case PACKET_TYPE.PLAYER_SET_PRESENT_BULLET:
		{
			Coop_Model_PlayerSetPresentBullet model64 = packet.GetModel<Coop_Model_PlayerSetPresentBullet>();
			player.SetPresentBullet(model64.presentBulletId, (BulletData.BulletPresent.TYPE)model64.type, model64.position, model64.bulletName);
			break;
		}
		case PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET:
		{
			Coop_Model_PlayerPickPresentBullet model62 = packet.GetModel<Coop_Model_PlayerPickPresentBullet>();
			player.DestroyPresentBulletObject(model62.presentBulletId);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET:
		{
			Coop_Model_PlayerShotZoneBullet model61 = packet.GetModel<Coop_Model_PlayerShotZoneBullet>();
			player.ShotZoneBullet(player, model61.bulletName, model61.position);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET:
		{
			Coop_Model_PlayerShotDecoyBullet model59 = packet.GetModel<Coop_Model_PlayerShotDecoyBullet>();
			player.ShotDecoyBullet(model59.id, model59.skIndex, model59.decoyId, model59.bulletName, model59.position, isHit: false);
			break;
		}
		case PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET:
		{
			Coop_Model_PlayerExplodeDecoyBullet model58 = packet.GetModel<Coop_Model_PlayerExplodeDecoyBullet>();
			player.ExplodeDecoyBullet(model58.decoyId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_STANDBY:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonStandby model55 = packet.GetModel<Coop_Model_PlayerCannonStandby>();
			player.ApplySyncPosition(model55.pos, model55.dir);
			player.ActCannonStandby(model55.cannonId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_SHOT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonShot model52 = packet.GetModel<Coop_Model_PlayerCannonShot>();
			player.ApplySyncPosition(model52.pos, model52.dir);
			player.SetCannonState(Player.CANNON_STATE.READY);
			player.ApplyCannonVector(model52.cannonVec);
			player.ActCannonShot();
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_ROTATE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonRotate model47 = packet.GetModel<Coop_Model_PlayerCannonRotate>();
			player.SetSyncCannonRotation(model47.cannonVec);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE:
		{
			Coop_Model_PlayerGetChargeSkillGauge model46 = packet.GetModel<Coop_Model_PlayerGetChargeSkillGauge>();
			player.OnGetChargeSkillGauge((BuffParam.BUFFTYPE)model46.buffType, model46.buffValue, model46.useSkillIndex, !model46.receive, model46.isCorrectWaveMatch);
			break;
		}
		case PACKET_TYPE.PLAYER_RESURRECT:
			packet.GetModel<Coop_Model_PlayerResurrect>();
			player.OnResurrection(isPacket: true);
			break;
		case PACKET_TYPE.PLAYER_GET_RESURRECT:
			packet.GetModel<Coop_Model_PlayerGetResurrect>();
			player.OnGetResurrection();
			break;
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC:
		{
			Coop_Model_PlayerSpecialActionGaugeSync model43 = packet.GetModel<Coop_Model_PlayerSpecialActionGaugeSync>();
			player.OnSyncSpecialActionGauge(model43.weaponIndex, model43.currentSpActionGauge);
			player.pairSwordsCtrl.SetComboLv(model43.comboLv);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_EXPAND_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeExpandRelease model39 = packet.GetModel<Coop_Model_PlayerChargeExpandRelease>();
			player.ApplySyncPosition(model39.pos, model39.dir);
			player.SetChargeExpandRelease(model39.charge_rate);
			player.SetLerpRotation(Quaternion.AngleAxis(model39.lerp_dir, Vector3.up) * Vector3.forward);
			player.SetActionPosition(model39.act_pos, model39.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_RIZE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpRize model35 = packet.GetModel<Coop_Model_PlayerJumpRize>();
			player.OnJumpRize(model35.dir, model35.level);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpEnd model31 = packet.GetModel<Coop_Model_PlayerJumpEnd>();
			player.OnJumpEnd(model31.pos, model31.isSuccess, model31.y);
			break;
		}
		case PACKET_TYPE.PLAYER_SOUL_BOOST:
		{
			Coop_Model_PlayerSoulBoost model30 = packet.GetModel<Coop_Model_PlayerSoulBoost>();
			player.OnSoulBoost(model30.isBoost);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveActionSync model28 = packet.GetModel<Coop_Model_PlayerEvolveActionSync>();
			player.OnSyncEvolveAction(model28.isAction);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveSpecialAction model26 = packet.GetModel<Coop_Model_PlayerEvolveSpecialAction>();
			player.ApplySyncPosition(model26.pos, model26.dir);
			player.ActEvolveSpecialAction();
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_POS:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchPos model23 = packet.GetModel<Coop_Model_PlayerSnatchPos>();
			player.snatchCtrl.OnHit(model23.enemyId, model23.hitPoint);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_START:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveStart model19 = packet.GetModel<Coop_Model_PlayerSnatchMoveStart>();
			player.OnSnatchMoveStart(model19.snatchPos);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveEnd model14 = packet.GetModel<Coop_Model_PlayerSnatchMoveEnd>();
			player.SetActionPosition(model14.act_pos, model14.act_pos_f);
			player.ApplySyncPosition(model14.pos, model14.dir);
			player.OnSnatchMoveEnd(model14.triggerIndex);
			break;
		}
		case PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerPairSwordsLaserEnd model10 = packet.GetModel<Coop_Model_PlayerPairSwordsLaserEnd>();
			player.OnSyncSpecialActionGauge(model10.weaponIndex, model10.currentSpActionGauge);
			player.pairSwordsCtrl.OnLaserEnd(isPacket: true);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotHealingHoming model5 = packet.GetModel<Coop_Model_PlayerShotHealingHoming>();
			player.OnShotHealingHoming(model5);
			break;
		}
		case PACKET_TYPE.PLAYER_SACRIFICED_HP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSacrificedHp model2 = packet.GetModel<Coop_Model_PlayerSacrificedHp>();
			player.spearCtrl.SacrificedHp(model2.sacrificedHp, isPacket: true);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP_CREATE:
		{
			Coop_Model_WaveMatchDropCreate model70 = packet.GetModel<Coop_Model_WaveMatchDropCreate>();
			MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropCreate(model70);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP_PICKED:
		{
			Coop_Model_WaveMatchDropPicked model69 = packet.GetModel<Coop_Model_WaveMatchDropPicked>();
			MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropPicked(model69);
			break;
		}
		case PACKET_TYPE.GATHER_GIMMICK_INFO:
		{
			Coop_Model_GatherGimmickInfo model68 = packet.GetModel<Coop_Model_GatherGimmickInfo>();
			MonoBehaviourSingleton<InGameProgress>.I.UpdatGatherGimmickInfo(model68.managedId, model68.ownerId, model68.isUsed);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER_GIMMICK:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGatherGimmick model66 = packet.GetModel<Coop_Model_PlayerGatherGimmick>();
			player.ApplySyncPosition(model66.pos, model66.dir);
			player.ActGatherGimmick(model66.gimmickId);
			player.SetActionPosition(model66.act_pos, model66.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER_GIMMICK_STATE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGatherGimmickState model63 = packet.GetModel<Coop_Model_PlayerGatherGimmickState>();
			player.OnGatherGimmickState(model63.state);
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_POSITION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncPosition model60 = packet.GetModel<Coop_Model_PlayerSyncPosition>();
			player.ApplySyncPosition(model60.pos, model60.dir);
			break;
		}
		case PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_INCREASE:
			if (base.character.isDead)
			{
				return true;
			}
			if (player.fishingCtrl == null)
			{
				return true;
			}
			player.fishingCtrl.OnReceiveCoopFishingGaugeIncrease();
			break;
		case PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_SYNC:
		{
			if (base.character.isDead)
			{
				return true;
			}
			if (player.fishingCtrl == null)
			{
				return true;
			}
			Coop_Model_PlayerCoopFishingGaugeSync model51 = packet.GetModel<Coop_Model_PlayerCoopFishingGaugeSync>();
			player.fishingCtrl.OnCurrentGaugeSync(model51.ownerUserId, model51.gaugeValue);
			break;
		}
		case PACKET_TYPE.PLAYER_COOP_FISHING_START:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCoopFishingStart model48 = packet.GetModel<Coop_Model_PlayerCoopFishingStart>();
			player.ApplySyncPosition(model48.pos, model48.dir);
			player.ActCoopFishingStart(model48.gimmickId);
			player.SetActionPosition(model48.actPos, model48.actPosFlag);
			player.SetLerpRotation(Vector3.zero);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_RESURRECTION_HOMING:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotResurrectionHoming model45 = packet.GetModel<Coop_Model_PlayerShotResurrectionHoming>();
			player.OnShotResurrectionHoming(model45);
			break;
		}
		case PACKET_TYPE.PLAYER_STONE_COUNT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerStoneCount model44 = packet.GetModel<Coop_Model_PlayerStoneCount>();
			player.StoneCount(model44.remaind_time, model44.stop, model44.requested);
			break;
		}
		case PACKET_TYPE.PLAYER_STONE_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerStoneEnd model41 = packet.GetModel<Coop_Model_PlayerStoneEnd>();
			player.ActStoneEnd(model41.countTime);
			break;
		}
		case PACKET_TYPE.PLAYER_FLICK_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerFlickAction model38 = packet.GetModel<Coop_Model_PlayerFlickAction>();
			player.ApplySyncPosition(model38.pos, model38.dir);
			player.ActFlickAction(model38.inputVec, isOriginal: false);
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_COMBINE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncCombine model33 = packet.GetModel<Coop_Model_PlayerSyncCombine>();
			if (player != null && player.pairSwordsCtrl != null)
			{
				player.pairSwordsCtrl.CombineBurst(model33.isCombine);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_SUBSTITUTE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncSubstitute model29 = packet.GetModel<Coop_Model_PlayerSyncSubstitute>();
			if (player != null && player.buffParam != null && player.buffParam.substituteCtrl != null)
			{
				player.buffParam.substituteCtrl.Sync(model29.num);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_WEAPON_ACTION_START:
			if (player.isDead)
			{
				return true;
			}
			packet.GetModel<Coop_Model_PlayerWeaponActionStart>();
			player.EventWeaponActionStart();
			break;
		case PACKET_TYPE.PLAYER_WEAPON_ACTION_END:
			if (player.isDead)
			{
				return true;
			}
			packet.GetModel<Coop_Model_PlayerWeaponActionEnd>();
			player.EventWeaponActionEnd();
			break;
		case PACKET_TYPE.PLAYER_SHOT_SHIELD_REFLECT:
		{
			Coop_Model_PlayerShotShieldReflect model25 = packet.GetModel<Coop_Model_PlayerShotShieldReflect>();
			player.OnShotShieldReflect(model25);
			break;
		}
		case PACKET_TYPE.PLAYER_RAIN_SHOT_CHARGE_RELEASE:
		{
			if (player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRainShotChargeRelease model24 = packet.GetModel<Coop_Model_PlayerRainShotChargeRelease>();
			player.OnRainShotChargeRelease(model24.fallPos, model24.fallRotY);
			break;
		}
		case PACKET_TYPE.PLAYER_CARRY:
		{
			if (player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCarry model21 = packet.GetModel<Coop_Model_PlayerCarry>();
			player.ApplySyncPosition(model21.pos, model21.dir);
			player.ActCarry(model21.type, model21.pointId);
			break;
		}
		case PACKET_TYPE.PLAYER_CARRY_IDLE:
		{
			if (player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCarryIdle model18 = packet.GetModel<Coop_Model_PlayerCarryIdle>();
			player.ApplySyncPosition(model18.pos, model18.dir);
			player.ActCarryIdle();
			break;
		}
		case PACKET_TYPE.PLAYER_CARRY_PUT:
		{
			if (player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCarryPut model15 = packet.GetModel<Coop_Model_PlayerCarryPut>();
			player.ApplySyncPosition(model15.pos, model15.dir);
			player.ActCarryPut(model15.pointId);
			break;
		}
		case PACKET_TYPE.PLAYER_ORACLE_HORIZONTAL_NEXT_MOTION:
		{
			if (player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerOracleHorizontalNextMotion model12 = packet.GetModel<Coop_Model_PlayerOracleHorizontalNextMotion>();
			player.thsCtrl.oracleCtrl.SetHorizontalNextMotion(model12.isFinish);
			player.ApplySyncPosition(model12.pos, model12.dir);
			break;
		}
		case PACKET_TYPE.PLAYER_TELEPORT_AVOID:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerTeleportAvoid model9 = packet.GetModel<Coop_Model_PlayerTeleportAvoid>();
			player.ApplySyncPosition(model9.pos, model9.dir);
			player.ActTeleportAvoid();
			break;
		}
		case PACKET_TYPE.PLAYER_QUEST_GIMMICK:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerQuestGimmick model6 = packet.GetModel<Coop_Model_PlayerQuestGimmick>();
			player.ApplySyncPosition(model6.pos, model6.dir);
			player.ActQuestGimmick(model6.gimmickId);
			player.SetActionPosition(model6.act_pos, model6.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_ORACLE_SPEAR_STOCK:
			if (base.character.isDead)
			{
				return true;
			}
			player.spearCtrl.OnUpdateOracleStock();
			break;
		case PACKET_TYPE.PLAYER_RUSH_AVOID:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRushAvoid model = packet.GetModel<Coop_Model_PlayerRushAvoid>();
			player.ApplySyncPosition(model.pos, model.dir);
			player.ActRushAvoid(model.inputVec);
			break;
		}
		default:
			return base.HandleCoopEvent(packet);
		}
		if (QuestManager.IsValidInGameExplore())
		{
			CoopClient coopClient2 = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
			if (coopClient2 != null)
			{
				MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(coopClient2);
			}
		}
		return true;
	}
}
