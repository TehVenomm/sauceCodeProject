// Decompiled with JetBrains decompiler
// Type: PlayerPacketSender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PlayerPacketSender : CharacterPacketSender
{
  public const float CANNON_ROTATE_INTERVAL = 1f;
  protected float cannonRotateTimer;

  protected Player player => (Player) this.owner;

  public override void OnUpdate()
  {
    base.OnUpdate();
    if (!(this.character is Self))
      return;
    Self character = this.character as Self;
    this.cannonRotateTimer += Time.deltaTime;
    if (!character.IsOnCannonMode() || (double) this.cannonRotateTimer < 1.0)
      return;
    Coop_Model_PlayerCannonRotate model = new Coop_Model_PlayerCannonRotate();
    model.id = this.owner.id;
    model.cannonVec = character.GetCannonVector();
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerCannonRotate>(model);
    this.cannonRotateTimer = 0.0f;
  }

  public override void OnLoadComplete(bool promise = true)
  {
    if (!this.enableSend || !this.owner.IsPuppet())
      return;
    Coop_Model_PlayerLoadComplete model = new Coop_Model_PlayerLoadComplete();
    model.id = this.owner.id;
    this.SendToExtra<Coop_Model_PlayerLoadComplete>(this.owner.coopClientId, model, promise);
  }

  public override void OnRecvLoadComplete(int to_client_id) => this.SendInitialize(to_client_id);

  public override void SendInitialize(int to_client_id = 0)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerInitialize model = new Coop_Model_PlayerInitialize();
    this._SendInitialize(model, false);
    if (to_client_id == 0)
      this.SendBroadcast<Coop_Model_PlayerInitialize>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
      {
        this._SendInitialize(send_model as Coop_Model_PlayerInitialize, true);
        return true;
      }));
    else
      this.SendToExtra<Coop_Model_PlayerInitialize>(to_client_id, model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
      {
        this._SendInitialize(send_model as Coop_Model_PlayerInitialize, true);
        return true;
      }));
    this.SendActionHistory(to_client_id);
    this.player.SyncDeadCount();
    this.player.SyncStoneCount();
  }

  private void _SendInitialize(Coop_Model_PlayerInitialize model, bool resend)
  {
    model.id = this.owner.id;
    if (!resend && this.actionHistoryData != null)
    {
      model.pos = this.actionHistoryData.startPos;
      model.dir = this.actionHistoryData.startDir;
    }
    else
      model.SetSyncPosition(this.owner);
    model.sid = this.player.id;
    model.hp = this.player.hp;
    model.healHp = this.player.healHp;
    model.target_id = Object.op_Inequality((Object) this.player.actionTarget, (Object) null) ? this.player.actionTarget.id : -1;
    model.stopcounter = this.player.isStopCounter;
    model.act_battle_start = false;
    if (this.player.actionID == (Character.ACTION_ID) 23)
      model.act_battle_start = true;
    else if (this.player.actionID == Character.ACTION_ID.IDLE && this.player.lastActionID == (Character.ACTION_ID) 23)
      model.act_battle_start = true;
    else if (!this.player.isActedBattleStart)
      model.act_battle_start = true;
    model.weapon_item = this.player.weaponData;
    model.weapon_index = this.player.weaponIndex;
    model.buff_sync_param = this.player.buffParam.CreateSyncParam();
    if (this.player.targetFieldGimmickCannon != null)
      model.cannonId = this.player.targetFieldGimmickCannon.GetId();
    model.bulletIndex = this.player.bulletIndex;
    if (this.player.fishingCtrl != null && this.player.fishingCtrl.IsFishing())
    {
      model.gatherGimmickId = this.player.gatherGimmickObject.GetId();
      model.fishingState = this.player.fishingCtrl.GetStateForInitialize();
    }
    if (!this.player.IsCarrying())
      return;
    model.carryingGimmickId = this.player.carryingGimmickObject.GetId();
  }

  public void OnSyncPosition()
  {
    Coop_Model_PlayerSyncPosition playerSyncPosition = new Coop_Model_PlayerSyncPosition();
    playerSyncPosition.id = this.owner.id;
    playerSyncPosition.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerSyncPosition>(playerSyncPosition);
    this.StackActionHistory((Coop_Model_ObjectBase) playerSyncPosition, false);
  }

  public void OnActAttackCombo(int id, string _motionLayerName, string _motionStateName = "")
  {
    Coop_Model_PlayerAttackCombo playerAttackCombo1 = new Coop_Model_PlayerAttackCombo();
    playerAttackCombo1.id = this.owner.id;
    playerAttackCombo1.pos = this.owner._position;
    Coop_Model_PlayerAttackCombo playerAttackCombo2 = playerAttackCombo1;
    Quaternion rotation = this.owner._rotation;
    double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
    playerAttackCombo2.dir = (float) y;
    playerAttackCombo1.attack_id = id;
    playerAttackCombo1.motionLayerName = _motionLayerName;
    playerAttackCombo1.motionStateName = _motionStateName;
    playerAttackCombo1.act_pos = this.character.actionPosition;
    playerAttackCombo1.act_pos_f = this.character.actionPositionFlag;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerAttackCombo>(playerAttackCombo1);
    this.StackActionHistory((Coop_Model_ObjectBase) playerAttackCombo1, true);
  }

  public void OnSetChargeRelease(float charge_rate, bool isChargeExRush)
  {
    Coop_Model_PlayerChargeRelease playerChargeRelease1 = new Coop_Model_PlayerChargeRelease();
    playerChargeRelease1.id = this.owner.id;
    playerChargeRelease1.SetSyncPosition(this.owner);
    if (Vector3.op_Equality(this.player.lerpRotateVec, Vector3.zero))
    {
      playerChargeRelease1.lerp_dir = playerChargeRelease1.dir;
    }
    else
    {
      Coop_Model_PlayerChargeRelease playerChargeRelease2 = playerChargeRelease1;
      Quaternion quaternion = Quaternion.LookRotation(this.player.lerpRotateVec);
      double y = (double) ((Quaternion) ref quaternion).eulerAngles.y;
      playerChargeRelease2.lerp_dir = (float) y;
    }
    playerChargeRelease1.charge_rate = charge_rate;
    playerChargeRelease1.act_pos = this.character.actionPosition;
    playerChargeRelease1.act_pos_f = this.character.actionPositionFlag;
    playerChargeRelease1.isExRushCharge = isChargeExRush;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerChargeRelease>(playerChargeRelease1);
    this.StackActionHistory((Coop_Model_ObjectBase) playerChargeRelease1, false);
  }

  public void OnSetChargeExpandRelease(float chargeRate)
  {
    Coop_Model_PlayerChargeExpandRelease chargeExpandRelease1 = new Coop_Model_PlayerChargeExpandRelease();
    chargeExpandRelease1.id = this.owner.id;
    chargeExpandRelease1.SetSyncPosition(this.owner);
    if (Vector3.op_Equality(this.player.lerpRotateVec, Vector3.zero))
    {
      chargeExpandRelease1.lerp_dir = chargeExpandRelease1.dir;
    }
    else
    {
      Coop_Model_PlayerChargeExpandRelease chargeExpandRelease2 = chargeExpandRelease1;
      Quaternion quaternion = Quaternion.LookRotation(this.player.lerpRotateVec);
      double y = (double) ((Quaternion) ref quaternion).eulerAngles.y;
      chargeExpandRelease2.lerp_dir = (float) y;
    }
    chargeExpandRelease1.charge_rate = chargeRate;
    chargeExpandRelease1.act_pos = this.character.actionPosition;
    chargeExpandRelease1.act_pos_f = this.character.actionPositionFlag;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerChargeExpandRelease>(chargeExpandRelease1);
    this.StackActionHistory((Coop_Model_ObjectBase) chargeExpandRelease1, false);
  }

  public void OnRestraintStart(RestraintInfo restInfo)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerRestraint model = new Coop_Model_PlayerRestraint();
    model.id = this.owner.id;
    model.duration = restInfo.duration;
    model.damageInterval = restInfo.damageInterval;
    model.damageRate = restInfo.damageRate;
    model.reduceTimeByFlick = restInfo.reduceTimeByFlick;
    model.effectName = restInfo.effectName;
    model.isStopMotion = restInfo.isStopMotion;
    model.isDisableRemoveByPlayerAttack = restInfo.isDisableRemoveByPlayerAttack;
    this.SendBroadcast<Coop_Model_PlayerRestraint>(model);
  }

  public void OnRestraintEnd()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerRestraintEnd model = new Coop_Model_PlayerRestraintEnd();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerRestraintEnd>(model);
  }

  public void OnActAvoid()
  {
    Coop_Model_PlayerAvoid modelPlayerAvoid = new Coop_Model_PlayerAvoid();
    modelPlayerAvoid.id = this.owner.id;
    modelPlayerAvoid.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerAvoid>(modelPlayerAvoid);
    this.StackActionHistory((Coop_Model_ObjectBase) modelPlayerAvoid, true);
  }

  public void OnWarp()
  {
    Coop_Model_PlayerWarp coopModelPlayerWarp = new Coop_Model_PlayerWarp();
    coopModelPlayerWarp.id = this.owner.id;
    coopModelPlayerWarp.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerWarp>(coopModelPlayerWarp);
    this.StackActionHistory((Coop_Model_ObjectBase) coopModelPlayerWarp, true);
  }

  public void OnTeleportAvoid()
  {
    Coop_Model_PlayerTeleportAvoid playerTeleportAvoid = new Coop_Model_PlayerTeleportAvoid();
    playerTeleportAvoid.id = this.owner.id;
    playerTeleportAvoid.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerTeleportAvoid>(playerTeleportAvoid);
    this.StackActionHistory((Coop_Model_ObjectBase) playerTeleportAvoid, true);
  }

  public void OnRushAvoid(Vector3 inputVec)
  {
    Coop_Model_PlayerRushAvoid modelPlayerRushAvoid = new Coop_Model_PlayerRushAvoid();
    modelPlayerRushAvoid.id = this.owner.id;
    modelPlayerRushAvoid.inputVec = inputVec;
    modelPlayerRushAvoid.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerRushAvoid>(modelPlayerRushAvoid);
    this.StackActionHistory((Coop_Model_ObjectBase) modelPlayerRushAvoid, true);
  }

  public void OnInputBlowClear()
  {
    Coop_Model_PlayerBlowClear modelPlayerBlowClear = new Coop_Model_PlayerBlowClear();
    modelPlayerBlowClear.id = this.owner.id;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerBlowClear>(modelPlayerBlowClear);
    this.StackActionHistory((Coop_Model_ObjectBase) modelPlayerBlowClear, false);
  }

  public void OnSetStunnedEnd()
  {
    Coop_Model_PlayerStunnedEnd playerStunnedEnd = new Coop_Model_PlayerStunnedEnd();
    playerStunnedEnd.id = this.owner.id;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerStunnedEnd>(playerStunnedEnd);
    this.StackActionHistory((Coop_Model_ObjectBase) playerStunnedEnd, false);
  }

  public void OnDeadCount(float time, bool stop)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerDeadCount model = new Coop_Model_PlayerDeadCount();
    model.id = this.owner.id;
    model.remaind_time = time;
    model.stop = stop;
    this.SendBroadcast<Coop_Model_PlayerDeadCount>(model);
  }

  public void OnStoneCount(float time, bool stop)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerStoneCount model = new Coop_Model_PlayerStoneCount();
    model.id = this.owner.id;
    model.remaind_time = time;
    model.stop = stop;
    this.SendBroadcast<Coop_Model_PlayerStoneCount>(model);
  }

  public void OnActStoneEnd(float countTime)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerStoneEnd model = new Coop_Model_PlayerStoneEnd();
    model.id = this.owner.id;
    model.countTime = countTime;
    this.SendBroadcast<Coop_Model_PlayerStoneEnd>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model => !Object.op_Equality((Object) this.player, (Object) null) && !this.player.IsStone()));
  }

  public void SendDeadCountRequest(int stageObjectId)
  {
    if (!this.enableSend)
      return;
    Coop_Model_PlayerDeadCountRequest model = new Coop_Model_PlayerDeadCountRequest();
    model.id = stageObjectId;
    this.SendBroadcast<Coop_Model_PlayerDeadCountRequest>(model);
  }

  public void OnDeadCountRequest(int toClientId, Player deadPlayer)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    this.StartCoroutine(this._OnDeadCountRequest(toClientId, deadPlayer));
  }

  private IEnumerator _OnDeadCountRequest(int toClientId, Player deadPlayer)
  {
    yield return (object) new WaitForSeconds(1.5f);
    if ((double) deadPlayer.rescueTime > 0.0 && !deadPlayer.IsPrayed() && !deadPlayer.isWaitingResurrectionHoming)
    {
      Coop_Model_PlayerDeadCount model = new Coop_Model_PlayerDeadCount();
      model.id = deadPlayer.id;
      model.remaind_time = deadPlayer.rescueTime;
      model.requested = true;
      model.stop = false;
      this.SendTo<Coop_Model_PlayerDeadCount>(toClientId, model);
    }
  }

  public void OnStopCounter(bool stop)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerStopCounter model = new Coop_Model_PlayerStopCounter();
    model.id = this.owner.id;
    model.stop = stop;
    this.SendBroadcast<Coop_Model_PlayerStopCounter>(model);
  }

  public void OnActDeadStandup(int standup_hp, Player.eContinueType cType)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerDeadStandup model = new Coop_Model_PlayerDeadStandup();
    model.id = this.owner.id;
    model.standupHp = standup_hp;
    model.cType = cType;
    this.SendBroadcast<Coop_Model_PlayerDeadStandup>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model => !Object.op_Equality((Object) this.player, (Object) null) && this.player.hp != 0));
  }

  public void OnPrayerStart(Player.PrayInfo prayInfo)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerPrayerStart model = new Coop_Model_PlayerPrayerStart();
    model.id = this.owner.id;
    model.sid = prayInfo.targetId;
    model.reason = (int) prayInfo.reason;
    this.SendBroadcast<Coop_Model_PlayerPrayerStart>(model);
  }

  public void OnPrayerEnd(Player.PrayInfo prayInfo)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerPrayerEnd model = new Coop_Model_PlayerPrayerEnd();
    model.id = this.owner.id;
    model.sid = prayInfo.targetId;
    model.reason = (int) prayInfo.reason;
    this.SendBroadcast<Coop_Model_PlayerPrayerEnd>(model);
  }

  public void OnChangePrayBoost(int prayedId, Player.BoostPrayInfo boostPrayInfo)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerPrayerBoost model = new Coop_Model_PlayerPrayerBoost();
    model.id = this.owner.id;
    model.sid = prayedId;
    model.boostPrayInfo = boostPrayInfo;
    this.SendBroadcast<Coop_Model_PlayerPrayerBoost>(model);
  }

  public void OnChangeWeapon()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerChangeWeapon model = new Coop_Model_PlayerChangeWeapon();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerChangeWeapon>(model);
  }

  public void OnApplyChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
  {
    if (this.enableSend && this.owner.IsOriginal())
    {
      Coop_Model_PlayerApplyChangeWeapon model = new Coop_Model_PlayerApplyChangeWeapon();
      model.id = this.owner.id;
      model.item = item;
      model.index = weapon_index;
      this.SendBroadcast<Coop_Model_PlayerApplyChangeWeapon>(model, true, onPreResend: (Func<Coop_Model_Base, bool>) (send_model =>
      {
        if (Object.op_Equality((Object) this.player, (Object) null))
          return false;
        Coop_Model_PlayerApplyChangeWeapon applyChangeWeapon = send_model as Coop_Model_PlayerApplyChangeWeapon;
        return applyChangeWeapon.item.eId == this.player.weaponData.eId && applyChangeWeapon.index == this.player.weaponIndex;
      }));
    }
    this.ClearActionHistory();
  }

  public void OnActGather(GatherPointObject gather_point)
  {
    Coop_Model_PlayerGather modelPlayerGather = new Coop_Model_PlayerGather();
    modelPlayerGather.id = this.owner.id;
    modelPlayerGather.SetSyncPosition(this.owner);
    modelPlayerGather.act_pos = this.character.actionPosition;
    modelPlayerGather.act_pos_f = this.character.actionPositionFlag;
    modelPlayerGather.point_id = (int) gather_point.pointData.pointID;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerGather>(modelPlayerGather);
    this.StackActionHistory((Coop_Model_ObjectBase) modelPlayerGather, true);
  }

  public void OnActSkillAction(int skill_index, SkillInfo.SkillParam skill_param)
  {
    Coop_Model_PlayerSkillAction playerSkillAction = new Coop_Model_PlayerSkillAction();
    playerSkillAction.id = this.owner.id;
    playerSkillAction.SetSyncPosition(this.owner);
    playerSkillAction.act_pos = this.character.actionPosition;
    playerSkillAction.act_pos_f = this.character.actionPositionFlag;
    playerSkillAction.skill_index = skill_index;
    playerSkillAction.isUsingSecondGrade = skill_param.isUsingSecondGrade;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerSkillAction>(playerSkillAction);
    this.StackActionHistory((Coop_Model_ObjectBase) playerSkillAction, true);
  }

  public void OnHealReceive(Character.HealData healData)
  {
    if (!this.enableSend || !this.owner.IsPuppet())
      return;
    Coop_Model_PlayerGetHeal model = new Coop_Model_PlayerGetHeal();
    model.Serialize(this.owner.id, healData, true);
    this.SendTo<Coop_Model_PlayerGetHeal>(this.owner.coopClientId, model);
  }

  public void OnGetHeal(Character.HealData healData)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerGetHeal model = new Coop_Model_PlayerGetHeal();
    model.Serialize(this.owner.id, healData, false);
    this.SendBroadcast<Coop_Model_PlayerGetHeal>(model);
  }

  public void OnChargeSkillGaugeReceive(
    BuffParam.BUFFTYPE buffType,
    int buffValue,
    int useSkillIndex)
  {
    if (!this.enableSend || !this.owner.IsPuppet())
      return;
    Coop_Model_PlayerGetChargeSkillGauge model = new Coop_Model_PlayerGetChargeSkillGauge();
    model.id = this.owner.id;
    model.buffType = (int) buffType;
    model.buffValue = buffValue;
    model.useSkillIndex = useSkillIndex;
    model.receive = true;
    this.SendTo<Coop_Model_PlayerGetChargeSkillGauge>(this.owner.coopClientId, model);
  }

  public void OnGetChargeSkillGauge(
    BuffParam.BUFFTYPE buffType,
    int buffValue,
    int useSkillIndex,
    bool isCorrectWaveMatch)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerGetChargeSkillGauge model = new Coop_Model_PlayerGetChargeSkillGauge();
    model.id = this.owner.id;
    model.buffType = (int) buffType;
    model.buffValue = buffValue;
    model.useSkillIndex = useSkillIndex;
    model.receive = false;
    model.isCorrectWaveMatch = isCorrectWaveMatch;
    this.SendBroadcast<Coop_Model_PlayerGetChargeSkillGauge>(model);
  }

  public void OnResurrectionReceive()
  {
    if (!this.enableSend || !this.owner.IsPuppet())
      return;
    Coop_Model_PlayerResurrect model = new Coop_Model_PlayerResurrect();
    model.id = this.owner.id;
    this.SendTo<Coop_Model_PlayerResurrect>(this.owner.coopClientId, model);
  }

  public void OnGetResurrection()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerGetResurrect model = new Coop_Model_PlayerGetResurrect();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerGetResurrect>(model);
  }

  public void OnActSpecialAction(bool start_effect, bool isSuccess)
  {
    Coop_Model_PlayerSpecialAction playerSpecialAction = new Coop_Model_PlayerSpecialAction();
    playerSpecialAction.id = this.owner.id;
    playerSpecialAction.SetSyncPosition(this.owner);
    playerSpecialAction.act_pos = this.character.actionPosition;
    playerSpecialAction.act_pos_f = this.character.actionPositionFlag;
    playerSpecialAction.start_effect = start_effect;
    playerSpecialAction.isSuccess = isSuccess;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerSpecialAction>(playerSpecialAction);
    this.StackActionHistory((Coop_Model_ObjectBase) playerSpecialAction, true);
  }

  public void OnUpdateSkillInfo()
  {
    Coop_Model_PlayerUpdateSkillInfo playerUpdateSkillInfo = new Coop_Model_PlayerUpdateSkillInfo();
    playerUpdateSkillInfo.id = this.owner.id;
    SkillInfo.SkillSettingsInfo skillSettingsInfo = new SkillInfo.SkillSettingsInfo();
    int skill_index = 0;
    for (int index = 9; skill_index < index; ++skill_index)
    {
      SkillInfo.SkillParam skillParam = this.player.skillInfo.GetSkillParam(skill_index);
      SkillInfo.SkillSettingsInfo.Element element = new SkillInfo.SkillSettingsInfo.Element();
      if (skillParam != null)
      {
        element.baseInfo = skillParam.baseInfo;
        element.useGaugeCounter = skillParam.useGaugeCounter;
      }
      skillSettingsInfo.elementList.Add(element);
    }
    playerUpdateSkillInfo.settings_info = skillSettingsInfo;
  }

  public void OnShotArrow(
    Vector3 shot_pos,
    Quaternion shot_rot,
    AttackInfo attack_info,
    bool is_sit_shot,
    bool is_aim_end)
  {
    Coop_Model_PlayerShotArrow modelPlayerShotArrow = new Coop_Model_PlayerShotArrow();
    modelPlayerShotArrow.id = this.owner.id;
    modelPlayerShotArrow.SetSyncPosition(this.owner);
    modelPlayerShotArrow.shot_pos = shot_pos;
    modelPlayerShotArrow.shot_rot = shot_rot;
    modelPlayerShotArrow.attack_name = attack_info.name;
    modelPlayerShotArrow.attack_rate = attack_info.rateInfoRate;
    modelPlayerShotArrow.shot_count = this.player.shotArrowCount;
    modelPlayerShotArrow.is_sit_shot = is_sit_shot;
    modelPlayerShotArrow.is_aim_end = is_aim_end;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerShotArrow>(modelPlayerShotArrow);
    this.StackActionHistory((Coop_Model_ObjectBase) modelPlayerShotArrow, false);
  }

  public void OnShotSoulArrow(Vector3 shotPos, Quaternion bowRot, List<Vector3> targetPosList)
  {
    Coop_Model_PlayerShotSoulArrow playerShotSoulArrow = new Coop_Model_PlayerShotSoulArrow();
    playerShotSoulArrow.id = this.owner.id;
    playerShotSoulArrow.SetSyncPosition(this.owner);
    playerShotSoulArrow.shotPos = shotPos;
    playerShotSoulArrow.bowRot = bowRot;
    playerShotSoulArrow.targetPosList = targetPosList;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerShotSoulArrow>(playerShotSoulArrow);
    this.StackActionHistory((Coop_Model_ObjectBase) playerShotSoulArrow, false);
  }

  public void OnSetPlayerStatus(int level, int atk, int def, int hp)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSetStatus model = new Coop_Model_PlayerSetStatus();
    model.id = this.owner.id;
    model.level = level;
    model.atk = atk;
    model.def = def;
    model.hp = hp;
    this.SendBroadcast<Coop_Model_PlayerSetStatus>(model);
  }

  public void OnGetRareDrop(REWARD_TYPE type, int item_id)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerGetRareDrop model = new Coop_Model_PlayerGetRareDrop();
    model.id = this.owner.id;
    model.type = (int) type;
    model.item_id = item_id;
    this.SendBroadcast<Coop_Model_PlayerGetRareDrop>(model);
  }

  public void OnGrabbedStart(int enemyId, string nodeName, float duration, int drainAtkId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerGrabbed model = new Coop_Model_PlayerGrabbed();
    model.id = this.owner.id;
    model.enemyId = enemyId;
    model.nodeName = nodeName;
    model.duration = duration;
    model.drainAtkId = drainAtkId;
    this.SendBroadcast<Coop_Model_PlayerGrabbed>(model);
  }

  public void OnGrabbedEnd()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerGrabbedEnd model = new Coop_Model_PlayerGrabbedEnd();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerGrabbedEnd>(model);
  }

  public void OnSetPresentBullet(
    int presentBulletId,
    BulletData.BulletPresent.TYPE type,
    Vector3 position,
    string bulletName)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSetPresentBullet model = new Coop_Model_PlayerSetPresentBullet();
    model.id = this.owner.id;
    model.presentBulletId = presentBulletId;
    model.type = (int) type;
    model.position = position;
    model.bulletName = bulletName;
    this.SendBroadcast<Coop_Model_PlayerSetPresentBullet>(model);
  }

  public void OnPickPresentBullet(int presentBulletId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerPickPresentBullet model = new Coop_Model_PlayerPickPresentBullet();
    model.id = this.owner.id;
    model.presentBulletId = presentBulletId;
    this.SendBroadcast<Coop_Model_PlayerPickPresentBullet>(model);
  }

  public void OnShotZoneBullet(string bulletName, Vector3 position)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerShotZoneBullet model = new Coop_Model_PlayerShotZoneBullet();
    model.id = this.owner.id;
    model.bulletName = bulletName;
    model.position = position;
    this.SendBroadcast<Coop_Model_PlayerShotZoneBullet>(model);
  }

  public void OnShotDecoyBullet(int skIndex, int decoyId, string bulletName, Vector3 position)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerShotDecoyBullet model = new Coop_Model_PlayerShotDecoyBullet();
    model.id = this.owner.id;
    model.skIndex = skIndex;
    model.decoyId = decoyId;
    model.bulletName = bulletName;
    model.position = position;
    this.SendBroadcast<Coop_Model_PlayerShotDecoyBullet>(model);
  }

  public void OnExplodeDecoyBullet(int decoyId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerExplodeDecoyBullet model = new Coop_Model_PlayerExplodeDecoyBullet();
    model.id = this.owner.id;
    model.decoyId = decoyId;
    this.SendBroadcast<Coop_Model_PlayerExplodeDecoyBullet>(model);
  }

  public void OnCannonStandby(int cannonId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCannonStandby model = new Coop_Model_PlayerCannonStandby();
    model.id = this.owner.id;
    model.cannonId = cannonId;
    model.SetSyncPosition(this.owner);
    this.SendBroadcast<Coop_Model_PlayerCannonStandby>(model);
  }

  public void OnCannonShot()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCannonShot model = new Coop_Model_PlayerCannonShot();
    model.id = this.owner.id;
    model.SetSyncPosition(this.owner);
    model.cannonVec = this.player.GetCannonVector();
    this.SendBroadcast<Coop_Model_PlayerCannonShot>(model);
  }

  public void OnActSpAttackContinue()
  {
    Coop_Model_PlayerSpecialActionContinue specialActionContinue = new Coop_Model_PlayerSpecialActionContinue();
    specialActionContinue.id = this.owner.id;
    specialActionContinue.SetSyncPosition(this.owner);
    specialActionContinue.act_pos = this.player.actionPosition;
    specialActionContinue.act_pos_f = this.player.actionPositionFlag;
    specialActionContinue.isHitSpAttack = this.player.isHitSpAttack;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerSpecialActionContinue>(specialActionContinue);
    this.StackActionHistory((Coop_Model_ObjectBase) specialActionContinue, true);
  }

  public void OnSyncSpActionGauge()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSpecialActionGaugeSync model = new Coop_Model_PlayerSpecialActionGaugeSync();
    model.id = this.owner.id;
    model.weaponIndex = this.player.weaponIndex;
    model.currentSpActionGauge = this.player.CurrentWeaponSpActionGauge;
    model.comboLv = this.player.pairSwordsCtrl.GetComboLv();
    this.SendBroadcast<Coop_Model_PlayerSpecialActionGaugeSync>(model);
  }

  public void OnSyncEvolveAction(bool isAction)
  {
    Coop_Model_PlayerEvolveActionSync evolveActionSync = new Coop_Model_PlayerEvolveActionSync();
    evolveActionSync.id = this.owner.id;
    evolveActionSync.isAction = isAction;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerEvolveActionSync>(evolveActionSync);
    this.StackActionHistory((Coop_Model_ObjectBase) evolveActionSync, true);
  }

  public void OnEvolveSpecialAction()
  {
    Coop_Model_PlayerEvolveSpecialAction evolveSpecialAction = new Coop_Model_PlayerEvolveSpecialAction();
    evolveSpecialAction.id = this.owner.id;
    evolveSpecialAction.SetSyncPosition(this.owner);
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerEvolveSpecialAction>(evolveSpecialAction);
    this.StackActionHistory((Coop_Model_ObjectBase) evolveSpecialAction, true);
  }

  public void OnSyncSoulBoost(bool isBoost)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSoulBoost model = new Coop_Model_PlayerSoulBoost();
    model.id = this.owner.id;
    model.isBoost = isBoost;
    this.SendBroadcast<Coop_Model_PlayerSoulBoost>(model);
  }

  public void OnJumpRize(Vector3 dir, int level)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerJumpRize model = new Coop_Model_PlayerJumpRize();
    model.id = this.owner.id;
    model.dir = dir;
    model.level = level;
    this.SendBroadcast<Coop_Model_PlayerJumpRize>(model);
  }

  public void OnJumpEnd(Vector3 pos, bool isSuccess, float y)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerJumpEnd model = new Coop_Model_PlayerJumpEnd();
    model.id = this.owner.id;
    model.pos = pos;
    model.isSuccess = isSuccess;
    model.y = y;
    this.SendBroadcast<Coop_Model_PlayerJumpEnd>(model);
  }

  public void OnSnatch(int enemyId, Vector3 hitPoint)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSnatchPos model = new Coop_Model_PlayerSnatchPos();
    model.id = this.owner.id;
    model.enemyId = enemyId;
    model.hitPoint = hitPoint;
    this.SendBroadcast<Coop_Model_PlayerSnatchPos>(model);
  }

  public void OnSnatchMoveStart(Vector3 snatchPos)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSnatchMoveStart model = new Coop_Model_PlayerSnatchMoveStart();
    model.id = this.owner.id;
    model.snatchPos = snatchPos;
    this.SendBroadcast<Coop_Model_PlayerSnatchMoveStart>(model);
  }

  public void OnSnatchMoveEnd(int triggerIndex = 0)
  {
    if (!((Behaviour) this).enabled || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSnatchMoveEnd model = new Coop_Model_PlayerSnatchMoveEnd();
    model.id = this.owner.id;
    model.SetSyncPosition(this.owner);
    model.pos = this.owner._position;
    Coop_Model_PlayerSnatchMoveEnd playerSnatchMoveEnd = model;
    Quaternion rotation = this.owner._rotation;
    double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
    playerSnatchMoveEnd.dir = (float) y;
    model.act_pos = this.character.actionPosition;
    model.act_pos_f = this.character.actionPositionFlag;
    model.triggerIndex = triggerIndex;
    this.SendBroadcast<Coop_Model_PlayerSnatchMoveEnd>(model);
  }

  public void OnPairSwordsLaserEnd()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerPairSwordsLaserEnd model = new Coop_Model_PlayerPairSwordsLaserEnd();
    model.id = this.owner.id;
    model.weaponIndex = this.player.weaponIndex;
    model.currentSpActionGauge = this.player.CurrentWeaponSpActionGauge;
    this.SendBroadcast<Coop_Model_PlayerPairSwordsLaserEnd>(model);
  }

  public void OnShotHealingHoming(Coop_Model_PlayerShotHealingHoming model)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    this.SendBroadcast<Coop_Model_PlayerShotHealingHoming>(model);
  }

  public void OnShotResurrectionHoming(Coop_Model_PlayerShotResurrectionHoming model)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    this.SendBroadcast<Coop_Model_PlayerShotResurrectionHoming>(model);
  }

  public void OnShotShieldReflect(Coop_Model_PlayerShotShieldReflect model)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    this.SendBroadcast<Coop_Model_PlayerShotShieldReflect>(model);
  }

  public void OnSacrificedHp(int sacrificedHp)
  {
    if (sacrificedHp <= 0 || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSacrificedHp model = new Coop_Model_PlayerSacrificedHp();
    model.id = this.owner.id;
    model.sacrificedHp = sacrificedHp;
    this.SendBroadcast<Coop_Model_PlayerSacrificedHp>(model);
  }

  public void OnCreateWaveMatchDropObject(
    int managedId,
    uint dataId,
    Vector3 basePos,
    Vector3 offset,
    float sec)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_WaveMatchDropCreate model = new Coop_Model_WaveMatchDropCreate();
    model.id = this.owner.id;
    model.managedId = managedId;
    model.dataId = dataId;
    model.basePos = basePos;
    model.offset = offset;
    model.sec = sec;
    this.SendBroadcast<Coop_Model_WaveMatchDropCreate>(model);
  }

  public void OnPickedWaveMatchDropObject(int managedId, uint tableId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_WaveMatchDropPicked model = new Coop_Model_WaveMatchDropPicked();
    model.id = this.owner.id;
    model.managedId = managedId;
    model.tableId = tableId;
    this.SendBroadcast<Coop_Model_WaveMatchDropPicked>(model);
  }

  public void OnActGatherGimmick(int id)
  {
    Coop_Model_PlayerGatherGimmick playerGatherGimmick = new Coop_Model_PlayerGatherGimmick();
    playerGatherGimmick.id = this.owner.id;
    playerGatherGimmick.SetSyncPosition(this.owner);
    playerGatherGimmick.act_pos = this.character.actionPosition;
    playerGatherGimmick.act_pos_f = this.character.actionPositionFlag;
    playerGatherGimmick.gimmickId = id;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerGatherGimmick>(playerGatherGimmick);
    this.StackActionHistory((Coop_Model_ObjectBase) playerGatherGimmick, true);
  }

  public void OnGatherGimmickState(int state)
  {
    Coop_Model_PlayerGatherGimmickState gatherGimmickState = new Coop_Model_PlayerGatherGimmickState();
    gatherGimmickState.id = this.owner.id;
    gatherGimmickState.state = state;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerGatherGimmickState>(gatherGimmickState);
    this.StackActionHistory((Coop_Model_ObjectBase) gatherGimmickState, true);
  }

  public void OnGatherGimmickInfo(int managedId, bool isUsed)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_GatherGimmickInfo model = new Coop_Model_GatherGimmickInfo();
    model.id = this.owner.id;
    model.managedId = managedId;
    model.ownerId = this.owner.id;
    model.isUsed = isUsed;
    this.SendBroadcast<Coop_Model_GatherGimmickInfo>(model);
  }

  public void OnActQuestGimmick(int id)
  {
    Coop_Model_PlayerQuestGimmick playerQuestGimmick = new Coop_Model_PlayerQuestGimmick();
    playerQuestGimmick.id = this.owner.id;
    playerQuestGimmick.SetSyncPosition(this.owner);
    playerQuestGimmick.act_pos = this.character.actionPosition;
    playerQuestGimmick.act_pos_f = this.character.actionPositionFlag;
    playerQuestGimmick.gimmickId = id;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerQuestGimmick>(playerQuestGimmick);
    this.StackActionHistory((Coop_Model_ObjectBase) playerQuestGimmick, true);
  }

  public void OnCoopFishingGaugeIncrease(int toClientId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCoopFishingGaugeIncrease model = new Coop_Model_PlayerCoopFishingGaugeIncrease();
    model.id = this.owner.id;
    this.SendTo<Coop_Model_PlayerCoopFishingGaugeIncrease>(toClientId, model);
  }

  public void OnCoopFishingGaugeSync(int ownerUserId, float gaugeValue)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCoopFishingGaugeSync model = new Coop_Model_PlayerCoopFishingGaugeSync();
    model.id = this.owner.id;
    model.ownerUserId = ownerUserId;
    model.gaugeValue = gaugeValue;
    this.SendBroadcast<Coop_Model_PlayerCoopFishingGaugeSync>(model);
  }

  public void OnActCoopFishingStart(int id)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCoopFishingStart model = new Coop_Model_PlayerCoopFishingStart();
    model.id = this.owner.id;
    model.SetSyncPosition(this.owner);
    model.actPos = this.player.actionPosition;
    model.actPosFlag = this.player.actionPositionFlag;
    model.gimmickId = id;
    this.SendBroadcast<Coop_Model_PlayerCoopFishingStart>(model);
  }

  public void OnActFlickAction(Vector3 inputVec)
  {
    Coop_Model_PlayerFlickAction playerFlickAction = new Coop_Model_PlayerFlickAction();
    playerFlickAction.id = this.owner.id;
    playerFlickAction.SetSyncPosition(this.owner);
    playerFlickAction.inputVec = inputVec;
    if (this.enableSend && this.owner.IsOriginal())
      this.SendBroadcast<Coop_Model_PlayerFlickAction>(playerFlickAction);
    this.StackActionHistory((Coop_Model_ObjectBase) playerFlickAction, true);
  }

  public void OnSyncCombine(bool isCombine)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSyncCombine model = new Coop_Model_PlayerSyncCombine();
    model.id = this.owner.id;
    model.isCombine = isCombine;
    this.SendBroadcast<Coop_Model_PlayerSyncCombine>(model);
  }

  public void OnSyncSubstitute(int num)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerSyncSubstitute model = new Coop_Model_PlayerSyncSubstitute();
    model.id = this.owner.id;
    model.num = num;
    this.SendBroadcast<Coop_Model_PlayerSyncSubstitute>(model);
  }

  public void OnWeaponActionStart()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerWeaponActionStart model = new Coop_Model_PlayerWeaponActionStart();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerWeaponActionStart>(model);
  }

  public void OnWeaponActionEnd()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerWeaponActionEnd model = new Coop_Model_PlayerWeaponActionEnd();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerWeaponActionEnd>(model);
  }

  public void OnRainShotChargeRelease(Vector3 pos, float rotY)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerRainShotChargeRelease model = new Coop_Model_PlayerRainShotChargeRelease();
    model.id = this.owner.id;
    model.fallPos = pos;
    model.fallRotY = rotY;
    this.SendBroadcast<Coop_Model_PlayerRainShotChargeRelease>(model);
  }

  public void OnActCarry(InGameProgress.eFieldGimmick type, int pointId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCarry modelPlayerCarry = new Coop_Model_PlayerCarry();
    modelPlayerCarry.id = this.owner.id;
    modelPlayerCarry.SetSyncPosition(this.owner);
    modelPlayerCarry.type = type;
    modelPlayerCarry.pointId = pointId;
    this.SendBroadcast<Coop_Model_PlayerCarry>(modelPlayerCarry);
    this.StackActionHistory((Coop_Model_ObjectBase) modelPlayerCarry, true);
  }

  public void OnActCarryIdle()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCarryIdle model = new Coop_Model_PlayerCarryIdle();
    model.SetSyncPosition(this.owner);
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerCarryIdle>(model);
  }

  public void OnActCarryPut(int pointId)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerCarryPut model = new Coop_Model_PlayerCarryPut();
    model.id = this.owner.id;
    model.SetSyncPosition(this.owner);
    model.pointId = pointId;
    this.SendBroadcast<Coop_Model_PlayerCarryPut>(model);
  }

  public void OnSetHorizontalNextMotion(bool isFinish)
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerOracleHorizontalNextMotion model = new Coop_Model_PlayerOracleHorizontalNextMotion();
    model.id = this.owner.id;
    model.SetSyncPosition(this.owner);
    model.isFinish = isFinish;
    this.SendBroadcast<Coop_Model_PlayerOracleHorizontalNextMotion>(model);
  }

  public void OnUpdateOracleSpearStock()
  {
    if (!this.enableSend || !this.owner.IsOriginal())
      return;
    Coop_Model_PlayerOracleSpearStock model = new Coop_Model_PlayerOracleSpearStock();
    model.id = this.owner.id;
    this.SendBroadcast<Coop_Model_PlayerOracleSpearStock>(model);
  }
}
