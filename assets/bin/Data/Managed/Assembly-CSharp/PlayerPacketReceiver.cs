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
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0835: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0894: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e58: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f56: Unknown result type (might be due to invalid IL or missing references)
		//IL_102c: Unknown result type (might be due to invalid IL or missing references)
		//IL_105d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1062: Unknown result type (might be due to invalid IL or missing references)
		//IL_1067: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Unknown result type (might be due to invalid IL or missing references)
		//IL_107e: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_117d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1248: Unknown result type (might be due to invalid IL or missing references)
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
			Coop_Model_PlayerInitialize model32 = packet.GetModel<Coop_Model_PlayerInitialize>();
			player.ApplySyncPosition(model32.pos, model32.dir, true);
			player.hp = model32.hp;
			player.healHp = model32.healHp;
			player.StopCounter(model32.stopcounter);
			StageObject target = null;
			if (model32.target_id >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model32.target_id);
			}
			player.SetActionTarget(target, true);
			player.buffParam.SetSyncParam(model32.buff_sync_param, true);
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncOwnerData(model32.id);
			}
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player);
			player.get_gameObject().SetActive(true);
			SetFilterMode(FILTER_MODE.NONE);
			player.isCoopInitialized = true;
			player.SetAppearPos(player._position);
			bool flag = false;
			if (player.weaponData == null != (model32.weapon_item == null))
			{
				flag = true;
			}
			else if (player.weaponData != null && player.weaponData.eId != model32.weapon_item.eId)
			{
				flag = true;
			}
			else if (player.weaponIndex != model32.weapon_index)
			{
				flag = true;
			}
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
			if (coopClient != null && !coopClient.IsBattleStart())
			{
				player.WaitBattleStart();
				player.get_gameObject().SetActive(false);
				MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject(player);
			}
			else if (flag)
			{
				player.LoadWeapon(model32.weapon_item, model32.weapon_index, delegate
				{
					player.ActBattleStart(true);
				});
			}
			else
			{
				player.SetNowWeapon(model32.weapon_item, model32.weapon_index);
				player.InitParameter();
				if (player.hp <= 0)
				{
					player.ActBattleStart(true);
					if (!player.isDead)
					{
						player.ActDeadLoop(false, 0f, 0f);
					}
				}
				else if (model32.act_battle_start)
				{
					player.ActBattleStart(false);
				}
				else
				{
					player.ActBattleStart(true);
					player.ActIdle(false, -1f);
				}
			}
			player.SetSyncUsingCannon(model32.cannonId);
			break;
		}
		case PACKET_TYPE.PLAYER_ATTACK_COMBO:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerAttackCombo model15 = packet.GetModel<Coop_Model_PlayerAttackCombo>();
			base.owner._position = model15.pos;
			base.owner._rotation = Quaternion.AngleAxis(model15.dir, Vector3.get_up());
			player.ActAttack(model15.attack_id, true, false);
			player.SetActionPosition(model15.act_pos, model15.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeRelease model36 = packet.GetModel<Coop_Model_PlayerChargeRelease>();
			player.ApplySyncExRush(model36.isExRushCharge);
			player.ApplySyncPosition(model36.pos, model36.dir, false);
			player.SetChargeRelease(model36.charge_rate);
			player.SetLerpRotation(Quaternion.AngleAxis(model36.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			player.SetActionPosition(model36.act_pos, model36.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_RESTRAINT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRestraint model33 = packet.GetModel<Coop_Model_PlayerRestraint>();
			RestraintInfo restraintInfo = new RestraintInfo();
			restraintInfo.enable = true;
			restraintInfo.duration = model33.duration;
			restraintInfo.damageInterval = model33.damageInterval;
			restraintInfo.damageRate = model33.damageRate;
			restraintInfo.reduceTimeByFlick = model33.reduceTimeByFlick;
			restraintInfo.effectName = model33.effectName;
			restraintInfo.isStopMotion = model33.isStopMotion;
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
			Coop_Model_PlayerAvoid model38 = packet.GetModel<Coop_Model_PlayerAvoid>();
			player.ApplySyncPosition(model38.pos, model38.dir, false);
			player.ActAvoid();
			break;
		}
		case PACKET_TYPE.PLAYER_WARP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerWarp model19 = packet.GetModel<Coop_Model_PlayerWarp>();
			player.ApplySyncPosition(model19.pos, model19.dir, false);
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
			Coop_Model_PlayerDeadCount model3 = packet.GetModel<Coop_Model_PlayerDeadCount>();
			player.DeadCount(model3.remaind_time, model3.stop);
			break;
		}
		case PACKET_TYPE.PLAYER_DEAD_STANDUP:
		{
			Coop_Model_PlayerDeadStandup model47 = packet.GetModel<Coop_Model_PlayerDeadStandup>();
			player.ActDeadStandup(model47.standupHp, model47.cType);
			break;
		}
		case PACKET_TYPE.PLAYER_STOP_COUNTER:
		{
			Coop_Model_PlayerStopCounter model46 = packet.GetModel<Coop_Model_PlayerStopCounter>();
			player.StopCounter(model46.stop);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGather model13 = packet.GetModel<Coop_Model_PlayerGather>();
			GatherPointObject gatherPointObject = null;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.gatherPointList != null)
			{
				int i = 0;
				for (int count = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList.Count; i < count; i++)
				{
					if (model13.point_id == (int)MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i].pointData.pointID)
					{
						gatherPointObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i];
						break;
					}
				}
			}
			if (gatherPointObject != null)
			{
				player.ApplySyncPosition(model13.pos, model13.dir, false);
				player.ActGather(gatherPointObject);
				player.SetActionPosition(model13.act_pos, model13.act_pos_f);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_SKILL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSkillAction model8 = packet.GetModel<Coop_Model_PlayerSkillAction>();
			player.ApplySyncPosition(model8.pos, model8.dir, false);
			player.ActSkillAction(model8.skill_index);
			player.SetActionPosition(model8.act_pos, model8.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_HEAL:
		{
			Coop_Model_PlayerGetHeal model7 = packet.GetModel<Coop_Model_PlayerGetHeal>();
			player.OnGetHeal(model7.heal_hp, (HEAL_TYPE)model7.heal_type, !model7.receive, (HEAL_EFFECT_TYPE)model7.effect_type, true);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialAction model2 = packet.GetModel<Coop_Model_PlayerSpecialAction>();
			player.ApplySyncPosition(model2.pos, model2.dir, false);
			player.ActSpecialAction(model2.start_effect, model2.isSuccess);
			player.SetActionPosition(model2.act_pos, model2.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialActionContinue model39 = packet.GetModel<Coop_Model_PlayerSpecialActionContinue>();
			player.ApplySyncPosition(model39.pos, model39.dir, false);
			player.ActSpAttackContinue();
			player.SetActionPosition(model39.act_pos, model39.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotArrow model35 = packet.GetModel<Coop_Model_PlayerShotArrow>();
			player.ApplySyncPosition(model35.pos, model35.dir, false);
			AttackInfo attack_info = player.FindAttackInfoExternal(model35.attack_name, true, model35.attack_rate);
			player.ShotArrow(model35.shot_pos, model35.shot_rot, attack_info, model35.is_sit_shot, model35.is_aim_end);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotSoulArrow model26 = packet.GetModel<Coop_Model_PlayerShotSoulArrow>();
			player.ApplySyncPosition(model26.pos, model26.dir, false);
			player.ShotSoulArrowPuppet(model26.shotPos, model26.bowRot, model26.targetPosList);
			break;
		}
		case PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO:
		{
			Coop_Model_PlayerUpdateSkillInfo model25 = packet.GetModel<Coop_Model_PlayerUpdateSkillInfo>();
			player.skillInfo.SetSettingsInfo(model25.settings_info, player.equipWeaponList);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_START:
		{
			Coop_Model_PlayerPrayerStart model23 = packet.GetModel<Coop_Model_PlayerPrayerStart>();
			player.OnPrayerStart(model23.sid);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_END:
		{
			Coop_Model_PlayerPrayerEnd model22 = packet.GetModel<Coop_Model_PlayerPrayerEnd>();
			player.OnPrayerEnd(model22.sid);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_BOOST:
		{
			Coop_Model_PlayerPrayerBoost model20 = packet.GetModel<Coop_Model_PlayerPrayerBoost>();
			player.OnPrayerBoost(model20.sid, model20.isBoost);
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
			Coop_Model_PlayerApplyChangeWeapon model12 = packet.GetModel<Coop_Model_PlayerApplyChangeWeapon>();
			if (player.weaponData.eId == (uint)model12.item.eId && player.weaponIndex == model12.index)
			{
				return true;
			}
			player.ApplyChangeWeapon(model12.item, model12.index);
			break;
		}
		case PACKET_TYPE.PLAYER_SETSTATUS:
		{
			Coop_Model_PlayerSetStatus model10 = packet.GetModel<Coop_Model_PlayerSetStatus>();
			player.OnSetPlayerStatus(model10.level, model10.atk, model10.def, model10.hp, true, null);
			if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.LEVEL_UP, player);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_GET_RAREDROP:
			if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid())
			{
				Coop_Model_PlayerGetRareDrop model5 = packet.GetModel<Coop_Model_PlayerGetRareDrop>();
				string text = null;
				switch (model5.type)
				{
				case 5:
				{
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)model5.item_id);
					if (skillItemData != null)
					{
						text = skillItemData.name;
					}
					break;
				}
				case 4:
				{
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)model5.item_id);
					if (equipItemData != null)
					{
						text = equipItemData.name;
					}
					break;
				}
				default:
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)model5.item_id);
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
			}
			break;
		case PACKET_TYPE.PLAYER_GRABBED:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGrabbed model48 = packet.GetModel<Coop_Model_PlayerGrabbed>();
			GrabInfo grabInfo = new GrabInfo();
			grabInfo.parentNode = model48.nodeName;
			grabInfo.duration = model48.duration;
			grabInfo.drainAttackId = model48.drainAtkId;
			player.ActGrabbedStart(model48.enemyId, grabInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_GRABBED_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGrabbedEnd model45 = packet.GetModel<Coop_Model_PlayerGrabbedEnd>();
			player.ActGrabbedEnd(model45.angle, model45.power);
			break;
		}
		case PACKET_TYPE.PLAYER_SET_PRESENT_BULLET:
		{
			Coop_Model_PlayerSetPresentBullet model44 = packet.GetModel<Coop_Model_PlayerSetPresentBullet>();
			player.SetPresentBullet(model44.presentBulletId, (BulletData.BulletPresent.TYPE)model44.type, model44.position, model44.bulletName);
			break;
		}
		case PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET:
		{
			Coop_Model_PlayerPickPresentBullet model43 = packet.GetModel<Coop_Model_PlayerPickPresentBullet>();
			player.DestroyPresentBulletObject(model43.presentBulletId);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET:
		{
			Coop_Model_PlayerShotZoneBullet model42 = packet.GetModel<Coop_Model_PlayerShotZoneBullet>();
			player.ShotZoneBullet(player, model42.bulletName, model42.position, false, false);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET:
		{
			Coop_Model_PlayerShotDecoyBullet model41 = packet.GetModel<Coop_Model_PlayerShotDecoyBullet>();
			player.ShotDecoyBullet(model41.id, model41.decoyId, model41.bulletName, model41.position, false);
			break;
		}
		case PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET:
		{
			Coop_Model_PlayerExplodeDecoyBullet model40 = packet.GetModel<Coop_Model_PlayerExplodeDecoyBullet>();
			player.ExplodeDecoyBullet(model40.decoyId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_STANDBY:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonStandby model37 = packet.GetModel<Coop_Model_PlayerCannonStandby>();
			player.ApplySyncPosition(model37.pos, model37.dir, false);
			player.ActCannonStandby(model37.cannonId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_SHOT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonShot model34 = packet.GetModel<Coop_Model_PlayerCannonShot>();
			player.ApplySyncPosition(model34.pos, model34.dir, false);
			player.SetCannonState(Player.CANNON_STATE.READY);
			player.ApplyCannonVector(model34.cannonVec);
			player.ActCannonShot();
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_ROTATE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonRotate model31 = packet.GetModel<Coop_Model_PlayerCannonRotate>();
			player.SetSyncCannonRotation(model31.cannonVec);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE:
		{
			Coop_Model_PlayerGetChargeSkillGauge model30 = packet.GetModel<Coop_Model_PlayerGetChargeSkillGauge>();
			player.OnGetChargeSkillGauge((BuffParam.BUFFTYPE)model30.buffType, model30.buffValue, model30.useSkillIndex, !model30.receive);
			break;
		}
		case PACKET_TYPE.PLAYER_RESURRECT:
		{
			Coop_Model_PlayerResurrect model29 = packet.GetModel<Coop_Model_PlayerResurrect>();
			player.OnResurrection(true);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_RESURRECT:
		{
			Coop_Model_PlayerGetResurrect model28 = packet.GetModel<Coop_Model_PlayerGetResurrect>();
			player.OnGetResurrection();
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC:
		{
			Coop_Model_PlayerSpecialActionGaugeSync model27 = packet.GetModel<Coop_Model_PlayerSpecialActionGaugeSync>();
			player.OnSyncSpecialActionGauge(model27.weaponIndex, model27.currentSpActionGauge);
			player.pairSwordsCtrl.SetComboLv(model27.comboLv);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_EXPAND_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeExpandRelease model24 = packet.GetModel<Coop_Model_PlayerChargeExpandRelease>();
			player.ApplySyncPosition(model24.pos, model24.dir, false);
			player.SetChargeExpandRelease(model24.charge_rate);
			player.SetLerpRotation(Quaternion.AngleAxis(model24.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			player.SetActionPosition(model24.act_pos, model24.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_RIZE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpRize model21 = packet.GetModel<Coop_Model_PlayerJumpRize>();
			player.OnJumpRize(model21.dir, model21.level);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpEnd model18 = packet.GetModel<Coop_Model_PlayerJumpEnd>();
			player.OnJumpEnd(model18.pos, model18.isSuccess, model18.y);
			break;
		}
		case PACKET_TYPE.PLAYER_SOUL_BOOST:
		{
			Coop_Model_PlayerSoulBoost model17 = packet.GetModel<Coop_Model_PlayerSoulBoost>();
			player.OnSoulBoost(model17.isBoost);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveActionSync model16 = packet.GetModel<Coop_Model_PlayerEvolveActionSync>();
			player.OnSyncEvolveAction(model16.isAction);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveSpecialAction model14 = packet.GetModel<Coop_Model_PlayerEvolveSpecialAction>();
			player.ApplySyncPosition(model14.pos, model14.dir, false);
			player.ActEvolveSpecialAction();
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_POS:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchPos model11 = packet.GetModel<Coop_Model_PlayerSnatchPos>();
			player.snatchCtrl.OnHit(model11.enemyId, model11.hitPoint);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_START:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveStart model9 = packet.GetModel<Coop_Model_PlayerSnatchMoveStart>();
			player.OnSnatchMoveStart(model9.snatchPos);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveEnd model6 = packet.GetModel<Coop_Model_PlayerSnatchMoveEnd>();
			player.SetActionPosition(model6.act_pos, model6.act_pos_f);
			player.ApplySyncPosition(model6.pos, model6.dir, false);
			player.OnSnatchMoveEnd(model6.triggerIndex);
			break;
		}
		case PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerPairSwordsLaserEnd model4 = packet.GetModel<Coop_Model_PlayerPairSwordsLaserEnd>();
			player.OnSyncSpecialActionGauge(model4.weaponIndex, model4.currentSpActionGauge);
			player.pairSwordsCtrl.OnLaserEnd(true);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotHealingHoming model = packet.GetModel<Coop_Model_PlayerShotHealingHoming>();
			player.OnShotHealingHoming(model);
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
