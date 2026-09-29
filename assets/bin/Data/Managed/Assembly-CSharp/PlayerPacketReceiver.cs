// Decompiled with JetBrains decompiler
// Type: PlayerPacketReceiver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class PlayerPacketReceiver : CharacterPacketReceiver
{
  protected Player player => (Player) this.owner;

  protected override bool CheckFilterPacket(CoopPacket packet)
  {
    return this.filterMode == ObjectPacketReceiver.FILTER_MODE.WAIT_INITIALIZE && packet.packetType == PACKET_TYPE.PLAYER_INITIALIZE || base.CheckFilterPacket(packet);
  }

  protected override bool HandleCoopEvent(CoopPacket packet)
  {
    switch (packet.packetType)
    {
      case PACKET_TYPE.PLAYER_LOAD_COMPLETE:
        if (!this.player.isSetAppearPos)
          return false;
        if (Object.op_Inequality((Object) this.player.playerSender, (Object) null))
        {
          this.player.playerSender.OnRecvLoadComplete(packet.fromClientId);
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_INITIALIZE:
        if (this.player.isLoading)
          return false;
        Coop_Model_PlayerInitialize model1 = packet.GetModel<Coop_Model_PlayerInitialize>();
        this.player.ApplySyncPosition(model1.pos, model1.dir, true);
        this.player.hp = model1.hp;
        this.player.healHp = model1.healHp;
        this.player.StopCounter(model1.stopcounter);
        StageObject target = (StageObject) null;
        if (model1.target_id >= 0)
          target = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(model1.target_id);
        this.player.SetActionTarget(target);
        this.player.buffParam.SetSyncParam(model1.buff_sync_param);
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
          MonoBehaviourSingleton<InGameRecorder>.I.ApplySyncOwnerData(model1.id);
        MonoBehaviourSingleton<StageObjectManager>.I.RemoveCacheObject((StageObject) this.player);
        ((Component) this.player).gameObject.SetActive(true);
        this.SetFilterMode(ObjectPacketReceiver.FILTER_MODE.NONE);
        this.player.isCoopInitialized = true;
        this.player.SetAppearPos(this.player._position);
        bool flag = false;
        if (this.player.weaponData == null != (model1.weapon_item == null))
          flag = true;
        else if (this.player.weaponData != null && this.player.weaponData.eId != model1.weapon_item.eId)
          flag = true;
        else if (this.player.weaponIndex != model1.weapon_index)
          flag = true;
        CoopClient byClientId1 = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
        if (Object.op_Inequality((Object) byClientId1, (Object) null) && !byClientId1.IsBattleStart())
        {
          this.player.WaitBattleStart();
          ((Component) this.player).gameObject.SetActive(false);
          MonoBehaviourSingleton<StageObjectManager>.I.AddCacheObject((StageObject) this.player);
        }
        else if (flag)
        {
          this.player.LoadWeapon(model1.weapon_item, model1.weapon_index, (PlayerLoader.OnCompleteLoad) (o => this.player.ActBattleStart(true)));
        }
        else
        {
          this.player.SetNowWeapon(model1.weapon_item, model1.weapon_index, this.player.uniqueEquipmentIndex);
          this.player.InitParameter();
          if (this.player.hp <= 0)
          {
            this.player.ActBattleStart(true);
            if (!this.player.isDead)
              this.player.ActDeadLoop();
          }
          else if (this.player.fishingCtrl != null && model1.fishingState > 0 && model1.gatherGimmickId > 0)
          {
            this.player.ActBattleStart(true);
            FishingController.eState fishingState = (FishingController.eState) model1.fishingState;
            if (fishingState == FishingController.eState.Coop)
            {
              this.player.ActCoopFishingStart(model1.gatherGimmickId);
              this.player.SetLerpRotation(Vector3.zero);
            }
            else
            {
              this.player.ActGatherGimmick(model1.gatherGimmickId);
              this.player.SetLerpRotation(Vector3.zero);
              this.player.fishingCtrl.ChangeState(fishingState);
              if (fishingState == FishingController.eState.Fight)
              {
                this.player.PlayMotion("fishing_send");
                this.player.EventActionRendererON((AnimEventData.EventData) null);
                SoundManager.PlayLoopSE(this.player.fishingCtrl.GetSeId(2), (DisableNotifyMonoBehaviour) this.player, this.player.FindNode(""));
              }
            }
          }
          else if (model1.carryingGimmickId > 0)
            this.player.ActCarry(InGameProgress.eFieldGimmick.CarriableGimmick, model1.carryingGimmickId);
          else if (model1.act_battle_start)
          {
            this.player.ActBattleStart();
          }
          else
          {
            this.player.ActBattleStart(true);
            this.player.ActIdle(false, -1f);
          }
        }
        this.player.SetSyncUsingCannon(model1.cannonId);
        this.player.bulletIndex = model1.bulletIndex;
        break;
      case PACKET_TYPE.PLAYER_ATTACK_COMBO:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerAttackCombo model2 = packet.GetModel<Coop_Model_PlayerAttackCombo>();
        this.owner._position = model2.pos;
        this.owner._rotation = Quaternion.AngleAxis(model2.dir, Vector3.up);
        this.player.ActAttack(model2.attack_id, true, false, model2.motionLayerName, model2.motionStateName);
        this.player.SetActionPosition(model2.act_pos, model2.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_CHARGE_RELEASE:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerChargeRelease model3 = packet.GetModel<Coop_Model_PlayerChargeRelease>();
        this.player.ApplySyncExRush(model3.isExRushCharge);
        this.player.ApplySyncPosition(model3.pos, model3.dir);
        this.player.SetChargeRelease(model3.charge_rate);
        this.player.SetLerpRotation(Quaternion.op_Multiply(Quaternion.AngleAxis(model3.lerp_dir, Vector3.up), Vector3.forward));
        this.player.SetActionPosition(model3.act_pos, model3.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_AVOID:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerAvoid model4 = packet.GetModel<Coop_Model_PlayerAvoid>();
        this.player.ApplySyncPosition(model4.pos, model4.dir);
        this.player.ActAvoid();
        break;
      case PACKET_TYPE.PLAYER_BLOW_CLEAR:
        if (this.character.isDead)
          return true;
        this.player.InputBlowClear();
        break;
      case PACKET_TYPE.PLAYER_STUNNED_END:
        if (this.character.isDead)
          return true;
        this.player.SetStunnedEnd();
        break;
      case PACKET_TYPE.PLAYER_DEAD_COUNT:
        Coop_Model_PlayerDeadCount model5 = packet.GetModel<Coop_Model_PlayerDeadCount>();
        this.player.DeadCount(model5.remaind_time, model5.stop, model5.requested);
        break;
      case PACKET_TYPE.PLAYER_DEAD_STANDUP:
        Coop_Model_PlayerDeadStandup model6 = packet.GetModel<Coop_Model_PlayerDeadStandup>();
        this.player.ActDeadStandup(model6.standupHp, model6.cType);
        break;
      case PACKET_TYPE.PLAYER_STOP_COUNTER:
        this.player.StopCounter(packet.GetModel<Coop_Model_PlayerStopCounter>().stop);
        break;
      case PACKET_TYPE.PLAYER_GATHER:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerGather model7 = packet.GetModel<Coop_Model_PlayerGather>();
        GatherPointObject gather_point = (GatherPointObject) null;
        if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.gatherPointList != null)
        {
          int index = 0;
          for (int count = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList.Count; index < count; ++index)
          {
            if (model7.point_id == (int) MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[index].pointData.pointID)
            {
              gather_point = MonoBehaviourSingleton<InGameProgress>.I.gatherPointList[index];
              break;
            }
          }
        }
        if (Object.op_Inequality((Object) gather_point, (Object) null))
        {
          this.player.ApplySyncPosition(model7.pos, model7.dir);
          this.player.ActGather(gather_point);
          this.player.SetActionPosition(model7.act_pos, model7.act_pos_f);
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_SKILL_ACTION:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSkillAction model8 = packet.GetModel<Coop_Model_PlayerSkillAction>();
        this.player.ApplySyncPosition(model8.pos, model8.dir);
        this.player.ActSkillAction(model8.skill_index, model8.isUsingSecondGrade);
        this.player.SetActionPosition(model8.act_pos, model8.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_GET_HEAL:
        Coop_Model_PlayerGetHeal model9 = packet.GetModel<Coop_Model_PlayerGetHeal>();
        this.player.ExecHealHp(model9.Deserialize(), !model9.receive);
        break;
      case PACKET_TYPE.PLAYER_SPECIAL_ACTION:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSpecialAction model10 = packet.GetModel<Coop_Model_PlayerSpecialAction>();
        this.player.ApplySyncPosition(model10.pos, model10.dir);
        this.player.ActSpecialAction(model10.start_effect, model10.isSuccess);
        this.player.SetActionPosition(model10.act_pos, model10.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_SHOT_ARROW:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerShotArrow model11 = packet.GetModel<Coop_Model_PlayerShotArrow>();
        this.player.ApplySyncPosition(model11.pos, model11.dir);
        AttackInfo attackInfoExternal = this.player.FindAttackInfoExternal(model11.attack_name, true, model11.attack_rate);
        this.player.ShotArrow(model11.shot_pos, model11.shot_rot, attackInfoExternal, model11.is_sit_shot, model11.is_aim_end);
        break;
      case PACKET_TYPE.PLAYER_UPDATE_SKILL_INFO:
        this.player.skillInfo.SetSettingsInfo(packet.GetModel<Coop_Model_PlayerUpdateSkillInfo>().settings_info, this.player.equipWeaponList);
        break;
      case PACKET_TYPE.PLAYER_PRAYER_START:
        Coop_Model_PlayerPrayerStart model12 = packet.GetModel<Coop_Model_PlayerPrayerStart>();
        this.player.OnPrayerStart(new Player.PrayInfo()
        {
          targetId = model12.sid,
          reason = (Player.PRAY_REASON) model12.reason
        });
        break;
      case PACKET_TYPE.PLAYER_PRAYER_END:
        Coop_Model_PlayerPrayerEnd model13 = packet.GetModel<Coop_Model_PlayerPrayerEnd>();
        this.player.OnPrayerEnd(new Player.PrayInfo()
        {
          targetId = model13.sid,
          reason = (Player.PRAY_REASON) model13.reason
        });
        break;
      case PACKET_TYPE.PLAYER_CHANGE_WEAPON:
        if (this.character.isDead)
          return true;
        this.player.ActChangeWeapon((CharaInfo.EquipItem) null, -1);
        break;
      case PACKET_TYPE.PLAYER_APPLY_CHANGE_WEAPON:
        Coop_Model_PlayerApplyChangeWeapon model14 = packet.GetModel<Coop_Model_PlayerApplyChangeWeapon>();
        if ((long) this.player.weaponData.eId == (long) (uint) model14.item.eId && this.player.weaponIndex == model14.index)
          return true;
        this.player.ApplyChangeWeapon(model14.item, model14.index);
        break;
      case PACKET_TYPE.PLAYER_SETSTATUS:
        Coop_Model_PlayerSetStatus model15 = packet.GetModel<Coop_Model_PlayerSetStatus>();
        this.player.OnSetPlayerStatus(model15.level, model15.atk, model15.def, model15.hp);
        if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
        {
          MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.LEVEL_UP, this.player);
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_GET_RAREDROP:
        if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid())
        {
          Coop_Model_PlayerGetRareDrop model16 = packet.GetModel<Coop_Model_PlayerGetRareDrop>();
          string str = (string) null;
          switch ((REWARD_TYPE) model16.type)
          {
            case REWARD_TYPE.EQUIP_ITEM:
              EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) model16.item_id);
              if (equipItemData != null)
              {
                str = equipItemData.name;
                break;
              }
              break;
            case REWARD_TYPE.SKILL_ITEM:
              SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) model16.item_id);
              if (skillItemData != null)
              {
                str = skillItemData.name;
                break;
              }
              break;
            case REWARD_TYPE.ACCESSORY:
              AccessoryTable.AccessoryData data = Singleton<AccessoryTable>.I.GetData((uint) model16.item_id);
              if (data != null)
              {
                str = data.name;
                break;
              }
              break;
            default:
              ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData((uint) model16.item_id);
              if (itemData != null)
              {
                str = itemData.name;
                break;
              }
              break;
          }
          if (str != null)
          {
            MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(this.player.charaName, StringTable.Format(STRING_CATEGORY.IN_GAME, 4000U, (object) str));
            break;
          }
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_GRABBED:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerGrabbed model17 = packet.GetModel<Coop_Model_PlayerGrabbed>();
        this.player.ActGrabbedStart(model17.enemyId, new GrabInfo()
        {
          parentNode = model17.nodeName,
          duration = model17.duration,
          drainAttackId = model17.drainAtkId
        });
        break;
      case PACKET_TYPE.PLAYER_GRABBED_END:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerGrabbedEnd model18 = packet.GetModel<Coop_Model_PlayerGrabbedEnd>();
        this.player.ActGrabbedEnd(model18.angle, model18.power);
        break;
      case PACKET_TYPE.PLAYER_RESTRAINT:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerRestraint model19 = packet.GetModel<Coop_Model_PlayerRestraint>();
        this.player.ActRestraint(new RestraintInfo()
        {
          enable = true,
          duration = model19.duration,
          damageInterval = model19.damageInterval,
          damageRate = model19.damageRate,
          reduceTimeByFlick = model19.reduceTimeByFlick,
          effectName = model19.effectName,
          isStopMotion = model19.isStopMotion,
          isDisableRemoveByPlayerAttack = model19.isDisableRemoveByPlayerAttack
        });
        break;
      case PACKET_TYPE.PLAYER_RESTRAINT_END:
        if (this.character.isDead)
          return true;
        this.player.ActRestraintEnd();
        break;
      case PACKET_TYPE.PLAYER_SET_PRESENT_BULLET:
        Coop_Model_PlayerSetPresentBullet model20 = packet.GetModel<Coop_Model_PlayerSetPresentBullet>();
        this.player.SetPresentBullet(model20.presentBulletId, (BulletData.BulletPresent.TYPE) model20.type, model20.position, model20.bulletName);
        break;
      case PACKET_TYPE.PLAYER_PICK_PRESENT_BULLET:
        this.player.DestroyPresentBulletObject(packet.GetModel<Coop_Model_PlayerPickPresentBullet>().presentBulletId);
        break;
      case PACKET_TYPE.PLAYER_CANNON_STANDBY:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerCannonStandby model21 = packet.GetModel<Coop_Model_PlayerCannonStandby>();
        this.player.ApplySyncPosition(model21.pos, model21.dir);
        this.player.ActCannonStandby(model21.cannonId);
        break;
      case PACKET_TYPE.PLAYER_CANNON_SHOT:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerCannonShot model22 = packet.GetModel<Coop_Model_PlayerCannonShot>();
        this.player.ApplySyncPosition(model22.pos, model22.dir);
        this.player.SetCannonState(Player.CANNON_STATE.READY);
        this.player.ApplyCannonVector(model22.cannonVec);
        this.player.ActCannonShot();
        break;
      case PACKET_TYPE.PLAYER_CANNON_ROTATE:
        if (this.character.isDead)
          return true;
        this.player.SetSyncCannonRotation(packet.GetModel<Coop_Model_PlayerCannonRotate>().cannonVec);
        break;
      case PACKET_TYPE.PLAYER_GET_CHARGE_SKILLGAUGE:
        Coop_Model_PlayerGetChargeSkillGauge model23 = packet.GetModel<Coop_Model_PlayerGetChargeSkillGauge>();
        this.player.OnGetChargeSkillGauge((BuffParam.BUFFTYPE) model23.buffType, model23.buffValue, model23.useSkillIndex, !model23.receive, model23.isCorrectWaveMatch);
        break;
      case PACKET_TYPE.PLAYER_SPECIAL_ACTION_CONTINUE:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSpecialActionContinue model24 = packet.GetModel<Coop_Model_PlayerSpecialActionContinue>();
        this.player.ApplySyncPosition(model24.pos, model24.dir);
        this.player.ActSpAttackContinue();
        this.player.SetActionPosition(model24.act_pos, model24.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_SPECIAL_ACTION_GAUGE_SYNC:
        Coop_Model_PlayerSpecialActionGaugeSync model25 = packet.GetModel<Coop_Model_PlayerSpecialActionGaugeSync>();
        this.player.OnSyncSpecialActionGauge(model25.weaponIndex, model25.currentSpActionGauge);
        this.player.pairSwordsCtrl.SetComboLv(model25.comboLv);
        break;
      case PACKET_TYPE.PLAYER_RESURRECT:
        packet.GetModel<Coop_Model_PlayerResurrect>();
        this.player.OnResurrection(true);
        break;
      case PACKET_TYPE.PLAYER_GET_RESURRECT:
        packet.GetModel<Coop_Model_PlayerGetResurrect>();
        this.player.OnGetResurrection();
        break;
      case PACKET_TYPE.PLAYER_PRAYER_BOOST:
        Coop_Model_PlayerPrayerBoost model26 = packet.GetModel<Coop_Model_PlayerPrayerBoost>();
        this.player.OnChangeBoostPray(model26.sid, model26.boostPrayInfo);
        break;
      case PACKET_TYPE.PLAYER_CHARGE_EXPAND_RELEASE:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerChargeExpandRelease model27 = packet.GetModel<Coop_Model_PlayerChargeExpandRelease>();
        this.player.ApplySyncPosition(model27.pos, model27.dir);
        this.player.SetChargeExpandRelease(model27.charge_rate);
        this.player.SetLerpRotation(Quaternion.op_Multiply(Quaternion.AngleAxis(model27.lerp_dir, Vector3.up), Vector3.forward));
        this.player.SetActionPosition(model27.act_pos, model27.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_SHOT_ZONE_BULLET:
        Coop_Model_PlayerShotZoneBullet model28 = packet.GetModel<Coop_Model_PlayerShotZoneBullet>();
        this.player.ShotZoneBullet(this.player, model28.bulletName, model28.position);
        break;
      case PACKET_TYPE.PLAYER_JUMP_RIZE:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerJumpRize model29 = packet.GetModel<Coop_Model_PlayerJumpRize>();
        this.player.OnJumpRize(model29.dir, model29.level);
        break;
      case PACKET_TYPE.PLAYER_JUMP_END:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerJumpEnd model30 = packet.GetModel<Coop_Model_PlayerJumpEnd>();
        this.player.OnJumpEnd(model30.pos, model30.isSuccess, model30.y);
        break;
      case PACKET_TYPE.PLAYER_SOUL_BOOST:
        this.player.OnSoulBoost(packet.GetModel<Coop_Model_PlayerSoulBoost>().isBoost);
        break;
      case PACKET_TYPE.PLAYER_SHOT_DECOY_BULLET:
        Coop_Model_PlayerShotDecoyBullet model31 = packet.GetModel<Coop_Model_PlayerShotDecoyBullet>();
        this.player.ShotDecoyBullet(model31.id, model31.skIndex, model31.decoyId, model31.bulletName, model31.position, false);
        break;
      case PACKET_TYPE.PLAYER_EXPLODE_DECOY_BULLET:
        this.player.ExplodeDecoyBullet(packet.GetModel<Coop_Model_PlayerExplodeDecoyBullet>().decoyId);
        break;
      case PACKET_TYPE.PLAYER_WARP:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerWarp model32 = packet.GetModel<Coop_Model_PlayerWarp>();
        this.player.ApplySyncPosition(model32.pos, model32.dir);
        this.player.ActWarp();
        break;
      case PACKET_TYPE.PLAYER_EVOLVE_ACTION_SYNC:
        if (this.character.isDead)
          return true;
        this.player.OnSyncEvolveAction(packet.GetModel<Coop_Model_PlayerEvolveActionSync>().isAction);
        break;
      case PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerEvolveSpecialAction model33 = packet.GetModel<Coop_Model_PlayerEvolveSpecialAction>();
        this.player.ApplySyncPosition(model33.pos, model33.dir);
        this.player.ActEvolveSpecialAction();
        break;
      case PACKET_TYPE.PLAYER_SNATCH_POS:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSnatchPos model34 = packet.GetModel<Coop_Model_PlayerSnatchPos>();
        this.player.snatchCtrl.OnHit(model34.enemyId, model34.hitPoint);
        break;
      case PACKET_TYPE.PLAYER_SNATCH_MOVE_START:
        if (this.character.isDead)
          return true;
        this.player.OnSnatchMoveStart(packet.GetModel<Coop_Model_PlayerSnatchMoveStart>().snatchPos);
        break;
      case PACKET_TYPE.PLAYER_SNATCH_MOVE_END:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSnatchMoveEnd model35 = packet.GetModel<Coop_Model_PlayerSnatchMoveEnd>();
        this.player.SetActionPosition(model35.act_pos, model35.act_pos_f);
        this.player.ApplySyncPosition(model35.pos, model35.dir);
        this.player.OnSnatchMoveEnd(model35.triggerIndex);
        break;
      case PACKET_TYPE.PLAYER_PAIR_SWORDS_LASER_END:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerPairSwordsLaserEnd model36 = packet.GetModel<Coop_Model_PlayerPairSwordsLaserEnd>();
        this.player.OnSyncSpecialActionGauge(model36.weaponIndex, model36.currentSpActionGauge);
        this.player.pairSwordsCtrl.OnLaserEnd(true);
        break;
      case PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING:
        if (this.character.isDead)
          return true;
        this.player.OnShotHealingHoming(packet.GetModel<Coop_Model_PlayerShotHealingHoming>());
        break;
      case PACKET_TYPE.PLAYER_SHOT_SOUL_ARROW:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerShotSoulArrow model37 = packet.GetModel<Coop_Model_PlayerShotSoulArrow>();
        this.player.ApplySyncPosition(model37.pos, model37.dir);
        this.player.ShotSoulArrowPuppet(model37.shotPos, model37.bowRot, model37.targetPosList);
        break;
      case PACKET_TYPE.PLAYER_DEAD_COUNT_REQUEST:
        packet.GetModel<Coop_Model_PlayerDeadCountRequest>();
        if ((double) this.player.rescueTime > 0.0 && !this.player.IsPrayed() && !this.player.isWaitingResurrectionHoming)
        {
          MonoBehaviourSingleton<StageObjectManager>.I.self.playerSender.OnDeadCountRequest(packet.fromClientId, this.player);
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_SACRIFICED_HP:
        if (this.character.isDead)
          return true;
        this.player.spearCtrl.SacrificedHp(packet.GetModel<Coop_Model_PlayerSacrificedHp>().sacrificedHp, true);
        break;
      case PACKET_TYPE.WAVEMATCH_DROP_CREATE:
        MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropCreate(packet.GetModel<Coop_Model_WaveMatchDropCreate>());
        break;
      case PACKET_TYPE.WAVEMATCH_DROP_PICKED:
        MonoBehaviourSingleton<InGameProgress>.I.OnRecvWaveMatchDropPicked(packet.GetModel<Coop_Model_WaveMatchDropPicked>());
        break;
      case PACKET_TYPE.GATHER_GIMMICK_INFO:
        Coop_Model_GatherGimmickInfo model38 = packet.GetModel<Coop_Model_GatherGimmickInfo>();
        MonoBehaviourSingleton<InGameProgress>.I.UpdatGatherGimmickInfo(model38.managedId, model38.ownerId, model38.isUsed);
        break;
      case PACKET_TYPE.PLAYER_GATHER_GIMMICK:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerGatherGimmick model39 = packet.GetModel<Coop_Model_PlayerGatherGimmick>();
        this.player.ApplySyncPosition(model39.pos, model39.dir);
        this.player.ActGatherGimmick(model39.gimmickId);
        this.player.SetActionPosition(model39.act_pos, model39.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_GATHER_GIMMICK_STATE:
        if (this.character.isDead)
          return true;
        this.player.OnGatherGimmickState(packet.GetModel<Coop_Model_PlayerGatherGimmickState>().state);
        break;
      case PACKET_TYPE.PLAYER_SYNC_POSITION:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSyncPosition model40 = packet.GetModel<Coop_Model_PlayerSyncPosition>();
        this.player.ApplySyncPosition(model40.pos, model40.dir);
        break;
      case PACKET_TYPE.PLAYER_FLICK_ACTION:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerFlickAction model41 = packet.GetModel<Coop_Model_PlayerFlickAction>();
        this.player.ApplySyncPosition(model41.pos, model41.dir);
        this.player.ActFlickAction(model41.inputVec, false);
        break;
      case PACKET_TYPE.PLAYER_SYNC_COMBINE:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSyncCombine model42 = packet.GetModel<Coop_Model_PlayerSyncCombine>();
        if (Object.op_Inequality((Object) this.player, (Object) null) && this.player.pairSwordsCtrl != null)
        {
          this.player.pairSwordsCtrl.CombineBurst(model42.isCombine);
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_INCREASE:
        if (this.character.isDead || this.player.fishingCtrl == null)
          return true;
        this.player.fishingCtrl.OnReceiveCoopFishingGaugeIncrease();
        break;
      case PACKET_TYPE.PLAYER_COOP_FISHING_START:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerCoopFishingStart model43 = packet.GetModel<Coop_Model_PlayerCoopFishingStart>();
        this.player.ApplySyncPosition(model43.pos, model43.dir);
        this.player.ActCoopFishingStart(model43.gimmickId);
        this.player.SetActionPosition(model43.actPos, model43.actPosFlag);
        this.player.SetLerpRotation(Vector3.zero);
        break;
      case PACKET_TYPE.PLAYER_COOP_FISHING_GAUGE_SYNC:
        if (this.character.isDead || this.player.fishingCtrl == null)
          return true;
        Coop_Model_PlayerCoopFishingGaugeSync model44 = packet.GetModel<Coop_Model_PlayerCoopFishingGaugeSync>();
        this.player.fishingCtrl.OnCurrentGaugeSync(model44.ownerUserId, model44.gaugeValue);
        break;
      case PACKET_TYPE.PLAYER_SYNC_SUBSTITUTE:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerSyncSubstitute model45 = packet.GetModel<Coop_Model_PlayerSyncSubstitute>();
        if (Object.op_Inequality((Object) this.player, (Object) null) && this.player.buffParam != null && this.player.buffParam.substituteCtrl != null)
        {
          this.player.buffParam.substituteCtrl.Sync(model45.num);
          break;
        }
        break;
      case PACKET_TYPE.PLAYER_SHOT_RESURRECTION_HOMING:
        if (this.character.isDead)
          return true;
        this.player.OnShotResurrectionHoming(packet.GetModel<Coop_Model_PlayerShotResurrectionHoming>());
        break;
      case PACKET_TYPE.PLAYER_STONE_COUNT:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerStoneCount model46 = packet.GetModel<Coop_Model_PlayerStoneCount>();
        this.player.StoneCount(model46.remaind_time, model46.stop, model46.requested);
        break;
      case PACKET_TYPE.PLAYER_STONE_END:
        if (this.character.isDead)
          return true;
        this.player.ActStoneEnd(packet.GetModel<Coop_Model_PlayerStoneEnd>().countTime);
        break;
      case PACKET_TYPE.PLAYER_WEAPON_ACTION_START:
        if (this.player.isDead)
          return true;
        packet.GetModel<Coop_Model_PlayerWeaponActionStart>();
        this.player.EventWeaponActionStart();
        break;
      case PACKET_TYPE.PLAYER_WEAPON_ACTION_END:
        if (this.player.isDead)
          return true;
        packet.GetModel<Coop_Model_PlayerWeaponActionEnd>();
        this.player.EventWeaponActionEnd();
        break;
      case PACKET_TYPE.PLAYER_SHOT_SHIELD_REFLECT:
        this.player.OnShotShieldReflect(packet.GetModel<Coop_Model_PlayerShotShieldReflect>());
        break;
      case PACKET_TYPE.PLAYER_RAIN_SHOT_CHARGE_RELEASE:
        if (this.player.isDead)
          return true;
        Coop_Model_PlayerRainShotChargeRelease model47 = packet.GetModel<Coop_Model_PlayerRainShotChargeRelease>();
        this.player.OnRainShotChargeRelease(model47.fallPos, model47.fallRotY);
        break;
      case PACKET_TYPE.PLAYER_ORACLE_HORIZONTAL_NEXT_MOTION:
        if (this.player.isDead)
          return true;
        Coop_Model_PlayerOracleHorizontalNextMotion model48 = packet.GetModel<Coop_Model_PlayerOracleHorizontalNextMotion>();
        this.player.thsCtrl.oracleCtrl.SetHorizontalNextMotion(model48.isFinish);
        this.player.ApplySyncPosition(model48.pos, model48.dir);
        break;
      case PACKET_TYPE.PLAYER_CARRY:
        if (this.player.isDead)
          return true;
        Coop_Model_PlayerCarry model49 = packet.GetModel<Coop_Model_PlayerCarry>();
        this.player.ApplySyncPosition(model49.pos, model49.dir);
        this.player.ActCarry(model49.type, model49.pointId);
        break;
      case PACKET_TYPE.PLAYER_CARRY_IDLE:
        if (this.player.isDead)
          return true;
        Coop_Model_PlayerCarryIdle model50 = packet.GetModel<Coop_Model_PlayerCarryIdle>();
        this.player.ApplySyncPosition(model50.pos, model50.dir);
        this.player.ActCarryIdle();
        break;
      case PACKET_TYPE.PLAYER_CARRY_PUT:
        if (this.player.isDead)
          return true;
        Coop_Model_PlayerCarryPut model51 = packet.GetModel<Coop_Model_PlayerCarryPut>();
        this.player.ApplySyncPosition(model51.pos, model51.dir);
        this.player.ActCarryPut(model51.pointId);
        break;
      case PACKET_TYPE.PLAYER_TELEPORT_AVOID:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerTeleportAvoid model52 = packet.GetModel<Coop_Model_PlayerTeleportAvoid>();
        this.player.ApplySyncPosition(model52.pos, model52.dir);
        this.player.ActTeleportAvoid();
        break;
      case PACKET_TYPE.PLAYER_QUEST_GIMMICK:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerQuestGimmick model53 = packet.GetModel<Coop_Model_PlayerQuestGimmick>();
        this.player.ApplySyncPosition(model53.pos, model53.dir);
        this.player.ActQuestGimmick(model53.gimmickId);
        this.player.SetActionPosition(model53.act_pos, model53.act_pos_f);
        break;
      case PACKET_TYPE.PLAYER_ORACLE_SPEAR_STOCK:
        if (this.character.isDead)
          return true;
        this.player.spearCtrl.OnUpdateOracleStock();
        break;
      case PACKET_TYPE.PLAYER_RUSH_AVOID:
        if (this.character.isDead)
          return true;
        Coop_Model_PlayerRushAvoid model54 = packet.GetModel<Coop_Model_PlayerRushAvoid>();
        this.player.ApplySyncPosition(model54.pos, model54.dir);
        this.player.ActRushAvoid(model54.inputVec);
        break;
      default:
        return base.HandleCoopEvent(packet);
    }
    if (QuestManager.IsValidInGameExplore())
    {
      CoopClient byClientId2 = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByClientId(packet.fromClientId);
      if (Object.op_Inequality((Object) byClientId2, (Object) null))
        MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(byClientId2);
    }
    return true;
  }
}
