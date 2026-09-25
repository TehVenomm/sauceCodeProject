// Decompiled with JetBrains decompiler
// Type: Self
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Self : Player
{
  protected Vector3 arrowAimLineVec = Vector3.forward;
  protected Vector2 arrowAimInputVec = Vector2.zero;
  protected Vector2 arrowAimInputOffset = Vector2.zero;
  protected GameObject arrowAimLesserCursorEffect;
  protected float arrowAimLesserCursorStartTime = -1f;
  private static readonly int lineSize = 3;
  private static readonly Vector3 lineOffset = new Vector3(0.5f, 0.0f, 0.0f);
  private const int lineDivide = 30;
  public TaskChecker taskChecker = new TaskChecker();
  private static readonly float STATUS_SYNC_INTERVAL = 5f;
  private float statusSyncTime = Self.STATUS_SYNC_INTERVAL;
  private int syncHp;
  private GameObject inkSplashEffect_;
  private float inkSplashTimerMax_;
  private float inkSplashTimer_;
  private float reduceTimeByFlick_;
  public const string EFFECT_NAME_INK_SPLASH = "ef_btl_pl_blind_01";
  public const string EFFECT_NAME_INK_FLICK = "ef_btl_pl_blind_02";
  public const string EFFECT_NAME_FLICK_WARNING = "ef_btl_target_flick";
  private static readonly Vector3 INKSPLASH_LANDSCAPE_OFFSET = new Vector3(0.0f, -0.15f, 0.0f);
  private EffectCtrl inkSplashCtrl_;
  private int inkSplashState_;
  private GameObject effectFlickWarning_;
  public const string EFFECT_NAME_BLIND = "ef_btl_pl_darkness_02";
  private Transform _blindEffect;
  private float _blindTimer;
  public Vector3 cannonAimForward = Vector3.zero;
  private Vector3 cannonAimEuler = Vector3.zero;
  private float baseEulerX;
  private bool isPlayingRotateSE;
  private Vector2 baseInputVec = Vector2.zero;
  private bool isBaseInputVec;
  private const float CANNON_DRAG_RATE_Y = 15f;
  private const float CANNON_ANGLE_LIMT_UP = 30f;
  private const float CANNON_ANGLE_LIMIT_DOWN = 10f;
  public float overSpeedLimit = 2f;
  public float dragRateX = 2f;
  private Transform effectTapTrans;
  private bool isFirstRideCannon = true;
  private CapsuleCollider _capsuleCollider;
  private Vector3 colliderCenter = Vector3.zero;
  private bool lastAerialFlag;

  public Vector3 arrowAimForward { get; protected set; }

  public Vector3 arrowTmpFoward { get; private set; }

  public int arrowAimStartSign { get; private set; }

  public int arrowAimSign { get; set; }

  public Vector3 arrowAimLesserCursorPos { get; protected set; }

  public List<LineRenderer> bulletLineRenderers { get; protected set; }

  public GatherPointObject nearGatherPoint { get; set; }

  public IFieldGimmickObject nearFieldGimmick { get; set; }

  protected override void Awake()
  {
    base.Awake();
    this.arrowAimForward = Vector3.zero;
    this.arrowAimLesserCursorPos = Vector3.zero;
    this.bulletLineRenderers = (List<LineRenderer>) null;
    this.objectType = StageObject.OBJECT_TYPE.SELF;
  }

  protected override void Clear() => base.Clear();

  public override void Load(PlayerLoadInfo load_info, PlayerLoader.OnCompleteLoad callback = null)
  {
    base.Load(load_info, callback);
    this.ResetShadowSealingUI();
    this.ResetConcussionUI();
  }

  protected override void LoadUniqueEquipment(
    StageObjectManager.CreatePlayerInfo info,
    PlayerLoader.OnCompleteLoad callback)
  {
    base.LoadUniqueEquipment(info, callback);
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.ChangeUniqueEquipment();
    if (!MonoBehaviourSingleton<InGameManager>.IsValid() || this.weaponEquipItemDataList.IsNullOrEmpty<EquipItemTable.EquipItemData>())
      return;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ResetItemDamageByWeapon(this.weaponEquipItemDataList);
  }

  public override void OnLoadComplete()
  {
    base.OnLoadComplete();
    if (Object.op_Inequality((Object) this.stepCtrl, (Object) null))
      this.stepCtrl.stampDistance = 1000f;
    if (this.isDead && (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && (double) this.rescueTime <= 0.0)
      this.BeginSpect();
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      UIPlayerStatus.OnLoadComplete();
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.UpdateBurstUIInfo();
    this._capsuleCollider = this._collider as CapsuleCollider;
  }

  private void OnScreenRotate(bool is_portrait)
  {
    if (!this.IsInkSplash())
      return;
    this.RotateInkSplash(is_portrait);
  }

  protected override void OnDisable()
  {
    for (int index = 0; index < this.bulletLineRenderers.Count; ++index)
      Object.Destroy((Object) ((Component) this.bulletLineRenderers[index]).gameObject);
    this.bulletLineRenderers.Clear();
    base.OnDisable();
  }

  protected override void OnDestroy()
  {
    if (this.IsInkSplash())
      this.InkSplashEnd();
    this.DestroyBlindEffect();
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.soulEnergyCtrl = (SoulEnergyController) null;
    base.OnDestroy();
  }

  public void OnCached()
  {
    this.InkSplashEnd();
    this.DestroyBlindEffect();
  }

  protected override void Initialize()
  {
    base.Initialize();
    if (this.bulletLineRenderers == null)
      this.bulletLineRenderers = new List<LineRenderer>();
    if (this.bulletLineRenderers.Count == 0)
    {
      for (int index = 0; index < Self.lineSize; ++index)
      {
        Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.bulletLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
        if (!Object.op_Equality((Object) transform, (Object) null))
        {
          LineRenderer component = ((Component) transform).GetComponent<LineRenderer>();
          if (!Object.op_Equality((Object) component, (Object) null))
          {
            ((Renderer) component).enabled = false;
            component.startColor = Color.yellow;
            component.endColor = Color.yellow;
            ((Renderer) component).material.renderQueue = 3001;
            this.bulletLineRenderers.Add(component);
          }
          else
            break;
        }
        else
          break;
      }
    }
    this.soulEnergyCtrl = new SoulEnergyController();
    this.soulEnergyCtrl.Initialize((Player) this);
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIPlayerStatus>.I.targetPlayer, (Object) this))
      MonoBehaviourSingleton<UIPlayerStatus>.I.SetTarget((Player) this);
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIEnduranceStatus>.I.targetPlayer, (Object) this))
      MonoBehaviourSingleton<UIEnduranceStatus>.I.SetTarget((Player) this);
    if (MonoBehaviourSingleton<UISkillButtonGroup>.IsValid())
      MonoBehaviourSingleton<UISkillButtonGroup>.I.SetTarget((Player) this);
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    if (!MonoBehaviourSingleton<InGameManager>.IsValid() || this.weaponEquipItemDataList.IsNullOrEmpty<EquipItemTable.EquipItemData>())
      return;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ResetItemDamageByWeapon(this.weaponEquipItemDataList);
  }

  protected override void Update()
  {
    base.Update();
    if (MonoBehaviourSingleton<UIContinueButton>.IsValid() && !((Component) MonoBehaviourSingleton<UIContinueButton>.I).gameObject.activeSelf && MonoBehaviourSingleton<UIContinueButton>.I.CheckVisible() && Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) this))
    {
      bool flag = false;
      if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
        flag = true;
      if (!flag)
      {
        MonoBehaviourSingleton<UIContinueButton>.I.Initialize();
        if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
          MonoBehaviourSingleton<UIContinueButton>.I.SetContinueButton(MonoBehaviourSingleton<InGameManager>.I.CanRushPayContinue());
        else if (QuestManager.IsValidInGameWaveMatch(true))
          MonoBehaviourSingleton<UIContinueButton>.I.SetContinueButton(false);
      }
    }
    if (this.IsNeedActivateUIButtonsForRush() && MonoBehaviourSingleton<InGameProgress>.IsValid())
      MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
    if (this.isArrowAimBossMode)
      this.UpdateBulletLine();
    if (QuestManager.IsValidInGameExplore())
    {
      this.statusSyncTime -= Time.deltaTime;
      if ((double) this.statusSyncTime <= 0.0 && this.syncHp != this.hp)
        this.SendExploreSyncPlayerStatus();
    }
    if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop && MonoBehaviourSingleton<UISpectatorButton>.I.IsEnable())
    {
      if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
        MonoBehaviourSingleton<UIPlayerStatus>.I.DoEnable();
      if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
        MonoBehaviourSingleton<UIEnduranceStatus>.I.DoEnable();
      MonoBehaviourSingleton<UISpectatorButton>.I.EndSpect();
      MonoBehaviourSingleton<UISkillButtonGroup>.I.DoEnable();
    }
    if (this.IsInkSplash())
      this.UpdateInkSplash();
    this.UpdateBlind();
  }

  protected override void FixedUpdate()
  {
    base.FixedUpdate();
    this.UpdateAerialCollider();
  }

  protected override void UpdateAction()
  {
    base.UpdateAction();
    switch (this.actionID)
    {
      case (Character.ACTION_ID) 24:
        if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() && !this.IsAutoReviving())
          this.OnEndContinueTimeEnd();
        if (!QuestManager.IsValidInGameTrial() || this.IsAutoReviving())
          break;
        this.OnEndContinueTimeEnd();
        break;
    }
  }

  public void OnTap()
  {
    this.soulEnergyCtrl.Tap();
    if (!this.isSpearHundred)
      return;
    this.spearHundredSecFromLastTap = 0.0f;
  }

  private bool IsNeedActivateUIButtonsForRush()
  {
    return MonoBehaviourSingleton<UIContinueButton>.IsValid() && !MonoBehaviourSingleton<UIContinueButton>.I.IsEnableButtonAll && MonoBehaviourSingleton<InGameManager>.IsValid() && (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && (double) this.continueTime <= 0.0 && this.isDead && this.actionID == (Character.ACTION_ID) 24;
  }

  public override void ActAttack(
    int id,
    bool send_packet = true,
    bool sync_immediately = false,
    string _motionLayerName = "",
    string _motionStateName = "")
  {
    if (this.isArrowAimBossMode && this.isArrowAimKeep)
    {
      this.arrowAimInputOffset = Vector2.op_Addition(this.arrowAimInputOffset, this.arrowAimInputVec);
      this.arrowAimInputVec = Vector2.zero;
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT) && id == 98)
    {
      this.taskChecker.OnHeatPairSwords();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnHeatPairSwords();
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && id == this.playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId && this.pairSwordsCtrl.IsComboLvMax())
    {
      this.taskChecker.OnSoulPairSwords();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulPairSwords();
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.BURST) && id == this.playerParameter.spearActionInfo.burstSpearInfo.hitComboAttackId)
    {
      this.taskChecker.OnBurstSpear();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnBurstSpear();
    }
    base.ActAttack(id, send_packet, sync_immediately, _motionLayerName, _motionStateName);
    if (!MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      return;
    MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true);
  }

  public override void SetChargeRelease(float charge_rate)
  {
    MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true);
    if (this.isArrowAimBossMode)
    {
      Vector3 velocity = Quaternion.op_Multiply(Quaternion.LookRotation(this.arrowAimForward), this.arrowAimLineVec);
      velocity.y = 0.0f;
      this.SetLerpRotation(velocity);
    }
    base.SetChargeRelease(charge_rate);
  }

  public override void SetChargeExpandRelease(float chargeRate)
  {
    MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true);
    base.SetChargeExpandRelease(chargeRate);
  }

  public override void SetInputAxis(Vector2 input_vec)
  {
    int num1 = this.startInputRotate ? 1 : 0;
    base.SetInputAxis(input_vec);
    int num2 = this.startInputRotate ? 1 : 0;
    if (num1 == num2)
      return;
    MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false);
    MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetDisable(true);
  }

  public override void ActDead(bool force_sync = false, bool recieve_direct = false)
  {
    base.ActDead(force_sync, recieve_direct);
    this.taskChecker.OnDeath();
    this.soulEnergyCtrl.Sleep();
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.CloseDialog();
    MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(true);
  }

  public override void ActDeadStandup(int standup_hp, Player.eContinueType cType)
  {
    base.ActDeadStandup(standup_hp, cType);
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || cType != Player.eContinueType.AUTO_REVIVE)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.SetDisableUIOpen(false);
  }

  public override void ActParalyze()
  {
    base.ActParalyze();
    if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
  }

  public override void OnEndContinueTimeEnd()
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
      return;
    base.OnEndContinueTimeEnd();
    if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && !MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
    {
      this.BeginSpect();
    }
    else
    {
      if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
      if (MonoBehaviourSingleton<InGameProgress>.IsValid() && QuestManager.IsValidInGameTrial())
      {
        MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
      }
      else
      {
        if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
          return;
        if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsForceDefeatQuest())
        {
          MonoBehaviourSingleton<InGameProgress>.I.BattleForceDefeatsEvent();
        }
        else
        {
          if (MonoBehaviourSingleton<InGameProgress>.I.isWaitContinueProtocol)
            return;
          MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
        }
      }
    }
  }

  public override bool ApplyGather()
  {
    int num = base.ApplyGather() ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.targetGatherPoint.Gather();
    this.isGatherInterruption = false;
    return num != 0;
  }

  public override bool ActSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
  {
    bool flag = base.ActSkillAction(skill_index);
    SkillInfo.SkillParam actSkillParam = this.skillInfo.actSkillParam;
    if (actSkillParam == null)
      return false;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddMySkillCount((int) actSkillParam.tableData.id);
    if (flag)
    {
      if (actSkillParam.tableData.type == SKILL_SLOT_TYPE.ATTACK && (!actSkillParam.tableData.isTeleportation || !this.isArrowAimLesserMode))
        MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true);
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
        MonoBehaviourSingleton<InGameProgress>.I.OnSkillUse(this.skillInfo.actSkillParam);
      this.taskChecker.OnUseMagi();
    }
    return flag;
  }

  public override int ExecHealHp(Character.HealData healData, bool isPacket = false)
  {
    int damage = base.ExecHealHp(healData, isPacket);
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
      MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerRecoverHp((Character) this, damage, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
    return damage;
  }

  public override void ApplyChangeWeapon(
    CharaInfo.EquipItem item,
    int weapon_index,
    StageObjectManager.CreatePlayerInfo createPlayerInfo = null)
  {
    base.ApplyChangeWeapon(item, weapon_index, createPlayerInfo);
    if (QuestManager.IsValidInGameExplore())
      this.SendExploreSyncPlayerStatus();
    if (!MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
  }

  protected override void EndAction()
  {
    bool arrowAimLesserMode = this.isArrowAimLesserMode;
    Character.ACTION_ID actionId = this.actionID;
    base.EndAction();
    if (actionId == Character.ACTION_ID.PARALYZE && MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
    for (int index = 0; index < this.bulletLineRenderers.Count; ++index)
      ((Renderer) this.bulletLineRenderers[index]).enabled = false;
    if (!MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      return;
    MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetDisable(false);
    if (arrowAimLesserMode)
      MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, this.playerParameter.specialActionInfo.arrowAimLesserUnlockTime);
    else if (MonoBehaviourSingleton<TargetMarkerManager>.I.parameter != null)
      MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, MonoBehaviourSingleton<TargetMarkerManager>.I.parameter.defaultUnlockTime);
    else
      MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false);
    this.enableCancelToCarryPut = false;
  }

  public override bool OnBuffStart(BuffParam.BuffData buffData)
  {
    if (!base.OnBuffStart(buffData))
      return false;
    if (buffData.type == BuffParam.BUFFTYPE.BLIND)
      this.CreateBlindEffect(buffData.time);
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
    if (QuestManager.IsValidInGameExplore())
      this.SendExploreSyncPlayerStatus();
    return true;
  }

  protected override void OnUIBuffRoutine(BuffParam.BUFFTYPE type, int value)
  {
    switch (type)
    {
      case BuffParam.BUFFTYPE.REGENERATE:
      case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
        if (!MonoBehaviourSingleton<UIDamageManager>.IsValid())
          break;
        MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerRecoverHp((Character) this, value, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
        break;
      case BuffParam.BUFFTYPE.POISON:
      case BuffParam.BUFFTYPE.BURNING:
      case BuffParam.BUFFTYPE.DEADLY_POISON:
      case BuffParam.BUFFTYPE.BLEEDING:
      case BuffParam.BUFFTYPE.ACID:
        if (!MonoBehaviourSingleton<UIDamageManager>.IsValid())
          break;
        MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage((Character) this, value, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
        break;
    }
  }

  public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
  {
    if (!base.OnBuffEnd(type, sync, isPlayEndEffect))
      return false;
    switch (type)
    {
      case BuffParam.BUFFTYPE.INK_SPLASH:
        this.InkSplashEnd();
        break;
      case BuffParam.BUFFTYPE.BLIND:
        this.DestroyBlindEffect();
        break;
    }
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.UpDateStatusIcon();
    if (QuestManager.IsValidInGameExplore())
      this.SendExploreSyncPlayerStatus();
    return true;
  }

  public override void OnInkSplash(InkSplashInfo info)
  {
    base.OnInkSplash(info);
    this.InkSplashStart(info);
  }

  public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    base.OnAttackedHitOwner(status);
    if (!status.attackInfo.toPlayer.isBuffCancellation || this.IsInBarrier() || !MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(string.Empty, STRING_CATEGORY.ENEMY_REACTION, 3U);
  }

  public override void OnHitAttack(
    AttackHitInfo info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    base.OnHitAttack(info, hit_param);
    if (hit_param.toObject is Enemy)
    {
      Enemy toObject = hit_param.toObject as Enemy;
      if (toObject.isSummonAttack || toObject.regionInfos.Length > hit_param.regionID && !toObject.regionInfos[hit_param.regionID].isAtkColliderHit)
        return;
    }
    if (info.attackType != AttackHitInfo.ATTACK_TYPE.OHS_ORACLE_SP)
      return;
    this.taskChecker.OnOracleOneHandSword();
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnOracleOneHandSword();
  }

  protected override void OnAttackFromHitDirection(
    AttackedHitStatusDirection status,
    StageObject to_object)
  {
    base.OnAttackFromHitDirection(status, to_object);
    if ((double) status.attackInfo.shakeCameraPercent == 0.0 || to_object.objectType != StageObject.OBJECT_TYPE.ENEMY || !MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(status.hitPos, status.attackInfo.shakeCameraPercent, status.attackInfo.shakeCycleTime);
  }

  protected override bool IsDamageValid(AttackedHitStatusDirection status)
  {
    return status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED || base.IsDamageValid(status);
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    if (TutorialStep.HasAllTutorialCompleted() && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.GACHA_QUEST_WIN))
      return;
    base.OnAttackedHitFix(status);
    if ((double) status.attackInfo.shakeCameraPercent != 0.0 && status.fromType == StageObject.OBJECT_TYPE.ENEMY && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(status.hitPos, status.attackInfo.shakeCameraPercent, status.attackInfo.shakeCycleTime);
    if (status.damage + status.shieldDamage <= 0 && (status.damage + status.shieldDamage != 0 || !this.shouldShowInvincibleDamage))
      return;
    if (Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null))
    {
      this.uiPlayerStatusGizmo.OnDamageSelf();
      if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
        MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage((Character) this, status.origin);
    }
    this.shouldShowInvincibleDamage = false;
  }

  public override Vector3 GetCameraTargetPos()
  {
    Vector3 cameraTargetPos = base.GetCameraTargetPos();
    if (this.isArrowAimLesserMode)
      cameraTargetPos = Vector3.op_Addition(cameraTargetPos, this.arrowAimLesserCursorPos);
    return cameraTargetPos;
  }

  public override bool CanPlayEffectEvent() => true;

  public override void OnAnimEvent(AnimEventData.EventData data)
  {
    switch (data.id)
    {
      case AnimEventFormat.ID.COMBO_INPUT_ON:
        SelfController controller = this.controller as SelfController;
        if (Object.op_Inequality((Object) controller, (Object) null) && controller.nextCommand != null && controller.nextCommand.type == SelfController.COMMAND_TYPE.ATTACK)
        {
          controller.CancelInput();
          break;
        }
        break;
      case AnimEventFormat.ID.CHARGE_INPUT_START:
        if (!MonoBehaviourSingleton<TargetMarkerManager>.I.parameter.enableNormalMarker)
        {
          MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false, MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarker.defaultUnlockTime);
          break;
        }
        break;
      case AnimEventFormat.ID.TARGET_LOCK_ON:
        MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(true);
        return;
      case AnimEventFormat.ID.TARGET_LOCK_OFF:
        MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false);
        return;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_FULL_BURST:
        if (this.thsCtrl != null)
        {
          if (!this.thsCtrl.DoFullBurst())
            return;
          this.taskChecker.OnBurstTwoHandSword();
          if (MonoBehaviourSingleton<InGameManager>.IsValid())
            MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnBurstTwoHandSword();
          if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
            MonoBehaviourSingleton<UIPlayerStatus>.I.DoFullBurstAction();
          if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
            return;
          MonoBehaviourSingleton<UIEnduranceStatus>.I.DoFullBurstAction();
          return;
        }
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_SINGLE_SHOT:
        if (this.thsCtrl != null)
        {
          if (!this.thsCtrl.DoShootAction())
            return;
          if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
            MonoBehaviourSingleton<UIPlayerStatus>.I.DoShootAction();
          if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
            return;
          MonoBehaviourSingleton<UIEnduranceStatus>.I.DoShootAction();
          return;
        }
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_RELOAD_NOW:
        if (this.thsCtrl != null && this.thsCtrl.IsReloadingNow)
        {
          if (!this.thsCtrl.DoReloadAction() || !MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
            return;
          MonoBehaviourSingleton<UIPlayerStatus>.I.DoReloadAction();
          return;
        }
        break;
      case AnimEventFormat.ID.CAMERA_RESET_POSITION:
        MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
        return;
    }
    base.OnAnimEvent(data);
  }

  protected override void EventCameraTargetOffsetOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || FieldManager.IsValidInGameNoBoss())
      return;
    float[] floatArgs = data.floatArgs;
    InGameCameraManager.TargetOffset targetOffset = new InGameCameraManager.TargetOffset();
    targetOffset.pos = new Vector3(floatArgs[0], floatArgs[1], floatArgs[2]);
    targetOffset.rot = new Vector3(floatArgs[3], floatArgs[4], floatArgs[5]);
    if (floatArgs.Length > 6)
      targetOffset.smoothMaxSpeed = floatArgs[6];
    MonoBehaviourSingleton<InGameCameraManager>.I.SetAnimEventTargetOffsetByPlayer(targetOffset);
  }

  protected override void EventCameraTargetOffsetOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByPlayer();
  }

  public override void EventCameraTargetRotateOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || StageObjectManager.IsBossAssimilated)
      return;
    InGameCameraManager.TargetPosition targetPosition = new InGameCameraManager.TargetPosition();
    targetPosition.pos = data.intArgs[0] <= 0 ? Vector3.op_Addition(this._position, new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2])) : Vector3.op_Addition(MonoBehaviourSingleton<StageObjectManager>.I.boss._position, new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]));
    if (data.floatArgs.Length > 3)
      targetPosition.smoothMaxSpeed = data.floatArgs[3];
    MonoBehaviourSingleton<InGameCameraManager>.I.SetAnimEventTargetPositionByPlayer(targetPosition);
  }

  public override void EventCameraTargetRotateOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetPositionByPlayer();
  }

  protected override void EventCameraStopOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || FieldManager.IsValidInGameNoBoss() || data.floatArgs.Length < 6)
      return;
    InGameCameraManager i = MonoBehaviourSingleton<InGameCameraManager>.I;
    Vector3 pos = Vector3.op_Addition(i.movePosition, new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]));
    Quaternion moveRotation = i.moveRotation;
    Vector3 rotEular = Vector3.op_Addition(((Quaternion) ref moveRotation).eulerAngles, new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
    if (data.floatArgs.Length >= 7)
      i.SetStopMaxSpeed(data.floatArgs[6]);
    if (data.floatArgs.Length >= 8)
      i.SetStopMaxRotSpeed(data.floatArgs[7]);
    if (data.intArgs.Length >= 1 && data.intArgs[0] != 0)
    {
      pos = Vector3.op_Addition(this._position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2])));
      Quaternion quaternion = Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5])));
      rotEular = ((Quaternion) ref quaternion).eulerAngles;
    }
    i.SetStopPos(pos);
    i.SetStopRotEular(rotEular);
    i.SetCameraMode(InGameCameraManager.CAMERA_MODE.STOP);
  }

  protected override void EventCameraStopOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || !MonoBehaviourSingleton<InGameCameraManager>.I.IsCameraMode(InGameCameraManager.CAMERA_MODE.STOP))
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
  }

  protected override void EventCameraCutOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || FieldManager.IsValidInGameNoBoss() || data.floatArgs.Length < 6)
      return;
    Vector3 vector3_1 = Vector3.op_Addition(this._position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2])));
    Vector3 vector3_2 = Vector3.op_Addition(this._position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), new Vector3(-data.floatArgs[0], data.floatArgs[1], data.floatArgs[2])));
    Quaternion cutRot1 = Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5])));
    Quaternion cutRot2 = Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), Quaternion.Euler(new Vector3(data.floatArgs[3], -data.floatArgs[4], data.floatArgs[5])));
    RaycastHit hit1 = new RaycastHit();
    RaycastHit hit2 = new RaycastHit();
    bool flag1 = AIUtility.RaycastObstacle((StageObject) this, vector3_1, out hit1);
    bool flag2 = AIUtility.RaycastObstacle((StageObject) this, vector3_2, out hit2);
    if (flag1 & flag2)
      return;
    InGameCameraManager i = MonoBehaviourSingleton<InGameCameraManager>.I;
    if (!flag1)
    {
      i.SetCutPos(vector3_1);
      i.SetCutRot(cutRot1);
    }
    else if (!flag2)
    {
      i.SetCutPos(vector3_2);
      i.SetCutRot(cutRot2);
    }
    i.SetCameraMode(InGameCameraManager.CAMERA_MODE.CUT);
  }

  protected override void EventCameraCutOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearCameraMode(InGameCameraManager.CAMERA_MODE.CUT);
  }

  public override void ShotArrow(
    Vector3 shot_pos,
    Quaternion shot_rot,
    AttackInfo attack_info,
    bool isSitShot,
    bool isAimEnd,
    bool isSend = false)
  {
    if (attack_info == null)
      return;
    base.ShotArrow(shot_pos, shot_rot, attack_info, isSitShot, isAimEnd, isSend);
    bool flag = isSitShot && this.spAttackType == SP_ATTACK_TYPE.BURST;
    if (!this.isArrowAimBossMode || flag)
      return;
    this.SetArrowAimBossVisible(false);
  }

  public override void ShotSoulArrow()
  {
    base.ShotSoulArrow();
    if (this.isArrowAimBossMode)
      this.SetArrowAimBossVisible(false);
    MonoBehaviourSingleton<TargetMarkerManager>.I.ResetMultiLock();
  }

  public override Vector3 GetBulletShotVec(Vector3 appear_pos)
  {
    if (!this.isArrowAimBossMode && (!this.isArrowAimLesserMode || !Object.op_Equality((Object) this.targetingPoint, (Object) null)))
      return base.GetBulletShotVec(appear_pos);
    Vector3 vector3 = Quaternion.op_Multiply(Quaternion.LookRotation(this.arrowAimForward), this.arrowAimLineVec);
    return ((Vector3) ref vector3).normalized;
  }

  public override void SetArrowAimBossMode(bool enable)
  {
    if (enable)
    {
      if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
        this.LookAt(this.actionTarget._position, true);
      else if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
        this.LookAt(((Component) MonoBehaviourSingleton<StageObjectManager>.I.boss).transform.position, false);
      this.isActSpecialAction = true;
    }
    else
    {
      this.targetingPointList.Clear();
      if (Object.op_Inequality((Object) this.targetAimAfeterPoint, (Object) null))
        this.targetingPointList.Add(this.targetAimAfeterPoint);
    }
    this.arrowTmpFoward = this._forward;
    this.arrowAimForward = this._forward;
    this.arrowAimLineVec = Vector3.forward;
    this.arrowAimInputVec = Vector2.zero;
    this.arrowAimInputOffset = Vector2.zero;
    base.SetArrowAimBossMode(enable);
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    if (enable)
      MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.ARROW_AIM_BOSS);
    else
      MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
  }

  public void SetArrowAimBossModeStartSign(Vector2 moveVec)
  {
    this.arrowAimStartSign = (int) Mathf.Sign(moveVec.x);
  }

  protected override void SetArrowAimBossVisible(bool enable)
  {
    base.SetArrowAimBossVisible(enable);
    if (this.bulletLineRenderers.Count < Self.lineSize)
      return;
    if (enable)
    {
      ((Renderer) this.bulletLineRenderers[0]).enabled = true;
      this.SetBulletLineColor(false);
      this.UpdateBulletLine();
    }
    else
    {
      for (int index = 0; index < Self.lineSize; ++index)
        ((Renderer) this.bulletLineRenderers[index]).enabled = false;
    }
  }

  public void UpdateArrowAimBossMode(Vector2 input_vec, Vector2 input_pos)
  {
    Vector3 bulletAppearPos = this.GetBulletAppearPos();
    Vector3 bulletShotVec = this.GetBulletShotVec(bulletAppearPos);
    this.arrowAimInputVec = input_vec;
    InGameSettingsManager.SelfController.ArrowAimBossSettings arrowAimBossSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimBossSettings;
    float num1 = input_vec.x + this.arrowAimInputOffset.x;
    float num2 = arrowAimBossSettings.dragRateX * Mathf.Sign(num1) * Mathf.Abs(num1);
    float num3 = Mathf.Abs(num2) - arrowAimBossSettings.angleLimitSide;
    if ((double) num3 < 0.0)
      num3 = 0.0f;
    float num4 = Mathf.Clamp(num3 * (Mathf.Sign(num2) * arrowAimBossSettings.overSpeedRate), -arrowAimBossSettings.overSpeedLimit, arrowAimBossSettings.overSpeedLimit);
    float num5 = input_pos.x / (float) Screen.width;
    float num6;
    if ((double) num1 <= 0.0)
    {
      num6 = -1f;
    }
    else
    {
      num6 = 1f;
      num5 = 1f - num5;
    }
    if ((double) num5 < 0.0)
      num5 = 0.0f;
    if ((double) num5 < (double) arrowAimBossSettings.overScreenRate && (double) arrowAimBossSettings.overScreenMargin < (double) arrowAimBossSettings.overScreenRate)
    {
      float num7 = (float) (((double) num5 - (double) arrowAimBossSettings.overScreenMargin) / ((double) arrowAimBossSettings.overScreenRate - (double) arrowAimBossSettings.overScreenMargin));
      if ((double) num7 < 0.0)
        num7 = 0.0f;
      float num8 = (float) ((double) num6 * (double) arrowAimBossSettings.overSpeedLimit * (1.0 - (double) num7));
      if ((double) Mathf.Abs(num8) > (double) Mathf.Abs(num4))
        num4 = num8;
      float num9 = 0.0f;
      if (this.targetingPointList.Count > 0)
      {
        bulletShotVec.y = 0.0f;
        int index = 0;
        for (int count = this.targetingPointList.Count; index < count; ++index)
        {
          Vector3 vector3 = Vector3.op_Subtraction(this.targetingPointList[index].param.markerPos, bulletAppearPos);
          vector3.y = 0.0f;
          float num10 = Vector3.Angle(bulletShotVec, vector3);
          if (index == 0 || (double) num10 < (double) num9)
            num9 = num10;
        }
      }
      float speedAngleMinRange = arrowAimBossSettings.overSpeedAngleMinRange;
      float angleChangeRange = arrowAimBossSettings.overSpeedAngleChangeRange;
      float overSpeedMaxRate = arrowAimBossSettings.overSpeedMaxRate;
      float num11 = (float) (1.0 + ((double) angleChangeRange <= 0.0 ? ((double) num9 - (double) speedAngleMinRange >= 0.0 ? 1.0 : 0.0) : (double) Mathf.Clamp01((num9 - speedAngleMinRange) / angleChangeRange)) * ((double) overSpeedMaxRate - 1.0));
      num4 *= num11;
    }
    float num12 = Mathf.Clamp(num2, -arrowAimBossSettings.angleLimitSide, arrowAimBossSettings.angleLimitSide);
    this.arrowAimForward = Quaternion.op_Multiply(Quaternion.Euler(new Vector3(0.0f, num4, 0.0f)), this.arrowAimForward);
    Vector3 vector3_1 = Vector3.op_Subtraction(MonoBehaviourSingleton<StageObjectManager>.I.boss._position, this._position);
    this.arrowAimSign = (double) Vector3.Cross(this.arrowTmpFoward, ((Vector3) ref vector3_1).normalized).y >= 0.0 ? 1 : -1;
    float num13 = input_vec.y + this.arrowAimInputOffset.y;
    this.arrowAimLineVec = Quaternion.op_Multiply(Quaternion.Euler(Mathf.Clamp(arrowAimBossSettings.dragRateY * -Mathf.Sign(num13) * Mathf.Abs(num13), -arrowAimBossSettings.angleLimitUp, arrowAimBossSettings.angleLimitUp), num12, 0.0f), Vector3.forward);
    this.SetLerpRotation(Quaternion.op_Multiply(Quaternion.Euler(new Vector3(0.0f, num12, 0.0f)), this.arrowAimForward));
  }

  public override void SetArrowAimLesserMode(bool enable)
  {
    if (enable)
    {
      this.arrowAimLesserCursorStartTime = Time.time;
      this.arrowAimLesserCursorPos = Vector3.zero;
      MonoBehaviourSingleton<TargetMarkerManager>.I.SetTargetLock(false);
    }
    else
    {
      this.arrowAimLesserCursorStartTime = -1f;
      this.arrowAimLesserCursorPos = Vector3.zero;
    }
    base.SetArrowAimLesserMode(enable);
  }

  protected override void SetArrowAimLesserVisible(bool enable)
  {
    base.SetArrowAimLesserVisible(enable);
    if (enable)
    {
      if (!Object.op_Equality((Object) this.arrowAimLesserCursorEffect, (Object) null))
        return;
      Transform transform = !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT) ? (!this.isArrowRainShot ? EffectManager.GetEffect(this.playerParameter.specialActionInfo.arrowAimLesserCursorEffectName, this._transform) : EffectManager.GetEffect(this.playerParameter.arrowActionInfo.arrowRainShotAimLesserCursorEffectName, this._transform)) : EffectManager.GetEffect("ef_btl_target_e_01", this._transform);
      if (Object.op_Equality((Object) transform, (Object) null))
        return;
      float num = 2f;
      transform.localScale = Vector3.op_Multiply(Vector3.one, num);
      this.arrowAimLesserCursorEffect = ((Component) transform).gameObject;
      transform.position = Vector3.op_Addition(this._position, this.arrowAimLesserCursorPos);
      Rigidbody rigidbody = ((Component) transform).gameObject.AddComponent<Rigidbody>();
      rigidbody.mass = 1f;
      rigidbody.angularDrag = 100f;
      rigidbody.isKinematic = false;
      rigidbody.constraints = (RigidbodyConstraints) 116;
      rigidbody.collisionDetectionMode = (CollisionDetectionMode) 1;
      CapsuleCollider collider = this._collider as CapsuleCollider;
      if (Object.op_Inequality((Object) collider, (Object) null))
      {
        CapsuleCollider capsuleCollider = ((Component) transform).gameObject.AddComponent<CapsuleCollider>();
        capsuleCollider.direction = collider.direction;
        capsuleCollider.height = collider.height / num;
        capsuleCollider.radius = collider.radius / num;
        capsuleCollider.center = Vector3.op_Division(collider.center, num);
      }
      Utility.SetLayerWithChildren(transform, 29);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.arrowAimLesserCursorEffect, (Object) null))
        return;
      Object.Destroy((Object) this.arrowAimLesserCursorEffect);
      this.arrowAimLesserCursorEffect = (GameObject) null;
    }
  }

  public override void UpdateArrowAimLesserMode(Vector2 input_vec)
  {
    base.UpdateArrowAimLesserMode(input_vec);
    InGameSettingsManager.SelfController.ArrowAimLesserSettings aimLesserSettings;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
    {
      aimLesserSettings = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.spearAimLesserSettings;
      this.UpdateSpearJumpEffect();
    }
    else
      aimLesserSettings = !this.isArrowRainShot ? MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowAimLesserSettings : MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.arrowRainShotAimLesserSettings;
    Rigidbody rigidbody = (Rigidbody) null;
    if (Object.op_Inequality((Object) this.arrowAimLesserCursorEffect, (Object) null))
    {
      rigidbody = this.arrowAimLesserCursorEffect.GetComponent<Rigidbody>();
      if (Object.op_Inequality((Object) rigidbody, (Object) null))
      {
        Vector3 vector3 = Vector3.op_Subtraction(rigidbody.position, this._position);
        vector3.y = 0.0f;
        this.arrowAimLesserCursorPos = vector3;
        Vector3 aimLesserCursorPos1 = this.arrowAimLesserCursorPos;
        if ((double) ((Vector3) ref aimLesserCursorPos1).magnitude >= (double) aimLesserSettings.cursorMaxDistance)
        {
          Vector3 aimLesserCursorPos2 = this.arrowAimLesserCursorPos;
          this.arrowAimLesserCursorPos = Vector3.op_Multiply(((Vector3) ref aimLesserCursorPos2).normalized, aimLesserSettings.cursorMaxDistance);
        }
        rigidbody.position = Vector3.op_Addition(this.arrowAimLesserCursorPos, this._position);
      }
    }
    this.arrowAimForward = !Vector3.op_Equality(this.arrowAimLesserCursorPos, Vector3.zero) ? Quaternion.op_Multiply(Quaternion.LookRotation(this.arrowAimLesserCursorPos), Vector3.forward) : this._forward;
    this.arrowAimLineVec = Vector3.forward;
    this.SetLerpRotation(this.arrowAimLesserCursorPos);
    Vector3 right = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.right;
    Vector3 forward = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.forward;
    right.y = 0.0f;
    ((Vector3) ref right).Normalize();
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    Vector3 vector3_1 = Vector3.zero;
    float magnitude = ((Vector2) ref input_vec).magnitude;
    if ((double) magnitude >= (double) aimLesserSettings.dragMinLength && (double) magnitude > 0.0)
    {
      float num1 = magnitude;
      if ((double) num1 >= (double) aimLesserSettings.dragMaxLength)
        num1 = aimLesserSettings.dragMaxLength;
      float num2 = (num1 - aimLesserSettings.dragMinLength) / (aimLesserSettings.dragMaxLength - aimLesserSettings.dragMinLength);
      input_vec = Vector2.op_Multiply(input_vec, num2 / magnitude);
      Vector3 vector3_2 = Vector3.op_Addition(Vector3.op_Multiply(right, input_vec.x), Vector3.op_Multiply(forward, input_vec.y));
      float num3 = 0.0f;
      if ((double) this.arrowAimLesserCursorStartTime >= 0.0)
      {
        float cursorStartSpeedTime = aimLesserSettings.cursorStartSpeedTime;
        num3 = (cursorStartSpeedTime - (Time.time - this.arrowAimLesserCursorStartTime)) / cursorStartSpeedTime;
        if ((double) num3 < 0.0)
        {
          num3 = 0.0f;
          this.arrowAimLesserCursorStartTime = -1f;
        }
      }
      float num4 = 1f;
      if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
        num4 = aimLesserSettings.cursorTargetingSpeedRate;
      float num5 = (float) ((double) num3 * ((double) aimLesserSettings.cursorStartSpeedRate - 1.0) + 1.0);
      vector3_1 = Vector3.op_Multiply(Vector3.op_Multiply(Vector3.op_Multiply(vector3_2, aimLesserSettings.cursorSpeed), num5), num4);
      if (Object.op_Inequality((Object) this.arrowAimLesserCursorEffect, (Object) null))
      {
        float num6 = 1f;
        this.arrowAimLesserCursorEffect.transform.localScale = Vector3.op_Multiply(Vector3.one, num6 + (float) (((double) aimLesserSettings.cursorScale - (double) num6) * (1.0 - (double) num3)));
      }
    }
    if (!Object.op_Inequality((Object) rigidbody, (Object) null))
      return;
    Vector3 aimLesserCursorPos3 = this.arrowAimLesserCursorPos;
    Vector3 normalized = ((Vector3) ref aimLesserCursorPos3).normalized;
    float num7 = Vector3.Dot(normalized, vector3_1) * Time.deltaTime;
    if ((double) num7 > 0.0)
    {
      Vector3 aimLesserCursorPos4 = this.arrowAimLesserCursorPos;
      float num8 = ((Vector3) ref aimLesserCursorPos4).magnitude + num7;
      if ((double) num8 > (double) aimLesserSettings.cursorMaxDistance)
      {
        float num9 = num8 - aimLesserSettings.cursorMaxDistance;
        vector3_1 = Vector3.op_Subtraction(vector3_1, Vector3.op_Division(Vector3.op_Multiply(normalized, num9), Time.deltaTime));
      }
    }
    rigidbody.velocity = vector3_1;
  }

  protected void UpdateBulletLine()
  {
    if (this.bulletLineRenderers.Count < Self.lineSize || !((Renderer) this.bulletLineRenderers[0]).enabled)
      return;
    Vector3 bulletAppearPos = this.GetBulletAppearPos();
    Vector3 bulletShotVec = this.GetBulletShotVec(bulletAppearPos);
    if (this.spAttackType == SP_ATTACK_TYPE.SOUL)
    {
      this.bulletLineRenderers[0].positionCount = 2;
      this.bulletLineRenderers[0].SetPosition(0, bulletAppearPos);
      this.bulletLineRenderers[0].SetPosition(1, Vector3.op_Addition(bulletAppearPos, Vector3.op_Multiply(bulletShotVec, 30f)));
      this.CheckMultiLock(bulletAppearPos, bulletShotVec);
    }
    else
    {
      AttackInfo attackInfo1 = this.FindAttackInfo(this.playerParameter.arrowActionInfo.attackInfoNames[(int) this.spAttackType], false);
      if (attackInfo1 == null)
        return;
      float chargingRate = this.GetChargingRate();
      if (!string.IsNullOrEmpty(attackInfo1.rateInfoName) && (double) chargingRate != 0.0)
      {
        AttackInfo attackInfo2 = this.FindAttackInfo(attackInfo1.rateInfoName, false);
        attackInfo1 = attackInfo1.GetRateAttackInfo(attackInfo2, chargingRate);
      }
      BulletData bulletData = attackInfo1.bulletData;
      if (Object.op_Equality((Object) bulletData, (Object) null))
        return;
      bool flag1 = bulletData.dataFall == null;
      if ((double) chargingRate >= 1.0 && (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST)))
        flag1 = true;
      bool flag2 = flag1 && this.isBoostMode && this.spAttackType == SP_ATTACK_TYPE.BURST;
      ((Renderer) this.bulletLineRenderers[1]).enabled = flag2;
      ((Renderer) this.bulletLineRenderers[2]).enabled = flag2;
      for (int index1 = 0; index1 < Self.lineSize; ++index1)
      {
        if (((Renderer) this.bulletLineRenderers[index1]).enabled)
        {
          Vector3 vector3_1 = bulletAppearPos;
          switch (index1)
          {
            case 1:
              vector3_1 = Vector3.op_Addition(bulletAppearPos, Self.lineOffset);
              break;
            case 2:
              vector3_1 = Vector3.op_Subtraction(bulletAppearPos, Self.lineOffset);
              break;
          }
          if (flag1)
          {
            this.bulletLineRenderers[index1].positionCount = 2;
            this.bulletLineRenderers[index1].SetPosition(0, vector3_1);
            Vector3 vector3_2 = Vector3.op_Addition(vector3_1, Vector3.op_Multiply(bulletShotVec, 30f));
            this.bulletLineRenderers[index1].SetPosition(1, vector3_2);
          }
          else
          {
            Vector3 vector3_3 = Vector3.op_Multiply(Physics.gravity, bulletData.dataFall.gravityRate);
            float gravityStartTime = bulletData.dataFall.gravityStartTime;
            float speed = bulletData.data.speed;
            this.bulletLineRenderers[index1].positionCount = 30;
            this.bulletLineRenderers[index1].SetPosition(0, vector3_1);
            float num1 = bulletData.data.appearTime / 30f;
            float num2 = 0.0f;
            for (int index2 = 1; index2 < 30; ++index2)
            {
              if ((double) num2 <= (double) gravityStartTime)
                num2 += num1 * 0.1f;
              else
                num2 += num1;
              Vector3 vector3_4 = Vector3.op_Addition(bulletAppearPos, Vector3.op_Multiply(Vector3.op_Multiply(bulletShotVec, speed), num2));
              if ((double) num2 >= (double) gravityStartTime)
                vector3_4 = Vector3.op_Addition(vector3_4, Vector3.op_Division(Vector3.op_Multiply(Vector3.op_Multiply(vector3_3, num2 - gravityStartTime), num2 - gravityStartTime), 2f));
              this.bulletLineRenderers[index1].SetPosition(index2, vector3_4);
            }
          }
        }
      }
    }
  }

  public void OnGetRareDrop(REWARD_TYPE type, int item_id)
  {
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnGetRareDrop(type, item_id);
  }

  protected override void OnSendChatMessage(string message)
  {
  }

  protected override void OnSendChatStamp(int stamp_id)
  {
  }

  public override void PlayVoice(int voice_id)
  {
    if (FieldManager.IsValidInTutorial())
      return;
    SoundManager.PlayVoice(this.loader.GetVoiceAudioClip(voice_id), voice_id, ch_id: this.voiceChannel, master: (DisableNotifyMonoBehaviour) this, parent: this.loader.head);
  }

  protected override bool EnablePlaySound() => true;

  public override void ActGuardDamage()
  {
    this.taskChecker.OnGuard();
    if (this._CheckJustGuardSec())
    {
      this.taskChecker.OnJustGuard();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnJustGuard();
    }
    base.ActGuardDamage();
  }

  public override bool ActSpecialAction(bool start_effect = true, bool isSuccess = true)
  {
    this.taskChecker.isEnableAttackCount = true;
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.isEnableAttackCount = true;
    return base.ActSpecialAction(start_effect, isSuccess);
  }

  public override void OnPrayerEnd(Player.PrayInfo prayInfo)
  {
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayInfo.targetId) as Player;
    if (prayInfo.reason == Player.PRAY_REASON.DEAD && Object.op_Inequality((Object) player, (Object) null) && !player.isDead)
      this.taskChecker.OnRevival();
    base.OnPrayerEnd(prayInfo);
  }

  public override void ActGrabbedStart(int enemyId, GrabInfo grabInfo)
  {
    base.ActGrabbedStart(enemyId, grabInfo);
    StageObject enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId);
    if (Object.op_Equality((Object) enemy, (Object) null))
      return;
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, true);
    InGameCameraManager.GrabInfo grabInfo1 = MonoBehaviourSingleton<InGameCameraManager>.I.grabInfo;
    grabInfo1.enabled = true;
    grabInfo1.enemyRoot = enemy._transform;
    grabInfo1.dir = ((Vector3) ref grabInfo.toCameraDir).normalized;
    grabInfo1.distance = grabInfo.cameraDistance;
    grabInfo1.smoothMaxSpeed = grabInfo.smoothMaxSpeed;
    MonoBehaviourSingleton<InGameCameraManager>.I.target = enemy.FindNode(grabInfo.cameraLookAt);
    MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.GRABBED);
  }

  public override void ActGrabbedEnd(float angle, float power)
  {
    base.ActGrabbedEnd(angle, power);
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, false);
    MonoBehaviourSingleton<InGameCameraManager>.I.grabInfo.enabled = false;
    MonoBehaviourSingleton<InGameCameraManager>.I.target = this._transform;
    MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
  }

  private void SendExploreSyncPlayerStatus()
  {
    this.statusSyncTime = Self.STATUS_SYNC_INTERVAL;
    this.syncHp = this.hp;
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncPlayerStatus(this);
  }

  public void CreateInkSplashEffect(Vector3 flickIconPos)
  {
    int num = 10;
    Transform effect1 = EffectManager.GetEffect("ef_btl_pl_blind_01", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
    if (Object.op_Inequality((Object) null, (Object) effect1))
    {
      effect1.localPosition = new Vector3(0.0f, 0.0f, 1f);
      this.inkSplashCtrl_ = ((Component) effect1).GetComponent<EffectCtrl>();
      this.inkSplashEffect_ = ((Component) effect1).gameObject;
      foreach (Renderer componentsInChild in this.inkSplashEffect_.GetComponentsInChildren<Renderer>())
        componentsInChild.sortingOrder = num;
    }
    Transform effect2 = EffectManager.GetEffect("ef_btl_target_flick", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
    if (!Object.op_Inequality((Object) null, (Object) effect2))
      return;
    effect2.localPosition = flickIconPos;
    effect2.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    this.effectFlickWarning_ = ((Component) effect2).gameObject;
    Renderer[] componentsInChildren = this.effectFlickWarning_.GetComponentsInChildren<Renderer>();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      if (num >= componentsInChildren[index].sortingOrder)
        componentsInChildren[index].sortingOrder = num + 1;
    }
    if (!MonoBehaviourSingleton<ScreenOrientationManager>.IsValid() || MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait)
      return;
    effect2.localPosition = Vector3.op_Addition(effect2.localPosition, Self.INKSPLASH_LANDSCAPE_OFFSET);
  }

  public void InkSplashStart(InkSplashInfo info)
  {
    if (this.IsInkSplash())
      return;
    this.inkSplashTimerMax_ = info.duration;
    this.inkSplashTimer_ = this.inkSplashTimerMax_;
    this.inkSplashState_ = 0;
    this.reduceTimeByFlick_ = info.reduceTimeByFlick;
    this.CreateInkSplashEffect(info.flickIconPos);
  }

  public void InkSplashEnd()
  {
    if (!this.IsInkSplash())
      return;
    if (Object.op_Inequality((Object) null, (Object) this.inkSplashEffect_))
    {
      EffectManager.ReleaseEffect(this.inkSplashEffect_);
      this.inkSplashEffect_ = (GameObject) null;
    }
    if (!Object.op_Inequality((Object) null, (Object) this.effectFlickWarning_))
      return;
    EffectManager.ReleaseEffect(this.effectFlickWarning_);
    this.effectFlickWarning_ = (GameObject) null;
  }

  public void UpdateInkSplash()
  {
    this.inkSplashTimer_ -= Time.deltaTime;
    if (Object.op_Equality((Object) null, (Object) this.inkSplashCtrl_))
      return;
    if (this.inkSplashState_ == 0)
    {
      if (2.0 * (double) this.inkSplashTimerMax_ / 3.0 <= (double) this.inkSplashTimer_)
        return;
      this.inkSplashCtrl_.animator.Play("ACT1");
      ++this.inkSplashState_;
    }
    else if (1 == this.inkSplashState_)
    {
      if ((double) this.inkSplashTimerMax_ / 3.0 <= (double) this.inkSplashTimer_)
        return;
      this.inkSplashCtrl_.animator.Play("ACT2");
      ++this.inkSplashState_;
    }
    else
    {
      if (2 != this.inkSplashState_ || 0.0 <= (double) this.inkSplashTimer_)
        return;
      this.InkSplashEnd();
      ++this.inkSplashState_;
    }
  }

  public void ReduceInkSplashTime()
  {
    this.inkSplashTimer_ -= this.reduceTimeByFlick_;
    this.buffParam.ReduceInkSplashTime(this.reduceTimeByFlick_);
    Transform effect = EffectManager.GetEffect("ef_btl_pl_blind_02", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
    if (!Object.op_Inequality((Object) null, (Object) effect))
      return;
    effect.localPosition = new Vector3(0.0f, 0.0f, 1f);
  }

  public override bool IsInkSplash()
  {
    return Object.op_Inequality((Object) null, (Object) this.inkSplashEffect_);
  }

  private void RotateInkSplash(bool is_portrait)
  {
    Transform transform = this.effectFlickWarning_.transform;
    if (is_portrait)
      transform.localPosition = Vector3.op_Subtraction(transform.localPosition, Self.INKSPLASH_LANDSCAPE_OFFSET);
    else
      transform.localPosition = Vector3.op_Addition(transform.localPosition, Self.INKSPLASH_LANDSCAPE_OFFSET);
  }

  private void CreateBlindEffect(float sec)
  {
    this._blindTimer = sec;
    if (Object.op_Inequality((Object) this._blindEffect, (Object) null))
      return;
    this._blindEffect = EffectManager.GetEffect("ef_btl_pl_darkness_02", MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform);
    if (Object.op_Equality((Object) this._blindEffect, (Object) null))
      return;
    this._blindEffect.localPosition = new Vector3(0.0f, 0.0f, 1f);
    foreach (Renderer componentsInChild in ((Component) this._blindEffect).GetComponentsInChildren<Renderer>())
      componentsInChild.sortingOrder = 10;
  }

  public void DestroyBlindEffect()
  {
    if (!Object.op_Inequality((Object) this._blindEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this._blindEffect).gameObject);
    this._blindEffect = (Transform) null;
  }

  public void UpdateBlind()
  {
    if (Object.op_Equality((Object) this._blindEffect, (Object) null))
      return;
    this._blindTimer -= Time.deltaTime;
    if ((double) this._blindTimer > 0.0)
      return;
    this.DestroyBlindEffect();
  }

  private void BeginSpect()
  {
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.DoDisable();
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.DoDisable();
    MonoBehaviourSingleton<UISkillButtonGroup>.I.DoDisable();
    MonoBehaviourSingleton<UISpectatorButton>.I.BeginSpect();
  }

  public bool IsAbleArrowSitShot()
  {
    return this.CheckAttackMode(Player.ATTACK_MODE.ARROW) && (this.spAttackType == SP_ATTACK_TYPE.NONE || this.spAttackType == SP_ATTACK_TYPE.HEAT) && this.enableSpAttackContinue;
  }

  public bool IsActionFromAvoid()
  {
    return (this.actionID == Character.ACTION_ID.MAX || this.actionID == (Character.ACTION_ID) 36 || this.actionID == (Character.ACTION_ID) 46 || this.actionID == (Character.ACTION_ID) 49) && (this.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD || this.playerParameter.twoHandSwordActionInfo.avoidAttackEnable);
  }

  public override void CancelCannonMode()
  {
    if (!this.IsOnCannonMode())
      return;
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.DEFAULT);
    if (this.targetFieldGimmickCannon != null && this.targetFieldGimmickCannon.IsUsing())
      this.targetFieldGimmickCannon.OnLeave();
    if (Object.op_Inequality((Object) this.effectTapTrans, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.effectTapTrans).gameObject);
      this.effectTapTrans = (Transform) null;
    }
    this.SetCannonState(Player.CANNON_STATE.NONE);
    this.targetFieldGimmickCannon = (IFieldGimmickCannon) null;
    this._rigidbody.isKinematic = false;
  }

  public override void SetCannonAimMode()
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
      this.LookAt(MonoBehaviourSingleton<StageObjectManager>.I.boss._position, true);
    if (this.targetFieldGimmickCannon == null)
      return;
    FieldGimmickCannonField fieldGimmickCannon = this.targetFieldGimmickCannon as FieldGimmickCannonField;
    this.cannonAimForward = !Object.op_Inequality((Object) fieldGimmickCannon, (Object) null) ? this._forward : fieldGimmickCannon.GetBaseTransformForward();
    Transform cannonTransform = this.targetFieldGimmickCannon.GetCannonTransform();
    if (Object.op_Inequality((Object) cannonTransform, (Object) null))
    {
      Quaternion localRotation = cannonTransform.localRotation;
      float negativeEuler = this.GetNegativeEuler(((Quaternion) ref localRotation).eulerAngles.x);
      this.baseEulerX = negativeEuler;
      this.cannonAimEuler = new Vector3(negativeEuler, 0.0f, 0.0f);
    }
    Quaternion rotation = this._transform.rotation;
    this.GetNegativeEuler(((Quaternion) ref rotation).eulerAngles.y);
    this.baseInputVec = Vector2.zero;
    this.isBaseInputVec = false;
    this.dragRateX = MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.cannonDragRateX;
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && this.targetFieldGimmickCannon.IsAimCamera())
      MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_AIM);
    if (this.isFirstRideCannon)
    {
      this.CreateCannonTapEffect();
      this.isFirstRideCannon = false;
    }
    this.SetCannonState(Player.CANNON_STATE.READY);
  }

  public override void SetCannonBeamMode()
  {
    if (this.targetFieldGimmickCannon == null)
      return;
    Vector3 position = this.targetFieldGimmickCannon.GetPosition();
    this._rotation = Quaternion.LookRotation(((Vector3) ref position).normalized, Vector3.up);
    this.cannonAimForward = this._forward;
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM_CHARGE);
    this.SetCannonState(Player.CANNON_STATE.READY);
  }

  private void CreateCannonTapEffect()
  {
    Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
    if (Object.op_Equality((Object) cameraTransform, (Object) null))
      return;
    this.effectTapTrans = EffectManager.GetEffect("ef_btl_cannon_tap", cameraTransform);
  }

  public override void UpdateCannonAimMode(Vector2 input_vec, Vector2 input_pos)
  {
    if (!this.isBaseInputVec)
    {
      this.baseInputVec = input_vec;
      this.isBaseInputVec = true;
    }
    float x = input_vec.x;
    float num1 = (double) Mathf.Abs(input_vec.x) - (double) Mathf.Abs(this.baseInputVec.x) > 0.0 ? x - this.baseInputVec.x : 0.0f;
    float num2 = this.dragRateX * Mathf.Sign(num1) * Mathf.Abs(num1);
    if ((double) num2 < -(double) this.overSpeedLimit)
      num2 = -this.overSpeedLimit;
    if ((double) num2 > (double) this.overSpeedLimit)
      num2 = this.overSpeedLimit;
    if ((double) Mathf.Abs(num2) > 0.0)
    {
      if (MonoBehaviourSingleton<SoundManager>.IsValid() && !this.isPlayingRotateSE)
      {
        SoundManager.PlayLoopSE(10000079, (DisableNotifyMonoBehaviour) this, this.targetFieldGimmickCannon.GetTransform());
        this.isPlayingRotateSE = true;
      }
    }
    else if (MonoBehaviourSingleton<SoundManager>.IsValid() && this.isPlayingRotateSE)
    {
      SoundManager.StopLoopSE(10000079, (DisableNotifyMonoBehaviour) this);
      this.isPlayingRotateSE = false;
    }
    this.cannonAimForward = Quaternion.op_Multiply(Quaternion.Euler(new Vector3(0.0f, num2, 0.0f)), this.cannonAimForward);
    float num3 = input_vec.y - this.baseInputVec.y;
    float num4 = (float) (15.0 * -(double) Mathf.Sign(num3)) * Mathf.Abs(num3) + this.baseEulerX;
    if ((double) num4 < -30.0)
      num4 = -30f;
    if ((double) num4 > 10.0)
      num4 = 10f;
    this.cannonAimEuler = new Vector3(num4, 0.0f, 0.0f);
    this._rotation = Quaternion.LookRotation(this.cannonAimForward);
    this._transform.rotation = this._rotation;
  }

  public Vector3 GetCannonShotEuler() => this.cannonAimEuler;

  public void ResetCannonShotAimEuler()
  {
    this.baseEulerX = this.cannonAimEuler.x;
    this.baseInputVec = Vector2.zero;
    this.isBaseInputVec = false;
    if (!MonoBehaviourSingleton<SoundManager>.IsValid())
      return;
    SoundManager.StopLoopSE(10000079, (DisableNotifyMonoBehaviour) this);
    this.isPlayingRotateSE = false;
  }

  private float GetNegativeEuler(float euler) => (double) euler <= 180.0 ? euler : euler - 360f;

  public void ResetShadowSealingUI()
  {
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT))
      MonoBehaviourSingleton<UIEnemyStatus>.I.ShowShadowSealing(true);
    else
      MonoBehaviourSingleton<UIEnemyStatus>.I.ShowShadowSealing(false);
  }

  public void ResetConcussionUI()
  {
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && this.IsOracleTwoHandSword())
      MonoBehaviourSingleton<UIEnemyStatus>.I.ShowConcussion(true);
    else
      MonoBehaviourSingleton<UIEnemyStatus>.I.ShowConcussion(false);
  }

  public override void CheckBuffShadowSealing()
  {
    if (this.isChangingWeapon)
      return;
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT))
    {
      this._EndBuffShadowSealing();
    }
    else
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (boss == null)
        this._EndBuffShadowSealing();
      else if (!boss.IsDebuffShadowSealing())
        this._EndBuffShadowSealing();
      else
        this._StartBuffShadowSealing();
    }
  }

  public bool CheckAvoidAttack()
  {
    if (!this.IsActionFromAvoid() || !this.enableSpAttackContinue)
      return false;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.BURST:
            int avoidAttackId1 = this.playerParameter.ohsActionInfo.burstOHSInfo.AvoidAttackID;
            string motionLayerName1 = this.GetMotionLayerName(this.attackMode, this.spAttackType, avoidAttackId1);
            this.ActAttack(avoidAttackId1, true, false, motionLayerName1, "");
            return true;
          case SP_ATTACK_TYPE.ORACLE:
            int avoidAttackId2 = this.playerParameter.ohsActionInfo.oracleOHSInfo.avoidAttackId;
            string motionLayerName2 = this.GetMotionLayerName(this.attackMode, this.spAttackType, avoidAttackId2);
            this.ActAttack(avoidAttackId2, true, false, motionLayerName2, "");
            return true;
          default:
            return false;
        }
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType == SP_ATTACK_TYPE.NONE || this.spAttackType == SP_ATTACK_TYPE.HEAT)
        {
          this.ActAttack(20, true, false, "", "");
          return true;
        }
        if (this.spAttackType == SP_ATTACK_TYPE.BURST)
        {
          int burstAvoidAttackId = this.playerParameter.twoHandSwordActionInfo.burstTHSInfo.BurstAvoidAttackID;
          string motionLayerName3 = this.GetMotionLayerName(this.attackMode, this.spAttackType, burstAvoidAttackId);
          this.ActAttack(burstAvoidAttackId, true, false, motionLayerName3, "");
          return true;
        }
        if (this.spAttackType != SP_ATTACK_TYPE.ORACLE)
          return false;
        int num1 = 64 /*0x40*/;
        string motionLayerName4 = this.GetMotionLayerName(this.attackMode, this.spAttackType, num1);
        this.ActAttack(num1, true, false, motionLayerName4, "");
        return true;
      case Player.ATTACK_MODE.SPEAR:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
          case SP_ATTACK_TYPE.HEAT:
            this.ActAttack(20, true, false, "", "");
            return true;
          case SP_ATTACK_TYPE.SOUL:
            this.ActAttack(this.playerParameter.spearActionInfo.Soul_AvoidAttackId, true, false, "", "");
            return true;
          case SP_ATTACK_TYPE.ORACLE:
            int avoidAttackId3 = this.playerParameter.spearActionInfo.oracle.avoidAttackId;
            string motionLayerName5 = this.GetMotionLayerName(this.attackMode, this.spAttackType, avoidAttackId3);
            this.ActAttack(avoidAttackId3, true, false, motionLayerName5, "");
            return true;
          default:
            return false;
        }
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.spAttackType == SP_ATTACK_TYPE.NONE || this.spAttackType == SP_ATTACK_TYPE.HEAT)
        {
          this.ActAttack(20, true, false, "", "");
          return true;
        }
        if (this.spAttackType == SP_ATTACK_TYPE.BURST)
        {
          this.ActAttack(21, true, false, "", "");
          return true;
        }
        if (this.spAttackType != SP_ATTACK_TYPE.ORACLE)
          return false;
        int num2 = 45;
        if (this.actionID == (Character.ACTION_ID) 49)
          num2 = 43;
        string motionLayerName6 = this.GetMotionLayerName(this.attackMode, this.spAttackType, num2);
        this.ActAttack(num2, true, false, motionLayerName6, "");
        return true;
      default:
        return false;
    }
  }

  public bool CheckAttackNext()
  {
    if (!this.enableAttackNext || this.attackMode != Player.ATTACK_MODE.PAIR_SWORDS || this.spAttackType != SP_ATTACK_TYPE.SOUL)
      return false;
    this.ActAttack(this.playerParameter.pairSwordsActionInfo.Soul_AttackNextId, true, false, "", "");
    return true;
  }

  public bool CheckWeaponActionForSpAction()
  {
    if (!this.enableWeaponAction)
      return false;
    this.EventWeaponActionStart();
    return true;
  }

  private void UpdateSpearJumpEffect()
  {
    if (this.jumpState > Player.eJumpState.Charge || (double) this.GetChargingRate() < 1.0)
      return;
    EffectCtrl component = this.arrowAimLesserCursorEffect.GetComponent<EffectCtrl>();
    if (component != null)
    {
      int num = this.CheckGaugeLevel();
      component.Play("LEVEL" + (object) num);
    }
    this.jumpState = Player.eJumpState.Charged;
  }

  public override void HitJumpAttack()
  {
    this.taskChecker.OnJump();
    base.HitJumpAttack();
  }

  protected override void _JumpRize()
  {
    this.UseSpGauge();
    this.OnJumpRize(this.arrowAimLesserCursorPos, this.useGaugeLevel);
    if (!this.isArrowAimKeep && this.isArrowAimLesserMode)
      this.SetArrowAimLesserMode(false);
    this.isArrowAimable = false;
    this.isArrowAimKeep = false;
    this.isArrowAimEnd = false;
    MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
  }

  protected override bool StartBoostMode()
  {
    int num = base.StartBoostMode() ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
    {
      this.taskChecker.OnSoulOneHandSword();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulOneHandSword();
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
    {
      this.taskChecker.OnSoulTwoHandSword();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulTwoHandSword();
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.SOUL))
    {
      this.taskChecker.OnSoulSpear();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulSpear();
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST))
    {
      this.taskChecker.OnBurstPairSwords();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnBurstPairSwords();
    }
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
    {
      this.taskChecker.OnSoulArrow();
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnSoulArrow();
    }
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST))
      return num != 0;
    this.taskChecker.OnBurstArrow();
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return num != 0;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnBurstArrow();
    return num != 0;
  }

  public float GetIgnoreTargetHeight()
  {
    if (this.weaponInfo == null)
      return 100f;
    return !((IList<float>) this.weaponInfo.ignoreTargetHeightsByType).IsNullOrEmpty<float>() && (SP_ATTACK_TYPE) this.weaponInfo.ignoreTargetHeightsByType.Length > this.spAttackType && (double) this.weaponInfo.ignoreTargetHeightsByType[(int) this.spAttackType] > 0.0 ? this.weaponInfo.ignoreTargetHeightsByType[(int) this.spAttackType] : this.weaponInfo.ignoreTargetHeight;
  }

  public bool ExecEvolve()
  {
    if (!this.IsEvolveWeapon() || !this.evolveCtrl.IsGaugeFull() || this.evolveCtrl.isExec || !this.IsChangeableAction((Character.ACTION_ID) 37) && !this.EnableActionUseEvolve())
      return false;
    this.ActEvolve();
    return true;
  }

  private bool EnableActionUseEvolve() => this.actionID == (Character.ACTION_ID) 30;

  public void CancelHit()
  {
    if (this.attackID != this.playerParameter.ohsActionInfo.Soul_AlteredSpAttackId)
      return;
    this.snatchCtrl.Cancel();
    this.SetNextTrigger(1);
  }

  public override void OnAvoidHit(StageObject fromObject, AttackHitInfo attackHitInfo)
  {
    if (!(fromObject is Enemy) || !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.ORACLE) || this.actionID != (Character.ACTION_ID) 49)
      return;
    this.pairSwordsCtrl.JustAvoid();
  }

  protected override bool GetTargetPos(out Vector3 pos)
  {
    pos = Vector3.zero;
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || !Object.op_Inequality((Object) this.targetPointWithSpWeak, (Object) null))
      return base.GetTargetPos(out pos);
    pos = this.targetPointWithSpWeak.param.markerPos;
    return true;
  }

  public bool IsChangeableAction(Character.ACTION_ID action_id, int _motionId)
  {
    if (_motionId <= 0)
      return this.IsChangeableAction(action_id);
    if (action_id != Character.ACTION_ID.ATTACK || !this.IsBurstTwoHandSword() || _motionId != this.playerParameter.twoHandSwordActionInfo.burstTHSInfo.FirstReloadActionAttackID)
      return this.IsChangeableAction(action_id);
    return this.thsCtrl != null && this.thsCtrl.IsChangebleReloadAction();
  }

  public bool isMultiLockMax()
  {
    int soulArrowLockNum = this.GetSoulArrowLockNum();
    return MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum() >= soulArrowLockNum;
  }

  private void CheckMultiLock(Vector3 start, Vector3 dir)
  {
    if (!MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      return;
    int soulArrowLockNum = this.GetSoulArrowLockNum();
    int multiLockNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum();
    RaycastHit raycastHit;
    if (multiLockNum >= soulArrowLockNum || !Physics.Raycast(start, dir, ref raycastHit, this.playerParameter.arrowActionInfo.soulRaycastDistance, 16 /*0x10*/))
      return;
    MultiLockMarker component = ((Component) ((RaycastHit) ref raycastHit).transform).GetComponent<MultiLockMarker>();
    if (Object.op_Equality((Object) component, (Object) null) || !component.Lock(multiLockNum, this.isBoostMode))
      return;
    int num = multiLockNum + 1;
    SoundManager.PlayOneShotSE(this.playerParameter.arrowActionInfo.soulLockSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
    int targetMarkerNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerNum();
    if (num < soulArrowLockNum && num < targetMarkerNum)
      return;
    MonoBehaviourSingleton<TargetMarkerManager>.I.HideMultiLock();
    EffectManager.OneShot("ef_btl_wsk_charge_end_01", ((Component) this.FindNode("R_Wep")).transform.position, Quaternion.identity);
    SoundManager.PlayOneShotSE(this.playerParameter.arrowActionInfo.soulLockMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
    this.SetBulletLineColor(true);
  }

  public void CheckMultiLock(MultiLockMarker m)
  {
    if (Object.op_Equality((Object) m, (Object) null) || !MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      return;
    int soulArrowLockNum = this.GetSoulArrowLockNum();
    int multiLockNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum();
    if (multiLockNum >= soulArrowLockNum || !m.Lock(multiLockNum, this.isBoostMode))
      return;
    int num = multiLockNum + 1;
    SoundManager.PlayOneShotSE(this.playerParameter.arrowActionInfo.soulLockSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
    int targetMarkerNum = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerNum();
    if (num < soulArrowLockNum && num < targetMarkerNum)
      return;
    MonoBehaviourSingleton<TargetMarkerManager>.I.HideMultiLock();
    EffectManager.OneShot("ef_btl_wsk_charge_end_01", ((Component) this.FindNode("R_Wep")).transform.position, Quaternion.identity);
    SoundManager.PlayOneShotSE(this.playerParameter.arrowActionInfo.soulLockMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
    this.SetBulletLineColor(true);
  }

  public void SetBulletLineColor(bool isMax)
  {
    Color color = !isMax ? (this.spAttackType == SP_ATTACK_TYPE.SOUL ? this.playerParameter.arrowActionInfo.bulletLineColorSoul : this.playerParameter.arrowActionInfo.bulletLineColor) : this.playerParameter.arrowActionInfo.bulletLineColorSoulFull;
    for (int index = 0; index < this.bulletLineRenderers.Count; ++index)
    {
      this.bulletLineRenderers[index].startColor = color;
      this.bulletLineRenderers[index].endColor = color;
    }
  }

  public void CheckWaveMatchAutoRevive()
  {
    if (!MonoBehaviourSingleton<UISpectatorButton>.IsValid() || !MonoBehaviourSingleton<UISpectatorButton>.I.IsEnable() || MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
      return;
    this.ActDeadStandup(this.hpMax, Player.eContinueType.REACH_NEXT_WAVE);
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.DoEnable();
    MonoBehaviourSingleton<UISpectatorButton>.I.EndSpect();
    MonoBehaviourSingleton<UISkillButtonGroup>.I.DoEnable();
    MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
  }

  private void UpdateAerialCollider()
  {
    if (!this.pairSwordsCtrl.IsUpdateAerialCollider() || !this.isAerial && !this.lastAerialFlag)
      return;
    this.colliderCenter.y = !this.isAerial ? this._capsuleCollider.height * 0.5f : this.loader.socketRoot.localPosition.y;
    this._capsuleCollider.center = this.colliderCenter;
    this.lastAerialFlag = this.isAerial;
  }

  public void SetSpearCursorPos(Vector3 vec) => this.arrowAimLesserCursorPos = vec;

  public override void RainShotChargeRelease()
  {
    base.RainShotChargeRelease();
    Vector3 pos = Vector3.op_Addition(this._transform.position, this.arrowAimLesserCursorPos);
    Quaternion rotation = this._transform.rotation;
    float y = ((Quaternion) ref rotation).eulerAngles.y;
    this.OnRainShotChargeRelease(pos, y);
    if (!this.isArrowAimKeep && this.isArrowAimLesserMode)
      this.SetArrowAimLesserMode(false);
    this.isArrowAimable = false;
    this.isArrowAimKeep = false;
    this.isArrowAimEnd = false;
    MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
  }

  public bool IsAbleArrowRainShot()
  {
    return this.CheckAttackMode(Player.ATTACK_MODE.ARROW) && this.spAttackType == SP_ATTACK_TYPE.BURST && this.enableSpAttackContinue && (this.IsActionFromAvoid() || this.isArrowRainShot);
  }

  public Vector3 GetArrowAimLesserCursorEffect()
  {
    return Object.op_Inequality((Object) this.arrowAimLesserCursorEffect, (Object) null) ? this.arrowAimLesserCursorEffect.transform.position : Vector3.zero;
  }

  public bool isAutoMode => this.controller is AutoSelfController;

  public void SwitchAutoBattle(bool isTurnOn)
  {
    if (isTurnOn && !this.isAutoMode)
    {
      Object.DestroyImmediate((Object) this.controller);
      this.controller = (ControllerBase) ((Component) this).gameObject.AddComponent<AutoSelfController>();
    }
    else
    {
      if (isTurnOn || !this.isAutoMode)
        return;
      Object.DestroyImmediate((Object) this.controller);
      this.controller = (ControllerBase) ((Component) this).gameObject.AddComponent<SelfController>();
    }
  }
}
