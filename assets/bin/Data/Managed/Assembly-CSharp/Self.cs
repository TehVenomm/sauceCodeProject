using Network;
using UnityEngine;

public class Self : Player
{
	private const int lineDivide = 30;

	public const string EFFECT_NAME_INK_SPLASH = "ef_btl_pl_blind_01";

	public const string EFFECT_NAME_INK_FLICK = "ef_btl_pl_blind_02";

	public const string EFFECT_NAME_FLICK_WARNING = "ef_btl_target_flick";

	public const string EFFECT_NAME_BLIND = "ef_btl_pl_darkness_02";

	private const float CANNON_DRAG_RATE_Y = 15f;

	private const float CANNON_ANGLE_LIMT_UP = 30f;

	private const float CANNON_ANGLE_LIMIT_DOWN = 10f;

	protected Vector3 arrowAimLineVec = Vector3.get_forward();

	protected Vector2 arrowAimInputVec = Vector2.get_zero();

	protected Vector2 arrowAimInputOffset = Vector2.get_zero();

	protected GameObject arrowAimLesserCursorEffect;

	protected float arrowAimLesserCursorStartTime = -1f;

	public TaskChecker taskChecker = new TaskChecker();

	private static readonly float STATUS_SYNC_INTERVAL = 5f;

	private float statusSyncTime = STATUS_SYNC_INTERVAL;

	private int syncHp;

	private GameObject inkSplashEffect_;

	private float inkSplashTimerMax_;

	private float inkSplashTimer_;

	private float reduceTimeByFlick_;

	private static readonly Vector3 INKSPLASH_LANDSCAPE_OFFSET = new Vector3(0f, -0.15f, 0f);

	private EffectCtrl inkSplashCtrl_;

	private int inkSplashState_;

	private GameObject effectFlickWarning_;

	private Transform _blindEffect;

	private float _blindTimer;

	public Vector3 cannonAimForward = Vector3.get_zero();

	private Vector3 cannonAimEuler = Vector3.get_zero();

	private float baseEulerX;

	private bool isPlayingRotateSE;

	private Vector2 baseInputVec = Vector2.get_zero();

	private bool isBaseInputVec;

	public float overSpeedLimit = 2f;

	public float dragRateX = 2f;

	private Transform effectTapTrans;

	private bool isFirstRideCannon = true;

	public Vector3 arrowAimForward
	{
		get;
		protected set;
	}

	public Vector3 arrowTmpFoward
	{
		get;
		private set;
	}

	public int arrowAimStartSign
	{
		get;
		private set;
	}

	public int arrowAimSign
	{
		get;
		set;
	}

	public Vector3 arrowAimLesserCursorPos
	{
		get;
		protected set;
	}

	public LineRenderer bulletLineRenderer
	{
		get;
		protected set;
	}

	public GatherPointObject nearGatherPoint
	{
		get;
		set;
	}

	public IFieldGimmickObject nearFieldGimmick
	{
		get;
		set;
	}

	public bool isAutoMode => base.controller is AutoSelfController;

	public Self()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		arrowAimForward = Vector3.get_zero();
		arrowAimLesserCursorPos = Vector3.get_zero();
		bulletLineRenderer = null;
		base.objectType = OBJECT_TYPE.SELF;
	}

	protected override void Clear()
	{
		base.Clear();
	}

	public override void Load(PlayerLoadInfo load_info, PlayerLoader.OnCompleteLoad callback = null)
	{
		base.Load(load_info, callback);
		ResetShadowSealingUI();
	}

	public override void OnLoadComplete()
	{
		base.OnLoadComplete();
		if (stepCtrl != null)
		{
			stepCtrl.stampDistance = 1000f;
		}
		if (base.isDead && (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && base.rescueTime <= 0f)
		{
			BeginSpect();
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpdateBurstUIInfo();
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.UpdateBurstUIInfo();
		}
	}

	private void OnScreenRotate(bool is_portrait)
	{
		if (IsInkSplash())
		{
			RotateInkSplash(is_portrait);
		}
	}

	protected override void OnDisable()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (bulletLineRenderer != null)
		{
			Object.Destroy(bulletLineRenderer.get_gameObject());
		}
		base.OnDisable();
	}

	protected override void OnDestroy()
	{
		if (IsInkSplash())
		{
			InkSplashEnd();
		}
		DestroyBlindEffect();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
		soulEnergyCtrl = null;
		base.OnDestroy();
	}

	public void OnCached()
	{
		InkSplashEnd();
		DestroyBlindEffect();
	}

	protected override void Initialize()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		if (bulletLineRenderer == null)
		{
			Transform val = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.bulletLine, MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
			if (val == null)
			{
				return;
			}
			bulletLineRenderer = val.GetComponent<LineRenderer>();
			if (bulletLineRenderer != null)
			{
				bulletLineRenderer.set_enabled(false);
				bulletLineRenderer.SetColors(Color.get_yellow(), Color.get_yellow());
				bulletLineRenderer.get_material().set_renderQueue(3001);
			}
		}
		soulEnergyCtrl = new SoulEnergyController();
		soulEnergyCtrl.Initialize(this);
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && MonoBehaviourSingleton<UIPlayerStatus>.I.targetPlayer != this)
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.SetTarget(this);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && MonoBehaviourSingleton<UIEnduranceStatus>.I.targetPlayer != this)
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.SetTarget(this);
		}
		if (MonoBehaviourSingleton<UISkillButtonGroup>.IsValid())
		{
			MonoBehaviourSingleton<UISkillButtonGroup>.I.SetTarget(this);
		}
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && !weaponEquipItemDataList.IsNullOrEmpty())
		{
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ResetItemDamageByWeapon(weaponEquipItemDataList);
		}
	}

	protected override void Update()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		if (MonoBehaviourSingleton<UIContinueButton>.IsValid() && !MonoBehaviourSingleton<UIContinueButton>.I.get_gameObject().get_activeSelf() && MonoBehaviourSingleton<UIContinueButton>.I.CheckVisible() && MonoBehaviourSingleton<StageObjectManager>.I.self == this)
		{
			bool flag = false;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
			{
				flag = true;
			}
			if (!flag)
			{
				MonoBehaviourSingleton<UIContinueButton>.I.Initialize();
				if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
				{
					bool continueButton = MonoBehaviourSingleton<InGameManager>.I.CanRushPayContinue();
					MonoBehaviourSingleton<UIContinueButton>.I.SetContinueButton(continueButton);
				}
				else if (QuestManager.IsValidInGameWaveMatch(true))
				{
					MonoBehaviourSingleton<UIContinueButton>.I.SetContinueButton(false);
				}
			}
		}
		if (IsNeedActivateUIButtonsForRush() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
		}
		if (base.isArrowAimBossMode)
		{
			UpdateBulletLine();
		}
		if (QuestManager.IsValidInGameExplore())
		{
			statusSyncTime -= Time.get_deltaTime();
			if (statusSyncTime <= 0f && syncHp != base.hp)
			{
				SendExploreSyncPlayerStatus();
			}
		}
		if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && MonoBehaviourSingleton<UISpectatorButton>.I.IsEnable())
		{
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.DoEnable();
			}
			if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
			{
				MonoBehaviourSingleton<UIEnduranceStatus>.I.DoEnable();
			}
			MonoBehaviourSingleton<UISpectatorButton>.I.EndSpect();
			MonoBehaviourSingleton<UISkillButtonGroup>.I.DoEnable();
		}
		if (IsInkSplash())
		{
			UpdateInkSplash();
		}
		UpdateBlind();
	}

	protected override void UpdateAction()
	{
		base.UpdateAction();
		ACTION_ID actionID = base.actionID;
		if (actionID == (ACTION_ID)23)
		{
			if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() && !IsAutoReviving())
			{
				OnEndContinueTimeEnd();
			}
			if (QuestManager.IsValidInGameTrial() && !IsAutoReviving())
			{
				OnEndContinueTimeEnd();
			}
		}
	}

	public void OnTap()
	{
		soulEnergyCtrl.Tap();
		if (isSpearHundred)
		{
			spearHundredSecFromLastTap = 0f;
		}
	}

	private bool IsNeedActivateUIButtonsForRush()
	{
		if (!MonoBehaviourSingleton<UIContinueButton>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIContinueButton>.I.IsEnableButtonAll)
		{
			return false;
		}
		if (!MonoBehaviourSingleton<InGameManager>.IsValid() || (!MonoBehaviourSingleton<InGameManager>.I.IsRush() && !QuestManager.IsValidInGameWaveMatch(true)))
		{
			return false;
		}
		if (base.continueTime > 0f)
		{
			return false;
		}
		if (!base.isDead)
		{
			return false;
		}
		if (base.actionID != (ACTION_ID)23)
		{
			return false;
		}
		return true;
	}

	public override void ActAttack(int id, bool send_packet = true, bool sync_immediately = false, string _motionLayerName = "")
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (base.isArrowAimBossMode && base.isArrowAimKeep)
		{
			arrowAimInputOffset += arrowAimInputVec;
			arrowAimInputVec = Vector2.get_zero();
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT) && id == 98)
		{
			taskChecker.OnHeatPairSwords();
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnHeatPairSwords();
			}
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && id == base.playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId && pairSwordsCtrl.IsComboLvMax())
		{
			taskChecker.OnSoulPairSwords();
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulPairSwords();
			}
		}
		base.ActAttack(id, send_packet, sync_immediately, _motionLayerName);
		if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
		{
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true, 0f);
		}
	}

	public override void SetChargeRelease(float charge_rate)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true, 0f);
		if (base.isArrowAimBossMode)
		{
			Vector3 lerpRotation = Quaternion.LookRotation(arrowAimForward) * arrowAimLineVec;
			lerpRotation.y = 0f;
			SetLerpRotation(lerpRotation);
		}
		base.SetChargeRelease(charge_rate);
	}

	public override void SetChargeExpandRelease(float chargeRate)
	{
		MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true, 0f);
		base.SetChargeExpandRelease(chargeRate);
	}

	public override void SetInputAxis(Vector2 input_vec)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		bool startInputRotate = base.startInputRotate;
		base.SetInputAxis(input_vec);
		if (startInputRotate != base.startInputRotate)
		{
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, 0f);
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetDisable(true);
		}
	}

	public override void ActDead(bool force_sync = false, bool recieve_direct = false)
	{
		base.ActDead(force_sync, recieve_direct);
		taskChecker.OnDeath();
		soulEnergyCtrl.Sleep();
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
			MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(true);
		}
	}

	public override void ActDeadStandup(int standup_hp, eContinueType cType)
	{
		base.ActDeadStandup(standup_hp, cType);
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && cType == eContinueType.AUTO_REVIVE)
		{
			MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
		}
	}

	public override void ActParalyze()
	{
		base.ActParalyze();
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
		}
	}

	public override void OnEndContinueTimeEnd()
	{
		if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			base.OnEndContinueTimeEnd();
			if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
			{
				BeginSpect();
			}
			else
			{
				if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
				{
					MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
				}
				if (MonoBehaviourSingleton<InGameProgress>.IsValid() && QuestManager.IsValidInGameTrial())
				{
					MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
				}
				else if (MonoBehaviourSingleton<InGameProgress>.IsValid())
				{
					if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsForceDefeatQuest())
					{
						MonoBehaviourSingleton<InGameProgress>.I.BattleForceDefeatsEvent();
					}
					else if (!MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol)
					{
						MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
					}
				}
			}
		}
	}

	public override bool ApplyGather()
	{
		bool flag = base.ApplyGather();
		if (flag)
		{
			base.targetGatherPoint.Gather();
			isGatherInterruption = false;
		}
		return flag;
	}

	public override bool ActSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
	{
		bool flag = base.ActSkillAction(skill_index, false);
		SkillInfo.SkillParam actSkillParam = base.skillInfo.actSkillParam;
		if (actSkillParam == null)
		{
			return false;
		}
		if (flag)
		{
			SKILL_SLOT_TYPE type = actSkillParam.tableData.type;
			if (type == SKILL_SLOT_TYPE.ATTACK)
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true, 0f);
			}
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.OnSkillUse(base.skillInfo.actSkillParam);
			}
			taskChecker.OnUseMagi();
		}
		return flag;
	}

	public override int ExecHealHp(HealData healData, bool isPacket = false)
	{
		int num = base.ExecHealHp(healData, isPacket);
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
		{
			MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerRecoverHp(this, num, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
		}
		return num;
	}

	public override void ApplyChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
	{
		base.ApplyChangeWeapon(item, weapon_index);
		if (QuestManager.IsValidInGameExplore())
		{
			SendExploreSyncPlayerStatus();
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
		}
	}

	protected override void EndAction()
	{
		bool isArrowAimLesserMode = base.isArrowAimLesserMode;
		ACTION_ID actionID = base.actionID;
		base.EndAction();
		ACTION_ID aCTION_ID = actionID;
		if (aCTION_ID == ACTION_ID.PARALYZE && MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
		}
		if (bulletLineRenderer != null)
		{
			bulletLineRenderer.set_enabled(false);
		}
		if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
		{
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetDisable(false);
			if (isArrowAimLesserMode)
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, base.playerParameter.specialActionInfo.arrowAimLesserUnlockTime);
			}
			else if (MonoBehaviourSingleton<TargetMarkerManager>.I.parameter != null)
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, MonoBehaviourSingleton<TargetMarkerManager>.I.parameter.defaultUnlockTime);
			}
			else
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, 0f);
			}
		}
	}

	public override bool OnBuffStart(BuffParam.BuffData buffData)
	{
		if (!base.OnBuffStart(buffData))
		{
			return false;
		}
		BuffParam.BUFFTYPE type = buffData.type;
		if (type == BuffParam.BUFFTYPE.BLIND)
		{
			CreateBlindEffect(buffData.time);
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
		}
		if (QuestManager.IsValidInGameExplore())
		{
			SendExploreSyncPlayerStatus();
		}
		return true;
	}

	protected override void OnUIBuffRoutine(BuffParam.BUFFTYPE type, int value)
	{
		switch (type)
		{
		case BuffParam.BUFFTYPE.REGENERATE:
		case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
			if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
			{
				MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerRecoverHp(this, value, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
			}
			break;
		case BuffParam.BUFFTYPE.POISON:
		case BuffParam.BUFFTYPE.BURNING:
		case BuffParam.BUFFTYPE.DEADLY_POISON:
			if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
			{
				MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage(this, value, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
			}
			break;
		}
	}

	public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
	{
		if (!base.OnBuffEnd(type, sync, isPlayEndEffect))
		{
			return false;
		}
		switch (type)
		{
		case BuffParam.BUFFTYPE.INK_SPLASH:
			InkSplashEnd();
			break;
		case BuffParam.BUFFTYPE.BLIND:
			DestroyBlindEffect();
			break;
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
		}
		if (QuestManager.IsValidInGameExplore())
		{
			SendExploreSyncPlayerStatus();
		}
		return true;
	}

	public override void OnInkSplash(InkSplashInfo info)
	{
		base.OnInkSplash(info);
		InkSplashStart(info);
	}

	public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		base.OnAttackedHitOwner(status);
		if (status.attackInfo.toPlayer.isBuffCancellation && !IsInBarrier() && MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(string.Empty, STRING_CATEGORY.ENEMY_REACTION, 3u);
		}
	}

	protected override void OnAttackFromHitDirection(AttackedHitStatusDirection status, StageObject to_object)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		base.OnAttackFromHitDirection(status, to_object);
		if (status.attackInfo.shakeCameraPercent != 0f && to_object.objectType == OBJECT_TYPE.ENEMY && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(status.hitPos, status.attackInfo.shakeCameraPercent, status.attackInfo.shakeCycleTime);
		}
	}

	protected override bool IsDamageValid(AttackedHitStatusDirection status)
	{
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED)
		{
			return true;
		}
		return base.IsDamageValid(status);
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
		{
			base.OnAttackedHitFix(status);
			if (status.attackInfo.shakeCameraPercent != 0f && status.fromType == OBJECT_TYPE.ENEMY && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(status.hitPos, status.attackInfo.shakeCameraPercent, status.attackInfo.shakeCycleTime);
			}
			if (status.damage + status.shieldDamage > 0 && uiPlayerStatusGizmo != null)
			{
				uiPlayerStatusGizmo.OnDamageSelf();
				if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
				{
					MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage(this, status.origin);
				}
			}
		}
	}

	public override Vector3 GetCameraTargetPos()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = base.GetCameraTargetPos();
		if (base.isArrowAimLesserMode)
		{
			val += arrowAimLesserCursorPos;
		}
		return val;
	}

	public override bool CanPlayEffectEvent()
	{
		return true;
	}

	public override void OnAnimEvent(AnimEventData.EventData data)
	{
		switch (data.id)
		{
		case AnimEventFormat.ID.COMBO_INPUT_ON:
		{
			SelfController selfController = base.controller as SelfController;
			if (selfController != null && selfController.nextCommand != null && selfController.nextCommand.type == SelfController.COMMAND_TYPE.ATTACK)
			{
				selfController.CancelInput();
			}
			break;
		}
		case AnimEventFormat.ID.CHARGE_INPUT_START:
			if (!MonoBehaviourSingleton<TargetMarkerManager>.I.parameter.enableNormalMarker)
			{
				MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarker.defaultUnlockTime);
			}
			break;
		case AnimEventFormat.ID.TARGET_LOCK_ON:
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true, 0f);
			return;
		case AnimEventFormat.ID.TARGET_LOCK_OFF:
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, 0f);
			return;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_FULL_BURST:
			if (thsCtrl != null)
			{
				if (thsCtrl.DoFullBurst())
				{
					taskChecker.OnBurstTwoHandSword();
					if (MonoBehaviourSingleton<InGameManager>.IsValid())
					{
						MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnBurstTwoHandSword();
					}
					if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIPlayerStatus>.I.DoFullBurstAction();
					}
					if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnduranceStatus>.I.DoFullBurstAction();
					}
				}
				return;
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_SINGLE_SHOT:
			if (thsCtrl != null)
			{
				if (thsCtrl.DoShootAction())
				{
					if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIPlayerStatus>.I.DoShootAction();
					}
					if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnduranceStatus>.I.DoShootAction();
					}
				}
				return;
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_RELOAD_NOW:
			if (thsCtrl != null && thsCtrl.IsReloadingNow)
			{
				if (thsCtrl.DoReloadAction() && MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
				{
					MonoBehaviourSingleton<UIPlayerStatus>.I.DoReloadAction();
				}
				return;
			}
			break;
		}
		base.OnAnimEvent(data);
	}

	protected override void EventCameraTargetOffsetOn(AnimEventData.EventData data)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			float[] floatArgs = data.floatArgs;
			InGameCameraManager.TargetOffset targetOffset = new InGameCameraManager.TargetOffset();
			targetOffset.pos = new Vector3(floatArgs[0], floatArgs[1], floatArgs[2]);
			targetOffset.rot = new Vector3(floatArgs[3], floatArgs[4], floatArgs[5]);
			MonoBehaviourSingleton<InGameCameraManager>.I.SetAnimEventTargetOffsetByPlayer(targetOffset);
		}
	}

	protected override void EventCameraTargetOffsetOff()
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByPlayer();
		}
	}

	protected override void EventCameraStopOn(AnimEventData.EventData data)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && data.floatArgs.Length >= 6)
		{
			InGameCameraManager i = MonoBehaviourSingleton<InGameCameraManager>.I;
			i.SetStopPos(i.movePosition + new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]));
			InGameCameraManager inGameCameraManager = i;
			Quaternion moveRotation = i.moveRotation;
			inGameCameraManager.SetStopRotEular(moveRotation.get_eulerAngles() + new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
			i.SetCameraMode(InGameCameraManager.CAMERA_MODE.STOP);
		}
	}

	protected override void EventCameraStopOff()
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && MonoBehaviourSingleton<InGameCameraManager>.I.IsCameraMode(InGameCameraManager.CAMERA_MODE.STOP))
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
		}
	}

	protected override void EventCameraCutOn(AnimEventData.EventData data)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && !FieldManager.IsValidInGameNoBoss() && data.floatArgs.Length >= 6)
		{
			Vector3 val = _position + Quaternion.LookRotation(_forward) * new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			Vector3 val2 = _position + Quaternion.LookRotation(_forward) * new Vector3(0f - data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			Quaternion cutRot = Quaternion.LookRotation(_forward) * Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
			Quaternion cutRot2 = Quaternion.LookRotation(_forward) * Quaternion.Euler(new Vector3(data.floatArgs[3], 0f - data.floatArgs[4], data.floatArgs[5]));
			RaycastHit hit = default(RaycastHit);
			RaycastHit hit2 = default(RaycastHit);
			bool flag = AIUtility.RaycastObstacle(this, val, out hit);
			bool flag2 = AIUtility.RaycastObstacle(this, val2, out hit2);
			if (!flag || !flag2)
			{
				InGameCameraManager i = MonoBehaviourSingleton<InGameCameraManager>.I;
				if (!flag)
				{
					i.SetCutPos(val);
					i.SetCutRot(cutRot);
				}
				else if (!flag2)
				{
					i.SetCutPos(val2);
					i.SetCutRot(cutRot2);
				}
				i.SetCameraMode(InGameCameraManager.CAMERA_MODE.CUT);
			}
		}
	}

	protected override void EventCameraCutOff()
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearCutCameraMode();
		}
	}

	public override void ShotArrow(Vector3 shot_pos, Quaternion shot_rot, AttackInfo attack_info, bool isSitShot, bool isAimEnd)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (attack_info != null)
		{
			base.ShotArrow(shot_pos, shot_rot, attack_info, isSitShot, isAimEnd);
			if (base.isArrowAimBossMode)
			{
				SetArrowAimBossVisible(false);
			}
		}
	}

	public override void ShotSoulArrow()
	{
		base.ShotSoulArrow();
		if (base.isArrowAimBossMode)
		{
			SetArrowAimBossVisible(false);
		}
		MonoBehaviourSingleton<TargetMarkerManager>.I.ResetMultiLock();
	}

	public override Vector3 GetBulletShotVec(Vector3 appear_pos)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (base.isArrowAimBossMode || (base.isArrowAimLesserMode && base.targetingPoint == null))
		{
			Vector3 val = Quaternion.LookRotation(arrowAimForward) * arrowAimLineVec;
			return val.get_normalized();
		}
		return base.GetBulletShotVec(appear_pos);
	}

	public override void SetArrowAimBossMode(bool enable)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			if (base.actionTarget != null)
			{
				LookAt(base.actionTarget._position, true);
			}
			else if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
			{
				LookAt(MonoBehaviourSingleton<StageObjectManager>.I.boss.get_transform().get_position(), false);
			}
			base.isActSpecialAction = true;
		}
		else
		{
			base.targetingPointList.Clear();
			if (base.targetAimAfeterPoint != null)
			{
				base.targetingPointList.Add(base.targetAimAfeterPoint);
			}
		}
		arrowTmpFoward = _forward;
		arrowAimForward = _forward;
		arrowAimLineVec = Vector3.get_forward();
		arrowAimInputVec = Vector2.get_zero();
		arrowAimInputOffset = Vector2.get_zero();
		base.SetArrowAimBossMode(enable);
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			if (enable)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.ARROW_AIM_BOSS);
			}
			else
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
			}
		}
	}

	public void SetArrowAimBossModeStartSign(Vector2 moveVec)
	{
		arrowAimStartSign = (int)Mathf.Sign(moveVec.x);
	}

	protected override void SetArrowAimBossVisible(bool enable)
	{
		base.SetArrowAimBossVisible(enable);
		if (bulletLineRenderer != null)
		{
			bulletLineRenderer.set_enabled(enable);
			if (enable)
			{
				SetBulletLineColor(false);
				UpdateBulletLine();
			}
		}
	}

	public void UpdateArrowAimBossMode(Vector2 input_vec, Vector2 input_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		Vector3 bulletAppearPos = GetBulletAppearPos();
		Vector3 bulletShotVec = GetBulletShotVec(bulletAppearPos);
		arrowAimInputVec = input_vec;
		InGameSettingsManager.SelfController.ArrowAimBossSettings arrowAimBossSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimBossSettings;
		float num = input_vec.x + arrowAimInputOffset.x;
		float num2 = arrowAimBossSettings.dragRateX * Mathf.Sign(num) * Mathf.Abs(num);
		float num3 = Mathf.Abs(num2) - arrowAimBossSettings.angleLimitSide;
		if (num3 < 0f)
		{
			num3 = 0f;
		}
		num3 *= Mathf.Sign(num2) * arrowAimBossSettings.overSpeedRate;
		num3 = Mathf.Clamp(num3, 0f - arrowAimBossSettings.overSpeedLimit, arrowAimBossSettings.overSpeedLimit);
		float num4 = input_pos.x / (float)Screen.get_width();
		float num5 = 1f;
		if (num <= 0f)
		{
			num5 = -1f;
		}
		else
		{
			num5 = 1f;
			num4 = 1f - num4;
		}
		if (num4 < 0f)
		{
			num4 = 0f;
		}
		if (num4 < arrowAimBossSettings.overScreenRate && arrowAimBossSettings.overScreenMargin < arrowAimBossSettings.overScreenRate)
		{
			float num6 = (num4 - arrowAimBossSettings.overScreenMargin) / (arrowAimBossSettings.overScreenRate - arrowAimBossSettings.overScreenMargin);
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			float num7 = num5 * arrowAimBossSettings.overSpeedLimit * (1f - num6);
			if (Mathf.Abs(num7) > Mathf.Abs(num3))
			{
				num3 = num7;
			}
			float num8 = 0f;
			if (base.targetingPointList.Count > 0)
			{
				bulletShotVec.y = 0f;
				int i = 0;
				for (int count = base.targetingPointList.Count; i < count; i++)
				{
					Vector3 val = base.targetingPointList[i].param.markerPos - bulletAppearPos;
					val.y = 0f;
					float num9 = Vector3.Angle(bulletShotVec, val);
					if (i == 0 || num9 < num8)
					{
						num8 = num9;
					}
				}
			}
			float overSpeedAngleMinRange = arrowAimBossSettings.overSpeedAngleMinRange;
			float overSpeedAngleChangeRange = arrowAimBossSettings.overSpeedAngleChangeRange;
			float overSpeedMaxRate = arrowAimBossSettings.overSpeedMaxRate;
			float num10 = 0f;
			if (overSpeedAngleChangeRange > 0f)
			{
				num10 = (num8 - overSpeedAngleMinRange) / overSpeedAngleChangeRange;
				num10 = Mathf.Clamp01(num10);
			}
			else
			{
				num10 = ((!(num8 - overSpeedAngleMinRange >= 0f)) ? 0f : 1f);
			}
			num10 = 1f + num10 * (overSpeedMaxRate - 1f);
			num3 *= num10;
		}
		num2 = Mathf.Clamp(num2, 0f - arrowAimBossSettings.angleLimitSide, arrowAimBossSettings.angleLimitSide);
		arrowAimForward = Quaternion.Euler(new Vector3(0f, num3, 0f)) * arrowAimForward;
		Vector3 val2 = MonoBehaviourSingleton<StageObjectManager>.I.boss._position - _position;
		Vector3 normalized = val2.get_normalized();
		Vector3 val3 = Vector3.Cross(arrowTmpFoward, normalized);
		arrowAimSign = ((val3.y >= 0f) ? 1 : (-1));
		float num11 = input_vec.y + arrowAimInputOffset.y;
		float num12 = arrowAimBossSettings.dragRateY * (0f - Mathf.Sign(num11)) * Mathf.Abs(num11);
		num12 = Mathf.Clamp(num12, 0f - arrowAimBossSettings.angleLimitUp, arrowAimBossSettings.angleLimitUp);
		arrowAimLineVec = Quaternion.Euler(num12, num2, 0f) * Vector3.get_forward();
		Vector3 lerpRotation = Quaternion.Euler(new Vector3(0f, num2, 0f)) * arrowAimForward;
		SetLerpRotation(lerpRotation);
	}

	public override void SetArrowAimLesserMode(bool enable)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (enable)
		{
			arrowAimLesserCursorStartTime = Time.get_time();
			arrowAimLesserCursorPos = Vector3.get_zero();
			MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, 0f);
		}
		else
		{
			arrowAimLesserCursorStartTime = -1f;
			arrowAimLesserCursorPos = Vector3.get_zero();
		}
		base.SetArrowAimLesserMode(enable);
	}

	protected override void SetArrowAimLesserVisible(bool enable)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		base.SetArrowAimLesserVisible(enable);
		if (enable)
		{
			if (arrowAimLesserCursorEffect == null)
			{
				Transform val = (!CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT)) ? EffectManager.GetEffect(base.playerParameter.specialActionInfo.arrowAimLesserCursorEffectName, base._transform) : EffectManager.GetEffect("ef_btl_target_e_01", base._transform);
				if (!(val == null))
				{
					float num = 2f;
					val.set_localScale(Vector3.get_one() * num);
					arrowAimLesserCursorEffect = val.get_gameObject();
					val.set_position(_position + arrowAimLesserCursorPos);
					Rigidbody val2 = val.get_gameObject().AddComponent<Rigidbody>();
					val2.set_mass(1f);
					val2.set_angularDrag(100f);
					val2.set_isKinematic(false);
					val2.set_constraints(116);
					val2.set_collisionDetectionMode(1);
					CapsuleCollider val3 = base._collider as CapsuleCollider;
					if (val3 != null)
					{
						CapsuleCollider val4 = val.get_gameObject().AddComponent<CapsuleCollider>();
						val4.set_direction(val3.get_direction());
						val4.set_height(val3.get_height() / num);
						val4.set_radius(val3.get_radius() / num);
						val4.set_center(val3.get_center() / num);
					}
					Utility.SetLayerWithChildren(val, 29);
				}
			}
		}
		else if (arrowAimLesserCursorEffect != null)
		{
			Object.Destroy(arrowAimLesserCursorEffect);
			arrowAimLesserCursorEffect = null;
		}
	}

	public override void UpdateArrowAimLesserMode(Vector2 input_vec)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		base.UpdateArrowAimLesserMode(input_vec);
		InGameSettingsManager.SelfController.ArrowAimLesserSettings arrowAimLesserSettings;
		if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
		{
			arrowAimLesserSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.spearAimLesserSettings;
			UpdateSpearJumpEffect();
		}
		else
		{
			arrowAimLesserSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimLesserSettings;
		}
		Rigidbody val = null;
		if (arrowAimLesserCursorEffect != null)
		{
			val = arrowAimLesserCursorEffect.GetComponent<Rigidbody>();
			if (val != null)
			{
				Vector3 arrowAimLesserCursorPos = val.get_position() - _position;
				arrowAimLesserCursorPos.y = 0f;
				this.arrowAimLesserCursorPos = arrowAimLesserCursorPos;
				Vector3 arrowAimLesserCursorPos2 = this.arrowAimLesserCursorPos;
				if (arrowAimLesserCursorPos2.get_magnitude() >= arrowAimLesserSettings.cursorMaxDistance)
				{
					Vector3 arrowAimLesserCursorPos3 = this.arrowAimLesserCursorPos;
					this.arrowAimLesserCursorPos = arrowAimLesserCursorPos3.get_normalized() * arrowAimLesserSettings.cursorMaxDistance;
				}
				val.set_position(this.arrowAimLesserCursorPos + _position);
			}
		}
		if (this.arrowAimLesserCursorPos == Vector3.get_zero())
		{
			arrowAimForward = _forward;
		}
		else
		{
			arrowAimForward = Quaternion.LookRotation(this.arrowAimLesserCursorPos) * Vector3.get_forward();
		}
		arrowAimLineVec = Vector3.get_forward();
		SetLerpRotation(this.arrowAimLesserCursorPos);
		Vector3 right = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_right();
		Vector3 forward = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_forward();
		right.y = 0f;
		right.Normalize();
		forward.y = 0f;
		forward.Normalize();
		Vector3 val2 = Vector3.get_zero();
		float magnitude = input_vec.get_magnitude();
		if (magnitude >= arrowAimLesserSettings.dragMinLength && magnitude > 0f)
		{
			float num = magnitude;
			if (num >= arrowAimLesserSettings.dragMaxLength)
			{
				num = arrowAimLesserSettings.dragMaxLength;
			}
			num -= arrowAimLesserSettings.dragMinLength;
			float num2 = num / (arrowAimLesserSettings.dragMaxLength - arrowAimLesserSettings.dragMinLength);
			input_vec *= num2 / magnitude;
			val2 = right * input_vec.x + forward * input_vec.y;
			float num3 = 0f;
			if (arrowAimLesserCursorStartTime >= 0f)
			{
				float cursorStartSpeedTime = arrowAimLesserSettings.cursorStartSpeedTime;
				num3 = (cursorStartSpeedTime - (Time.get_time() - arrowAimLesserCursorStartTime)) / cursorStartSpeedTime;
				if (num3 < 0f)
				{
					num3 = 0f;
					arrowAimLesserCursorStartTime = -1f;
				}
			}
			float num4 = 1f;
			if (base.actionTarget != null)
			{
				num4 = arrowAimLesserSettings.cursorTargetingSpeedRate;
			}
			float num5 = num3 * (arrowAimLesserSettings.cursorStartSpeedRate - 1f) + 1f;
			val2 = val2 * arrowAimLesserSettings.cursorSpeed * num5 * num4;
			if (arrowAimLesserCursorEffect != null)
			{
				float num6 = 1f;
				float num7 = num6 + (arrowAimLesserSettings.cursorScale - num6) * (1f - num3);
				arrowAimLesserCursorEffect.get_transform().set_localScale(Vector3.get_one() * num7);
			}
		}
		if (val != null)
		{
			Vector3 arrowAimLesserCursorPos4 = this.arrowAimLesserCursorPos;
			Vector3 normalized = arrowAimLesserCursorPos4.get_normalized();
			float num8 = Vector3.Dot(normalized, val2) * Time.get_deltaTime();
			if (num8 > 0f)
			{
				Vector3 arrowAimLesserCursorPos5 = this.arrowAimLesserCursorPos;
				float magnitude2 = arrowAimLesserCursorPos5.get_magnitude();
				float num9 = magnitude2 + num8;
				if (num9 > arrowAimLesserSettings.cursorMaxDistance)
				{
					float num10 = num9 - arrowAimLesserSettings.cursorMaxDistance;
					val2 -= normalized * num10 / Time.get_deltaTime();
				}
			}
			val.set_velocity(val2);
		}
	}

	protected void UpdateBulletLine()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		if (!(bulletLineRenderer == null) && bulletLineRenderer.get_enabled())
		{
			Vector3 bulletAppearPos = GetBulletAppearPos();
			Vector3 bulletShotVec = GetBulletShotVec(bulletAppearPos);
			if (base.spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				bulletLineRenderer.SetVertexCount(2);
				bulletLineRenderer.SetPosition(0, bulletAppearPos);
				Vector3 val = bulletAppearPos + bulletShotVec * 30f;
				bulletLineRenderer.SetPosition(1, val);
				CheckMultiLock(bulletAppearPos, bulletShotVec);
			}
			else
			{
				AttackInfo attackInfo = FindAttackInfo(base.playerParameter.arrowActionInfo.attackInfoNames[(int)base.spAttackType], false, false);
				if (attackInfo != null)
				{
					float chargingRate = GetChargingRate();
					if (!string.IsNullOrEmpty(attackInfo.rateInfoName) && chargingRate != 0f)
					{
						AttackInfo rate_info = FindAttackInfo(attackInfo.rateInfoName, false, false);
						attackInfo = attackInfo.GetRateAttackInfo(rate_info, chargingRate);
					}
					BulletData bulletData = attackInfo.bulletData;
					if (!(bulletData == null))
					{
						if (bulletData.dataFall == null || (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT) && base.attackID == 1 && chargingRate >= 1f))
						{
							bulletLineRenderer.SetVertexCount(2);
							bulletLineRenderer.SetPosition(0, bulletAppearPos);
							Vector3 val2 = bulletAppearPos + bulletShotVec * 30f;
							bulletLineRenderer.SetPosition(1, val2);
						}
						else if (bulletData.dataFall != null)
						{
							Vector3 val3 = Physics.get_gravity() * bulletData.dataFall.gravityRate;
							float gravityStartTime = bulletData.dataFall.gravityStartTime;
							float speed = bulletData.data.speed;
							bulletLineRenderer.SetVertexCount(30);
							bulletLineRenderer.SetPosition(0, bulletAppearPos);
							float num = bulletData.data.appearTime / 30f;
							float num2 = 0f;
							for (int i = 1; i < 30; i++)
							{
								num2 = ((!(num2 <= gravityStartTime)) ? (num2 + num) : (num2 + num * 0.1f));
								Vector3 val4 = bulletAppearPos + bulletShotVec * speed * num2;
								if (num2 >= gravityStartTime)
								{
									val4 += val3 * (num2 - gravityStartTime) * (num2 - gravityStartTime) / 2f;
								}
								bulletLineRenderer.SetPosition(i, val4);
							}
						}
					}
				}
			}
		}
	}

	public void OnGetRareDrop(REWARD_TYPE type, int item_id)
	{
		if (base.playerSender != null)
		{
			base.playerSender.OnGetRareDrop(type, item_id);
		}
	}

	protected override void OnSendChatMessage(string message)
	{
	}

	protected override void OnSendChatStamp(int stamp_id)
	{
	}

	public override void PlayVoice(int voice_id)
	{
		if (!FieldManager.IsValidInTutorial())
		{
			SoundManager.PlayVoice(base.loader.GetVoiceAudioClip(voice_id), voice_id, 1f, voiceChannel, this, base.loader.head);
		}
	}

	protected override bool EnablePlaySound()
	{
		return true;
	}

	public override void ActGuardDamage()
	{
		taskChecker.OnGuard();
		if (_CheckJustGuardSec())
		{
			taskChecker.OnJustGuard();
			if (MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnJustGuard();
			}
		}
		base.ActGuardDamage();
	}

	public override bool ActSpecialAction(bool start_effect = true, bool isSuccess = true)
	{
		taskChecker.isEnableAttackCount = true;
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.isEnableAttackCount = true;
		}
		return base.ActSpecialAction(start_effect, isSuccess);
	}

	public override void OnPrayerEnd(int prayer_id)
	{
		Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayer_id) as Player;
		if (player != null && !player.isDead)
		{
			taskChecker.OnRevival();
		}
		base.OnPrayerEnd(prayer_id);
	}

	public override void ActGrabbedStart(int enemyId, GrabInfo grabInfo)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		base.ActGrabbedStart(enemyId, grabInfo);
		StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId);
		if (!(stageObject == null))
		{
			MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, true);
			InGameCameraManager.GrabInfo grabInfo2 = MonoBehaviourSingleton<InGameCameraManager>.I.grabInfo;
			grabInfo2.enabled = true;
			grabInfo2.enemyRoot = stageObject._transform;
			grabInfo2.dir = grabInfo.toCameraDir.get_normalized();
			grabInfo2.distance = grabInfo.cameraDistance;
			MonoBehaviourSingleton<InGameCameraManager>.I.target = stageObject.FindNode(grabInfo.cameraLookAt);
			MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.GRABBED);
		}
	}

	public override void ActGrabbedEnd(float angle, float power)
	{
		base.ActGrabbedEnd(angle, power);
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, false);
		MonoBehaviourSingleton<InGameCameraManager>.I.grabInfo.enabled = false;
		MonoBehaviourSingleton<InGameCameraManager>.I.target = base._transform;
		MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
	}

	private void SendExploreSyncPlayerStatus()
	{
		statusSyncTime = STATUS_SYNC_INTERVAL;
		syncHp = base.hp;
		MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncPlayerStatus(this, -1);
	}

	public void CreateInkSplashEffect(Vector3 flickIconPos)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		int num = 10;
		Transform effect = EffectManager.GetEffect("ef_btl_pl_blind_01", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
		if (null != effect)
		{
			effect.set_localPosition(new Vector3(0f, 0f, 1f));
			inkSplashCtrl_ = effect.GetComponent<EffectCtrl>();
			inkSplashEffect_ = effect.get_gameObject();
			Renderer[] componentsInChildren = inkSplashEffect_.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].set_sortingOrder(num);
			}
		}
		effect = EffectManager.GetEffect("ef_btl_target_flick", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
		if (null != effect)
		{
			effect.set_localPosition(flickIconPos);
			effect.set_localScale(new Vector3(0.2f, 0.2f, 0.2f));
			effectFlickWarning_ = effect.get_gameObject();
			Renderer[] componentsInChildren2 = effectFlickWarning_.GetComponentsInChildren<Renderer>();
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				if (num >= componentsInChildren2[j].get_sortingOrder())
				{
					componentsInChildren2[j].set_sortingOrder(num + 1);
				}
			}
			if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() && !MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
			{
				effect.set_localPosition(effect.get_localPosition() + INKSPLASH_LANDSCAPE_OFFSET);
			}
		}
	}

	public void InkSplashStart(InkSplashInfo info)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (!IsInkSplash())
		{
			inkSplashTimerMax_ = info.duration;
			inkSplashTimer_ = inkSplashTimerMax_;
			inkSplashState_ = 0;
			reduceTimeByFlick_ = info.reduceTimeByFlick;
			CreateInkSplashEffect(info.flickIconPos);
		}
	}

	public void InkSplashEnd()
	{
		if (IsInkSplash())
		{
			if (null != inkSplashEffect_)
			{
				EffectManager.ReleaseEffect(inkSplashEffect_, true, false);
				inkSplashEffect_ = null;
			}
			if (null != effectFlickWarning_)
			{
				EffectManager.ReleaseEffect(effectFlickWarning_, true, false);
				effectFlickWarning_ = null;
			}
		}
	}

	public void UpdateInkSplash()
	{
		inkSplashTimer_ -= Time.get_deltaTime();
		if (!(null == inkSplashCtrl_))
		{
			if (inkSplashState_ == 0)
			{
				float num = 2f * inkSplashTimerMax_ / 3f;
				if (num > inkSplashTimer_)
				{
					inkSplashCtrl_.animator.Play("ACT1");
					inkSplashState_++;
				}
			}
			else if (inkSplashState_ == 1)
			{
				float num2 = inkSplashTimerMax_ / 3f;
				if (num2 > inkSplashTimer_)
				{
					inkSplashCtrl_.animator.Play("ACT2");
					inkSplashState_++;
				}
			}
			else if (inkSplashState_ == 2 && 0f > inkSplashTimer_)
			{
				InkSplashEnd();
				inkSplashState_++;
			}
		}
	}

	public void ReduceInkSplashTime()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		inkSplashTimer_ -= reduceTimeByFlick_;
		buffParam.ReduceInkSplashTime(reduceTimeByFlick_);
		Transform effect = EffectManager.GetEffect("ef_btl_pl_blind_02", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
		if (null != effect)
		{
			effect.set_localPosition(new Vector3(0f, 0f, 1f));
		}
	}

	public override bool IsInkSplash()
	{
		return null != inkSplashEffect_;
	}

	private void RotateInkSplash(bool is_portrait)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Transform val = effectFlickWarning_.get_transform();
		if (is_portrait)
		{
			val.set_localPosition(val.get_localPosition() - INKSPLASH_LANDSCAPE_OFFSET);
		}
		else
		{
			val.set_localPosition(val.get_localPosition() + INKSPLASH_LANDSCAPE_OFFSET);
		}
	}

	private void CreateBlindEffect(float sec)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		_blindTimer = sec;
		if (!(_blindEffect != null))
		{
			_blindEffect = EffectManager.GetEffect("ef_btl_pl_darkness_02", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
			if (!(_blindEffect == null))
			{
				_blindEffect.set_localPosition(new Vector3(0f, 0f, 1f));
				Renderer[] componentsInChildren = _blindEffect.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].set_sortingOrder(10);
				}
			}
		}
	}

	public void DestroyBlindEffect()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		if (_blindEffect != null)
		{
			EffectManager.ReleaseEffect(_blindEffect.get_gameObject(), true, false);
			_blindEffect = null;
		}
	}

	public void UpdateBlind()
	{
		if (!(_blindEffect == null))
		{
			_blindTimer -= Time.get_deltaTime();
			if (_blindTimer <= 0f)
			{
				DestroyBlindEffect();
			}
		}
	}

	private void BeginSpect()
	{
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.DoDisable();
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.DoDisable();
		}
		MonoBehaviourSingleton<UISkillButtonGroup>.I.DoDisable();
		MonoBehaviourSingleton<UISpectatorButton>.I.BeginSpect();
	}

	public bool IsAbleArrowSitShot()
	{
		if (!CheckAttackMode(ATTACK_MODE.ARROW))
		{
			return false;
		}
		if (base.spAttackType != 0 && base.spAttackType != SP_ATTACK_TYPE.HEAT)
		{
			return false;
		}
		if (!base.enableSpAttackContinue)
		{
			return false;
		}
		return true;
	}

	public bool IsActionFromAvoid()
	{
		if (base.actionID == ACTION_ID.MAX || base.actionID == (ACTION_ID)35)
		{
			if (base.attackMode == ATTACK_MODE.TWO_HAND_SWORD && !base.playerParameter.twoHandSwordActionInfo.avoidAttackEnable)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public override void CancelCannonMode()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		if (IsOnCannonMode())
		{
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
			}
			if (base.targetFieldGimmickCannon != null && base.targetFieldGimmickCannon.IsUsing())
			{
				base.targetFieldGimmickCannon.OnLeave();
			}
			if (effectTapTrans != null)
			{
				EffectManager.ReleaseEffect(effectTapTrans.get_gameObject(), true, false);
				effectTapTrans = null;
			}
			SetCannonState(CANNON_STATE.NONE);
			base.targetFieldGimmickCannon = null;
			base._rigidbody.set_isKinematic(false);
		}
	}

	public override void SetCannonAimMode()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			LookAt(MonoBehaviourSingleton<StageObjectManager>.I.boss._position, true);
		}
		if (base.targetFieldGimmickCannon != null)
		{
			FieldGimmickCannonField fieldGimmickCannonField = base.targetFieldGimmickCannon as FieldGimmickCannonField;
			if (fieldGimmickCannonField != null)
			{
				cannonAimForward = fieldGimmickCannonField.GetBaseTransformForward();
			}
			else
			{
				cannonAimForward = _forward;
			}
			Transform cannonTransform = base.targetFieldGimmickCannon.GetCannonTransform();
			if (cannonTransform != null)
			{
				Quaternion localRotation = cannonTransform.get_localRotation();
				Vector3 eulerAngles = localRotation.get_eulerAngles();
				float x = eulerAngles.x;
				cannonAimEuler = new Vector3(baseEulerX = GetNegativeEuler(x), 0f, 0f);
			}
			Quaternion rotation = base._transform.get_rotation();
			Vector3 eulerAngles2 = rotation.get_eulerAngles();
			float y = eulerAngles2.y;
			y = GetNegativeEuler(y);
			baseInputVec = Vector2.get_zero();
			isBaseInputVec = false;
			dragRateX = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.cannonDragRateX;
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && base.targetFieldGimmickCannon.IsAimCamera())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_AIM);
			}
			if (isFirstRideCannon)
			{
				CreateCannonTapEffect();
				isFirstRideCannon = false;
			}
			SetCannonState(CANNON_STATE.READY);
		}
	}

	public override void SetCannonBeamMode()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (base.targetFieldGimmickCannon != null)
		{
			Vector3 position = base.targetFieldGimmickCannon.GetPosition();
			Vector3 normalized = position.get_normalized();
			_rotation = Quaternion.LookRotation(normalized, Vector3.get_up());
			cannonAimForward = _forward;
			if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM_CHARGE);
			}
			SetCannonState(CANNON_STATE.READY);
		}
	}

	private void CreateCannonTapEffect()
	{
		Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
		if (!(cameraTransform == null))
		{
			Transform val = effectTapTrans = EffectManager.GetEffect("ef_btl_cannon_tap", cameraTransform);
		}
	}

	public override void UpdateCannonAimMode(Vector2 input_vec, Vector2 input_pos)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		if (!isBaseInputVec)
		{
			baseInputVec = input_vec;
			isBaseInputVec = true;
		}
		float x = input_vec.x;
		x = ((!(Mathf.Abs(input_vec.x) - Mathf.Abs(baseInputVec.x) <= 0f)) ? (x - baseInputVec.x) : 0f);
		float num = dragRateX * Mathf.Sign(x) * Mathf.Abs(x);
		if (num < 0f - overSpeedLimit)
		{
			num = 0f - overSpeedLimit;
		}
		if (num > overSpeedLimit)
		{
			num = overSpeedLimit;
		}
		if (Mathf.Abs(num) > 0f)
		{
			if (MonoBehaviourSingleton<SoundManager>.IsValid() && !isPlayingRotateSE)
			{
				SoundManager.PlayLoopSE(10000079, this, base.targetFieldGimmickCannon.GetTransform());
				isPlayingRotateSE = true;
			}
		}
		else if (MonoBehaviourSingleton<SoundManager>.IsValid() && isPlayingRotateSE)
		{
			SoundManager.StopLoopSE(10000079, this);
			isPlayingRotateSE = false;
		}
		cannonAimForward = Quaternion.Euler(new Vector3(0f, num, 0f)) * cannonAimForward;
		float num2 = input_vec.y - baseInputVec.y;
		float num3 = 15f * (0f - Mathf.Sign(num2)) * Mathf.Abs(num2);
		num3 += baseEulerX;
		if (num3 < -30f)
		{
			num3 = -30f;
		}
		if (num3 > 10f)
		{
			num3 = 10f;
		}
		cannonAimEuler = new Vector3(num3, 0f, 0f);
		_rotation = Quaternion.LookRotation(cannonAimForward);
		base._transform.set_rotation(_rotation);
	}

	public Vector3 GetCannonShotEuler()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return cannonAimEuler;
	}

	public void ResetCannonShotAimEuler()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		baseEulerX = cannonAimEuler.x;
		baseInputVec = Vector2.get_zero();
		isBaseInputVec = false;
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			SoundManager.StopLoopSE(10000079, this);
			isPlayingRotateSE = false;
		}
	}

	private float GetNegativeEuler(float euler)
	{
		return (!(euler > 180f)) ? euler : (euler - 360f);
	}

	public void ResetShadowSealingUI()
	{
		if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT))
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.ShowShadowSealing(true);
		}
		else
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.ShowShadowSealing(false);
		}
	}

	public override void CheckBuffShadowSealing()
	{
		if (!isChangingWeapon)
		{
			if (!CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT))
			{
				_EndBuffShadowSealing();
			}
			else
			{
				Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
				if (object.ReferenceEquals(boss, null))
				{
					_EndBuffShadowSealing();
				}
				else if (!boss.IsDebuffShadowSealing())
				{
					_EndBuffShadowSealing();
				}
				else
				{
					_StartBuffShadowSealing();
				}
			}
		}
	}

	public bool CheckAvoidAttack()
	{
		if (IsActionFromAvoid() && base.enableSpAttackContinue)
		{
			switch (base.attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
			{
				SP_ATTACK_TYPE spAttackType = base.spAttackType;
				if (spAttackType == SP_ATTACK_TYPE.BURST)
				{
					int avoidAttackID = base.playerParameter.ohsActionInfo.burstOHSInfo.AvoidAttackID;
					string motionLayerName3 = GetMotionLayerName(base.attackMode, base.spAttackType, avoidAttackID);
					string motionLayerName2 = motionLayerName3;
					ActAttack(avoidAttackID, true, false, motionLayerName2);
					return true;
				}
				return false;
			}
			case ATTACK_MODE.TWO_HAND_SWORD:
				if (base.spAttackType == SP_ATTACK_TYPE.NONE || base.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					ActAttack(20, true, false, string.Empty);
					return true;
				}
				if (base.spAttackType == SP_ATTACK_TYPE.BURST)
				{
					int burstAvoidAttackID = base.playerParameter.twoHandSwordActionInfo.burstTHSInfo.BurstAvoidAttackID;
					string motionLayerName = GetMotionLayerName(base.attackMode, base.spAttackType, burstAvoidAttackID);
					string motionLayerName2 = motionLayerName;
					ActAttack(burstAvoidAttackID, true, false, motionLayerName2);
					return true;
				}
				return false;
			case ATTACK_MODE.SPEAR:
				switch (base.spAttackType)
				{
				case SP_ATTACK_TYPE.NONE:
				case SP_ATTACK_TYPE.HEAT:
					ActAttack(20, true, false, string.Empty);
					return true;
				case SP_ATTACK_TYPE.SOUL:
					ActAttack(base.playerParameter.spearActionInfo.Soul_AvoidAttackId, true, false, string.Empty);
					return true;
				default:
					return false;
				}
			case ATTACK_MODE.PAIR_SWORDS:
				if (base.spAttackType == SP_ATTACK_TYPE.NONE || base.spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					ActAttack(20, true, false, string.Empty);
					return true;
				}
				return false;
			default:
				return false;
			}
		}
		return false;
	}

	public bool CheckAttackNext()
	{
		if (!base.enableAttackNext)
		{
			return false;
		}
		ATTACK_MODE attackMode = base.attackMode;
		if (attackMode == ATTACK_MODE.PAIR_SWORDS && base.spAttackType == SP_ATTACK_TYPE.SOUL)
		{
			ActAttack(base.playerParameter.pairSwordsActionInfo.Soul_AttackNextId, true, false, string.Empty);
			return true;
		}
		return false;
	}

	private void UpdateSpearJumpEffect()
	{
		if (jumpState <= eJumpState.Charge)
		{
			float chargingRate = GetChargingRate();
			if (chargingRate >= 1f)
			{
				EffectCtrl component = arrowAimLesserCursorEffect.GetComponent<EffectCtrl>();
				if (!object.ReferenceEquals(component, null))
				{
					int num = CheckGaugeLevel();
					component.Play("LEVEL" + num);
				}
				jumpState = eJumpState.Charged;
			}
		}
	}

	public override void HitJumpAttack()
	{
		taskChecker.OnJump();
		base.HitJumpAttack();
	}

	protected override void _JumpRize()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		UseSpGauge();
		OnJumpRize(arrowAimLesserCursorPos, useGaugeLevel);
		if (!base.isArrowAimKeep && base.isArrowAimLesserMode)
		{
			SetArrowAimLesserMode(false);
		}
		base.isArrowAimable = false;
		base.isArrowAimKeep = false;
		base.isArrowAimEnd = false;
		MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
	}

	protected override bool StartBoostMode()
	{
		bool flag = base.StartBoostMode();
		if (flag)
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				taskChecker.OnSoulOneHandSword();
				if (MonoBehaviourSingleton<InGameManager>.IsValid())
				{
					MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulOneHandSword();
				}
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				taskChecker.OnSoulTwoHandSword();
				if (MonoBehaviourSingleton<InGameManager>.IsValid())
				{
					MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulTwoHandSword();
				}
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.SOUL))
			{
				taskChecker.OnSoulSpear();
				if (MonoBehaviourSingleton<InGameManager>.IsValid())
				{
					MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulSpear();
				}
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
			{
				taskChecker.OnSoulArrow();
				if (MonoBehaviourSingleton<InGameManager>.IsValid())
				{
					MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulArrow();
				}
			}
		}
		return flag;
	}

	public float GetIgnoreTargetHeight()
	{
		if (!object.ReferenceEquals(base.weaponInfo, null))
		{
			if (!base.weaponInfo.ignoreTargetHeightsByType.IsNullOrEmpty() && base.weaponInfo.ignoreTargetHeightsByType.Length > (int)base.spAttackType && base.weaponInfo.ignoreTargetHeightsByType[(int)base.spAttackType] > 0f)
			{
				return base.weaponInfo.ignoreTargetHeightsByType[(int)base.spAttackType];
			}
			return base.weaponInfo.ignoreTargetHeight;
		}
		return 100f;
	}

	public bool ExecEvolve()
	{
		if (!IsEvolveWeapon() || !evolveCtrl.IsGaugeFull() || evolveCtrl.isExec)
		{
			return false;
		}
		if (!IsChangeableAction((ACTION_ID)36) && !EnableActionUseEvolve())
		{
			return false;
		}
		ActEvolve();
		return true;
	}

	private bool EnableActionUseEvolve()
	{
		if (base.actionID == (ACTION_ID)29)
		{
			return true;
		}
		return false;
	}

	public void CancelHit()
	{
		if (base.attackID == base.playerParameter.ohsActionInfo.Soul_AlteredSpAttackId)
		{
			snatchCtrl.Cancel();
			SetNextTrigger(1);
		}
	}

	protected override bool GetTargetPos(out Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		pos = Vector3.get_zero();
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && base.targetPointWithSpWeak != null)
		{
			pos = base.targetPointWithSpWeak.param.markerPos;
			return true;
		}
		return base.GetTargetPos(out pos);
	}

	public bool IsChangeableAction(ACTION_ID action_id, int _motionId)
	{
		if (_motionId <= 0)
		{
			return IsChangeableAction(action_id);
		}
		if (action_id == ACTION_ID.ATTACK && IsBurstTwoHandSword() && _motionId == base.playerParameter.twoHandSwordActionInfo.burstTHSInfo.FirstReloadActionAttackID)
		{
			return thsCtrl != null && thsCtrl.IsChangebleReloadAction();
		}
		return IsChangeableAction(action_id);
	}

	public bool isMultiLockMax()
	{
		int multiLockMax = GetMultiLockMax();
		int multiLockNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum();
		return multiLockNum >= multiLockMax;
	}

	private int GetMultiLockMax()
	{
		return (!base.isBoostMode) ? base.playerParameter.arrowActionInfo.soulLockMax : base.playerParameter.arrowActionInfo.soulBoostLockMax;
	}

	private void CheckMultiLock(Vector3 start, Vector3 dir)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
		{
			int multiLockMax = GetMultiLockMax();
			int multiLockNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum();
			RaycastHit val = default(RaycastHit);
			if (multiLockNum < multiLockMax && Physics.Raycast(start, dir, ref val, base.playerParameter.arrowActionInfo.soulRaycastDistance, 16))
			{
				MultiLockMarker component = val.get_transform().GetComponent<MultiLockMarker>();
				if (!(component == null) && component.Lock(multiLockNum, base.isBoostMode))
				{
					multiLockNum++;
					SoundManager.PlayOneShotSE(base.playerParameter.arrowActionInfo.soulLockSeId, this, FindNode(string.Empty));
					int targetMarkerNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerNum();
					if (multiLockNum >= multiLockMax || multiLockNum >= targetMarkerNum)
					{
						MonoBehaviourSingleton<TargetMarkerManager>.I.HideMultiLock();
						EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("R_Wep").get_transform().get_position(), Quaternion.get_identity(), false);
						SoundManager.PlayOneShotSE(base.playerParameter.arrowActionInfo.soulLockMaxSeId, this, FindNode(string.Empty));
						SetBulletLineColor(true);
					}
				}
			}
		}
	}

	public void CheckMultiLock(MultiLockMarker m)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (!(m == null) && MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
		{
			int multiLockMax = GetMultiLockMax();
			int multiLockNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum();
			if (multiLockNum < multiLockMax && m.Lock(multiLockNum, base.isBoostMode))
			{
				multiLockNum++;
				SoundManager.PlayOneShotSE(base.playerParameter.arrowActionInfo.soulLockSeId, this, FindNode(string.Empty));
				int targetMarkerNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerNum();
				if (multiLockNum >= multiLockMax || multiLockNum >= targetMarkerNum)
				{
					MonoBehaviourSingleton<TargetMarkerManager>.I.HideMultiLock();
					EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("R_Wep").get_transform().get_position(), Quaternion.get_identity(), false);
					SoundManager.PlayOneShotSE(base.playerParameter.arrowActionInfo.soulLockMaxSeId, this, FindNode(string.Empty));
					SetBulletLineColor(true);
				}
			}
		}
	}

	public void SetBulletLineColor(bool isMax)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Color val = (!isMax) ? ((base.spAttackType != SP_ATTACK_TYPE.SOUL) ? base.playerParameter.arrowActionInfo.bulletLineColor : base.playerParameter.arrowActionInfo.bulletLineColorSoul) : base.playerParameter.arrowActionInfo.bulletLineColorSoulFull;
		bulletLineRenderer.SetColors(val, val);
	}

	public void CheckWaveMatchAutoRevive()
	{
		if (MonoBehaviourSingleton<UISpectatorButton>.IsValid() && MonoBehaviourSingleton<UISpectatorButton>.I.IsEnable() && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			ActDeadStandup(base.hpMax, eContinueType.REACH_NEXT_WAVE);
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.DoEnable();
			}
			MonoBehaviourSingleton<UISpectatorButton>.I.EndSpect();
			MonoBehaviourSingleton<UISkillButtonGroup>.I.DoEnable();
			MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
		}
	}

	public void SetSpearCursorPos(Vector3 vec)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		arrowAimLesserCursorPos = vec;
	}

	public Vector3 GetArrowAimLesserCursorEffect()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (arrowAimLesserCursorEffect != null)
		{
			return arrowAimLesserCursorEffect.get_transform().get_position();
		}
		return Vector3.get_zero();
	}

	public void SwitchAutoBattle(bool isTurnOn)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (isTurnOn && !isAutoMode)
		{
			Object.DestroyImmediate(base.controller);
			base.controller = this.get_gameObject().AddComponent<AutoSelfController>();
		}
		else if (!isTurnOn && isAutoMode)
		{
			Object.DestroyImmediate(base.controller);
			base.controller = this.get_gameObject().AddComponent<SelfController>();
		}
	}
}
