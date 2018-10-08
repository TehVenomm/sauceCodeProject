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
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_093b: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1033: Unknown result type (might be due to invalid IL or missing references)
		//IL_1110: Unknown result type (might be due to invalid IL or missing references)
		//IL_1141: Unknown result type (might be due to invalid IL or missing references)
		//IL_1146: Unknown result type (might be due to invalid IL or missing references)
		//IL_114b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1150: Unknown result type (might be due to invalid IL or missing references)
		//IL_1162: Unknown result type (might be due to invalid IL or missing references)
		//IL_119a: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1261: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1313: Unknown result type (might be due to invalid IL or missing references)
		//IL_132c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1483: Unknown result type (might be due to invalid IL or missing references)
		//IL_14af: Unknown result type (might be due to invalid IL or missing references)
		//IL_1518: Unknown result type (might be due to invalid IL or missing references)
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
			Coop_Model_PlayerInitialize model50 = packet.GetModel<Coop_Model_PlayerInitialize>();
			player.ApplySyncPosition(model50.pos, model50.dir, true);
			player.hp = model50.hp;
			player.healHp = model50.healHp;
			player.StopCounter(model50.stopcounter);
			StageObject target = null;
			if (model50.target_id >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model50.target_id);
			}
			player.SetActionTarget(target, true);
			player.buffParam.SetSyncParam(model50.buff_sync_param, true);
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncOwnerData(model50.id);
			}
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(player);
			player.get_gameObject().SetActive(true);
			SetFilterMode(FILTER_MODE.NONE);
			player.isCoopInitialized = true;
			player.SetAppearPos(player._position);
			bool flag = false;
			if (player.weaponData == null != (model50.weapon_item == null))
			{
				flag = true;
			}
			else if (player.weaponData != null && player.weaponData.eId != model50.weapon_item.eId)
			{
				flag = true;
			}
			else if (player.weaponIndex != model50.weapon_index)
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
				player.LoadWeapon(model50.weapon_item, model50.weapon_index, delegate
				{
					player.ActBattleStart(true);
				});
			}
			else
			{
				player.SetNowWeapon(model50.weapon_item, model50.weapon_index);
				player.InitParameter();
				if (player.hp <= 0)
				{
					player.ActBattleStart(true);
					if (!player.isDead)
					{
						player.ActDeadLoop(false, 0f, 0f);
					}
				}
				else if (model50.act_battle_start)
				{
					player.ActBattleStart(false);
				}
				else
				{
					player.ActBattleStart(true);
					player.ActIdle(false, -1f);
				}
			}
			player.SetSyncUsingCannon(model50.cannonId);
			player.bulletIndex = model50.bulletIndex;
			break;
		}
		case PACKET_TYPE.PLAYER_ATTACK_COMBO:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerAttackCombo model28 = packet.GetModel<Coop_Model_PlayerAttackCombo>();
			base.owner._position = model28.pos;
			base.owner._rotation = Quaternion.AngleAxis(model28.dir, Vector3.get_up());
			player.ActAttack(_motionLayerName: model28.motionLayerName, id: model28.attack_id, send_packet: true, sync_immediately: false);
			player.SetActionPosition(model28.act_pos, model28.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeRelease model32 = packet.GetModel<Coop_Model_PlayerChargeRelease>();
			player.ApplySyncExRush(model32.isExRushCharge);
			player.ApplySyncPosition(model32.pos, model32.dir, false);
			player.SetChargeRelease(model32.charge_rate);
			player.SetLerpRotation(Quaternion.AngleAxis(model32.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			player.SetActionPosition(model32.act_pos, model32.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_RESTRAINT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRestraint model15 = packet.GetModel<Coop_Model_PlayerRestraint>();
			RestraintInfo restraintInfo = new RestraintInfo();
			restraintInfo.enable = true;
			restraintInfo.duration = model15.duration;
			restraintInfo.damageInterval = model15.damageInterval;
			restraintInfo.damageRate = model15.damageRate;
			restraintInfo.reduceTimeByFlick = model15.reduceTimeByFlick;
			restraintInfo.effectName = model15.effectName;
			restraintInfo.isStopMotion = model15.isStopMotion;
			restraintInfo.isDisableRemoveByPlayerAttack = model15.isDisableRemoveByPlayerAttack;
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
			Coop_Model_PlayerAvoid model17 = packet.GetModel<Coop_Model_PlayerAvoid>();
			player.ApplySyncPosition(model17.pos, model17.dir, false);
			player.ActAvoid();
			break;
		}
		case PACKET_TYPE.PLAYER_WARP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerWarp model42 = packet.GetModel<Coop_Model_PlayerWarp>();
			player.ApplySyncPosition(model42.pos, model42.dir, false);
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
			Coop_Model_PlayerDeadCount model23 = packet.GetModel<Coop_Model_PlayerDeadCount>();
			player.DeadCount(model23.remaind_time, model23.stop, model23.requested);
			break;
		}
		case PACKET_TYPE.PLAYER_DEAD_COUNT_REQUEST:
		{
			Coop_Model_PlayerDeadCountRequest model8 = packet.GetModel<Coop_Model_PlayerDeadCountRequest>();
			if (player.rescueTime > 0f && !player.IsPrayed())
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnDeadCountRequest(packet.fromClientId, player);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_DEAD_STANDUP:
		{
			Coop_Model_PlayerDeadStandup model54 = packet.GetModel<Coop_Model_PlayerDeadStandup>();
			player.ActDeadStandup(model54.standupHp, model54.cType);
			break;
		}
		case PACKET_TYPE.PLAYER_STOP_COUNTER:
		{
			Coop_Model_PlayerStopCounter model51 = packet.GetModel<Coop_Model_PlayerStopCounter>();
			player.StopCounter(model51.stop);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGather model30 = packet.GetModel<Coop_Model_PlayerGather>();
			GatherPointObject gatherPointObject = null;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.gatherPointList != null)
			{
				int i = 0;
				for (int count = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList.Count; i < count; i++)
				{
					if (model30.point_id == (int)MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i].pointData.pointID)
					{
						gatherPointObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i];
						break;
					}
				}
			}
			if (gatherPointObject != null)
			{
				player.ApplySyncPosition(model30.pos, model30.dir, false);
				player.ActGather(gatherPointObject);
				player.SetActionPosition(model30.act_pos, model30.act_pos_f);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_SKILL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSkillAction model21 = packet.GetModel<Coop_Model_PlayerSkillAction>();
			player.ApplySyncPosition(model21.pos, model21.dir, false);
			player.ActSkillAction(model21.skill_index, model21.isUsingSecondGrade);
			player.SetActionPosition(model21.act_pos, model21.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_HEAL:
		{
			Coop_Model_PlayerGetHeal model20 = packet.GetModel<Coop_Model_PlayerGetHeal>();
			player.ExecHealHp(model20.Deserialize(), !model20.receive);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialAction model9 = packet.GetModel<Coop_Model_PlayerSpecialAction>();
			player.ApplySyncPosition(model9.pos, model9.dir, false);
			player.ActSpecialAction(model9.start_effect, model9.isSuccess);
			player.SetActionPosition(model9.act_pos, model9.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialActionContinue model53 = packet.GetModel<Coop_Model_PlayerSpecialActionContinue>();
			player.ApplySyncPosition(model53.pos, model53.dir, false);
			player.ActSpAttackContinue();
			player.SetActionPosition(model53.act_pos, model53.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotArrow model48 = packet.GetModel<Coop_Model_PlayerShotArrow>();
			player.ApplySyncPosition(model48.pos, model48.dir, false);
			AttackInfo attack_info = player.FindAttackInfoExternal(model48.attack_name, true, model48.attack_rate);
			player.ShotArrow(model48.shot_pos, model48.shot_rot, attack_info, model48.is_sit_shot, model48.is_aim_end);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotSoulArrow model41 = packet.GetModel<Coop_Model_PlayerShotSoulArrow>();
			player.ApplySyncPosition(model41.pos, model41.dir, false);
			player.ShotSoulArrowPuppet(model41.shotPos, model41.bowRot, model41.targetPosList);
			break;
		}
		case PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO:
		{
			Coop_Model_PlayerUpdateSkillInfo model39 = packet.GetModel<Coop_Model_PlayerUpdateSkillInfo>();
			player.skillInfo.SetSettingsInfo(model39.settings_info, player.equipWeaponList);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_START:
		{
			Coop_Model_PlayerPrayerStart model38 = packet.GetModel<Coop_Model_PlayerPrayerStart>();
			player.OnPrayerStart(model38.sid);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_END:
		{
			Coop_Model_PlayerPrayerEnd model36 = packet.GetModel<Coop_Model_PlayerPrayerEnd>();
			player.OnPrayerEnd(model36.sid);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_BOOST:
		{
			Coop_Model_PlayerPrayerBoost model35 = packet.GetModel<Coop_Model_PlayerPrayerBoost>();
			player.OnChangeBoostPray(model35.sid, model35.boostPrayInfo);
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
			Coop_Model_PlayerApplyChangeWeapon model26 = packet.GetModel<Coop_Model_PlayerApplyChangeWeapon>();
			if (player.weaponData.eId == (uint)model26.item.eId && player.weaponIndex == model26.index)
			{
				return true;
			}
			player.ApplyChangeWeapon(model26.item, model26.index);
			break;
		}
		case PACKET_TYPE.PLAYER_SETSTATUS:
		{
			Coop_Model_PlayerSetStatus model24 = packet.GetModel<Coop_Model_PlayerSetStatus>();
			player.OnSetPlayerStatus(model24.level, model24.atk, model24.def, model24.hp, true, null);
			if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.LEVEL_UP, player);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_GET_RAREDROP:
			if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid())
			{
				Coop_Model_PlayerGetRareDrop model18 = packet.GetModel<Coop_Model_PlayerGetRareDrop>();
				string text = null;
				switch (model18.type)
				{
				case 5:
				{
					SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)model18.item_id);
					if (skillItemData != null)
					{
						text = skillItemData.name;
					}
					break;
				}
				case 4:
				{
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)model18.item_id);
					if (equipItemData != null)
					{
						text = equipItemData.name;
					}
					break;
				}
				case 14:
				{
					AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint)model18.item_id);
					if (data != null)
					{
						text = data.name;
					}
					break;
				}
				default:
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint)model18.item_id);
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
			Coop_Model_PlayerGrabbed model14 = packet.GetModel<Coop_Model_PlayerGrabbed>();
			GrabInfo grabInfo = new GrabInfo();
			grabInfo.parentNode = model14.nodeName;
			grabInfo.duration = model14.duration;
			grabInfo.drainAttackId = model14.drainAtkId;
			player.ActGrabbedStart(model14.enemyId, grabInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_GRABBED_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGrabbedEnd model6 = packet.GetModel<Coop_Model_PlayerGrabbedEnd>();
			player.ActGrabbedEnd(model6.angle, model6.power);
			break;
		}
		case PACKET_TYPE.PLAYER_SET_PRESENT_BULLET:
		{
			Coop_Model_PlayerSetPresentBullet model5 = packet.GetModel<Coop_Model_PlayerSetPresentBullet>();
			player.SetPresentBullet(model5.presentBulletId, (BulletData.BulletPresent.TYPE)model5.type, model5.position, model5.bulletName);
			break;
		}
		case PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET:
		{
			Coop_Model_PlayerPickPresentBullet model3 = packet.GetModel<Coop_Model_PlayerPickPresentBullet>();
			player.DestroyPresentBulletObject(model3.presentBulletId);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET:
		{
			Coop_Model_PlayerShotZoneBullet model2 = packet.GetModel<Coop_Model_PlayerShotZoneBullet>();
			player.ShotZoneBullet(player, model2.bulletName, model2.position, false, false);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET:
		{
			Coop_Model_PlayerShotDecoyBullet model56 = packet.GetModel<Coop_Model_PlayerShotDecoyBullet>();
			player.ShotDecoyBullet(model56.id, model56.skIndex, model56.decoyId, model56.bulletName, model56.position, false);
			break;
		}
		case PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET:
		{
			Coop_Model_PlayerExplodeDecoyBullet model55 = packet.GetModel<Coop_Model_PlayerExplodeDecoyBullet>();
			player.ExplodeDecoyBullet(model55.decoyId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_STANDBY:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonStandby model52 = packet.GetModel<Coop_Model_PlayerCannonStandby>();
			player.ApplySyncPosition(model52.pos, model52.dir, false);
			player.ActCannonStandby(model52.cannonId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_SHOT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonShot model49 = packet.GetModel<Coop_Model_PlayerCannonShot>();
			player.ApplySyncPosition(model49.pos, model49.dir, false);
			player.SetCannonState(Player.CANNON_STATE.READY);
			player.ApplyCannonVector(model49.cannonVec);
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
		{
			Coop_Model_PlayerResurrect model45 = packet.GetModel<Coop_Model_PlayerResurrect>();
			player.OnResurrection(true);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_RESURRECT:
		{
			Coop_Model_PlayerGetResurrect model44 = packet.GetModel<Coop_Model_PlayerGetResurrect>();
			player.OnGetResurrection();
			break;
		}
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
			Coop_Model_PlayerChargeExpandRelease model40 = packet.GetModel<Coop_Model_PlayerChargeExpandRelease>();
			player.ApplySyncPosition(model40.pos, model40.dir, false);
			player.SetChargeExpandRelease(model40.charge_rate);
			player.SetLerpRotation(Quaternion.AngleAxis(model40.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			player.SetActionPosition(model40.act_pos, model40.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_RIZE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpRize model37 = packet.GetModel<Coop_Model_PlayerJumpRize>();
			player.OnJumpRize(model37.dir, model37.level);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpEnd model34 = packet.GetModel<Coop_Model_PlayerJumpEnd>();
			player.OnJumpEnd(model34.pos, model34.isSuccess, model34.y);
			break;
		}
		case PACKET_TYPE.PLAYER_SOUL_BOOST:
		{
			Coop_Model_PlayerSoulBoost model33 = packet.GetModel<Coop_Model_PlayerSoulBoost>();
			player.OnSoulBoost(model33.isBoost);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveActionSync model31 = packet.GetModel<Coop_Model_PlayerEvolveActionSync>();
			player.OnSyncEvolveAction(model31.isAction);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveSpecialAction model29 = packet.GetModel<Coop_Model_PlayerEvolveSpecialAction>();
			player.ApplySyncPosition(model29.pos, model29.dir, false);
			player.ActEvolveSpecialAction();
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_POS:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchPos model27 = packet.GetModel<Coop_Model_PlayerSnatchPos>();
			player.snatchCtrl.OnHit(model27.enemyId, model27.hitPoint);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_START:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveStart model25 = packet.GetModel<Coop_Model_PlayerSnatchMoveStart>();
			player.OnSnatchMoveStart(model25.snatchPos);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveEnd model22 = packet.GetModel<Coop_Model_PlayerSnatchMoveEnd>();
			player.SetActionPosition(model22.act_pos, model22.act_pos_f);
			player.ApplySyncPosition(model22.pos, model22.dir, false);
			player.OnSnatchMoveEnd(model22.triggerIndex);
			break;
		}
		case PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerPairSwordsLaserEnd model19 = packet.GetModel<Coop_Model_PlayerPairSwordsLaserEnd>();
			player.OnSyncSpecialActionGauge(model19.weaponIndex, model19.currentSpActionGauge);
			player.pairSwordsCtrl.OnLaserEnd(true);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotHealingHoming model16 = packet.GetModel<Coop_Model_PlayerShotHealingHoming>();
			player.OnShotHealingHoming(model16);
			break;
		}
		case PACKET_TYPE.PLAYER_SACRIFICED_HP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSacrificedHp model13 = packet.GetModel<Coop_Model_PlayerSacrificedHp>();
			player.spearCtrl.SacrificedHp(model13.sacrificedHp, true);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP_CREATE:
		{
			Coop_Model_WaveMatchDropCreate model12 = packet.GetModel<Coop_Model_WaveMatchDropCreate>();
			MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropCreate(model12);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP_PICKED:
		{
			Coop_Model_WaveMatchDropPicked model11 = packet.GetModel<Coop_Model_WaveMatchDropPicked>();
			MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropPicked(model11);
			break;
		}
		case PACKET_TYPE.GATHER_GIMMICK_INFO:
		{
			Coop_Model_GatherGimmickInfo model10 = packet.GetModel<Coop_Model_GatherGimmickInfo>();
			MonoBehaviourSingleton<InGameProgress>.I.UpdatGatherGimmickInfo(model10.managedId, model10.ownerId, model10.isUsed);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER_GIMMICK:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGatherGimmick model7 = packet.GetModel<Coop_Model_PlayerGatherGimmick>();
			player.ApplySyncPosition(model7.pos, model7.dir, false);
			player.ActGatherGimmick(model7.gimmickId);
			player.SetActionPosition(model7.act_pos, model7.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER_GIMMICK_STATE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGatherGimmickState model4 = packet.GetModel<Coop_Model_PlayerGatherGimmickState>();
			player.OnGatherGimmickState(model4.state);
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_POSITION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncPosition model = packet.GetModel<Coop_Model_PlayerSyncPosition>();
			player.ApplySyncPosition(model.pos, model.dir, false);
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
