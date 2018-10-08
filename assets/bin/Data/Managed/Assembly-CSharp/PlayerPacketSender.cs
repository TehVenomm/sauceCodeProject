using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPacketSender : CharacterPacketSender
{
	public const float CANNON_ROTATE_INTERVAL = 1f;

	protected float cannonRotateTimer;

	protected Player player => (Player)base.owner;

	public override void OnUpdate()
	{
		base.OnUpdate();
		if (base.character is Self)
		{
			Self self = base.character as Self;
			cannonRotateTimer += Time.deltaTime;
			if (self.IsOnCannonMode() && cannonRotateTimer >= 1f)
			{
				Coop_Model_PlayerCannonRotate coop_Model_PlayerCannonRotate = new Coop_Model_PlayerCannonRotate();
				coop_Model_PlayerCannonRotate.id = base.owner.id;
				coop_Model_PlayerCannonRotate.cannonVec = self.GetCannonVector();
				if (base.enableSend && base.owner.IsOriginal())
				{
					SendBroadcast(coop_Model_PlayerCannonRotate, false, null, null);
				}
				cannonRotateTimer = 0f;
			}
		}
	}

	public override void OnLoadComplete(bool promise = true)
	{
		if (base.enableSend && base.owner.IsPuppet())
		{
			Coop_Model_PlayerLoadComplete coop_Model_PlayerLoadComplete = new Coop_Model_PlayerLoadComplete();
			coop_Model_PlayerLoadComplete.id = base.owner.id;
			SendToExtra(base.owner.coopClientId, coop_Model_PlayerLoadComplete, promise, null, null);
		}
	}

	public override void OnRecvLoadComplete(int to_client_id)
	{
		SendInitialize(to_client_id);
	}

	public override void SendInitialize(int to_client_id = 0)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerInitialize model = new Coop_Model_PlayerInitialize();
			_SendInitialize(model, false);
			if (to_client_id == 0)
			{
				SendBroadcast(model, true, null, delegate(Coop_Model_Base send_model)
				{
					_SendInitialize(send_model as Coop_Model_PlayerInitialize, true);
					return true;
				});
			}
			else
			{
				SendToExtra(to_client_id, model, true, null, delegate(Coop_Model_Base send_model)
				{
					_SendInitialize(send_model as Coop_Model_PlayerInitialize, true);
					return true;
				});
			}
			SendActionHistory(to_client_id);
			player.SyncDeadCount();
		}
	}

	private void _SendInitialize(Coop_Model_PlayerInitialize model, bool resend)
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
		model.sid = player.id;
		model.hp = player.hp;
		model.healHp = player.healHp;
		model.target_id = ((!((Object)player.actionTarget != (Object)null)) ? (-1) : player.actionTarget.id);
		model.stopcounter = player.isStopCounter;
		model.act_battle_start = false;
		if (player.actionID == (Character.ACTION_ID)22)
		{
			model.act_battle_start = true;
		}
		else if (player.actionID == Character.ACTION_ID.IDLE && player.lastActionID == (Character.ACTION_ID)22)
		{
			model.act_battle_start = true;
		}
		else if (!player.isActedBattleStart)
		{
			model.act_battle_start = true;
		}
		model.weapon_item = player.weaponData;
		model.weapon_index = player.weaponIndex;
		model.buff_sync_param = player.buffParam.CreateSyncParam(BuffParam.BUFFTYPE.NONE);
		if (player.targetFieldGimmickCannon != null)
		{
			model.cannonId = player.targetFieldGimmickCannon.GetId();
		}
		model.bulletIndex = player.bulletIndex;
	}

	public void OnSyncPosition()
	{
		Coop_Model_PlayerSyncPosition coop_Model_PlayerSyncPosition = new Coop_Model_PlayerSyncPosition();
		coop_Model_PlayerSyncPosition.id = base.owner.id;
		coop_Model_PlayerSyncPosition.SetSyncPosition(base.owner);
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerSyncPosition, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerSyncPosition, false);
	}

	public void OnActAttackCombo(int id, string _motionLayerName)
	{
		Coop_Model_PlayerAttackCombo coop_Model_PlayerAttackCombo = new Coop_Model_PlayerAttackCombo();
		coop_Model_PlayerAttackCombo.id = base.owner.id;
		coop_Model_PlayerAttackCombo.pos = base.owner._position;
		Coop_Model_PlayerAttackCombo coop_Model_PlayerAttackCombo2 = coop_Model_PlayerAttackCombo;
		Vector3 eulerAngles = base.owner._rotation.eulerAngles;
		coop_Model_PlayerAttackCombo2.dir = eulerAngles.y;
		coop_Model_PlayerAttackCombo.attack_id = id;
		coop_Model_PlayerAttackCombo.motionLayerName = _motionLayerName;
		coop_Model_PlayerAttackCombo.act_pos = base.character.actionPosition;
		coop_Model_PlayerAttackCombo.act_pos_f = base.character.actionPositionFlag;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerAttackCombo, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerAttackCombo, true);
	}

	public void OnSetChargeRelease(float charge_rate, bool isChargeExRush)
	{
		Coop_Model_PlayerChargeRelease coop_Model_PlayerChargeRelease = new Coop_Model_PlayerChargeRelease();
		coop_Model_PlayerChargeRelease.id = base.owner.id;
		coop_Model_PlayerChargeRelease.SetSyncPosition(base.owner);
		if (player.lerpRotateVec == Vector3.zero)
		{
			coop_Model_PlayerChargeRelease.lerp_dir = coop_Model_PlayerChargeRelease.dir;
		}
		else
		{
			Coop_Model_PlayerChargeRelease coop_Model_PlayerChargeRelease2 = coop_Model_PlayerChargeRelease;
			Vector3 eulerAngles = Quaternion.LookRotation(player.lerpRotateVec).eulerAngles;
			coop_Model_PlayerChargeRelease2.lerp_dir = eulerAngles.y;
		}
		coop_Model_PlayerChargeRelease.charge_rate = charge_rate;
		coop_Model_PlayerChargeRelease.act_pos = base.character.actionPosition;
		coop_Model_PlayerChargeRelease.act_pos_f = base.character.actionPositionFlag;
		coop_Model_PlayerChargeRelease.isExRushCharge = isChargeExRush;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerChargeRelease, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerChargeRelease, false);
	}

	public void OnSetChargeExpandRelease(float chargeRate)
	{
		Coop_Model_PlayerChargeExpandRelease coop_Model_PlayerChargeExpandRelease = new Coop_Model_PlayerChargeExpandRelease();
		coop_Model_PlayerChargeExpandRelease.id = base.owner.id;
		coop_Model_PlayerChargeExpandRelease.SetSyncPosition(base.owner);
		if (player.lerpRotateVec == Vector3.zero)
		{
			coop_Model_PlayerChargeExpandRelease.lerp_dir = coop_Model_PlayerChargeExpandRelease.dir;
		}
		else
		{
			Coop_Model_PlayerChargeExpandRelease coop_Model_PlayerChargeExpandRelease2 = coop_Model_PlayerChargeExpandRelease;
			Vector3 eulerAngles = Quaternion.LookRotation(player.lerpRotateVec).eulerAngles;
			coop_Model_PlayerChargeExpandRelease2.lerp_dir = eulerAngles.y;
		}
		coop_Model_PlayerChargeExpandRelease.charge_rate = chargeRate;
		coop_Model_PlayerChargeExpandRelease.act_pos = base.character.actionPosition;
		coop_Model_PlayerChargeExpandRelease.act_pos_f = base.character.actionPositionFlag;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerChargeExpandRelease, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerChargeExpandRelease, false);
	}

	public void OnRestraintStart(RestraintInfo restInfo)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerRestraint coop_Model_PlayerRestraint = new Coop_Model_PlayerRestraint();
			coop_Model_PlayerRestraint.id = base.owner.id;
			coop_Model_PlayerRestraint.duration = restInfo.duration;
			coop_Model_PlayerRestraint.damageInterval = restInfo.damageInterval;
			coop_Model_PlayerRestraint.damageRate = restInfo.damageRate;
			coop_Model_PlayerRestraint.reduceTimeByFlick = restInfo.reduceTimeByFlick;
			coop_Model_PlayerRestraint.effectName = restInfo.effectName;
			coop_Model_PlayerRestraint.isStopMotion = restInfo.isStopMotion;
			coop_Model_PlayerRestraint.isDisableRemoveByPlayerAttack = restInfo.isDisableRemoveByPlayerAttack;
			SendBroadcast(coop_Model_PlayerRestraint, false, null, null);
		}
	}

	public void OnRestraintEnd()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerRestraintEnd coop_Model_PlayerRestraintEnd = new Coop_Model_PlayerRestraintEnd();
			coop_Model_PlayerRestraintEnd.id = base.owner.id;
			SendBroadcast(coop_Model_PlayerRestraintEnd, false, null, null);
		}
	}

	public void OnActAvoid()
	{
		Coop_Model_PlayerAvoid coop_Model_PlayerAvoid = new Coop_Model_PlayerAvoid();
		coop_Model_PlayerAvoid.id = base.owner.id;
		coop_Model_PlayerAvoid.SetSyncPosition(base.owner);
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerAvoid, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerAvoid, true);
	}

	public void OnWarp()
	{
		Coop_Model_PlayerWarp coop_Model_PlayerWarp = new Coop_Model_PlayerWarp();
		coop_Model_PlayerWarp.id = base.owner.id;
		coop_Model_PlayerWarp.SetSyncPosition(base.owner);
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerWarp, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerWarp, true);
	}

	public void OnInputBlowClear()
	{
		Coop_Model_PlayerBlowClear coop_Model_PlayerBlowClear = new Coop_Model_PlayerBlowClear();
		coop_Model_PlayerBlowClear.id = base.owner.id;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerBlowClear, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerBlowClear, false);
	}

	public void OnSetStunnedEnd()
	{
		Coop_Model_PlayerStunnedEnd coop_Model_PlayerStunnedEnd = new Coop_Model_PlayerStunnedEnd();
		coop_Model_PlayerStunnedEnd.id = base.owner.id;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerStunnedEnd, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerStunnedEnd, false);
	}

	public void OnDeadCount(float time, bool stop)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerDeadCount coop_Model_PlayerDeadCount = new Coop_Model_PlayerDeadCount();
			coop_Model_PlayerDeadCount.id = base.owner.id;
			coop_Model_PlayerDeadCount.remaind_time = time;
			coop_Model_PlayerDeadCount.stop = stop;
			SendBroadcast(coop_Model_PlayerDeadCount, false, null, null);
		}
	}

	public void SendDeadCountRequest(int stageObjectId)
	{
		if (base.enableSend)
		{
			Coop_Model_PlayerDeadCountRequest coop_Model_PlayerDeadCountRequest = new Coop_Model_PlayerDeadCountRequest();
			coop_Model_PlayerDeadCountRequest.id = stageObjectId;
			SendBroadcast(coop_Model_PlayerDeadCountRequest, false, null, null);
		}
	}

	public void OnDeadCountRequest(int toClientId, Player deadPlayer)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			StartCoroutine(_OnDeadCountRequest(toClientId, deadPlayer));
		}
	}

	private IEnumerator _OnDeadCountRequest(int toClientId, Player deadPlayer)
	{
		yield return (object)new WaitForSeconds(1.5f);
		if (deadPlayer.rescueTime > 0f && !deadPlayer.IsPrayed())
		{
			SendTo(toClientId, new Coop_Model_PlayerDeadCount
			{
				id = deadPlayer.id,
				remaind_time = deadPlayer.rescueTime,
				requested = true,
				stop = false
			}, false, null, null);
		}
	}

	public void OnStopCounter(bool stop)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerStopCounter coop_Model_PlayerStopCounter = new Coop_Model_PlayerStopCounter();
			coop_Model_PlayerStopCounter.id = base.owner.id;
			coop_Model_PlayerStopCounter.stop = stop;
			SendBroadcast(coop_Model_PlayerStopCounter, false, null, null);
		}
	}

	public void OnActDeadStandup(int standup_hp, Player.eContinueType cType)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerDeadStandup coop_Model_PlayerDeadStandup = new Coop_Model_PlayerDeadStandup();
			coop_Model_PlayerDeadStandup.id = base.owner.id;
			coop_Model_PlayerDeadStandup.standupHp = standup_hp;
			coop_Model_PlayerDeadStandup.cType = cType;
			SendBroadcast(coop_Model_PlayerDeadStandup, true, null, delegate
			{
				if ((Object)player == (Object)null)
				{
					return false;
				}
				if (player.hp == 0)
				{
					return false;
				}
				return true;
			});
		}
	}

	public void OnPrayerStart(int prayer_id)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerPrayerStart coop_Model_PlayerPrayerStart = new Coop_Model_PlayerPrayerStart();
			coop_Model_PlayerPrayerStart.id = base.owner.id;
			coop_Model_PlayerPrayerStart.sid = prayer_id;
			SendBroadcast(coop_Model_PlayerPrayerStart, false, null, null);
		}
	}

	public void OnPrayerEnd(int prayer_id)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerPrayerEnd coop_Model_PlayerPrayerEnd = new Coop_Model_PlayerPrayerEnd();
			coop_Model_PlayerPrayerEnd.id = base.owner.id;
			coop_Model_PlayerPrayerEnd.sid = prayer_id;
			SendBroadcast(coop_Model_PlayerPrayerEnd, false, null, null);
		}
	}

	public void OnChangePrayBoost(int prayedId, Player.BoostPrayInfo boostPrayInfo)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerPrayerBoost coop_Model_PlayerPrayerBoost = new Coop_Model_PlayerPrayerBoost();
			coop_Model_PlayerPrayerBoost.id = base.owner.id;
			coop_Model_PlayerPrayerBoost.sid = prayedId;
			coop_Model_PlayerPrayerBoost.boostPrayInfo = boostPrayInfo;
			SendBroadcast(coop_Model_PlayerPrayerBoost, false, null, null);
		}
	}

	public void OnChangeWeapon()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerChangeWeapon coop_Model_PlayerChangeWeapon = new Coop_Model_PlayerChangeWeapon();
			coop_Model_PlayerChangeWeapon.id = base.owner.id;
			SendBroadcast(coop_Model_PlayerChangeWeapon, false, null, null);
		}
	}

	public void OnApplyChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerApplyChangeWeapon coop_Model_PlayerApplyChangeWeapon = new Coop_Model_PlayerApplyChangeWeapon();
			coop_Model_PlayerApplyChangeWeapon.id = base.owner.id;
			coop_Model_PlayerApplyChangeWeapon.item = item;
			coop_Model_PlayerApplyChangeWeapon.index = weapon_index;
			SendBroadcast(coop_Model_PlayerApplyChangeWeapon, true, null, delegate(Coop_Model_Base send_model)
			{
				if ((Object)player == (Object)null)
				{
					return false;
				}
				Coop_Model_PlayerApplyChangeWeapon coop_Model_PlayerApplyChangeWeapon2 = send_model as Coop_Model_PlayerApplyChangeWeapon;
				if (coop_Model_PlayerApplyChangeWeapon2.item.eId != player.weaponData.eId || coop_Model_PlayerApplyChangeWeapon2.index != player.weaponIndex)
				{
					return false;
				}
				return true;
			});
		}
		ClearActionHistory();
	}

	public void OnActGather(GatherPointObject gather_point)
	{
		Coop_Model_PlayerGather coop_Model_PlayerGather = new Coop_Model_PlayerGather();
		coop_Model_PlayerGather.id = base.owner.id;
		coop_Model_PlayerGather.SetSyncPosition(base.owner);
		coop_Model_PlayerGather.act_pos = base.character.actionPosition;
		coop_Model_PlayerGather.act_pos_f = base.character.actionPositionFlag;
		coop_Model_PlayerGather.point_id = (int)gather_point.pointData.pointID;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerGather, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerGather, true);
	}

	public void OnActSkillAction(int skill_index, SkillInfo.SkillParam skill_param)
	{
		Coop_Model_PlayerSkillAction coop_Model_PlayerSkillAction = new Coop_Model_PlayerSkillAction();
		coop_Model_PlayerSkillAction.id = base.owner.id;
		coop_Model_PlayerSkillAction.SetSyncPosition(base.owner);
		coop_Model_PlayerSkillAction.act_pos = base.character.actionPosition;
		coop_Model_PlayerSkillAction.act_pos_f = base.character.actionPositionFlag;
		coop_Model_PlayerSkillAction.skill_index = skill_index;
		coop_Model_PlayerSkillAction.isUsingSecondGrade = skill_param.isUsingSecondGrade;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerSkillAction, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerSkillAction, true);
	}

	public void OnHealReceive(Character.HealData healData)
	{
		if (base.enableSend && base.owner.IsPuppet())
		{
			Coop_Model_PlayerGetHeal coop_Model_PlayerGetHeal = new Coop_Model_PlayerGetHeal();
			coop_Model_PlayerGetHeal.Serialize(base.owner.id, healData, true);
			SendTo(base.owner.coopClientId, coop_Model_PlayerGetHeal, false, null, null);
		}
	}

	public void OnGetHeal(Character.HealData healData)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerGetHeal coop_Model_PlayerGetHeal = new Coop_Model_PlayerGetHeal();
			coop_Model_PlayerGetHeal.Serialize(base.owner.id, healData, false);
			SendBroadcast(coop_Model_PlayerGetHeal, false, null, null);
		}
	}

	public void OnChargeSkillGaugeReceive(BuffParam.BUFFTYPE buffType, int buffValue, int useSkillIndex)
	{
		if (base.enableSend && base.owner.IsPuppet())
		{
			Coop_Model_PlayerGetChargeSkillGauge coop_Model_PlayerGetChargeSkillGauge = new Coop_Model_PlayerGetChargeSkillGauge();
			coop_Model_PlayerGetChargeSkillGauge.id = base.owner.id;
			coop_Model_PlayerGetChargeSkillGauge.buffType = (int)buffType;
			coop_Model_PlayerGetChargeSkillGauge.buffValue = buffValue;
			coop_Model_PlayerGetChargeSkillGauge.useSkillIndex = useSkillIndex;
			coop_Model_PlayerGetChargeSkillGauge.receive = true;
			SendTo(base.owner.coopClientId, coop_Model_PlayerGetChargeSkillGauge, false, null, null);
		}
	}

	public void OnGetChargeSkillGauge(BuffParam.BUFFTYPE buffType, int buffValue, int useSkillIndex, bool isCorrectWaveMatch)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerGetChargeSkillGauge coop_Model_PlayerGetChargeSkillGauge = new Coop_Model_PlayerGetChargeSkillGauge();
			coop_Model_PlayerGetChargeSkillGauge.id = base.owner.id;
			coop_Model_PlayerGetChargeSkillGauge.buffType = (int)buffType;
			coop_Model_PlayerGetChargeSkillGauge.buffValue = buffValue;
			coop_Model_PlayerGetChargeSkillGauge.useSkillIndex = useSkillIndex;
			coop_Model_PlayerGetChargeSkillGauge.receive = false;
			coop_Model_PlayerGetChargeSkillGauge.isCorrectWaveMatch = isCorrectWaveMatch;
			SendBroadcast(coop_Model_PlayerGetChargeSkillGauge, false, null, null);
		}
	}

	public void OnResurrectionReceive()
	{
		if (base.enableSend && base.owner.IsPuppet())
		{
			Coop_Model_PlayerResurrect coop_Model_PlayerResurrect = new Coop_Model_PlayerResurrect();
			coop_Model_PlayerResurrect.id = base.owner.id;
			SendTo(base.owner.coopClientId, coop_Model_PlayerResurrect, false, null, null);
		}
	}

	public void OnGetResurrection()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerGetResurrect coop_Model_PlayerGetResurrect = new Coop_Model_PlayerGetResurrect();
			coop_Model_PlayerGetResurrect.id = base.owner.id;
			SendBroadcast(coop_Model_PlayerGetResurrect, false, null, null);
		}
	}

	public void OnActSpecialAction(bool start_effect, bool isSuccess)
	{
		Coop_Model_PlayerSpecialAction coop_Model_PlayerSpecialAction = new Coop_Model_PlayerSpecialAction();
		coop_Model_PlayerSpecialAction.id = base.owner.id;
		coop_Model_PlayerSpecialAction.SetSyncPosition(base.owner);
		coop_Model_PlayerSpecialAction.act_pos = base.character.actionPosition;
		coop_Model_PlayerSpecialAction.act_pos_f = base.character.actionPositionFlag;
		coop_Model_PlayerSpecialAction.start_effect = start_effect;
		coop_Model_PlayerSpecialAction.isSuccess = isSuccess;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerSpecialAction, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerSpecialAction, true);
	}

	public void OnUpdateSkillInfo()
	{
		Coop_Model_PlayerUpdateSkillInfo coop_Model_PlayerUpdateSkillInfo = new Coop_Model_PlayerUpdateSkillInfo();
		coop_Model_PlayerUpdateSkillInfo.id = base.owner.id;
		SkillInfo.SkillSettingsInfo skillSettingsInfo = new SkillInfo.SkillSettingsInfo();
		int i = 0;
		for (int num = 9; i < num; i++)
		{
			SkillInfo.SkillParam skillParam = player.skillInfo.GetSkillParam(i);
			SkillInfo.SkillSettingsInfo.Element element = new SkillInfo.SkillSettingsInfo.Element();
			if (skillParam != null)
			{
				element.baseInfo = skillParam.baseInfo;
				element.useGaugeCounter = skillParam.useGaugeCounter;
			}
			skillSettingsInfo.elementList.Add(element);
		}
		coop_Model_PlayerUpdateSkillInfo.settings_info = skillSettingsInfo;
	}

	public void OnShotArrow(Vector3 shot_pos, Quaternion shot_rot, AttackInfo attack_info, bool is_sit_shot, bool is_aim_end)
	{
		Coop_Model_PlayerShotArrow coop_Model_PlayerShotArrow = new Coop_Model_PlayerShotArrow();
		coop_Model_PlayerShotArrow.id = base.owner.id;
		coop_Model_PlayerShotArrow.SetSyncPosition(base.owner);
		coop_Model_PlayerShotArrow.shot_pos = shot_pos;
		coop_Model_PlayerShotArrow.shot_rot = shot_rot;
		coop_Model_PlayerShotArrow.attack_name = attack_info.name;
		coop_Model_PlayerShotArrow.attack_rate = attack_info.rateInfoRate;
		coop_Model_PlayerShotArrow.shot_count = player.shotArrowCount;
		coop_Model_PlayerShotArrow.is_sit_shot = is_sit_shot;
		coop_Model_PlayerShotArrow.is_aim_end = is_aim_end;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerShotArrow, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerShotArrow, false);
	}

	public void OnShotSoulArrow(Vector3 shotPos, Quaternion bowRot, List<Vector3> targetPosList)
	{
		Coop_Model_PlayerShotSoulArrow coop_Model_PlayerShotSoulArrow = new Coop_Model_PlayerShotSoulArrow();
		coop_Model_PlayerShotSoulArrow.id = base.owner.id;
		coop_Model_PlayerShotSoulArrow.SetSyncPosition(base.owner);
		coop_Model_PlayerShotSoulArrow.shotPos = shotPos;
		coop_Model_PlayerShotSoulArrow.bowRot = bowRot;
		coop_Model_PlayerShotSoulArrow.targetPosList = targetPosList;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerShotSoulArrow, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerShotSoulArrow, false);
	}

	public void OnSetPlayerStatus(int level, int atk, int def, int hp)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSetStatus coop_Model_PlayerSetStatus = new Coop_Model_PlayerSetStatus();
			coop_Model_PlayerSetStatus.id = base.owner.id;
			coop_Model_PlayerSetStatus.level = level;
			coop_Model_PlayerSetStatus.atk = atk;
			coop_Model_PlayerSetStatus.def = def;
			coop_Model_PlayerSetStatus.hp = hp;
			SendBroadcast(coop_Model_PlayerSetStatus, false, null, null);
		}
	}

	public void OnGetRareDrop(REWARD_TYPE type, int item_id)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerGetRareDrop coop_Model_PlayerGetRareDrop = new Coop_Model_PlayerGetRareDrop();
			coop_Model_PlayerGetRareDrop.id = base.owner.id;
			coop_Model_PlayerGetRareDrop.type = (int)type;
			coop_Model_PlayerGetRareDrop.item_id = item_id;
			SendBroadcast(coop_Model_PlayerGetRareDrop, false, null, null);
		}
	}

	public void OnGrabbedStart(int enemyId, string nodeName, float duration, int drainAtkId)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerGrabbed coop_Model_PlayerGrabbed = new Coop_Model_PlayerGrabbed();
			coop_Model_PlayerGrabbed.id = base.owner.id;
			coop_Model_PlayerGrabbed.enemyId = enemyId;
			coop_Model_PlayerGrabbed.nodeName = nodeName;
			coop_Model_PlayerGrabbed.duration = duration;
			coop_Model_PlayerGrabbed.drainAtkId = drainAtkId;
			SendBroadcast(coop_Model_PlayerGrabbed, false, null, null);
		}
	}

	public void OnGrabbedEnd()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerGrabbedEnd coop_Model_PlayerGrabbedEnd = new Coop_Model_PlayerGrabbedEnd();
			coop_Model_PlayerGrabbedEnd.id = base.owner.id;
			SendBroadcast(coop_Model_PlayerGrabbedEnd, false, null, null);
		}
	}

	public void OnSetPresentBullet(int presentBulletId, BulletData.BulletPresent.TYPE type, Vector3 position, string bulletName)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSetPresentBullet coop_Model_PlayerSetPresentBullet = new Coop_Model_PlayerSetPresentBullet();
			coop_Model_PlayerSetPresentBullet.id = base.owner.id;
			coop_Model_PlayerSetPresentBullet.presentBulletId = presentBulletId;
			coop_Model_PlayerSetPresentBullet.type = (int)type;
			coop_Model_PlayerSetPresentBullet.position = position;
			coop_Model_PlayerSetPresentBullet.bulletName = bulletName;
			SendBroadcast(coop_Model_PlayerSetPresentBullet, false, null, null);
		}
	}

	public void OnPickPresentBullet(int presentBulletId)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerPickPresentBullet coop_Model_PlayerPickPresentBullet = new Coop_Model_PlayerPickPresentBullet();
			coop_Model_PlayerPickPresentBullet.id = base.owner.id;
			coop_Model_PlayerPickPresentBullet.presentBulletId = presentBulletId;
			SendBroadcast(coop_Model_PlayerPickPresentBullet, false, null, null);
		}
	}

	public void OnShotZoneBullet(string bulletName, Vector3 position)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerShotZoneBullet coop_Model_PlayerShotZoneBullet = new Coop_Model_PlayerShotZoneBullet();
			coop_Model_PlayerShotZoneBullet.id = base.owner.id;
			coop_Model_PlayerShotZoneBullet.bulletName = bulletName;
			coop_Model_PlayerShotZoneBullet.position = position;
			SendBroadcast(coop_Model_PlayerShotZoneBullet, false, null, null);
		}
	}

	public void OnShotDecoyBullet(int skIndex, int decoyId, string bulletName, Vector3 position)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerShotDecoyBullet coop_Model_PlayerShotDecoyBullet = new Coop_Model_PlayerShotDecoyBullet();
			coop_Model_PlayerShotDecoyBullet.id = base.owner.id;
			coop_Model_PlayerShotDecoyBullet.skIndex = skIndex;
			coop_Model_PlayerShotDecoyBullet.decoyId = decoyId;
			coop_Model_PlayerShotDecoyBullet.bulletName = bulletName;
			coop_Model_PlayerShotDecoyBullet.position = position;
			SendBroadcast(coop_Model_PlayerShotDecoyBullet, false, null, null);
		}
	}

	public void OnExplodeDecoyBullet(int decoyId)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerExplodeDecoyBullet coop_Model_PlayerExplodeDecoyBullet = new Coop_Model_PlayerExplodeDecoyBullet();
			coop_Model_PlayerExplodeDecoyBullet.id = base.owner.id;
			coop_Model_PlayerExplodeDecoyBullet.decoyId = decoyId;
			SendBroadcast(coop_Model_PlayerExplodeDecoyBullet, false, null, null);
		}
	}

	public void OnCannonStandby(int cannonId)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerCannonStandby coop_Model_PlayerCannonStandby = new Coop_Model_PlayerCannonStandby();
			coop_Model_PlayerCannonStandby.id = base.owner.id;
			coop_Model_PlayerCannonStandby.cannonId = cannonId;
			coop_Model_PlayerCannonStandby.SetSyncPosition(base.owner);
			SendBroadcast(coop_Model_PlayerCannonStandby, false, null, null);
		}
	}

	public void OnCannonShot()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerCannonShot coop_Model_PlayerCannonShot = new Coop_Model_PlayerCannonShot();
			coop_Model_PlayerCannonShot.id = base.owner.id;
			coop_Model_PlayerCannonShot.SetSyncPosition(base.owner);
			coop_Model_PlayerCannonShot.cannonVec = player.GetCannonVector();
			SendBroadcast(coop_Model_PlayerCannonShot, false, null, null);
		}
	}

	public void OnActSpAttackContinue()
	{
		Coop_Model_PlayerSpecialActionContinue coop_Model_PlayerSpecialActionContinue = new Coop_Model_PlayerSpecialActionContinue();
		coop_Model_PlayerSpecialActionContinue.id = base.owner.id;
		coop_Model_PlayerSpecialActionContinue.SetSyncPosition(base.owner);
		coop_Model_PlayerSpecialActionContinue.act_pos = player.actionPosition;
		coop_Model_PlayerSpecialActionContinue.act_pos_f = player.actionPositionFlag;
		coop_Model_PlayerSpecialActionContinue.isHitSpAttack = player.isHitSpAttack;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerSpecialActionContinue, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerSpecialActionContinue, true);
	}

	public void OnSyncSpActionGauge()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSpecialActionGaugeSync coop_Model_PlayerSpecialActionGaugeSync = new Coop_Model_PlayerSpecialActionGaugeSync();
			coop_Model_PlayerSpecialActionGaugeSync.id = base.owner.id;
			coop_Model_PlayerSpecialActionGaugeSync.weaponIndex = player.weaponIndex;
			coop_Model_PlayerSpecialActionGaugeSync.currentSpActionGauge = player.CurrentWeaponSpActionGauge;
			coop_Model_PlayerSpecialActionGaugeSync.comboLv = player.pairSwordsCtrl.GetComboLv();
			SendBroadcast(coop_Model_PlayerSpecialActionGaugeSync, false, null, null);
		}
	}

	public void OnSyncEvolveAction(bool isAction)
	{
		Coop_Model_PlayerEvolveActionSync coop_Model_PlayerEvolveActionSync = new Coop_Model_PlayerEvolveActionSync();
		coop_Model_PlayerEvolveActionSync.id = base.owner.id;
		coop_Model_PlayerEvolveActionSync.isAction = isAction;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerEvolveActionSync, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerEvolveActionSync, true);
	}

	public void OnEvolveSpecialAction()
	{
		Coop_Model_PlayerEvolveSpecialAction coop_Model_PlayerEvolveSpecialAction = new Coop_Model_PlayerEvolveSpecialAction();
		coop_Model_PlayerEvolveSpecialAction.id = base.owner.id;
		coop_Model_PlayerEvolveSpecialAction.SetSyncPosition(base.owner);
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerEvolveSpecialAction, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerEvolveSpecialAction, true);
	}

	public void OnSyncSoulBoost(bool isBoost)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSoulBoost coop_Model_PlayerSoulBoost = new Coop_Model_PlayerSoulBoost();
			coop_Model_PlayerSoulBoost.id = base.owner.id;
			coop_Model_PlayerSoulBoost.isBoost = isBoost;
			SendBroadcast(coop_Model_PlayerSoulBoost, false, null, null);
		}
	}

	public void OnJumpRize(Vector3 dir, int level)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerJumpRize coop_Model_PlayerJumpRize = new Coop_Model_PlayerJumpRize();
			coop_Model_PlayerJumpRize.id = base.owner.id;
			coop_Model_PlayerJumpRize.dir = dir;
			coop_Model_PlayerJumpRize.level = level;
			SendBroadcast(coop_Model_PlayerJumpRize, false, null, null);
		}
	}

	public void OnJumpEnd(Vector3 pos, bool isSuccess, float y)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerJumpEnd coop_Model_PlayerJumpEnd = new Coop_Model_PlayerJumpEnd();
			coop_Model_PlayerJumpEnd.id = base.owner.id;
			coop_Model_PlayerJumpEnd.pos = pos;
			coop_Model_PlayerJumpEnd.isSuccess = isSuccess;
			coop_Model_PlayerJumpEnd.y = y;
			SendBroadcast(coop_Model_PlayerJumpEnd, false, null, null);
		}
	}

	public void OnSnatch(int enemyId, Vector3 hitPoint)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSnatchPos coop_Model_PlayerSnatchPos = new Coop_Model_PlayerSnatchPos();
			coop_Model_PlayerSnatchPos.id = base.owner.id;
			coop_Model_PlayerSnatchPos.enemyId = enemyId;
			coop_Model_PlayerSnatchPos.hitPoint = hitPoint;
			SendBroadcast(coop_Model_PlayerSnatchPos, false, null, null);
		}
	}

	public void OnSnatchMoveStart(Vector3 snatchPos)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSnatchMoveStart coop_Model_PlayerSnatchMoveStart = new Coop_Model_PlayerSnatchMoveStart();
			coop_Model_PlayerSnatchMoveStart.id = base.owner.id;
			coop_Model_PlayerSnatchMoveStart.snatchPos = snatchPos;
			SendBroadcast(coop_Model_PlayerSnatchMoveStart, false, null, null);
		}
	}

	public void OnSnatchMoveEnd(int triggerIndex = 0)
	{
		if (base.enabled && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSnatchMoveEnd coop_Model_PlayerSnatchMoveEnd = new Coop_Model_PlayerSnatchMoveEnd();
			coop_Model_PlayerSnatchMoveEnd.id = base.owner.id;
			coop_Model_PlayerSnatchMoveEnd.SetSyncPosition(base.owner);
			coop_Model_PlayerSnatchMoveEnd.pos = base.owner._position;
			Coop_Model_PlayerSnatchMoveEnd coop_Model_PlayerSnatchMoveEnd2 = coop_Model_PlayerSnatchMoveEnd;
			Vector3 eulerAngles = base.owner._rotation.eulerAngles;
			coop_Model_PlayerSnatchMoveEnd2.dir = eulerAngles.y;
			coop_Model_PlayerSnatchMoveEnd.act_pos = base.character.actionPosition;
			coop_Model_PlayerSnatchMoveEnd.act_pos_f = base.character.actionPositionFlag;
			coop_Model_PlayerSnatchMoveEnd.triggerIndex = triggerIndex;
			SendBroadcast(coop_Model_PlayerSnatchMoveEnd, false, null, null);
		}
	}

	public void OnPairSwordsLaserEnd()
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_PlayerPairSwordsLaserEnd coop_Model_PlayerPairSwordsLaserEnd = new Coop_Model_PlayerPairSwordsLaserEnd();
			coop_Model_PlayerPairSwordsLaserEnd.id = base.owner.id;
			coop_Model_PlayerPairSwordsLaserEnd.weaponIndex = player.weaponIndex;
			coop_Model_PlayerPairSwordsLaserEnd.currentSpActionGauge = player.CurrentWeaponSpActionGauge;
			SendBroadcast(coop_Model_PlayerPairSwordsLaserEnd, false, null, null);
		}
	}

	public void OnShotHealingHoming(Coop_Model_PlayerShotHealingHoming model)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(model, false, null, null);
		}
	}

	public void OnSacrificedHp(int sacrificedHp)
	{
		if (sacrificedHp > 0 && base.owner.IsOriginal())
		{
			Coop_Model_PlayerSacrificedHp coop_Model_PlayerSacrificedHp = new Coop_Model_PlayerSacrificedHp();
			coop_Model_PlayerSacrificedHp.id = base.owner.id;
			coop_Model_PlayerSacrificedHp.sacrificedHp = sacrificedHp;
			SendBroadcast(coop_Model_PlayerSacrificedHp, false, null, null);
		}
	}

	public void OnCreateWaveMatchDropObject(int managedId, uint dataId, Vector3 basePos, Vector3 offset, float sec)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_WaveMatchDropCreate coop_Model_WaveMatchDropCreate = new Coop_Model_WaveMatchDropCreate();
			coop_Model_WaveMatchDropCreate.id = base.owner.id;
			coop_Model_WaveMatchDropCreate.managedId = managedId;
			coop_Model_WaveMatchDropCreate.dataId = dataId;
			coop_Model_WaveMatchDropCreate.basePos = basePos;
			coop_Model_WaveMatchDropCreate.offset = offset;
			coop_Model_WaveMatchDropCreate.sec = sec;
			SendBroadcast(coop_Model_WaveMatchDropCreate, false, null, null);
		}
	}

	public void OnPickedWaveMatchDropObject(int managedId, uint tableId)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_WaveMatchDropPicked coop_Model_WaveMatchDropPicked = new Coop_Model_WaveMatchDropPicked();
			coop_Model_WaveMatchDropPicked.id = base.owner.id;
			coop_Model_WaveMatchDropPicked.managedId = managedId;
			coop_Model_WaveMatchDropPicked.tableId = tableId;
			SendBroadcast(coop_Model_WaveMatchDropPicked, false, null, null);
		}
	}

	public void OnActGatherGimmick(int id)
	{
		Coop_Model_PlayerGatherGimmick coop_Model_PlayerGatherGimmick = new Coop_Model_PlayerGatherGimmick();
		coop_Model_PlayerGatherGimmick.id = base.owner.id;
		coop_Model_PlayerGatherGimmick.SetSyncPosition(base.owner);
		coop_Model_PlayerGatherGimmick.act_pos = base.character.actionPosition;
		coop_Model_PlayerGatherGimmick.act_pos_f = base.character.actionPositionFlag;
		coop_Model_PlayerGatherGimmick.gimmickId = id;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerGatherGimmick, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerGatherGimmick, true);
	}

	public void OnGatherGimmickState(int state)
	{
		Coop_Model_PlayerGatherGimmickState coop_Model_PlayerGatherGimmickState = new Coop_Model_PlayerGatherGimmickState();
		coop_Model_PlayerGatherGimmickState.id = base.owner.id;
		coop_Model_PlayerGatherGimmickState.state = state;
		if (base.enableSend && base.owner.IsOriginal())
		{
			SendBroadcast(coop_Model_PlayerGatherGimmickState, false, null, null);
		}
		StackActionHistory(coop_Model_PlayerGatherGimmickState, true);
	}

	public void OnGatherGimmickInfo(int managedId, bool isUsed)
	{
		if (base.enableSend && base.owner.IsOriginal())
		{
			Coop_Model_GatherGimmickInfo coop_Model_GatherGimmickInfo = new Coop_Model_GatherGimmickInfo();
			coop_Model_GatherGimmickInfo.id = base.owner.id;
			coop_Model_GatherGimmickInfo.managedId = managedId;
			coop_Model_GatherGimmickInfo.ownerId = base.owner.id;
			coop_Model_GatherGimmickInfo.isUsed = isUsed;
			SendBroadcast(coop_Model_GatherGimmickInfo, false, null, null);
		}
	}
}
