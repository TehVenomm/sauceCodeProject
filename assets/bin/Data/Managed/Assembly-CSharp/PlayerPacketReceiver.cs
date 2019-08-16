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
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c89: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_109d: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_112c: Unknown result type (might be due to invalid IL or missing references)
		//IL_117d: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_122a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1307: Unknown result type (might be due to invalid IL or missing references)
		//IL_1338: Unknown result type (might be due to invalid IL or missing references)
		//IL_133d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1342: Unknown result type (might be due to invalid IL or missing references)
		//IL_1347: Unknown result type (might be due to invalid IL or missing references)
		//IL_1359: Unknown result type (might be due to invalid IL or missing references)
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1458: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_150a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1523: Unknown result type (might be due to invalid IL or missing references)
		//IL_167a: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_170f: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1813: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aec: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c14: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c40: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cb9: Unknown result type (might be due to invalid IL or missing references)
		switch (packet.packetType)
		{
		case PACKET_TYPE.PLAYER_LOAD_COMPLETE:
			if (!this.player.isSetAppearPos)
			{
				return false;
			}
			if (this.player.playerSender != null)
			{
				this.player.playerSender.OnRecvLoadComplete(packet.fromClientId);
			}
			break;
		case PACKET_TYPE.PLAYER_INITIALIZE:
		{
			if (this.player.isLoading)
			{
				return false;
			}
			Coop_Model_PlayerInitialize model4 = packet.GetModel<Coop_Model_PlayerInitialize>();
			this.player.ApplySyncPosition(model4.pos, model4.dir, force_sync: true);
			this.player.hp = model4.hp;
			this.player.healHp = model4.healHp;
			this.player.StopCounter(model4.stopcounter);
			StageObject target = null;
			if (model4.target_id >= 0)
			{
				target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model4.target_id);
			}
			this.player.SetActionTarget(target);
			this.player.buffParam.SetSyncParam(model4.buff_sync_param);
			if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
			{
				MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncOwnerData(model4.id);
			}
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject(this.player);
			this.player.get_gameObject().SetActive(true);
			SetFilterMode(FILTER_MODE.NONE);
			this.player.isCoopInitialized = true;
			this.player.SetAppearPos(this.player._position);
			bool flag = false;
			if (this.player.weaponData == null != (model4.weapon_item == null))
			{
				flag = true;
			}
			else if (this.player.weaponData != null && this.player.weaponData.eId != model4.weapon_item.eId)
			{
				flag = true;
			}
			else if (this.player.weaponIndex != model4.weapon_index)
			{
				flag = true;
			}
			CoopClient coopClient = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
			if (coopClient != null && !coopClient.IsBattleStart())
			{
				this.player.WaitBattleStart();
				this.player.get_gameObject().SetActive(false);
				MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject(this.player);
			}
			else if (flag)
			{
				this.player.LoadWeapon(model4.weapon_item, model4.weapon_index, delegate
				{
					this.player.ActBattleStart(effect_only: true);
				});
			}
			else
			{
				this.player.SetNowWeapon(model4.weapon_item, model4.weapon_index, this.player.uniqueEquipmentIndex);
				this.player.InitParameter();
				if (this.player.hp <= 0)
				{
					this.player.ActBattleStart(effect_only: true);
					if (!this.player.isDead)
					{
						this.player.ActDeadLoop();
					}
				}
				else if (this.player.fishingCtrl != null && model4.fishingState > 0 && model4.gatherGimmickId > 0)
				{
					this.player.ActBattleStart(effect_only: true);
					FishingController.eState fishingState = (FishingController.eState)model4.fishingState;
					if (fishingState == FishingController.eState.Coop)
					{
						this.player.ActCoopFishingStart(model4.gatherGimmickId);
						this.player.SetLerpRotation(Vector3.get_zero());
					}
					else
					{
						this.player.ActGatherGimmick(model4.gatherGimmickId);
						this.player.SetLerpRotation(Vector3.get_zero());
						this.player.fishingCtrl.ChangeState(fishingState);
						if (fishingState == FishingController.eState.Fight)
						{
							this.player.PlayMotion("fishing_send");
							this.player.EventActionRendererON(null);
							SoundManager.PlayLoopSE(this.player.fishingCtrl.GetSeId(2), this.player, this.player.FindNode(string.Empty));
						}
					}
				}
				else if (model4.carryingGimmickId > 0)
				{
					this.player.ActCarry(InGameProgress.eFieldGimmick.CarriableGimmick, model4.carryingGimmickId);
				}
				else if (model4.act_battle_start)
				{
					this.player.ActBattleStart();
				}
				else
				{
					this.player.ActBattleStart(effect_only: true);
					this.player.ActIdle();
				}
			}
			this.player.SetSyncUsingCannon(model4.cannonId);
			this.player.bulletIndex = model4.bulletIndex;
			break;
		}
		case PACKET_TYPE.PLAYER_ATTACK_COMBO:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerAttackCombo model53 = packet.GetModel<Coop_Model_PlayerAttackCombo>();
			base.owner._position = model53.pos;
			base.owner._rotation = Quaternion.AngleAxis(model53.dir, Vector3.get_up());
			Player player = this.player;
			int attack_id = model53.attack_id;
			string motionLayerName = model53.motionLayerName;
			string motionStateName = model53.motionStateName;
			player.ActAttack(attack_id, send_packet: true, sync_immediately: false, motionLayerName, motionStateName);
			this.player.SetActionPosition(model53.act_pos, model53.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeRelease model44 = packet.GetModel<Coop_Model_PlayerChargeRelease>();
			this.player.ApplySyncExRush(model44.isExRushCharge);
			this.player.ApplySyncPosition(model44.pos, model44.dir);
			this.player.SetChargeRelease(model44.charge_rate);
			this.player.SetLerpRotation(Quaternion.AngleAxis(model44.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			this.player.SetActionPosition(model44.act_pos, model44.act_pos_f);
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
			this.player.ActRestraint(restraintInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_RESTRAINT_END:
			if (base.character.isDead)
			{
				return true;
			}
			this.player.ActRestraintEnd();
			break;
		case PACKET_TYPE.PLAYER_AVOID:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerAvoid model16 = packet.GetModel<Coop_Model_PlayerAvoid>();
			this.player.ApplySyncPosition(model16.pos, model16.dir);
			this.player.ActAvoid();
			break;
		}
		case PACKET_TYPE.PLAYER_WARP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerWarp model57 = packet.GetModel<Coop_Model_PlayerWarp>();
			this.player.ApplySyncPosition(model57.pos, model57.dir);
			this.player.ActWarp();
			break;
		}
		case PACKET_TYPE.PLAYER_BLOW_CLEAR:
			if (base.character.isDead)
			{
				return true;
			}
			this.player.InputBlowClear();
			break;
		case PACKET_TYPE.PLAYER_STUNNED_END:
			if (base.character.isDead)
			{
				return true;
			}
			this.player.SetStunnedEnd();
			break;
		case PACKET_TYPE.PLAYER_DEAD_COUNT:
		{
			Coop_Model_PlayerDeadCount model20 = packet.GetModel<Coop_Model_PlayerDeadCount>();
			this.player.DeadCount(model20.remaind_time, model20.stop, model20.requested);
			break;
		}
		case PACKET_TYPE.PLAYER_DEAD_COUNT_REQUEST:
		{
			Coop_Model_PlayerDeadCountRequest model71 = packet.GetModel<Coop_Model_PlayerDeadCountRequest>();
			if (this.player.rescueTime > 0f && !this.player.IsPrayed() && !this.player.isWaitingResurrectionHoming)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnDeadCountRequest(packet.fromClientId, this.player);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_DEAD_STANDUP:
		{
			Coop_Model_PlayerDeadStandup model61 = packet.GetModel<Coop_Model_PlayerDeadStandup>();
			this.player.ActDeadStandup(model61.standupHp, model61.cType);
			break;
		}
		case PACKET_TYPE.PLAYER_STOP_COUNTER:
		{
			Coop_Model_PlayerStopCounter model58 = packet.GetModel<Coop_Model_PlayerStopCounter>();
			this.player.StopCounter(model58.stop);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGather model28 = packet.GetModel<Coop_Model_PlayerGather>();
			GatherPointObject gatherPointObject = null;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.gatherPointList != null)
			{
				int i = 0;
				for (int count = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList.Count; i < count; i++)
				{
					if (model28.point_id == (int)MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i].pointData.pointID)
					{
						gatherPointObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[i];
						break;
					}
				}
			}
			if (gatherPointObject != null)
			{
				this.player.ApplySyncPosition(model28.pos, model28.dir);
				this.player.ActGather(gatherPointObject);
				this.player.SetActionPosition(model28.act_pos, model28.act_pos_f);
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
			this.player.ApplySyncPosition(model13.pos, model13.dir);
			this.player.ActSkillAction(model13.skill_index, model13.isUsingSecondGrade);
			this.player.SetActionPosition(model13.act_pos, model13.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_HEAL:
		{
			Coop_Model_PlayerGetHeal model11 = packet.GetModel<Coop_Model_PlayerGetHeal>();
			this.player.ExecHealHp(model11.Deserialize(), !model11.receive);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialAction model72 = packet.GetModel<Coop_Model_PlayerSpecialAction>();
			this.player.ApplySyncPosition(model72.pos, model72.dir);
			this.player.ActSpecialAction(model72.start_effect, model72.isSuccess);
			this.player.SetActionPosition(model72.act_pos, model72.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSpecialActionContinue model60 = packet.GetModel<Coop_Model_PlayerSpecialActionContinue>();
			this.player.ApplySyncPosition(model60.pos, model60.dir);
			this.player.ActSpAttackContinue();
			this.player.SetActionPosition(model60.act_pos, model60.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotArrow model54 = packet.GetModel<Coop_Model_PlayerShotArrow>();
			this.player.ApplySyncPosition(model54.pos, model54.dir);
			AttackInfo attack_info = this.player.FindAttackInfoExternal(model54.attack_name, fix_rate: true, model54.attack_rate);
			this.player.ShotArrow(model54.shot_pos, model54.shot_rot, attack_info, model54.is_sit_shot, model54.is_aim_end);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotSoulArrow model42 = packet.GetModel<Coop_Model_PlayerShotSoulArrow>();
			this.player.ApplySyncPosition(model42.pos, model42.dir);
			this.player.ShotSoulArrowPuppet(model42.shotPos, model42.bowRot, model42.targetPosList);
			break;
		}
		case PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO:
		{
			Coop_Model_PlayerUpdateSkillInfo model39 = packet.GetModel<Coop_Model_PlayerUpdateSkillInfo>();
			this.player.skillInfo.SetSettingsInfo(model39.settings_info, this.player.equipWeaponList);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_START:
		{
			Coop_Model_PlayerPrayerStart model38 = packet.GetModel<Coop_Model_PlayerPrayerStart>();
			Player.PrayInfo prayInfo2 = new Player.PrayInfo();
			prayInfo2.targetId = model38.sid;
			prayInfo2.reason = (Player.PRAY_REASON)model38.reason;
			this.player.OnPrayerStart(prayInfo2);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_END:
		{
			Coop_Model_PlayerPrayerEnd model36 = packet.GetModel<Coop_Model_PlayerPrayerEnd>();
			Player.PrayInfo prayInfo = new Player.PrayInfo();
			prayInfo.targetId = model36.sid;
			prayInfo.reason = (Player.PRAY_REASON)model36.reason;
			this.player.OnPrayerEnd(prayInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_PRAYER_BOOST:
		{
			Coop_Model_PlayerPrayerBoost model34 = packet.GetModel<Coop_Model_PlayerPrayerBoost>();
			this.player.OnChangeBoostPray(model34.sid, model34.boostPrayInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_CHANGE_WEAPON:
			if (base.character.isDead)
			{
				return true;
			}
			this.player.ActChangeWeapon(null, -1);
			break;
		case PACKET_TYPE.PLAYER_APPLY_CHANGE_WEAPON:
		{
			Coop_Model_PlayerApplyChangeWeapon model22 = packet.GetModel<Coop_Model_PlayerApplyChangeWeapon>();
			if (this.player.weaponData.eId == (uint)model22.item.eId && this.player.weaponIndex == model22.index)
			{
				return true;
			}
			this.player.ApplyChangeWeapon(model22.item, model22.index);
			break;
		}
		case PACKET_TYPE.PLAYER_SETSTATUS:
		{
			Coop_Model_PlayerSetStatus model17 = packet.GetModel<Coop_Model_PlayerSetStatus>();
			this.player.OnSetPlayerStatus(model17.level, model17.atk, model17.def, model17.hp);
			if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.LEVEL_UP, this.player);
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
				MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(this.player.charaName, StringTable.Format(STRING_CATEGORY.IN_GAME, 4000u, text));
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
			this.player.ActGrabbedStart(model3.enemyId, grabInfo);
			break;
		}
		case PACKET_TYPE.PLAYER_GRABBED_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGrabbedEnd model69 = packet.GetModel<Coop_Model_PlayerGrabbedEnd>();
			this.player.ActGrabbedEnd(model69.angle, model69.power);
			break;
		}
		case PACKET_TYPE.PLAYER_SET_PRESENT_BULLET:
		{
			Coop_Model_PlayerSetPresentBullet model68 = packet.GetModel<Coop_Model_PlayerSetPresentBullet>();
			this.player.SetPresentBullet(model68.presentBulletId, (BulletData.BulletPresent.TYPE)model68.type, model68.position, model68.bulletName);
			break;
		}
		case PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET:
		{
			Coop_Model_PlayerPickPresentBullet model66 = packet.GetModel<Coop_Model_PlayerPickPresentBullet>();
			this.player.DestroyPresentBulletObject(model66.presentBulletId);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET:
		{
			Coop_Model_PlayerShotZoneBullet model65 = packet.GetModel<Coop_Model_PlayerShotZoneBullet>();
			this.player.ShotZoneBullet(this.player, model65.bulletName, model65.position);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET:
		{
			Coop_Model_PlayerShotDecoyBullet model63 = packet.GetModel<Coop_Model_PlayerShotDecoyBullet>();
			this.player.ShotDecoyBullet(model63.id, model63.skIndex, model63.decoyId, model63.bulletName, model63.position, isHit: false);
			break;
		}
		case PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET:
		{
			Coop_Model_PlayerExplodeDecoyBullet model62 = packet.GetModel<Coop_Model_PlayerExplodeDecoyBullet>();
			this.player.ExplodeDecoyBullet(model62.decoyId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_STANDBY:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonStandby model59 = packet.GetModel<Coop_Model_PlayerCannonStandby>();
			this.player.ApplySyncPosition(model59.pos, model59.dir);
			this.player.ActCannonStandby(model59.cannonId);
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_SHOT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonShot model56 = packet.GetModel<Coop_Model_PlayerCannonShot>();
			this.player.ApplySyncPosition(model56.pos, model56.dir);
			this.player.SetCannonState(Player.CANNON_STATE.READY);
			this.player.ApplyCannonVector(model56.cannonVec);
			this.player.ActCannonShot();
			break;
		}
		case PACKET_TYPE.PLAYER_CANNON_ROTATE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCannonRotate model51 = packet.GetModel<Coop_Model_PlayerCannonRotate>();
			this.player.SetSyncCannonRotation(model51.cannonVec);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE:
		{
			Coop_Model_PlayerGetChargeSkillGauge model50 = packet.GetModel<Coop_Model_PlayerGetChargeSkillGauge>();
			this.player.OnGetChargeSkillGauge((BuffParam.BUFFTYPE)model50.buffType, model50.buffValue, model50.useSkillIndex, !model50.receive, model50.isCorrectWaveMatch);
			break;
		}
		case PACKET_TYPE.PLAYER_RESURRECT:
		{
			Coop_Model_PlayerResurrect model48 = packet.GetModel<Coop_Model_PlayerResurrect>();
			this.player.OnResurrection(isPacket: true);
			break;
		}
		case PACKET_TYPE.PLAYER_GET_RESURRECT:
		{
			Coop_Model_PlayerGetResurrect model47 = packet.GetModel<Coop_Model_PlayerGetResurrect>();
			this.player.OnGetResurrection();
			break;
		}
		case PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC:
		{
			Coop_Model_PlayerSpecialActionGaugeSync model45 = packet.GetModel<Coop_Model_PlayerSpecialActionGaugeSync>();
			this.player.OnSyncSpecialActionGauge(model45.weaponIndex, model45.currentSpActionGauge);
			this.player.pairSwordsCtrl.SetComboLv(model45.comboLv);
			break;
		}
		case PACKET_TYPE.PLAYER_CHARGE_EXPAND_RELEASE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerChargeExpandRelease model41 = packet.GetModel<Coop_Model_PlayerChargeExpandRelease>();
			this.player.ApplySyncPosition(model41.pos, model41.dir);
			this.player.SetChargeExpandRelease(model41.charge_rate);
			this.player.SetLerpRotation(Quaternion.AngleAxis(model41.lerp_dir, Vector3.get_up()) * Vector3.get_forward());
			this.player.SetActionPosition(model41.act_pos, model41.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_RIZE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpRize model37 = packet.GetModel<Coop_Model_PlayerJumpRize>();
			this.player.OnJumpRize(model37.dir, model37.level);
			break;
		}
		case PACKET_TYPE.PLAYER_JUMP_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerJumpEnd model33 = packet.GetModel<Coop_Model_PlayerJumpEnd>();
			this.player.OnJumpEnd(model33.pos, model33.isSuccess, model33.y);
			break;
		}
		case PACKET_TYPE.PLAYER_SOUL_BOOST:
		{
			Coop_Model_PlayerSoulBoost model32 = packet.GetModel<Coop_Model_PlayerSoulBoost>();
			this.player.OnSoulBoost(model32.isBoost);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveActionSync model30 = packet.GetModel<Coop_Model_PlayerEvolveActionSync>();
			this.player.OnSyncEvolveAction(model30.isAction);
			break;
		}
		case PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerEvolveSpecialAction model27 = packet.GetModel<Coop_Model_PlayerEvolveSpecialAction>();
			this.player.ApplySyncPosition(model27.pos, model27.dir);
			this.player.ActEvolveSpecialAction();
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_POS:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchPos model23 = packet.GetModel<Coop_Model_PlayerSnatchPos>();
			this.player.snatchCtrl.OnHit(model23.enemyId, model23.hitPoint);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_START:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveStart model19 = packet.GetModel<Coop_Model_PlayerSnatchMoveStart>();
			this.player.OnSnatchMoveStart(model19.snatchPos);
			break;
		}
		case PACKET_TYPE.PLAYER_SNATCH_MOVE_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSnatchMoveEnd model14 = packet.GetModel<Coop_Model_PlayerSnatchMoveEnd>();
			this.player.SetActionPosition(model14.act_pos, model14.act_pos_f);
			this.player.ApplySyncPosition(model14.pos, model14.dir);
			this.player.OnSnatchMoveEnd(model14.triggerIndex);
			break;
		}
		case PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerPairSwordsLaserEnd model10 = packet.GetModel<Coop_Model_PlayerPairSwordsLaserEnd>();
			this.player.OnSyncSpecialActionGauge(model10.weaponIndex, model10.currentSpActionGauge);
			this.player.pairSwordsCtrl.OnLaserEnd(isPacket: true);
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotHealingHoming model5 = packet.GetModel<Coop_Model_PlayerShotHealingHoming>();
			this.player.OnShotHealingHoming(model5);
			break;
		}
		case PACKET_TYPE.PLAYER_SACRIFICED_HP:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSacrificedHp model2 = packet.GetModel<Coop_Model_PlayerSacrificedHp>();
			this.player.spearCtrl.SacrificedHp(model2.sacrificedHp, isPacket: true);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP_CREATE:
		{
			Coop_Model_WaveMatchDropCreate model75 = packet.GetModel<Coop_Model_WaveMatchDropCreate>();
			MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropCreate(model75);
			break;
		}
		case PACKET_TYPE.WAVEMATCH_DROP_PICKED:
		{
			Coop_Model_WaveMatchDropPicked model74 = packet.GetModel<Coop_Model_WaveMatchDropPicked>();
			MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropPicked(model74);
			break;
		}
		case PACKET_TYPE.GATHER_GIMMICK_INFO:
		{
			Coop_Model_GatherGimmickInfo model73 = packet.GetModel<Coop_Model_GatherGimmickInfo>();
			MonoBehaviourSingleton<InGameProgress>.I.UpdatGatherGimmickInfo(model73.managedId, model73.ownerId, model73.isUsed);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER_GIMMICK:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGatherGimmick model70 = packet.GetModel<Coop_Model_PlayerGatherGimmick>();
			this.player.ApplySyncPosition(model70.pos, model70.dir);
			this.player.ActGatherGimmick(model70.gimmickId);
			this.player.SetActionPosition(model70.act_pos, model70.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_GATHER_GIMMICK_STATE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerGatherGimmickState model67 = packet.GetModel<Coop_Model_PlayerGatherGimmickState>();
			this.player.OnGatherGimmickState(model67.state);
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_POSITION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncPosition model64 = packet.GetModel<Coop_Model_PlayerSyncPosition>();
			this.player.ApplySyncPosition(model64.pos, model64.dir);
			break;
		}
		case PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_INCREASE:
			if (base.character.isDead)
			{
				return true;
			}
			if (this.player.fishingCtrl == null)
			{
				return true;
			}
			this.player.fishingCtrl.OnReceiveCoopFishingGaugeIncrease();
			break;
		case PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_SYNC:
		{
			if (base.character.isDead)
			{
				return true;
			}
			if (this.player.fishingCtrl == null)
			{
				return true;
			}
			Coop_Model_PlayerCoopFishingGaugeSync model55 = packet.GetModel<Coop_Model_PlayerCoopFishingGaugeSync>();
			this.player.fishingCtrl.OnCurrentGaugeSync(model55.ownerUserId, model55.gaugeValue);
			break;
		}
		case PACKET_TYPE.PLAYER_COOP_FISHING_START:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCoopFishingStart model52 = packet.GetModel<Coop_Model_PlayerCoopFishingStart>();
			this.player.ApplySyncPosition(model52.pos, model52.dir);
			this.player.ActCoopFishingStart(model52.gimmickId);
			this.player.SetActionPosition(model52.actPos, model52.actPosFlag);
			this.player.SetLerpRotation(Vector3.get_zero());
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_RESURRECTION_HOMING:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerShotResurrectionHoming model49 = packet.GetModel<Coop_Model_PlayerShotResurrectionHoming>();
			this.player.OnShotResurrectionHoming(model49);
			break;
		}
		case PACKET_TYPE.PLAYER_STONE_COUNT:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerStoneCount model46 = packet.GetModel<Coop_Model_PlayerStoneCount>();
			this.player.StoneCount(model46.remaind_time, model46.stop, model46.requested);
			break;
		}
		case PACKET_TYPE.PLAYER_STONE_END:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerStoneEnd model43 = packet.GetModel<Coop_Model_PlayerStoneEnd>();
			this.player.ActStoneEnd(model43.countTime);
			break;
		}
		case PACKET_TYPE.PLAYER_FLICK_ACTION:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerFlickAction model40 = packet.GetModel<Coop_Model_PlayerFlickAction>();
			this.player.ApplySyncPosition(model40.pos, model40.dir);
			this.player.ActFlickAction(model40.inputVec, isOriginal: false);
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_COMBINE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncCombine model35 = packet.GetModel<Coop_Model_PlayerSyncCombine>();
			if (this.player != null && this.player.pairSwordsCtrl != null)
			{
				this.player.pairSwordsCtrl.CombineBurst(model35.isCombine);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_SYNC_SUBSTITUTE:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerSyncSubstitute model31 = packet.GetModel<Coop_Model_PlayerSyncSubstitute>();
			if (this.player != null && this.player.buffParam != null && this.player.buffParam.substituteCtrl != null)
			{
				this.player.buffParam.substituteCtrl.Sync(model31.num);
			}
			break;
		}
		case PACKET_TYPE.PLAYER_WEAPON_ACTION_START:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerWeaponActionStart model29 = packet.GetModel<Coop_Model_PlayerWeaponActionStart>();
			this.player.EventWeaponActionStart();
			break;
		}
		case PACKET_TYPE.PLAYER_WEAPON_ACTION_END:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerWeaponActionEnd model26 = packet.GetModel<Coop_Model_PlayerWeaponActionEnd>();
			this.player.EventWeaponActionEnd();
			break;
		}
		case PACKET_TYPE.PLAYER_SHOT_SHIELD_REFLECT:
		{
			Coop_Model_PlayerShotShieldReflect model25 = packet.GetModel<Coop_Model_PlayerShotShieldReflect>();
			this.player.OnShotShieldReflect(model25);
			break;
		}
		case PACKET_TYPE.PLAYER_RAIN_SHOT_CHARGE_RELEASE:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRainShotChargeRelease model24 = packet.GetModel<Coop_Model_PlayerRainShotChargeRelease>();
			this.player.OnRainShotChargeRelease(model24.fallPos, model24.fallRotY);
			break;
		}
		case PACKET_TYPE.PLAYER_CARRY:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCarry model21 = packet.GetModel<Coop_Model_PlayerCarry>();
			this.player.ApplySyncPosition(model21.pos, model21.dir);
			this.player.ActCarry(model21.type, model21.pointId);
			break;
		}
		case PACKET_TYPE.PLAYER_CARRY_IDLE:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCarryIdle model18 = packet.GetModel<Coop_Model_PlayerCarryIdle>();
			this.player.ApplySyncPosition(model18.pos, model18.dir);
			this.player.ActCarryIdle();
			break;
		}
		case PACKET_TYPE.PLAYER_CARRY_PUT:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerCarryPut model15 = packet.GetModel<Coop_Model_PlayerCarryPut>();
			this.player.ApplySyncPosition(model15.pos, model15.dir);
			this.player.ActCarryPut(model15.pointId);
			break;
		}
		case PACKET_TYPE.PLAYER_ORACLE_HORIZONTAL_NEXT_MOTION:
		{
			if (this.player.isDead)
			{
				return true;
			}
			Coop_Model_PlayerOracleHorizontalNextMotion model12 = packet.GetModel<Coop_Model_PlayerOracleHorizontalNextMotion>();
			this.player.thsCtrl.oracleCtrl.SetHorizontalNextMotion(model12.isFinish);
			this.player.ApplySyncPosition(model12.pos, model12.dir);
			break;
		}
		case PACKET_TYPE.PLAYER_TELEPORT_AVOID:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerTeleportAvoid model9 = packet.GetModel<Coop_Model_PlayerTeleportAvoid>();
			this.player.ApplySyncPosition(model9.pos, model9.dir);
			this.player.ActTeleportAvoid();
			break;
		}
		case PACKET_TYPE.PLAYER_QUEST_GIMMICK:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerQuestGimmick model6 = packet.GetModel<Coop_Model_PlayerQuestGimmick>();
			this.player.ApplySyncPosition(model6.pos, model6.dir);
			this.player.ActQuestGimmick(model6.gimmickId);
			this.player.SetActionPosition(model6.act_pos, model6.act_pos_f);
			break;
		}
		case PACKET_TYPE.PLAYER_ORACLE_SPEAR_STOCK:
			if (base.character.isDead)
			{
				return true;
			}
			this.player.spearCtrl.OnUpdateOracleStock();
			break;
		case PACKET_TYPE.PLAYER_RUSH_AVOID:
		{
			if (base.character.isDead)
			{
				return true;
			}
			Coop_Model_PlayerRushAvoid model = packet.GetModel<Coop_Model_PlayerRushAvoid>();
			this.player.ApplySyncPosition(model.pos, model.dir);
			this.player.ActRushAvoid(model.inputVec);
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
